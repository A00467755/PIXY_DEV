using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PIXY.Models
{
    public class PaymentVM
    {
        public int ID { get; set; }

        [NotMapped]
        [Required]
        //[RegularExpression("\\b[0 - 9]{4}\\s[0 - 9]{4}\\s[0 - 9]{ 4}\\s[0 - 9]{ 4}\\b", ErrorMessage = "Please input vaild Credit Card Number")]
        public int CardNo { get; set; }

        [NotMapped]
        [Required]
        public string NameOnCard { get; set; }

        [NotMapped]
        [Required]
        [RegularExpression("^[0-9]{2}$", ErrorMessage = "Please input vaild Month")]
        public string ExpiryDateMonth { get; set; }

        [NotMapped]
        [Required]
        [RegularExpression("^[0-9]{2}$", ErrorMessage = "Please input vaild Year")]
        public string ExpiryDateYear { get; set; }

        [NotMapped]
        [Required]
        [RegularExpression("^[0-9]{3}$", ErrorMessage = "Please input vaild Security Code")]
        public string SecurityCode { get; set; }


        public string Address { get; set; }

        public string City { get; set; }

        public string Province { get; set; }

        public string Country { get; set; }

        public string PostalCode { get; set; }
    }
}
