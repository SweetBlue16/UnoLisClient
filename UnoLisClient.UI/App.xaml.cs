using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Globalization;
using System.Threading;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Windows.Media;

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

            var langCode = global::UnoLisClient.UI.Properties.Settings.Default.languageCode;
            if (string.IsNullOrWhiteSpace(langCode))
                langCode = "en-US";

            Thread.CurrentThread.CurrentUICulture = new CultureInfo(langCode);
            Thread.CurrentThread.CurrentCulture = new CultureInfo(langCode);
        }
    }
}
