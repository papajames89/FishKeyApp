using FishKeyApp.Models;
using Plugin.Maui.Audio;

namespace FishKeyApp.Controllers
{
    public class AudioPlayerController
    {
        private readonly FtpController _ftpController;
        private readonly IAudioManager audioManager;
        protected IAudioPlayer player = null;       // media player
        protected FileStream stream = null;         // stream used for playing
        private MediaState mediaState;
        public AudioPlayerController(IAudioManager audioManager)
        {
            this.audioManager = audioManager;
            mediaState = MediaState.Stopped;
            _ftpController = new FtpController();
        }
        public void PlayAudio(string fileName)
        {
            // ignore if we're already playing
            if (mediaState == MediaState.Playing)
            {
                StopAudio();
                return;
            }

            try
            {
                // This is where we are storing local audio files
                string cacheDir = FileSystem.Current.CacheDirectory;

                // get the fully qualified path to the local file
                var localFile = $"{cacheDir}\\{fileName}";

                // download if need
                if (!File.Exists(localFile))
                {
                    _ftpController.DownloadMp3File(fileName);
                }

                // File exists now. Read it
                stream = File.OpenRead(localFile);

                // create the audio player
                player = audioManager.CreatePlayer(stream);

                // start playing
                player.Play();
                mediaState= MediaState.Playing;
            }
            catch (Exception e)
            {
            }
        }

        public void StopAudio()
        {
            player.Stop();
            player.Dispose();
            mediaState = MediaState.Stopped;
        }
    }
}
