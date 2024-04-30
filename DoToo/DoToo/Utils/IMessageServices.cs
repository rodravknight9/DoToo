using System.Threading.Tasks;

namespace DoToo.Utils
{
    public interface IMessageServices
    {
        Task ShowAsync(string message);
        Task<bool> AskAsync(string message);
    }
}
