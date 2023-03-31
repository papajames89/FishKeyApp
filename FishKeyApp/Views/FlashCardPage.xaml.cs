using FishKeyApp.ViewModels;

namespace FishKeyApp.Views
{
    public partial class FlashCardPage : ContentPage
    {
        public FlashCardPage(FlashCardViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }

        private async void Page_Appearing(object sender, EventArgs e)
        {
            base.OnAppearing();


            if (BindingContext != null && BindingContext is FlashCardViewModel vm)
            {
                await vm.InitAsync();
            }

        }
    }
}