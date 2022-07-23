using MovieLibrary.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace MovieLibrary.Core.Extensions
{
    public static class MovieQueryExtension
    {
        public static IEnumerable<Movie> ApplyPagination(this IEnumerable<Movie> Movie, int pageNumber, int itemsPerPage) =>
            Movie.Skip((pageNumber - 1) * itemsPerPage).Take(itemsPerPage);

        public static IEnumerable<Movie> FilterByCategories(this IEnumerable<Movie> movies, int[] categoriesIds) =>
            movies.Where(movie => movie.MovieCategories.Select(x => x.Category).Any(category => categoriesIds.Contains(category.Id)));

        public static IEnumerable<Movie> FilterByMaxImdb(this IEnumerable<Movie> movies, decimal maxImdb) =>
            movies.Where(movie => movie.ImdbRating <= maxImdb);

        public static IEnumerable<Movie> FilterByMinImbd(this IEnumerable<Movie> movies, decimal minImdb) =>
            movies.Where(movie => movie.ImdbRating >= minImdb);

        public static IEnumerable<Movie> FilterByText(this IEnumerable<Movie> movies, string searchText)
            => movies.Where(movie => movie.Title.ToUpper().Contains(searchText.ToUpper()));
    }
}
