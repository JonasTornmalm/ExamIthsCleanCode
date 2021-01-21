using MovieLibrary.DTOs;
using MovieLibrary.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieLibrary.Services
{
    public class MovieService : IMovieService
    {
        private readonly IFetchDataService _fetchDataService;

        public MovieService(IFetchDataService dataFetcher)
        {
            _fetchDataService = dataFetcher;
        }

        public IEnumerable<Movie> GetSortedTopList(bool ascendingOrder)
        {
            var allMoviesList = _fetchDataService.GetAllMovies();
            if (ascendingOrder)
            {
                return RemoveDuplicates(allMoviesList.ToList().OrderBy(x => x.Rated));
                
            }
            else
            {
                return RemoveDuplicates(allMoviesList.ToList().OrderByDescending(x => x.Rated));
            }
        }

        public IEnumerable<Movie> RemoveDuplicates(IOrderedEnumerable<Movie> movies)
        {
            return movies.Distinct();
        }

        public Movie GetMovieById(string id)
        {
            var movie = _fetchDataService.GetAllMovies().Where(x => x.Id == id).FirstOrDefault();

            return movie;
        }
    }
}
