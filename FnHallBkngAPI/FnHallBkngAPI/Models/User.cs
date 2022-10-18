using System;
using System.Collections.Generic;

namespace FnHallBkngAPI.Models
{
    public partial class User
    {
        public User()
        {
            FnHallBkngs = new HashSet<FnHallBkng>();
        }

        public int Uid { get; set; }
        public string? Uname { get; set; }
        public string? Pword { get; set; }
        public string? Emailid { get; set; }

        public virtual ICollection<FnHallBkng> FnHallBkngs { get; set; }
    }
}
