using System.Media;
using System.Windows;

namespace Melembre.Services
{
    static class ExecuteSound
    {
        static SoundPlayer player = new SoundPlayer();
        public static void play_sound(string patch)
        {
            player.SoundLocation = patch;
            player.LoadAsync();
            player.PlayLooping();
        }

        public static void stop_sound()
        {
            player.Stop();
        }

    }
}
