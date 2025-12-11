using System;
using System.ServiceModel;
using UnoLisClient.Logic.Helpers;

namespace UnoLisClient.Logic.Services
{
    public static class ServerConnectionMonitor
    {
        public static Action<string> OnServerConnectionLost { get; set; }

        public static void Monitor(ICommunicationObject channel)
        {
            if (channel == null)
            {
                return;
            }
            channel.Faulted -= OnChannelFaulted;
            channel.Faulted += OnChannelFaulted;
        }

        private static void OnChannelFaulted(object sender, EventArgs e)
        {
            Logger.Error("[CLIENT-MONITOR] Critical WCF Channel Faulted.");
            OnServerConnectionLost?.Invoke("Msg_ServerConnectionLost");
        }
    }
}