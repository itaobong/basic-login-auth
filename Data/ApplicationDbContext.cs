using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LoginAuth.Models;

namespace LoginAuth.Data
{
    /// <summary>
    /// Database context class for the application, extending IdentityDbContext to include Identity framework tables
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// Initializes a new instance of the ApplicationDbContext
        /// </summary>
        /// <param name="options">The options to be used by DbContext</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}