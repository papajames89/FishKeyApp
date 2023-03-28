using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using FishKeyApp.Views;

namespace FishKeyApp.ViewModels
{
    public partial class WelcomeViewModel : ObservableObject
    {
        [RelayCommand]
        Task GoToCreateUserPage() => Shell.Current.GoToAsync(nameof(CreateUserPage));

        [RelayCommand]
        Task GoToSelectUserPage() => Shell.Current.GoToAsync(nameof(SelectUserPage));
    }
}