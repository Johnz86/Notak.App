namespace Notak.Services;

public class StoreData
{
    private SQLiteAsyncConnection _connection;
    private SQLiteConnectionString _db_connection;

    public StoreData()
    {
        _ = SetupStorage();
    }

    private string StorePath() {
        var databasePath = Path.Combine(FileSystem.AppDataDirectory, "Notak.db");
        System.Diagnostics.Debug.WriteLine($"Store path: {databasePath}");
        return databasePath;
    }

    private string GetSecureKey() {
        string _dbEncryptionKey = SecureStorage.GetAsync("dbKey").Result;
        if (string.IsNullOrEmpty(_dbEncryptionKey))
        {
            Guid g = new Guid();
            _dbEncryptionKey = g.ToString();
            SecureStorage.SetAsync("dbKey", _dbEncryptionKey);
        }
        System.Diagnostics.Debug.WriteLine($"Key: {_dbEncryptionKey}");
        return _dbEncryptionKey;
    }

    private async Task SetupStorage() {
        _db_connection = new SQLiteConnectionString(StorePath(), true, key: GetSecureKey());
        _connection = new SQLiteAsyncConnection(_db_connection);
        await Initialise();
    }

    private async Task Initialise()
    {
        await _connection.CreateTableAsync<NoteTask>();
    }

    public async Task<List<NoteTask>> GetNotes()
    {
        return await _connection.Table<NoteTask>().ToListAsync();

    }

    public async Task<NoteTask> GetNote(int id)
    {
        var query = _connection.Table<NoteTask>().Where(t => t.Id == id);

        return await query.FirstOrDefaultAsync();
    }

    public async Task<int> AddNote(NoteTask item)
    {
        return await _connection.InsertAsync(item);
    }

    public async Task<int> DeleteNote(NoteTask item)
    {
        return await _connection.DeleteAsync(item);
    }

    public async Task<int> UpdateNote(NoteTask item)
    {
        return await _connection.UpdateAsync(item);
    }

    public async Task CleanUp() {
        await _connection.CloseAsync();
        File.Delete(StorePath());
        SecureStorage.Remove("dbKey");
        await SetupStorage();
    }
}
