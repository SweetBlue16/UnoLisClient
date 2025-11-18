using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnoLisClient.UI.ViewModels;

namespace UnoLisClient.UI.ViewModels.ViewModelEntities
{
    /// <summary>
    /// ViewModel que representa un "slot" o asiento de jugador en el Lobby.
    /// Controla la visualización del avatar, nombre y estado de listo.
    /// </summary>
    public class LobbyPlayerViewModel : ObservableObject
    {
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
        /// Llena el slot con los datos de un jugador real.
        /// </summary>
        /// <param name="nickname">Nombre del jugador.</param>
        /// <param name="avatar">Ruta o identificador del avatar.</param>
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
            Nickname = "Esperando...";
            AvatarUrl = null;
            IsReady = false;
        }
    }
}
