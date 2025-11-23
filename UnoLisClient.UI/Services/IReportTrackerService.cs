using UnoLisClient.UI.Utilities;

namespace UnoLisClient.UI.Services
{
    public interface IReportTrackerService
    {
        void AddReport(string nickname);
        bool HasReported(string nickname);
        void Clear();
    }

    public class ReportTrackerService : IReportTrackerService
    {
        public void AddReport(string nickname) => SessionReportTracker.AddReport(nickname);
        public bool HasReported(string nickname) => SessionReportTracker.HasReported(nickname);
        public void Clear() => SessionReportTracker.Clear();
    }
}
