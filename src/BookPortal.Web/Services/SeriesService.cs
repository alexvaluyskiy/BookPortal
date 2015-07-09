using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using BookPortal.Web.Domain;
using BookPortal.Web.Domain.Models;
using BookPortal.Web.Domain.Models.Types;
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
            var serieIds = _bookContext.PublisherSeries.Where(c => c.PublisherId == publisherId).Select(c => c.SerieId).ToList();

            return await _bookContext.Series.Where(c => serieIds.Contains(c.Id)).ToListAsync();
        }

        public async Task<SerieResponse> GetSerieAsync(int serieId)
        {
            // get serie information
            var query = from s in _bookContext.Series
                        where s.Id == serieId
                        select new SerieResponse
                        {
                            SerieId = s.Id,
                            Name = s.Name,
                            Description = s.Description,
                            YearOpen = s.YearOpen,
                            YearClose = s.YearClose,
                            SerieClosed = s.SerieClosed
                        };

            var serie = await query.SingleOrDefaultAsync();

            if (serie == null)
                return null;

            // get language information
            if (serie.LanguageId > 0)
            {
                var language = _bookContext.Languages.SingleOrDefault(c => c.Id == serie.LanguageId);
                if (language != null)
                {
                    serie.LanguageId = language.Id;
                    serie.Name = language.Name;
                }
            }

            // get publishers ids
            var publisherIds = _bookContext.PublisherSeries
                .Where(c => c.SerieId == serieId)
                .Select(c => c.PublisherId)
                .ToList();

            // get publisher
            var publishers = _bookContext.Publishers
                .Where(c => publisherIds.Contains(c.Id))
                .Select(c => new PublisherResponse
                {
                    PublisherId = c.Id,
                    Name = c.Name
                }).ToList();

            serie.Publishers = publishers;

            return serie;
        }

        public async Task<IEnumerable<Edition>> GetSerieEditionsAsync(SerieRequest request)
        {
            string sql = @"
                SELECT e.*
                FROM edition_series es INNER JOIN editions e ON e.edition_id = es.edition_id
                WHERE es.serie_id = @serie_id
            ";

            switch (request.Sort)
            {
                case SerieEditionsSort.Name:
                    sql += "ORDER BY e.name, e.year";
                    break;
                case SerieEditionsSort.Authors:
                    sql += "ORDER BY e.authors, es.sort, e.year";
                    break;
                case SerieEditionsSort.Year:
                    sql += "ORDER BY e.year, es.sort, e.name";
                    break;
                default:
                    sql += "ORDER BY e.release_date DESC, es.sort";
                    break;
            }

            List<Edition> editions = new List<Edition>();

            var connection = _bookContext.Database.GetDbConnection() as SqlConnection;
            if (connection != null)
            {
                connection.Open();
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@serie_id", request.SerieId);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var edition = new Edition();
                            edition.Id = reader.GetValue<int>("edition_id");
                            edition.Name = reader.GetValue<string>("name");
                            edition.Year = reader.GetValue<int>("year");
                            edition.CoverType = (EditionCoverType)reader.GetValue<int>("cover_type");
                            edition.Format = reader.GetValue<string>("format");
                            edition.Authors = reader.GetValue<string>("authors");
                            edition.Description = reader.GetValue<string>("description");
                            edition.ReleaseDate = reader.GetValue<DateTime?>("release_date");
                            editions.Add(edition);
                        }
                    }
                }
                connection.Close();
            }

            return editions.Skip(request.Offset).Take(request.Limit);
        }

        public async Task<SerieTreeItem> GetSerieTreeAsync(int serieId)
        {
            List<Serie> seriesTree = new List<Serie>();

            var sql = @"
                DECLARE @parent_serie_id as Int;

                WITH parent AS
                (
                    SELECT s.* FROM series s WHERE s.serie_id = @serie_id
                    UNION ALL
                    SELECT s.* FROM series s JOIN parent AS a ON s.serie_id = a.parent_serie_id
                )
                SELECT TOP 1 @parent_serie_id = serie_id FROM parent WHERE parent_serie_id is NULL;

                WITH tree AS
                (
                    SELECT s.* FROM series s WHERE s.serie_id = @parent_serie_id
                    UNION ALL
                    SELECT s.* FROM series s JOIN tree AS a ON s.parent_serie_id = a.serie_id
                )
                SELECT serie_id, name, parent_serie_id FROM tree";

            var connection = _bookContext.Database.GetDbConnection() as SqlConnection;
            if (connection != null)
            {
                connection.Open();
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@serie_id", serieId);
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
            }

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
