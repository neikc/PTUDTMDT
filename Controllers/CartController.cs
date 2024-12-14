using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PTUDTMDT.Extensions;
using PTUDTMDT.Models;
using PTUDTMDT.ViewModels.CartViewModel;

namespace PTUDTMDT.Controllers
{
    public class CartController : Controller
    {
        private readonly PtudtmdtContext _context;
        private const string CartSessionKey = "Cart";

        public CartController(PtudtmdtContext context)
        {
            _context = context;
        }
        public IActionResult Cart()
        {
            List<CartItemViewModel> cart;

            if (User.Identity.IsAuthenticated)
            {
                var MaTaiKhoan = User.Identity.Name;

                // Lấy giỏ hàng từ DonHang với trạng thái "Cart"
                var logincart = _context.DonHangs
                    .Include(d => d.ChiTietDonHangs)
                    .ThenInclude(c => c.MaSanPhamNavigation)
                    .FirstOrDefault(d => d.MaTaiKhoan == MaTaiKhoan && d.TrangThai == "Cart");

                // Kiểm tra nếu có giỏ hàng
                if (logincart != null)
                {
                    cart = logincart.ChiTietDonHangs.Select(c => new CartItemViewModel
                    {
                        Product = c.MaSanPhamNavigation,
                        Quantity = c.SoLuong ?? 0
                    }).ToList();
                }
                else
                {
                    // Nếu không có giỏ hàng, tạo giỏ hàng mới
                    cart = new List<CartItemViewModel>();
                }

                // Lấy giỏ hàng từ session
                var sessionCart = HttpContext.Session.GetObject<List<CartItemViewModel>>(CartSessionKey) ?? new List<CartItemViewModel>();

                // Gộp giỏ hàng từ Session vào giỏ hàng DB nếu có sản phẩm trong session
                if (sessionCart.Any())
                {
                    // Nếu giỏ hàng trong session không rỗng, gộp với giỏ hàng trong DB
                    foreach (var sessionItem in sessionCart)
                    {
                        // dbItem là sản phẩm trong giỏ hàng DB
                        var dbItem = logincart.ChiTietDonHangs
                            .FirstOrDefault(c => c.MaSanPham == sessionItem.Product.MaSanPham);

                        if (dbItem != null)
                        {
                            // Cập nhật số lượng sản phẩm trong DB
                            dbItem.SoLuong += sessionItem.Quantity;
                            dbItem.TongTienSp = dbItem.SoLuong * dbItem.MaSanPhamNavigation.GiaSauGiam;
                        }
                        else
                        {
                            // Thêm sản phẩm mới vào DB
                            _context.ChiTietDonHangs.Add(new ChiTietDonHang
                            {
                                MaCtdh = Guid.NewGuid().ToString(),
                                MaDonHang = logincart.MaDonHang,
                                MaSanPham = sessionItem.Product.MaSanPham,
                                SoLuong = sessionItem.Quantity,
                                GiaSanPham = sessionItem.Product.GiaSauGiam,
                                TongTienSp = sessionItem.Quantity * sessionItem.Product.GiaSauGiam,
                                NgayTaoDon = DateTime.Now
                            });
                        }
                    }

                    // Lưu thay đổi vào cơ sở dữ liệu
                    _context.SaveChanges();

                    // Xóa giỏ hàng trong session sau khi đã gộp vào DB
                    HttpContext.Session.Remove(CartSessionKey);
                }
            }
            else
            {
                // Nếu người dùng chưa đăng nhập, lấy giỏ hàng từ Session
                cart = HttpContext.Session.GetObject<List<CartItemViewModel>>(CartSessionKey) ?? new List<CartItemViewModel>();
            }

            // Tạo ViewModel để truyền dữ liệu qua View
            var ViewModel = new CartIndexViewModel
            {
                ItemList = cart,
                BestSellers = GetBestSellers(10),
                OnSale = GetOnSale(10)
            };

            return View(ViewModel);
        }



