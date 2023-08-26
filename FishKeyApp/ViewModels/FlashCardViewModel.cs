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
    [QueryProperty(nameof(ProgressValue), nameof(ProgressValue))]
    [QueryProperty(nameof(ProgressValuePercentage), nameof(ProgressValuePercentage))]
    [QueryProperty(nameof(CardHeightRequest), nameof(CardHeightRequest))]
    [QueryProperty(nameof(WordsCounter), nameof(WordsCounter))]
    [QueryProperty(nameof(Frame1ZIndex), nameof(Frame1ZIndex))]
    [QueryProperty(nameof(Frame2ZIndex), nameof(Frame2ZIndex))]
    [QueryProperty(nameof(IsVisibleSpeaker), nameof(IsVisibleSpeaker))]

    public partial class FlashCardViewModel : ObservableObject
    {
        private readonly FtpController _ftpController;
        private readonly DatabaseController _databaseController;
        private readonly CardCategoryController _cardCategoryController;
        private Locale _polishLocale;
        private int _cardHeightRequest = 300;

        public FlashCardViewModel()
        {
            _databaseController = new DatabaseController();
            _cardCategoryController = new CardCategoryController();
            _ftpController = new FtpController();
            GetPolishLocale();
        }

        public Task InitAsync()
        {
            CurrentUser = _databaseController.GetUser(CurrentContext.Name);
            CurrentFlashCard = _cardCategoryController.GetRandomFlashCard(CurrentUser, Category);
            CardLabel = CurrentFlashCard.English;
            ProgressValue = _cardCategoryController.GetCategoryProgress(CurrentUser, Category);
            ProgressValuePercentage = $"{(Int16)(ProgressValue*100)} %";
            CardHeightRequest = GetCardHeightRequest(CardLabel.Length);
            ImgUrl = _ftpController.DownloadImgFile(CurrentFlashCard.ImgUrl);
            FlipBtnOpacity = 1;
            ResultBtnsOpacity = 0;
            Frame1ZIndex = 2;
            Frame2ZIndex = 1;
            IsVisibleSpeaker = false;
            WordsCounter = $"{_cardCategoryController.GetCategoryWordsCount(Category)} words";
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
            CardLabel = CurrentFlashCard.English;
        }

        [RelayCommand]
        private async void PlayAudio()
        {
            var settings = new SpeechOptions()
            {
                Volume = .75f,
                Pitch = 1.0f,
                Locale = _polishLocale
            };

            await TextToSpeech.Default.SpeakAsync(CardLabel, settings);
        }

        [RelayCommand]
        Task GoBack() => Shell.Current.GoToAsync("..");

        [RelayCommand]
        void Flip()
        {
            if (CardLabel == CurrentFlashCard.English)
            {
                CardLabel = CurrentFlashCard.Polish;
                CardHeightRequest = GetCardHeightRequest(CardLabel.Length);
            }

            if (FlipBtnOpacity == 1)
            {
                IsVisibleSpeaker = true;
                Frame1ZIndex = 1;
                Frame2ZIndex = 2;
                ResultBtnsOpacity = 1;
                FlipBtnOpacity = 0;
            }
            else return;
        }

        [RelayCommand]
        async Task Yes()
        {
            try
            {
                CurrentUser = _databaseController.UpdateUser(CurrentUser, CurrentFlashCard);
                _databaseController.SaveUser(CurrentUser);
                ProgressValue = _cardCategoryController.GetCategoryProgress(CurrentUser, Category);
                ProgressValuePercentage = $"{(Int16)(ProgressValue * 100)} %";
                await UpdateCurrentCard();
                CardHeightRequest = GetCardHeightRequest(CardLabel.Length);

                if (ResultBtnsOpacity == 1)
                {
                    IsVisibleSpeaker = false;
                    Frame1ZIndex = 2;
                    Frame2ZIndex = 1;
                    ResultBtnsOpacity = 0;
                    FlipBtnOpacity = 1;
                }
                else return;
            }
            catch (Exception e)
            {
            }
        }

        [RelayCommand]
        async Task No()
        {
            await UpdateCurrentCard();
            CardHeightRequest = GetCardHeightRequest(CardLabel.Length);
            if (ResultBtnsOpacity == 1)
            {
                IsVisibleSpeaker = false;
                Frame1ZIndex = 2;
                Frame2ZIndex = 1;
                ResultBtnsOpacity = 0;
                FlipBtnOpacity = 1;
            }
            else return;
        }

        private async void GetPolishLocale()
        {
            var locales = await TextToSpeech.GetLocalesAsync();

            _polishLocale = locales.Where(x => x.Language == "pl-PL").First();
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
        double progressValue;

        [ObservableProperty]
        string progressValuePercentage;

        [ObservableProperty]
        int cardHeightRequest;

        [ObservableProperty]
        string wordsCounter;

        [ObservableProperty]
        int frame1ZIndex;

        [ObservableProperty]
        int frame2ZIndex;

        [ObservableProperty]
        bool isVisibleSpeaker;
    }
}