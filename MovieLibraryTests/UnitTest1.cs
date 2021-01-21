using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MovieLibrary.DTOs;
using MovieLibrary.Services;
using MovieLibrary.Services.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace MovieLibraryTests
{
    [TestClass]
    public class UnitTest1
    {
        private Mock<IFetchDataService> _fetchDataService;
        [TestMethod]
        public void GetSortedTopList_AscendingTest()
        {
            _fetchDataService = new Mock<IFetchDataService>();
            _fetchDataService.Setup(x => x.GetAllMovies()).Returns(new List<Movie>()
            {
                new Movie(){Id = "1", Rated = 10, Title = "testMovie1"},
                new Movie(){Id = "2", Rated = 9, Title = "testMovie2"},
                new Movie(){Id = "3", Rated = 8, Title = "testMovie3"},
                new Movie(){Id = "4", Rated = 7, Title = "testMovie4"}
            });

            var movieService = new MovieService(_fetchDataService.Object);

            var expected = "testMovie4";

            var actual = movieService.GetSortedTopList(true);

            Assert.AreEqual(expected, actual.First().Title);
        }
    }
}
