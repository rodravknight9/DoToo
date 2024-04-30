using System.Threading.Tasks;

namespace DoToo.Utils
{
    public class MessageService : IMessageServices
    {
        public async Task ShowAsync(string message)
        {
            await App.Current.MainPage.DisplayAlert("DoToo", message, "Ok");
        }

        public async Task<bool> AskAsync(string message)
        {
            return await App.Current.MainPage.DisplayAlert("DoToo", message, "Ok", "Cancel");
        }
    }
}
