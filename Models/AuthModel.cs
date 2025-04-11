namespace LoginAuth.Models
{
    /// <summary>
    /// Model class representing the login request data
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// User's email address used for authentication
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// User's password for authentication
        /// </summary>
        public string? Password { get; set; }
    }

    /// <summary>
    /// Model class representing the user registration data
    /// </summary>
    public class RegisterModel
    {
        /// <summary>
        /// Email address for the new user account
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Password for the new user account
        /// </summary>
        public string? Password { get; set; }

        /// <summary>
        /// User's first name
        /// </summary>
        public string? FirstName { get; set; }

        /// <summary>
        /// User's last name
        /// </summary>
        public string? LastName { get; set; }
    }
}
