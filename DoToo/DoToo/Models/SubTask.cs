namespace DoToo.Models
{
    //TODO: support subtasks
    public class SubTask
    {
        public int Id { get; set; }
        public string Detail { get; set; }
        public int TodoItemId { get; set; }
        public bool IsCompleted { get; set; }
    }
}
