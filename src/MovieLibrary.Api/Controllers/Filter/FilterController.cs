using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieLibrary.Api.Common;
using MovieLibrary.Api.Controllers.Filter.Dto;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

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
                .GetAll(config => config.Include(mc => mc.MovieCategories).ThenInclude(category => category.Category))
                .OrderByDescending(movie => movie.ImdbRating)
                .Select(movie => _mapper.Map<MovieWithCategoriesDto>(movie));

            if ((requestDto.PageNumber == null && requestDto.ItemsPerPage != null) ||
                (requestDto.PageNumber != null && requestDto.ItemsPerPage == null))
            {
                throw new ArgumentException($"Parameters {nameof(requestDto.PageNumber)} and {nameof(requestDto.ItemsPerPage)} both must have values or both must not have values in the same request.");
            }

            if (string.IsNullOrWhiteSpace(requestDto.SearchText) == false)
            {
                result = result.Where(movie => movie.Title.ToUpper().Contains(requestDto.SearchText.ToUpper()));
            }

            if(requestDto.MinImdb.HasValue)
            {
                result = result.Where(movie => movie.ImdbRating >= requestDto.MinImdb);
            }

            if(requestDto.MaxImdb.HasValue)
            {
                result = result.Where(movie => movie.ImdbRating <= requestDto.MaxImdb);
            }

            if(requestDto.CategoriesIds.Any())
            {
                result = result.Where(movie => movie.Categories.Any(category => requestDto.CategoriesIds.Contains(category.Id)));
            }

            if(requestDto.PageNumber.HasValue)
            {
                result = result.Skip((requestDto.PageNumber.Value - 1) * requestDto.ItemsPerPage.Value).Take(requestDto.ItemsPerPage.Value);
            }

            return result;
        }
    }
}
