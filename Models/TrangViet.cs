using System;
using System.Collections.Generic;

namespace PTUDTMDT.Models;

public partial class TrangViet
{
    public string MaTrangViet { get; set; } = null!;

    public string? TenTrangViet { get; set; }

    public string? NoiDung { get; set; }

    public string? Thumb { get; set; }

    public DateTime? ThoiGianDang { get; set; }

    public string? TieuDe { get; set; }

    public string? TuKhoa { get; set; }

    public string? Alias { get; set; }

    public string? Url { get; set; }
}
