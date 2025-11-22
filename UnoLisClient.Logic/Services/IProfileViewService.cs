using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnoLisClient.Logic.Models;
using UnoLisClient.Logic.UnoLisServerReference.ProfileView;
using UnoLisServer.Common.Models;

namespace UnoLisClient.Logic.Services
{
    /// <summary>
    /// Interface for dependency injection in ProfileViewService
    /// </summary>
    public interface IProfileViewService
    {
        Task<ServiceResponse<ProfileData>> GetProfileDataAsync(string nickname);
    }
}
