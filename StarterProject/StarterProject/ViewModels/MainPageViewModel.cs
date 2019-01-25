using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Acr.UserDialogs;
using Plugin.Permissions.Abstractions;
using StarterProject.Interfaces;
using Xamarin.Forms;

namespace StarterProject.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private IPermissionServices _permissionServices;
        private IOmdbApi _omdbApi;
        private IDialogsService _dialogsService;
        public MainPageViewModel(INavigationService navigationService,IPermissionServices permissionServices,IOmdbApi omdbApi,IDialogsService dialogsService)
            : base(navigationService)
        {
            Title = "Main Page";
            _permissionServices = permissionServices;
            _omdbApi = omdbApi;
            _dialogsService = dialogsService;
        }

        public ICommand AskPermission
        {
            get { return new Command(async () =>
            {
                var permission = Permission.Camera;
                var status = await _permissionServices.HasPermisssion(permission);
                UserDialogs.Instance.Alert(permission.ToString() + status);
            }); }
        }

        public ICommand CallService
        {
            get
            {
                return new Command(async () =>
                {
                    var data = await _omdbApi.GetMoviesByTitle("a");
                    await _dialogsService.AlertAsync(data);
                   
                });
            }
        }
    }
}