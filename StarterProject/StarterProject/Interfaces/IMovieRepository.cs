using System.Threading.Tasks;

namespace StarterProject.Interfaces
{
    public interface IMovieRepository
    {
        Task<string> GetMovie(string Title);
    }
}