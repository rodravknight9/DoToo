using SQLite;

namespace DoToo.Models
{
    public class SubTask
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Detail { get; set; }
        public int TodoItemId { get; set; }
        public bool IsCompleted { get; set; }
    }
}
