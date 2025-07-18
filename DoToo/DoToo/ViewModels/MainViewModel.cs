using DoToo.Models;
using DoToo.Repositories;
using DoToo.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DoToo.ViewModels
{
    public class MainViewModel : ViewModel
    {

        private readonly TodoItemRepository _repository;
        private readonly ISubTaskRepository _subTaskRepository;

        //Note: ObservableCollection can notify listeners about changes in the list
        public ObservableCollection<TodoItemViewModel> Items { get; set; }
        public bool ShowAll { get; set; }

        //Note: command must be a property, all command should have the ICommand type
        public ICommand AddItem => new Command(async () =>
        {
            var itemView = Resolver.Resolve<ItemView>();
            var vm = itemView.BindingContext as ItemViewModel;
            vm.SubTasks = new ObservableCollection<SubtaskViewModel>(new List<SubtaskViewModel>());
            await Navigation.PushAsync(itemView);
        });

        public ICommand ToggleFilter => new Command(async () =>
        {
            ShowAll = !ShowAll;
            await LoadData();
        });

        /// <summary>
        /// Used in the Selected Item View list property
        /// </summary>
        public TodoItemViewModel SelectedItem
        {
            get { return null; }
            set
            {
                Device.BeginInvokeOnMainThread(async () => await NavigateToItem(value));
                RaisePropertyChanged(nameof(SelectedItem));
            }
        }

        public MainViewModel(TodoItemRepository repository, SubTaskRepository subTaskRepository)
        {
            // define the event listeners when adding/updating an item
            repository.OnItemAdded += (sender, item) => Items.Add(CreateTodoItemViewModel(item));
            repository.OnItemUpdated += (sender, item) => Task.Run(async () => await LoadData());

            _repository = repository;
            _subTaskRepository = subTaskRepository;
            Task.Run(async () => await LoadData());
        }

        private async Task NavigateToItem(TodoItemViewModel item)
        {
            if (item == null)
                return;

            var itemView = Resolver.Resolve<ItemView>();
            var vm = itemView.BindingContext as ItemViewModel;
            vm.Item = item.Item;
            vm.Load();

            if (vm.Item.Id > 0)
            {
                var test = await _subTaskRepository.GetItems(vm.Item.Id);
                var subTasks = test
                    .Select(t => vm.CreateSubTaskViewModel(t));
                vm.SubTasks = new ObservableCollection<SubtaskViewModel>(subTasks);
            }

            vm.canBeDeleted = vm.Item.Id > 0;
            await Navigation.PushAsync(itemView);
        }

        private async Task LoadData()
        {
            var items = await _repository.GetItems(ShowAll);
            var itemViewModels = items.Select(i => CreateTodoItemViewModel(i));

            Items = new ObservableCollection<TodoItemViewModel>(itemViewModels);
        }

        private TodoItemViewModel CreateTodoItemViewModel(TodoItem item)
        {
            var itemViewModel = new TodoItemViewModel(item);
            itemViewModel.ItemStatusChanged += ItemStatusChanged;
            return itemViewModel;
        }

        private void ItemStatusChanged(object sender, EventArgs e)
        {
            if (sender is TodoItemViewModel item)
            {
                if (!ShowAll && item.Item.Completed)
                {
                    //Note: since we are using the ObservableCollection this will autoamtically update
                    Items.Remove(item); 
                }

                Task.Run(async () => await _repository.UpdateItem(item.Item));

            }
        }

        
    }
}
