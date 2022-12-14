namespace PIXY.Models
{
    public class ImageVM
    {

        public int ID { get; set; }

        public int UserId { get; set; }
        public string CategoryDesc { get; set; }

        public string ImageType { get; set; }

        public string FilePathWatermark { get; set; }
        public string FilePath { get; set; }

        public Double Price { get; set; }

        public Boolean HaveHardcopy { get; set; }

        public Boolean IsPurchased { get; set; }

        public Boolean IsInCart{ get; set; }
        public string AuthorName { get; set; }
    }
}
