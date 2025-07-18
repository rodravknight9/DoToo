using DoToo.Models;
using DoToo.Repositories;
using DoToo.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DoToo.ViewModels
{
    public class ItemViewModel : ViewModel
    {
        private readonly TodoItemRepository _repository;
        private readonly ISubTaskRepository _taskRepository;
        private readonly IMessageServices _messageServices;
        public TodoItem Item {
            //get { return Item; }
            //set
            //{
            //    Item = value;
            //    NewSubTask.Id = value.Id;
            //}
            get; set;
        }
        public bool canBeDeleted { get; set; }
        public ObservableCollection<SubtaskViewModel> SubTasks { get; set; }
        public SubTask NewSubTask { get; set; }

        public ItemViewModel(
            TodoItemRepository repository, 
            SubTaskRepository subTaskRepository,
            IMessageServices messageServices)
        {
            _repository = repository;
            _taskRepository = subTaskRepository;            
            _messageServices = messageServices;

            Item = new TodoItem
            {
                Title = string.Empty,
                Due = DateTime.Now.AddDays(1)
            };
            NewSubTask = new SubTask
            {
                Detail = string.Empty,
                IsCompleted = false
            };
        }

        public void Load()
        {
            NewSubTask.TodoItemId = Item.Id;
        }

        public ICommand Save => new Command(async () =>
        {
            await _repository.AddOrUpdate(Item);
            await _taskRepository.UpdateAndAddMany(SubTasks.Select(s => s.SubTask).ToList());

            await _messageServices.ShowAsync("Item has been saved");
            await Navigation.PopAsync();
        });

        public ICommand Delete => new Command(async () =>
        {
            if(Item.Id <= 0) return;

            bool isOk = await _messageServices.AskAsync("Are you sure you want to delete this item?");
            if (!isOk) return;

            await _repository.DeleteItem(Item.Id);
            await Navigation.PopAsync();
        });

        public ICommand AddSubTask => new Command(async () =>
        {
            if (Item.Id <= 0)
            { 
                await _messageServices.ShowAsync("Todo has not been saved yet");
                return;
            }

            if (string.IsNullOrEmpty(NewSubTask.Detail))
            {
                return;
            }
            
            SubTasks.Add(CreateSubTaskViewModel(NewSubTask));
        });

        public SubtaskViewModel CreateSubTaskViewModel(SubTask subTask)
        {
            var subTaskVm = new SubtaskViewModel(subTask);
            return subTaskVm;
        }
    }
}
