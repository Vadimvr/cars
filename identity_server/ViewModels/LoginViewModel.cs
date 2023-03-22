using System.ComponentModel.DataAnnotations;

namespace identity_server.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string? UserName { get; set; }

        [Required]
        public string? Password { get; set; }

        [Required]
        public string? ReturnUrl { get; set; }
    }
}