using Notak.Model;
using Notak.Services;
using Notak.View;

namespace Notak.ViewModel;

public partial class NoteTaskViewModel: BaseViewModel
{
    readonly Database _database;
    public ObservableCollection<NoteTask> NoteTasks { get; set; } = new();
    
    public NoteTaskViewModel()
    {
        Items = new ObservableCollection<string>();
        _database = new Database();
        _ = Initialise();
    }

    async Task Initialise()
    { 
        var notes = await _database.GetTodos();
        foreach (var note in notes)
        {
            NoteTasks.Add(note);
            Items.Add(note.Title);
        }
    }

    [ObservableProperty]
    ObservableCollection<string> items;

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
        var inserted = await _database.AddTodo(note);
        if (inserted != 0) {
            Items.Add(Text);
        }
        Text = string.Empty;
    }

    [ICommand]
    async void Delete(string s)
    {
        if (Items.Contains(s))
        {
            Items.Remove(s);
        }
    }

    [ICommand]
    async Task Tap(string s)
    {
        await Shell.Current.GoToAsync($"{nameof(DetailPage)}?Text={s}");
    }

}

