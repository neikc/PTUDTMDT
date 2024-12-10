using PTUDTMDT.Controllers;
using PTUDTMDT.Models;
using X.PagedList;
using X.PagedList.Mvc.Core;

namespace PTUDTMDT.ViewModels.ShopViewModel
{
    public class ShopIndexViewModel
    {
        public IPagedList<SanPham> Products { get; set; }
        public IEnumerable<SanPham> BestSellers { get; set; }
        public IEnumerable<LoaiSanPham> Categories { get; set; }
        public ShopFilterModel Filter { get; set; }
    }
}
