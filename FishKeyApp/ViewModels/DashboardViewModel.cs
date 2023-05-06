using Android.Webkit;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FishKeyApp.Controllers;
using FishKeyApp.Models;
using FishKeyApp.Views;

namespace FishKeyApp.ViewModels
{
    [QueryProperty(nameof(User), nameof(User))]
    [QueryProperty(nameof(Categories), nameof(Categories))]
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
            //categories = new List<CategoryProgressModel>
            //{
            //    new CategoryProgressModel()
            //    {
            //        CategoryName = "A1",
            //        //ProgressValue = _cardCategoryController.GetCategoryProgress(_databaseController.GetUser(user), "A1").ToString()
            //    },
            //    new CategoryProgressModel()
            //    {
            //        CategoryName = "A2",
            //        //ProgressValue = _cardCategoryController.GetCategoryProgress(_databaseController.GetUser(user), "A2").ToString()
            //    },
            //    new CategoryProgressModel()
            //    {
            //        CategoryName = "B1",
            //        //ProgressValue = _cardCategoryController.GetCategoryProgress(_databaseController.GetUser(user), "B1").ToString()
            //    },
            //    new CategoryProgressModel()
            //    {
            //        CategoryName = "B2",
            //        //ProgressValue = _cardCategoryController.GetCategoryProgress(_databaseController.GetUser(user), "B2").ToString()
            //    },
            //    new CategoryProgressModel()
            //    {
            //        CategoryName = "Na lotnisku",
            //        //ProgressValue = _cardCategoryController.GetCategoryProgress(_databaseController.GetUser(user), "Na lotnisku").ToString()
            //    },
            //    new CategoryProgressModel()
            //    {
            //        CategoryName = "Czesci ciala",
            //        //ProgressValue = _cardCategoryController.GetCategoryProgress(_databaseController.GetUser(user), "Czesci ciala").ToString()
            //    },
            //    new CategoryProgressModel()
            //    {
            //        CategoryName = "Jedzenie i picie",
            //        //ProgressValue = _cardCategoryController.GetCategoryProgress(_databaseController.GetUser(user), "Jedzenie i picie").ToString()
            //    },
            //    new CategoryProgressModel()
            //    {
            //        CategoryName = "Kategoria testowa",
            //        //ProgressValue = _cardCategoryController.GetCategoryProgress(_databaseController.GetUser(user), "Kategoria testowa").ToString()
            //    }
            //};
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
                    CategoryName = "Czesci ciala",
                    ProgressValue = GetCategoryProgressLabel(User, "Czesci ciala")
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
            return $"{((int)(_cardCategoryController.GetCategoryProgress(_databaseController.GetUser(user), category)*100))} %";
        }

        [RelayCommand]
        Task LogOut() => Shell.Current.GoToAsync($"../..");

        [RelayCommand]
        public void ResetCategory(string category)
        {
            _cardCategoryController.ResetCategoryProgress(user, category);
            InitAsync();
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
    }
}