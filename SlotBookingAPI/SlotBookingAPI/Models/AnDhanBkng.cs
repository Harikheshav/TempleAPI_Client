using System;
using System.Collections.Generic;

namespace SlotBookingAPI.Models
{
    public partial class AnDhanBkng
    {
        public int Bkid { get; set; }
        public int? Cost { get; set; }
        public string? Det { get; set; }
        public DateTime? Sdt { get; set; }
        public DateTime? Edt { get; set; }
        public int? Userid { get; set; }

        public virtual User? User { get; set; }
        public AnDhanBkng(SlotBkng slot)
        {
            Bkid = slot.Bkid;
            Cost = slot.Cost;
            Det = slot.Det;
            Sdt = slot.Sdt;
            Edt = slot.Edt;
        }
        public AnDhanBkng() { }
    }
}
