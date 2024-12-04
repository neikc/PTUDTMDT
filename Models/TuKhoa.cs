using System;
using System.Collections.Generic;

namespace PTUDTMDT.Models;

public partial class TuKhoa
{
    public string MaTuKhoa { get; set; } = null!;

    public string? TenTuKhoa { get; set; }

    public virtual ICollection<ChiTietTuKhoa> ChiTietTuKhoas { get; set; } = new List<ChiTietTuKhoa>();
}
