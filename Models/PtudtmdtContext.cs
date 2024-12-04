using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PTUDTMDT.Models;

public partial class PtudtmdtContext : DbContext
{
    public PtudtmdtContext()
    {
    }

    public PtudtmdtContext(DbContextOptions<PtudtmdtContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }

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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=CKEN56;Initial Catalog=ptudtmdt;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChiTietDonHang>(entity =>
        {
            entity.HasKey(e => e.MaCtdh).HasName("PK__ChiTietD__1E4E40F08902BDFD");

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

        modelBuilder.Entity<DanhGiaSanPham>(entity =>
        {
            entity.HasKey(e => e.MaDanhGia).HasName("PK__DanhGiaS__AA9515BFFF00F673");

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
            entity.HasKey(e => e.MaDiaChi).HasName("PK__DiaChi__EB61213EB2E77E0F");

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
            entity.Property(e => e.ViTriNha)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DonHang>(entity =>
        {
            entity.HasKey(e => e.MaDonHang).HasName("PK__DonHang__129584AD29855D08");

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
            entity.HasKey(e => e.MaGiamGia).HasName("PK__GiamGia__EF9458E4AA88837B");

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
            entity.HasKey(e => e.MaGiaoHang).HasName("PK__GiaoHang__81CCF4FDB618EB53");

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
            entity.HasKey(e => e.MaKhachHang).HasName("PK__KhachHan__88D2F0E512C2EC18");

            entity.ToTable("KhachHang");

            entity.Property(e => e.MaKhachHang)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Hinh)
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
            entity.HasKey(e => e.MaLoai).HasName("PK__LoaiSanP__730A5759B65DC646");

            entity.ToTable("LoaiSanPham");

            entity.Property(e => e.MaLoai)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Alias)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Hinh)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MoTa).HasColumnType("text");
            entity.Property(e => e.TenLoai)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PhuongThucThanhToan>(entity =>
        {
            entity.HasKey(e => e.MaPttt).HasName("PK__PhuongTh__B30A2802D873866F");

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
            entity.HasKey(e => e.MaSanPham).HasName("PK__SanPham__FAC7442D4F1599F6");

            entity.ToTable("SanPham");

            entity.Property(e => e.MaSanPham)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Alias)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.HanSuDung).HasColumnType("datetime");
            entity.Property(e => e.Hinh1)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Hinh2)
                .HasMaxLength(50)
                .IsUnicode(false);
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

            entity.HasOne(d => d.MaLoaiNavigation).WithMany(p => p.SanPhams)
                .HasForeignKey(d => d.MaLoai)
                .HasConstraintName("sanpham_maloai");
        });

        modelBuilder.Entity<TaiKhoan>(entity =>
        {
            entity.HasKey(e => e.MaTaiKhoan).HasName("PK__TaiKhoan__AD7C6529E99E0A11");

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
            entity.Property(e => e.VaiTro)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.MaKhachHangNavigation).WithMany(p => p.TaiKhoans)
                .HasForeignKey(d => d.MaKhachHang)
                .HasConstraintName("taikhoan_makhachhang");
        });

        modelBuilder.Entity<TrangViet>(entity =>
        {
            entity.HasKey(e => e.MaTrangViet).HasName("PK__TrangVie__6DAAED94C37D6D69");

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
            entity.HasKey(e => e.MaTrangWeb).HasName("PK__TrangWeb__F3C8EBD84E5F3189");

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
            entity.HasKey(e => e.MaTuKhoa).HasName("PK__TuKhoa__21DA9568A6020086");

            entity.ToTable("TuKhoa");

            entity.Property(e => e.MaTuKhoa)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TenTuKhoa)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasMany(d => d.MaSanPhams).WithMany(p => p.MaTuKhoas)
                .UsingEntity<Dictionary<string, object>>(
                    "ChiTietTuKhoa",
                    r => r.HasOne<SanPham>().WithMany()
                        .HasForeignKey("MaSanPham")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("ChiTietTuKhoa_masanpham"),
                    l => l.HasOne<TuKhoa>().WithMany()
                        .HasForeignKey("MaTuKhoa")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("ChiTietTuKhoa_matukhoa"),
                    j =>
                    {
                        j.HasKey("MaTuKhoa", "MaSanPham").HasName("PK__ChiTietT__EE76E12A5BC03125");
                        j.ToTable("ChiTietTuKhoa");
                        j.IndexerProperty<string>("MaTuKhoa")
                            .HasMaxLength(50)
                            .IsUnicode(false);
                        j.IndexerProperty<string>("MaSanPham")
                            .HasMaxLength(50)
                            .IsUnicode(false);
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
