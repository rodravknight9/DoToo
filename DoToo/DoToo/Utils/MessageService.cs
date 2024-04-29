using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DoToo.Utils
{
    public class MessageService : IMessageServices
    {
        public async Task<bool> ShowAsync(string message)
        {
            return await App.Current.MainPage.DisplayAlert("YourApp", message, "Ok", "Cancel");
        }
    }
}
