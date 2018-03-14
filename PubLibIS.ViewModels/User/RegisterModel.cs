using System.ComponentModel.DataAnnotations;

namespace PubLibIS.ViewModels
{
    public class RegisterModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare(otherProperty: "Password", ErrorMessage = "Passwords are not equals!")]
        [Display(Name = "Confirm password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public string Address { get; set; }

        public string Name { get; set; }

        public bool Admin { get; set; }
    }
}
