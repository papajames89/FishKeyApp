using FishKeyApp.ViewModels;

namespace FishKeyApp.Views;

public partial class SelectUserPage : ContentPage
{
	public SelectUserPage(SelectUserViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}