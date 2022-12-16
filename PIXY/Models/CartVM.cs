using System.ComponentModel;

namespace PIXY.Models
{
    public class CartVM
    {
        public int ID { get; set; }
        public int UserId { get; set; }
        public int ImageId { get; set; }
        public Boolean IsHardcopy { get; set; }
        [DisplayName("No. Of Hardcopy")]
        public int NoOfHardcopy { get; set; }
        public string FilePathWatermark { get; set; }
        public Double Price { get; set; }
    }
}
