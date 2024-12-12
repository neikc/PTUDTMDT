using System.ComponentModel.DataAnnotations;

namespace PTUDTMDT.ViewModels.AccountViewModel
{
    public class LoginViewModel
    {
        [Display(Name = "Tên tài khoản")]
        [Required(ErrorMessage = "*Tên tài khoản là bắt buộc")]
        public string? TenTaiKhoan { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "*Mật khẩu là bắt buộc")]
        public string? MatKhau { get; set; }
    }
}
