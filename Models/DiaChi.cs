using System;
using System.Collections.Generic;

namespace PTUDTMDT.Models;

public partial class DiaChi
{
    public string MaDiaChi { get; set; } = null!;

    public string? TenDiaChi { get; set; }

    public string? ViTriNha { get; set; }

    public string? ChuanHoaUrl { get; set; }

    public string? TheLoai { get; set; }

    public virtual ICollection<KhachHang> KhachHangs { get; set; } = new List<KhachHang>();
}
