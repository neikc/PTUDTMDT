using System;
using System.Collections.Generic;

namespace PTUDTMDT.Models;

public partial class GiaoHang
{
    public string MaGiaoHang { get; set; } = null!;

    public string? TenShipper { get; set; }

    public string? CongTyGiaoHang { get; set; }

    public string? Sdt { get; set; }

    public virtual ICollection<DonHang> DonHangs { get; set; } = new List<DonHang>();
}
