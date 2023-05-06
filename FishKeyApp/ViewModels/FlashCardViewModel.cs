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
    [QueryProperty(nameof(IsBusy), nameof(IsBusy))]
    [QueryProperty(nameof(ProgressValue), nameof(ProgressValue))]
    [QueryProperty(nameof(ProgressValuePercentage), nameof(ProgressValuePercentage))]
    [QueryProperty(nameof(CardHeightRequest), nameof(CardHeightRequest))]

    public partial class FlashCardViewModel : ObservableObject
    {
        private readonly FtpController _ftpController;
        private readonly AudioPlayerController _audioPlayerController;
        private readonly DatabaseController _databaseController;
        private readonly CardCategoryController _cardCategoryController;
        private int _cardHeightRequest = 300;

        public FlashCardViewModel(IAudioManager audioManager)
        {
            _audioPlayerController = new AudioPlayerController(audioManager);
            _databaseController = new DatabaseController();
            _cardCategoryController = new CardCategoryController();
            _ftpController = new FtpController();
            IsBusy = false;
        }

        public Task InitAsync()
        {
            CurrentUser = _databaseController.GetUser(CurrentContext.Name);
            CurrentFlashCard = _cardCategoryController.GetRandomFlashCard(CurrentUser, Category);
            CardLabel = CurrentFlashCard.Polish;
            ProgressValue = _cardCategoryController.GetCategoryProgress(CurrentUser, Category);
            ProgressValuePercentage = $"{(Int16)(ProgressValue*100)} %";
            CardHeightRequest = GetCardHeightRequest(CardLabel.Length);
            ImgUrl = _ftpController.DownloadImgFile(CurrentFlashCard.ImgUrl);
            _ftpController.DownloadMp3File(CurrentFlashCard.Mp3Url);
            FlipBtnOpacity = 1;
            ResultBtnsOpacity = 0;
            IsBusy = false;
            return Task.CompletedTask;
        }

        private int GetCardHeightRequest(int sentenceLenght)
        {
            if (sentenceLenght > 30)
            {
                return 320;
            }
            else return _cardHeightRequest;
        }

        private async Task UpdateCurrentCard()
        {
            CurrentFlashCard = _cardCategoryController.GetRandomFlashCard(CurrentUser, Category);
            ImgUrl = _ftpController.DownloadImgFile(CurrentFlashCard.ImgUrl);
            await _ftpController.DownloadMp3FileAsync(CurrentFlashCard.Mp3Url);
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
                CardHeightRequest = GetCardHeightRequest(CardLabel.Length);
            }

            if (FlipBtnOpacity == 1)
            {
                ResultBtnsOpacity = 1;
                FlipBtnOpacity = 0;
            }
            else return;
        }

        [RelayCommand]
        async Task Yes()
        {
            IsBusy = true;
            try
            {
                CurrentUser = _databaseController.UpdateUser(CurrentUser, CurrentFlashCard);
                _databaseController.SaveUser(CurrentUser);
                ProgressValue = _cardCategoryController.GetCategoryProgress(CurrentUser, Category);
                ProgressValuePercentage = $"{(Int16)(ProgressValue * 100)} %";
                await UpdateCurrentCard();

                if (ResultBtnsOpacity == 1)
                {
                    ResultBtnsOpacity = 0;
                    FlipBtnOpacity = 1;
                }
                else return;
            }
            catch (Exception e)
            {
            }
            finally { IsBusy = false; }
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

        [ObservableProperty]
        bool isBusy;

        [ObservableProperty]
        double progressValue;

        [ObservableProperty]
        string progressValuePercentage;

        [ObservableProperty]
        int cardHeightRequest;
    }
}