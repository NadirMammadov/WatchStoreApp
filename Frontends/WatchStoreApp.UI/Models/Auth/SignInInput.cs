using System.ComponentModel.DataAnnotations;

namespace WatchStoreApp.UI.Models.Auth
{
    public class SigninInput
    {
        [Required]
        [Display(Name = "Email Adresiniz")]
        public string Email { get; set; } = null!;
        [Required]
        [Display(Name = "Sifreniz")]
        public string Password { get; set; } = null!;
        [Display(Name = "Beni hatirla")]
        public bool IsRemember { get; set; }
    }
}
