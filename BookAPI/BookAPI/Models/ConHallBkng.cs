using System;
using System.Collections.Generic;

namespace BookAPI.Models
{
    public partial class ConHallBkng
    {
        public int Bkid { get; set; }
        public int? Cost { get; set; }
        public string? Det { get; set; }
        public DateTime? Sdt { get; set; }
        public DateTime? Edt { get; set; }
        public int? Userid { get; set; }

        public virtual User? User { get; set; }
    }
}
