using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FishKeyApp.Models;
using Plugin.Maui.Audio;
using FishKeyApp.Controllers;

namespace FishKeyApp.ViewModels
{
    [QueryProperty(nameof(Category), nameof(Category))]
    [QueryProperty(nameof(ImgUrl), nameof(ImgUrl))]
    [QueryProperty(nameof(CardLabel), nameof(CardLabel))]
    [QueryProperty(nameof(CurrentContext), nameof(CurrentContext))]
    [QueryProperty(nameof(CurrentUser), nameof(CurrentUser))]
    [QueryProperty(nameof(CurrentFlashCard), nameof(CurrentFlashCard))]
    [QueryProperty(nameof(FlipBtnOpacity), nameof(FlipBtnOpacity))]
    [QueryProperty(nameof(ResultBtnsOpacity), nameof(ResultBtnsOpacity))]

    public partial class FlashCardViewModel : ObservableObject
    {
        private readonly FtpController _ftpController;
        private readonly AudioPlayerController _audioPlayerController;
        private readonly DatabaseController _databaseController;
        private readonly CardCategoryController _cardCategoryController;
        private bool _isFlipped = false;

        public FlashCardViewModel(IAudioManager audioManager)
        {
            _audioPlayerController = new AudioPlayerController(audioManager);
            _databaseController = new DatabaseController();
            _cardCategoryController = new CardCategoryController();
            _ftpController = new FtpController();
        }

        public Task InitAsync()
        {
            CurrentUser = _databaseController.GetUser(CurrentContext.Name);
            CurrentFlashCard = _cardCategoryController.GetRandomFlashCard(CurrentUser, Category);
            ImgUrl = _ftpController.DownloadImgFile(CurrentFlashCard.ImgUrl);
            CardLabel = CurrentFlashCard.Polish;
            FlipBtnOpacity = 1;
            ResultBtnsOpacity = 0;
            return Task.CompletedTask;
        }

        private void UpdateCurrentCard()
        {
            CurrentFlashCard = _cardCategoryController.GetRandomFlashCard(CurrentUser, Category);
            ImgUrl = _ftpController.DownloadImgFile(CurrentFlashCard.ImgUrl);
            CardLabel = CurrentFlashCard.Polish;
        }

        [RelayCommand]
        private void PlayAudio()
        {
            _audioPlayerController.PlayAudio(CurrentFlashCard.Mp3Url);
        }

        [RelayCommand]
        Task GoBack() => Shell.Current.GoToAsync("..");

        [RelayCommand]
        void Flip()
        {
            if (CardLabel == CurrentFlashCard.Polish)
            {
                CardLabel = CurrentFlashCard.English;
            }

            if (FlipBtnOpacity == 1)
            {
                ResultBtnsOpacity = 1;
                FlipBtnOpacity = 0;
            }
            else return;
        }

        [RelayCommand]
        void Yes()
        {
            CurrentUser = _databaseController.UpdateUser(CurrentUser, CurrentFlashCard);
            _databaseController.SaveUser(CurrentUser);
            UpdateCurrentCard();
            if (ResultBtnsOpacity == 1)
            {
                ResultBtnsOpacity = 0;
                FlipBtnOpacity = 1;
            }
            else return;
        }

        [RelayCommand]
        void No()
        {
            UpdateCurrentCard();
            if (ResultBtnsOpacity == 1)
            {
                ResultBtnsOpacity = 0;
                FlipBtnOpacity = 1;
            }
            else return;
        }

        [ObservableProperty]
        string imgUrl;

        [ObservableProperty]
        string category;

        [ObservableProperty]
        string cardLabel;

        [ObservableProperty]
        int flipBtnOpacity;

        [ObservableProperty]
        int resultBtnsOpacity;

        [ObservableProperty]
        CurrentContextModel currentContext;

        [ObservableProperty]
        UserModel currentUser;

        [ObservableProperty]
        FlashCardModel currentFlashCard;
    }
}