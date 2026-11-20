# Ca kiểm thử nghiệm thu do Ngọc Như đóng góp

Các ca dưới đây giúp kiểm tra giao diện web và một số thao tác trên ứng dụng Windows bằng dữ liệu mẫu của Fashion Shop. Mỗi ca có các bước, kết quả mong đợi và kết quả quan sát để dùng trong đợt nghiệm thu QA-02.

## Chuẩn bị

- Bật SQL Server và dùng cơ sở dữ liệu mẫu theo [hướng dẫn khởi động](../handbook/README.md#khởi-động-bản-demo). Cần ít nhất hai sản phẩm khớp từ khóa `ao`, trong đó có sản phẩm còn biến thể để thêm vào giỏ.
- Chạy `.\run-web.cmd` và mở `http://localhost:5193` trên một phiên trình duyệt mới. Chạy `.\run-desktop.cmd` và dùng tài khoản mẫu Admin và Staff trong [hướng dẫn đăng nhập](../handbook/README.md#đăng-nhập-và-xem-quyền) cho phần Windows.
- Khi chạy lại, ghi phiên bản, kết quả thực tế và trạng thái từng ca. Ghi rõ nếu thiếu dữ liệu hoặc có bước phụ thuộc vào bản tích hợp.

## Website

| Mã | Các bước | Kết quả mong đợi | Kết quả quan sát |
| --- | --- | --- | --- |
| NN-WEB-01 · Trang 404 và favicon | Mở `/qa02-khong-ton-tai`. Kiểm tra yêu cầu trang và tài nguyên favicon trong tab **Network** của công cụ trình duyệt. Bấm **Xem sản phẩm**. | Trang báo **Không tìm thấy trang**, yêu cầu trả HTTP 404, favicon SVG tải thành công và nút dẫn sang danh sách sản phẩm. | Đạt. HTTP 404 giữ nguyên trên trang tiếng Việt. Favicon trả HTTP 200 với kiểu `image/svg+xml`. Nút mở `/Products`. |
| NN-WEB-02 · Tìm kiếm và chuyển trang | Mở `/Products?keyword=ao&pageSize=1`. Ghi nhận sản phẩm đầu tiên rồi bấm **Tiếp**. | Từ khóa không dấu tìm được tên có dấu. Trang 2 giữ từ khóa và kích thước trang, đồng thời hiển thị sản phẩm khác trang 1. | Đạt. URL trang tiếp vẫn có `keyword=ao` và `pageSize=1`. Chỉ báo chuyển từ trang 1 sang trang 2 và liên kết sản phẩm đổi. |
| NN-WEB-03 · Thêm vào giỏ và mở đặt hàng | Mở một sản phẩm còn hàng. Chọn biến thể còn hàng và số lượng 1 rồi bấm **Thêm vào giỏ**. Mở **Giỏ hàng**, kiểm tra món vừa chọn và bấm **Tiến hành đặt hàng**. | Giỏ hiển thị đúng sản phẩm, size, màu và số lượng. Trang đặt hàng có thông tin người nhận, địa chỉ và lựa chọn thanh toán. | Đạt. Giỏ giữ đúng biến thể và số lượng. Ca kết thúc tại biểu mẫu đặt hàng. |
| NN-WEB-04 · Tra cứu mã đơn không tồn tại | Mở **Tra cứu đơn hàng**, nhập `HANDBOOK-DEMO` và `0900000000`, rồi bấm **Tra cứu đơn hàng**. Dùng cặp dữ liệu giả khác nếu cặp này đã có trong cơ sở dữ liệu thử. | Hiện thông báo **Không tìm thấy đơn hàng phù hợp** và giữ biểu mẫu để nhập lại. | Đạt. Trang hiển thị thông báo và vẫn dùng được biểu mẫu tra cứu. |

### Sản phẩm đã xem gần đây

Hai ca sau cần bản tích hợp hiển thị partial WEB-05 trên trang chi tiết. Kết quả ở cột cuối được ghi trên bản sao kiểm thử có gắn partial và kết nối SQL Server thật. Chạy lại hai ca trên `main` sau khi tích hợp WEB-05 vào trang chi tiết.

| Mã | Các bước | Kết quả mong đợi | Kết quả quan sát |
| --- | --- | --- | --- |
| NN-WEB-05 · Lịch sử và thứ tự xem | Dùng phiên trình duyệt mới. Mở lần lượt ba trang chi tiết khác nhau. Ở trang thứ ba, bấm thẻ của sản phẩm xem đầu tiên. Kiểm tra khóa `fashionshop.recentlyViewed` trong **Application → Local Storage**. Nếu dữ liệu có đủ, mở liên tiếp bảy sản phẩm khác nhau. | Lần xem đầu ẩn khối. Khối liệt kê các sản phẩm khác theo thứ tự xem gần nhất, với liên kết dẫn đúng trang. Xem lại đưa sản phẩm lên đầu lịch sử và chỉ lưu một lần. Lịch sử lưu tối đa sáu sản phẩm, tính cả trang đang xem. | Đạt trên bản sao kiểm thử. Đã kiểm tra giới hạn bằng cách nạp lịch sử mẫu có hơn sáu mục rồi mở trang chi tiết thật. |
| NN-WEB-06 · Bộ nhớ trình duyệt không dùng được | Trên bản tích hợp, kiểm tra hai tình huống. Thứ nhất, sửa khóa lịch sử thành chuỗi JSON không hợp lệ rồi tải lại trang. Thứ hai, mở trang bằng phiên thử nghiệm chặn truy cập `localStorage`. | Dữ liệu hỏng được bỏ qua và sản phẩm đang xem khởi tạo lịch sử mới. Khi bộ nhớ bị chặn, khối lịch sử ẩn và trang chi tiết vẫn sử dụng được. | Đạt trên bản sao kiểm thử. Không có lỗi JavaScript chưa xử lý trong hai lần thử. |

## Ứng dụng Windows

| Mã | Các bước | Kết quả mong đợi | Kết quả quan sát |
| --- | --- | --- | --- |
| NN-DES-01 · Đăng nhập Admin và xem sản phẩm | Đăng nhập bằng tài khoản Admin mẫu. Kiểm tra **Trang chủ**, sau đó mở **Quản lý sản phẩm**. | Cửa sổ hiển thị quyền Admin. Thẻ số sản phẩm đang kinh doanh và danh sách sản phẩm tải từ SQL Server. | Đạt. Dashboard có số sản phẩm đang kinh doanh và lưới sản phẩm có dữ liệu. |
| NN-DES-02 · Phân quyền Staff và chuẩn bị phiếu nhập | Đăng nhập bằng tài khoản Staff mẫu. Bấm **Quản lý danh mục**, đóng thông báo rồi mở **Phiếu nhập kho**. Chọn sản phẩm và biến thể, nhập số lượng 1, giá nhập `113000` và bấm **Thêm dòng**. | Mục danh mục báo **Không đủ quyền**. Staff vẫn mở được phiếu nhập, thêm được dòng vào bản nháp và thấy tổng một món trị giá 113.000 đ. | Đạt đến bước lập bản nháp. Ca kết thúc trước nút lưu phiếu. |

## Phiên bản và môi trường

- Bốn ca web đầu chạy bằng Chrome trên `main` tại `1d3c6e8` với SQL Server 2022 trong Docker.
- Kết quả Windows lấy từ lần chạy DOC-01 trên `fd3325e` và lần kiểm tra lại đến bước lập bản nháp. Mã WinForms ở hai phiên bản giống nhau. Bộ điều khiển thử nghiệm WinForms hết thời gian chờ ở một số lần chạy lại.
