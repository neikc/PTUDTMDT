using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PTUDTMDT.Models;
using PTUDTMDT.ViewModels.HomeViewModel;
using PTUDTMDT.ViewModels.ShopViewModel;
using System.Diagnostics;

namespace PTUDTMDT.Controllers
{
    public class HomeController : Controller
    {
        private readonly PtudtmdtContext _context;

        public HomeController(PtudtmdtContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel
            {
                Products = GetProduct(),
                BestSellers = GetBestSellers(10),
                Categories = GetCategories(),
                Reviews = GetReviews(3)
            };
            return View(viewModel);
        }

        public IActionResult TermsOfUse()
        {
            var viewModel = new IndexViewModel
            {
                Products = GetProduct(),
                BestSellers = GetBestSellers(10),
                Categories = GetCategories(),
                Reviews = GetReviews(3)
            };
            return View(viewModel);
        }

        public IActionResult Refunds()
        {
            var viewModel = new IndexViewModel
            {
                Products = GetProduct(),
                BestSellers = GetBestSellers(10),
                Categories = GetCategories(),
                Reviews = GetReviews(3)
            };
            return View(viewModel);
        }
        public IActionResult Privacy()
        {
            var viewModel = new IndexViewModel
            {
                Products = GetProduct(),
                BestSellers = GetBestSellers(10),
                Categories = GetCategories(),
                Reviews = GetReviews(3)
            };
            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #region Supporting Methods
        private IEnumerable<DanhGiaSanPham> GetReviews(int count)
        {
            return _context.DanhGiaSanPhams
                .Include(r => r.MaTaiKhoanNavigation)
                    .ThenInclude(o => o.MaKhachHangNavigation)
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

        private IEnumerable<SanPham> GetBestSellers(int count)
        {
            return _context.SanPhams
                .Where(p => p.BestSellers.HasValue && p.BestSellers.Value)
                .OrderBy(x => Guid.NewGuid())
                .Take(count)
                .ToList();
        }

        private IEnumerable<SanPham> GetProduct()
        {
            return _context.SanPhams
                .OrderBy(x => Guid.NewGuid())
                .ToList();
        }

        #endregion
    }
}
