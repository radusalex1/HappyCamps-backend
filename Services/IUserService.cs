using HappyCamps_backend.Models;

namespace HappyCamps_backend.Services
{
    public interface IUserService
    {
        public User GetUserByEmail(string email);

        public bool SaveNewUser(User user);
    }
}
