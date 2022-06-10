namespace Notak.View;

public partial class MainPage : ContentPage
{

	public MainPage(NoteTaskViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}

