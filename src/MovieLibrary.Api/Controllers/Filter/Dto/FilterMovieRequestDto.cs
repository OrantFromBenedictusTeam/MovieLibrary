namespace MovieLibrary.Api.Controllers.Filter.Dto
{
    public class FilterMovieRequestDto: PagingRequestDto
    {
        public string SearchText { get; set; }
        public decimal? MinImdb { get; set; }
        public decimal? MaxImdb { get; set; }
        public int[] CategoriesIds { get; set; }
    }
}
