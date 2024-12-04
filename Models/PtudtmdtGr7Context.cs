using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PTUDTMDT.Models;

public partial class PtudtmdtGr7Context : DbContext
{
    public PtudtmdtGr7Context()
    {
    }

    public PtudtmdtGr7Context(DbContextOptions<PtudtmdtGr7Context> options)
        : base(options)
    {
    }

    public virtual DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }

    public virtual DbSet<ChiTietTuKhoa> ChiTietTuKhoas { get; set; }

    public virtual DbSet<DanhGiaSanPham> DanhGiaSanPhams { get; set; }

    public virtual DbSet<DiaChi> DiaChis { get; set; }

    public virtual DbSet<DonHang> DonHangs { get; set; }

    public virtual DbSet<GiamGium> GiamGia { get; set; }

    public virtual DbSet<GiaoHang> GiaoHangs { get; set; }

    public virtual DbSet<KhachHang> KhachHangs { get; set; }

    public virtual DbSet<LoaiSanPham> LoaiSanPhams { get; set; }

    public virtual DbSet<PhuongThucThanhToan> PhuongThucThanhToans { get; set; }

    public virtual DbSet<SanPham> SanPhams { get; set; }

    public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }

    public virtual DbSet<TrangViet> TrangViets { get; set; }

    public virtual DbSet<TrangWeb> TrangWebs { get; set; }

    public virtual DbSet<TuKhoa> TuKhoas { get; set; }

    public virtual DbSet<VaiTro> VaiTros { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=CKEN56;Initial Catalog=PTUDTMDT_Gr7;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChiTietDonHang>(entity =>
        {
            entity.HasKey(e => e.MaCtdh).HasName("PK__ChiTietD__1E4E40F0F7EF77FA");

            entity.ToTable("ChiTietDonHang");

            entity.Property(e => e.MaCtdh)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MaCTDH");
            entity.Property(e => e.MaDonHang)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MaSanPham)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NgayTaoDon).HasColumnType("datetime");
            entity.Property(e => e.TongTienSp).HasColumnName("TongTienSP");

            entity.HasOne(d => d.MaDonHangNavigation).WithMany(p => p.ChiTietDonHangs)
                .HasForeignKey(d => d.MaDonHang)
                .HasConstraintName("chitietdonhang_madonhang");

            entity.HasOne(d => d.MaSanPhamNavigation).WithMany(p => p.ChiTietDonHangs)
                .HasForeignKey(d => d.MaSanPham)
                .HasConstraintName("chitietdonhang_masanpham");
        });

        modelBuilder.Entity<ChiTietTuKhoa>(entity =>
        {
            entity.HasKey(e => new { e.MaTuKhoa, e.MaSanPham }).HasName("PK__ChiTietT__EE76E12A9E348D9D");

            entity.ToTable("ChiTietTuKhoa");

            entity.Property(e => e.MaTuKhoa)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MaSanPham)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TenSanPham)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TenTuKhoa)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.MaSanPhamNavigation).WithMany(p => p.ChiTietTuKhoas)
                .HasForeignKey(d => d.MaSanPham)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ChiTietTuKhoa_masanpham");

            entity.HasOne(d => d.MaTuKhoaNavigation).WithMany(p => p.ChiTietTuKhoas)
                .HasForeignKey(d => d.MaTuKhoa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ChiTietTuKhoa_matukhoa");
        });

        modelBuilder.Entity<DanhGiaSanPham>(entity =>
        {
            entity.HasKey(e => e.MaDanhGia).HasName("PK__DanhGiaS__AA9515BF50357016");

            entity.ToTable("DanhGiaSanPham");

            entity.Property(e => e.MaDanhGia)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LoiDanhGia).HasColumnType("text");
            entity.Property(e => e.MaSanPham)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MaTaiKhoan)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.MaSanPhamNavigation).WithMany(p => p.DanhGiaSanPhams)
                .HasForeignKey(d => d.MaSanPham)
                .HasConstraintName("danhgiasanpham_masanpham");

            entity.HasOne(d => d.MaTaiKhoanNavigation).WithMany(p => p.DanhGiaSanPhams)
                .HasForeignKey(d => d.MaTaiKhoan)
                .HasConstraintName("danhgiasanpham_mataikhoan");
        });

        modelBuilder.Entity<DiaChi>(entity =>
        {
            entity.HasKey(e => e.MaDiaChi).HasName("PK__DiaChi__EB61213E0D72E15F");

            entity.ToTable("DiaChi");

            entity.Property(e => e.MaDiaChi)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ChuanHoaUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ChuanHoaURL");
            entity.Property(e => e.TenDiaChi)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TheLoai)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DonHang>(entity =>
        {
            entity.HasKey(e => e.MaDonHang).HasName("PK__DonHang__129584ADF6E13B63");

            entity.ToTable("DonHang");

            entity.Property(e => e.MaDonHang)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DiaChi)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.GhiChu).HasColumnType("text");
            entity.Property(e => e.MaGiamGia)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MaGiaoHang)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MaPttt)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MaPTTT");
            entity.Property(e => e.MaTaiKhoan)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NgayGiaoHang).HasColumnType("datetime");
            entity.Property(e => e.NgayLenDon).HasColumnType("datetime");
            entity.Property(e => e.NgayThanhToan).HasColumnType("datetime");
            entity.Property(e => e.TrangThai)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TtthanhToan).HasColumnName("TTThanhToan");

            entity.HasOne(d => d.MaGiamGiaNavigation).WithMany(p => p.DonHangs)
                .HasForeignKey(d => d.MaGiamGia)
                .HasConstraintName("donhang_magiamgia");

            entity.HasOne(d => d.MaGiaoHangNavigation).WithMany(p => p.DonHangs)
                .HasForeignKey(d => d.MaGiaoHang)
                .HasConstraintName("donhang_magiaohang");

            entity.HasOne(d => d.MaPtttNavigation).WithMany(p => p.DonHangs)
                .HasForeignKey(d => d.MaPttt)
                .HasConstraintName("donhang_mapttt");

            entity.HasOne(d => d.MaTaiKhoanNavigation).WithMany(p => p.DonHangs)
                .HasForeignKey(d => d.MaTaiKhoan)
                .HasConstraintName("donhang_mataikhoan");
        });

        modelBuilder.Entity<GiamGium>(entity =>
        {
            entity.HasKey(e => e.MaGiamGia).HasName("PK__GiamGia__EF9458E44B41CF3C");

            entity.Property(e => e.MaGiamGia)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NgayBatDau).HasColumnType("datetime");
            entity.Property(e => e.NgayKetThuc).HasColumnType("datetime");
            entity.Property(e => e.TenMa)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<GiaoHang>(entity =>
        {
            entity.HasKey(e => e.MaGiaoHang).HasName("PK__GiaoHang__81CCF4FD0AA28673");

            entity.ToTable("GiaoHang");

            entity.Property(e => e.MaGiaoHang)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CongTyGiaoHang)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Sdt)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("SDT");
            entity.Property(e => e.TenShipper)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<KhachHang>(entity =>
        {
            entity.HasKey(e => e.MaKhachHang).HasName("PK__KhachHan__88D2F0E582107327");

            entity.ToTable("KhachHang");

            entity.Property(e => e.MaKhachHang)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.HoTen)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.MaDiaChi)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PhuongXa)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.QuanHuyen)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SoDienThoai)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.MaDiaChiNavigation).WithMany(p => p.KhachHangs)
                .HasForeignKey(d => d.MaDiaChi)
                .HasConstraintName("khachhang_madiachi");
        });

        modelBuilder.Entity<LoaiSanPham>(entity =>
        {
            entity.HasKey(e => e.MaLoai).HasName("PK__LoaiSanP__730A575903B51FAD");

            entity.ToTable("LoaiSanPham");

            entity.Property(e => e.MaLoai)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Alias)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.MoTa).HasColumnType("text");
            entity.Property(e => e.TenLoai)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Thumb)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PhuongThucThanhToan>(entity =>
        {
            entity.HasKey(e => e.MaPttt).HasName("PK__PhuongTh__B30A28028C8F1457");

            entity.ToTable("PhuongThucThanhToan");

            entity.Property(e => e.MaPttt)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MaPTTT");
            entity.Property(e => e.NoiDung).HasColumnType("text");
            entity.Property(e => e.TenPttt)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TenPTTT");
        });

        modelBuilder.Entity<SanPham>(entity =>
        {
            entity.HasKey(e => e.MaSanPham).HasName("PK__SanPham__FAC7442DA1746E32");

            entity.ToTable("SanPham");

            entity.Property(e => e.MaSanPham)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Alias)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.HanSuDung).HasColumnType("datetime");
            entity.Property(e => e.MaLoai)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MoTa).HasColumnType("text");
            entity.Property(e => e.NgaySanXuat).HasColumnType("datetime");
            entity.Property(e => e.ShortDesc).HasColumnType("text");
            entity.Property(e => e.SltonKho).HasColumnName("SLTonKho");
            entity.Property(e => e.TenSanPham)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Thumb)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.MaLoaiNavigation).WithMany(p => p.SanPhams)
                .HasForeignKey(d => d.MaLoai)
                .HasConstraintName("sanpham_maloai");
        });

        modelBuilder.Entity<TaiKhoan>(entity =>
        {
            entity.HasKey(e => e.MaTaiKhoan).HasName("PK__TaiKhoan__AD7C65291243D3C2");

            entity.ToTable("TaiKhoan");

            entity.Property(e => e.MaTaiKhoan)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LanCuoiTruyCap).HasColumnType("datetime");
            entity.Property(e => e.MaKhachHang)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MaVaiTro)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MatKhau)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NgayTaoTk)
                .HasColumnType("datetime")
                .HasColumnName("NgayTaoTK");
            entity.Property(e => e.SoDienThoai)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TenTaiKhoan)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TinhTrangTk).HasColumnName("TinhTrangTK");

            entity.HasOne(d => d.MaKhachHangNavigation).WithMany(p => p.TaiKhoans)
                .HasForeignKey(d => d.MaKhachHang)
                .HasConstraintName("taikhoan_makhachhang");

            entity.HasOne(d => d.MaVaiTroNavigation).WithMany(p => p.TaiKhoans)
                .HasForeignKey(d => d.MaVaiTro)
                .HasConstraintName("taikhoan_mavaitro");
        });

        modelBuilder.Entity<TrangViet>(entity =>
        {
            entity.HasKey(e => e.MaTrangViet).HasName("PK__TrangVie__6DAAED94955A7B88");

            entity.ToTable("TrangViet");

            entity.Property(e => e.MaTrangViet)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Alias)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.NoiDung).HasColumnType("text");
            entity.Property(e => e.TenTrangViet)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ThoiGianDang).HasColumnType("datetime");
            entity.Property(e => e.Thumb)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.TieuDe)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.TuKhoa)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Url)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("URL");
        });

        modelBuilder.Entity<TrangWeb>(entity =>
        {
            entity.HasKey(e => e.MaTrangWeb).HasName("PK__TrangWeb__F3C8EBD8FC7070B1");

            entity.ToTable("TrangWeb");

            entity.Property(e => e.MaTrangWeb)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TenTrangWeb)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Url)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("URL");
        });

        modelBuilder.Entity<TuKhoa>(entity =>
        {
            entity.HasKey(e => e.MaTuKhoa).HasName("PK__TuKhoa__21DA95685771A748");

            entity.ToTable("TuKhoa");

            entity.Property(e => e.MaTuKhoa)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TenTuKhoa)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VaiTro>(entity =>
        {
            entity.HasKey(e => e.MaVaiTro).HasName("PK__VaiTro__C24C41CFE459D9A1");

            entity.ToTable("VaiTro");

            entity.Property(e => e.MaVaiTro)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ChiTiet).HasColumnType("text");
            entity.Property(e => e.TenVaiTro)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
