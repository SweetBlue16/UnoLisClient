using System.Collections.Generic;

namespace UnoLisClient.UI.Utilities
{
    public static class SessionReportTracker
    {
        private static readonly HashSet<string> _reportedPlayers = new HashSet<string>();

        public static void AddReport(string nickname)
        {
            if (!string.IsNullOrEmpty(nickname))
            {
                _reportedPlayers.Add(nickname);
            }
        }

        public static bool HasReported(string nickname)
        {
            return !string.IsNullOrEmpty(nickname) && _reportedPlayers.Contains(nickname);
        }

        public static void Clear()
        {
            _reportedPlayers.Clear();
        }
    }
}
