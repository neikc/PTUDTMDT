using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PTUDTMDT.Models;
using PTUDTMDT.ViewModels;
using X.PagedList;
using X.PagedList.Mvc.Core;
using X.PagedList.Extensions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Linq;

namespace PTUDTMDT.Controllers
{
    //Tổng hợp toàn bộ Filter, phân trang vào 1 class cho dễ quản lý
    public class ShopFilterModel
    {
        public string? SearchTerm { get; set; }
        public int? PriceRangeMin { get; set; } = 1000;
        public int? PriceRangeMax { get; set; } = 1000000000;
        public string? Category { get; set; }
        public string? Sorting { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 12;
    }

    public class ShopController : Controller
    {
        private readonly PtudtmdtContext _context;

        public ShopController(PtudtmdtContext context)
        {
            _context = context;
        }

        public IActionResult ShopIndex(ShopFilterModel filter)
        {
            //Khởi tạo filter mới nếu filter truyền vào là null
            filter ??= new ShopFilterModel();
            
            //Chuẩn hóa bộ lọc giá (các trường hợp có thể người dùng kéo sai)
            ValidatePriceRange(filter);

            //Khởi tạo danh sách sản phẩm ban đầu dựa trên thanh tìm kiếm
            var query = BuildBaseQuery(filter);

            //Áp dụng các bộ lọc
            query = ApplyFilters(query, filter);

            //Áp dụng sắp xếp
            query = ApplySorting(query, filter.Sorting);

            //Phân trang
            var products = query.ToList().ToPagedList(filter.Page, filter.PageSize);

            //Tạo view model để truyền về view ShopIndex
            var viewModel = new ShopViewModel
            {
                Products = products,
                BestSellers = GetBestSellers(3),
                Categories = GetCategories(),
                Filter = filter
            };

            return View(viewModel);
        }

        public IActionResult ShopDetail(ShopFilterModel filter)
        {
            return View();
        }
        private IQueryable<SanPham> BuildBaseQuery(ShopFilterModel filter)
        {
            if (string.IsNullOrEmpty(filter.SearchTerm))
            {
                return _context.SanPhams
                              .Include(s => s.MaLoaiNavigation)
                              .OrderBy(p => p.MaSanPham);
            }

            return _context.SanPhams
                .Include(s => s.MaTuKhoas)
                .Where(p => p.MaTuKhoas.Any(k => k.TenTuKhoa.ToLower().Contains(filter.SearchTerm.ToLower())) ||
                           p.TenSanPham.ToLower().Contains(filter.SearchTerm.ToLower()) ||
                           p.MoTa.ToLower().Contains(filter.SearchTerm.ToLower()))
                .OrderBy(p => p.MaSanPham);
        }

        private IQueryable<SanPham> ApplyFilters(IQueryable<SanPham> query, ShopFilterModel filter)
        {
            //Filter theo giá
            if (filter.PriceRangeMin.HasValue && filter.PriceRangeMax.HasValue)
            {
                query = query.Where(p => p.GiaSauGiam >= filter.PriceRangeMin &&
                                       p.GiaSauGiam <= filter.PriceRangeMax);
            }

            //Filter theo loại sản phẩm
            if (filter.Category != null)
            {
                query = query.Where(p => p.MaLoai == filter.Category);
            }

            return query;
        }

        private void ValidatePriceRange(ShopFilterModel filter)
        {
            //Xử lý các trường hợp người dùng kéo sai thanh giá
            if (filter.PriceRangeMin > filter.PriceRangeMax)
            {
                filter.PriceRangeMin = filter.PriceRangeMax;
            }
            if (filter.PriceRangeMax == 1000)
            {
                filter.PriceRangeMax = 1000000000;
            }
        }

        private IEnumerable<SanPham> GetBestSellers(int count)
        {
            return _context.SanPhams
                .Where(p => p.BestSellers.HasValue && p.BestSellers.Value)
                .OrderBy(x => Guid.NewGuid())
                .Take(count)
                .ToList();
        }

        private IEnumerable<LoaiSanPham> GetCategories()
        {
            return _context.LoaiSanPhams
                .Include(l => l.SanPhams)
                .ToList();
        }

        private IQueryable<SanPham> ApplySorting(IQueryable<SanPham> query, string? sorting)
        {
            switch (sorting)
            {
                case "name_asc":
                    query = query.OrderBy(p => p.TenSanPham);
                    break;
                case "name_desc":
                    query = query.OrderByDescending(p => p.TenSanPham);
                    break;
                case "price_asc":
                    query = query.OrderBy(p => p.GiaSauGiam);
                    break;
                case "price_desc":
                    query = query.OrderByDescending(p => p.GiaSauGiam);
                    break;
                default:
                    break; // Mặc định không thay đổi thứ tự
            }
            return query;
        }

    }
}
