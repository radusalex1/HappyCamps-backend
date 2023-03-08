namespace HappyCamps_backend.Validator
{
    public interface IValidator
    {
        /// <summary>
        /// This method checks if the email is valid.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>True if valid,False otherwise.</returns>
        public bool IsEmailValid(string email);
    }
}