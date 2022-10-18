using System;
using System.Collections.Generic;

namespace AnDhanBkngAPI.Models
{
    public partial class User
    {
        public User()
        {
            AnDhanBkngs = new HashSet<AnDhanBkng>();
        }

        public int Uid { get; set; }
        public string? Uname { get; set; }
        public string? Pword { get; set; }
        public string? Emailid { get; set; }

        public virtual ICollection<AnDhanBkng> AnDhanBkngs { get; set; }
    }
}
