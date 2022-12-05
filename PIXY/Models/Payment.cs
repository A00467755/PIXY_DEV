namespace PIXY.Models
{
    public class Payment
    {
        public int ID { get; set; }

        public int CardNo { get; set; }
        public string NameOnCard { get; set; }
        public int ExpiryDateMonth { get; set; }

        public int ExpiryDateYear{ get; set; }

        public int SecurityCode { get; set; }

    }
}
