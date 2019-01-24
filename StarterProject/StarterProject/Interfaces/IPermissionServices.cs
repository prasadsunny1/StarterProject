using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StarterProject.Interfaces
{
    public interface IPermissionServices
    {
        Task<bool> HasPermisssion(Permission permission);
    }
}
