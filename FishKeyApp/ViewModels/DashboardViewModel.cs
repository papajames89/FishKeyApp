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
            PageHeader = "Select category";
            Subtitle = "It is important to exercise caution when clicking the reset button, as this action cannot be reversed.";
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
                    CategoryName = "At the airport",
                    ProgressValue = GetCategoryProgressLabel(User, "Na lotnisku")
                },
                new CategoryProgressModel()
                {
                    CategoryName = "Body parts",
                    ProgressValue = GetCategoryProgressLabel(User, "Czesci ciala")
                },
                new CategoryProgressModel()
                {
                    CategoryName = "Food and drinks",
                    ProgressValue = GetCategoryProgressLabel(User, "Jedzenie i picie")
                },
                new CategoryProgressModel()
                {
                    CategoryName = "Test category",
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
            string cat = GetPolisPageName(category);
            await _cardCategoryController.ResetCategoryProgress(user, cat);
            await InitAsync();
        }

        [RelayCommand]
        public Task GoToFlashCardPage(string category)
        {
            string cat = GetPolisPageName(category);
            _currentContextModel.Name = user;
            _currentContextModel.Category = category;
            return Shell.Current.GoToAsync($"{nameof(FlashCardPage)}?Category={cat}",
                new Dictionary<string, object>
                {
                    ["CurrentContext"] = _currentContextModel
                });
        }

        private string GetPolisPageName(string category)
        {
            string kategoria = "";
            if (category != "A1" && category != "A2" && category != "B1" && category != "B2")
            {
                switch (category)
                {
                    case "At the airport":
                        kategoria = "Na lotnisku";
                        break;
                    case "Body parts":
                        kategoria = "Czesci ciala";
                        break;
                    case "Food and drinks":
                        kategoria = "Jedzenie i picie";
                        break;
                    case "Test category":
                        kategoria = "Kategoria testowa";
                        break;
                }

                return kategoria;
            }
            return category;
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