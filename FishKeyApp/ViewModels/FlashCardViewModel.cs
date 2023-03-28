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

        public FlashCardViewModel(IAudioManager audioManager)
        {
            _audioPlayerController = new AudioPlayerController(audioManager);
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
            _audioPlayerController.PlayAudio("hair.mp3");
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