using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PIXY.Models
{
    public class UserVM
    {
        const string RegSpecCharPattern = @"^(?!.*[;:!@#$%^*+?\\\/<>0-9]).*$";

        [Required]
        public int ID { get; set; }
        [Required]
        [RegularExpression(RegSpecCharPattern, ErrorMessage = "Contains invalid character: ?!.*[;:!@#$%^*+?\\/<>0123456789")]
        public string LastName { get; set; }
        [Required]
        [RegularExpression(RegSpecCharPattern, ErrorMessage = "Contains invalid character: ?!.*[;:!@#$%^*+?\\/<>0123456789")]
        public string FirstName { get; set; }
        [Required]
        [RegularExpression("^[^@\\s]+@[^@\\s]+\\.(com|net|org|gov)$", ErrorMessage = "Please input vaild Email ID")]
        public string Email { get; set; }
        [Required]
        [RegularExpression("^\\(?([0-9]{3})\\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Please input a valid 10 digit number")]
        public string PhoneNumber { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [RegularExpression(RegSpecCharPattern, ErrorMessage = "Contains invalid character: ?!.*[;:!@#$%^*+?\\/<>0123456789")]
        public string City { get; set; }

        [Required]
        [RegularExpression(RegSpecCharPattern, ErrorMessage = "Contains invalid character: ?!.*[;:!@#$%^*+?\\/<>0123456789")]
        public string Province { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        [Remote("CheckPostalCode", "Users", HttpMethod = "POST", AdditionalFields = "Country", ErrorMessage = "Please enter a valid Postal Code/ Zip Code")]
        public string PostalCode { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }


        [NotMapped] // Does not effect with database
        [Required(ErrorMessage = "Confirm Password required")]
        [DataType(DataType.Password)]
        [CompareAttribute("Password", ErrorMessage = "Password doesn't match.")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
