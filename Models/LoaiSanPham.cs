using System;
using System.Collections.Generic;

namespace PTUDTMDT.Models;

public partial class LoaiSanPham
{
    public string MaLoai { get; set; } = null!;

    public string? TenLoai { get; set; }

    public string? MoTa { get; set; }

    public string? Hinh { get; set; }

    public string? Alias { get; set; }

    public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();
}
