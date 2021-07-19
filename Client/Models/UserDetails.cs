using Newtonsoft.Json;
using System.Collections.Generic;

namespace Client.Models
{

    public class AllUser
    {
        [JsonProperty("users")]
        public List<UserDetails> AllUsers { get; set; }
    }


    public class UserDetails
    {
        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("userType")]
        public string UserType { get; set; }
    }

    public class UserType
    {
        public const string Privileged = "privileged";
        public const string Normal = "normal";
    }
}
