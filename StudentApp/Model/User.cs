using System;
using System.Data;
using LanguageExt;

namespace StudentApp_FunctionalProgramming_.Model
{
    public class User : Record<User>
    {
        public readonly string Username;
        public readonly string Password;

        private User(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public static User Create(string username, string password)
        {
            return new User(username, password);
        }

        public static User Create(IDataRecord record)
        {
            return Create(
              username: Convert.ToString(record["username"]),
              password: Convert.ToString(record["password"])
            );
        }
    }
}
