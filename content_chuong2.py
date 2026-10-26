# -*- coding: utf-8 -*-
"""
content_chuong2.py
==================
Nội dung Chương 2 – Khảo sát công nghệ liên quan.
Toàn bộ mô tả trong chương này được đối chiếu trực tiếp với mã nguồn thực tế của
hai dự án QuanLyDesktop và WebBanHang.
"""

BLOCKS = [

    ("h1", "CHƯƠNG 2: KHẢO SÁT CÔNG NGHỆ LIÊN QUAN"),

    ("p",
     "Chương 2 trình bày cơ sở lý luận của các quyết định kỹ thuật trong đồ án và kết quả khảo sát mã nguồn "
     "thực tế của hai phân hệ. Cách trình bày đi theo hai lớp: lớp lý luận giải thích vì sao một nguyên lý hay "
     "một mô hình kiến trúc được chọn, lớp khảo sát chỉ ra nguyên lý đó xuất hiện ở dòng mã nào, tệp nào. Mục "
     "tiêu của cách trình bày này là để người đọc có thể kiểm chứng từng nhận định thay vì tin vào phần mô tả, "
     "đồng thời để báo cáo không trở thành bản liệt kê công nghệ mà không gắn với sản phẩm."),

    # ------------------------------------------------------------------ 2.1
    ("h2", "2.1. Lý luận tổng quan về kiến trúc phần mềm"),

    ("p",
     "Kiến trúc phần mềm là tập hợp các quyết định về cách chia hệ thống thành các phần, cách các phần giao "
     "tiếp và cách phân bổ trách nhiệm. Một kiến trúc tốt không làm chương trình chạy nhanh hơn ở nghĩa vi mô, "
     "nhưng làm chi phí thay đổi thấp hơn: khi yêu cầu thay đổi, người sửa biết chính xác phải mở tệp nào và "
     "biết thay đổi đó không làm hỏng phần còn lại. Hai nguyên lý được dùng làm thước đo trong suốt đồ án là "
     "Single Responsibility Principle và Separation of Concerns."),

    ("h3", "2.1.1. Nguyên lý đơn trách nhiệm (Single Responsibility Principle)"),

    ("p",
     "Single Responsibility Principle phát biểu rằng một thành phần phần mềm chỉ nên có một lý do duy nhất để "
     "thay đổi. Cần hiểu “lý do thay đổi” theo nghĩa người yêu cầu thay đổi, không phải theo nghĩa số dòng mã. "
     "Một lớp vừa mở kết nối cơ sở dữ liệu, vừa kiểm tra dữ liệu nhập, vừa định dạng tiền tệ và vừa bắt sự kiện "
     "nút bấm có tới bốn nhóm người quan tâm khác nhau: quản trị viên cơ sở dữ liệu, người kiểm thử nghiệp vụ, "
     "kế toán và người dùng cuối. Khi bất kỳ nhóm nào yêu cầu thay đổi, cùng một tệp phải sửa, và rủi ro làm "
     "hỏng các chức năng không liên quan tăng lên."),

    ("p",
     "Trong đồ án, nguyên lý này được hiện thực theo ba cách. Thứ nhất, phần truy cập dữ liệu của web được tách "
     "khỏi phần điều khiển: mọi câu lệnh SQL nằm trong các lớp repository ở thư mục Data/, còn controller chỉ "
     "điều phối đầu vào và chọn view. Thứ hai, phần định dạng hiển thị được đặt trong model: ví dụ lớp Product "
     "tự tính nhãn tồn kho “Còn hàng”, “Sắp hết hàng”, “Hết hàng” và ảnh dự phòng, nên view không phải chứa các "
     "câu điều kiện if lồng nhau. Thứ ba, phần trạng thái đơn hàng được tập trung trong lớp hằng số "
     "OrderConstants, nhờ vậy chuỗi “Chờ xử lý” không bị gõ lại rải rác và không có nguy cơ sai một ký tự."),

    ("h3", "2.1.2. Nguyên lý phân tách mối quan tâm (Separation of Concerns)"),

    ("p",
     "Separation of Concerns là nguyên lý rộng hơn: mỗi mối quan tâm kỹ thuật nên được xử lý ở một nơi, và các "
     "mối quan tâm khác nhau không nên trộn vào cùng một đoạn mã. Ba mối quan tâm thường gặp nhất là giao diện, "
     "nghiệp vụ và dữ liệu. Nếu trộn lẫn, hệ thống sẽ gặp hiện tượng “logic rò rỉ”: một quy tắc nghiệp vụ bị "
     "viết lại ở nhiều nơi, khi quy tắc thay đổi thì có nơi được sửa và có nơi bị bỏ quên."),

    ("p",
     "Đồ án minh chứng rủi ro này bằng một ví dụ thật. Quy tắc “không được bán vượt tồn kho” xuất hiện ở ba nơi "
     "với ba mức độ khác nhau: ở giao diện web, nút thêm vào giỏ bị vô hiệu hóa khi biến thể hết hàng; ở bộ "
     "điều khiển, số lượng được kiểm tra với tồn kho hiện tại và thông báo lỗi thân thiện; ở tầng dữ liệu, câu "
     "lệnh cập nhật chỉ chạy khi StockQuantity lớn hơn hoặc bằng số lượng cần trừ. Ba lớp này không trùng lặp vô "
     "ích: hai lớp đầu bảo vệ trải nghiệm người dùng, lớp thứ ba bảo vệ tính toàn vẹn dữ liệu khi có truy cập "
     "đồng thời. Phân tách mối quan tâm không có nghĩa là mỗi quy tắc chỉ được xuất hiện một lần, mà là mỗi lớp "
     "chịu trách nhiệm một khía cạnh khác nhau của cùng một quy tắc."),

    ("table", {
        "caption": "2.1: Đối chiếu nguyên lý SRP và SoC với cấu trúc thực tế của đồ án",
        "cols": ["Thành phần", "Trách nhiệm duy nhất", "Minh chứng trong mã nguồn"],
        "widths": [4.2, 5.4, 6.4],
        "font": 10.5,
        "bold_first_col": True,
        "rows": [
            ["Controller\n(Web)",
             "Tiếp nhận tham số, kiểm tra tính hợp lệ của yêu cầu, chọn view hoặc chuyển hướng",
             "ProductsController, CartController, CheckoutController chỉ gọi repository và service, không chứa SQL"],
            ["Repository\n(Web)",
             "Duy nhất việc đọc – ghi cơ sở dữ liệu và quản lý giao dịch",
             "SqlOrderRepository, SqlProductRepository; toàn bộ SqlCommand và SqlTransaction nằm ở đây"],
            ["Model hiển thị\n(Web)",
             "Mang dữ liệu đã được chuẩn hóa và tự suy ra các thuộc tính trình bày",
             "Product.DisplayImage, Product.StockText, OrderLookupViewModel.StatusFlow"],
            ["Service giỏ hàng\n(Web)",
             "Quản lý trạng thái giỏ hàng trong session, không quan tâm đến nguồn dữ liệu sản phẩm",
             "CartService: Add, UpdateQuantity, Remove, Clear; dữ liệu lưu dạng JSON trong session"],
            ["Form (Desktop)",
             "Bắt sự kiện giao diện và điều phối hiển thị trên một màn hình nghiệp vụ",
             "FormBienTheSanPham chỉ xử lý biến thể; FormDonHang chỉ xử lý đơn hàng"],
            ["DatabaseConfig\n(Desktop)",
             "Duy nhất việc tạo chuỗi kết nối từ cấu hình môi trường",
             "DatabaseConfig.ConnectionString dùng SqlConnectionStringBuilder, không chứa mật khẩu trong mã nguồn"],
        ],
    }),

    ("h3", "2.1.3. Kiến trúc MVC cho phân hệ Web"),

    ("p",
     "MVC chia ứng dụng web thành ba thành phần: Model giữ dữ liệu và quy tắc, View chịu trách nhiệm trình bày, "
     "Controller tiếp nhận yêu cầu và điều phối. ASP.NET Core MVC hiện thực mô hình này bằng cơ chế định tuyến: "
     "một URL được ánh xạ tới một action method, action method trả về view cùng một model. Ưu điểm lớn nhất của "
     "MVC trong bối cảnh đồ án là tính khớp tự nhiên với giao thức HTTP: mỗi action là một điểm vào rõ ràng, "
     "mỗi yêu cầu là một chu trình khép kín, và trạng thái không bị giữ lại giữa các yêu cầu ngoài session – "
     "điều này buộc nhóm phải suy nghĩ rõ ràng về việc dữ liệu nào cần lưu bền vững."),

    ("p",
     "Nhược điểm của MVC là số tệp tăng theo số chức năng và việc lặp lại các bước kiểm tra ở nhiều action. Đồ "
     "án giảm nhược điểm này bằng cách đưa các kiểm tra dùng chung vào model với DataAnnotations, dùng TempData "
     "để chuyển thông báo sau khi chuyển hướng, và dùng một action dùng chung để quay lại trang trước đó một "
     "cách an toàn. Hình 2.1 mô tả vòng đời một yêu cầu thêm vào giỏ hàng, từ lúc trình duyệt gửi biểu mẫu đến "
     "lúc trang được dựng lại."),

    ("figure", {
        "caption": "2.1: Vòng đời một yêu cầu MVC khi khách thêm sản phẩm vào giỏ hàng",
        "file": "diagrams/03_mvc_request_flow.mmd",
        "note": "Sequence Diagram: Browser → Routing → CartController → Repository/Service → Session → View",
        "height": 7.0,
    }),

    ("p",
     "Vòng đời trong Hình 2.1 cho thấy hai điểm quan trọng. Thứ nhất, controller không tự đọc cơ sở dữ liệu mà "
     "hỏi repository, nhờ vậy cùng một truy vấn có thể được dùng lại ở nhiều action. Thứ hai, sau khi thay đổi "
     "giỏ hàng, action không trả về view trực tiếp mà chuyển hướng, để tránh hiện tượng gửi lại biểu mẫu khi "
     "người dùng nhấn F5 – một lỗi thường gặp khiến giỏ hàng bị cộng hai lần."),

    ("h3", "2.1.4. Kiến trúc hướng sự kiện và phân tầng cho ứng dụng Desktop"),

    ("p",
     "Ứng dụng Windows Forms hoạt động theo mô hình hướng sự kiện: chương trình không chạy tuần tự từ trên "
     "xuống mà chờ người dùng tác động, mỗi tác động kích hoạt một hàm xử lý. Mô hình này phù hợp với công việc "
     "nhập liệu tốc độ cao tại cửa hàng, nơi nhân viên dùng bàn phím nhiều hơn chuột. Tuy vậy, nếu đặt toàn bộ "
     "câu lệnh SQL ngay trong hàm xử lý sự kiện thì form sẽ nhanh chóng trở thành một khối mã không thể kiểm "
     "thử, và mọi thay đổi về cấu trúc bảng đều phải sửa trong giao diện."),

    ("p",
     "Vì vậy phân hệ desktop được tổ chức theo hướng phân tầng trong phạm vi một tiến trình: tầng giao diện gồm "
     "các form và các control; tầng kết nối gồm DatabaseConfig; tầng dữ liệu là các câu lệnh SQL tham số hóa "
     "được đặt trong những hàm có tên nghiệp vụ rõ ràng; và tầng quy tắc là các hàm kiểm tra như "
     "VariantAlreadyExists hay GetAllowedNextStatuses. Việc phân tầng ở đây không dùng interface và dependency "
     "injection như trên web, bởi quy mô ứng dụng nhỏ và mỗi form chỉ phục vụ một màn hình nghiệp vụ; nhưng ranh "
     "giới trách nhiệm vẫn được giữ để form không trực tiếp chứa logic dữ liệu phức tạp."),

    ("figure", {
        "caption": "2.2: Phân tầng xử lý trong phân hệ Windows Forms",
        "file": "diagrams/04_winforms_layers.mmd",
        "note": "Sự kiện giao diện → hàm xử lý nghiệp vụ → truy vấn tham số hóa → SQL Server",
        "height": 6.4,
    }),

    ("p",
     "Hình 2.2 cũng cho thấy điểm khác biệt cốt lõi so với web: trên desktop, trạng thái nghiệp vụ được giữ "
     "trong bộ nhớ của form (sản phẩm đang chọn, dòng đang chọn trên lưới), nên các thao tác liên tiếp có thể "
     "dùng lại ngữ cảnh mà không cần truyền tham số qua URL. Đổi lại, form phải tự quản lý vòng đời dữ liệu: "
     "sau mỗi thao tác ghi, lưới phải được nạp lại để tránh hiển thị dữ liệu cũ. Trong đồ án, quy ước này được "
     "áp dụng nhất quán: mọi hàm xử lý ghi thành công đều gọi lại hàm nạp dữ liệu tương ứng trước khi kết thúc."),

    ("table", {
        "caption": "2.2: So sánh hai mô hình kiến trúc được sử dụng trong đồ án",
        "cols": ["Tiêu chí", "MVC (ASP.NET Core)", "Hướng sự kiện, phân tầng (WinForms)"],
        "widths": [3.4, 6.2, 6.4],
        "font": 10.5,
        "bold_first_col": True,
        "rows": [
            ["Đơn vị xử lý",
             "Action method, mỗi yêu cầu HTTP là một chu trình độc lập",
             "Hàm xử lý sự kiện, form giữ trạng thái trong suốt thời gian mở"],
            ["Trạng thái",
             "Không lưu giữ mặc định; chỉ giỏ hàng lưu trong session",
             "Lưu trong biến thành viên của form (sản phẩm đang chọn, dòng đang chọn)"],
            ["Tương tác người dùng",
             "Điều hướng bằng URL, thao tác tuần tự từng bước",
             "Nhiều thao tác trên cùng màn hình, hỗ trợ bàn phím và lưới dữ liệu"],
            ["Kiểm tra dữ liệu vào",
             "DataAnnotations ở model, kiểm tra lại ở controller",
             "Hàm kiểm tra riêng trong form, thông báo bằng hộp thoại"],
            ["Phù hợp với",
             "Khách hàng ở xa, thiết bị không cài đặt, cần hiển thị đẹp",
             "Nhân viên thao tác liên tục, cần tốc độ nhập liệu"],
            ["Rủi ro chính",
             "Lặp mã kiểm tra, quên chống gửi lại biểu mẫu",
             "Mã phình to trong form nếu không tách hàm, dữ liệu hiển thị lỗi thời"],
        ],
    }),

    # ------------------------------------------------------------------ 2.2
    ("h2", "2.2. Công nghệ và thư viện sử dụng"),

    ("p",
     "Nguyên tắc chọn công nghệ của nhóm là ưu tiên công nghệ có sẵn trên nền .NET 8 và có thể vận hành mà "
     "không cần hạ tầng phức tạp. Đồ án không dùng thư viện giao diện bên thứ ba cho ứng dụng desktop (không "
     "dùng Guna UI hay Metro Framework); toàn bộ giao diện được dựng bằng control chuẩn của WinForms kết hợp "
     "định dạng thủ công về màu nền, màu chữ và khoảng đệm. Lựa chọn này giảm phụ thuộc và giúp việc biên dịch "
     "ổn định trên nhiều máy, đổi lại nhóm phải tự xử lý các chi tiết thẩm mỹ."),

    ("p",
     "Một điểm cần nêu rõ để tránh hiểu nhầm: đồ án không dùng Entity Framework hay bất kỳ ORM nào. Toàn bộ "
     "truy cập dữ liệu ở cả hai phân hệ đều dùng ADO.NET trực tiếp với Microsoft.Data.SqlClient. Nhóm chọn "
     "ADO.NET vì ba lý do: thứ nhất, đề tài hướng đến việc hiểu và kiểm soát câu lệnh SQL thực sự chạy; thứ "
     "hai, những yêu cầu như trừ kho có điều kiện và cập nhật theo trạng thái hiện tại cần câu lệnh viết tay để "
     "kiểm tra số dòng bị tác động; thứ ba, việc không có ORM buộc nhóm phải đọc kết quả và xử lý giá trị NULL "
     "một cách tường minh, nhờ vậy các lỗi kiểu dữ liệu được phát hiện sớm."),

    ("h3", "2.2.1. Backend và ứng dụng Desktop"),

    ("p",
     "Cả hai phân hệ đều chạy trên .NET 8. Phân hệ desktop là dự án kiểu WinExe với thuộc tính UseWindowsForms, "
     "chỉ phụ thuộc duy nhất gói Microsoft.Data.SqlClient phiên bản 5.2.2. Việc giữ danh sách phụ thuộc tối "
     "thiểu giúp thời gian biên dịch ngắn và giảm khả năng xung đột phiên bản khi chuyển máy. Ngôn ngữ C# được "
     "dùng với các tính năng hiện đại như khai báo biến bằng var, chuỗi nội suy, biểu thức using và kiểu nullable "
     "để giảm lỗi tham chiếu null."),

    ("h3", "2.2.2. Frontend Web"),

    ("p",
     "Phần giao diện web dùng Razor View Engine để trộn mã C# với HTML, Bootstrap 5 cho hệ thống lưới và thành "
     "phần giao diện, jQuery cùng các thư viện liên quan cho tương tác phía trình duyệt. Điểm đáng chú ý là "
     "trang chi tiết sản phẩm không dùng thư viện chọn biến thể phức tạp mà tự dựng hai nhóm nút chọn kích cỡ và "
     "màu, sau đó đồng bộ với một thẻ select ẩn bằng một đoạn JavaScript nhỏ. Cách làm này giữ được khả năng "
     "hoạt động khi JavaScript bị tắt: thẻ select vẫn cho phép gửi đúng mã biến thể về máy chủ."),

    ("p",
     "Về cấu hình, ứng dụng web bật localization với culture vi-VN để định dạng số và ngày theo tiếng Việt, bật "
     "session với thời gian nhàn rỗi hai giờ và cookie HttpOnly, đồng thời đặt middleware theo đúng thứ tự "
     "Routing, Session, Authorization. Thứ tự này quan trọng vì nếu đặt Session sau Authorization thì các bộ lọc "
     "phân quyền không đọc được dữ liệu phiên."),

    ("h3", "2.2.3. Cơ sở dữ liệu"),

    ("p",
     "Cơ sở dữ liệu là SQL Server 2022 chạy trong Docker. Cấu trúc gồm tám bảng, năm chỉ mục bổ trợ, một view "
     "tổng hợp tồn kho và các ràng buộc CHECK bảo vệ miền giá trị. Lý do chọn SQL Server thay vì một hệ quản trị "
     "nhẹ hơn là nhu cầu về giao dịch, khóa ở mức dòng và ràng buộc duy nhất có điều kiện – những tính năng "
     "trực tiếp bảo vệ nghiệp vụ tồn kho của đồ án. Việc chạy trong container giúp toàn bộ nhóm dùng chung một "
     "phiên bản và có thể tái tạo môi trường bằng một câu lệnh."),

    ("table", {
        "caption": "2.3: Tổng hợp công nghệ, thư viện và phạm vi sử dụng",
        "cols": ["Thành phần", "Công nghệ / thư viện", "Phiên bản", "Sử dụng trong đồ án"],
        "widths": [3.0, 4.4, 2.2, 6.4],
        "font": 10.5,
        "rows": [
            ["Nền tảng", ".NET", "8.0", "Cả hai dự án, mục tiêu net8.0 và net8.0-windows"],
            ["Desktop", "Windows Forms", "8.0", "Năm form nghiệp vụ và các control chuẩn, không dùng thư viện giao diện ngoài"],
            ["Truy cập dữ liệu", "Microsoft.Data.SqlClient (ADO.NET)", "5.2.2", "SqlConnection, SqlCommand, SqlDataAdapter, SqlTransaction ở cả hai phân hệ"],
            ["Web", "ASP.NET Core MVC", "8.0", "Controllers, Razor Views, Session, Dependency Injection, localization vi-VN"],
            ["Giao diện web", "Bootstrap, jQuery, jQuery Validation", "5.x / 3.x", "Lưới, thành phần giao diện, kiểm tra biểu mẫu phía trình duyệt"],
            ["Cơ sở dữ liệu", "SQL Server", "2022 (Docker)", "Tám bảng, một view, năm chỉ mục, ràng buộc CHECK và UNIQUE"],
            ["Mô hình hóa", "Mermaid", "10.x", "14 sơ đồ lưu trong thư mục diagrams/, render bằng Mermaid CLI"],
            ["Quản lý mã nguồn", "Git", "–", "Nhánh riêng cho từng thành viên, hợp nhất sau khi rà soát"],
        ],
    }),

    # ------------------------------------------------------------------ 2.3
    ("h2", "2.3. Kiến trúc chi tiết của từng phân hệ"),

    ("p",
     "Mục này đi vào cấu trúc thư mục và vai trò từng tệp. Cách đọc đề nghị là đọc cây thư mục trước để nắm vị "
     "trí, sau đó đọc bảng vai trò để hiểu trách nhiệm, cuối cùng đối chiếu với các nhận định về nguyên lý ở mục "
     "2.1. Toàn bộ tên tệp, tên hàm và tên bảng trong mục này được trích từ mã nguồn thực tế, không phải tên dự "
     "kiến."),

    ("h3", "2.3.1. Cấu trúc phân hệ QuanLyDesktop"),

    ("p",
     "Phân hệ desktop được tổ chức theo màn hình nghiệp vụ: mỗi form gồm ba tệp cùng tên (tệp mã xử lý, tệp "
     "thiết kế giao diện và tệp tài nguyên), cộng thêm các tệp hạ tầng dùng chung. Cấu trúc này do Visual Studio "
     "sinh ra và được giữ nguyên vì nó tách phần giao diện do trình thiết kế quản lý khỏi phần mã do lập trình "
     "viên viết, giúp việc mở lại form trong trình thiết kế không làm mất mã xử lý."),

    ("code", r"""QuanLyDesktop/
├── QuanLyDesktop.csproj          // net8.0-windows, WinExe, Microsoft.Data.SqlClient 5.2.2
├── Program.cs                    // điểm khởi động, bật chế độ hiển thị, mở Form1
├── DatabaseConfig.cs             // tạo chuỗi kết nối từ biến môi trường
├── Form1.cs / .Designer.cs / .resx        // đăng nhập và phân quyền
├── FormMain.cs / .Designer.cs / .resx     // dashboard, quản lý sản phẩm, điều hướng
├── FormDanhMuc.cs / .Designer.cs / .resx  // quản lý loại thời trang
├── FormBienTheSanPham.cs / .Designer.cs   // quản lý tổ hợp màu - kích cỡ - tồn kho
├── FormDonHang.cs / .Designer.cs / .resx  // tiếp nhận và xử lý đơn hàng
└── bin/ , obj/                            // thư mục sinh tự động khi biên dịch"""),

    ("p",
     "Cấu trúc trên cho thấy phân hệ desktop không có thư mục Models hay Data riêng. Đây là hệ quả của quyết định "
     "phân tầng trong phạm vi tiến trình đã trình bày ở mục 2.1.4: các lớp dữ liệu tạm được thay bằng đối tượng "
     "DataTable do SqlDataAdapter sinh ra, còn câu lệnh SQL được đặt trong hàm của từng form. Với quy mô năm "
     "form, cách tổ chức này giữ số tệp nhỏ và dễ theo dõi; nếu phân hệ mở rộng thêm nhiều màn hình, việc tách "
     "một lớp truy cập dữ liệu dùng chung sẽ là bước tái cấu trúc cần thiết."),

    ("table", {
        "caption": "2.4: Vai trò của từng tệp trong phân hệ QuanLyDesktop",
        "cols": ["Tệp", "Trách nhiệm chính", "Điểm kỹ thuật đáng chú ý"],
        "widths": [4.2, 5.6, 6.2],
        "font": 10.5,
        "bold_first_col": True,
        "rows": [
            ["DatabaseConfig.cs",
             "Cung cấp thuộc tính ConnectionString dùng chung cho mọi form",
             "Đọc mật khẩu từ biến môi trường FASHIONSHOP_SQL_PASSWORD, ném lỗi có hướng dẫn nếu thiếu; "
             "bật Encrypt và TrustServerCertificate cho môi trường phát triển"],
            ["Program.cs",
             "Điểm khởi động, thiết lập chế độ hiển thị và mở form đăng nhập",
             "Dùng ApplicationConfiguration.Initialize để áp dụng cấu hình hiển thị tập trung"],
            ["Form1.cs",
             "Đăng nhập, kiểm tra tài khoản đang hoạt động và chuyển sang form chính",
             "Truy vấn nối Users với Roles để lấy RoleName; dùng ShowDialog và ẩn form đăng nhập thay vì đóng"],
            ["FormMain.cs",
             "Dashboard tổng quan và quản lý sản phẩm ở cấp mẫu hàng; mở các form nghiệp vụ khác",
             "Ba truy vấn đếm cho dashboard; tìm kiếm tham số hóa; xóa mềm bằng IsActive = 0"],
            ["FormDanhMuc.cs",
             "Thêm, sửa, ngừng dùng danh mục; tìm kiếm theo tên",
             "Kiểm tra trùng tên trước khi ghi; cập nhật có điều kiện theo trạng thái hiện tại"],
            ["FormBienTheSanPham.cs",
             "Quản lý tổ hợp màu sắc – kích cỡ và số lượng tồn của một sản phẩm",
             "Kích cỡ chọn từ danh sách cố định; kiểm tra trùng tổ hợp; ràng buộc độ dài màu"],
            ["FormDonHang.cs",
             "Xem danh sách đơn, lọc theo trạng thái, duyệt và chuyển trạng thái, xem chi tiết, đánh dấu thanh toán",
             "Danh sách trạng thái kế tiếp được giới hạn; cập nhật theo trạng thái nguồn; hoàn kho khi hủy"],
            ["Form1.Designer.cs và các tệp .Designer.cs",
             "Khai báo control, vị trí, kích thước và sự kiện do trình thiết kế sinh",
             "Không chứa logic nghiệp vụ; chỉ gán thuộc tính và nối sự kiện"],
        ],
    }),

    ("h3", "2.3.2. Phân tích chi tiết các Form nghiệp vụ"),

    ("p",
     "Form1 là cửa ngõ của phân hệ quản trị. Khi người dùng nhấn nút đăng nhập, form thực hiện một truy vấn nối "
     "hai bảng Users và Roles, lọc theo tên đăng nhập, mật khẩu và điều kiện IsActive bằng 1. Kết quả trả về "
     "gồm họ tên và tên vai trò; nếu không có dòng nào, form hiển thị thông báo chung “tên đăng nhập hoặc mật "
     "khẩu không đúng” thay vì nói rõ sai ở đâu, nhằm tránh tiết lộ sự tồn tại của tài khoản. Sau khi đăng nhập "
     "thành công, form chính được mở bằng ShowDialog và form đăng nhập được ẩn; khi form chính đóng, form đăng "
     "nhập hiện lại và các ô nhập được xóa trắng."),

    ("p",
     "FormMain đảm nhận hai vai trò: màn hình tổng quan và nơi quản lý sản phẩm. Phần tổng quan chạy ba truy vấn "
     "đếm độc lập – số sản phẩm đang bán, số đơn phát sinh trong ngày và tổng tiền các đơn đã thanh toán trong "
     "ngày – rồi hiển thị thành ba thẻ số liệu. Phần quản lý sản phẩm gồm một lưới dữ liệu, một vùng nhập liệu "
     "và các nút thêm, cập nhật, làm mới, tìm kiếm, quản lý biến thể. Điểm đáng chú ý là thao tác xóa không "
     "xóa vật lý dòng dữ liệu mà chuyển IsActive về 0, nhờ vậy các đơn hàng cũ vẫn giữ được tham chiếu tới sản "
     "phẩm đã ngừng bán – một yêu cầu bắt buộc đối với hệ thống có lịch sử giao dịch."),

    ("p",
     "FormDanhMuc quản lý loại thời trang. Ngoài bốn thao tác cơ bản, form còn kiểm tra trùng tên trước khi ghi, "
     "giới hạn độ dài tên danh mục và mô tả, và dùng câu lệnh cập nhật có điều kiện IsActive bằng đúng giá trị "
     "đang hiển thị. Điều kiện này là một dạng kiểm soát đồng thời lạc quan: nếu một người khác vừa đổi trạng "
     "thái của danh mục, số dòng bị tác động sẽ bằng 0 và form biết rằng dữ liệu đã thay đổi, từ đó nạp lại lưới "
     "thay vì ghi đè lên kết quả của người kia."),

    ("p",
     "FormBienTheSanPham là form thể hiện rõ nhất đặc thù của ngành thời trang. Form nhận vào mã sản phẩm và tên "
     "sản phẩm từ FormMain, hiển thị chúng ở phần tiêu đề để nhân viên luôn biết mình đang nhập hàng cho mẫu nào. "
     "Ô kích cỡ là combobox với danh sách cố định gồm XS, S, M, L, XL, XXL và Free size; ô màu sắc là textbox có "
     "ràng buộc độ dài; ô số lượng là NumericUpDown với giá trị nhỏ nhất bằng 0. Trước khi ghi, hàm "
     "VariantAlreadyExists kiểm tra tổ hợp màu và kích cỡ đã tồn tại trong cùng sản phẩm hay chưa, có tham số "
     "loại trừ để khi cập nhật không tự báo trùng với chính dòng đang sửa. Cơ sở dữ liệu cũng có ràng buộc duy "
     "nhất tương ứng, tạo thành hai lớp bảo vệ: lớp kiểm tra trong mã nguồn cho thông báo thân thiện, lớp ràng "
     "buộc trong cơ sở dữ liệu cho tính đúng đắn tuyệt đối."),

    ("p",
     "FormDonHang là nơi diễn ra nghiệp vụ sau bán hàng. Form có ô tìm kiếm theo mã đơn hoặc tên khách, combobox "
     "lọc theo trạng thái với sáu lựa chọn, lưới danh sách đơn và vùng chi tiết. Khi nhân viên chọn một dòng, "
     "form nạp danh sách mặt hàng của đơn đó. Hai thao tác quan trọng là chuyển trạng thái và đánh dấu thanh "
     "toán. Thao tác chuyển trạng thái trước hết kiểm tra trạng thái đích có nằm trong danh sách được phép của "
     "trạng thái hiện tại hay không, sau đó thực hiện câu lệnh cập nhật có điều kiện trạng thái nguồn; nếu số "
     "dòng bị tác động bằng 0 thì form kết luận đơn đã bị người khác xử lý và yêu cầu nạp lại. Khi đơn chuyển "
     "sang Đã hủy, form hoàn lại tồn kho cho từng biến thể bằng một câu lệnh cập nhật nhóm; khi đơn chuyển sang "
     "Hoàn thành, form tự đặt cờ đã thanh toán."),

    ("table", {
        "caption": "2.5: Các control chính trên giao diện quản trị và ý nghĩa nghiệp vụ",
        "cols": ["Form", "Control tiêu biểu", "Ý nghĩa nghiệp vụ"],
        "widths": [3.6, 5.2, 7.2],
        "font": 10.5,
        "bold_first_col": True,
        "rows": [
            ["Form1", "txtTenDangNhap, txtMatKhau, btnDangNhap, btnThoat",
             "Xác thực người dùng quản trị; nút thoát kết thúc ứng dụng"],
            ["FormMain", "panelDashboard, ba thẻ số liệu, dgvSanPham, grpThongTin, btnThem, btnSua, btnXoa, btnBienThe",
             "Xem nhanh tình hình kinh doanh; thêm, sửa, ngừng bán sản phẩm; mở form biến thể cho sản phẩm đang chọn"],
            ["FormDanhMuc", "dgvDanhMuc, txtTenDanhMuc, txtMoTa, nút thêm / sửa / ngừng dùng, ô tìm kiếm",
             "Quản lý loại thời trang làm cơ sở phân loại cho sản phẩm và bộ lọc trên web"],
            ["FormBienTheSanPham", "cboSize, txtColor, nudStock, dgvVariants",
             "Nhập và cập nhật tồn kho theo tổ hợp kích cỡ – màu sắc cho một sản phẩm cụ thể"],
            ["FormDonHang", "txtTimKiem, cboTrangThai, dgvDonHang, vùng chi tiết, btnDuyetDon, btnThanhToan",
             "Theo dõi đơn mới, chuyển trạng thái theo vòng đời, đánh dấu thanh toán, xem mặt hàng trong đơn"],
        ],
    }),

    ("h3", "2.3.3. Cấu trúc phân hệ WebBanHang"),

    ("p",
     "Phân hệ web được tổ chức theo đúng quy ước của ASP.NET Core MVC, có bổ sung thư mục Data cho lớp truy cập "
     "dữ liệu và thư mục Services cho nghiệp vụ giỏ hàng. Việc tách hai thư mục này ra khỏi Models là quyết định "
     "có chủ ý: Models chỉ chứa kiểu dữ liệu và quy tắc trình bày, không chứa câu lệnh SQL hay truy cập session."),

    ("code", r"""WebBanHang/
├── Program.cs                    // cấu hình DI, session, localization, pipeline
├── appsettings.json              // Server, Database, UserId (không chứa mật khẩu)
├── Controllers/
│   ├── HomeController.cs         // trang chủ, giới thiệu, trang lỗi
│   ├── ProductsController.cs     // danh sách, tìm kiếm, chi tiết
│   ├── CartController.cs         // thêm, sửa, xóa, xóa toàn bộ giỏ
│   ├── CheckoutController.cs     // hiển thị và xử lý đặt hàng
│   └── OrderLookupController.cs  // tra cứu đơn theo mã đơn và số điện thoại
├── Data/
│   ├── SqlConnectionFactory.cs   // tạo kết nối, đọc mật khẩu từ cấu hình hoặc môi trường
│   ├── IProductRepository.cs     // hợp đồng đọc dữ liệu sản phẩm
│   ├── SqlProductRepository.cs   // truy vấn danh mục, sản phẩm, biến thể
│   ├── IOrderRepository.cs       // hợp đồng tạo đơn và tra cứu đơn
│   └── SqlOrderRepository.cs     // giao dịch tạo đơn, trừ kho, tra cứu
├── Models/                       // Product, ProductVariant, CartItem, các ViewModel, hằng số
├── Services/
│   ├── ICartService.cs           // hợp đồng giỏ hàng
│   └── CartService.cs            // lưu giỏ hàng dạng JSON trong session
├── Views/                        // _Layout, Home, Products, Cart, Checkout, OrderLookup
└── wwwroot/                      // css, js, thư viện, ảnh"""),

    ("p",
     "Cấu trúc trên cho thấy một đặc điểm quan trọng: không có thư mục cho Entity Framework, cũng không có lớp "
     "DbContext. Thay vào đó là các cặp interface – lớp hiện thực cho repository, được đăng ký trong "
     "Program.cs với vòng đời scoped. Việc dùng interface không nhằm mục đích thay thế hiện thực trong đồ án, mà "
     "nhằm làm rõ hợp đồng dữ liệu: controller chỉ biết repository có những phương thức nào, không biết câu lệnh "
     "SQL bên trong."),

    ("table", {
        "caption": "2.6: Vai trò của các nhóm tệp trong phân hệ WebBanHang",
        "cols": ["Nhóm tệp", "Trách nhiệm", "Chi tiết kỹ thuật"],
        "widths": [3.8, 5.2, 7.0],
        "font": 10.5,
        "bold_first_col": True,
        "rows": [
            ["Program.cs", "Cấu hình toàn ứng dụng",
             "Đăng ký controller với view, bộ nhớ đệm phân tán, session 2 giờ, HttpContextAccessor, "
             "repository scoped, factory singleton; cảnh báo khi thiếu mật khẩu cơ sở dữ liệu"],
            ["Controllers", "Điều phối yêu cầu HTTP",
             "Kiểm tra ModelState, gọi repository hoặc service, đặt TempData cho thông báo, chuyển hướng an toàn"],
            ["Data", "Truy cập dữ liệu bằng ADO.NET",
             "Câu lệnh tham số hóa, xử lý giao dịch, đọc giá trị nullable, escape ký tự đại diện khi tìm kiếm"],
            ["Models", "Kiểu dữ liệu và quy tắc trình bày",
             "Thuộc tính suy ra như DisplayImage, StockText, DiscountPercent; DataAnnotations cho biểu mẫu đặt hàng"],
            ["Services", "Nghiệp vụ giỏ hàng theo phiên",
             "Giỏ hàng tuần tự hóa JSON trong session, giới hạn 99 đơn vị mỗi dòng, gộp số lượng khi thêm trùng"],
            ["Views", "Trình bày giao diện",
             "Layout dùng chung, partial _ProductCard, trang chi tiết có bộ chọn kích cỡ và màu, thanh tiến trình trạng thái"],
            ["wwwroot", "Tài nguyên tĩnh", "Bootstrap, jQuery, biểu định kiểu riêng và ảnh dự phòng khi sản phẩm thiếu ảnh"],
        ],
    }),

    ("h3", "2.3.4. Controllers và các action method chính"),

    ("p",
     "HomeController dựng trang chủ bằng một view model tổng hợp gồm danh sách danh mục, tám sản phẩm nổi bật "
     "và tổng số sản phẩm đang bán. Việc gom ba truy vấn vào một view model giúp view không phải gọi dữ liệu, "
     "đồng thời giảm số lần mở kết nối. ProductsController phục vụ hai màn hình: danh sách có tìm kiếm theo từ "
     "khóa và lọc theo danh mục, và trang chi tiết nhận tham số id, trả về 404 nếu không tìm thấy sản phẩm. "
     "Điểm cần nói rõ: bộ lọc theo kích cỡ và màu sắc không nằm ở danh sách sản phẩm mà nằm ở trang chi tiết, "
     "nơi khách chọn tổ hợp cụ thể trước khi thêm vào giỏ. Danh sách chỉ lọc theo danh mục và từ khóa."),

    ("p",
     "CartController xử lý bốn thao tác: thêm, cập nhật số lượng, xóa một dòng và xóa toàn bộ. Action thêm được "
     "gắn thuộc tính chống giả mạo yêu cầu, kiểm tra biến thể có tồn tại và còn hàng, giới hạn số lượng trong "
     "khoảng từ 1 đến 99, và từ chối nếu tổng số lượng sau khi cộng vượt tồn kho mà không tiết lộ con số tồn kho "
     "chính xác. Sau khi xử lý, action chuyển hướng về trang trước đó với điều kiện URL là đường dẫn nội bộ, "
     "nhờ vậy tránh được lỗi chuyển hướng mở. CheckoutController hiển thị biểu mẫu đặt hàng, kiểm tra phương "
     "thức thanh toán có được hỗ trợ, kiểm tra tính hợp lệ của model, sau đó gọi repository tạo đơn trong một "
     "giao dịch và xóa giỏ hàng nếu thành công. OrderLookupController yêu cầu đồng thời mã đơn và số điện thoại, "
     "đây là biện pháp ngăn việc dò mã đơn để xem thông tin người khác."),

    ("table", {
        "caption": "2.7: Danh mục action method, route và trách nhiệm xử lý",
        "cols": ["Controller", "Action và route", "Trách nhiệm"],
        "widths": [3.4, 4.6, 8.0],
        "font": 10.5,
        "bold_first_col": True,
        "rows": [
            ["HomeController", "Index – /", "Dựng trang chủ: danh mục, sản phẩm nổi bật, tổng số sản phẩm"],
            ["HomeController", "Privacy, Error", "Trang giới thiệu và trang hiển thị lỗi thân thiện"],
            ["ProductsController", "Index – /Products", "Danh sách sản phẩm, tìm theo từ khóa, lọc theo danh mục"],
            ["ProductsController", "Detail – /Products/Detail/{id}", "Chi tiết sản phẩm, danh sách biến thể, chọn kích cỡ và màu"],
            ["CartController", "Index – /Cart", "Hiển thị giỏ hàng kèm tổng tiền và tổng số lượng"],
            ["CartController", "Add (POST)", "Thêm biến thể vào giỏ, kiểm tra tồn kho, gộp dòng trùng"],
            ["CartController", "Update, Remove, Clear", "Đổi số lượng, xóa một dòng, xóa toàn bộ giỏ"],
            ["CheckoutController", "Index – /Checkout (GET)", "Hiển thị biểu mẫu; chuyển về giỏ nếu giỏ rỗng"],
            ["CheckoutController", "Index (POST), Success", "Tạo đơn trong giao dịch, xóa giỏ, hiển thị trang cảm ơn"],
            ["OrderLookupController", "Index – /tra-cuu-don-hang", "Tra cứu đơn theo mã đơn và số điện thoại, hiển thị tiến trình"],
        ],
    }),

    ("h3", "2.3.5. Lớp truy cập dữ liệu và xử lý giao dịch"),

    ("p",
     "SqlProductRepository chứa một hằng chuỗi câu lệnh chọn sản phẩm dùng lại ở nhiều phương thức, trong đó có "
     "một truy vấn con tính tổng tồn kho của sản phẩm từ bảng biến thể. Nhờ vậy mọi nơi hiển thị sản phẩm đều "
     "dùng cùng một định nghĩa “còn hàng”, tránh trường hợp trang chủ nói còn hàng còn trang chi tiết nói hết "
     "hàng. Phương thức tìm kiếm dùng tham số và thoát các ký tự đại diện của LIKE, đồng thời áp dụng collation "
     "tiếng Việt để tìm không phân biệt dấu, nên khách gõ “ao so mi” vẫn tìm được “áo sơ mi”."),

    ("p",
     "SqlOrderRepository là lớp phức tạp nhất của đồ án. Phương thức tạo đơn thực hiện các bước: mở kết nối và "
     "bắt đầu giao dịch với mức cô lập ReadCommitted; tạo hoặc lấy mã khách hàng theo số điện thoại; sinh mã đơn "
     "theo mẫu DH kèm thời gian và số ngẫu nhiên, thử lại tối đa ba lần nếu vi phạm ràng buộc duy nhất; gom các "
     "dòng hàng theo mã biến thể và sắp xếp theo mã để giảm khả năng khóa chéo giữa hai giao dịch đồng thời; với "
     "từng biến thể, đọc tồn kho và thực hiện câu lệnh trừ kho có điều kiện; nếu số dòng bị tác động bằng 0 thì "
     "ném ngoại lệ để hủy toàn bộ giao dịch; cuối cùng ghi chi tiết đơn với độ chính xác tiền tệ 18,2 và cập "
     "nhật tổng tiền của đơn. Toàn bộ các bước nằm trong một khối try với hàm hủy giao dịch an toàn trong khối "
     "catch."),

    ("code", """// Trích SqlOrderRepository: trừ kho có điều kiện, chống bán vượt tồn
UPDATE ProductVariants
SET StockQuantity = StockQuantity - @quantity
WHERE ProductVariantId = @variantId
  AND StockQuantity >= @quantity;

IF @@ROWCOUNT = 0
    THROW 51000, N'Tồn kho không đủ cho biến thể đã chọn', 1;"""),

    ("p",
     "Đoạn mã trên là hiện thực cô đọng nhất của nguyên tắc “để cơ sở dữ liệu phân xử”. Điều kiện StockQuantity "
     "lớn hơn hoặc bằng số lượng cần trừ nằm trong chính câu lệnh cập nhật, nên không có khoảng thời gian nào "
     "giữa bước kiểm tra và bước ghi để một giao dịch khác chen vào. Đây chính là điểm khác biệt so với cách làm "
     "đọc tồn kho rồi mới quyết định, vốn có thể bán vượt tồn khi hai khách đặt cùng lúc."),

    ("table", {
        "caption": "2.8: Các phương thức truy vấn chính và cơ chế bảo vệ tương ứng",
        "cols": ["Lớp / phương thức", "Mục đích", "Cơ chế bảo vệ"],
        "widths": [5.2, 5.4, 5.4],
        "font": 10.5,
        "rows": [
            ["SqlProductRepository.GetProductsAsync",
             "Danh sách sản phẩm có tìm kiếm và lọc danh mục",
             "Tham số hóa; escape ký tự LIKE; collation tiếng Việt; chỉ lấy sản phẩm đang bán"],
            ["SqlProductRepository.GetFeaturedProductsAsync",
             "Tám sản phẩm nổi bật cho trang chủ",
             "Phân trang bằng OFFSET – FETCH, không nạp toàn bộ bảng"],
            ["SqlProductRepository.GetVariantsAsync",
             "Danh sách biến thể kèm tồn kho của một sản phẩm",
             "Chỉ đọc các cột cần thiết, sắp xếp theo kích cỡ và màu để hiển thị ổn định"],
            ["SqlOrderRepository.CreateOrderAsync",
             "Tạo đơn hàng hoàn chỉnh",
             "Giao dịch ReadCommitted; trừ kho có điều kiện; thử lại mã đơn tối đa ba lần; hủy toàn bộ khi lỗi"],
            ["SqlOrderRepository.FindOrderForLookupAsync",
             "Tra cứu đơn cho khách",
             "Bắt buộc khớp mã đơn và số điện thoại; so khớp cả dạng có và không có ký tự phân cách"],
            ["SqlOrderRepository.GetOrderDetailsAsync",
             "Chi tiết mặt hàng của một đơn",
             "Đọc kèm tên sản phẩm, kích cỡ, màu để hiển thị đúng biến thể đã mua"],
            ["CartService.Add",
             "Thêm hoặc cộng dồn số lượng trong giỏ",
             "Giới hạn 99 đơn vị mỗi dòng; kiểm tra với tồn kho ở controller trước khi gọi"],
            ["SqlConnectionFactory.CreateConnection",
             "Tạo kết nối tới SQL Server",
             "Đọc mật khẩu từ cấu hình hoặc biến môi trường; đặt thời gian chờ kết nối 15 giây"],
        ],
    }),

    ("h3", "2.3.6. Nhận xét về kiến trúc sau khi khảo sát mã nguồn"),

    ("p",
     "Có thể thấy kiến trúc của đồ án đã đạt được mục tiêu phân tách ở mức đủ dùng: hai phân hệ độc lập về triển "
     "khai nhưng thống nhất về dữ liệu; lớp truy cập dữ liệu của web được tách khỏi controller; các quy tắc quan "
     "trọng về tồn kho và trạng thái đơn đều được đặt ở nơi có khả năng thực thi thực sự là cơ sở dữ liệu. Đổi "
     "lại, phân hệ desktop vẫn còn câu lệnh SQL nằm trong form, và quy tắc chuyển trạng thái đơn hàng tồn tại ở "
     "hai nơi (kiểm tra trong mã nguồn desktop và giá trị lưu trong cơ sở dữ liệu). Đây là những điểm được ghi "
     "nhận trung thực và đưa vào phần hạn chế ở Chương 3, kèm hướng khắc phục cụ thể."),

    ("note",
     "Kết luận Chương 2: công nghệ được chọn không phải công nghệ mới nhất mà là công nghệ cho phép hiện thực "
     "đúng ba yêu cầu đã đặt ra ở Chương 1 – truy vấn quan hệ chặt, giao dịch ở mức cơ sở dữ liệu, và hai mô "
     "hình lập trình cùng chia sẻ quy tắc nghiệp vụ. Phần còn lại của báo cáo sẽ dùng chính cấu trúc này để phân "
     "tích nghiệp vụ và thiết kế chi tiết."),
]
