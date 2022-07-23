namespace MovieLibrary.Api.Controllers.Movie.Dto
{
    public class CreateMovieDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int Year { get; set; }

        public decimal ImdbRating { get; set; }
    }
}
