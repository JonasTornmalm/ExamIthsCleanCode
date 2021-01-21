using MovieLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieLibrary.Services.Contracts
{
    public interface IMovieService
    {
        public IEnumerable<Movie> GetSortedTopList(bool ascendingOrder);
        public IEnumerable<Movie> RemoveDuplicates(IOrderedEnumerable<Movie> movies);
        public Movie GetMovieById(string id);
    }
}
