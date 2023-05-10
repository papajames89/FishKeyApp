using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FishKeyApp.Controllers;
using FishKeyApp.Models;
using FishKeyApp.Views;

namespace FishKeyApp.ViewModels
{
    [QueryProperty(nameof(UserName), nameof(UserName))]
    [QueryProperty(nameof(PageHeader), nameof(PageHeader))]
    [QueryProperty(nameof(Subtitle), nameof(Subtitle))]
    public partial class CreateUserViewModel : ObservableObject
    {
        private readonly DatabaseController _databaseController;
        public CreateUserViewModel()
        {
            _databaseController = new DatabaseController();
            var test = FileSystem.Current.AppDataDirectory;
            PageHeader = "My first name is";
            Subtitle = "This is how your account will appear in FishKey app and you will not be able to change it";
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
            UserName = string.Empty;
        }

        [RelayCommand]
        public void RemoveAllUsers()
        {
            _databaseController.RemoveAllUsers();
        }

        [RelayCommand]
        Task GoToDashboardPage(string user) => Shell.Current.GoToAsync($"{nameof(DashboardPage)}?User={user}");

        [ObservableProperty]
        private string userName;

        [ObservableProperty]
        string pageHeader;

        [ObservableProperty]
        string subtitle;
    }
}