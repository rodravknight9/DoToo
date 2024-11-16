using SQLite;
using System.Threading.Tasks;

namespace DoToo.Repositories.Database
{
    public interface ISqLiteDoToo
    {
        SQLiteAsyncConnection CreateConnection();
        Task CreateOrMigrateTables();
    }
}
