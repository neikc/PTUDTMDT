using System;
using System.Collections.Generic;

namespace PTUDTMDT.Models;

public partial class ChiTietTuKhoa
{
    public string MaTuKhoa { get; set; } = null!;

    public string MaSanPham { get; set; } = null!;

    public string? TenSanPham { get; set; }

    public string? TenTuKhoa { get; set; }

    public virtual SanPham MaSanPhamNavigation { get; set; } = null!;

    public virtual TuKhoa MaTuKhoaNavigation { get; set; } = null!;
}
