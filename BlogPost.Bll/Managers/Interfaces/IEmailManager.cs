using System.Threading.Tasks;

namespace BlogPost.Bll.Managers.Interfaces
{
    public interface IEmailManager
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
