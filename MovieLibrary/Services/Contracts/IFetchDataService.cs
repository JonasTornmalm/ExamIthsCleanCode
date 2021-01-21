using MovieLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieLibrary.Services.Contracts
{
    public interface IFetchDataService
    {
        public IEnumerable<Movie> GetAllMovies();
    }
}
