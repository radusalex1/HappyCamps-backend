using HappyCamps_backend.DTOs;
using HappyCamps_backend.Validator;

namespace HappyCamps_backend.Services
{
    public class ValidateUserLoginDTO : IValidateUserLoginDTO,IValidator
    { 
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

        public bool IsValid(UserLoginDto userLoginDto)
        {
            if(userLoginDto == null)
            {
                return false;
            }

            if(string.IsNullOrEmpty(userLoginDto.Email))
            {
                return false;
            }

            if(!IsEmailValid(userLoginDto.Email))
            {
                return false;
            }

            if (string.IsNullOrEmpty(userLoginDto.Password))
            {
                return false;
            }

            return true;
        }
    }
}
