using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PIXY.Models
{
    public class User
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression("^[^@\\s]+@[^@\\s]+\\.(com|net|org|gov)$", ErrorMessage = "Please input vaild Email ID")]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Address { get; set; }

        [Required]
        [Remote("IsAlreadySigned", "Users", HttpMethod = "POST", ErrorMessage = "User Name already exists in database.")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
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
