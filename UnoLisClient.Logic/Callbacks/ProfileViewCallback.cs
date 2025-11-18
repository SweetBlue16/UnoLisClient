using System;
using System.ServiceModel;
using System.Windows;
using UnoLisClient.Logic.UnoLisServerReference.ProfileView;
using UnoLisServer.Common.Models;

namespace UnoLisClient.Logic.Callbacks
{
    [CallbackBehavior(UseSynchronizationContext = false)]
    public class ProfileViewCallback : IProfileViewManagerCallback
    {
        private readonly Action<ServiceResponse<ProfileData>> _onResponse;

        public ProfileViewCallback(Action<ServiceResponse<ProfileData>> onResponse)
        {
            _onResponse = onResponse;
        }

        public void ProfileDataReceived(ServiceResponse<ProfileData> response)
        {
            Console.WriteLine($"[DEBUG] Callback recibido - Success: {response.Success}");

            // Ejecutar directamente sin Dispatcher
            try
            {
                _onResponse?.Invoke(response);
                Console.WriteLine($"[DEBUG] Callback completado exitosamente");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] En callback: {ex.Message}");
            }
        }
    }
}
