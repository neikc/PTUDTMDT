using System;
using System.Collections.Generic;

namespace PTUDTMDT.Models;

public partial class ChiTietDonHang
{
    public string MaCtdh { get; set; } = null!;

    public string? MaDonHang { get; set; }

    public string? MaSanPham { get; set; }

    public int? SoLuong { get; set; }

    public double? GiaSanPham { get; set; }

    public double? TongTienSp { get; set; }

    public DateTime? NgayTaoDon { get; set; }

    public virtual DonHang? MaDonHangNavigation { get; set; }

    public virtual SanPham? MaSanPhamNavigation { get; set; }
}
