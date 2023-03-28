using FishKeyApp.ViewModels;

namespace FishKeyApp.Views;

public partial class FlashCardPage : ContentPage
{
	public FlashCardPage(FlashCardViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}