using System.Threading.Tasks;

namespace DoToo.Utils
{
    public interface IMessageServices
    {
        Task<bool> ShowAsync(string message);
    }
}
