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