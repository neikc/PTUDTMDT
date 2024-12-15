using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PTUDTMDT.Models;
using PTUDTMDT.ViewModels.AccountViewModel;
using System;
using System.Linq;
using System.Security.Claims;

namespace PTUDTMDT.Controllers
{
    public class AccountController : Controller
    {
        private readonly PtudtmdtContext _context;

        public AccountController(PtudtmdtContext context)
        {
            _context = context;
        }
        #region Register
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            // Kiểm tra tính hợp lệ của model
            if (ModelState.IsValid)
            {
                // Tạo đối tượng TaiKhoan
                var taikhoan = new TaiKhoan
                {
                    TenTaiKhoan = model.TenTaiKhoan,
                    MatKhau = model.MatKhau,  // Lưu mật khẩu đã mã hóa
                    SoDienThoai = model.SoDienThoai,
                    Email = model.Email,
                    TinhTrangTk = true,  // Tài khoản mới sẽ được kích hoạt
                    NgayTaoTk = DateTime.Now,
                    // Các thuộc tính khác như VaiTro, LanCuoiTruyCap có thể được thêm vào nếu cần
                };

                // Tạo đối tượng KhachHang
                var khachhang = new KhachHang
                {
                    HoTen = model.HoTen,
                    SoDienThoai = model.SoDienThoai,
                    GioiTinh = model.GioiTinh,
                    Email = model.Email,
                    NgaySinh = model.NgaySinh,  // Đảm bảo sử dụng kiểu DateOnly
                    QuanHuyen = model.QuanHuyen,
                    PhuongXa = model.PhuongXa,
                    HieuLuc = true  // Nếu bạn muốn khách hàng luôn hợp lệ khi đăng ký
                };

                // Lưu vào cơ sở dữ liệu
                _context.TaiKhoans.Add(taikhoan);  // Thêm tài khoản vào bảng TaiKhoan
                _context.KhachHangs.Add(khachhang);  // Thêm khách hàng vào bảng KhachHang
                _context.SaveChanges();  // Lưu thay đổi vào cơ sở dữ liệu

                // Chuyển hướng đến trang đăng nhập sau khi đăng ký thành công
                return RedirectToAction("Login", "Account");
            }

            // Nếu có lỗi, quay lại trang đăng ký với thông báo lỗi
            return View(model);
        }
        #endregion

