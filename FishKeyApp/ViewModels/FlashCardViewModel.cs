using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FishKeyApp.Models;
using Plugin.Maui.Audio;
using FishKeyApp.Controllers;
using Android.Media;
using Java.Net;

namespace FishKeyApp.ViewModels
{
    [QueryProperty(nameof(Category), nameof(Category))]
    [QueryProperty(nameof(ImgUrl), nameof(ImgUrl))]
    [QueryProperty(nameof(CurrentContext), nameof(CurrentContext))]
    [QueryProperty(nameof(FlipBtnOpacity), nameof(FlipBtnOpacity))]
    [QueryProperty(nameof(YesBtnOpacity), nameof(YesBtnOpacity))]
    [QueryProperty(nameof(NoBtnOpacity), nameof(NoBtnOpacity))]

    public partial class FlashCardViewModel : ObservableObject
    {
        private readonly FtpController _ftpController;
        private readonly AudioPlayerController _audioPlayerController;
        private readonly IAudioManager audioManager;
        protected IAudioPlayer player = null;       // media player
        protected FileStream stream = null;         // stream used for playing
        private MediaState mediaState;

        public FlashCardViewModel(IAudioManager audioManager)
        {
            this.audioManager= audioManager;
            //_audioPlayerController = new AudioPlayerController(audioManager);
            _ftpController = new FtpController();
            FlipBtnOpacity = 1;
            YesBtnOpacity = 0;
            NoBtnOpacity = 0;
            SetUp();
        }

        private void SetUp()
        {
            ImgUrl = _ftpController.DownloadImgFile("back.jpg");
        }

        [RelayCommand]
        private async Task PlayAudio()
        {
            // ignore if we're already playing
            //if (mediaState == MediaState.Playing)
            //{
            //    //StopAudio();
            //    return;
            //}
            //string url = "https://www.learningcontainer.com/wp-content/uploads/2020/02/Kalimba.mp3";

            //try
            //{
            //    // This is where we are storing local audio files
            //    string cacheDir = FileSystem.Current.CacheDirectory;


            //    // get the fully qualified path to the local file
            //    var localFile = $"{cacheDir}\\{Path.GetFileName(url)}";

            //    // download if need be
            //    if (!File.Exists(localFile))
            //    {
            //        // this code downloads the file from the URL
            //        using (var client = new HttpClient())
            //        {
            //            var uri = new Uri(url);
            //            var response = await client.GetAsync(url);
            //            response.EnsureSuccessStatusCode();
            //            using (var stream = await response.Content.ReadAsStreamAsync())
            //            {
            //                var fileInfo = new FileInfo(localFile);
            //                using (var fileStream = fileInfo.OpenWrite())
            //                {
            //                    await stream.CopyToAsync(fileStream);
            //                }
            //            }
            //        }
            //    }

            //    // File exists now. Read it
            //    stream = File.OpenRead(localFile);

            //    // create the audio player
            //    player = audioManager.CreatePlayer(stream);

            //    // start playing
            //    player.Play();


            //}
            //catch (Exception e)
            //{
            //}




            // here we go!
            try
            {
                // This is where we are storing local audio files
                string cacheDir = FileSystem.Current.CacheDirectory;

                // get the fully qualified path to the local file
                var localFile = $"{cacheDir}\\body.mp3";

                // download if need
                if (!File.Exists(localFile))
                {
                    _ftpController.DownloadMp3File("body.mp3");
                }

                var exist = File.Exists(localFile);

                // File exists now. Read it
                stream = File.OpenRead(localFile);

                // create the audio player
                player = audioManager.CreatePlayer(stream);

                // start playing
                player.Play();
                // configure the UI for playing
                mediaState = MediaState.Playing;
            }
            catch (Exception e)
            {
            }
        }

        [RelayCommand]
        Task GoBack() => Shell.Current.GoToAsync("..");

        [RelayCommand]
        void Flip()
        {
            if (FlipBtnOpacity == 1)
            {
                YesBtnOpacity = 1;
                NoBtnOpacity = 1;
                FlipBtnOpacity = 0;
            }
            else return;
        }

        [RelayCommand]
        void Yes()
        {
            if (YesBtnOpacity == 1)
            {
                YesBtnOpacity = 0;
                NoBtnOpacity = 0;
                FlipBtnOpacity = 1;
            }
            else return;
        }

        [RelayCommand]
        void No()
        {
            if (NoBtnOpacity == 1)
            {
                YesBtnOpacity = 0;
                NoBtnOpacity = 0;
                FlipBtnOpacity = 1;
            }
            else return;
        }

        [ObservableProperty]
        string imgUrl;

        [ObservableProperty]
        string category;

        [ObservableProperty]
        int flipBtnOpacity;

        [ObservableProperty]
        int yesBtnOpacity;

        [ObservableProperty]
        int noBtnOpacity;

        [ObservableProperty]
        CurrentContextModel currentContext;
    }
}