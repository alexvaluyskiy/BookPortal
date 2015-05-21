using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using BookPortal.Web.Domain;
using BookPortal.Web.Domain.Models;
using BookPortal.Web.Models;
using Microsoft.Data.Entity;

namespace BookPortal.Web.Services
{
    public class SeriesService
    {
        private readonly BookContext _bookContext;

        public SeriesService(BookContext bookContext)
        {
            _bookContext = bookContext;
        }

        public async Task<IReadOnlyList<Serie>> GetSeriesAsync(int publisherId)
        {
            return await _bookContext.Series.Where(c => c.PublisherId == publisherId).ToListAsync();
        }

        public async Task<Serie> GetSerieAsync(int serieId)
        {
            return await _bookContext.Series.Where(c => c.Id == serieId).SingleOrDefaultAsync();
        }

        public async Task<SerieTreeItem> GetSerieTreeAsync(int serieId)
        {
            List<Serie> seriesTree = new List<Serie>();

            // find the root of the tree
            var rootSerieSql = @"
                WITH parent AS
                (
	                SELECT s.* FROM series s WHERE s.serie_id = @serieid
	                UNION ALL
	                SELECT s.* FROM series s JOIN parent AS a ON s.serie_id = a.parent_serie_id
                )
                SELECT TOP 1 serie_id FROM parent WHERE parent_serie_id is NULL";

            int rootSerieId;

            var connection = _bookContext.Database.AsSqlServer().Connection.DbConnection as SqlConnection;
            connection.Open();
            using (var command = new SqlCommand(rootSerieSql, connection))
            {
                command.Parameters.AddWithValue("@serieid", serieId);
                object result = await command.ExecuteScalarAsync();
                rootSerieId = (int)(result ?? 0);
            }

            if (rootSerieId == 0)
                return null;

            // find all items in the tree
            var treeSql = @"
                WITH tree AS
                (
                    SELECT s.* FROM series s WHERE s.serie_id = @serieid
                    UNION ALL
                    SELECT s.* FROM series s JOIN tree AS a ON s.parent_serie_id = a.serie_id
                )
                SELECT serie_id, name, parent_serie_id FROM tree";

            using (var command = new SqlCommand(treeSql, connection))
            {
                command.Parameters.AddWithValue("@serieid", rootSerieId);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var serie = new Serie();
                        serie.Id = reader.GetValue<int>("serie_id");
                        serie.Name = reader.GetValue<string>("name");
                        serie.ParentSerieId = reader.GetValue<int?>("parent_serie_id");
                        seriesTree.Add(serie);
                    }
                }
            }
            connection.Close();

            SerieTreeItem tree = GetSerieTree(seriesTree, null).SingleOrDefault();

            return tree;
        }

        private List<SerieTreeItem> GetSerieTree(List<Serie> list, int? parent)
        {
            var tempList = list.Where(x => x.ParentSerieId == parent).Select(x => new SerieTreeItem
            {
                Id = x.Id,
                Name = x.Name,
                Series = GetSerieTree(list, x.Id)
            }).ToList();

            return tempList.Count > 0 ? tempList : null;
        }
    }
}
