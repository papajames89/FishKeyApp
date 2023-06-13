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
            PageHeader = "Wybierz swoje konto";
            Subtitle = "Ważne jest, aby zachować ostrożność podczas klikania przycisku usuwania, ponieważ ta czynność nie może zostać cofnięta.";
        }

        [RelayCommand]
        public async Task RemoveAllUsers()
        {
            await _databaseController.RemoveAllUsers();
            ListOfUsers = _databaseController.GetListOfUsers();
        }

        [RelayCommand]
        public async Task RemoveUser(string user)
        {
            await _databaseController.RemoveUser(user);
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