using System;
using System.Collections.Generic;
using System.Text;

namespace BMPClassLibrary.Model
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        // Full Name
        public string FullName
        {
            get 
            { 
                return FirstName + " " + LastName; 
            }
        }
    }
}
