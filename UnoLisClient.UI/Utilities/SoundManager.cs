using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IO;
using System.Windows.Media;

namespace UnoLisClient.UI.Utils
{
    public static class SoundManager
    {
        private static MediaPlayer _player;

        /// <summary>
        /// Reproduce un efecto de sonido a partir del nombre del archivo en Assets.
        /// </summary>
        /// <param name="fileName">Ejemplo: "buttonClick.mp3"</param>
        public static void PlaySound(string fileName, double volume = 0.6)
        {
            try
            {
                string soundPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", fileName);

                if (!File.Exists(soundPath))
                {
                    System.Diagnostics.Debug.WriteLine($"⚠️ No se encontró el sonido: {soundPath}");
                    return;
                }

                _player = new MediaPlayer();
                _player.Open(new Uri(soundPath, UriKind.Absolute));
                _player.Volume = volume;
                _player.Play();

                // Limpieza automática al terminar
                _player.MediaEnded += (s, _) =>
                {
                    _player.Close();
                    _player = null;
                };
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ Error al reproducir sonido: {ex.Message}");
            }
        }

        /// <summary>
        /// Sonido rápido para clics de botones
        /// </summary>
        public static void PlayClick()
        {
            PlaySound("buttonClick.mp3", 1.0);
        }

        /// <summary>
        /// Sonido rápido para errores o alertas
        /// </summary>
        public static void PlayError()
        {
            PlaySound("buttonClick.mp3", 0.8);
        }

        /// <summary>
        /// Sonido de confirmación o éxito
        /// </summary>
        public static void PlaySuccess()
        {
            PlaySound("buttonClick.mp3", 0.7);
        }
    }
}