using System;
using System.Collections.Generic;
using System.Text;

namespace BMPClassLibrary.Model
{
    public class Admin
    {
        public int AdminId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string AdminPassword { get; set; } = string.Empty;

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
