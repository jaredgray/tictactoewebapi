using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tictactoewebapi.Model
{
    public class User : ModelEntity
    {
        public User() { }
        public User(string name, string email) { this.name = name; this.email = email; }
        public string name { get; set; }
        public string email { get; set; }
    }

    public static class UserExtensions
    {
        public static User UpdateWith(this User user, User updateWith)
        {
            user.name = updateWith.name;
            return user;
        }
    }
}
