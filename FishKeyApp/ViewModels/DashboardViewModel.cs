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
        private CurrentContextModel _currentContextModel;
        private readonly CardCategoryController _cardCategoryController;
        public DashboardViewModel()
        {
            categories = new List<string>()
            {
                "A1",
                "Kategoria testowa"
            };
            _currentContextModel = new CurrentContextModel() { Name = user };
            _cardCategoryController = new CardCategoryController();
        }


        [RelayCommand]
        Task LogOut() => Shell.Current.GoToAsync($"../..");

        [RelayCommand]
        public void ResetCategory(string category)
        {
            _cardCategoryController.ResetCategoryProgress(user, category);
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
        public List<string> categories;
    }
}