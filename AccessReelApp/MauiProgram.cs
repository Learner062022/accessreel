﻿using AccessReelApp.ViewModels;
using AccessReelApp.Views;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace AccessReelApp;

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

		// Setup for pages *required for both the page and the view models*
		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddTransient<MainViewModel>();
        builder.Services.AddSingleton<NewsPage>();
        builder.Services.AddTransient<NewsViewModel>();
        builder.Services.AddSingleton<ReviewsPage>();
        builder.Services.AddTransient<ReviewsViewModel>();
        builder.Services.AddSingleton<InterviewsPage>();
        builder.Services.AddTransient<InterviewsViewModel>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
