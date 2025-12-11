using log4net;
using log4net.Config;
using System;
using System.IO;
using System.Reflection;

namespace UnoLisClient.Logic.Helpers
{
    public static class Logger
    {
        private static readonly ILog _logger;

        static Logger()
        {
            try
            {
                var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());

                XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

                if (!Directory.Exists("Logs"))
                {
                    Directory.CreateDirectory("Logs");
                }

                _logger = LogManager.GetLogger(typeof(Logger));
                _logger.Info("✅ Client logger initialized successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("⚠️ Failed to initialize client logger: " + ex.Message);
            }
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
            if (ex == null)
            {
                _logger.Error(message);
            }
            else
            {
                _logger.Error(message + $" → {ex.GetType().Name}: {ex.Message}", ex);
            }
        }
    }
}
