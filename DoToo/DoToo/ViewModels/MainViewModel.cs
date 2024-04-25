using DoToo.Models;
using DoToo.Repositories;
using DoToo.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DoToo.ViewModels
{
    public class MainViewModel : ViewModel
    {

        private readonly TodoItemRepository _repository;

        //Note: ObservableCollection can notify listeners about changes in the list
        public ObservableCollection<TodoItemViewModel> Items { get; set; }
        public bool ShowAll { get; set; }
        public string FilterText => ShowAll ? "All" : "Active";

        public TodoItemViewModel SelectedItem
        {
            get { return null; }
            set 
            {
                Device.BeginInvokeOnMainThread(async () => await NavigateToItem(value));
                RaisePropertyChanged(nameof(SelectedItem));
            }
        }

        private async Task NavigateToItem(TodoItemViewModel item)
        {
            if (item == null)
                return;

            var itemView = Resolver.Resolve<ItemView>();
            var vm = itemView.BindingContext as ItemViewModel;
            vm.Item = item.Item;
            await Navigation.PushAsync(itemView);
        }


        public MainViewModel(TodoItemRepository repository)
        {
            repository.OnItemAdded += (sender, item) =>  Items.Add(CreateTodoItemViewModel(item));
            repository.OnItemUpdated += (sender, item) => Task.Run(async () => await LoadData());

            _repository = repository;
            Task.Run(async () => await LoadData());
        }

        private async Task LoadData()
        {
            var items = await _repository.GetItems();
            if (!ShowAll)
            {
                items = items.Where(x => x.Completed == false).ToList();
            }
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

        //Note: command must be a property, all command should have the ICommand type
        public ICommand AddItem => new Command(async () =>
        {
            var itemView = Resolver.Resolve<ItemView>();
            await Navigation.PushAsync(itemView);
        });

        public ICommand ToggleFilter => new Command(async () =>
        {
            ShowAll = !ShowAll;
            await LoadData();
        });
    }
}
