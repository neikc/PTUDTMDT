using Microsoft.AspNetCore.Antiforgery;
using System.ComponentModel.DataAnnotations;

namespace PTUDTMDT.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Display(Name = "Tên tài khoản")]
        [Required(ErrorMessage = "*Tên tài khoản là bắt buộc")]
        public string? TenTaiKhoan { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "*Mật khẩu là bắt buộc")]
        public string? MatKhau { get; set; }

        [Display(Name = "Họ Tên")]
        [Required(ErrorMessage = "*Họ tên là bắt buộc")]
        public string? HoTen { get; set; }

        [Display(Name = "Giới tính")]
        [Required(ErrorMessage = "*Giới tính là bắt buộc")]
        public bool? GioiTinh { get; set; }

        [Display(Name = "Số điện thoại")]
        public string? SoDienThoai { get; set; }

        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string? Email { get; set; }

        [Display(Name = "Ngày sinh")]
        public DateOnly? NgaySinh { get; set; }

        [Display(Name = "Quận - Huyện")]
        public string? QuanHuyen { get; set; }

        [Display(Name = "Phường - Xã")]
        public string? PhuongXa { get; set; }

        [Display(Name = "Ảnh đại diện")]
        public string? Hinh { get; set; }
    }
}
