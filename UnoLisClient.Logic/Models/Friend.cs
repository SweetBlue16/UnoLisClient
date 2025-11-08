using System.Windows.Media;
using UnoLisClient.Logic.UnoLisServerReference.Friends;

namespace UnoLisClient.Logic.Models
{
    /// <summary>
    /// Representa a un amigo dentro del cliente UNO LIS.
    /// Puede crearse a partir del DTO FriendData recibido del servidor.
    /// Usada tanto en el lobby como en las páginas de amigos.
    /// </summary>
    public class Friend
    {
        public string Nickname { get; set; }
        public string StatusMessage { get; set; }
        public bool IsOnline { get; set; }

        public Brush StatusColor => IsOnline ? Brushes.LimeGreen : Brushes.Gray;
        public bool Invited { get; set; }

        public Friend() { }

        public Friend(FriendData dto)
        {
            Nickname = dto.FriendNickname;
            StatusMessage = dto.StatusMessage;
            IsOnline = dto.IsOnline;
            Invited = false;
        }

        public static Friend FromDto(FriendData dto) => new Friend(dto);
    }
}
