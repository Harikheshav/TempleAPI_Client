using System.ComponentModel.DataAnnotations;

namespace PoojaBkngAPI.Models
{
    public partial class PoojaBkng
    {
        public int Bkid { get; set; }
        public int? Cost { get; set; }
        public int? Pooid { get; set; }
        public string? Det { get; set; }
        public DateTime? Sdt { get; set; }
        public DateTime? Edt { get; set; }
        public int? Userid { get; set; }

        public virtual Pooja? Poo { get; set; }
        public virtual User? User { get; set; }
    }
}
