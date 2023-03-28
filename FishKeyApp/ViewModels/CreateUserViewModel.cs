using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace FishKeyApp.ViewModels
{
    public partial class CreateUserViewModel : ObservableObject
    {
        [RelayCommand]
        Task GoBack() => Shell.Current.GoToAsync("..");
    }
}