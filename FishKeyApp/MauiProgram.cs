using CommunityToolkit.Maui;
using epj.RadialDial.Maui;
using FishKeyApp.Controls;
#if ANDROID
using FishKeyApp.Platforms.Android;
#endif
using FishKeyApp.ViewModels;
using FishKeyApp.Views;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
using Plugin.Maui.Audio;

namespace FishKeyApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Roboto-Black.ttf", "RobotoBlack");
                    fonts.AddFont("Roboto-Bold.ttf", "RobotoBold");
                    fonts.AddFont("Roboto-Light.ttf", "RobotoLight");
                    fonts.AddFont("Roboto-Medium.ttf", "RobotoMedium");
                    fonts.AddFont("Roboto-Regular.ttf", "RobotoRegular");
                    fonts.AddFont("Roboto-Thin.ttf", "RobotoThin");
                })
                .UseRadialDial()
                .ConfigureLifecycleEvents(events =>
                {
#if ANDROID
                    events.AddAndroid(android => android.OnCreate((activity, bundle) => MakeStatusBarTranslucent(activity)));

                    static void MakeStatusBarTranslucent(Android.App.Activity activity)
                    {
                        activity.Window.SetFlags(Android.Views.WindowManagerFlags.LayoutNoLimits, Android.Views.WindowManagerFlags.LayoutNoLimits);

                        activity.Window.ClearFlags(Android.Views.WindowManagerFlags.TranslucentStatus);

                        activity.Window.SetStatusBarColor(Android.Graphics.Color.Transparent);
                    }
#endif
                });
#if ANDROID
            Microsoft.Maui.Handlers.ElementHandler.ElementMapper.AppendToMapping("Classic", (handler, view) =>
            {
                if (view is CustomEntry)
                {
                    EntryMapper.Map(handler, view);
                }
            });
#endif
            builder.Services.AddTransient<WelcomePage>();
            builder.Services.AddTransient<CreateUserPage>();
            builder.Services.AddTransient<SelectUserPage>();
            builder.Services.AddTransient<DashboardPage>();
            builder.Services.AddTransient<FlashCardPage>();

            builder.Services.AddTransient<WelcomeViewModel>();
            builder.Services.AddTransient<CreateUserViewModel>();
            builder.Services.AddTransient<SelectUserViewModel>();
            builder.Services.AddTransient<DashboardViewModel>();
            builder.Services.AddTransient<FlashCardViewModel>();

            builder.Services.AddSingleton(AudioManager.Current);

#if DEBUG
            builder.Logging.AddDebug();
#endif
            var app = builder.Build();
            return app;
        }
    }
}