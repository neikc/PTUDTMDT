using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
                                new Claim(ClaimTypes.Name, taikhoan.TenTaiKhoan),

                                // Lưu vai trò của người dùng (admin, user, ...) vào claim
                                new Claim(ClaimTypes.Role, taikhoan.VaiTro),
                                new Claim("avatar", (taikhoan.MaKhachHangNavigation?.Hinh) ?? "avatar.jpg"),
                            };

                            // Tạo một danh tính người dùng (identity) dựa trên danh sách claims và dùng cách xác thực bằng cookie
                            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                            // Dùng danh tính vừa tạo để tạo một đối tượng đại diện cho người dùng hiện tại
                            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                            // Đăng nhập người dùng bằng cách lưu thông tin vào cookie
                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,claimsPrincipal);

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
        public IActionResult Profile()
        {
            return View();
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
