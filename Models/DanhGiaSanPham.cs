using System;
using System.Collections.Generic;

namespace PTUDTMDT.Models;

public partial class DanhGiaSanPham
{
    public string MaDanhGia { get; set; } = null!;

    public string? MaTaiKhoan { get; set; }

    public string? MaSanPham { get; set; }

    public int? SaoDanhGia { get; set; }

    public string? LoiDanhGia { get; set; }

    public virtual SanPham? MaSanPhamNavigation { get; set; }

    public virtual TaiKhoan? MaTaiKhoanNavigation { get; set; }
}
