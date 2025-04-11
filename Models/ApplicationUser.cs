using Microsoft.AspNetCore.Identity;

namespace LoginAuth.Models
{
    /// <summary>
    /// Custom application user class that extends IdentityUser to add additional user properties
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// The user's first name
        /// </summary>
        public string? FirstName { get; set; }

        /// <summary>
        /// The user's last name
        /// </summary>
        public string? LastName { get; set; }
    }
}