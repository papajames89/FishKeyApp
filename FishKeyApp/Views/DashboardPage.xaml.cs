using FishKeyApp.ViewModels;
using Java.Lang;

namespace FishKeyApp.Views
{
    public partial class DashboardPage : ContentPage
    {
        public DashboardPage(DashboardViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }

        private async void Page_Appearing(object sender, EventArgs e)
        {
            base.OnAppearing();


            if (BindingContext != null && BindingContext is DashboardViewModel vm)
            {
                await vm.InitAsync();
            }
        }
    }
}