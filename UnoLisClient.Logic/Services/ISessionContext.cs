using UnoLisClient.Logic.Models;

namespace UnoLisClient.Logic.Services
{
    public interface ISessionContext
    {
        string CurrentUserNickname { get; }
        ClientProfileData CurrentUserProfile { get; }
    }

    public class SessionContext : ISessionContext
    {
        public string CurrentUserNickname => CurrentSession.CurrentUserNickname;
        public ClientProfileData CurrentUserProfile => CurrentSession.CurrentUserProfileData;
    }
}
