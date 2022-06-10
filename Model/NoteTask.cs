namespace Notak.Model;

public class NoteTask
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime Due { get; set; }
    public bool Done { get; set; }
}