using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TempleClient.Models
{
    public partial class Admin
    {
        public int Uid { get; set; }
        public string? Uname { get; set; }
        public string? Pword { get; set; }
        [EmailAddress]
        public string? Emailid { get; set; }
    }
}
