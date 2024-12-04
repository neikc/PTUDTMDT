using System;
using System.Collections.Generic;

namespace PTUDTMDT.Models;

public partial class TuKhoa
{
    public string MaTuKhoa { get; set; } = null!;

    public string? TenTuKhoa { get; set; }

    public virtual ICollection<SanPham> MaSanPhams { get; set; } = new List<SanPham>();
}
