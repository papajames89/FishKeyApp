using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FishKeyApp.Models;
using FishKeyApp.Views;

namespace FishKeyApp.ViewModels
{
    public partial class SelectUserViewModel : ObservableObject
    {
        public List<UserModel> ListOfUsers { get; set; }
        public SelectUserViewModel()
        {
            ListOfUsers = new List<UserModel>
            {
                new UserModel() { Name = "Stefan", Color = "Red" },
                new UserModel() { Name = "Batory", Color = "Blue" }
            };
        }

        [RelayCommand]
        Task GoBack() => Shell.Current.GoToAsync("..");

        [RelayCommand]
        Task GoToDashboardPage(string user) => Shell.Current.GoToAsync($"{nameof(DashboardPage)}?User={user}");
    }
}