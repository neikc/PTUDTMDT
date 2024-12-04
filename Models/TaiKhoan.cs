using System;
using System.Collections.Generic;

namespace PTUDTMDT.Models;

public partial class TaiKhoan
{
    public string MaTaiKhoan { get; set; } = null!;

    public string? TenTaiKhoan { get; set; }

    public string? MatKhau { get; set; }

    public string? SoDienThoai { get; set; }

    public string? Email { get; set; }

    public string? MaVaiTro { get; set; }

    public DateTime? NgayTaoTk { get; set; }

    public DateTime? LanCuoiTruyCap { get; set; }

    public bool? TinhTrangTk { get; set; }

    public string? MaKhachHang { get; set; }

    public virtual ICollection<DanhGiaSanPham> DanhGiaSanPhams { get; set; } = new List<DanhGiaSanPham>();

    public virtual ICollection<DonHang> DonHangs { get; set; } = new List<DonHang>();

    public virtual KhachHang? MaKhachHangNavigation { get; set; }

    public virtual VaiTro? MaVaiTroNavigation { get; set; }
}
