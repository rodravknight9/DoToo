using DoToo.Models;
using DoToo.Repositories;
using DoToo.Utils;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace DoToo.ViewModels
{
    public class ItemViewModel : ViewModel
    {
        private readonly TodoItemRepository _repository;
        private readonly IMessageServices _messageServices;
        public TodoItem Item { get; set; }
        public ItemViewModel(TodoItemRepository repository, IMessageServices messageServices)
        {
            _repository = repository;
            _messageServices = messageServices;
            Item = new TodoItem
            {
                Title = string.Empty,
                Due = DateTime.Now.AddDays(1)
            };
        }

        public ICommand Save => new Command(async () =>
        {
            await _repository.AddOrUpdate(Item);
            await Navigation.PopAsync();
        });

        public ICommand Delete => new Command(async () =>
        {
            if(Item.Id <= 0) return;
            bool isOk = await _messageServices.AskAsync("Are you sure you want to delete this item?");
            if (!isOk) return;
            await Navigation.PopAsync();
        });

    }
}
