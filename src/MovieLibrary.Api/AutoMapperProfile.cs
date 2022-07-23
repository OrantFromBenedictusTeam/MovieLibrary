﻿using AutoMapper;
using MovieLibrary.Api.Controllers.Category.Dto;
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

            CreateMap<BaseCategoryDto, Category>();
            CreateMap<CreateCategoryDto, Category>();
        }
    }
}