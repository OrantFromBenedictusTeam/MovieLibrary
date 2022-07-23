using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieLibrary.Api.Common;
using MovieLibrary.Api.Controllers.Filter.Dto;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using MovieLibrary.Core.Extensions;

namespace MovieLibrary.Api.Controllers.Filter
{
    [ApiController]
    [Route("/v1/Movie/[controller]")]
    public class FilterController : ControllerBase
    {
        private readonly IRepository<Data.Entities.Movie> _movieRepository;
        private readonly IMapper _mapper;

        public FilterController(IRepository<Data.Entities.Movie> movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public IEnumerable<MovieWithCategoriesDto> GetFilteredMovies([FromBody] FilterMovieRequestDto requestDto)
        {
            var result = _movieRepository
                .GetAll(config => config.Include(mc => mc.MovieCategories).ThenInclude(category => category.Category));

            ValidateRequest(requestDto);

            if (IsSearchStringGiven(requestDto))
            {
                result = result.FilterByText(requestDto.SearchText);
            }

            if (requestDto.MinImdb.HasValue)
            {
                result = result.FilterByMinImbd(requestDto.MinImdb.Value);
            }

            if (requestDto.MaxImdb.HasValue)
            {
                result = result.FilterByMaxImdb(requestDto.MaxImdb.Value);
            }

            if (requestDto.CategoriesIds?.Any() ?? false)
            {
                result = result.FilterByCategories(requestDto.CategoriesIds);
            }

            if (requestDto.PageNumber.HasValue)
            {
                result = result.ApplyPagination(requestDto.PageNumber.Value, requestDto.ItemsPerPage.Value);
            }

            return result.OrderByDescending(movie => movie.ImdbRating).Select(movie => _mapper.Map<MovieWithCategoriesDto>(movie));

            static bool IsSearchStringGiven(FilterMovieRequestDto requestDto) =>
                string.IsNullOrWhiteSpace(requestDto.SearchText) == false;

            static void ValidateRequest(FilterMovieRequestDto requestDto)
            {
                if ((requestDto.PageNumber == null && requestDto.ItemsPerPage != null) ||
                    (requestDto.PageNumber != null && requestDto.ItemsPerPage == null))
                {
                    throw new ArgumentException($"Parameters {nameof(requestDto.PageNumber)} and {nameof(requestDto.ItemsPerPage)} both must have values or both must not have values in the same request.");
                }
            }
        }
    }
}
