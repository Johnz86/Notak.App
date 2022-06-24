namespace Notak.View;

public partial class SettingsPage : ContentPage
{
	public SettingsPage(NoteTaskViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}