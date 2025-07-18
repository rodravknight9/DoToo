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
        Task AddOrUpdate(SubTask subTask);
        Task Add(SubTask subTask);
        Task Update(SubTask subTask);
        Task UpdateAndAddMany(List<SubTask> subTasks);
        Task AddMany(List<SubTask> subTasks);
        Task UpdateMany(List<SubTask> subTasks);
        Task<bool> DeleteOne(int id);
    }
}
