using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace UnoLisClient.UI.Utils
{
    public static class SoundManager
    {
        public static void PlaySound(string fileName, double volume = 0.6)
        {
            string soundPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", fileName);

            try
            {
                if (!File.Exists(soundPath))
                    throw new FileNotFoundException("El archivo de sonido no existe.", soundPath);

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
            PlaySound("buttonClick.mp3", 2.0);
        }

        public static void PlayError()
        {
            PlaySound("buttonClick.mp3", 0.8);
        }

        public static void PlaySuccess()
        {
            PlaySound("buttonClick.mp3", 0.7);
        }
    }
}