        [Authorize]
        [HttpGet]
        public IActionResult CheckOut()
        {
            var MaTaiKhoan = User.Identity.Name;

            var user = _context.TaiKhoans
                .Include(t => t.MaKhachHangNavigation)
                .ThenInclude(kh => kh.MaDiaChiNavigation)
                .FirstOrDefault(t => t.MaTaiKhoan == MaTaiKhoan);

            // Lấy đơn hàng có trạng thái "Cart" từ DB
            var logincart = _context.DonHangs
                .Include(d => d.ChiTietDonHangs)
                .ThenInclude(c => c.MaSanPhamNavigation)
                .FirstOrDefault(d => d.MaTaiKhoan == MaTaiKhoan && d.TrangThai == "Cart");

            // Tạo giỏ hàng từ DB
            var cart = logincart?.ChiTietDonHangs.Select(c => new CartItemViewModel
            {
                Product = c.MaSanPhamNavigation,
                Quantity = c.SoLuong ?? 0
            }).ToList() ?? new List<CartItemViewModel>();

            // Tạo ViewModel để hiển thị thông tin trong view
            var ViewModel = new CheckOutViewModel
            {
                ItemList = cart,
                OnSale = GetOnSale(10),
                VoucherList = GetVoucherList(User.Identity.Name),
                BestSellers = GetBestSellers(10),

                HoTen = user.MaKhachHangNavigation.HoTen,
                Email = user.Email,
                SoDienThoai = user.MaKhachHangNavigation.SoDienThoai,
                DiaChi = user.MaKhachHangNavigation.MaDiaChiNavigation.TenDiaChi
            };
            return View(ViewModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult CheckOut(CheckOutViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Gửi lại thông tin view nếu form không hợp lệ
                model.OnSale = GetOnSale(10);
                model.VoucherList = GetVoucherList(User.Identity.Name);
                model.BestSellers = GetBestSellers(10);
                return View(model);
            }

            // Lấy tài khoản người dùng
            var MaTaiKhoan = User.Identity.Name;

            // Lấy đơn hàng "Cart", tức là lấy giỏ hàng ra từ CSDL
            var cartDonHang = _context.DonHangs
                .Include(d => d.ChiTietDonHangs)
                .ThenInclude(c => c.MaSanPhamNavigation)
                .FirstOrDefault(d => d.MaTaiKhoan == MaTaiKhoan && d.TrangThai == "Cart");

            if (cartDonHang == null || !cartDonHang.ChiTietDonHangs.Any())
            {
                ModelState.AddModelError("", "Giỏ hàng của bạn đang trống hoặc không tồn tại.");
                return View(model);
            }

            // Cập nhật thông tin đơn hàng với dữ liệu từ form
            #region Cập nhật dữ liệu từ form vào CSDL
            // Kiểm tra nếu phương thức giao hàng là Delivery
            if (model.MaGiaoHang == "Delivery")
            {
                // Tạo MaGiaoHang mới và gán cho đơn hàng
                cartDonHang.MaGiaoHang = Guid.NewGuid().ToString();

                // Tạo một đối tượng mới cho bảng GiaoHang và thêm vào DB
                var newGiaoHang = new GiaoHang
                {
                    MaGiaoHang = cartDonHang.MaGiaoHang,
                    // Các thuộc tính khác có thể để trống (empty)
                };
                _context.GiaoHangs.Add(newGiaoHang);
            }
            else
            {
                // Nếu không phải Delivery, gán giá trị Pickup
                cartDonHang.MaGiaoHang = "Pickup";
            }
            cartDonHang.MaPttt = model.MaPttt;
            cartDonHang.DiaChi = model.DiaChi;
            cartDonHang.GhiChu = model.GhiChu;
            cartDonHang.TrangThai = "Đang xử lý"; // Đổi trạng thái thành "Pending"
            cartDonHang.TtthanhToan = false;   // Chưa thanh toán
            cartDonHang.NgayLenDon = DateTime.Now;
            cartDonHang.TongTien = cartDonHang.ChiTietDonHangs.Sum(c => c.SoLuong * c.GiaSanPham); // Tính tổng tiền

            // Lưu thay đổi
            _context.DonHangs.Update(cartDonHang);
            _context.SaveChanges();
            #endregion

            // Chuyển hướng tới trang xác nhận
            TempData["SuccessMessage"] = "Đơn hàng của bạn đã được xử lý thành công!";
            return RedirectToAction("OrderConfirmation", new { maDonHang = cartDonHang.MaDonHang });
        }

        public IActionResult OrderConfirmation()
        {
            return View();
        }


        [HttpPost]
        public IActionResult AddToCart(string MaSanPham, int quantity = 1)
        {
            var product = _context.SanPhams.Find(MaSanPham); // Tìm sản phẩm
            if (product == null)
                return Json(new { success = false, message = "Sản phẩm không tồn tại" });

            // Kiểm tra nếu người dùng đã đăng nhập
            if (User.Identity.IsAuthenticated)
            {
                var MaTaiKhoan = User.Identity.Name;

                // Lấy đơn hàng có trạng thái "Cart" của người dùng
                var logincart = _context.DonHangs.FirstOrDefault(d => d.MaTaiKhoan == MaTaiKhoan && d.TrangThai == "Cart");

                if (logincart == null)
                {
                    // Nếu không có giỏ hàng, khởi tạo giỏ hàng
                    logincart = new DonHang
                    {
                        MaDonHang = Guid.NewGuid().ToString(),
                        MaTaiKhoan = MaTaiKhoan,
                        TrangThai = "Cart", // Trạng thái là "Cart" cho đơn hàng này
                        NgayLenDon = DateTime.Now
                    };
                    _context.DonHangs.Add(logincart);
                    _context.SaveChanges(); // Lưu giỏ hàng vào DB
                }

                // Kiểm tra sản phẩm trong chi tiết giỏ hàng
                var logincartContent = _context.ChiTietDonHangs
                    .FirstOrDefault(c => c.MaDonHang == logincart.MaDonHang && c.MaSanPham == MaSanPham);

                if (logincartContent != null)
                {
                    // Nếu sản phẩm đã có trong giỏ hàng, cập nhật số lượng
                    logincartContent.SoLuong += quantity;
                    logincartContent.TongTienSp = logincartContent.SoLuong * logincartContent.MaSanPhamNavigation.GiaSauGiam;
                }
                else
                {
                    // Nếu sản phẩm chưa có trong giỏ hàng, thêm mới
                    _context.ChiTietDonHangs.Add(new ChiTietDonHang
                    {
                        MaCtdh = Guid.NewGuid().ToString(),
                        MaDonHang = logincart.MaDonHang,
                        MaSanPham = MaSanPham,
                        SoLuong = quantity,
                        GiaSanPham = product.GiaSauGiam,
                        TongTienSp = quantity * product.GiaSauGiam,
                        NgayTaoDon = DateTime.Now
                    });
                }

                _context.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu
            }
            else
            {
                // Nếu người dùng chưa đăng nhập, lưu vào Session
                var cart = HttpContext.Session.GetObject<List<CartItemViewModel>>(CartSessionKey) ?? new List<CartItemViewModel>();

                var cartItem = cart.FirstOrDefault(x => x.Product.MaSanPham == MaSanPham);
                if (cartItem != null)
                {
                    cartItem.Quantity += quantity;
                }
                else
                {
                    cart.Add(new CartItemViewModel
                    {
                        Product = product,
                        Quantity = quantity
                    });
                }
                HttpContext.Session.SetObject(CartSessionKey, cart);
            }

            TempData["Message"] = "Đã thêm vào giỏ hàng";
            return RedirectToAction("Cart"); // Quay lại trang giỏ hàng
        }




        [HttpPost]
        public IActionResult UpdateQuantity(string MaSanPham, string action)
        {
            // Lấy giỏ hàng từ Session
            var cart = HttpContext.Session.GetObject<List<CartItemViewModel>>(CartSessionKey) ?? new List<CartItemViewModel>();

            // Tìm sản phẩm trong giỏ hàng
            var cartItem = cart.FirstOrDefault(x => x.Product.MaSanPham == MaSanPham);
            if (cartItem != null)
            {
                if (action == "increase")
                {
                    cartItem.Quantity++; // Tăng số lượng
                }
                else if (action == "decrease" && cartItem.Quantity > 1)
                {
                    cartItem.Quantity--; // Giảm số lượng, không cho xuống dưới 1
                }
            }

            // Lưu giỏ hàng vào session sau khi cập nhật
            HttpContext.Session.SetObject(CartSessionKey, cart);

            // Điều hướng về trang giỏ hàng
            return RedirectToAction("Cart");
        }

        [HttpPost]
        public IActionResult RemoveFromCart(string MaSanPham)
        {
            if (User.Identity.IsAuthenticated)
            {
                var MaTaiKhoan = User.Identity.Name;

                // Lấy đơn hàng có trạng thái "Cart" của người dùng
                var logincart = _context.DonHangs
                    .Include(d => d.ChiTietDonHangs)
                    .FirstOrDefault(d => d.MaTaiKhoan == MaTaiKhoan && d.TrangThai == "Cart");

                if (logincart != null)
                {
                    // Tìm sản phẩm trong chi tiết giỏ hàng
                    var cartItem = logincart.ChiTietDonHangs.FirstOrDefault(x => x.MaSanPham == MaSanPham);
                    if (cartItem != null)
                    {
                        // Xóa sản phẩm khỏi giỏ hàng DB
                        _context.ChiTietDonHangs.Remove(cartItem);
                        _context.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu
                    }
                }
            }
            else
            {
                // Nếu người dùng chưa đăng nhập, lấy giỏ hàng từ Session
                var cart = HttpContext.Session.GetObject<List<CartItemViewModel>>(CartSessionKey) ?? new List<CartItemViewModel>();

                // Tìm sản phẩm trong giỏ hàng
                var cartItem = cart.FirstOrDefault(x => x.Product.MaSanPham == MaSanPham);
                if (cartItem != null)
                {
                    // Xóa sản phẩm khỏi giỏ hàng
                    cart.Remove(cartItem);

                    // Lưu giỏ hàng mới vào Session
                    HttpContext.Session.SetObject(CartSessionKey, cart);
                }
            }

            // Điều hướng lại trang giỏ hàng
            return RedirectToAction("Cart");
        }


        #region Supporting Methods
        private IEnumerable<SanPham> GetOnSale(int count)
        {
            var onSaleProducts = _context.SanPhams
                .Include(p => p.MaLoaiNavigation)
                .Where(p => p.GiaSanPham != p.GiaSauGiam)
                .OrderBy(x => Guid.NewGuid())
                .Take(count)
                .ToList();

            return onSaleProducts ?? Enumerable.Empty<SanPham>();
        }

        private IEnumerable<SanPham> GetBestSellers(int count)
        {
            return _context.SanPhams
                .Where(p => p.BestSellers.HasValue && p.BestSellers.Value)
                .OrderBy(x => Guid.NewGuid())
                .Take(count)
                .ToList();
        }

        private IEnumerable<GiamGium> GetVoucherList(string MaTaiKhoan)
        {
            return _context.GiamGia
             .Where(g => g.MaGiamGia.StartsWith(MaTaiKhoan + "voucher"))
             .ToList() ?? new List<GiamGium>();
        }
        #endregion
    }
}

