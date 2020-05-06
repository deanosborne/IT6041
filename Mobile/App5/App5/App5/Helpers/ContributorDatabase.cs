using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using App5.Models;

namespace App5.Helpers
{
    public class ContributorDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public ContributorDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Contributors>().Wait();
        }

        public Task<List<Contributors>> GetNotesAsync()
        {
            return _database.Table<Contributors>().ToListAsync();
        }

        public Task<Contributors> GetNoteAsync(int id)
        {
            return _database.Table<Contributors>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveNoteAsync(Contributors note)
        {
            if (note.ID != 0)
            {
                return _database.UpdateAsync(note);
            }
            else
            {
                return _database.InsertAsync(note);
            }
        }

        public Task<int> DeleteNoteAsync(Contributors note)
        {
            return _database.DeleteAsync(note);
        }
    }
}