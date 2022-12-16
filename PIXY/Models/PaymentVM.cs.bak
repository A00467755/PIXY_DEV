using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PIXY.Models
{
    public class PaymentVM
    {
        const string RegSpecCharPattern = @"^(?!.*[;:!@#$%^*+?\/<>0-9]).*$";

        public int ID { get; set; }

        [NotMapped]
        [Required]
        public string CardType { get; set; }

        [NotMapped]
        [Required]
        [Remote("ValidateCard","Carts", AdditionalFields = "CardType", HttpMethod = "Post", ErrorMessage = "Invalid Card Number")]
        public string CardNo { get; set; }

        [NotMapped]
        [Required]
        [RegularExpression(RegSpecCharPattern, ErrorMessage = "Invalid Character: ?!.*[;:!@#$%^*+?\\/<>0123456789")]


        public string NameOnCard { get; set; }

        [NotMapped]
        [Required]
        [RegularExpression("[0][1-9]|[1][0-2]", ErrorMessage = "Please input vaild Month")]
        public string ExpiryDateMonth { get; set; }

        [NotMapped]
        [Required]
        [RegularExpression("[2][0][2][2-9]|[2][0][3][0-7]", ErrorMessage = "Please input vaild Year")]
        public string ExpiryDateYear { get; set; }

        [NotMapped]
        [Required]
        [RegularExpression("^[0-9]{3}$", ErrorMessage = "Please input vaild Security Code")]
        public string SecurityCode { get; set; }
        public string Address { get; set; }

    }
}
