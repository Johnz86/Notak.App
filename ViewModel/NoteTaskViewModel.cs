using Notak.Model;
using Notak.Services;
using Notak.View;

namespace Notak.ViewModel;

public partial class NoteTaskViewModel: BaseViewModel
{
    readonly StoreData _database;
    
    public NoteTaskViewModel(StoreData storeData)
    {
        IsBusy = true;
        Title = "Notak - Simple Notes";
        _database = storeData;
        _ = Initialise();
        IsBusy = false;
    }

    async Task Initialise()
    { 
        var notes = await _database.GetNotes();
        NoteTasks = new(notes);
    }

    [ObservableProperty]
    ObservableCollection<NoteTask> noteTasks;

    [ObservableProperty]
    string text;

    [ICommand]
    async Task Add()
    {
        if (string.IsNullOrWhiteSpace(Text))
            return;
        var note = new NoteTask
        {
            Title = Text,
            Done = false,
            Due = DateTime.Now,
        };
        var inserted = await _database.AddNote(note);
        if (inserted != 0) {
            NoteTasks.Add(note);
        }
        Text = string.Empty;
    }

    [ICommand]
    async void Delete(int id)
    {
        if (NoteTasks.Count > 0)
        {
            NoteTasks.RemoveAt(id);
        }
    }

    [ICommand]
    async Task Tap(string s)
    {
        await Shell.Current.GoToAsync($"{nameof(DetailPage)}?Text={s}");
    }

    [ICommand]
    async Task CleanUp() 
    { 
        NoteTasks.Clear();
        await _database.CleanUp();
    }

}

