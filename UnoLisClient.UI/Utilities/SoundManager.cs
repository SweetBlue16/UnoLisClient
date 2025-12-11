using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace UnoLisClient.UI.Utilities
{
    public static class SoundManager
    {
        private const string AssetsFolderName = "Assets";

        private const string ButtonClickFileName = "buttonClick.mp3";
        private const string ErrorFileName = "buttonClick.mp3";
        private const string SuccessFileName = "buttonClick.mp3";

        private const double DefaultVolume = 0.6;
        private const double ClickVolume = 2.0;
        private const double ErrorVolume = 0.8;
        private const double SuccessVolume = 0.7;

        public static void PlaySound(string fileName, double volume = DefaultVolume)
        {
            string soundPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AssetsFolderName, fileName);

            try
            {
                if (!File.Exists(soundPath))
                {
                    throw new FileNotFoundException("El archivo de sonido no existe.", soundPath);
                }

                var player = new MediaPlayer();
                player.Open(new Uri(soundPath, UriKind.Absolute));
                player.Volume = volume;

                player.MediaEnded += (s, e) =>
                {
                    try
                    {
                        player.Stop();
                        player.Close();
                    }
                    catch (Exception closeEx)
                    {
                        Debug.WriteLine($"⚠️ Error al cerrar el reproductor: {closeEx.Message}");
                    }
                };

                player.Play();
            }
            catch (FileNotFoundException fnfEx)
            {
                Debug.WriteLine($"⚠️ Sonido no encontrado: {fnfEx.FileName}");
            }
            catch (UriFormatException uriEx)
            {
                Debug.WriteLine($"❌ Ruta de sonido inválida: {uriEx.Message}");
            }
            catch (InvalidOperationException opEx)
            {
                Debug.WriteLine($"⚠️ Error al reproducir el sonido (MediaPlayer): {opEx.Message}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ Error inesperado al reproducir sonido: {ex.Message}");
            }
        }

        public static void PlayClick()
        {
            PlaySound(ButtonClickFileName, ClickVolume);
        }

        public static void PlayError()
        {
            PlaySound(ErrorFileName, ErrorVolume);
        }

        public static void PlaySuccess()
        {
            PlaySound(SuccessFileName, SuccessVolume);
        }
    }
}