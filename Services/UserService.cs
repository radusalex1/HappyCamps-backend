using Arch.EntityFrameworkCore;
using HappyCamps_backend.Context;
using HappyCamps_backend.Models;

namespace HappyCamps_backend.Services
{
    public class UserService : IUserService
    {
        private readonly HappyCampsDataContext happyCampsDataContext_context;
        public UserService(HappyCampsDataContext happyCampsDataContext)
        {
            this.happyCampsDataContext_context = happyCampsDataContext;
        }
        public User? GetUserByEmail(string email)
        {
            var user = happyCampsDataContext_context.Users
                 .FirstOrDefault(x => x.Email == email);
            if (user == null)
            {
                return null;
            }
           return user;
        }

        public bool SaveNewUser(User user)
        {
            if(user == null) return false;

            happyCampsDataContext_context.Users.Add(user);

            happyCampsDataContext_context.SaveChanges();

            return true;
        }
    }
}
