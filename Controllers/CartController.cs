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
        public IActionResult CheckOut()
        {
            var cart = new List<CartItemViewModel>();
            var MaTaiKhoan = User.Identity.Name;

            // Lấy giỏ hàng từ session
            var sessionCart = HttpContext.Session.GetObject<List<CartItemViewModel>>(CartSessionKey) ?? new List<CartItemViewModel>();

            // Lấy đơn hàng có trạng thái "Cart" để làm giỏ hàng từ DB
            var logincart = _context.DonHangs
                .Include(d => d.ChiTietDonHangs)
                .ThenInclude(c => c.MaSanPhamNavigation)
                .FirstOrDefault(d => d.MaTaiKhoan == MaTaiKhoan && d.TrangThai == "Cart");

            // Kiểm tra nếu có giỏ hàng trong DB
            if (logincart != null)
            {
                cart = logincart.ChiTietDonHangs.Select(c => new CartItemViewModel
                {
                    Product = c.MaSanPhamNavigation,
                    Quantity = c.SoLuong ?? 0
                }).ToList();
            }

            // Gộp giỏ hàng từ session vào giỏ hàng trong DB nếu người dùng đã đăng nhập
            if (sessionCart.Any())
            {
                // Nếu đã có giỏ hàng trong DB, ta gộp lại
                if (logincart != null)
                {
                    foreach (var sessionItem in sessionCart)
                    {
                        var dbItem = logincart.ChiTietDonHangs
                            .FirstOrDefault(c => c.MaSanPham == sessionItem.Product.MaSanPham);

                        if (dbItem != null)
                        {
                            // Cập nhật số lượng trong DB
                            dbItem.SoLuong += sessionItem.Quantity;
                            dbItem.TongTienSp = dbItem.SoLuong * dbItem.MaSanPhamNavigation.GiaSauGiam;
                        }
                        else
                        {
                            // Thêm mới sản phẩm vào giỏ hàng trong DB
                            _context.ChiTietDonHangs.Add(new ChiTietDonHang
                            {
                                MaCtdh = Guid.NewGuid().ToString(),
                                MaDonHang = logincart.MaDonHang,
                                MaSanPham = sessionItem.Product.MaSanPham,
                                SoLuong = sessionItem.Quantity,
                                GiaSanPham = sessionItem.Product.GiaSauGiam,
                                TongTienSp = sessionItem.Quantity * sessionItem.Product.GiaSauGiam
                            });
                        }
                    }

                    _context.SaveChanges();
                }
                else
                {
                    // Nếu giỏ hàng trong DB chưa tồn tại, tạo mới một DonHang và ChiTietDonHang
                    var newDonHang = new DonHang
                    {
                        MaDonHang = Guid.NewGuid().ToString(),
                        MaTaiKhoan = MaTaiKhoan,
                        TrangThai = "Cart",
                        NgayLenDon = DateTime.Now,
                        TongTien = sessionCart.Sum(item => item.Quantity * item.Product.GiaSauGiam)
                    };

                    _context.DonHangs.Add(newDonHang);
                    _context.SaveChanges(); // Lưu DonHang vào DB

                    // Thêm các sản phẩm trong session vào ChiTietDonHang
                    foreach (var sessionItem in sessionCart)
                    {
                        _context.ChiTietDonHangs.Add(new ChiTietDonHang
                        {
                            MaDonHang = newDonHang.MaDonHang,
                            MaSanPham = sessionItem.Product.MaSanPham,
                            SoLuong = sessionItem.Quantity,
                            GiaSanPham = sessionItem.Product.GiaSauGiam,
                            TongTienSp = sessionItem.Quantity * sessionItem.Product.GiaSauGiam
                        });
                    }

                    _context.SaveChanges(); // Lưu ChiTietDonHang vào DB
                }

                // Xóa giỏ hàng trong session sau khi đã gộp vào DB
                HttpContext.Session.Clear();
            }

            // Tạo ViewModel và trả về view
            var ViewModel = new CheckOutViewModel
            {
                ItemList = cart,
                OnSale = GetOnSale(10),
                User = _context.TaiKhoans.Find(User.Identity.Name),
                VoucherList = GetVoucherList(User.Identity.Name),
                BestSellers = GetBestSellers(10)
            };

            return View(ViewModel);
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
             .ToList();
        }
        #endregion
    }
}

