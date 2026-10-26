# -*- coding: utf-8 -*-
"""
content_chuong3.py
==================
Nội dung Chương 3 – Phân tích và thiết kế hệ thống.
Số liệu, tên bảng, tên cột và tên hàm được đối chiếu với CSDL_ShopQuanAo.sql
và mã nguồn của cả hai phân hệ.
"""

BLOCKS = [

    ("h1", "CHƯƠNG 3: PHÂN TÍCH VÀ THIẾT KẾ HỆ THỐNG"),

    ("p",
     "Chương 3 là phần trọng tâm của báo cáo, chuyển từ mô tả “hệ thống làm được gì” sang mô tả “hệ thống được "
     "tổ chức như thế nào”. Nội dung được sắp theo đúng trình tự của một quy trình phân tích – thiết kế: bắt đầu "
     "từ nghiệp vụ (ai làm gì, theo thứ tự nào), tiếp đến chức năng (hệ thống phải cung cấp những gì), rồi đến "
     "dữ liệu (lưu gì, quan hệ nào, ràng buộc nào), và cuối cùng là giao diện – luồng màn hình. Mỗi bước đều "
     "dùng kết quả của bước trước làm đầu vào, nhờ vậy không có hạng mục nào xuất hiện trong thiết kế mà không "
     "có nguồn gốc từ một yêu cầu nghiệp vụ cụ thể."),

    # =================================================================== 3.1
    ("h2", "3.1. Phân tích nghiệp vụ"),

    ("p",
     "Ba quy trình nghiệp vụ dưới đây bao phủ toàn bộ vòng đời giá trị của hệ thống: nhập hàng vào kho, bán hàng "
     "cho khách, và xử lý đơn sau khi khách đặt. Ba quy trình này không độc lập hoàn toàn mà chồng lên nhau tại "
     "một điểm duy nhất là số lượng tồn kho của biến thể, nên việc phân tích phải chỉ rõ mỗi quy trình đọc và "
     "ghi tồn kho ở bước nào."),

    ("table", {
        "caption": "3.1: Tổng hợp ba quy trình nghiệp vụ chính của hệ thống",
        "cols": ["Quy trình", "Tác nhân", "Phân hệ", "Kết quả nghiệp vụ"],
        "widths": [4.0, 3.6, 3.0, 5.4],
        "font": 10.5,
        "bold_first_col": True,
        "rows": [
            ["QT1 – Quản lý danh mục và biến thể sản phẩm",
             "Nhân viên kho, quản trị viên", "Desktop",
             "Tồn kho theo tổ hợp màu – kích cỡ được tạo mới và cập nhật chính xác"],
            ["QT2 – Duyệt web, chọn biến thể và đặt hàng",
             "Khách hàng", "Web",
             "Đơn hàng được ghi nhận, tồn kho giảm tương ứng, khách nhận mã đơn"],
            ["QT3 – Xử lý và cập nhật trạng thái đơn hàng",
             "Nhân viên bán hàng", "Desktop",
             "Đơn đi đúng vòng đời; đơn hủy được hoàn kho; đơn hoàn thành được ghi nhận thanh toán"],
        ],
    }),

    ("h3", "3.1.1. Quy trình 1 – Quản lý danh mục và biến thể sản phẩm"),

    ("p",
     "Mục tiêu của quy trình là đưa một mẫu hàng mới từ trạng thái “có trong danh mục” sang trạng thái “bán được "
     "trên web”. Điều kiện để một mẫu hàng bán được không chỉ là có tên và giá, mà là có ít nhất một biến thể với "
     "số lượng tồn lớn hơn không. Vì vậy quy trình gồm hai tầng: tầng mẫu hàng (thuộc FormMain) và tầng biến thể "
     "(thuộc FormBienTheSanPham). Người thực hiện phải hoàn thành tầng thứ nhất trước, vì form biến thể cần một "
     "mã sản phẩm đã tồn tại để gắn các dòng tồn kho vào."),

    ("p",
     "Ở tầng mẫu hàng, nhân viên nhập mã sản phẩm, tên, giá bán, danh mục và đường dẫn ảnh. Hệ thống kiểm tra ba "
     "điều kiện: mã và tên không được rỗng, giá phải lớn hơn không, và mã sản phẩm không được trùng với bất kỳ "
     "sản phẩm nào khác. Điều kiện trùng mã được kiểm tra hai lần: một lần trong mã nguồn để hiển thị thông báo "
     "dễ hiểu, một lần bởi ràng buộc duy nhất ở cột ProductCode. Nếu chỉ dựa vào kiểm tra trong mã nguồn, hai "
     "nhân viên nhập cùng lúc có thể cùng vượt qua bước kiểm tra và tạo ra hai dòng trùng mã."),

    ("p",
     "Ở tầng biến thể, nhân viên chọn kích cỡ từ danh sách cố định, nhập màu và số lượng tồn. Danh sách kích cỡ "
     "được cố định trong mã nguồn gồm XS, S, M, L, XL, XXL và Free size. Việc cố định danh sách này là một đánh "
     "đổi có chủ ý: nó ngăn các lỗi chính tả kiểu “XL” và “Xl” bị coi là hai kích cỡ khác nhau, nhưng lại không "
     "phù hợp với một số mặt hàng dùng kích cỡ số như quần jeans. Trong dữ liệu mẫu, quần jeans dùng kích cỡ 29, "
     "30, 31, 32 – những giá trị này được nạp qua script chứ không nhập được từ giao diện. Đây là hạn chế được "
     "ghi nhận ở mục 3.4.7 và có hướng khắc phục rõ ràng: chuyển danh sách kích cỡ thành bảng dữ liệu riêng."),

    ("figure", {
        "caption": "3.1: Sơ đồ hoạt động quy trình nhập kho biến thể sản phẩm",
        "file": "diagrams/05_activity_nhap_kho_bienthe.mmd",
        "note": "Kiểm tra trùng tổ hợp màu – kích cỡ trước khi ghi; tồn kho không được âm",
        "height": 8.2,
    }),

    ("table", {
        "caption": "3.2: Các bước xử lý của quy trình nhập kho biến thể",
        "cols": ["Bước", "Hành động", "Kiểm tra / ràng buộc", "Dữ liệu phát sinh"],
        "widths": [1.4, 5.0, 5.6, 4.0],
        "font": 10.5,
        "aligns": ["c", "l", "l", "l"],
        "rows": [
            ["1", "Chọn sản phẩm cần nhập hàng trên lưới sản phẩm", "Sản phẩm phải đang ở trạng thái đang bán",
             "Không ghi dữ liệu"],
            ["2", "Mở form biến thể, hệ thống nạp danh sách biến thể hiện có",
             "Chỉ nạp biến thể thuộc sản phẩm đang chọn", "Đọc bảng ProductVariants"],
            ["3", "Chọn kích cỡ, nhập màu sắc, nhập số lượng tồn",
             "Kích cỡ thuộc danh sách cố định; màu không rỗng và không quá 50 ký tự; số lượng ≥ 0",
             "Không ghi dữ liệu"],
            ["4", "Nhấn thêm mới", "Tổ hợp màu – kích cỡ chưa tồn tại trong sản phẩm",
             "Thêm một dòng ProductVariants"],
            ["5", "Hoặc chọn một dòng và nhấn cập nhật",
             "Tổ hợp sau khi sửa không trùng dòng khác; có loại trừ chính dòng đang sửa",
             "Cập nhật số lượng hoặc thuộc tính"],
            ["6", "Hệ thống nạp lại danh sách biến thể", "Không có ngoại lệ", "Giao diện đồng bộ với dữ liệu"],
        ],
    }),

    ("h3", "3.1.2. Quy trình 2 – Khách hàng duyệt web, chọn biến thể và đặt hàng"),

    ("p",
     "Quy trình này là quy trình có nhiều điểm có thể thất bại nhất, vì nó phụ thuộc vào hành vi của người dùng "
     "bên ngoài và vào tình trạng tồn kho tại đúng thời điểm đặt hàng. Quy trình bắt đầu khi khách mở trang chủ "
     "hoặc trang danh sách sản phẩm. Trang danh sách cho phép tìm theo từ khóa và lọc theo danh mục; trang chi "
     "tiết mới là nơi khách chọn tổ hợp kích cỡ và màu cụ thể. Việc tách hai bước như vậy phản ánh đúng hành vi "
     "mua sắm: khách thu hẹp phạm vi trước, sau đó mới quyết định biến thể."),

    ("p",
     "Trên trang chi tiết, mỗi kích cỡ và mỗi màu được hiển thị thành nút chọn. Khi khách chọn, giao diện tìm "
     "trong danh sách biến thể đã nạp sẵn để xác định tổ hợp tương ứng và cập nhật giá trị của một thẻ chọn ẩn. "
     "Nếu một tổ hợp không tồn tại, giao diện hiển thị trạng thái không khả dụng. Điểm quan trọng là quyết định "
     "cuối cùng vẫn do máy chủ đưa ra: khi yêu cầu thêm vào giỏ được gửi đi, controller đọc lại biến thể từ cơ "
     "sở dữ liệu và kiểm tra tồn kho, không tin vào giá trị do trình duyệt gửi lên. Đây là nguyên tắc bắt buộc "
     "đối với mọi hệ thống thương mại điện tử, vì dữ liệu từ trình duyệt có thể bị sửa bằng công cụ phát triển."),

    ("p",
     "Khi khách nhấn đặt hàng, hệ thống thực hiện một chuỗi bước trong một giao dịch. Đầu tiên kiểm tra giỏ hàng "
     "không rỗng và biểu mẫu hợp lệ. Tiếp theo, gom các dòng hàng theo biến thể và sắp xếp theo mã biến thể để "
     "giảm khả năng khóa chéo. Với từng biến thể, hệ thống đọc tồn kho hiện tại, tính đơn giá, rồi trừ kho bằng "
     "câu lệnh có điều kiện. Nếu bất kỳ biến thể nào không đủ hàng, toàn bộ giao dịch bị hủy: không có đơn hàng "
     "nào được tạo và tồn kho của các biến thể đã trừ trước đó được khôi phục. Cuối cùng hệ thống ghi chi tiết "
     "đơn, cập nhật tổng tiền, và chỉ khi tất cả thành công mới xóa giỏ hàng của khách."),

    ("figure", {
        "caption": "3.2: Sơ đồ hoạt động quy trình đặt hàng trực tuyến",
        "file": "diagrams/06_activity_dat_hang_online.mmd",
        "note": "Toàn bộ bước trừ kho và ghi đơn nằm trong một giao dịch; lỗi ở bất kỳ bước nào đều hủy giao dịch",
        "height": 8.6,
    }),

    ("table", {
        "caption": "3.3: Các bước xử lý khi khách đặt hàng trực tuyến",
        "cols": ["Bước", "Hành động", "Xử lý khi thành công", "Xử lý khi thất bại"],
        "widths": [1.4, 4.6, 5.0, 5.0],
        "font": 10.5,
        "aligns": ["c", "l", "l", "l"],
        "rows": [
            ["1", "Khách xác nhận giỏ hàng và mở trang thanh toán",
             "Hiển thị biểu mẫu nhận hàng", "Giỏ rỗng thì chuyển về trang giỏ hàng"],
            ["2", "Khách nhập họ tên, số điện thoại, địa chỉ, ghi chú",
             "Dữ liệu được kiểm tra theo DataAnnotations", "Hiển thị lỗi tại từng ô, giữ nguyên dữ liệu đã nhập"],
            ["3", "Khách chọn phương thức thanh toán",
             "Chấp nhận COD hoặc chuyển khoản", "Phương thức không hỗ trợ thì từ chối và thông báo"],
            ["4", "Hệ thống mở giao dịch và ghi nhận khách hàng",
             "Tạo mới hoặc dùng lại khách theo số điện thoại", "Ngoại lệ thì hủy giao dịch và báo lỗi"],
            ["5", "Hệ thống tạo đơn và sinh mã đơn",
             "Mã đơn duy nhất, dạng DH kèm thời gian", "Trùng mã thì thử lại, tối đa ba lần"],
            ["6", "Hệ thống trừ kho từng biến thể",
             "Số dòng bị tác động bằng 1", "Bằng 0 thì ném ngoại lệ và hủy toàn bộ giao dịch"],
            ["7", "Hệ thống ghi chi tiết đơn và tổng tiền",
             "Đơn ở trạng thái Chờ xử lý", "Lỗi thì hủy giao dịch, tồn kho được khôi phục"],
            ["8", "Xóa giỏ hàng và chuyển sang trang cảm ơn",
             "Khách thấy mã đơn để tra cứu", "Không xảy ra vì đã nằm ngoài giao dịch"],
        ],
    }),

    ("h3", "3.1.3. Quy trình 3 – Xử lý và cập nhật trạng thái đơn hàng"),

    ("p",
     "Sau khi đơn được tạo trên web, đơn nằm ở trạng thái Chờ xử lý và chờ nhân viên tác động. Quy trình xử lý "
     "đơn trên desktop được thiết kế theo nguyên tắc “chỉ cho phép bước đi hợp lệ”: mỗi trạng thái có một tập "
     "trạng thái kế tiếp được định nghĩa trước, và mọi yêu cầu chuyển trạng thái nằm ngoài tập đó đều bị từ chối "
     "ngay trên giao diện. Cách làm này ngăn được các lỗi nghiệp vụ như chuyển một đơn đã hủy sang Hoàn thành, "
     "hoặc bỏ qua bước Đang giao để nhảy từ Đã duyệt sang Hoàn thành."),

    ("p",
     "Bảng chuyển trạng thái dưới đây là đặc tả chính thức của quy trình. Cột “Tác động dữ liệu” cho thấy mỗi "
     "bước chuyển không chỉ thay đổi một trường mà có thể kéo theo thay đổi tồn kho hoặc cờ thanh toán. Đặc biệt, "
     "bước chuyển sang Đã hủy là bước duy nhất làm tăng tồn kho, và bước chuyển sang Hoàn thành là bước duy nhất "
     "tự động đặt cờ đã thanh toán cho đơn giao hàng thu tiền tận nơi."),

    ("table", {
        "caption": "3.4: Ma trận chuyển trạng thái đơn hàng và tác động dữ liệu",
        "cols": ["Trạng thái hiện tại", "Được chuyển sang", "Điều kiện", "Tác động dữ liệu"],
        "widths": [3.4, 4.0, 4.0, 4.6],
        "font": 10.5,
        "bold_first_col": True,
        "rows": [
            ["Chờ xử lý", "Đã duyệt", "Nhân viên xác nhận đơn hợp lệ",
             "Cập nhật Status, không đổi tồn kho vì đã trừ khi tạo đơn"],
            ["Chờ xử lý", "Đã hủy", "Đơn không liên lạc được hoặc khách yêu cầu hủy",
             "Cập nhật Status và hoàn lại tồn kho cho từng biến thể trong đơn"],
            ["Đã duyệt", "Đang giao", "Đơn đã được đóng gói và bàn giao vận chuyển",
             "Cập nhật Status"],
            ["Đã duyệt", "Đã hủy", "Khách đổi ý trước khi giao",
             "Cập nhật Status và hoàn lại tồn kho"],
            ["Đang giao", "Hoàn thành", "Khách đã nhận hàng",
             "Cập nhật Status và đặt IsPaid bằng 1"],
            ["Hoàn thành", "–", "Trạng thái kết thúc", "Không cho phép chuyển tiếp"],
            ["Đã hủy", "–", "Trạng thái kết thúc", "Không cho phép chuyển tiếp, kể cả đánh dấu thanh toán"],
        ],
    }),

    ("p",
     "Về kỹ thuật, mỗi lần chuyển trạng thái được thực hiện bằng một câu lệnh cập nhật có điều kiện trạng thái "
     "nguồn. Nếu số dòng bị tác động bằng không, nghĩa là đơn đã bị người khác xử lý trong khoảng thời gian giữa "
     "lúc nạp danh sách và lúc bấm nút; khi đó hệ thống thông báo và nạp lại danh sách thay vì ghi đè. Đây cùng "
     "một nguyên tắc với quy trình 1: điều kiện nghiệp vụ được đưa vào câu lệnh để trạng thái cuối cùng luôn "
     "nhất quán với dữ liệu thực tế."),

    ("figure", {
        "caption": "3.3: Sơ đồ hoạt động quy trình xử lý đơn hàng trên ứng dụng quản trị",
        "file": "diagrams/07_activity_xuly_don_hang.mmd",
        "note": "Kiểm tra trạng thái kế tiếp hợp lệ; hoàn kho khi hủy; ghi nhận thanh toán khi hoàn thành",
        "height": 8.0,
    }),

    # =================================================================== 3.2
    ("h2", "3.2. Phân tích chức năng"),

    ("h3", "3.2.1. Tác nhân và phạm vi sử dụng"),

    ("p",
     "Hệ thống có ba tác nhân. Khách hàng là tác nhân bên ngoài, không cần tài khoản, chỉ tương tác qua trình "
     "duyệt. Nhân viên bán hàng và quản trị viên là tác nhân bên trong, đăng nhập vào ứng dụng desktop; hai vai "
     "trò này chia sẻ phần lớn chức năng, chỉ khác nhau ở quyền quản lý danh mục. Việc chỉ có hai vai trò nội bộ "
     "là phù hợp với quy mô cửa hàng nhỏ, nhưng thiết kế dữ liệu đã tách bảng Roles ra riêng để có thể bổ sung "
     "vai trò mới mà không phải sửa cấu trúc."),

    ("table", {
        "caption": "3.5: Danh sách tác nhân và quyền hạn tương ứng",
        "cols": ["Tác nhân", "Loại", "Phương thức truy cập", "Quyền hạn chính"],
        "widths": [3.0, 2.4, 4.0, 6.6],
        "font": 10.5,
        "bold_first_col": True,
        "rows": [
            ["Khách hàng", "Bên ngoài", "Trình duyệt, website cổng 5193",
             "Duyệt sản phẩm, tìm kiếm, chọn biến thể, quản lý giỏ hàng, đặt hàng, tra cứu đơn"],
            ["Nhân viên bán hàng", "Bên trong", "Ứng dụng WinForms",
             "Xem dashboard, quản lý sản phẩm và biến thể, xử lý đơn hàng, đánh dấu thanh toán"],
            ["Quản trị viên", "Bên trong", "Ứng dụng WinForms",
             "Toàn bộ quyền của nhân viên, thêm quản lý danh mục và ngừng dùng danh mục"],
            ["Hệ thống", "Tác nhân phụ", "–",
             "Sinh mã đơn, kiểm tra tồn kho, hoàn kho khi hủy đơn, giới hạn trạng thái kế tiếp"],
        ],
    }),

    ("p",
     "Sơ đồ Use Case tổng quan dưới đây đặt ba tác nhân bên ngoài đường bao hệ thống và nhóm các chức năng theo "
     "phân hệ. Cách vẽ này giúp thấy ngay rằng chỉ có một điểm chung giữa hai phân hệ là dữ liệu, còn về mặt "
     "chức năng thì chúng gần như không giao nhau. Đây là hệ quả trực tiếp của kiến trúc đã trình bày ở Chương 1."),

    ("figure", {
        "caption": "3.4: Sơ đồ Use Case tổng quan của hệ thống",
        "file": "diagrams/08_usecase_tongquan.mmd",
        "note": "Ba tác nhân: Khách hàng, Nhân viên bán hàng, Quản trị viên",
        "height": 7.6,
    }),

    ("table", {
        "caption": "3.6: Danh mục Use Case của hệ thống",
        "cols": ["Mã", "Tên Use Case", "Tác nhân", "Phân hệ", "Mức ưu tiên"],
        "widths": [1.4, 6.0, 3.4, 2.6, 2.6],
        "font": 10.5,
        "aligns": ["c", "l", "l", "c", "c"],
        "rows": [
            ["UC00", "Đăng nhập và phân quyền quản trị", "Nhân viên, Quản trị viên", "Desktop", "Cao"],
            ["UC01", "Quản lý biến thể sản phẩm thời trang", "Nhân viên, Quản trị viên", "Desktop", "Cao"],
            ["UC02", "Quản lý và xử lý đơn hàng", "Nhân viên, Quản trị viên", "Desktop", "Cao"],
            ["UC03", "Tìm kiếm và lọc sản phẩm thời trang", "Khách hàng", "Web", "Cao"],
            ["UC04", "Đặt hàng trực tuyến", "Khách hàng", "Web", "Cao"],
            ["UC05", "Tra cứu tình trạng đơn hàng", "Khách hàng", "Web", "Trung bình"],
            ["UC06", "Quản lý danh mục sản phẩm", "Quản trị viên", "Desktop", "Trung bình"],
            ["UC07", "Quản lý sản phẩm và trạng thái kinh doanh", "Nhân viên, Quản trị viên", "Desktop", "Cao"],
            ["UC08", "Xem dashboard tổng quan", "Nhân viên, Quản trị viên", "Desktop", "Thấp"],
        ],
    }),

    ("h3", "3.2.2. Use Case chi tiết của phân hệ Desktop"),

    ("p",
     "Sơ đồ Use Case phân hệ desktop cho thấy cấu trúc điều hướng của ứng dụng: từ đăng nhập, người dùng vào form "
     "chính, từ đó mở các form nghiệp vụ. Quan hệ bao gồm (include) thể hiện rằng mọi chức năng quản lý đều cần "
     "dữ liệu đã được nạp lại sau mỗi thao tác ghi, còn quan hệ mở rộng (extend) thể hiện các nhánh xử lý lỗi "
     "như trùng mã sản phẩm hay chuyển trạng thái không hợp lệ."),

    ("figure", {
        "caption": "3.5: Sơ đồ Use Case phân hệ quản trị Desktop",
        "file": "diagrams/09_usecase_winforms.mmd",
        "note": "Từ đăng nhập đến các form nghiệp vụ; nhánh mở rộng thể hiện xử lý lỗi",
        "height": 7.4,
    }),

    ("h3", "3.2.3. Use Case chi tiết của phân hệ Web"),

    ("p",
     "Sơ đồ Use Case phân hệ web có một đặc điểm khác biệt: tác nhân khách hàng không đăng nhập, nên mọi chức "
     "năng phải tự bảo vệ bằng dữ liệu đầu vào chứ không dựa vào phiên đăng nhập. Điều này giải thích vì sao chức "
     "năng tra cứu đơn hàng yêu cầu đồng thời mã đơn và số điện thoại, và vì sao mọi action thay đổi dữ liệu đều "
     "kiểm tra lại tồn kho từ cơ sở dữ liệu."),

    ("figure", {
        "caption": "3.6: Sơ đồ Use Case phân hệ bán hàng trên Web",
        "file": "diagrams/10_usecase_web.mmd",
        "note": "Khách hàng không cần tài khoản; giỏ hàng lưu trong phiên làm việc",
        "height": 7.2,
    }),

    ("h3", "3.2.4. Đặc tả chi tiết các Use Case trọng yếu"),

    ("p",
     "Bốn Use Case dưới đây được đặc tả đầy đủ theo cấu trúc thống nhất: thông tin chung, tiền điều kiện, hậu "
     "điều kiện, luồng sự kiện chính và luồng ngoại lệ. Cách đặc tả này đủ để lập trình viên hiện thực và đủ để "
     "người kiểm thử viết kịch bản mà không cần hỏi lại."),

    ("table", {
        "caption": "3.7: Đặc tả UC01 – Quản lý biến thể sản phẩm thời trang",
        "cols": ["Thuộc tính", "Nội dung"],
        "widths": [3.4, 12.6],
        "font": 11,
        "bold_first_col": True,
        "rows": [
            ["Mã / Tên", "UC01 – Quản lý biến thể sản phẩm thời trang"],
            ["Tác nhân", "Nhân viên bán hàng, Quản trị viên"],
            ["Mô tả", "Tạo mới và cập nhật tổ hợp màu sắc – kích cỡ kèm số lượng tồn của một sản phẩm đã tồn tại"],
            ["Tiền điều kiện", "Đã đăng nhập; sản phẩm cha tồn tại và đang ở trạng thái đang bán"],
            ["Hậu điều kiện",
             "Biến thể mới xuất hiện trong bảng ProductVariants và hiển thị trên trang chi tiết sản phẩm của web"],
            ["Luồng chính",
             "1. Chọn sản phẩm trên lưới và nhấn quản lý biến thể.\n"
             "2. Hệ thống nạp danh sách biến thể hiện có của sản phẩm.\n"
             "3. Nhập kích cỡ, màu sắc, số lượng tồn.\n"
             "4. Nhấn thêm mới hoặc cập nhật.\n"
             "5. Hệ thống kiểm tra dữ liệu và tổ hợp trùng.\n"
             "6. Hệ thống ghi dữ liệu và nạp lại lưới."],
            ["Luồng ngoại lệ",
             "3a. Kích cỡ hoặc màu để trống: thông báo yêu cầu nhập đầy đủ.\n"
             "3b. Màu dài quá 50 ký tự: thông báo giới hạn độ dài.\n"
             "5a. Tổ hợp màu – kích cỡ đã tồn tại: thông báo trùng, không ghi dữ liệu.\n"
             "5b. Vi phạm ràng buộc duy nhất ở cơ sở dữ liệu: hiển thị thông báo trùng và nạp lại dữ liệu.\n"
             "6a. Mất kết nối cơ sở dữ liệu: thông báo lỗi, dữ liệu cũ được giữ nguyên."],
            ["Quy tắc nghiệp vụ",
             "Mỗi sản phẩm chỉ có một biến thể cho mỗi cặp kích cỡ và màu; số lượng tồn luôn lớn hơn hoặc bằng 0"],
            ["Tần suất", "Vài lần mỗi ngày, tăng đột biến khi nhập hàng mùa mới"],
        ],
    }),

    ("table", {
        "caption": "3.8: Đặc tả UC02 – Quản lý và xử lý đơn hàng",
        "cols": ["Thuộc tính", "Nội dung"],
        "widths": [3.4, 12.6],
        "font": 11,
        "bold_first_col": True,
        "rows": [
            ["Mã / Tên", "UC02 – Quản lý và xử lý đơn hàng"],
            ["Tác nhân", "Nhân viên bán hàng, Quản trị viên"],
            ["Mô tả",
             "Theo dõi đơn mới, xem chi tiết, chuyển trạng thái theo vòng đời, đánh dấu thanh toán"],
            ["Tiền điều kiện", "Đã đăng nhập; tồn tại ít nhất một đơn hàng ở trạng thái Chờ xử lý"],
            ["Hậu điều kiện",
             "Trạng thái đơn được cập nhật đúng vòng đời; nếu hủy đơn thì tồn kho được hoàn lại; "
             "nếu hoàn thành thì cờ thanh toán được đặt"],
            ["Luồng chính",
             "1. Mở form đơn hàng, hệ thống nạp danh sách đơn.\n"
             "2. Lọc theo từ khóa hoặc theo trạng thái nếu cần.\n"
             "3. Chọn một đơn, hệ thống hiển thị chi tiết mặt hàng.\n"
             "4. Nhấn duyệt đơn, chọn trạng thái đích.\n"
             "5. Hệ thống kiểm tra trạng thái đích có hợp lệ.\n"
             "6. Hệ thống cập nhật theo trạng thái nguồn, hoàn kho nếu hủy.\n"
             "7. Hệ thống nạp lại danh sách, giữ nguyên bộ lọc."],
            ["Luồng ngoại lệ",
             "3a. Đơn không có chi tiết: hiển thị thông báo không có mặt hàng.\n"
             "5a. Trạng thái đích không hợp lệ: thông báo và giữ nguyên đơn.\n"
             "6a. Đơn đã bị người khác xử lý: số dòng cập nhật bằng 0, thông báo và nạp lại.\n"
             "6b. Hoàn kho cho đơn đã hủy: chỉ thực hiện một lần, không hoàn lặp."],
            ["Quy tắc nghiệp vụ",
             "Chờ xử lý → Đã duyệt hoặc Đã hủy; Đã duyệt → Đang giao hoặc Đã hủy; Đang giao → Hoàn thành; "
             "Hoàn thành và Đã hủy là trạng thái kết thúc"],
            ["Tần suất", "Nhiều lần mỗi ngày, cao điểm vào buổi tối"],
        ],
    }),

    ("table", {
        "caption": "3.9: Đặc tả UC03 – Tìm kiếm và lọc sản phẩm thời trang theo kích cỡ, màu sắc",
        "cols": ["Thuộc tính", "Nội dung"],
        "widths": [3.4, 12.6],
        "font": 11,
        "bold_first_col": True,
        "rows": [
            ["Mã / Tên", "UC03 – Tìm kiếm và lọc sản phẩm thời trang"],
            ["Tác nhân", "Khách hàng"],
            ["Mô tả",
             "Tìm sản phẩm theo từ khóa, lọc theo danh mục, sau đó chọn kích cỡ và màu cụ thể ở trang chi tiết"],
            ["Tiền điều kiện", "Không cần đăng nhập; có ít nhất một sản phẩm đang bán"],
            ["Hậu điều kiện", "Danh sách kết quả được hiển thị; biến thể được chọn sẵn sàng để thêm vào giỏ"],
            ["Luồng chính",
             "1. Khách mở trang danh sách sản phẩm.\n"
             "2. Nhập từ khóa hoặc chọn một danh mục.\n"
             "3. Hệ thống truy vấn và hiển thị kết quả kèm số lượng.\n"
             "4. Khách mở trang chi tiết sản phẩm.\n"
             "5. Khách chọn kích cỡ, sau đó chọn màu.\n"
             "6. Hệ thống xác định tổ hợp biến thể và hiển thị tình trạng tồn kho."],
            ["Luồng ngoại lệ",
             "2a. Từ khóa không có kết quả: hiển thị trạng thái rỗng kèm gợi ý xóa bộ lọc.\n"
             "3a. Danh mục không tồn tại: bỏ điều kiện lọc, hiển thị toàn bộ sản phẩm.\n"
             "5a. Tổ hợp kích cỡ và màu không tồn tại: hiển thị không khả dụng, không cho thêm vào giỏ.\n"
             "5b. Tổ hợp tồn tại nhưng hết hàng: hiển thị nhãn hết hàng và vô hiệu hóa nút mua."],
            ["Quy tắc nghiệp vụ",
             "Tìm kiếm không phân biệt dấu tiếng Việt; chỉ hiển thị sản phẩm đang bán; "
             "sản phẩm có tổng tồn bằng 0 vẫn hiển thị nhưng ở trạng thái hết hàng"],
            ["Tần suất", "Rất cao, là luồng truy cập chính của website"],
        ],
    }),

    ("table", {
        "caption": "3.10: Đặc tả UC04 – Đặt hàng trực tuyến",
        "cols": ["Thuộc tính", "Nội dung"],
        "widths": [3.4, 12.6],
        "font": 11,
        "bold_first_col": True,
        "rows": [
            ["Mã / Tên", "UC04 – Đặt hàng trực tuyến"],
            ["Tác nhân", "Khách hàng"],
            ["Mô tả", "Xác nhận giỏ hàng, cung cấp thông tin nhận hàng và tạo đơn hàng trong cơ sở dữ liệu"],
            ["Tiền điều kiện", "Giỏ hàng có ít nhất một dòng; các biến thể trong giỏ còn tồn kho"],
            ["Hậu điều kiện",
             "Một đơn hàng mới ở trạng thái Chờ xử lý được tạo cùng chi tiết đơn; tồn kho giảm tương ứng; "
             "giỏ hàng được xóa; khách nhận mã đơn"],
            ["Luồng chính",
             "1. Khách mở trang thanh toán từ giỏ hàng.\n"
             "2. Nhập họ tên, số điện thoại, địa chỉ, ghi chú và chọn phương thức thanh toán.\n"
             "3. Nhấn đặt hàng.\n"
             "4. Hệ thống kiểm tra dữ liệu biểu mẫu và phương thức thanh toán.\n"
             "5. Hệ thống mở giao dịch, ghi khách hàng, tạo đơn và chi tiết.\n"
             "6. Hệ thống trừ kho từng biến thể và cập nhật tổng tiền.\n"
             "7. Giao dịch được xác nhận, giỏ hàng bị xóa.\n"
             "8. Khách được chuyển tới trang cảm ơn kèm mã đơn."],
            ["Luồng ngoại lệ",
             "1a. Giỏ rỗng: chuyển về trang giỏ hàng kèm thông báo.\n"
             "4a. Dữ liệu không hợp lệ: hiển thị lỗi từng ô, giữ dữ liệu đã nhập.\n"
             "4b. Số điện thoại sai định dạng: yêu cầu nhập lại theo mẫu bắt đầu bằng số 0.\n"
             "6a. Một biến thể không đủ hàng: hủy toàn bộ giao dịch, thông báo không đủ hàng.\n"
             "6b. Trùng mã đơn: sinh lại mã, tối đa ba lần; nếu vẫn trùng thì báo lỗi.\n"
             "6c. Lỗi cơ sở dữ liệu giữa chừng: hủy giao dịch, tồn kho không thay đổi."],
            ["Quy tắc nghiệp vụ",
             "Không bán vượt tồn kho; tổng tiền bằng tổng của số lượng nhân đơn giá từng dòng; "
             "mã đơn là duy nhất"],
            ["Tần suất", "Hằng ngày, là mục tiêu cuối cùng của toàn bộ hệ thống"],
        ],
    }),

    ("p",
     "Use Case thứ năm – tra cứu đơn hàng – được đặc tả ngắn hơn vì luồng xử lý đơn giản, nhưng có một điểm thiết "
     "kế đáng lưu ý về bảo mật: hệ thống không cho phép tra cứu chỉ bằng mã đơn. Nếu chỉ cần mã đơn, một người có "
     "thể dò tuần tự các mã theo mẫu và xem được thông tin của người khác. Việc yêu cầu thêm số điện thoại buộc "
     "người tra cứu phải biết một thông tin không công khai, đồng thời không gây khó khăn cho khách thật vì họ "
     "vừa nhập số điện thoại khi đặt hàng."),

    ("table", {
        "caption": "3.11: Đặc tả UC05 – Tra cứu tình trạng đơn hàng",
        "cols": ["Thuộc tính", "Nội dung"],
        "widths": [3.4, 12.6],
        "font": 11,
        "bold_first_col": True,
        "rows": [
            ["Mã / Tên", "UC05 – Tra cứu tình trạng đơn hàng"],
            ["Tác nhân", "Khách hàng"],
            ["Mô tả", "Tra cứu đơn đã đặt theo mã đơn kết hợp số điện thoại, xem tiến trình bốn bước"],
            ["Tiền điều kiện", "Khách đã có mã đơn do hệ thống sinh khi đặt hàng thành công"],
            ["Hậu điều kiện", "Hiển thị thông tin đơn, mặt hàng, trạng thái và tiến trình xử lý"],
            ["Luồng chính",
             "1. Khách mở trang tra cứu đơn hàng.\n"
             "2. Nhập mã đơn và số điện thoại đã dùng khi đặt.\n"
             "3. Hệ thống tìm đơn khớp cả hai thông tin.\n"
             "4. Hệ thống hiển thị chi tiết và thanh tiến trình trạng thái."],
            ["Luồng ngoại lệ",
             "2a. Thiếu một trong hai thông tin: yêu cầu nhập đủ.\n"
             "3a. Không tìm thấy đơn: thông báo chung, không tiết lộ mã đơn có tồn tại hay không.\n"
             "3b. Đơn ở trạng thái Đã hủy: hiển thị nhãn hủy thay cho thanh tiến trình."],
            ["Quy tắc nghiệp vụ", "Bắt buộc khớp đồng thời mã đơn và số điện thoại; không phân biệt khoảng trắng trong số điện thoại"],
            ["Tần suất", "Trung bình, thường dùng trong hai ngày sau khi đặt hàng"],
        ],
    }),

    ("h3", "3.2.5. Danh sách User Story theo chuẩn Agile"),

    ("p",
     "Product Backlog của đồ án được viết dưới dạng User Story để mỗi hạng mục gắn với một nhu cầu thực của người "
     "dùng thay vì một đầu việc kỹ thuật. Mỗi câu chuyện được ước lượng bằng điểm công sức tương đối và được gắn "
     "điều kiện nghiệm thu để có thể kiểm tra khách quan khi kết thúc Sprint."),

    ("table", {
        "caption": "3.12: Product Backlog – Danh sách User Story của hệ thống",
        "cols": ["Mã", "Với tư cách là", "Tôi muốn", "Để", "Điểm"],
        "widths": [1.4, 2.8, 6.6, 4.2, 1.0],
        "font": 10,
        "aligns": ["c", "l", "l", "l", "c"],
        "rows": [
            ["US01", "Nhân viên", "đăng nhập bằng tài khoản được cấp",
             "không ai khác dùng được phần mềm quản trị", "3"],
            ["US02", "Quản trị viên", "xem số sản phẩm, số đơn và doanh thu trong ngày",
             "nắm tình hình ngay khi mở phần mềm", "3"],
            ["US03", "Quản trị viên", "thêm và ngừng dùng danh mục thời trang",
             "sản phẩm được phân loại đúng nhóm", "2"],
            ["US04", "Nhân viên", "thêm sản phẩm mới với mã, tên, giá, ảnh",
             "đưa mẫu hàng mới lên kệ", "3"],
            ["US05", "Nhân viên", "sửa giá và thông tin sản phẩm",
             "phản ánh đúng giá bán hiện hành", "2"],
            ["US06", "Nhân viên", "ngừng bán sản phẩm mà không xóa dữ liệu",
             "đơn hàng cũ vẫn giữ được tham chiếu", "2"],
            ["US07", "Nhân viên kho", "nhập tồn kho theo từng cặp màu và kích cỡ",
             "biết chính xác còn bao nhiêu chiếc mỗi loại", "5"],
            ["US08", "Nhân viên kho", "được cảnh báo khi nhập trùng tổ hợp màu và kích cỡ",
             "không tạo ra hai dòng tồn kho cho cùng một loại hàng", "3"],
            ["US09", "Khách hàng", "tìm sản phẩm bằng từ khóa tiếng Việt không dấu",
             "không phải gõ đúng dấu mới tìm được", "3"],
            ["US10", "Khách hàng", "lọc sản phẩm theo loại thời trang",
             "thu hẹp phạm vi khi cửa hàng có nhiều mẫu", "2"],
            ["US11", "Khách hàng", "chọn kích cỡ và màu ngay trên trang sản phẩm",
             "biết tổ hợp mình cần còn hàng hay không", "5"],
            ["US12", "Khách hàng", "thêm sản phẩm vào giỏ hàng và đổi số lượng",
             "mua nhiều món trong một lần", "3"],
            ["US13", "Khách hàng", "đặt hàng mà không cần tạo tài khoản",
             "việc mua sắm không bị chậm lại", "3"],
            ["US14", "Khách hàng", "nhận mã đơn ngay sau khi đặt",
             "có căn cứ để tra cứu và đối chiếu sau này", "2"],
            ["US15", "Khách hàng", "tra cứu trạng thái đơn bằng mã đơn và số điện thoại",
             "biết đơn đang ở bước nào", "3"],
            ["US16", "Nhân viên", "xem và lọc danh sách đơn theo trạng thái",
             "xử lý đơn mới trước", "3"],
            ["US17", "Nhân viên", "chuyển trạng thái đơn theo đúng quy trình",
             "không xảy ra đơn nhảy bước hoặc đơn hủy bị hoàn thành", "5"],
            ["US18", "Nhân viên", "tồn kho được tự động hoàn lại khi hủy đơn",
             "số liệu kho không bị mất oan", "5"],
            ["US19", "Nhân viên", "đánh dấu đơn đã thanh toán",
             "đối chiếu được tiền đã thu", "2"],
            ["US20", "Khách hàng", "hệ thống không cho mua vượt tồn kho",
             "không bị hủy đơn sau khi đã chờ giao", "5"],
        ],
    }),

    ("table", {
        "caption": "3.13: Ánh xạ User Story vào Sprint và điều kiện nghiệm thu",
        "cols": ["Sprint", "User Story", "Điều kiện nghiệm thu chính"],
        "widths": [2.6, 4.4, 9.0],
        "font": 10.5,
        "aligns": ["c", "l", "l"],
        "rows": [
            ["Sprint 1", "US01, US02",
             "Đăng nhập sai bị từ chối; tài khoản bị vô hiệu hóa không đăng nhập được; dashboard hiển thị đúng số liệu trong ngày"],
            ["Sprint 2", "US03 – US08",
             "Tạo được 30 biến thể mẫu; nhập trùng tổ hợp bị chặn ở cả giao diện và cơ sở dữ liệu; sản phẩm ngừng bán không hiện trên web"],
            ["Sprint 3", "US09 – US14, US20",
             "Tìm không dấu ra kết quả; chọn được biến thể; đặt hàng thành công tạo đơn và giảm tồn kho; đặt vượt tồn bị chặn và không tạo đơn"],
            ["Sprint 4", "US15 – US19",
             "Tra cứu đúng khi khớp cả hai thông tin; chuyển trạng thái sai bị chặn; hủy đơn hoàn đúng số lượng; hoàn thành tự đánh dấu thanh toán"],
        ],
    }),

    # =================================================================== 3.3
    ("h2", "3.3. Thiết kế cơ sở dữ liệu"),

    ("p",
     "Cơ sở dữ liệu gồm tám bảng, được thiết kế theo hướng chuẩn hóa đến dạng chuẩn thứ ba: mỗi bảng mô tả một "
     "loại thực thể, không có thuộc tính lặp, và mọi phụ thuộc hàm đều phụ thuộc vào khóa chính. Riêng bảng chi "
     "tiết đơn hàng cố tình lưu lặp lại một số thông tin như tên sản phẩm, kích cỡ và màu. Đây không phải lỗi "
     "chuẩn hóa mà là quyết định có chủ ý: chi tiết đơn hàng là chứng từ, phải giữ đúng thông tin tại thời điểm "
     "mua, kể cả khi sản phẩm sau này đổi tên hoặc đổi giá."),

    ("h3", "3.3.1. Lược đồ thực thể mối quan hệ"),

    ("p",
     "Sơ đồ ERD dưới đây thể hiện các quan hệ chính: một danh mục có nhiều sản phẩm; một sản phẩm có nhiều biến "
     "thể; một khách hàng có nhiều đơn; một đơn có nhiều chi tiết; mỗi chi tiết trỏ tới một sản phẩm và có thể "
     "trỏ tới một biến thể cụ thể. Quan hệ giữa chi tiết đơn và biến thể được để ở dạng tùy chọn (nullable) vì "
     "chi tiết đơn phải tồn tại được ngay cả khi biến thể đã bị xóa khỏi danh mục bán."),

    ("figure", {
        "caption": "3.7: Lược đồ thực thể mối quan hệ (ERD) của cơ sở dữ liệu FashionShopDb",
        "file": "diagrams/11_erd.mmd",
        "note": "Tám bảng, khóa chính, khóa ngoại và ràng buộc duy nhất trên tổ hợp sản phẩm – kích cỡ – màu",
        "height": 9.0,
    }),

    ("table", {
        "caption": "3.14: Danh mục bảng dữ liệu và ánh xạ với tên nghiệp vụ tiếng Việt",
        "cols": ["Tên bảng vật lý", "Tên nghiệp vụ", "Vai trò", "Số cột"],
        "widths": [3.8, 4.4, 6.4, 1.4],
        "font": 10.5,
        "bold_first_col": True,
        "aligns": ["l", "l", "l", "c"],
        "rows": [
            ["Roles", "Vai trò / Quyền hạn", "Phân quyền cho tài khoản quản trị", "3"],
            ["Users", "Người dùng / Tài khoản", "Tài khoản đăng nhập của nhân viên và quản trị viên", "9"],
            ["Categories", "Danh mục sản phẩm", "Loại thời trang: áo thun, sơ mi, quần jeans, áo khoác, váy đầm", "5"],
            ["Products", "Sản phẩm", "Mẫu hàng ở cấp thiết kế: mã, tên, giá, ảnh, danh mục", "11"],
            ["ProductVariants", "Biến thể sản phẩm", "Tổ hợp kích cỡ – màu sắc kèm số lượng tồn", "5"],
            ["Customers", "Khách hàng", "Thông tin người mua, được nhận diện theo số điện thoại", "6"],
            ["Orders", "Đơn hàng", "Đầu đơn: mã đơn, người nhận, tổng tiền, trạng thái, thanh toán", "12"],
            ["OrderDetails", "Chi tiết đơn hàng", "Các dòng hàng của đơn, lưu kèm thông tin tại thời điểm mua", "10"],
        ],
    }),

    ("h3", "3.3.2. Thiết kế vật lý các bảng"),

    ("p",
     "Các bảng dưới đây trình bày thiết kế vật lý theo đúng thứ tự tạo trong script CSDL_ShopQuanAo.sql. Cột "
     "“Khóa” ghi PK cho khóa chính, FK cho khóa ngoại, UK cho ràng buộc duy nhất; cột “Null” ghi YES nếu cho phép "
     "giá trị rỗng. Việc đọc theo thứ tự này cũng là thứ tự đúng khi triển khai, vì bảng sau tham chiếu bảng trước."),

    ("table", {
        "caption": "3.15: Thiết kế bảng Roles – vai trò người dùng",
        "cols": ["Cột", "Kiểu dữ liệu", "Khóa", "Null", "Mô tả"],
        "widths": [4.2, 3.4, 1.6, 1.4, 5.4],
        "font": 10,
        "rows": [
            ["RoleId", "INT IDENTITY", "PK", "NO", "Khóa chính, tự tăng"],
            ["RoleName", "NVARCHAR(50)", "–", "NO", "Tên vai trò: Admin hoặc Staff"],
            ["Description", "NVARCHAR(200)", "–", "YES", "Mô tả phạm vi quyền của vai trò"],
        ],
    }),

    ("table", {
        "caption": "3.16: Thiết kế bảng Users – tài khoản quản trị và nhân viên",
        "cols": ["Cột", "Kiểu dữ liệu", "Khóa", "Null", "Mô tả"],
        "widths": [4.2, 3.4, 1.6, 1.4, 5.4],
        "font": 10,
        "rows": [
            ["UserId", "INT IDENTITY", "PK", "NO", "Khóa chính, tự tăng"],
            ["Username", "NVARCHAR(50)", "UK", "NO", "Tên đăng nhập, không trùng"],
            ["PasswordHash", "NVARCHAR(255)", "–", "NO", "Mật khẩu phục vụ đăng nhập của đồ án minh họa"],
            ["FullName", "NVARCHAR(100)", "–", "NO", "Họ tên hiển thị trên thanh tiêu đề"],
            ["Email", "NVARCHAR(100)", "–", "YES", "Thư điện tử liên hệ"],
            ["PhoneNumber", "NVARCHAR(20)", "–", "YES", "Số điện thoại liên hệ"],
            ["RoleId", "INT", "FK → Roles", "NO", "Vai trò quyết định quyền sử dụng"],
            ["IsActive", "BIT, mặc định 1", "–", "NO", "Tài khoản còn hiệu lực hay đã bị khóa"],
            ["CreatedAt", "DATETIME, mặc định GETDATE()", "–", "NO", "Thời điểm tạo tài khoản"],
        ],
    }),

    ("table", {
        "caption": "3.17: Thiết kế bảng Categories – danh mục loại thời trang",
        "cols": ["Cột", "Kiểu dữ liệu", "Khóa", "Null", "Mô tả"],
        "widths": [4.2, 3.4, 1.6, 1.4, 5.4],
        "font": 10,
        "rows": [
            ["CategoryId", "INT IDENTITY", "PK", "NO", "Khóa chính, tự tăng"],
            ["Name", "NVARCHAR(100)", "–", "NO", "Tên loại: áo thun, sơ mi, quần jeans…"],
            ["Description", "NVARCHAR(255)", "–", "YES", "Mô tả ngắn dùng cho phần giới thiệu danh mục"],
            ["ImageUrl", "NVARCHAR(500)", "–", "YES", "Ảnh đại diện của danh mục"],
            ["IsActive", "BIT, mặc định 1", "–", "NO", "Danh mục còn dùng hay đã ngừng"],
        ],
    }),

    ("table", {
        "caption": "3.18: Thiết kế bảng Products – sản phẩm ở cấp mẫu hàng",
        "cols": ["Cột", "Kiểu dữ liệu", "Khóa", "Null", "Mô tả"],
        "widths": [4.2, 3.4, 1.6, 1.4, 5.4],
        "font": 10,
        "rows": [
            ["ProductId", "INT IDENTITY", "PK", "NO", "Khóa chính, tự tăng"],
            ["ProductCode", "NVARCHAR(50)", "UK", "NO", "Mã sản phẩm: AT-001, SM-002, QJ-003…"],
            ["Name", "NVARCHAR(200)", "–", "NO", "Tên sản phẩm hiển thị trên web"],
            ["Description", "NVARCHAR(MAX)", "–", "YES", "Mô tả chất liệu, kiểu dáng"],
            ["Price", "DECIMAL(18,2)", "–", "NO", "Giá bán hiện hành, ràng buộc lớn hơn 0"],
            ["OriginalPrice", "DECIMAL(18,2)", "–", "YES", "Giá gốc để tính phần trăm giảm giá"],
            ["ImageUrl", "NVARCHAR(500)", "–", "YES", "Ảnh đại diện sản phẩm"],
            ["CategoryId", "INT", "FK → Categories", "NO", "Danh mục mà sản phẩm thuộc về"],
            ["IsActive", "BIT, mặc định 1", "–", "NO", "Còn bán hay đã ngừng bán (xóa mềm)"],
            ["IsFeatured", "BIT, mặc định 0", "–", "NO", "Đánh dấu để hiển thị ở trang chủ"],
            ["CreatedAt", "DATETIME, mặc định GETDATE()", "–", "NO", "Thời điểm tạo sản phẩm"],
        ],
    }),

    ("table", {
        "caption": "3.19: Thiết kế bảng ProductVariants – biến thể kích cỡ và màu sắc",
        "cols": ["Cột", "Kiểu dữ liệu", "Khóa", "Null", "Mô tả"],
        "widths": [4.2, 3.4, 1.6, 1.4, 5.4],
        "font": 10,
        "rows": [
            ["ProductVariantId", "INT IDENTITY", "PK", "NO", "Khóa chính, tự tăng"],
            ["ProductId", "INT", "FK → Products", "NO", "Sản phẩm cha; xóa sản phẩm sẽ xóa biến thể theo"],
            ["Size", "NVARCHAR(20)", "UK*", "NO", "Kích cỡ: XS, S, M, L, XL, XXL, Free size hoặc cỡ số"],
            ["Color", "NVARCHAR(50)", "UK*", "NO", "Màu sắc: Trắng, Đen, Be, Xanh đậm, Hồng…"],
            ["StockQuantity", "INT, mặc định 0", "–", "NO", "Số lượng tồn, ràng buộc lớn hơn hoặc bằng 0"],
        ],
    }),

    ("p",
     "Dấu * ở cột khóa của bảng ProductVariants có nghĩa ràng buộc duy nhất không đặt trên một cột riêng lẻ mà "
     "trên tổ hợp ba cột ProductId, Size và Color. Cách đặt này cho phép hai sản phẩm khác nhau cùng có biến thể "
     "M – Trắng, nhưng không cho phép một sản phẩm có hai dòng M – Trắng. Đây là ràng buộc cốt lõi bảo vệ toàn "
     "bộ mô hình tồn kho của đồ án."),

    ("table", {
        "caption": "3.20: Thiết kế bảng Customers – thông tin khách hàng",
        "cols": ["Cột", "Kiểu dữ liệu", "Khóa", "Null", "Mô tả"],
        "widths": [4.2, 3.4, 1.6, 1.4, 5.4],
        "font": 10,
        "rows": [
            ["CustomerId", "INT IDENTITY", "PK", "NO", "Khóa chính, tự tăng"],
            ["FullName", "NVARCHAR(100)", "–", "NO", "Họ tên khách hàng"],
            ["PhoneNumber", "NVARCHAR(20)", "–", "NO", "Số điện thoại, dùng để nhận diện khách quay lại"],
            ["Email", "NVARCHAR(100)", "–", "YES", "Thư điện tử nếu khách cung cấp"],
            ["Address", "NVARCHAR(255)", "–", "YES", "Địa chỉ gợi nhớ từ lần mua gần nhất"],
            ["CreatedAt", "DATETIME, mặc định GETDATE()", "–", "NO", "Thời điểm khách phát sinh lần đầu"],
        ],
    }),

    ("table", {
        "caption": "3.21: Thiết kế bảng Orders – đơn hàng",
        "cols": ["Cột", "Kiểu dữ liệu", "Khóa", "Null", "Mô tả"],
        "widths": [4.2, 3.4, 1.6, 1.4, 5.4],
        "font": 10,
        "rows": [
            ["OrderId", "INT IDENTITY", "PK", "NO", "Khóa chính, tự tăng"],
            ["OrderCode", "NVARCHAR(30)", "UK", "NO", "Mã đơn dạng DH kèm thời gian và số ngẫu nhiên"],
            ["OrderDate", "DATETIME, mặc định GETDATE()", "–", "NO", "Thời điểm đặt hàng"],
            ["CustomerId", "INT", "FK → Customers", "YES", "Khách hàng tương ứng nếu đã nhận diện"],
            ["CustomerName", "NVARCHAR(100)", "–", "NO", "Tên người nhận tại thời điểm đặt"],
            ["CustomerPhone", "NVARCHAR(20)", "–", "NO", "Số điện thoại người nhận"],
            ["ShippingAddress", "NVARCHAR(255)", "–", "NO", "Địa chỉ giao hàng"],
            ["Note", "NVARCHAR(500)", "–", "YES", "Ghi chú của khách"],
            ["TotalAmount", "DECIMAL(18,2)", "–", "NO", "Tổng tiền của đơn, không âm"],
            ["Status", "NVARCHAR(50)", "–", "NO", "Chờ xử lý, Đã duyệt, Đang giao, Hoàn thành, Đã hủy"],
            ["PaymentMethod", "NVARCHAR(50)", "–", "NO", "COD, Tiền mặt hoặc Chuyển khoản"],
            ["IsPaid", "BIT, mặc định 0", "–", "NO", "Đơn đã được ghi nhận thanh toán hay chưa"],
        ],
    }),

    ("table", {
        "caption": "3.22: Thiết kế bảng OrderDetails – chi tiết đơn hàng",
        "cols": ["Cột", "Kiểu dữ liệu", "Khóa", "Null", "Mô tả"],
        "widths": [4.2, 3.4, 1.6, 1.4, 5.4],
        "font": 10,
        "rows": [
            ["OrderDetailId", "INT IDENTITY", "PK", "NO", "Khóa chính, tự tăng"],
            ["OrderId", "INT", "FK → Orders", "NO", "Đơn hàng chứa dòng chi tiết"],
            ["ProductId", "INT", "FK → Products", "NO", "Sản phẩm được mua"],
            ["ProductVariantId", "INT", "–", "YES", "Biến thể cụ thể; để trống nếu biến thể đã bị xóa"],
            ["ProductName", "NVARCHAR(200)", "–", "NO", "Tên sản phẩm tại thời điểm mua (chứng từ)"],
            ["Size", "NVARCHAR(20)", "–", "YES", "Kích cỡ tại thời điểm mua"],
            ["Color", "NVARCHAR(50)", "–", "YES", "Màu sắc tại thời điểm mua"],
            ["Quantity", "INT", "–", "NO", "Số lượng mua, ràng buộc lớn hơn 0"],
            ["UnitPrice", "DECIMAL(18,2)", "–", "NO", "Đơn giá tại thời điểm mua"],
            ["TotalPrice", "DECIMAL(18,2)", "–", "NO", "Thành tiền của dòng"],
        ],
    }),

    ("h3", "3.3.3. Ràng buộc toàn vẹn và chỉ mục"),

    ("p",
     "Ràng buộc toàn vẹn là phần dễ bị xem nhẹ nhất khi thiết kế cơ sở dữ liệu nhưng lại là phần quyết định chất "
     "lượng dữ liệu lâu dài. Trong đồ án, mỗi quy tắc nghiệp vụ quan trọng đều được hiện thực ít nhất ở một ràng "
     "buộc cấp cơ sở dữ liệu, vì đây là lớp bảo vệ duy nhất không thể bị bỏ qua bởi bất kỳ chương trình nào kết "
     "nối vào, kể cả công cụ quản trị bên ngoài ứng dụng."),

    ("table", {
        "caption": "3.23: Danh sách ràng buộc CHECK và UNIQUE trong cơ sở dữ liệu",
        "cols": ["Tên ràng buộc", "Bảng", "Biểu thức / quy tắc", "Ý nghĩa nghiệp vụ"],
        "widths": [4.6, 2.6, 5.4, 3.4],
        "font": 10,
        "bold_first_col": True,
        "rows": [
            ["CK_Products_Price", "Products", "Price > 0", "Không bán sản phẩm với giá bằng 0 hoặc âm"],
            ["CK_Products_OriginalPrice", "Products", "OriginalPrice IS NULL OR OriginalPrice >= 0",
             "Giá gốc nếu có phải không âm"],
            ["CK_ProductVariants_StockQuantity", "ProductVariants", "StockQuantity >= 0",
             "Tồn kho không bao giờ âm, chặn bán vượt tồn"],
            ["CK_Orders_TotalAmount", "Orders", "TotalAmount >= 0", "Tổng tiền đơn không âm"],
            ["CK_Orders_Status", "Orders",
             "Status IN (N’Chờ xử lý’, N’Đã duyệt’, N’Đang giao’, N’Hoàn thành’, N’Đã hủy’)",
             "Chỉ nhận trạng thái thuộc vòng đời của hệ thống"],
            ["CK_Orders_PaymentMethod", "Orders",
             "PaymentMethod IN (N’COD’, N’Tiền mặt’, N’Chuyển khoản’)",
             "Không lưu phương thức thanh toán ngoài danh mục hỗ trợ"],
            ["CK_OrderDetails_Quantity", "OrderDetails", "Quantity > 0", "Mỗi dòng hàng phải có số lượng dương"],
            ["CK_OrderDetails_UnitPrice", "OrderDetails", "UnitPrice >= 0", "Đơn giá không âm"],
            ["CK_OrderDetails_TotalPrice", "OrderDetails", "TotalPrice >= 0", "Thành tiền không âm"],
            ["UQ_ProductVariants_Product_Size_Color", "ProductVariants",
             "UNIQUE (ProductId, Size, Color)", "Mỗi sản phẩm chỉ có một biến thể cho mỗi cặp kích cỡ và màu"],
        ],
    }),

    ("p",
     "Về chỉ mục, đồ án không đặt chỉ mục tràn lan mà chọn theo đúng ba truy vấn chạy thường xuyên nhất: lọc sản "
     "phẩm theo danh mục, lọc đơn theo trạng thái và ngày, và tra cứu đơn theo số điện thoại. Việc dùng chỉ mục "
     "có cột bao gồm (INCLUDE) giúp câu truy vấn lấy được thêm cột mà không phải quay lại bảng gốc, giảm chi phí "
     "đọc đáng kể khi dữ liệu tăng."),

    ("table", {
        "caption": "3.24: Danh sách chỉ mục và mục đích tối ưu",
        "cols": ["Tên chỉ mục", "Bảng và cột", "Loại", "Mục đích"],
        "widths": [5.0, 5.2, 2.0, 3.8],
        "font": 10,
        "bold_first_col": True,
        "rows": [
            ["UQ_ProductVariants_Product_Size_Color", "ProductVariants (ProductId, Size, Color)", "Duy nhất",
             "Bảo vệ quy tắc một tổ hợp chỉ có một dòng; đồng thời tăng tốc tra cứu biến thể"],
            ["IX_ProductVariants_ProductId", "ProductVariants (ProductId)", "Không duy nhất",
             "Nạp danh sách biến thể của một sản phẩm"],
            ["IX_Products_CategoryId", "Products (CategoryId) INCLUDE (IsActive)", "Không duy nhất",
             "Lọc sản phẩm theo danh mục trên web mà không phải đọc bảng gốc"],
            ["IX_Orders_Status_OrderDate", "Orders (Status, OrderDate DESC)", "Không duy nhất",
             "Lọc đơn theo trạng thái và sắp xếp mới nhất trước"],
            ["IX_Orders_CustomerPhone", "Orders (CustomerPhone)", "Không duy nhất",
             "Tra cứu đơn theo số điện thoại, cũng là nhận diện khách quay lại"],
            ["IX_OrderDetails_OrderId", "OrderDetails (OrderId) INCLUDE (ProductId, Quantity)", "Không duy nhất",
             "Nạp chi tiết đơn và tính lại tồn kho khi hoàn hàng"],
        ],
    }),

    ("p",
     "Ngoài bảng và chỉ mục, cơ sở dữ liệu còn có view vw_ProductStock tổng hợp tồn kho theo sản phẩm bằng cách "
     "cộng số lượng của toàn bộ biến thể. View này là nguồn duy nhất cho cột tồn kho tổng trên lưới sản phẩm, nhờ "
     "vậy con số “còn bao nhiêu” ở mọi màn hình đều được tính theo cùng một công thức."),

    # =================================================================== 3.4
    ("h2", "3.4. Thiết kế giao diện và luồng màn hình"),

    ("p",
     "Thiết kế giao diện của đồ án đặt hai mục tiêu khác nhau cho hai phân hệ. Với web, mục tiêu là giảm số bước "
     "để khách hoàn tất một đơn hàng và làm rõ tình trạng còn hàng ngay tại chỗ chọn biến thể. Với desktop, mục "
     "tiêu là giảm số lần di chuyển chuột của nhân viên: mọi thao tác thường dùng phải nằm trong một màn hình, "
     "không mở hộp thoại phụ nếu không cần thiết."),

    ("h3", "3.4.1. Luồng màn hình của website"),

    ("p",
     "Luồng màn hình web gồm năm màn hình chính: trang chủ, danh sách sản phẩm, chi tiết sản phẩm, giỏ hàng và "
     "thanh toán, kết thúc ở trang cảm ơn có mã đơn. Trang tra cứu đơn hàng là một nhánh phụ, có thể truy cập từ "
     "thanh điều hướng ở bất kỳ thời điểm nào. Điểm cần lưu ý là khách có thể đi từ trang chủ tới trang chi tiết "
     "mà không cần qua danh sách, nhờ khối sản phẩm nổi bật và các liên kết danh mục; điều này rút ngắn đường đi "
     "cho khách đã biết mình muốn gì."),

    ("figure", {
        "caption": "3.8: Sơ đồ luồng màn hình của website bán hàng",
        "file": "diagrams/12_screenflow_web.mmd",
        "note": "Năm màn hình chính và nhánh tra cứu đơn hàng truy cập được từ mọi trang",
        "height": 7.4,
    }),

    ("table", {
        "caption": "3.25: Các màn hình web, điểm vào và hành động chính",
        "cols": ["Màn hình", "Route", "Điểm vào", "Hành động chính"],
        "widths": [3.2, 4.0, 4.4, 4.4],
        "font": 10,
        "bold_first_col": True,
        "rows": [
            ["Trang chủ", "/", "Mở website, logo", "Xem sản phẩm nổi bật, đi tới danh mục, tìm kiếm"],
            ["Danh sách sản phẩm", "/Products", "Menu sản phẩm, chip danh mục", "Tìm theo từ khóa, lọc danh mục, mở chi tiết"],
            ["Chi tiết sản phẩm", "/Products/Detail/{id}", "Thẻ sản phẩm", "Chọn kích cỡ, chọn màu, chọn số lượng, thêm vào giỏ"],
            ["Giỏ hàng", "/Cart", "Biểu tượng giỏ trên header", "Đổi số lượng, xóa dòng, sang thanh toán"],
            ["Thanh toán", "/Checkout", "Nút tiến hành đặt hàng", "Nhập thông tin nhận hàng, chọn thanh toán, đặt hàng"],
            ["Cảm ơn", "/Checkout/Success", "Sau khi đặt thành công", "Đọc mã đơn, sang trang tra cứu"],
            ["Tra cứu đơn", "/tra-cuu-don-hang", "Menu tra cứu", "Nhập mã đơn và số điện thoại, xem tiến trình"],
        ],
    }),

    ("h3", "3.4.2. Luồng màn hình của ứng dụng quản trị"),

    ("p",
     "Luồng màn hình desktop có dạng hình cây với gốc là form chính. Sau khi đăng nhập, người dùng luôn quay về "
     "form chính sau khi đóng bất kỳ form nghiệp vụ nào, nhờ vậy không bị mất ngữ cảnh và không phải đăng nhập "
     "lại. Đây là khác biệt quan trọng so với thiết kế mở nhiều form song song: cách mở tuần tự giúp tránh tình "
     "trạng hai form cùng sửa một bản ghi."),

    ("figure", {
        "caption": "3.9: Sơ đồ luồng màn hình của ứng dụng quản trị Desktop",
        "file": "diagrams/13_screenflow_winforms.mmd",
        "note": "Form1 là cửa ngõ; FormMain là trung tâm điều hướng tới ba form nghiệp vụ",
        "height": 6.8,
    }),

    ("table", {
        "caption": "3.26: Các form desktop, điều kiện mở và hành động chính",
        "cols": ["Form", "Mở từ", "Điều kiện", "Hành động chính"],
        "widths": [3.4, 3.2, 4.4, 5.0],
        "font": 10,
        "bold_first_col": True,
        "rows": [
            ["Form1", "Khởi động ứng dụng", "Luôn mở đầu tiên",
             "Đăng nhập; thoát ứng dụng; hiện lại sau khi form chính đóng"],
            ["FormMain", "Đăng nhập thành công", "Tài khoản đang hoạt động",
             "Xem dashboard; thêm, sửa, ngừng bán sản phẩm; tìm kiếm; mở form khác; đăng xuất"],
            ["FormDanhMuc", "Nút quản lý danh mục", "Chỉ vai trò Admin",
             "Thêm, sửa, ngừng dùng danh mục; tìm theo tên"],
            ["FormBienTheSanPham", "Nút quản lý biến thể", "Đã chọn một sản phẩm",
             "Thêm và cập nhật tổ hợp màu – kích cỡ, số lượng tồn"],
            ["FormDonHang", "Nút quản lý đơn hàng", "Đã đăng nhập",
             "Lọc danh sách, xem chi tiết, chuyển trạng thái, đánh dấu thanh toán"],
        ],
    }),

    ("h3", "3.4.3. Bố cục giao diện quản trị"),

    ("p",
     "Giao diện desktop dùng bố cục hai vùng: vùng điều hướng bên trái và vùng nội dung bên phải. Vùng điều hướng "
     "chứa tên cửa hàng, các nút chức năng và nút đăng xuất; vùng nội dung thay đổi theo chức năng đang chọn. "
     "Cách bố trí này giữ nguyên vị trí các nút điều hướng trong suốt phiên làm việc, giúp nhân viên hình thành "
     "phản xạ vị trí và thao tác nhanh hơn so với giao diện dạng nhiều tab."),

    ("p",
     "Riêng màn hình quản lý sản phẩm dùng bố cục ba phần: bên trái là lưới sản phẩm, bên phải là vùng nhập liệu "
     "theo nhóm thông tin, phía trên là thanh tìm kiếm và các nút thao tác. Bố cục này cho phép nhân viên vừa "
     "nhìn danh sách vừa sửa thông tin mà không phải mở thêm cửa sổ. Các nút thao tác được đặt cùng hàng với ô "
     "tìm kiếm để giảm quãng di chuyển chuột giữa hai khu vực."),

    ("table", {
        "caption": "3.27: Bố cục các vùng chức năng trên giao diện quản trị",
        "cols": ["Khu vực", "Thành phần", "Vai trò trong luồng thao tác"],
        "widths": [3.6, 5.6, 6.8],
        "font": 10.5,
        "bold_first_col": True,
        "rows": [
            ["Thanh thương hiệu", "Tên cửa hàng, phụ đề, tên người đăng nhập và vai trò",
             "Cho biết đang ở đúng hệ thống và đúng quyền"],
            ["Vùng điều hướng", "Năm nút chức năng và nút đăng xuất",
             "Chuyển giữa dashboard, sản phẩm, đơn hàng, danh mục"],
            ["Thẻ số liệu", "Ba thẻ: sản phẩm đang bán, đơn trong ngày, doanh thu trong ngày",
             "Nắm tình hình ngay khi đăng nhập, không cần mở báo cáo"],
            ["Lưới dữ liệu", "Bảng sản phẩm hoặc đơn hàng, có cột trạng thái tô màu",
             "Chọn bản ghi để xem hoặc thao tác; màu giúp nhận biết trạng thái tức thời"],
            ["Vùng nhập liệu", "Các ô văn bản, ô số, combobox theo nhóm thông tin",
             "Nhập và sửa dữ liệu, có nhãn rõ ràng cho từng trường"],
            ["Thanh hành động", "Nút làm mới, thêm mới, cập nhật, tìm kiếm, mở form con",
             "Thực hiện thao tác; thứ tự nút phản ánh tần suất sử dụng"],
            ["Thanh trạng thái", "Thông báo kết quả thao tác",
             "Xác nhận thao tác đã thành công hoặc nêu lý do thất bại"],
        ],
    }),

    ("h3", "3.4.4. Bố cục giao diện website"),

    ("p",
     "Website dùng một layout chung gồm thanh thông báo trên cùng, header với logo, ô tìm kiếm, liên kết tra cứu "
     "và biểu tượng giỏ hàng có số lượng, thanh điều hướng chính và footer chia ba cột. Việc giữ nguyên header "
     "trên mọi trang giúp khách luôn thấy giỏ hàng của mình, đồng thời tạo cảm giác liền mạch khi chuyển giữa "
     "các bước mua hàng."),

    ("p",
     "Trang chi tiết sản phẩm là màn hình được đầu tư nhiều nhất. Phần trên chia hai cột: cột trái là ảnh sản phẩm "
     "kèm nhãn giảm giá, cột phải gồm tên, giá, nhãn tình trạng tồn kho, bộ chọn kích cỡ, bộ chọn màu, bộ chọn số "
     "lượng và hai nút hành động. Bộ chọn kích cỡ hiển thị dạng các ô chữ nhật, bộ chọn màu hiển thị dạng ô màu "
     "tròn kèm tên màu. Phần dưới là mô tả chi tiết và các cam kết đổi trả, giao hàng. Khi khách cuộn xuống, một "
     "thanh mua hàng cố định xuất hiện ở cuối màn hình để không phải cuộn ngược lên."),

    ("table", {
        "caption": "3.28: Bố cục các khối giao diện chính trên website",
        "cols": ["Khối giao diện", "Thành phần", "Mục đích thiết kế"],
        "widths": [4.0, 6.0, 6.0],
        "font": 10.5,
        "bold_first_col": True,
        "rows": [
            ["Thanh thông báo", "Dòng chữ khuyến mãi hoặc chính sách giao hàng",
             "Truyền đạt thông tin thương mại mà không chiếm diện tích nội dung"],
            ["Header", "Logo, ô tìm kiếm, liên kết tra cứu, giỏ hàng kèm số lượng",
             "Điểm truy cập nhanh tới ba chức năng dùng nhiều nhất"],
            ["Khối hero", "Tiêu đề lớn, mô tả ngắn, nút kêu gọi hành động",
             "Định hướng phong cách thương hiệu và dẫn vào danh sách sản phẩm"],
            ["Danh mục", "Các thẻ danh mục có ảnh đại diện",
             "Cho khách chọn theo phong cách thay vì phải tìm bằng từ khóa"],
            ["Lưới sản phẩm", "Thẻ sản phẩm gồm ảnh, tên, giá, nhãn tồn kho, phần trăm giảm",
             "So sánh nhanh nhiều sản phẩm trong cùng một màn hình"],
            ["Bộ chọn biến thể", "Ô chọn kích cỡ, ô chọn màu, thẻ chọn ẩn",
             "Chọn đúng tổ hợp trước khi thêm vào giỏ, tránh phải chọn lại"],
            ["Giỏ hàng", "Bảng dòng hàng, ô số lượng, tổng tiền, nút thanh toán",
             "Kiểm tra lại đơn trước khi chuyển sang nhập thông tin"],
            ["Thanh toán", "Biểu mẫu nhận hàng, lựa chọn thanh toán, tóm tắt đơn",
             "Nhập một lần, thấy ngay tổng tiền phải trả"],
            ["Tra cứu đơn", "Biểu mẫu hai trường, thanh tiến trình bốn bước",
             "Cho khách biết đơn đang ở đâu mà không cần gọi điện"],
        ],
    }),

    ("h3", "3.4.5. Triển khai hệ thống"),

    ("p",
     "Sơ đồ triển khai dưới đây mô tả cách các thành phần được bố trí trên máy. Cơ sở dữ liệu chạy trong container "
     "với volume lưu trữ bền vững và healthcheck định kỳ; hai script khởi tạo và migration được gắn vào container "
     "ở chế độ chỉ đọc để không thể bị sửa từ bên trong. Website chạy trên Kestrel ở cổng 5193, còn ứng dụng "
     "desktop là một tiến trình độc lập đọc mật khẩu cơ sở dữ liệu từ biến môi trường."),

    ("figure", {
        "caption": "3.10: Sơ đồ triển khai hệ thống",
        "file": "diagrams/14_deployment.mmd",
        "note": "SQL Server trong Docker với volume bền vững; hai ứng dụng kết nối qua cổng 1433 chỉ trên loopback",
        "height": 6.4,
    }),

    ("h3", "3.4.6. Kết quả kiểm thử chức năng"),

    ("p",
     "Kiểm thử được thực hiện trên cơ sở dữ liệu thật trong Docker, không dùng dữ liệu giả lập, vì phần lớn rủi ro "
     "của hệ thống nằm ở tương tác giữa giao dịch và ràng buộc dữ liệu. Bảng dưới đây ghi lại các kịch bản quan "
     "trọng nhất cùng kết quả thực tế, bao gồm cả các kịch bản được thiết kế để thất bại."),

    ("table", {
        "caption": "3.29: Kết quả kiểm thử các kịch bản nghiệp vụ quan trọng",
        "cols": ["Mã", "Kịch bản", "Dữ liệu đầu vào", "Kết quả mong đợi", "Kết quả thực tế"],
        "widths": [1.2, 4.4, 3.4, 4.0, 3.0],
        "font": 9.5,
        "aligns": ["c", "l", "l", "l", "l"],
        "rows": [
            ["TC01", "Thêm biến thể mới cho sản phẩm", "Áo thun, S, Trắng, 25",
             "Ghi thành công, lưới có thêm dòng", "Đạt"],
            ["TC02", "Thêm biến thể trùng tổ hợp", "Áo thun, S, Trắng",
             "Bị chặn, thông báo trùng", "Đạt"],
            ["TC03", "Đặt hàng với tồn kho đủ", "Áo thun M – Trắng, số lượng 2, tồn 18",
             "Tạo đơn 378.000 đồng, tồn còn 16", "Đạt"],
            ["TC04", "Đặt hàng vượt tồn kho", "Số lượng 5, tồn 3",
             "Không tạo đơn, tồn không đổi", "Đạt"],
            ["TC05", "Đặt hai đơn liên tiếp cho món cuối cùng", "Tồn 1, hai đơn cùng biến thể",
             "Chỉ một đơn thành công", "Đạt"],
            ["TC06", "Chuyển trạng thái không hợp lệ", "Đang giao → Đã duyệt",
             "Bị chặn, giữ nguyên trạng thái", "Đạt"],
            ["TC07", "Hủy đơn đã duyệt", "Đơn 2 dòng hàng",
             "Hoàn kho đúng số lượng từng dòng", "Đạt"],
            ["TC08", "Duyệt đơn đã bị người khác xử lý", "Trạng thái đã đổi trước khi bấm",
             "Thông báo và nạp lại danh sách", "Đạt"],
            ["TC09", "Hoàn thành đơn giao hàng", "Đơn đang giao",
             "Trạng thái Hoàn thành, tự đánh dấu thanh toán", "Đạt"],
            ["TC10", "Tra cứu đơn sai số điện thoại", "Đúng mã đơn, sai số điện thoại",
             "Không tìm thấy, không tiết lộ thông tin", "Đạt"],
            ["TC11", "Tìm kiếm không dấu", "Từ khóa “ao so mi”",
             "Trả về sản phẩm áo sơ mi", "Đạt"],
            ["TC12", "Đăng nhập tài khoản bị khóa", "Tài khoản có IsActive bằng 0",
             "Từ chối đăng nhập", "Đạt"],
        ],
    }),

    ("p",
     "Hai kịch bản TC04 và TC05 là hai kịch bản có giá trị nhất về mặt kỹ thuật. TC04 kiểm chứng rằng điều kiện "
     "trừ kho trong câu lệnh cập nhật thực sự hoạt động, không chỉ là kiểm tra ở giao diện. TC05 kiểm chứng tính "
     "cô lập của giao dịch khi có hai yêu cầu đồng thời, đây là kịch bản mà phương pháp kiểm thử thủ công thông "
     "thường bỏ qua nhưng lại là nguyên nhân phổ biến gây sai lệch tồn kho trong hệ thống thương mại điện tử."),

    ("h3", "3.4.7. Hạn chế của hệ thống và hướng phát triển"),

    ("p",
     "Một báo cáo trung thực phải nêu rõ những gì hệ thống chưa làm được. Nhóm xác định bảy hạn chế dưới đây, mỗi "
     "hạn chế kèm nguyên nhân và hướng khắc phục cụ thể. Việc ghi nhận này không làm giảm giá trị sản phẩm mà "
     "ngược lại, cho thấy nhóm hiểu rõ ranh giới của thiết kế hiện tại."),

    ("table", {
        "caption": "3.30: Hạn chế của hệ thống và hướng phát triển tiếp theo",
        "cols": ["Hạn chế", "Nguyên nhân hiện tại", "Hướng khắc phục"],
        "widths": [4.2, 5.4, 6.4],
        "font": 10,
        "bold_first_col": True,
        "rows": [
            ["Mật khẩu lưu ở dạng văn bản thuần",
             "Đồ án ưu tiên minh họa luồng đăng nhập, chưa tích hợp thư viện băm",
             "Dùng PBKDF2 hoặc BCrypt, thêm cột Salt, đổi mật khẩu lần đầu khi triển khai thật"],
            ["Kích cỡ bị cố định trong mã nguồn",
             "Danh sách kích cỡ được khai báo trực tiếp trong form biến thể",
             "Tách bảng Sizes theo từng loại sản phẩm, cho phép cỡ số như 29, 30, 31 cho quần jeans"],
            ["Chưa có in hóa đơn",
             "Form đơn hàng mới dừng ở xem chi tiết trên lưới",
             "Bổ sung chức năng xuất hóa đơn PDF hoặc in trực tiếp qua PrintDocument"],
            ["Chưa tích hợp cổng thanh toán",
             "Thanh toán được ghi nhận thủ công bằng cờ IsPaid",
             "Tích hợp VNPay hoặc MoMo, thêm bảng giao dịch và xử lý callback"],
            ["Chưa có quản lý nhập kho theo lô",
             "Tồn kho chỉ lưu số lượng hiện tại, không lưu lịch sử nhập",
             "Thêm bảng StockReceipts và StockReceiptDetails để truy vết nguồn nhập"],
            ["Phân hệ desktop chưa tách lớp dữ liệu",
             "Câu lệnh SQL đặt trong form để giữ số tệp nhỏ",
             "Tách lớp repository dùng chung, áp dụng mẫu tương tự phân hệ web"],
            ["Chưa có kiểm thử tự động",
             "Kiểm thử được thực hiện thủ công theo bảng kịch bản",
             "Bổ sung kiểm thử đơn vị cho repository và kiểm thử tích hợp cho luồng đặt hàng"],
        ],
    }),

    ("p",
     "Ngoài bảy hạn chế trên, một hướng phát triển có giá trị cao là bổ sung chức năng lọc sản phẩm theo kích cỡ "
     "và màu ngay ở trang danh sách, thay vì chỉ chọn được ở trang chi tiết. Về mặt kỹ thuật, việc này cần một "
     "truy vấn nối giữa bảng sản phẩm và bảng biến thể với điều kiện tồn kho lớn hơn không; về mặt dữ liệu, cấu "
     "trúc hiện tại đã hỗ trợ đầy đủ nên không phải sửa bảng. Đây là ví dụ cho thấy một thiết kế dữ liệu đúng sẽ "
     "làm cho các tính năng mở rộng trở nên rẻ hơn nhiều."),

    ("note",
     "Kết luận Chương 3: ba quy trình nghiệp vụ đã được mô tả tới mức có thể kiểm thử; bốn Use Case trọng yếu có "
     "đặc tả đầy đủ luồng chính và luồng ngoại lệ; cơ sở dữ liệu có tám bảng với ràng buộc bảo vệ các quy tắc "
     "quan trọng; và giao diện hai phân hệ được thiết kế theo hai mục tiêu khác nhau nhưng cùng phục vụ một dòng "
     "dữ liệu duy nhất. Những điểm chưa hoàn thiện đã được ghi nhận kèm hướng khắc phục, tạo cơ sở cho việc tiếp "
     "tục phát triển sản phẩm sau khi kết thúc học phần."),
]
