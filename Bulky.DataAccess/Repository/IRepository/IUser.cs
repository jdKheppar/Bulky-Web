using Bulky.Models;

namespace Bulky.DataAccess.Repository.IRepository
{
    public interface IUser : IRepository<User>
    {
        User GetByUsername(string username);
        User GetByEmail(string email);
        bool ValidateCredentials(string username, string password);
    }
}
