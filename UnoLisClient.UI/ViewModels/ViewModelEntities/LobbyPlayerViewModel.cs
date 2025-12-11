using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnoLisClient.UI.ViewModels;

namespace UnoLisClient.UI.ViewModels.ViewModelEntities
{
    /// <summary>
    /// Viewmodel that represents a players slot in lobbby
    /// </summary>
    public class LobbyPlayerViewModel : ObservableObject
    {
        private const string DefaultNickname = "Esperando...";

        private bool _isSlotFilled;
        public bool IsSlotFilled
        {
            get => _isSlotFilled;
            set => SetProperty(ref _isSlotFilled, value);
        }

        private string _nickname;
        public string Nickname
        {
            get => _nickname;
            set => SetProperty(ref _nickname, value);
        }

        private string _avatarUrl;
        public string AvatarUrl
        {
            get => _avatarUrl;
            set => SetProperty(ref _avatarUrl, value);
        }

        private bool _isReady;
        public bool IsReady
        {
            get => _isReady;
            set => SetProperty(ref _isReady, value);
        }

        public LobbyPlayerViewModel()
        {
            ClearSlot();
        }

        /// <summary>
        /// Fills slot with player data
        /// </summary>
        /// <param name="nickname">Player name.</param>
        /// <param name="avatar">Path or avatar ID.</param>
        public void FillSlot(string nickname, string avatar)
        {
            Nickname = nickname;
            AvatarUrl = avatar;
            IsReady = false;
            IsSlotFilled = true;
        }

        public void ClearSlot()
        {
            IsSlotFilled = false;
            Nickname = DefaultNickname;
            AvatarUrl = null;
            IsReady = false;
        }
    }
}
