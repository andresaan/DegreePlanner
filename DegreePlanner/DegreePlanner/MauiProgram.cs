using DegreePlanner.View;
using DegreePlanner.ViewModel;
using CommunityToolkit.Maui;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Plugin.LocalNotification;

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
                //.UseLocalNotification()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<DegreePlanView>();
            builder.Services.AddTransient<DegreePlanViewModel>();

            builder.Services.AddTransient<AddEditCourseView>();
            builder.Services.AddTransient<AddEditCourseViewModel>();

            builder.Services.AddTransient<EditTermView>();
            builder.Services.AddTransient<EditTermViewModel>();

            builder.Services.AddTransient<ITermService, TermService>();
            builder.Services.AddTransient<ISetNotificationService, SetNotificationService>();

            builder.Services.AddTransient<AddTermPopup>();



            return builder.Build();
        }
    }
}