using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using FishKeyApp.Views;

namespace FishKeyApp.ViewModels
{
    [QueryProperty(nameof(Subtitle), nameof(Subtitle))]
    public partial class WelcomeViewModel : ObservableObject
    {
        public WelcomeViewModel()
        {
            Subtitle = "By tapping Create User or Sign In, you agree to our Terms.";
        }
        [RelayCommand]
        Task GoToCreateUserPage() => Shell.Current.GoToAsync(nameof(CreateUserPage));

        [RelayCommand]
        Task GoToSelectUserPage() => Shell.Current.GoToAsync(nameof(SelectUserPage));

        [ObservableProperty]
        string subtitle;
    }
}