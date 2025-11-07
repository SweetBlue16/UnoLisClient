namespace UnoLisClient.UI.ViewModels.ViewModelEntities
{
    public class LeaderboardEntry : BaseViewModel
    {
        public int Rank { get; set; }
        public string PlayerName { get; set; }
        public int Score { get; set; }
        public int MatchesPlayed { get; set; }
        public string WinRate { get; set; }
    }
}
