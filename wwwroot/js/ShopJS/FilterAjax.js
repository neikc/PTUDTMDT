$(document).ready(function () {
    // Gửi yêu cầu tìm kiếm qua Ajax
    $('#searchForm').submit(function (e) {
        e.preventDefault(); // Ngừng form submit mặc định

        var searchTerm = $('input[name="searchTerm"]').val(); // Lấy từ khóa tìm kiếm
        var priceRangeMin = $('#rangeInputMin').val(); // Lấy giá tối thiểu
        var priceRangeMax = $('#rangeInputMax').val(); // Lấy giá tối đa

        // Gửi yêu cầu Ajax để tìm kiếm và lọc sản phẩm
        $.ajax({
            url: '@Url.Action("ShopIndex", "Shop")', // Đường dẫn đến action trong controller
            type: 'GET',
            data: {
                searchTerm: searchTerm,
                priceRangeMin: priceRangeMin,
                priceRangeMax: priceRangeMax
            },
            success: function (data) {
                // Cập nhật lại phần sản phẩm trên trang (danh sách sản phẩm đã tìm kiếm và lọc)
                $('#productList').html(data); // Giả sử bạn có phần tử #productList để cập nhật kết quả
            }
        });
    });

    // Gửi yêu cầu khi thay đổi bộ lọc giá
    $('#filterForm').submit(function (e) {
        e.preventDefault(); // Ngừng form submit mặc định

        var priceRangeMin = $('#rangeInputMin').val(); // Lấy giá tối thiểu
        var priceRangeMax = $('#rangeInputMax').val(); // Lấy giá tối đa
        var searchTerm = $('input[name="searchTerm"]').val(); // Lấy từ khóa tìm kiếm

        // Gửi yêu cầu Ajax để lọc sản phẩm
        $.ajax({
            url: '@Url.Action("ShopIndex", "Shop")', // Đường dẫn đến action trong controller
            type: 'GET',
            data: {
                searchTerm: searchTerm,
                priceRangeMin: priceRangeMin,
                priceRangeMax: priceRangeMax
            },
            success: function (data) {
                // Cập nhật lại phần sản phẩm trên trang (danh sách sản phẩm đã lọc)
                $('#productList').html(data); // Giả sử bạn có phần tử #productList để cập nhật kết quả
            }
        });
    });
});
