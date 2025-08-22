using Bulky.Models;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.DataAcess;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace Bulky.DataAccess.Repository.IRepository
{
    public class UserRepository : Repository<User>, IUser
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public User GetByUsername(string username)
        {
            return _db.JUsers.FirstOrDefault(u => u.Username == username && u.IsActive);
        }

        public User GetByEmail(string email)
        {
            return _db.JUsers.FirstOrDefault(u => u.Email == email && u.IsActive);
        }

        public bool ValidateCredentials(string username, string password)
        {
            var user = GetByUsername(username);
            if (user == null) return false;
            
            return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
        }
    }
}
