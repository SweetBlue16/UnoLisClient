using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnoLisClient.Logic.Helpers
{
    public static class UserHelper
    {
        public static bool IsGuest(string nickname)
        {
            if (string.IsNullOrEmpty(nickname))
            {
                return false;
            }
            return nickname.StartsWith("Guest_", StringComparison.OrdinalIgnoreCase);
        }
    }
}
