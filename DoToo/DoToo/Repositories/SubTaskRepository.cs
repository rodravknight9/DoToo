using DoToo.Models;
using DoToo.Repositories.Database;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DoToo.Repositories
{
    public class SubTaskRepository : ISubTaskRepository
    {
        private ISqLiteDoToo _database;
        private SQLiteAsyncConnection _connection;

        public event EventHandler<SubTask> OnSubTaskAdded;
        public event EventHandler<SubTask> OnSubTaskUpdated;
        public event EventHandler<SubTask> OnSubTaskDeleted;

        public SubTaskRepository(ISqLiteDoToo database)
        {
            _database = database;
            _connection = database.CreateConnection();
        }

        public async Task Add(SubTask subTask)
        {
            await _connection.InsertAsync(subTask);
            OnSubTaskAdded?.Invoke(this, subTask);
        }

        public async Task AddOrUpdate(SubTask subTask)
        {
            if (subTask.Id == 0)
            {
                await Add(subTask);
            }
            else
            {
                await Update(subTask);
            }
        }

        public Task<bool> DeleteOne(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<SubTask>> GetItems(int todoId)
        {
            return await _connection.Table<SubTask>()
                .Where(t => t.TodoItemId.Equals(todoId))
                .ToListAsync();
        }

        public async Task Update(SubTask subTask)
        {
            await _connection.UpdateAsync(subTask);
            OnSubTaskUpdated?.Invoke(this, subTask);
        }
    }
}
