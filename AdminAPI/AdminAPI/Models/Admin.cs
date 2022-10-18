using System;
using System.Collections.Generic;

namespace AdminAPI.Models
{
    public partial class Admin
    {
        public int Uid { get; set; }
        public string? Uname { get; set; }
        public string? Pword { get; set; }
        public string? Emailid { get; set; }
    }
}
