using MovieLibrary.Api.Controllers.Category.Dto;
using System.Collections.Generic;

namespace MovieLibrary.Api.Controllers.Filter.Dto
{
    public class MovieWithCategoriesDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int Year { get; set; }

        public decimal ImdbRating { get; set; }

        public List<GetCategoryDto> Categories { get; set; }
    }
}
