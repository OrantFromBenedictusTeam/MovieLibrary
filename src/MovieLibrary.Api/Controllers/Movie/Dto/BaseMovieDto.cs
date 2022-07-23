namespace MovieLibrary.Api.Controllers.Movie.Dto
{
    public abstract class BaseMovieDto 
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public int Year { get; set; }

        public decimal ImdbRating { get; set; }
    }
}
