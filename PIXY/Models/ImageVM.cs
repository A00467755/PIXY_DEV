using System.ComponentModel;

namespace PIXY.Models
{
    public class ImageVM
    {

        public int ID { get; set; }

        [DisplayName("ID")]
        public int UserId { get; set; }
        [DisplayName("CATEGORY")]
        public string CategoryDesc { get; set; }

        [DisplayName("FORMAT")]
        public string ImageType { get; set; }
        [DisplayName("TAGS")]
        public string ImageTags { get; set; }

        [DisplayName("AUTHOR")]
        public string AuthorName { get; set; }

        [DisplayName("PRODUCTS")]
        public string FilePathWatermark { get; set; }
        public string FilePath { get; set; }

        [DisplayName("PRICE")]
        public Double Price { get; set; }

        [DisplayName("FILE")]
        public Boolean HaveHardcopy { get; set; }

        public Boolean IsPurchased { get; set; }

        public Boolean IsInCart{ get; set; }


    }
}
