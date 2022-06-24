using Notak.View;
using Notak.Services;
namespace Notak;

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

		builder.Services.AddSingleton<StoreData>();
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<NoteTaskViewModel>();

        builder.Services.AddSingleton<SettingsPage>();
        builder.Services.AddSingleton<TagsPage>();

        builder.Services.AddTransient<DetailPage>();
        builder.Services.AddTransient<DetailViewModel>();
        return builder.Build();
	}
}
