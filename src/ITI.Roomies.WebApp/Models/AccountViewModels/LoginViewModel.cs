using System.ComponentModel.DataAnnotations;

namespace ITI.Roomies.WebApp.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType( DataType.Password )]
        public string Password { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string BirthDate { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
    }
}
