using PTUDTMDT.Models;

namespace PTUDTMDT.ViewModels.CartViewModel
{
    public class CartIndexViewModel
    {
        public IEnumerable<CartItemViewModel> ItemList { get; set; }
        public IEnumerable<SanPham> BestSellers { get; set; }
        public IEnumerable<SanPham> OnSale { get; set; }
    }
}
