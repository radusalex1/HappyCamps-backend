using HappyCamps_backend.Context;
using HappyCamps_backend.Models;
using HappyCamps_backend.Validator;

namespace HappyCamps_backend.Services
{
    public class ValidateNewUser : IValidateNewUser,IValidator
    {
        private readonly HappyCampsDataContext _DbContext;

        public ValidateNewUser(HappyCampsDataContext happyCampsDataContext)
        {
            _DbContext = happyCampsDataContext;
        }

        public async Task<bool> IsValid(User user)
        {
            if (user == null)
            {
                return false;
            }

            if (string.IsNullOrEmpty(user.FirstName))
            {
                return false;
            }

            if (string.IsNullOrEmpty(user.LastName))
            {
                return false;
            }

            if (string.IsNullOrEmpty(user.Email))
            {
                return false;
            }

            if (!IsEmailValid(user.Email))
            {
                return false;
            }

            if (string.IsNullOrEmpty(user.Password))
            {
                return false;
            }

            if (user.BirthDate == null)
            {
                return false;
            }

            if (string.IsNullOrEmpty(user.PhoneNumber))
            {
                return false;
            }

            if (string.IsNullOrEmpty(user.RoleType.ToString()))
            {
                return false;
            }

            return true;
        }

        public async Task<bool> IsUserUnique(User user)
        {
            var existingUser = _DbContext.Users
                .FirstOrDefault(x => x.Email == user.Email && x.PhoneNumber == user.PhoneNumber);

            if (existingUser != null)
            {
                return false;
            }

            return true;
        }

        public bool IsEmailValid(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
