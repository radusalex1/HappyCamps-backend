using HappyCamps_backend.DTOs;

namespace HappyCamps_backend.Services
{
    public interface IValidateUserLoginDTO
    {
        /// <summary>
        /// This method checks if user has any null or empty fields.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>False if it has null or empty fields,True otherwise.</returns>
        public bool IsValid(UserLoginDto userLoginDto);
    }
}
