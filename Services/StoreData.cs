namespace Notak.Services;

public class Database
{
    private readonly SQLiteAsyncConnection _connection;

    public Database()
    {
        var dataDir = FileSystem.AppDataDirectory;
        var databasePath = Path.Combine(dataDir, "Notak.db");

        string _dbEncryptionKey = SecureStorage.GetAsync("dbKey").Result;

        if (string.IsNullOrEmpty(_dbEncryptionKey))
        {
            Guid g = new Guid();
            _dbEncryptionKey = g.ToString();
            SecureStorage.SetAsync("dbKey", _dbEncryptionKey);
        }

        var dbOptions = new SQLiteConnectionString(databasePath, true, key: _dbEncryptionKey);

        _connection = new SQLiteAsyncConnection(dbOptions);

        _ = Initialise();
    }

    private async Task Initialise()
    {
        await _connection.CreateTableAsync<NoteTask>();
    }

    public async Task<List<NoteTask>> GetTodos()
    {
        return await _connection.Table<NoteTask>().ToListAsync();

    }

    public async Task<NoteTask> GetTodo(int id)
    {
        var query = _connection.Table<NoteTask>().Where(t => t.Id == id);

        return await query.FirstOrDefaultAsync();
    }

    public async Task<int> AddTodo(NoteTask item)
    {
        return await _connection.InsertAsync(item);
    }

    public async Task<int> DeleteTodo(NoteTask item)
    {
        return await _connection.DeleteAsync(item);
    }

    public async Task<int> UpdateTodo(NoteTask item)
    {
        return await _connection.UpdateAsync(item);
    }
}
