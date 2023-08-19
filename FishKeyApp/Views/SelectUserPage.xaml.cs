using FishKeyApp.ViewModels;

namespace FishKeyApp.Views;

public partial class SelectUserPage : ContentPage
{
    public readonly SelectUserViewModel _selectUserViewModel;
    public SelectUserPage(SelectUserViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}