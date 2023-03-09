using HappyCamps_backend.Models;

namespace HappyCamps_backend.Services
{
    public interface IValidateNewUser
    {
        /// <summary>
        /// This method checks if user is valid.(It has correct fields or not null fields)
        /// </summary>
        /// <param name="user"></param>
        /// <returns>False if it has null or empty fields,True otherwise.</returns>
        public Task<bool> IsValid(User user);

        /// <summary>
        /// This method verifies if the user is unique. It checks after the uniqueness fields.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>True if is unique,False otherwise.</returns>
        public Task<bool> IsUserUnique(User user);
    }
}
