using System;
using UnoLisClient.Logic.UnoLisServerReference.Report;

namespace UnoLisClient.Logic.Helpers
{
    public static class BanSessionManager
    {
        public static event Action<BanInfo> PlayerBanned;

        public static void TriggerBan(BanInfo banInfo)
        {
            if (banInfo != null)
            {
                PlayerBanned?.Invoke(banInfo);
            }
        }
    }
}
