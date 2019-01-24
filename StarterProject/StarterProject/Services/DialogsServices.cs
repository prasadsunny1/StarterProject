using System.Threading.Tasks;
using Acr.UserDialogs;
using StarterProject.Interfaces;

namespace StarterProject.Services
{
    public class DialogsServices : IDialogsService
    {
        public DialogsServices()
        {

        }

        public void Alert(string message, string title = null, string closeButton = null)
        {
            if (string.IsNullOrEmpty(closeButton))
                closeButton = AppResources.OK;
            UserDialogs.Instance.Alert(message, title, closeButton);
        }

        public Task AlertAsync(string message, string title = null, string closeButton = null)
        {
            if (string.IsNullOrEmpty(closeButton))
                closeButton = AppResources.OK;

            return UserDialogs.Instance.AlertAsync(message, title, closeButton);
        }

        public Task<bool> QuestionAsync(string message, string title = null, string acceptButton = null, string cancelButton = null)
        {
            if (string.IsNullOrEmpty(acceptButton))
                acceptButton = AppResources.OK;

            if (string.IsNullOrEmpty(cancelButton))
                acceptButton = AppResources.Cancel;

            return UserDialogs.Instance.ConfirmAsync(message, title, acceptButton, cancelButton);
        }

        public void ShowLoading(string title)
        {
            UserDialogs.Instance.ShowLoading(title);
        }

        public void HideLoading()
        {
            UserDialogs.Instance.HideLoading();
        }
    }
}