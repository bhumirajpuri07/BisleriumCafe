using System.ComponentModel.DataAnnotations;

namespace BisleriumCafe.Models
{
    /// <summary>
    /// Represents a user with authentication information.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password of the user.
        /// </summary>
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the role of the user.
        /// </summary>
        public string Role { get; set; }
    }
}
