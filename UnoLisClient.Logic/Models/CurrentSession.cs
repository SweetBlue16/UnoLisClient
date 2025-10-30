using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnoLisClient.Logic.UnoLisServerReference.ProfileView;

namespace UnoLisClient.Logic.Models
{
    public static class CurrentSession
    {
        public static string CurrentUserNickname { get; set; }
        public static ClientProfileData CurrentUserProfileData { get; set; }
    }
}
