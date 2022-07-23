using AutoMapper;
using MovieLibrary.Api.Controllers.Category.Dto;
using MovieLibrary.Api.Controllers.Movie.Dto;
using MovieLibrary.Data.Entities;

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
        }
    }
}