using HappyCamps_backend.Common;
using System.ComponentModel.DataAnnotations;

namespace HappyCamps_backend.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        
    }
}
