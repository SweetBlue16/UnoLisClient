using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;
using System.IO;
using System.Reflection;

namespace UnoLisClient.UI.Utilities
{
    public static class LogManager
    {
        private static readonly ILog _logger;

        static LogManager()
        {
            var logRepository = log4net.LogManager.GetRepository(Assembly.GetEntryAssembly());

            XmlConfigurator.Configure(logRepository, new FileInfo("App.config"));

            _logger = log4net.LogManager.GetLogger(typeof(LogManager));

            if (!Directory.Exists("Logs"))
                Directory.CreateDirectory("Logs");

            _logger.Info("✅ Logger inicializado correctamente.");
        }

        public static void Info(string message)
        {
            _logger.Info(message);
        }

        public static void Warn(string message)
        {
            _logger.Warn(message);
        }

        public static void Error(string message, Exception ex = null)
        {
            if (ex != null)
                _logger.Error($"{message} → {ex.GetType().Name}: {ex.Message}", ex);
            else
                _logger.Error(message);
        }

        public static void Debug(string message)
        {
            _logger.Debug(message);
        }
    }
}

