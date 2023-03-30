using Android.OS.Strictmode;
using FishKeyApp.ViewModels;
using FishKeyApp.Views;
using Java.Security;

namespace FishKeyApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(CreateUserPage), typeof(CreateUserPage));
            Routing.RegisterRoute(nameof(SelectUserPage), typeof(SelectUserPage));
            Routing.RegisterRoute(nameof(DashboardPage), typeof(DashboardPage));
            Routing.RegisterRoute(nameof(FlashCardPage), typeof(FlashCardPage));
        }
    }
}