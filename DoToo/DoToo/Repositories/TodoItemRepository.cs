using DoToo.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using DoToo.Repositories.Database;

namespace DoToo.Repositories
{
    public class TodoItemRepository : ITodoItemRepository
    {
        private ISqLiteDoToo _database;
        private SQLiteAsyncConnection _connection;

        public event EventHandler<TodoItem> OnItemAdded;
        public event EventHandler<TodoItem> OnItemUpdated;
        public event EventHandler<TodoItem> OnItemDeleted;

        public TodoItemRepository(ISqLiteDoToo database)
        {
            _database = database;
            _connection = database.CreateConnection();
        }


        public async Task<List<TodoItem>> GetItems()
        {
            return await _connection.Table<TodoItem>().ToListAsync();
        }

        public async Task<List<TodoItem>> GetItems(bool isShowAll)
        {

            Func<TodoItem, bool> test = (todo) =>
            {
                if(isShowAll) return true;
                return todo.Completed == false;
            };
            return await _connection.Table<TodoItem>()
               //.Where(t => test(t))
                .ToListAsync();
        }

        public async Task AddOrUpdate(TodoItem item)
        {
            if (item.Id == 0)
            {
                await AddItem(item);
            }
            else
            {
                await UpdateItem(item);
            }
        }

        public async Task AddItem(TodoItem item)
        {
            await _connection.InsertAsync(item);
            OnItemAdded?.Invoke(this, item);
        }

        public async Task UpdateItem(TodoItem item)
        {
            await _connection.UpdateAsync(item);
            OnItemUpdated?.Invoke(this, item);
        }
        
        public async Task<bool> DeleteItem(int id)
        {
            TodoItem item = await _connection.Table<TodoItem>()
                .Where(t => t.Id == id)
                .FirstOrDefaultAsync();

            if (item == null)
                return false;

            await _connection.DeleteAsync(item);
            OnItemUpdated?.Invoke(this, item);
            return true;
        }

    }
}
