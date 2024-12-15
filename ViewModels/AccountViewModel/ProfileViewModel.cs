using PTUDTMDT.Models;

namespace PTUDTMDT.ViewModels.AccountViewModel
{
    public class ProfileViewModel
    {
        public RegisterViewModel ThongTin { get; set; }
        public IEnumerable<DonHang> DonHangDangXuLy { get; set; }
        public IEnumerable<DonHang> DonHangDaXuLy { get; set; }
        public IEnumerable<DonHang> DonHangHoanTat { get; set; }
    }
}
