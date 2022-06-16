using Notak.View;

namespace Notak;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        if (DeviceInfo.Idiom == DeviceIdiom.Phone) {
            Shell.Current.CurrentItem = PhoneTabs;
        }
        Routing.RegisterRoute(nameof(DetailPage), typeof(DetailPage));
        Routing.RegisterRoute(nameof(TagsPage), typeof(TagsPage));
        Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
    }

    async void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
    {
        try
        {
            await Shell.Current.GoToAsync($"{nameof(SettingsPage)}");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"err: {ex.Message}");
        }
    }

    
    async Task GoBack()
    {
        try
        {
            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"err: {ex.Message}");
        }
    }
}
