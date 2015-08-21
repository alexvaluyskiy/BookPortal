using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookPortal.Web.Domain;
using BookPortal.Web.Models.Responses;
using Dapper;
using Microsoft.Framework.Caching.Memory;

namespace BookPortal.Web.Repositories
{
    public class GenresRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly IMemoryCache _memoryCache;

        public GenresRepository(IConnectionFactory connectionFactory, IMemoryCache memoryCache)
        {
            _connectionFactory = connectionFactory;
            _memoryCache = memoryCache;
        }

        public async Task<List<GenreWorksGroupResponse>> GetGenreWorksGroupsAsync()
        {
            List<GenreWorksGroupResponse> value;
            string cacheKey = "genre:work:sgroups";

            if (!_memoryCache.TryGetValue(cacheKey, out value))
            {
                const string sql = "SELECT genre_work_group_id as 'GenreWorkGroupId', name, [level] FROM genre_works_groups";

                var connection = _connectionFactory.GetDbConnection;
                var genreworkgroup = await connection.QueryAsync<GenreWorksGroupResponse>(sql);

                value = genreworkgroup.ToList();
                _memoryCache.Set(cacheKey, value, new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1) });
            }

            return value;
        }

        public async Task<List<GenrePersonResponse>> GetAuthorGenres(int personId)
        {
            const string sql = @"
                SELECT
                    gw.genre_work_id as 'GenreWorkId', gw.name, gpv.genre_count as 'GenreCount'
                FROM
                    genre_persons_view AS gpv
                    INNER JOIN genre_works AS gw ON gpv.genre_work_id = gw.genre_work_id
                WHERE
                    gpv.person_id = @personId
                ORDER BY
                    gpv.genre_count DESC";

            var connection = _connectionFactory.GetDbConnection;
            var genres = await connection.QueryAsync<GenrePersonResponse>(sql, new { personId });

            return genres.ToList();
        }

        public async Task<List<GenreWorkResponse>> GetWorkGenres(int workId)
        {
            const string sql = @"
                SELECT
                    gw.genre_work_id as 'GenreWorkId', gw.parent_genre_work_id as 'GenreParentWorkId', gw.name, gw.level,
                    gwv.genre_count as 'GenreCount', gw.genre_work_group_id as 'GenreWorkGroupId'
                FROM
                    genre_works_view AS gwv
                    INNER JOIN genre_works AS gw ON gwv.genre_work_id = gw.genre_work_id
                    INNER JOIN genre_works_groups as gwg ON gw.genre_work_group_id = gwg.genre_work_group_id
                WHERE
                    gwv.work_id = @workId
                ORDER BY
                    gwg.level, gwv.genre_count DESC, gw.level";

            var connection = _connectionFactory.GetDbConnection;
            var query = await connection.QueryAsync<GenreWorkResponse>(sql, new { workId });

            return query.ToList();
        }

        public async Task<int> GetWorkGenresTotalVoters(int workId)
        {
            const string sql = "SELECT COUNT(DISTINCT user_id) FROM genre_work_users WHERE work_id = @workId";

            var connection = _connectionFactory.GetDbConnection;
            var query = await connection.QueryAsync<int>(sql, new { workId });

            return query.SingleOrDefault();
        }
    }
}
