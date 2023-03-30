using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FishKeyApp.Controllers;
using FishKeyApp.Models;
using FishKeyApp.Views;

namespace FishKeyApp.ViewModels
{
    public partial class SelectUserViewModel : ObservableObject
    {
        public List<UserModel> ListOfUsers { get; set; }
        private readonly DatabaseController _databaseController;
        public SelectUserViewModel()
        {
            _databaseController = new DatabaseController();
            ListOfUsers = _databaseController.GetListOfUsers();
        }

        [RelayCommand]
        Task GoBack() => Shell.Current.GoToAsync("..");

        [RelayCommand]
        Task GoToDashboardPage(string user) => Shell.Current.GoToAsync($"{nameof(DashboardPage)}?User={user}");
    }
}