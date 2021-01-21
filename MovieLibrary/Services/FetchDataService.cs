using Microsoft.Extensions.Options;
using MovieLibrary.DTOs;
using MovieLibrary.Infrastructure;
using MovieLibrary.Services.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MovieLibrary.Services
{
    public class FetchDataService : IFetchDataService
    {
        private readonly string topListUrl;
        private readonly string detailedMoviesUrl;
        public FetchDataService(IOptions<EndPointOptions> options)
        {
            topListUrl = options.Value.Toplist;
            detailedMoviesUrl = options.Value.DetailedMovies;
        }

        public IEnumerable<Movie> GetAllMovies()
        {
            var moviesFromTop100 = GetMoviesFromTop100();
            var moviesFromDetailedMovies = GetMoviesFromDetailedMovies();
            var allMovies = moviesFromTop100.Concat(moviesFromDetailedMovies);
            return allMovies;
        }

        private IEnumerable<Movie> GetMoviesFromDetailedMovies()
        {
            var json = GetJsonFromExternalApi(detailedMoviesUrl);
            var detailedMovies = JsonConvert.DeserializeObject<List<DetailedMovieRoot>>(json);
            var movies = MapDetailedMoviesToMovieDTO(detailedMovies);
            return movies;
        }

        private IEnumerable<Movie> GetMoviesFromTop100()
        {
            var json = GetJsonFromExternalApi(topListUrl);
            var movies = JsonConvert.DeserializeObject<List<Movie>>(json, new JsonSerializerSettings
            {
                Culture = new System.Globalization.CultureInfo("se-SV")
            });
            return movies;
        }

        private string GetJsonFromExternalApi(string url)
        {
            var client = new HttpClient();
            var httpResponse = client.GetAsync(url).Result;
            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Something went wrong when fetching the movie data");
            }
            var content = httpResponse.Content.ReadAsStream().ToString();
            if (content is null)
            {
                throw new Exception("No data in response from external API");
            }
            return new StreamReader(httpResponse.Content.ReadAsStream()).ReadToEnd();
        }

        private IEnumerable<Movie> MapDetailedMoviesToMovieDTO(List<DetailedMovieRoot> detailedMovies)
        {
            return detailedMovies.Select(m => new Movie { Id = m.id, Rated = m.imdbRating, Title = m.title });
        }
    }
}
