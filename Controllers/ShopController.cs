using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PTUDTMDT.Models;
using PTUDTMDT.ViewModels;
using X.PagedList;
using X.PagedList.Mvc.Core;
using X.PagedList.Extensions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PTUDTMDT.Controllers
{
    public class ShopController : Controller
    {
        private readonly PtudtmdtContext _context;

        public ShopController(PtudtmdtContext context)
        {
            _context = context;
        }

        public IActionResult ShopIndex(int? page, string? searchTerm, int? priceRangeMin, int? priceRangeMax)
        {
            // Số mục hiển thị trên mỗi trang
            int pageSize = 12;
            // Trang hiện tại (mặc định là trang 1 nếu không được truyền vào)
            int pageNumber = page ?? 1;

            // Lấy danh sách sản phẩm từ database cho mục thể hiện danh sách sản phẩm
            IPagedList<SanPham> products;
            IEnumerable<SanPham> products_tmp;

            //Nếu không có từ khóa tìm kiếm thì lấy tất cả sản phẩm
           
            if (searchTerm == null)
            {
                products_tmp = _context.SanPhams
                                   .Include(s => s.MaLoaiNavigation)
                                   .OrderBy(p => p.MaSanPham)
                                   .ToList();
            }
            //Nếu có từ khóa tìm kiếm thì lọc sản phẩm theo từ khóa
            else
            {
                products_tmp = _context.SanPhams
                   .Include(s => s.MaTuKhoas)
                   .Where(p => p.MaTuKhoas.Any(k => k.TenTuKhoa.ToLower().Contains(searchTerm.ToLower())) ||  // Từ nhập vào có trùng với keyword nào của sản phẩm không
                               p.TenSanPham.ToLower().Contains(searchTerm.ToLower()) ||  // Từ nhập vào có trong tên sản phẩm không?
                               p.MoTa.ToLower().Contains(searchTerm.ToLower())) // Từ nhập vào có trong description của sản phẩm không
                   .OrderBy(p => p.MaSanPham)
                   .ToList();
            }

            //Nếu có filter giá sàn thì lọc sản phẩm theo giá
            if (priceRangeMin == null) priceRangeMin = 1000;
            if (priceRangeMax == null) priceRangeMax = 1000000000;
            if (priceRangeMin > priceRangeMax) priceRangeMin = priceRangeMax;
            if (priceRangeMax== 1000) priceRangeMax = 1000000000;
            if (priceRangeMax.HasValue && priceRangeMin.HasValue)
            {
                products_tmp = products_tmp.Where(p => p.GiaSauGiam >= priceRangeMin && p.GiaSauGiam <= priceRangeMax).ToList();
            }

            //Phân trang sản phẩm
            products = products_tmp.ToPagedList(pageNumber, pageSize);


            //Tạo danh sách sản phẩm bán chạy nhất
            IEnumerable<SanPham> BestSellers;
            BestSellers = _context.SanPhams
                .Where(p => p.BestSellers == true)
                .OrderBy(p => p.MaSanPham)
                .ToList();

            // Tạo ViewModel
            var viewModel = new ShopViewModel
            {
                Categories = _context.LoaiSanPhams
                                     .Include(l => l.SanPhams)
                                     .ToList(),
                Products = products, // Đưa danh sách sản phẩm đã phân trang vào ViewModel
                BestSellers = BestSellers // Đưa danh sách sản phẩm bán chạy vào ViewModel
            };

            return View(viewModel);
        }

    }
}
