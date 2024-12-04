using System;
using System.Collections.Generic;

namespace PTUDTMDT.Models;

public partial class DonHang
{
    public string MaDonHang { get; set; } = null!;

    public string? MaGiamGia { get; set; }

    public string? MaTaiKhoan { get; set; }

    public string? MaGiaoHang { get; set; }

    public DateTime? NgayLenDon { get; set; }

    public DateTime? NgayGiaoHang { get; set; }

    public string? TrangThai { get; set; }

    public bool? TtthanhToan { get; set; }

    public double? TongTien { get; set; }

    public string? MaPttt { get; set; }

    public DateTime? NgayThanhToan { get; set; }

    public string? DiaChi { get; set; }

    public string? GhiChu { get; set; }

    public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();

    public virtual GiamGium? MaGiamGiaNavigation { get; set; }

    public virtual GiaoHang? MaGiaoHangNavigation { get; set; }

    public virtual PhuongThucThanhToan? MaPtttNavigation { get; set; }

    public virtual TaiKhoan? MaTaiKhoanNavigation { get; set; }
}
