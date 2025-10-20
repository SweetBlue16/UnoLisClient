using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnoLisClient.UI.UnoLisServerReference.ProfileView;

namespace UnoLisClient.UI.Managers
{
    public static class CurrentSession
    {
        public static string CurrentUserNickname { get; set; }
        public static ClientProfileData CurrentUserProfileData { get; set; }
    }
}
