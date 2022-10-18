using System;
using System.Collections.Generic;

namespace PoojaAPI.Models
{
    public partial class Pooja
    {
        public int Pid { get; set; }
        public string? Name { get; set; }
        public int? Cost { get; set; }
        public string? Details { get; set; }
    }
}
