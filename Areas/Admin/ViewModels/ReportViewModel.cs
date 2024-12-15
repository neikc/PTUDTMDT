namespace PTUDTMDT.Areas.Admin.ViewModels
{
    public class ReportViewModel
    {
        // Báo cáo đơn hàng
        public int TotalOrders { get; set; }
        public double TotalRevenue { get; set; }

        // Báo cáo sản phẩm
        public int TotalProductsAdded { get; set; }
        public int TotalProductsSold { get; set; }

        // Báo cáo người dùng
        public int NewUsersThisMonth { get; set; }
    }

}
