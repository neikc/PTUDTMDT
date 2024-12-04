using System;
using System.Collections.Generic;

namespace PTUDTMDT.Models;

public partial class GiamGium
{
    public string MaGiamGia { get; set; } = null!;

    public string? TenMa { get; set; }

    public double? GiaTri { get; set; }

    public DateTime? NgayBatDau { get; set; }

    public DateTime? NgayKetThuc { get; set; }

    public int? SoLuong { get; set; }

    public bool? TrangThai { get; set; }

    public virtual ICollection<DonHang> DonHangs { get; set; } = new List<DonHang>();
}
