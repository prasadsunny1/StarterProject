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
        public async Task<bool> HasPermisssion(Permission permission)
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(permission);
                if (status != PermissionStatus.Granted)
                {
                    var shouldShowRationale =
                        await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(permission);
                    if (shouldShowRationale)
                    {
//                    await DisplayAlert("We need to access this permission"+ permission.ToString(), "OK");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(permission);
                    //Best practice to always check that the key exists
                    if (results.ContainsKey(permission))
                        status = results[permission];
                }


                if (status == PermissionStatus.Granted)
                {
                    return true;
                }
                else
                {
//                    show dialog Unable to get permission
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }
    }
}