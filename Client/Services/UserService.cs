using Client.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Client.Services
{
    public delegate void UserLoggedInEventHandler(UserDetails userDetails);

    public class UserService
    {
        #region Fields and Constants
        private const string UserFileLocation = @"Resources/all_users.json";

        #endregion

        #region Making Singleton
        private UserService()
        {
            //Making it private, for singleton
        }

        private static object syncLock = new object();
        
        private static UserService _userService;

        public static UserService GetUserService()
        {
            if (_userService == null)
            {
                lock (syncLock)
                {
                    if (_userService == null)
                        _userService = new UserService();
                }
            }
            return _userService;
        }
        #endregion

        #region Public Members
        public event UserLoggedInEventHandler UserLoggedIn;
        public UserDetails CurrentUser { get; private set; }
        #endregion

        #region Public Methods (API's)

        public List<UserDetails> GetAllUsers()
        {
            if (!File.Exists(UserFileLocation))
                throw new FileNotFoundException("User file db not available");

            var jsonData = File.ReadAllText(UserFileLocation);
            var allUsers = JsonConvert.DeserializeObject<AllUser>(jsonData);

            return allUsers.AllUsers;
        }

        public bool LoginUser(UserDetails userDetails)
        {
            var currentUser = GetAllUsers().Where(user => user.UserType == userDetails.UserName && user.Password == userDetails.Password).FirstOrDefault();
            if (currentUser == null)
                return false;

            CurrentUser = currentUser;
            if (UserLoggedIn != null)
                UserLoggedIn(currentUser);
            return true;
        }
        #endregion
    }
}