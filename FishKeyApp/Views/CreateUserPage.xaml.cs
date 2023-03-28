using FishKeyApp.ViewModels;

namespace FishKeyApp.Views;

public partial class CreateUserPage : ContentPage
{
    public CreateUserPage(CreateUserViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}