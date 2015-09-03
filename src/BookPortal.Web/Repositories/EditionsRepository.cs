using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BookPortal.Core.Framework.Models;
using BookPortal.Web.Domain;
using BookPortal.Web.Domain.Models;
using BookPortal.Web.Domain.Models.Types;
using BookPortal.Web.Models;
using BookPortal.Web.Models.Requests;
using BookPortal.Web.Models.Responses;
using Dapper;

namespace BookPortal.Web.Repositories
{
    public class EditionsRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public EditionsRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<List<EditionResponse>> GetEditionsBySerieAsync(int serieId)
        {
            var editionsSql = @"
                SELECT e.edition_id as 'editionid', e.name, e.year, e.type, e.authors, e.description, e.correct
                FROM edition_series es
                INNER JOIN editions e ON es.edition_id = e.edition_id
                WHERE es.serie_id = @serieId";

            var connection = _connectionFactory.GetDbConnection;
            return (await connection.QueryAsync<EditionResponse>(editionsSql, new { serieId })).ToList();
        }
    }
}