        #region Login
        [HttpGet]
        public ActionResult Login(string? ReturnURL)
        {
            ViewBag.ReturnURL = ReturnURL;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model, string? ReturnURL)
        {
            ViewBag.ReturnURL = ReturnURL;
            if (ModelState.IsValid)
            {
                var taikhoan = _context.TaiKhoans
                    .FirstOrDefault(tk => tk.TenTaiKhoan == model.TenTaiKhoan && tk.MatKhau == model.MatKhau);
                
                if (taikhoan == null)
                {
                    ModelState.AddModelError("", "Sai thông tin đăng nhập");
                }
                else
                {
                    if (taikhoan.TinhTrangTk == false)
                    {
                        ModelState.AddModelError("", "Tài khoản đã bị vô hiệu hóa. Vui lòng liên hệ Admin để kích hoạt lại tài khoản");
                    }
                    else
                    {
                        if (taikhoan.MatKhau != model.MatKhau)
                        {
                            ModelState.AddModelError("", "Sai thông tin đăng nhập");
                        }
                        else
                        {
                            // Tạo danh sách thông tin (claims) để lưu thông tin về người dùng
                            var claims = new List<Claim>
                            {
                                // Lưu tên người dùng vào claim
                                new Claim(ClaimTypes.Name, taikhoan.MaTaiKhoan),

                                // Lưu vai trò của người dùng (admin, user, ...) vào claim
                                new Claim(ClaimTypes.Role, taikhoan.VaiTro),
                                new Claim("avatar", (taikhoan.MaKhachHangNavigation?.Hinh) ?? "avatar.jpg"),
                                new Claim("userName", taikhoan.TenTaiKhoan),
                            };

                            // Tạo một danh tính người dùng (identity) dựa trên danh sách claims và dùng cách xác thực bằng cookie
                            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                            // Dùng danh tính vừa tạo để tạo một đối tượng đại diện cho người dùng hiện tại
                            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                            // Đăng nhập người dùng bằng cách lưu thông tin vào cookie
                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,claimsPrincipal);

                            // Chuyển hướng đến trang mà người dùng muốn truy cập sau khi đăng nhập
                            if (!string.IsNullOrEmpty(ReturnURL) && Url.IsLocalUrl(ReturnURL))
                            {
                                return Redirect(ReturnURL);
                            }
                            return RedirectToAction("Index", "Home");
                        }
                    }    
                }    
            }
            ModelState.AddModelError("", "Đăng nhập không thành công");
            return View(model);
        }
        #endregion

        #region Profile
        [Authorize]
        [HttpGet]
        public IActionResult Profile()
        {
            var MaTaiKhoan = User.Identity.Name;
            var khachhang = _context.TaiKhoans
                .Include(kh => kh.MaKhachHangNavigation)
                .FirstOrDefault(kh => kh.MaTaiKhoan == MaTaiKhoan);

            var ThongTin = new RegisterViewModel
            {
                MatKhau = khachhang.MatKhau,
                TenTaiKhoan = khachhang.TenTaiKhoan,
                HoTen = khachhang.MaKhachHangNavigation.HoTen,
                GioiTinh = khachhang.MaKhachHangNavigation.GioiTinh,
                SoDienThoai = khachhang.MaKhachHangNavigation.SoDienThoai,
                Email = khachhang.MaKhachHangNavigation.Email,
                NgaySinh = khachhang.MaKhachHangNavigation.NgaySinh,
                QuanHuyen = khachhang.MaKhachHangNavigation.QuanHuyen,
                PhuongXa = khachhang.MaKhachHangNavigation.PhuongXa,
                Hinh = khachhang.MaKhachHangNavigation.Hinh
            };

            var viewModel = new ProfileViewModel
            {
                ThongTin = ThongTin,
                DonHangDangXuLy = _context.DonHangs.Where(dh => dh.MaTaiKhoanNavigation.MaKhachHang == khachhang.MaKhachHang && dh.TrangThai == "Đang xử lý").ToList(),
                DonHangDaXuLy = _context.DonHangs.Where(dh => dh.MaTaiKhoanNavigation.MaKhachHang == khachhang.MaKhachHang && dh.TrangThai == "Đã xử lý").ToList(),
                DonHangHoanTat = _context.DonHangs.Where(dh => dh.MaTaiKhoanNavigation.MaKhachHang == khachhang.MaKhachHang && dh.TrangThai == "Đã xong").ToList()
            };
            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Profile(ProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var MaTaiKhoan = User.Identity.Name;
                var taikhoan = _context.TaiKhoans
                    .Include(kh => kh.MaKhachHangNavigation)
                    .FirstOrDefault(kh => kh.MaTaiKhoan == MaTaiKhoan);

                taikhoan.MatKhau = model.ThongTin.MatKhau;
                taikhoan.MaKhachHangNavigation.HoTen = model.ThongTin.HoTen;
                taikhoan.MaKhachHangNavigation.GioiTinh = model.ThongTin.GioiTinh;
                taikhoan.MaKhachHangNavigation.SoDienThoai = model.ThongTin.SoDienThoai;
                taikhoan.MaKhachHangNavigation.Email = model.ThongTin.Email;
                taikhoan.MaKhachHangNavigation.NgaySinh = model.ThongTin.NgaySinh;
                taikhoan.MaKhachHangNavigation.QuanHuyen = model.ThongTin.QuanHuyen;
                taikhoan.MaKhachHangNavigation.PhuongXa = model.ThongTin.PhuongXa;
                taikhoan.MaKhachHangNavigation.Hinh = model.ThongTin.Hinh;

                _context.SaveChanges();
                return RedirectToAction("Profile", "Account");

            } else
            {
                return View("Profile", model);
            }
        }
        #endregion

        #region LogOut
        [Authorize]
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}
