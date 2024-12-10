using PTUDTMDT.Models;

namespace PTUDTMDT.ViewModels.ShopViewModel
{
    public class ShopDetailViewModel
    {
        public SanPham Product { get; set; }
        public IEnumerable<SanPham> RelatedProducts { get; set; }
        public IEnumerable<SanPham> BestSellers { get; set; }
        public IEnumerable<LoaiSanPham> Categories { get; set; }
    }
}
