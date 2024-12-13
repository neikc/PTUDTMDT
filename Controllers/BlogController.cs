using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PTUDTMDT.Models;
using PTUDTMDT.ViewModels.BlogViewModel;

namespace PTUDTMDT.Controllers
{
    public class BlogController : Controller
    {
        private readonly PtudtmdtContext _context;

        public BlogController(PtudtmdtContext context)
        {
            _context = context;
        }

        public IActionResult BlogIndex()
        {
            var viewModel = new BlogDetailViewModel
            {
                BestSellers = GetBestSellers(5),
                Categories = GetCategories()
            };

            return View(viewModel);
        }

        public IActionResult BlogDetail()
        {
            var viewModel = new BlogDetailViewModel
            {
                BestSellers = GetBestSellers(5),
                Categories = GetCategories()
            };

            return View(viewModel);
        }
        public IActionResult BlogDetail1()
        {
            var viewModel = new BlogDetailViewModel
            {
                BestSellers = GetBestSellers(5),
                Categories = GetCategories()
            };

            return View(viewModel);
        }
        public IActionResult BlogDetail2()
        {
            var viewModel = new BlogDetailViewModel
            {
                BestSellers = GetBestSellers(5),
                Categories = GetCategories()
            };

            return View(viewModel);
        }
        
        #region Supporting Methods

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
        #endregion

    }
}
