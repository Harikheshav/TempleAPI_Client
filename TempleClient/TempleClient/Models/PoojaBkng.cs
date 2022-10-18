using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TempleClient.Models
{
    public partial class PoojaBkng
    {
        public int Bkid { get; set; }
        [DataType(DataType.Currency)]
        public int? Cost { get; set; }
        public int? Pooid { get; set; }
        public string? Det { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? Sdt { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? Edt { get; set; }
        public int? Userid { get; set; }

        public virtual Pooja? Poo { get; set; }
        public virtual User? User { get; set; }
        public PoojaBkng() 
        { 
        }
    }
}
