using DoToo.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoToo.Repositories
{
    public interface ISubTaskRepository
    {
        event EventHandler<SubTask> OnSubTaskAdded;
        event EventHandler<SubTask> OnSubTaskUpdated;
        event EventHandler<SubTask> OnSubTaskDeleted;

        Task<List<SubTask>> GetItems(int todoId);
        Task Add(SubTask subTask);
        Task Update(SubTask subTask);
        Task AddOrUpdate(SubTask subTask);
        Task<bool> DeleteOne(int id);
    }
}
