using System.ComponentModel.DataAnnotations;
using SlotBookingAPI.Models;
namespace SlotBookingAPI.Models
{
    public class SlotBkng
    {
        public int Bkid { get; set; }
        public int Cost { get; set; }
        public string? Det { get; set; }
        public DateTime? Sdt { get; set; }
        public DateTime? Edt { get; set; }
        public string SlotName { get; set; }

        public SlotBkng()
        {

        }

    }
}
