using System;
using System.Collections.Generic;

namespace PTUDTMDT.Models;

public partial class KhachHang
{
    public string MaKhachHang { get; set; } = null!;

    public string? HoTen { get; set; }

    public bool? GioiTinh { get; set; }

    public string? Email { get; set; }

    public string? SoDienThoai { get; set; }

    public string? MaDiaChi { get; set; }

    public DateOnly? NgaySinh { get; set; }

    public string? Hinh { get; set; }

    public bool? HieuLuc { get; set; }

    public string? QuanHuyen { get; set; }

    public string? PhuongXa { get; set; }

    public virtual DiaChi? MaDiaChiNavigation { get; set; }

    public virtual ICollection<TaiKhoan> TaiKhoans { get; set; } = new List<TaiKhoan>();

    public string LayDiaChiHinh()
    {
        // Chọn hình dựa trên số hình
        if (string.IsNullOrEmpty(Hinh))
        {
            return "~/img/avatar.jpg"; // Trả về hình mặc định nếu thiếu thông tin
        }

        // Trả về đường dẫn hình ảnh mà không chỉnh sửa tên file
        return $"~/img/Customer/{Hinh}";
    }
}
