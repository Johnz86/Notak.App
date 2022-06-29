namespace Notak.ViewModel;

[QueryProperty(nameof(NoteTask), "NoteTask")]
public partial class DetailViewModel : BaseViewModel
{
    [ObservableProperty]
    NoteTask noteTask;

    [ICommand]
    async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }

    [ICommand]
    async Task Delete()
    {
        await Shell.Current.GoToAsync("..");
    }

    [ICommand]
    async Task Edit()
    {
        await Shell.Current.GoToAsync("..");
    }
}
