using PTUDTMDT.Models;

namespace PTUDTMDT.ViewModels.BlogViewModel
{
    public class BlogDetailViewModel
    {
        public IEnumerable<SanPham> BestSellers { get; set; }
        public IEnumerable<LoaiSanPham> Categories { get; set; }
    }
}
