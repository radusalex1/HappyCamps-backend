using HappyCamps_backend.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HappyCamps_backend.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        [Column(TypeName = "date")]
        public DateTime BirthDate { get; set; }

        public string PhoneNumber { get; set; }

        public string Instagram { get; set; }

        [Column("Role")]
        public Role RoleType { get; set; }

        public bool Accepted { get; set; } = false;
    }
}
