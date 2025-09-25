using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using UnoLisClient.UI.PopUpWindows;

namespace UnoLisClient.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // UI Tests
            string title = UnoLisClient.UI.Properties.Langs.Global.WarningLabel;
            string message = string.Format(UnoLisClient.UI.Properties.Langs.FriendsList.RemoveFriendMessageLabel, "SweetBlue16"); 
            string watermark = UnoLisClient.UI.Properties.Langs.Global.CodeLabel;
            var win = new QuestionPopUpWindow(title, message);
            win.Title = UnoLisClient.UI.Properties.Langs.Global.WarningLabel.ToUpper();
            win.Show();
        }
    }
}
