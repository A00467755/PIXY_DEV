using System.ComponentModel.DataAnnotations;

namespace PIXY.Models
{
    public class PurchasedItem
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int ImageId { get; set; }
        [Required]
        public Boolean IsHardcopy { get; set; }
        [Required]
        public int NoOfHardcopy { get; set; }
    }
}
