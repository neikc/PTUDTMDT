using PTUDTMDT.Models;
using X.PagedList;

namespace PTUDTMDT.ViewModels.HomeViewModel
{
    public class IndexViewModel
    {
        public IEnumerable<SanPham> Products { get; set; }
        public IEnumerable<SanPham> BestSellers { get; set; }
        public IEnumerable<LoaiSanPham> Categories { get; set; }
        public IEnumerable<DanhGiaSanPham> Reviews { get; set; } = new List<DanhGiaSanPham>();
    }
}
