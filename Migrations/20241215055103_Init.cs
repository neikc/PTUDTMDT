using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PTUDTMDT.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DiaChi",
                columns: table => new
                {
                    MaDiaChi = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    TenDiaChi = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    ViTriNha = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    ChuanHoaURL = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    TheLoai = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DiaChi__EB61213EB2E77E0F", x => x.MaDiaChi);
                });

            migrationBuilder.CreateTable(
                name: "GiamGia",
                columns: table => new
                {
                    MaGiamGia = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    TenMa = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    GiaTri = table.Column<double>(type: "float", nullable: true),
                    NgayBatDau = table.Column<DateTime>(type: "datetime", nullable: true),
                    NgayKetThuc = table.Column<DateTime>(type: "datetime", nullable: true),
                    SoLuong = table.Column<int>(type: "int", nullable: true),
                    TrangThai = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__GiamGia__EF9458E4AA88837B", x => x.MaGiamGia);
                });

            migrationBuilder.CreateTable(
                name: "GiaoHang",
                columns: table => new
                {
                    MaGiaoHang = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    TenShipper = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CongTyGiaoHang = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    SDT = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__GiaoHang__81CCF4FDB618EB53", x => x.MaGiaoHang);
                });

            migrationBuilder.CreateTable(
                name: "LoaiSanPham",
                columns: table => new
                {
                    MaLoai = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    TenLoai = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    MoTa = table.Column<string>(type: "text", nullable: true),
                    Hinh = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Alias = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__LoaiSanP__730A5759B65DC646", x => x.MaLoai);
                });

            migrationBuilder.CreateTable(
                name: "PhuongThucThanhToan",
                columns: table => new
                {
                    MaPTTT = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    TenPTTT = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    NoiDung = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PhuongTh__B30A2802D873866F", x => x.MaPTTT);
                });

            migrationBuilder.CreateTable(
                name: "TrangViet",
                columns: table => new
                {
                    MaTrangViet = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    TenTrangViet = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    NoiDung = table.Column<string>(type: "text", nullable: true),
                    Thumb = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    ThoiGianDang = table.Column<DateTime>(type: "datetime", nullable: true),
                    TieuDe = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    TuKhoa = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Alias = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    URL = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TrangVie__6DAAED94C37D6D69", x => x.MaTrangViet);
                });

            migrationBuilder.CreateTable(
                name: "TrangWeb",
                columns: table => new
                {
                    MaTrangWeb = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    TenTrangWeb = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    URL = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TrangWeb__F3C8EBD84E5F3189", x => x.MaTrangWeb);
                });

            migrationBuilder.CreateTable(
                name: "TuKhoa",
                columns: table => new
                {
                    MaTuKhoa = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    TenTuKhoa = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TuKhoa__21DA9568A6020086", x => x.MaTuKhoa);
                });

            migrationBuilder.CreateTable(
                name: "KhachHang",
                columns: table => new
                {
                    MaKhachHang = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    HoTen = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    GioiTinh = table.Column<bool>(type: "bit", nullable: true),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    SoDienThoai = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    MaDiaChi = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    NgaySinh = table.Column<DateOnly>(type: "date", nullable: true),
                    Hinh = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    HieuLuc = table.Column<bool>(type: "bit", nullable: true),
                    QuanHuyen = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    PhuongXa = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__KhachHan__88D2F0E512C2EC18", x => x.MaKhachHang);
                    table.ForeignKey(
                        name: "khachhang_madiachi",
                        column: x => x.MaDiaChi,
                        principalTable: "DiaChi",
                        principalColumn: "MaDiaChi");
                });

            migrationBuilder.CreateTable(
                name: "SanPham",
                columns: table => new
                {
                    MaSanPham = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    TenSanPham = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    MaLoai = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    GiaSanPham = table.Column<double>(type: "float", nullable: true),
                    GiamGia = table.Column<double>(type: "float", nullable: true),
                    GiaSauGiam = table.Column<double>(type: "float", nullable: true),
                    ShortDesc = table.Column<string>(type: "text", nullable: true),
                    BestSellers = table.Column<bool>(type: "bit", nullable: true),
                    TinhTrang = table.Column<bool>(type: "bit", nullable: true),
                    SLTonKho = table.Column<int>(type: "int", nullable: true),
                    HomeFlag = table.Column<bool>(type: "bit", nullable: true),
                    MoTa = table.Column<string>(type: "text", nullable: true),
                    Hinh1 = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Hinh2 = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    NgaySanXuat = table.Column<DateTime>(type: "datetime", nullable: true),
                    HanSuDung = table.Column<DateTime>(type: "datetime", nullable: true),
                    Alias = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SanPham__FAC7442D4F1599F6", x => x.MaSanPham);
                    table.ForeignKey(
                        name: "sanpham_maloai",
                        column: x => x.MaLoai,
                        principalTable: "LoaiSanPham",
                        principalColumn: "MaLoai");
                });

            migrationBuilder.CreateTable(
                name: "TaiKhoan",
                columns: table => new
                {
                    MaTaiKhoan = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    TenTaiKhoan = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    MatKhau = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    SoDienThoai = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    VaiTro = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    NgayTaoTK = table.Column<DateTime>(type: "datetime", nullable: true),
                    LanCuoiTruyCap = table.Column<DateTime>(type: "datetime", nullable: true),
                    TinhTrangTK = table.Column<bool>(type: "bit", nullable: true),
                    MaKhachHang = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TaiKhoan__AD7C6529E99E0A11", x => x.MaTaiKhoan);
                    table.ForeignKey(
                        name: "taikhoan_makhachhang",
                        column: x => x.MaKhachHang,
                        principalTable: "KhachHang",
                        principalColumn: "MaKhachHang");
                });

            migrationBuilder.CreateTable(
                name: "ChiTietTuKhoa",
                columns: table => new
                {
                    MaTuKhoa = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    MaSanPham = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ChiTietT__EE76E12A5BC03125", x => new { x.MaTuKhoa, x.MaSanPham });
                    table.ForeignKey(
                        name: "ChiTietTuKhoa_masanpham",
                        column: x => x.MaSanPham,
                        principalTable: "SanPham",
                        principalColumn: "MaSanPham");
                    table.ForeignKey(
                        name: "ChiTietTuKhoa_matukhoa",
                        column: x => x.MaTuKhoa,
                        principalTable: "TuKhoa",
                        principalColumn: "MaTuKhoa");
                });

            migrationBuilder.CreateTable(
                name: "DanhGiaSanPham",
                columns: table => new
                {
                    MaDanhGia = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    MaTaiKhoan = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    MaSanPham = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    SaoDanhGia = table.Column<int>(type: "int", nullable: true),
                    LoiDanhGia = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DanhGiaS__AA9515BFFF00F673", x => x.MaDanhGia);
                    table.ForeignKey(
                        name: "danhgiasanpham_masanpham",
                        column: x => x.MaSanPham,
                        principalTable: "SanPham",
                        principalColumn: "MaSanPham");
                    table.ForeignKey(
                        name: "danhgiasanpham_mataikhoan",
                        column: x => x.MaTaiKhoan,
                        principalTable: "TaiKhoan",
                        principalColumn: "MaTaiKhoan");
                });

            migrationBuilder.CreateTable(
                name: "DonHang",
                columns: table => new
                {
                    MaDonHang = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    MaGiamGia = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    MaTaiKhoan = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    MaGiaoHang = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    NgayLenDon = table.Column<DateTime>(type: "datetime", nullable: true),
                    NgayGiaoHang = table.Column<DateTime>(type: "datetime", nullable: true),
                    TrangThai = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    TTThanhToan = table.Column<bool>(type: "bit", nullable: true),
                    TongTien = table.Column<double>(type: "float", nullable: true),
                    MaPTTT = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    NgayThanhToan = table.Column<DateTime>(type: "datetime", nullable: true),
                    DiaChi = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    GhiChu = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DonHang__129584AD29855D08", x => x.MaDonHang);
                    table.ForeignKey(
                        name: "donhang_magiamgia",
                        column: x => x.MaGiamGia,
                        principalTable: "GiamGia",
                        principalColumn: "MaGiamGia");
                    table.ForeignKey(
                        name: "donhang_magiaohang",
                        column: x => x.MaGiaoHang,
                        principalTable: "GiaoHang",
                        principalColumn: "MaGiaoHang");
                    table.ForeignKey(
                        name: "donhang_mapttt",
                        column: x => x.MaPTTT,
                        principalTable: "PhuongThucThanhToan",
                        principalColumn: "MaPTTT");
                    table.ForeignKey(
                        name: "donhang_mataikhoan",
                        column: x => x.MaTaiKhoan,
                        principalTable: "TaiKhoan",
                        principalColumn: "MaTaiKhoan");
                });

            migrationBuilder.CreateTable(
                name: "ChiTietDonHang",
                columns: table => new
                {
                    MaCTDH = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    MaDonHang = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    MaSanPham = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    SoLuong = table.Column<int>(type: "int", nullable: true),
                    GiaSanPham = table.Column<double>(type: "float", nullable: true),
                    TongTienSP = table.Column<double>(type: "float", nullable: true),
                    NgayTaoDon = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ChiTietD__1E4E40F08902BDFD", x => x.MaCTDH);
                    table.ForeignKey(
                        name: "chitietdonhang_madonhang",
                        column: x => x.MaDonHang,
                        principalTable: "DonHang",
                        principalColumn: "MaDonHang");
                    table.ForeignKey(
                        name: "chitietdonhang_masanpham",
                        column: x => x.MaSanPham,
                        principalTable: "SanPham",
                        principalColumn: "MaSanPham");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDonHang_MaDonHang",
                table: "ChiTietDonHang",
                column: "MaDonHang");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDonHang_MaSanPham",
                table: "ChiTietDonHang",
                column: "MaSanPham");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietTuKhoa_MaSanPham",
                table: "ChiTietTuKhoa",
                column: "MaSanPham");

            migrationBuilder.CreateIndex(
                name: "IX_DanhGiaSanPham_MaSanPham",
                table: "DanhGiaSanPham",
                column: "MaSanPham");

            migrationBuilder.CreateIndex(
                name: "IX_DanhGiaSanPham_MaTaiKhoan",
                table: "DanhGiaSanPham",
                column: "MaTaiKhoan");

            migrationBuilder.CreateIndex(
                name: "IX_DonHang_MaGiamGia",
                table: "DonHang",
                column: "MaGiamGia");

            migrationBuilder.CreateIndex(
                name: "IX_DonHang_MaGiaoHang",
                table: "DonHang",
                column: "MaGiaoHang");

            migrationBuilder.CreateIndex(
                name: "IX_DonHang_MaPTTT",
                table: "DonHang",
                column: "MaPTTT");

            migrationBuilder.CreateIndex(
                name: "IX_DonHang_MaTaiKhoan",
                table: "DonHang",
                column: "MaTaiKhoan");

            migrationBuilder.CreateIndex(
                name: "IX_KhachHang_MaDiaChi",
                table: "KhachHang",
                column: "MaDiaChi");

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_MaLoai",
                table: "SanPham",
                column: "MaLoai");

            migrationBuilder.CreateIndex(
                name: "IX_TaiKhoan_MaKhachHang",
                table: "TaiKhoan",
                column: "MaKhachHang");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietDonHang");

            migrationBuilder.DropTable(
                name: "ChiTietTuKhoa");

            migrationBuilder.DropTable(
                name: "DanhGiaSanPham");

            migrationBuilder.DropTable(
                name: "TrangViet");

            migrationBuilder.DropTable(
                name: "TrangWeb");

            migrationBuilder.DropTable(
                name: "DonHang");

            migrationBuilder.DropTable(
                name: "TuKhoa");

            migrationBuilder.DropTable(
                name: "SanPham");

            migrationBuilder.DropTable(
                name: "GiamGia");

            migrationBuilder.DropTable(
                name: "GiaoHang");

            migrationBuilder.DropTable(
                name: "PhuongThucThanhToan");

            migrationBuilder.DropTable(
                name: "TaiKhoan");

            migrationBuilder.DropTable(
                name: "LoaiSanPham");

            migrationBuilder.DropTable(
                name: "KhachHang");

            migrationBuilder.DropTable(
                name: "DiaChi");
        }
    }
}
