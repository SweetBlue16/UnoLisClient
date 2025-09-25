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
            string title = UnoLisClient.UI.Properties.Langs.Global.ConfirmationLabel;
            string message = UnoLisClient.UI.Properties.Langs.Global.ConfirmationMessageLabel;
            string watermark = UnoLisClient.UI.Properties.Langs.Global.CodeLabel;
            var win = new SimplePopUpWindow(title, message);
            win.Title = UnoLisClient.UI.Properties.Langs.Global.ConfirmationLabel;
            win.Show();
        }
    }
}
