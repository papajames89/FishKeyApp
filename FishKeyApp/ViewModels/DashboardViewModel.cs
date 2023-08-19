using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FishKeyApp.Controllers;
using FishKeyApp.Models;
using FishKeyApp.Views;
using System.Reflection.PortableExecutable;

namespace FishKeyApp.ViewModels
{
    [QueryProperty(nameof(User), nameof(User))]
    [QueryProperty(nameof(Categories), nameof(Categories))]
    [QueryProperty(nameof(PageHeader), nameof(PageHeader))]
    [QueryProperty(nameof(Subtitle), nameof(Subtitle))]
    public partial class DashboardViewModel : ObservableObject
    {
        private DatabaseController _databaseController;
        private CurrentContextModel _currentContextModel;
        private readonly CardCategoryController _cardCategoryController;
        public DashboardViewModel()
        {
            _databaseController = new DatabaseController();
            _currentContextModel = new CurrentContextModel() { Name = user };
            _cardCategoryController = new CardCategoryController();
            PageHeader = "Wybierz kategorię";
            Subtitle = "Ważne jest, aby zachować ostrożność podczas klikania przycisku reset, ponieważ ta czynność nie może zostać cofnięta.";
        }

        public Task InitAsync()
        {
            Categories = new List<CategoryProgressModel>
            {
                new CategoryProgressModel()
                {
                    CategoryName = "A1",
                    ProgressValue = GetCategoryProgressLabel(User, "A1")
                },
                new CategoryProgressModel()
                {
                    CategoryName = "A2",
                    ProgressValue = GetCategoryProgressLabel(User, "A2")
                },
                new CategoryProgressModel()
                {
                    CategoryName = "B1",
                    ProgressValue = GetCategoryProgressLabel(User, "B1")
                },
                new CategoryProgressModel()
                {
                    CategoryName = "B2",
                    ProgressValue = GetCategoryProgressLabel(User, "B2")
                },
                new CategoryProgressModel()
                {
                    CategoryName = "Na lotnisku",
                    ProgressValue = GetCategoryProgressLabel(User, "Na lotnisku")
                },
                new CategoryProgressModel()
                {
                    CategoryName = "Części ciała",
                    ProgressValue = GetCategoryProgressLabel(User, "Części ciała")
                },
                new CategoryProgressModel()
                {
                    CategoryName = "Jedzenie i picie",
                    ProgressValue = GetCategoryProgressLabel(User, "Jedzenie i picie")
                },
                new CategoryProgressModel()
                {
                    CategoryName = "Kategoria testowa",
                    ProgressValue = GetCategoryProgressLabel(User, "Kategoria testowa")
                }
            };
            return Task.CompletedTask;
        }

        private string GetCategoryProgressLabel(string user, string category)
        {
            return $"{((int)(_cardCategoryController.GetCategoryProgress(_databaseController.GetUser(user), category)*100))}";
        }

        [RelayCommand]
        Task LogOut() => Shell.Current.GoToAsync($"../..");

        [RelayCommand]
        public async Task ResetCategory(string category)
        {
            await _cardCategoryController.ResetCategoryProgress(user, category);
            await InitAsync();
        }

        [RelayCommand]
        public Task GoToFlashCardPage(string category)
        {
            _currentContextModel.Name = user;
            _currentContextModel.Category = category;
            return Shell.Current.GoToAsync($"{nameof(FlashCardPage)}?Category={category}",
                new Dictionary<string, object>
                {
                    ["CurrentContext"] = _currentContextModel
                });
        }

        [ObservableProperty]
        string user;

        [ObservableProperty]
        public List<CategoryProgressModel> categories;

        [ObservableProperty]
        string pageHeader;

        [ObservableProperty]
        string subtitle;
    }
}