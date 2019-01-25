using System.Threading.Tasks;
using Refit;

namespace StarterProject.Interfaces
{
    public interface IOmdbApi
    {
        [Get("/?t={title}")]
        Task<string> GetMoviesByTitle(string title,[Query(AppConstants.OmdbKey)]string apikey = AppConstants.OmdbKey );
    }
}