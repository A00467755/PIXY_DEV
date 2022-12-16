using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PIXY.Models
{
    public class Image
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string CategoryDesc { get; set; }
        [Required]
        public string ImageType { get; set; }
        [Required]
        public string ImageTags { get; set; }
        [Required]
        public string FilePathWatermark { get; set; }
        [Required]
        public string FilePath { get; set; }
        [Required]
        public Double Price { get; set; }
        [Required]
        public Boolean HaveHardcopy { get; set; }


    }
}
