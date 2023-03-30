using FishKeyApp.Controls;
using FishKeyApp.Platforms.Android;
using FishKeyApp.ViewModels;
using FishKeyApp.Views;
using Microsoft.Extensions.Logging;
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
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            Microsoft.Maui.Handlers.ElementHandler.ElementMapper.AppendToMapping("Classic", (handler, view) =>
            {
                if (view is CustomEntry)
                {
                    EntryMapper.Map(handler, view);
                }
            });

            builder.Services.AddSingleton<WelcomePage>();
            builder.Services.AddSingleton<CreateUserPage>();
            builder.Services.AddSingleton<SelectUserPage>();
            builder.Services.AddSingleton<DashboardPage>();
            builder.Services.AddSingleton<FlashCardPage>();

            builder.Services.AddSingleton<WelcomeViewModel>();
            builder.Services.AddSingleton<CreateUserViewModel>();
            builder.Services.AddSingleton<SelectUserViewModel>();
            builder.Services.AddSingleton<DashboardViewModel>();
            builder.Services.AddSingleton<FlashCardViewModel>();

            builder.Services.AddSingleton(AudioManager.Current);

#if DEBUG
            builder.Logging.AddDebug();
#endif
            var app = builder.Build();
            return app;
        }
    }
}