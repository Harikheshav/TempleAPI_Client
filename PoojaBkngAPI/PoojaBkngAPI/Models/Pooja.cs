using System;
using System.Collections.Generic;

namespace PoojaBkngAPI.Models
{
    public partial class Pooja
    {
        public Pooja()
        {
            PoojaBkngs = new HashSet<PoojaBkng>();
        }

        public int Pid { get; set; }
        public string? Name { get; set; }
        public int? Cost { get; set; }
        public string? Details { get; set; }

        public virtual ICollection<PoojaBkng> PoojaBkngs { get; set; }
    }
}
