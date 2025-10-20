using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnoLisClient.UI.Managers;
using UnoLisClient.UI.UnoLisServerReference.ProfileEdit;
using UnoLisClient.UI.UnoLisServerReference.ProfileView;

namespace UnoLisClient.UI.Utilities
{
    public static class ProfileDataMapper
    {
        public static ClientProfileData ToClientModel(this UnoLisServerReference.ProfileView.ProfileData data)
        {
            if (data == null)
            {
                return null;
            }

            return new ClientProfileData
            {
                Nickname = data.Nickname,
                FullName = data.FullName,
                Email = data.Email,
                FacebookUrl = data.FacebookUrl,
                InstagramUrl = data.InstagramUrl,
                TikTokUrl = data.TikTokUrl,
                MatchesPlayed = data.MatchesPlayed,
                Wins = data.Wins,
                Losses = data.Losses,
                ExperiencePoints = data.ExperiencePoints
            };
        }

        public static UnoLisServerReference.ProfileEdit.ProfileData ToProfileEditContract(this ClientProfileData data)
        {
            if (data == null)
            {
                return null;
            }

            return new UnoLisServerReference.ProfileEdit.ProfileData
            {
                Nickname = data.Nickname,
                FullName = data.FullName,
                Email = data.Email,
                FacebookUrl = data.FacebookUrl,
                InstagramUrl = data.InstagramUrl,
                TikTokUrl = data.TikTokUrl,
                Password = data.Password
            };
        }
        
    }
}
