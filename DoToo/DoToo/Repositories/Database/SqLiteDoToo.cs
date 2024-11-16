using DoToo.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DoToo.Repositories.Database
{
    public class SqLiteDoToo : ISqLiteDoToo
    {
        private SQLiteAsyncConnection _connection;
        public SQLiteAsyncConnection CreateConnection()
        {
            if (_connection != null)
                return _connection;

            var documentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var databasePath = Path.Combine(documentPath, "TodoItems.db");

            //TODO: move this in a differet folder
            _connection = new SQLiteAsyncConnection(databasePath);
            //await _connection.CreateTableAsync<TodoItem>();
            //await _connection.CreateTableAsync<SubTask>();

            return _connection;
        }

        public async Task CreateOrMigrateTables()
        {
            await _connection.CreateTableAsync<TodoItem>();
            await _connection.CreateTableAsync<SubTask>();
        }
    }
}
