using HappyCamps_backend.Models;

namespace HappyCamps_backend.DTOs
{
    public class TokenizedUser:User
    {
        public TokenizedUser(User user)
        {
            this.user = user;
        }

        public User user { get; set; }
        public string Token { get; set; }
    }
}
