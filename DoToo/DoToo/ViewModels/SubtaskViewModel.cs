using DoToo.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoToo.ViewModels
{
    public class SubtaskViewModel : ViewModel
    {
        public SubTask SubTask { get; set; }

        public SubtaskViewModel(SubTask subTask)
        {
            SubTask = subTask;
        }
    }
}
