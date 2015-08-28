using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookPortal.Web.Domain;
using BookPortal.Web.Domain.Models.Types;
using BookPortal.Web.Models.Responses;

namespace BookPortal.Web.Repositories
{
    public class PersonsRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public PersonsRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<Dictionary<int, List<PersonResponse>>> GetPersonsByWorksIdsAsync(IEnumerable<int> workIds)
        {
            var connection = _connectionFactory.GetDbConnection;
            var peopleSql = @"
                    SELECT pw.work_id as 'WorkId', p.person_id as 'PersonId', p.name as 'Name', pw.type as 'PersonType'
                    FROM persons AS p
                    INNER JOIN person_works AS pw ON p.person_id = pw.person_id
                    WHERE pw.work_id IN @workIds";

            var people = await connection.QueryAsync(() => new
            {
                WorkId = default(int),
                PersonId = default(int),
                Name = default(string),
                PersonType = default(WorkPersonType)
            }, peopleSql, new { workIds });

            var peopleDic = people.GroupBy(c => c.WorkId).ToDictionary(c => c.Key, c => c.Select(d => new PersonResponse
            {
                PersonId = d.PersonId,
                Name = d.Name,
                PersonType = d.PersonType
            }).ToList());

            return peopleDic;
        }
    }
}
