using System;
using System.Collections.Generic;

namespace BookAPI.Models
{
    public partial class User
    {
        public User()
        {
            AnDhanBkngs = new HashSet<AnDhanBkng>();
            ConHallBkngs = new HashSet<ConHallBkng>();
            FnHallBkngs = new HashSet<FnHallBkng>();
            PoojaBkngs = new HashSet<PoojaBkng>();
        }

        public int Uid { get; set; }
        public string? Uname { get; set; }
        public string? Pword { get; set; }
        public string? Emailid { get; set; }

        public virtual ICollection<AnDhanBkng> AnDhanBkngs { get; set; }
        public virtual ICollection<ConHallBkng> ConHallBkngs { get; set; }
        public virtual ICollection<FnHallBkng> FnHallBkngs { get; set; }
        public virtual ICollection<PoojaBkng> PoojaBkngs { get; set; }
    }
}
