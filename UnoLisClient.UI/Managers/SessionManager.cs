using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnoLisClient.UI.UnoLisServerReference.Profile;

namespace UnoLisClient.UI.Managers
{
    public static class SessionManager
    {
        public static ProfileData CurrentProfile { get; set; }
        public static bool IsLoggedIn => CurrentProfile != null;

        public static void ClearSession()
        {
            CurrentProfile = null;
        }
    }
}
