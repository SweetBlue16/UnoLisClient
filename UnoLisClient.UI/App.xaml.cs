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
            string title = UnoLisClient.UI.Properties.Langs.Global.ConnectionErrorLabel;
            string message = UnoLisClient.UI.Properties.Langs.Global.ConnectionErrorMessageLabel;
            var win = new SimplePopUpWindow(title, message);
            win.Title = UnoLisClient.UI.Properties.Langs.Global.ConnectionErrorLabel;
            win.Show();
        }
    }
}
