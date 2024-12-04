using System;
using System.Collections.Generic;

namespace PTUDTMDT.Models;

public partial class TrangWeb
{
    public string MaTrangWeb { get; set; } = null!;

    public string? TenTrangWeb { get; set; }

    public string? Url { get; set; }
}
