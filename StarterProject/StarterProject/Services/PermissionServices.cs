using Plugin.Permissions.Abstractions;
using StarterProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Plugin.Permissions;

namespace StarterProject.Services
{
    public class PermissionServices : IPermissionServices
    {
        IDialogsService _dialogsService;
        public PermissionServices(IDialogsService dialogsService)
        {
            _dialogsService = dialogsService;
        }

        public async Task<bool> HasPermisssion(Permission permission)
        {
            if (!(await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(permission)))
            {
            await CrossPermissions.Current.RequestPermissionsAsync(permission);
            }
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(permission);

                if (status != PermissionStatus.Granted)
                {
                   await RequestCameraPermissionAsync(permission);
                }
                status = await CrossPermissions.Current.CheckPermissionStatusAsync(permission);
                return status == PermissionStatus.Granted;
        }

        async Task RequestCameraPermissionAsync(Permission permission)
        {
            var shouldShowRathional = await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(permission);
            if (! shouldShowRathional)
            {
                var openSettings = await _dialogsService.QuestionAsync("We need this permission to do that.Please allow it from Settings>Permissions", AppResources.AppName, "Open Settings", "Later");
                if (openSettings)
                {
                    CrossPermissions.Current.OpenAppSettings();
                }               
            }
            else
            {
                var responce = await _dialogsService.QuestionAsync("Displaying Rationale", AppResources.AppName, "Grant", "Cancel");
                if (responce)
                {
                var permissionInfo = await CrossPermissions.Current.RequestPermissionsAsync(permission);
                }
            }
        }
    }
}