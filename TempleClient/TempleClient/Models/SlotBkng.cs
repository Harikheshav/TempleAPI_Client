using System.ComponentModel.DataAnnotations;

namespace TempleClient.Models
{
    public class SlotBkng
    {
        [Key]
        public int Bkid { get; set; }
        
        [Required,DataType(DataType.Currency)]
        public int Cost { get; set; }
        public string? Det { get; set; }
        [Required,DataType(DataType.DateTime)]
        [ValidateIfPast(ErrorMessage = "Cannot Assign Events on Past Date and Time")]
        public DateTime? Sdt { get; set; }
        [Required,DataType(DataType.DateTime)]
        [ValidateIfFuture("Sdt",ErrorMessage = "Event ends before it gets started")]
        public DateTime? Edt { get; set; }
        [Required]
        public string SlotName { get; set; }

        public SlotBkng()
        {

        }

    }
}
