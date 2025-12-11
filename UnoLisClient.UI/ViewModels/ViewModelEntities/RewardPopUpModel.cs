namespace UnoLisClient.UI.ViewModels.ViewModelEntities
{
    public class RewardPopUpModel : ObservableObject
    {
        private const string DefaultAvatarImagePath = "pack://application:,,,/Avatars/LogoUNO.png";
        private const string DefaultAvatarName = "Error";

        public string AvatarName { get; set; }
        public string AvatarImagePath { get; set; }

        public RewardPopUpModel(AvatarModel rewardedAvatar)
        {
            if (rewardedAvatar != null)
            {
                AvatarName = rewardedAvatar.Name;
                AvatarImagePath = rewardedAvatar.ImagePath;
            }
            else
            {
                AvatarName = DefaultAvatarName;
                AvatarImagePath = DefaultAvatarImagePath;
            }
        }
    }
}
