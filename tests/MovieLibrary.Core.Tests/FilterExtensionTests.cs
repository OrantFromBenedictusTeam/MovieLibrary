using Moq;
using MovieLibrary.Api.Common;
using MovieLibrary.Data.Entities;
using NUnit.Framework;
using MovieLibrary.Core.Extensions;
using System.Collections.Generic;
using Shouldly;
using System.Linq;

namespace MovieLibrary.Core.Tests
{

    [TestFixture]
    public class FilterExtensionTests
    {
        [Test]
        public void FilterByText_OneTitleIncludesSearchText_ReturnOneMovie()
        {
            //Arrange
            var movieRepository = GetMovieReopository();

            //Act
            var result = movieRepository.GetAll().FilterByText("Wściekli 2");

            //Assert
            result.Count().ShouldBe(1);
        }

        [Test]
        public void FilterByImdb_OneMovieHasImdbBetweenGinven_ReturnOneMovie()
        {
            //Arrange
            var movieRepository = GetMovieReopository();

            //Act
            var result = movieRepository.GetAll().FilterByMinImbd(0.9M).FilterByMaxImdb(1.9M);

            //Assert
            result.Count().ShouldBe(1);

        }

        [Test]
        public void FilterByCategories_MoviesForGivenTwoIdsExist_ReturnSomeMovies()
        {
            //Arrange
            var movieRepository = GetMovieReopository();

            //Act
            var allMovies = movieRepository.GetAll();
            var result = allMovies.FilterByCategories(new int[] { 1, 2 });

            //Assert
            result.Count().ShouldBe(2);

        }

        //And so on... 

        private IRepository<Movie> GetMovieReopository()
        {
            var movies = Enumerable.Range(1, 10).Select(i => new Movie
            {
                Id = 1,
                Title = $"Szybcy i Wściekli {i}",
                Description = $"Film jak wiele innych",
                ImdbRating = i,
                MovieCategories = new List<MovieCategory> {
                    new MovieCategory
                    {
                        Category = new Category { Name = $"Kategoria {i}", Id = i}
                    },
                    new MovieCategory
                    {
                      Category = new Category { Name = $"Kategoria {i+1}", Id = i+1}
                    }
                }
            }); ;
            var repositoryMock = new Mock<IRepository<Movie>>();
            repositoryMock.Setup(x => x.GetAll()).Returns(movies);
            return repositoryMock.Object;
        }
    }
}