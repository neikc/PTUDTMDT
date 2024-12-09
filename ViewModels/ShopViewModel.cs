using PTUDTMDT.Models;
using X.PagedList;
using X.PagedList.Mvc.Core;

namespace PTUDTMDT.ViewModels
{
    public class ShopViewModel
    {
        public IPagedList<SanPham> Products { get; set; } //Sản phẩm dành cho khu vực hiern thị sản phẩm, dựa vào đây để phân trang
        public IEnumerable<LoaiSanPham> Categories { get; set; } //Danh sách các danh mục sản phẩm
        public IEnumerable<SanPham> BestSellers { get; set; } //Danh sách sản phẩm bán chạy
        public int? PriceRangeMax { get; set; } //Sử dụng cho filter giá - giá trần
        public int? PriceRangeMin { get; set; } //Sử dụng cho filter giá - giá sàn
    }


}
