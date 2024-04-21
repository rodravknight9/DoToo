using DoToo.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoToo.ViewModel
{
    public class ItemViewModel : ViewModel
    {
        private readonly TodoItemRepository _repository;
        public ItemViewModel(TodoItemRepository repository)
        {
            _repository = repository;
        }

    }
}
