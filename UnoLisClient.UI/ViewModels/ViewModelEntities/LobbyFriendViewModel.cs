using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnoLisClient.Logic.Models;
using UnoLisClient.UI.Services;
using UnoLisClient.UI.ViewModels;

namespace UnoLisClient.UI.ViewModels.ViewModelEntities
{
    public class LobbyFriendViewModel : ObservableObject
    {
        private readonly Friend _friend;

        public string Nickname => _friend.Nickname;
        public bool IsOnline => _friend.IsOnline;

        private bool _invited;
        public bool Invited
        {
            get => _invited;
            set => SetProperty(ref _invited, value);
        }

        public LobbyFriendViewModel(Friend friend)
        {
            _friend = friend;
            Invited = false;
        }
    }
}
