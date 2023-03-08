using HappyCamps_backend.Context;

namespace HappyCamps_backend.Services
{
    public class ValidateNewUser : IValidateNewUser
    {
        private readonly HappyCampsDataContext _DbContext;
        public ValidateNewUser(HappyCampsDataContext happyCampsDataContext)
        {
            this._DbContext = happyCampsDataContext;
        }

        public async Task<bool> HasUniqueEmail(string email)
        {

            var user = _DbContext.Users.FirstOrDefault(x => x.Email == email);

            if(user==null)
            {
                return true;
            }

            return false;
        }
    }
}
