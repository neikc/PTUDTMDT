using PTUDTMDT.Models;

namespace PTUDTMDT.ViewModels.CartViewModel
{
    public class CheckOutViewModel
    {
        public IEnumerable<CartItemViewModel> ItemList { get; set; }
        public IEnumerable<SanPham> OnSale { get; set; } = new List<SanPham>();
        public IEnumerable<SanPham> BestSellers { get; set; } = new List<SanPham>();
        public TaiKhoan User { get; set; }
        public IEnumerable<GiamGium> VoucherList { get; set; } = new List<GiamGium>();
    }
}
