using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FishKeyApp.Controllers;
using FishKeyApp.Models;
using FishKeyApp.Views;

namespace FishKeyApp.ViewModels
{
    [QueryProperty(nameof(ListOfUsers), nameof(ListOfUsers))]
    public partial class SelectUserViewModel : ObservableObject
    {
        private readonly DatabaseController _databaseController;
        public SelectUserViewModel()
        {
            _databaseController = new DatabaseController();
            ListOfUsers = _databaseController.GetListOfUsers();
        }

        [RelayCommand]
        public void RemoveAllUsers()
        {
            _databaseController.RemoveAllUsers();
            ListOfUsers = _databaseController.GetListOfUsers();
        }

        [RelayCommand]
        public void RemoveUser(string user)
        {
            _databaseController.RemoveUser(user);
            ListOfUsers = _databaseController.GetListOfUsers();
        }

        [RelayCommand]
        Task GoBack() => Shell.Current.GoToAsync("..");

        [RelayCommand]
        Task GoToDashboardPage(string user) => Shell.Current.GoToAsync($"{nameof(DashboardPage)}?User={user}");

        [ObservableProperty]
        public List<UserModel> listOfUsers;
    }
}