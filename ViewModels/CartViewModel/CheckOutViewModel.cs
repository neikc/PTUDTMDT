using PTUDTMDT.Models;
using System.ComponentModel.DataAnnotations;

namespace PTUDTMDT.ViewModels.CartViewModel
{
    public class CheckOutViewModel
    {
        public IEnumerable<CartItemViewModel> ItemList { get; set; }
        public IEnumerable<SanPham> OnSale { get; set; } = new List<SanPham>();
        public IEnumerable<SanPham> BestSellers { get; set; } = new List<SanPham>();
        public IEnumerable<GiamGium> VoucherList { get; set; } = new List<GiamGium>();

        /**Các thuộc tính dành cho form**/
        // Thông tin cá nhân
        public string? HoTen { get; set; }

        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        public string? Email { get; set; }

        [Phone(ErrorMessage = "Số điện thoại không hợp lệ.")]
        public string? SoDienThoai { get; set; }

        public bool Checkbox { get; set; } = true;

        // Phương thức giao hàng
        public string? MaGiaoHang { get; set; }

        public string? DiaChi { get; set; }

        public string? GhiChu { get; set; }

        // Phương thức thanh toán
        public string? MaPttt { get; set; }

        // Dành cho phần áp dụng mã giảm giá
        public string? MaGiamGia { get; set; }
        public double? TongTienSauGiam { get; set; } // Tính tổng tiền sau khi giảm
    }
}
