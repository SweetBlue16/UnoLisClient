namespace UnoLisClient.UI.ViewModels.ViewModelEntities
{
    public class RewardPopUpModel : ObservableObject
    {
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
                AvatarName = "Error";
                AvatarImagePath = "pack://application:,,,/Avatars/LogoUNO.png";
            }
        }
    }
}
