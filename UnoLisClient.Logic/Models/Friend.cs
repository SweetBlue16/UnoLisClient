using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace UnoLisClient.Logic.Models
{
    public class Friend
    {
        public string FriendName { get; set; }
        public string Status { get; set; }
        public Brush StatusColor { get; set; }
        public bool Invited { get; set; }
    }
}