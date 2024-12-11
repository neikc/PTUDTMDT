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

    public int AverageRating
    {
        get
        {
            if (DanhGiaSanPhams == null || DanhGiaSanPhams.Count == 0)
            {
                return 0; // Nếu không có đánh giá, trả về 0
            }

            // Tính trung bình sao và làm tròn về kiểu int
            var totalSao = DanhGiaSanPhams.Where(dg => dg.SaoDanhGia.HasValue).Sum(dg => dg.SaoDanhGia.Value);
            return (int)Math.Round(totalSao / (double)DanhGiaSanPhams.Count(dg => dg.SaoDanhGia.HasValue));
        }
    }

    public string LayDiaChiHinh(int soHinh)
    {
        // Chọn hình dựa trên số hình
        string? tenSanPham = soHinh == 1 ? Hinh1 : Hinh2;

        if (string.IsNullOrEmpty(tenSanPham) || string.IsNullOrEmpty(MaLoai))
        {
            return "~/img/Product/default.png"; // Trả về hình mặc định nếu thiếu thông tin
        }

        // Trả về đường dẫn hình ảnh mà không chỉnh sửa tên file
        return $"~/img/Product/{MaLoai}/{tenSanPham}";
    }

    public string FormatPrice(double? price)
    {
        return string.Format("{0:N0}đ", price).Replace(",", ".");
    }

}
