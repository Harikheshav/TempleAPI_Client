using System;
using System.Collections.Generic;

namespace UserAPI.Models
{
    public partial class User
    {
        public int Uid { get; set; }
        public string? Uname { get; set; }
        public string? Pword { get; set; }
        public string? Emailid { get; set; }
    }
}
