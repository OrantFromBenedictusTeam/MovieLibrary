using AutoMapper;
using MovieLibrary.Api.Controllers.Category.Dto;
using MovieLibrary.Api.Controllers.Filter.Dto;
using MovieLibrary.Api.Controllers.Movie.Dto;
using MovieLibrary.Data.Entities;
using System.Linq;

namespace MovieLibrary.Api
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Category, CreateCategoryDto>();
            CreateMap<Category, UpdateCategoryDto>();
            CreateMap<Category, GetCategoryDto>();
            CreateMap<CreateCategoryDto, Category>();

            CreateMap<Movie, CreateMovieDto>();
            CreateMap<Movie, UpdateMovieDto>();
            CreateMap<Movie, GetMovieDto>();
            CreateMap<CreateMovieDto, Movie>();

            CreateMap<Movie, MovieWithCategoriesDto>().ForMember(
                                dto => dto.Categories,
                opt => opt.MapFrom(entity => entity.MovieCategories.Select(movieCategory => movieCategory.Category).ToList()));
        }
    }
}