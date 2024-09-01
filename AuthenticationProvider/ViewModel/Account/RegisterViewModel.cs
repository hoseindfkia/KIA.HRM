using System.ComponentModel.DataAnnotations;

namespace AuthenticationProvider.ViewModel.Account
{
    public class RegisterViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare(nameof(Password))]
        public string PasswordConfirmed { get; set; }
    }
}
