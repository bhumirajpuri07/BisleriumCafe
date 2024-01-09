using BisleriumCafe.Models;

namespace BisleriumCafe.Services
{
    public class UserService
    {
        private static User currentUser;
        private static List<User> users;

        // Static constructor to initialize predefined users
        static UserService()
        {
            // Create predefined users
            User adminUser = new User { Username = "admin", Password = "admin", Role = "admin" };
            User staffUser = new User { Username = "staff", Password = "staff", Role = "staff" };

            // Initialize the list and add predefined users
            users = new List<User> { adminUser, staffUser };
        }

        /// <summary>
        /// Get a list of all users.
        /// </summary>
        /// <returns>List of users.</returns>
        public static List<User> GetAllUsers()
        {
            return users;
        }

        /// <summary>
        /// Login functionality to authenticate a user.
        /// </summary>
        /// <param name="loginUser">User to be authenticated.</param>
        /// <returns>True if login is successful, false otherwise.</returns>
        public static bool Login(User loginUser)
        {
            foreach (User user in users)
            {
                // Check if username and password match
                if (user.Username.ToLower() == loginUser.Username.ToLower() && user.Password == loginUser.Password)
                {
                    // Set the current user and return true
                    currentUser = user;
                    return true;
                }
            }
            // Return false if no match is found
            return false;
        }

        /// <summary>
        /// Logout the currently logged-in user.
        /// </summary>
        /// <returns>True if logout is successful, false otherwise.</returns>
        public static bool Logout()
        {
            // Set the current user to null indicating logout
            currentUser = null;
            return true;
        }

        /// <summary>
        /// Check if any user is currently logged in.
        /// </summary>
        /// <returns>True if a user is logged in, false otherwise.</returns>
        public static bool IsLoggedIn()
        {
            return currentUser != null;
        }

        /// <summary>
        /// Check if the currently logged-in user is an admin.
        /// </summary>
        /// <returns>True if the user is an admin, false otherwise.</returns>
        public static bool IsAdmin()
        {
            return currentUser?.Role.ToLower() == "admin";
        }

        /// <summary>
        /// Get the currently logged-in user.
        /// </summary>
        /// <returns>Currently logged-in user.</returns>
        public User GetCurrentUser()
        {
            return currentUser;
        }
    }
}
