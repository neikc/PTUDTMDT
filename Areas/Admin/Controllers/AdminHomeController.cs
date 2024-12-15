using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PTUDTMDT.Areas.Admin.ViewModels;
using PTUDTMDT.Models;

namespace PTUDTMDT.Areas.Admin.Controllers
{


    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("Admin")]
    [Route("Admin/Home")]
    public class AdminHomeController : Controller
    {
        private readonly PtudtmdtContext _context;

        public AdminHomeController(PtudtmdtContext context)
        {
            _context = context;
        }

        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            var viewModel = new ReportViewModel
            {
                TotalOrders = GetTotalOrders(),
                TotalRevenue = GetTotalRevenue(),
                TotalProductsAdded = GetTotalProductsAdded(),
                TotalProductsSold = GetTotalProductsSold(),
                NewUsersThisMonth = GetNewUsersThisMonth()
            };

            return View(viewModel);
        }


        // Báo cáo tổng số đơn hàng
        private int GetTotalOrders()
        {
            return _context.DonHangs.Count();
        }

        // Báo cáo tổng doanh thu
        private double GetTotalRevenue()
        {
            return _context.ChiTietDonHangs
                .Sum(c => c.TongTienSp.GetValueOrDefault());  // Trả về 0 nếu TongTienSp là null
        }

        // Báo cáo tổng số sản phẩm đã thêm vào
        private int GetTotalProductsAdded()
        {
            return _context.SanPhams.Count();
        }

        // Báo cáo tổng số sản phẩm đã bán
        private int GetTotalProductsSold()
        {
            return _context.ChiTietDonHangs
                .Sum(c => c.SoLuong.GetValueOrDefault());  // Trả về 0 nếu SoLuong là null
        }

        // Báo cáo số người dùng mới trong tháng
        private int GetNewUsersThisMonth()
        {
            return _context.TaiKhoans
                .Where(t => t.NgayTaoTk.HasValue && t.NgayTaoTk.Value.Month == DateTime.Now.Month)
                .Count();
        }

    }
}

