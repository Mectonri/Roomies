using System.ComponentModel.DataAnnotations;
using System;

namespace ITI.Roomies.WebApp.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display( Name = "Email" )]
        public string Email { get; set; }

        [Required]
        [StringLength( 100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6 )]
        [DataType( DataType.Password )]
        [Display( Name = "Password" )]
        public string Password { get; set; }

        [DataType( DataType.Password )]
        [Display( Name = "Confirm password" )]
        [Compare( "Password", ErrorMessage = "The password and confirmation password do not match." )]
        public string ConfirmPassword { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime BirthDate { get; set; }

        [Required]
        [StringLength (10, ErrorMessage ="You must have 10 digits.",MinimumLength =10)]
        public string PhoneNumber { get; set; }
    }
}
