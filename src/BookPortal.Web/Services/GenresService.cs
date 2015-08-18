using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookPortal.Core.Framework.Models;
using BookPortal.Web.Models.Responses;
using BookPortal.Web.Repositories;
using System.Linq;
using Microsoft.Framework.OptionsModel;

namespace BookPortal.Web.Services
{
    public class GenresService
    {
        private readonly GenresRepository _genresRepository;
        private readonly IOptions<AppSettings> _options;

        public GenresService(GenresRepository genresRepository, IOptions<AppSettings> options)
        {
            _genresRepository = genresRepository;
            _options = options;
        }

        public async Task<ApiObject<GenrePersonResponse>> GetAuthorGenres(int personId)
        {
            var genres =  await _genresRepository.GetAuthorGenres(personId);

            int genreLimit = _options.Options.PersonGenreLimit > 0 ? _options.Options.PersonGenreLimit : 5;

            var genreTotal = genres.Sum(c => c.GenreCount);
            genres.ForEach(c => c.GenreTotal = genreTotal);

            return new ApiObject<GenrePersonResponse>(genres.Take(genreLimit));
        }

        public async Task<ApiObject<GenreWorkResponse>> GetWorkGenres(int workId)
        {
            List<GenreWorkResponse> genres = await _genresRepository.GetWorkGenres(workId);
            int totalVoters = await _genresRepository.GetWorkGenresTotalVoters(workId);

            var tree = GetWorkGenresTree(genres, null, totalVoters);

            return new ApiObject<GenreWorkResponse>(tree);
        }

        private List<GenreWorkResponse> GetWorkGenresTree(List<GenreWorkResponse> list, int? parent, int totalVoters)
        {
            var tempList = list.Where(x => x.GenreParentWorkId == parent && WorkGenresFormula(x.GenreCount, totalVoters)).Select(x => new GenreWorkResponse
            {
                GenreWorkId = x.GenreWorkId,
                Name = x.Name,
                GenreCount = x.GenreCount,
                GenreTotal = totalVoters,
                GenreWorkGroupId = parent == null ? x.GenreWorkGroupId : null,
                Genres = GetWorkGenresTree(list, x.GenreWorkId, totalVoters)
            }).ToList();

            return tempList.Count > 0 ? tempList : null;
        }

        private bool WorkGenresFormula(int votesCount, int votersCount)
        {
            return Math.Round(votesCount * 10.0 / votersCount) >= 5;
        }
    }
}
