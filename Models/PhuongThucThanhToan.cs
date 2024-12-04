using System;
using System.Collections.Generic;

namespace PTUDTMDT.Models;

public partial class PhuongThucThanhToan
{
    public string MaPttt { get; set; } = null!;

    public string? TenPttt { get; set; }

    public string? NoiDung { get; set; }

    public virtual ICollection<DonHang> DonHangs { get; set; } = new List<DonHang>();
}
