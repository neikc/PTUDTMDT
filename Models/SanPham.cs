using System;
using System.Collections.Generic;

namespace PTUDTMDT.Models;

public partial class SanPham
{
    public string MaSanPham { get; set; } = null!;

    public string? TenSanPham { get; set; }

    public string? MaLoai { get; set; }

    public double? GiaSanPham { get; set; }

    public double? GiamGia { get; set; }

    public double? GiaSauGiam { get; set; }

    public string? ShortDesc { get; set; }

    public bool? BestSellers { get; set; }

    public bool? TinhTrang { get; set; }

    public int? SltonKho { get; set; }

    public bool? HomeFlag { get; set; }

    public string? MoTa { get; set; }

    public string? Hinh1 { get; set; }

    public string? Hinh2 { get; set; }

    public DateTime? NgaySanXuat { get; set; }

    public DateTime? HanSuDung { get; set; }

    public string? Alias { get; set; }

    public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();

    public virtual ICollection<DanhGiaSanPham> DanhGiaSanPhams { get; set; } = new List<DanhGiaSanPham>();

    public virtual LoaiSanPham? MaLoaiNavigation { get; set; }

    public virtual ICollection<TuKhoa> MaTuKhoas { get; set; } = new List<TuKhoa>();
}
