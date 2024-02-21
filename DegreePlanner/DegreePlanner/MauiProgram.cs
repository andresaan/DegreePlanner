using DegreePlanner.View;
using DegreePlanner.ViewModel;
using DAL;
using CommunityToolkit.Maui;

namespace DegreePlanner
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
                });

            builder.Services.AddSingleton<DegreePlanView>();
            builder.Services.AddTransient<DegreePlanViewModel>();

            builder.Services.AddTransient<AddTermPopup>();

            return builder.Build();
        }
    }
}