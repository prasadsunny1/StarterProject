using System.Threading.Tasks;

namespace StarterProject.Interfaces
{
    public interface IDialogsService
    {
        void Alert(string message, string title = null, string closeButton = null);

        Task AlertAsync(string message, string title = null, string closeButton = null);

        Task<bool> QuestionAsync(string message, string title = null, string acceptButton = null, string cancelButton = null);

        void ShowLoading(string title);

        void HideLoading();
    }
}