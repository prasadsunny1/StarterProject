using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using MonkeyCache.LiteDB;
using Polly;
using StarterProject.Interfaces;
using Xamarin.Essentials;

namespace StarterProject.Services
{
    public class MovieRepository : IMovieRepository
    {
        private IOmdbApi _omdbApi;

        public MovieRepository(IOmdbApi omdbApi)
        {
            _omdbApi = omdbApi;
        }

        public async Task<string> GetMovie(string title)
        {
            var key = $"Movies_{title}";

//            disconnected and cache available
            if (Connectivity.NetworkAccess != NetworkAccess.Internet && Barrel.Current.Exists(key))
            {
                return Barrel.Current.Get<string>(key);
            }

//            connected and cache available
            if (!Barrel.Current.IsExpired(key) && Barrel.Current.Exists(key))
            {
                return Barrel.Current.Get<string>(key);
            }

            var movies = await Policy
                .Handle<WebException>()
                .Or<HttpRequestException>()
                .Or<TimeoutException>()
                .WaitAndRetryAsync
                (
                    retryCount: 3,
                    sleepDurationProvider: retryAttempt =>
                    {
                        Debug.WriteLine($"Attempt {retryAttempt}");
                        return TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
                    }).ExecuteAsync(async () => await _omdbApi.GetMoviesByTitle(title));

            Barrel.Current.Add(key, movies, TimeSpan.FromSeconds(10));
            return movies;
        }
    }
}