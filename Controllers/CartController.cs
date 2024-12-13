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
            //CartSessionKey là key để lưu giỏ hàng vào Session
            var cart = HttpContext.Session.GetObject<List<CartItemViewModel>>(CartSessionKey) ?? new List<CartItemViewModel>();

            var ViewModel = new CartIndexViewModel
            {
                ItemList = cart,
                BestSellers = GetBestSellers(10),
                OnSale = GetOnSale(10)
            };
            return View(ViewModel);
        }

        [HttpPost]
        public IActionResult AddToCart(string MaSanPham, int quantity = 1)
        {
            var product = _context.SanPhams.Find(MaSanPham); //Tìm sản phẩm có MaSanPham tương ứng
            if (product == null)
                return Json(new { success = false, message = "Sản phẩm không tồn tại" });

            //Lấy giỏ hàng từ Session
            var cart = HttpContext.Session.GetObject<List<CartItemViewModel>>(CartSessionKey) ?? new List<CartItemViewModel>();

            //Kiểm tra sản phẩm đã có trong giỏ hàng chưa
            var cartItem = cart.FirstOrDefault(x => x.Product.MaSanPham == MaSanPham);
            if (cartItem != null)
            {
                //Nếu đã có thì tăng số lượng
                cartItem.Quantity += quantity;
            }
            else
            {
                //Nếu chưa có thì thêm mới
                cart.Add(new CartItemViewModel
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            //Lưu giỏ hàng mới vào Session
            HttpContext.Session.SetObject(CartSessionKey, cart);

            TempData["Message"] = "Đã thêm vào giỏ hàng";
            return RedirectToAction("Cart"); // fallback nếu không có returnUrl
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

            // Lưu lại giỏ hàng vào Session
            HttpContext.Session.SetObject(CartSessionKey, cart);

            // Điều hướng về trang giỏ hàng
            return RedirectToAction("Cart");
        }

        [HttpPost]
        public IActionResult RemoveFromCart(string MaSanPham)
        {
            // Lấy giỏ hàng từ Session
            var cart = HttpContext.Session.GetObject<List<CartItemViewModel>>(CartSessionKey) ?? new List<CartItemViewModel>();

            // Tìm sản phẩm trong giỏ hàng
            var cartItem = cart.FirstOrDefault(x => x.Product.MaSanPham == MaSanPham);
            if (cartItem != null)
            {
                // Xóa sản phẩm khỏi giỏ hàng
                cart.Remove(cartItem);
            }

            // Lưu giỏ hàng mới vào Session
            HttpContext.Session.SetObject(CartSessionKey, cart);

            // Điều hướng lại trang giỏ hàng
            return RedirectToAction("Cart");
        }
    
        public IActionResult CheckOut()
        {
            return View();
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
        #endregion
    }
}

