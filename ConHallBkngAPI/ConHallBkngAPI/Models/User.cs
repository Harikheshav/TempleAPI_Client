using System;
using System.Collections.Generic;

namespace ConHallBkngAPI.Models
{
    public partial class User
    {
        public User()
        {
            ConHallBkngs = new HashSet<ConHallBkng>();
        }

        public int Uid { get; set; }
        public string? Uname { get; set; }
        public string? Pword { get; set; }
        public string? Emailid { get; set; }

        public virtual ICollection<ConHallBkng> ConHallBkngs { get; set; }
    }
}
