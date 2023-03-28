using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FishKeyApp.Models;
using FishKeyApp.Views;

namespace FishKeyApp.ViewModels
{
    [QueryProperty(nameof(User), nameof(User))]
    public partial class DashboardViewModel : ObservableObject
    {
        public List<CategoryModel> Categories { get; set; }
        private CurrentContextModel _currentContextModel;
        public DashboardViewModel()
        {
            Categories = new List<CategoryModel>
            {
                new CategoryModel(){ Category = "Category1", Image = "Img1.jpg" },
                new CategoryModel(){ Category = "Category2", Image = "Img2.jpg" }
            };
            _currentContextModel = new CurrentContextModel() { Name = user };
        }

        [RelayCommand]
        Task LogOut() => Shell.Current.GoToAsync($"../..");

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
    }
}