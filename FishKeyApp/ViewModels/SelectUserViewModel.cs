using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FishKeyApp.Controllers;
using FishKeyApp.Models;
using FishKeyApp.Views;
using System.Reflection.PortableExecutable;

namespace FishKeyApp.ViewModels
{
    [QueryProperty(nameof(ListOfUsers), nameof(ListOfUsers))]
    [QueryProperty(nameof(PageHeader), nameof(PageHeader))]
    [QueryProperty(nameof(Subtitle), nameof(Subtitle))]
    public partial class SelectUserViewModel : ObservableObject
    {
        private readonly DatabaseController _databaseController;
        public SelectUserViewModel()
        {
            _databaseController = new DatabaseController();
            ListOfUsers = _databaseController.GetListOfUsers();
            PageHeader = "Select your account";
            Subtitle = "It is important to exercise caution when clicking the remove button, as this action cannot be reversed.";
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

        [ObservableProperty]
        string pageHeader;

        [ObservableProperty]
        string subtitle;
    }
}