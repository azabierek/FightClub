using FightClub.Services;
using FightClub.View;
using FightClub.ViewModel;
using Microsoft.Extensions.Logging;

namespace FightClub;

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

		builder.Services.AddSingleton<FightClubRepository>();

		builder.Services.AddSingleton<MainPageViewModel>();
		builder.Services.AddTransient<FighterDetailsViewModel>();

		builder.Services.AddSingleton<MainPage>();
        builder.Services.AddTransient<FighterDetailsPage>();


#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
