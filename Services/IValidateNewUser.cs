namespace HappyCamps_backend.Services
{
    public interface IValidateNewUser
    {
        public Task<bool> HasUniqueEmail(string email);
    }
}
