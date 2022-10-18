using System;
using System.Collections.Generic;

namespace PoojaBkngAPI.Models
{
    public partial class User
    {
        public User()
        {
            PoojaBkngs = new HashSet<PoojaBkng>();
        }

        public int Uid { get; set; }
        public string? Uname { get; set; }
        public string? Pword { get; set; }
        public string? Emailid { get; set; }

        public virtual ICollection<PoojaBkng> PoojaBkngs { get; set; }
    }
}
