using System.Collections.Generic;

namespace MovieLibrary.Data.Entities
{
    public class Movie: Entity
    {
        public Movie()
        {
            MovieCategories = new List<MovieCategory>();
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Year { get; set; }

        public decimal ImdbRating { get; set; }

        public virtual ICollection<MovieCategory> MovieCategories { get; set; }
    }
}
