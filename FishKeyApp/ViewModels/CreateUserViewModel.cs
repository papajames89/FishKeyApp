using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FishKeyApp.Controllers;
using FishKeyApp.Models;
using FishKeyApp.Views;

namespace FishKeyApp.ViewModels
{
    [QueryProperty(nameof(UserName), nameof(UserName))]
    [QueryProperty(nameof(ListOfUsers), nameof(ListOfUsers))]
    public partial class CreateUserViewModel : ObservableObject
    {
        private readonly DatabaseController _databaseController;
        public CreateUserViewModel()
        {
            _databaseController = new DatabaseController();
            ListOfUsers = _databaseController.GetListOfUsers();
            var test = FileSystem.Current.AppDataDirectory;
        }
        [RelayCommand]
        Task GoBack()
        {
            UserName = string.Empty;
            return Shell.Current.GoToAsync("..");
        }
        

        [RelayCommand]
        public void CreateUser()
        {
            _databaseController.CreateDatabase(UserName);
            ListOfUsers = _databaseController.GetListOfUsers();
            UserName = string.Empty;
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
        Task GoToDashboardPage(string user) => Shell.Current.GoToAsync($"{nameof(DashboardPage)}?User={user}");

        [ObservableProperty]
        private string userName;

        [ObservableProperty]
        private List<UserModel> listOfUsers;
    }
}