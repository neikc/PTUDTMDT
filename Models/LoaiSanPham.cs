using System;
using System.Collections.Generic;

namespace PTUDTMDT.Models;

public partial class LoaiSanPham
{
    public string MaLoai { get; set; } = null!;

    public string? TenLoai { get; set; }

    public string? MoTa { get; set; }

    public byte[]? Hinh { get; set; }

    public string? Thumb { get; set; }

    public string? Alias { get; set; }

    public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();
}
