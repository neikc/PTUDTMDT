using Microsoft.AspNetCore.Mvc;
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
            return View(cart);
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
    }
}

