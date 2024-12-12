using PTUDTMDT.Models;

namespace PTUDTMDT.ViewModels.CartViewModel
{
    public class CartItemViewModel
    {
        public SanPham Product { get; set; }
        public int Quantity { get; set; }
        public double Total => ((Product.GiaSauGiam ?? Product.GiaSanPham) ?? 0) * Quantity;

    }
}
