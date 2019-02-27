using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private IMovieRepository _movieRepository;
        Stopwatch _sw = new Stopwatch();

        public MainPageViewModel(INavigationService navigationService, IPermissionServices permissionServices,
            IOmdbApi omdbApi, IDialogsService dialogsService, IMovieRepository movieRepository,IInsightService insightService)
            : base(navigationService,dialogsService,insightService)
        {
            Title = "Main Page";
            _permissionServices = permissionServices;
            _omdbApi = omdbApi;
            _movieRepository = movieRepository;

            DataList = new List<string> {"hello", "World"};
        }

        public string StopwatchTime { get; set; }

        public ICommand AskPermission
        {
            get
            {
                return new Command(async () =>
                {
                    var permission = Permission.Camera;
                    var status = await _permissionServices.HasPermisssion(permission);
                    UserDialogs.Instance.Alert(permission.ToString() + status);
                });
            }
        }
        public ICommand ItemTappedCommand
        {
            get
            {
                return new Command<object>(async (pressedItem) =>
                {    
                    Debug.WriteLine(pressedItem);
                });
            }
        }
        
        public List<string> DataList {get; set;}
        public ICommand CallService
        {
            get
            {
                return new Command(async () =>
                {
                   _sw.Start();
                    var data = await _movieRepository.GetMovie("a");
                    StopwatchTime = StopwatchTime + ( _sw.ElapsedMilliseconds.ToString() + ",");
                    _sw.Reset();
//                    await _dialogsService.AlertAsync(data);
                });
            }
        }
    }
}