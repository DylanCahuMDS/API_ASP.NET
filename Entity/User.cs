using System;
using System.ComponentModel.DataAnnotations;

namespace APIMDS
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }


        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }


    }
}
