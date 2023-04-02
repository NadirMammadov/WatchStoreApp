using System.ComponentModel.DataAnnotations;

namespace WatchStoreApp.UI.Models.Auth
{
    public class SignUpInput
    {
        [Display(Name = "İstifadəçi adı")]
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        [Display(Name = "Şifrə")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        [Display(Name = "Təkrar şifrə")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Şifrələri düzgün daxil edin")]
        public string RePassword { get; set; } = null!;
    }
}
