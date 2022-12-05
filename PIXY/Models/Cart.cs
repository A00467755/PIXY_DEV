namespace PIXY.Models
{
    public class Cart
    {
        public int ID { get; set; }
        public int UserId { get; set; }
        public int ImageId { get; set; }
        public Boolean IsHardcopy { get; set; }
        public int NoOfHardcopy { get; set; }
    }
}
