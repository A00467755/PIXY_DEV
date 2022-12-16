using System.ComponentModel;

namespace PIXY.Models
{
    public class PurchasedItemVM
    {
        public int ID { get; set; }
        public int UserId { get; set; }
        public int ImageId { get; set; }
        public Boolean IsHardcopy { get; set; }
        [DisplayName("No. Of Hardcopy")]
        public int NoOfHardcopy { get; set; }

        public string FilePath { get; set; }

        public List<int> SelectedImage { get; set; }
    }
}
