# -*- coding: utf-8 -*-
"""
content_chuong1.py
==================
Nội dung Chương 1 – Tổng quan hệ thống.

Mỗi phần tử của BLOCKS là một bộ (kind, value). Các kind được generate_doc.py
hỗ trợ: h1, h2, h3, p, p_noindent, note, bul, bul2, num, code, table, figure, pb.
"""

BLOCKS = [

    ("h1", "CHƯƠNG 1: TỔNG QUAN HỆ THỐNG"),

    ("p",
     "Chương mở đầu có nhiệm vụ trả lời ba câu hỏi nền tảng trước khi đi vào bất kỳ dòng mã nguồn nào: "
     "bài toán mà đồ án giải quyết xuất phát từ đâu, sản phẩm cần đạt được điều gì, và nhóm đã tổ chức "
     "công việc như thế nào để biến yêu cầu thành phần mềm chạy được. Cách trình bày được giữ theo tinh "
     "thần của báo cáo mẫu: mỗi nhận định đều phải dẫn tới một quyết định kỹ thuật cụ thể, tránh việc mô "
     "tả chung chung rồi mới giải thích ở chương sau. Vì vậy, ngay trong chương này, các khái niệm về biến "
     "thể sản phẩm, tồn kho theo thuộc tính và tính nhất quán dữ liệu giữa hai phân hệ đã được đặt lên bàn "
     "ngay từ đầu, để những lựa chọn về kiến trúc, công nghệ và cơ sở dữ liệu ở các chương tiếp theo có "
     "điểm tựa rõ ràng."),

    # ------------------------------------------------------------------ 1.1
    ("h2", "1.1. Bối cảnh và vấn đề"),

    ("p",
     "Kinh doanh thời trang bán lẻ tại Việt Nam trong những năm gần đây dịch chuyển mạnh sang mô hình đa "
     "kênh: một cửa hàng quy mô nhỏ vẫn có thể vừa bán trực tiếp tại điểm bán, vừa bán qua mạng xã hội, vừa "
     "có một trang web riêng để khách tự chọn hàng. Điều đó tạo ra một nghịch lý vận hành quen thuộc: nhu "
     "cầu tiếp nhận đơn hàng tăng nhanh nhưng năng lực kiểm soát hàng tồn lại không tăng tương ứng. Trang "
     "web càng dễ đặt hàng thì số đơn ảo, số đơn phải gọi lại để xác nhận, số trường hợp báo hết hàng sau "
     "khi khách đã chuyển khoản càng nhiều. Vấn đề gốc không nằm ở tốc độ đường truyền hay giao diện đẹp "
     "hay không, mà nằm ở chỗ dữ liệu hàng hóa không được mô tả đủ chi tiết để hai kênh cùng nhìn thấy một "
     "sự thật duy nhất."),

    ("h3", "1.1.1. Đặc thù dữ liệu của ngành kinh doanh thời trang"),

    ("p",
     "Khác với một cửa hàng tạp hóa – nơi mỗi mặt hàng là một đơn vị tồn kho duy nhất, một mẫu quần áo trong "
     "cửa hàng thời trang thực chất là một họ các sản phẩm con. Cùng một thiết kế áo sơ mi có thể được nhập "
     "về với bốn màu và năm kích cỡ, tạo thành hai mươi tổ hợp; mỗi tổ hợp có số lượng tồn riêng, giá bán "
     "riêng khi chạy chương trình khuyến mãi theo màu, và vòng đời nhập – xuất riêng. Khách hàng không mua "
     "một dòng trong bảng sản phẩm, khách hàng mua đúng một tổ hợp cụ thể: áo sơ mi trắng, cỡ M. Nếu cơ sở "
     "dữ liệu chỉ lưu số lượng tồn ở cấp sản phẩm cha, nhân viên bán hàng buộc phải ghi nhớ hoặc ghi chú "
     "tay phần còn lại, và mọi con số tổng hợp sau đó đều mất giá trị đối chiếu."),

    ("p",
     "Bên cạnh hai thuộc tính bắt buộc là màu sắc và kích cỡ, dữ liệu thời trang còn chịu ảnh hưởng của chất "
     "liệu (cotton, linen, lụa, kaki), kiểu dáng và mùa vụ. Những thuộc tính này quyết định cách khách hàng "
     "tìm kiếm: người mua thường bắt đầu bằng loại sản phẩm (áo, quần, váy, phụ kiện), sau đó thu hẹp theo "
     "kích cỡ của bản thân và cuối cùng chọn màu phù hợp với tủ đồ. Một hệ thống chỉ cho phép tìm theo tên "
     "sản phẩm sẽ buộc khách phải mở rất nhiều trang mới tìm được món đồ vừa ý, còn nhân viên kho thì không "
     "có cách nào trả lời nhanh câu hỏi “còn cỡ L màu đen hay không”. Chính đặc thù này biến bài toán quản lý "
     "thời trang thành bài toán quản lý tổ hợp thuộc tính, chứ không phải bài toán đếm số lượng đơn thuần."),

    ("h3", "1.1.2. Giới hạn của phương thức quản lý sổ sách và bảng tính"),

    ("p",
     "Ở giai đoạn đầu, hầu hết cửa hàng nhỏ dùng sổ tay hoặc một tệp bảng tính để theo dõi hàng và đơn. Cách "
     "làm này có ưu điểm là chi phí gần bằng không và không cần đào tạo, nhưng bộc lộ ba giới hạn mang tính "
     "cấu trúc, không thể khắc phục bằng cách gõ cẩn thận hơn. Thứ nhất, bảng tính không có khái niệm ràng "
     "buộc dữ liệu: một ô số lượng hoàn toàn có thể chứa chữ, một mã sản phẩm có thể bị nhập trùng ở hai "
     "dòng, và không có cơ chế nào ngăn giá bán âm. Thứ hai, bảng tính không có giao dịch: khi nhân viên "
     "đang sửa số lượng tồn mà mất điện, tệp có thể ở trạng thái nửa cũ nửa mới, và người tiếp theo đọc được "
     "một con số không ai chịu trách nhiệm. Thứ ba, bảng tính không phục vụ được nhiều người cùng lúc theo "
     "thời gian thực: hai nhân viên cùng mở một tệp, người lưu sau sẽ ghi đè kết quả của người lưu trước, "
     "hiện tượng “mất cập nhật” xảy ra mà không có bất kỳ cảnh báo nào."),

    ("p",
     "Hệ quả nghiệp vụ của ba giới hạn trên rất cụ thể. Kho hàng sai lệch dần theo thời gian vì mỗi lần bán "
     "không được trừ tự động khỏi tồn; đơn hàng bị xử lý chậm vì phải tra cứu thủ công giữa nhiều tệp; và "
     "khi có khiếu nại, cửa hàng không có bằng chứng kỹ thuật nào để đối chiếu ngoài lời kể của nhân viên. "
     "Những sai lệch nhỏ tích lũy thành khoản chênh lệch lớn vào cuối tháng, và chi phí để tìm ra nguyên nhân "
     "thường cao hơn nhiều so với chi phí đầu tư một hệ thống quản lý đúng nghĩa."),

    ("table", {
        "caption": "1.1: So sánh phương thức quản lý truyền thống với hệ thống phần mềm của đồ án",
        "cols": ["Tiêu chí", "Sổ sách / bảng tính", "Hệ thống của đồ án"],
        "widths": [3.4, 6.0, 6.6],
        "font": 11,
        "bold_first_col": True,
        "rows": [
            ["Mô hình tồn kho",
             "Một dòng cho mỗi mẫu sản phẩm, màu và cỡ ghi chú trong ô văn bản",
             "Tách Products và ProductVariants; mỗi tổ hợp màu – cỡ là một dòng có số lượng riêng"],
            ["Kiểm soát trùng lặp",
             "Không có; phụ thuộc trí nhớ người nhập",
             "Kiểm tra trùng trong mã nguồn và ràng buộc UNIQUE trên (ProductId, Size, Color)"],
            ["Tính toàn vẹn khi lỗi",
             "Không có giao dịch, tệp dễ hỏng khi ghi dở",
             "SqlTransaction: trừ kho và tạo chi tiết đơn hoặc cùng thành công, hoặc cùng hủy"],
            ["Nhiều người cùng dùng",
             "Dùng chung tệp, người lưu sau ghi đè người lưu trước",
             "SQL Server quản lý khóa; giao dịch đồng thời được kiểm soát ở mức dòng"],
            ["Tra cứu",
             "Tìm bằng mắt hoặc bộ lọc đơn giản",
             "Truy vấn tham số hóa, tìm tương đối không phân biệt dấu tiếng Việt"],
            ["Đối chiếu khi có khiếu nại",
             "Không có lịch sử đáng tin cậy",
             "Đơn hàng, chi tiết đơn và trạng thái được lưu bền vững, có thể tra theo mã đơn"],
            ["Chi phí vận hành",
             "Thấp ban đầu, tăng nhanh khi số đơn tăng",
             "Một lần triển khai, hai phân hệ dùng chung một cơ sở dữ liệu"],
        ],
    }),

    ("h3", "1.1.3. Khoảng trống mà đồ án cần lấp đầy"),

    ("p",
     "Từ phân tích trên, có thể xác định khoảng trống không phải là “thiếu một ứng dụng” mà là thiếu sự đồng "
     "bộ giữa hai nhu cầu có tính chất rất khác nhau. Nhu cầu thứ nhất là bán hàng: khách hàng ở xa, dùng "
     "điện thoại, cần nhìn thấy ảnh, giá, còn hàng hay không, chọn cỡ và màu, đặt hàng trong vài phút. Nhu "
     "cầu thứ hai là vận hành: nhân viên kho ngồi tại cửa hàng, cần nhập hàng chục biến thể trong một buổi, "
     "cần duyệt đơn nhanh bằng bàn phím, cần biết ngay hôm nay có bao nhiêu đơn chưa xử lý. Nếu dùng một "
     "công nghệ duy nhất cho cả hai, một trong hai bên sẽ bị ép vào một môi trường không phù hợp: trang web "
     "thuần quản trị khó thao tác nhanh khi nhập kho, còn ứng dụng desktop không thể tiếp cận khách hàng ở xa."),

    ("p",
     "Vì vậy, hướng giải quyết của đồ án là tách phần giao tiếp với khách hàng và phần vận hành nội bộ thành "
     "hai ứng dụng độc lập nhưng chia sẻ đúng một cơ sở dữ liệu: website bán hàng xây dựng bằng ASP.NET Core "
     "MVC phục vụ khách hàng, và ứng dụng quản trị xây dựng bằng C# WinForms phục vụ nhân viên. Việc dùng "
     "chung một cơ sở dữ liệu là điều kiện để mọi thay đổi về kho và đơn hàng do một bên thực hiện được bên "
     "kia nhìn thấy ngay, đồng thời tránh được bài toán đồng bộ dữ liệu giữa hai kho dữ liệu riêng – một bài "
     "toán phức tạp hơn nhiều so với quy mô của đồ án."),

    # ------------------------------------------------------------------ 1.2
    ("h2", "1.2. Mục tiêu nghiên cứu và phạm vi đề tài"),

    ("h3", "1.2.1. Mục tiêu tổng quát"),

    ("p",
     "Mục tiêu tổng quát của đề tài là xây dựng một giải pháp phần mềm quản lý và bán hàng thời trang hoàn "
     "chỉnh ở mức có thể vận hành thật, bao gồm hai phân hệ tương tác với cùng một cơ sở dữ liệu, có kiểm "
     "soát tính toàn vẹn khi xảy ra tranh chấp dữ liệu, và có đủ tài liệu phân tích – thiết kế để một người "
     "khác có thể tiếp nhận và mở rộng. Từ “hoàn chỉnh” ở đây được hiểu theo nghĩa nghiệp vụ: một đơn hàng "
     "phát sinh trên web phải đi được trọn vòng đời từ lúc khách đặt, qua bước nhân viên duyệt, giao hàng, "
     "đến lúc hoàn thành hoặc hủy, và mọi bước đều để lại dấu vết trong cơ sở dữ liệu."),

    ("p",
     "Mục tiêu thứ hai mang tính kỹ thuật: chứng minh khả năng vận dụng đồng thời hai mô hình lập trình trên "
     "nền .NET, gồm mô hình hướng sự kiện của Windows Forms và mô hình MVC của ASP.NET Core, với cùng một "
     "quy tắc nghiệp vụ. Khi quy tắc trừ kho được hiện thực hai lần ở hai công nghệ khác nhau, nhóm buộc "
     "phải trả lời câu hỏi mà một đề tài chỉ dùng một công nghệ thường bỏ qua: quy tắc nào thuộc về tầng dữ "
     "liệu, quy tắc nào thuộc về tầng hiển thị, và làm thế nào để hai phân hệ không đưa ra hai kết luận khác "
     "nhau về cùng một con số tồn kho."),

    ("table", {
        "caption": "1.2: Mục tiêu cụ thể và tiêu chí nghiệm thu tương ứng",
        "cols": ["Mã", "Mục tiêu cụ thể", "Tiêu chí nghiệm thu"],
        "widths": [1.5, 8.0, 6.5],
        "font": 11,
        "aligns": ["c", "l", "l"],
        "rows": [
            ["MT1",
             "Quản lý danh mục và sản phẩm ở cấp mẫu hàng, có phân loại theo loại thời trang",
             "Thêm, sửa, ngừng bán được; danh mục có kiểm tra trùng tên; sản phẩm hiển thị được trên web"],
            ["MT2",
             "Quản lý biến thể sản phẩm theo tổ hợp màu sắc – kích cỡ kèm số lượng tồn riêng",
             "Mỗi tổ hợp là duy nhất trong một sản phẩm; tồn kho không âm; sửa tồn kho có kiểm soát"],
            ["MT3",
             "Cho phép khách hàng tìm kiếm, lọc theo loại và chọn đúng biến thể trước khi mua",
             "Tìm theo từ khóa không phân biệt dấu; lọc theo danh mục; chọn được cỡ và màu còn hàng"],
            ["MT4",
             "Đặt hàng trực tuyến an toàn về tồn kho, kể cả khi nhiều khách mua cùng lúc",
             "Tạo đơn và trừ kho trong một giao dịch; không bán vượt tồn; mã đơn không trùng"],
            ["MT5",
             "Tiếp nhận và xử lý đơn hàng trên ứng dụng desktop theo đúng vòng đời trạng thái",
             "Chỉ cho phép chuyển trạng thái hợp lệ; hủy đơn hoàn lại tồn kho đúng số lượng"],
            ["MT6",
             "Bảo vệ dữ liệu trước các lỗi nhập liệu và tấn công chèn mã",
             "Toàn bộ truy vấn có tham số hóa; dữ liệu vào được kiểm tra ở cả hai phân hệ"],
            ["MT7",
             "Cung cấp tài liệu phân tích – thiết kế đủ để bàn giao và mở rộng",
             "Có sơ đồ kiến trúc, Use Case, ERD, thiết kế CSDL vật lý và Screen Flow kèm mã Mermaid"],
        ],
    }),

    ("h3", "1.2.2. Phạm vi thực hiện"),

    ("p",
     "Phạm vi chức năng của đồ án tập trung vào bốn nhóm nghiệp vụ cốt lõi: quản lý danh mục, quản lý sản phẩm "
     "và biến thể, bán hàng trực tuyến, và xử lý đơn hàng. Bốn nhóm này tương ứng với bốn Use Case được đặc "
     "tả chi tiết ở Chương 3, cộng thêm chức năng tra cứu đơn hàng dành cho khách đã mua và chức năng đăng "
     "nhập quản trị có phân quyền. Nhóm chủ động giới hạn phạm vi như vậy để mỗi chức năng đều có thể được "
     "kiểm thử tới cùng thay vì có nhiều chức năng nhưng chỉ chạy được ở mức minh họa."),

    ("p",
     "Những phần nằm ngoài phạm vi được xác định rõ ngay từ đầu để tránh hiểu nhầm khi đánh giá sản phẩm: "
     "đồ án không tích hợp cổng thanh toán trực tuyến (thanh toán khi nhận hàng và chuyển khoản được ghi nhận "
     "thủ công), không có chức năng quản lý nhà cung cấp và phiếu nhập kho theo lô, không có báo cáo doanh thu "
     "theo kỳ dạng biểu đồ nâng cao, không gửi email hay tin nhắn tự động, và không xử lý ảnh tải lên từ trình "
     "duyệt. Những điểm này được trình bày lại trong mục 3.4.5 như một phần của kế hoạch phát triển tiếp."),

    # ------------------------------------------------------------------ 1.3
    ("h2", "1.3. Phương pháp thực hiện và mô hình hệ thống"),

    ("h3", "1.3.1. Kiến trúc tổng thể của hệ thống"),

    ("p",
     "Hệ thống được tổ chức thành ba khối: phân hệ khách hàng (website ASP.NET Core MVC), phân hệ quản trị "
     "(ứng dụng Windows Forms), và cơ sở dữ liệu SQL Server 2022 dùng chung. Hai phân hệ ứng dụng không gọi "
     "trực tiếp vào nhau, không chia sẻ tệp cấu hình và không trao đổi tệp tin; chúng chỉ giao tiếp gián tiếp "
     "qua cơ sở dữ liệu. Đây là quyết định kiến trúc quan trọng nhất của đồ án, vì nó biến cơ sở dữ liệu thành "
     "nguồn sự thật duy nhất (single source of truth) và cho phép mỗi phân hệ được triển khai, khởi động lại "
     "hoặc nâng cấp độc lập mà không ảnh hưởng tới phân hệ còn lại."),

    ("p",
     "Về mặt triển khai, cả ba khối chạy trên cùng một máy trong môi trường phát triển: website lắng nghe trên "
     "cổng 5193, ứng dụng desktop là một tiến trình WinForms, và SQL Server chạy trong container Docker tên "
     "mssql2022 với cổng 1433 chỉ mở trên địa chỉ loopback 127.0.0.1. Việc đóng gói cơ sở dữ liệu trong Docker "
     "mang lại lợi ích thực tế: mọi thành viên trong nhóm có cùng một phiên bản SQL Server, cùng bộ script khởi "
     "tạo, nên lỗi “chạy được trên máy tôi nhưng không chạy trên máy bạn” gần như bị loại bỏ."),

    ("figure", {
        "caption": "1.1: Kiến trúc tổng thể của hệ thống quản lý và bán hàng thời trang",
        "file": "diagrams/01_system_architecture.mmd",
        "note": "Khách hàng, nhân viên và cơ sở dữ liệu dùng chung; hai phân hệ không gọi trực tiếp vào nhau",
        "height": 6.8,
    }),

    ("p",
     "Hình 1.1 cho thấy hai đường truy cập dữ liệu hoàn toàn tách biệt. Khách hàng chỉ đi qua trình duyệt tới "
     "website; nhân viên chỉ đi qua ứng dụng desktop; cả hai đường đều kết thúc tại cùng một cơ sở dữ liệu "
     "FashionShopDb. Sự tách biệt này cũng là ranh giới bảo mật: website không có quyền đọc bảng Users, còn "
     "ứng dụng desktop không mở bất kỳ cổng mạng nào ra ngoài."),

    ("table", {
        "caption": "1.3: Phân rã chức năng theo phân hệ của hệ thống",
        "cols": ["Phân hệ", "Đối tượng sử dụng", "Nhóm chức năng chính", "Công nghệ"],
        "widths": [3.0, 3.2, 6.0, 3.8],
        "font": 11,
        "bold_first_col": True,
        "rows": [
            ["Web bán hàng\n(WebBanHang)",
             "Khách hàng mua lẻ",
             "Xem trang chủ, danh sách sản phẩm, chi tiết sản phẩm và chọn biến thể, giỏ hàng, đặt hàng, tra cứu đơn",
             "ASP.NET Core MVC 8, Razor, Bootstrap 5, Session"],
            ["Quản trị Desktop\n(QuanLyDesktop)",
             "Nhân viên, quản trị viên",
             "Đăng nhập và phân quyền, dashboard tổng quan, quản lý danh mục, sản phẩm, biến thể, xử lý đơn hàng",
             "C# WinForms .NET 8, ADO.NET"],
            ["Cơ sở dữ liệu\ntrung tâm",
             "Cả hai phân hệ",
             "Lưu danh mục, sản phẩm, biến thể và tồn kho, khách hàng, đơn hàng, chi tiết đơn, người dùng và vai trò",
             "SQL Server 2022 (Docker), T-SQL"],
        ],
    }),

    ("h3", "1.3.2. Luồng truyền nhận dữ liệu giữa hai phân hệ"),

    ("p",
     "Do không có lời gọi trực tiếp giữa hai phân hệ, mọi tương tác diễn ra theo ba dạng luồng. Dạng thứ nhất "
     "là luồng đọc – hiển thị: nhân viên nhập một biến thể mới trên ứng dụng desktop, dữ liệu được ghi vào bảng "
     "ProductVariants; ngay sau đó khách hàng mở trang chi tiết sản phẩm trên web, website truy vấn lại bảng "
     "này và thấy tồn kho đã thay đổi, không cần bất kỳ thao tác đồng bộ nào. Dạng thứ hai là luồng ghi có "
     "tranh chấp: khách đặt hàng trên web làm giảm tồn kho, trong khi nhân viên có thể đang mở form biến thể; "
     "điều kiện trừ kho và điều kiện cập nhật được đặt ngay trong câu lệnh SQL để cơ sở dữ liệu là bên phân "
     "xử thắng – thua, thay vì để mã ứng dụng tự phán đoán trên dữ liệu đã cũ."),

    ("p",
     "Dạng thứ ba là luồng chuyển trạng thái: đơn hàng do khách tạo trên web ở trạng thái Chờ xử lý; nhân viên "
     "mở form đơn hàng trên desktop để duyệt, chuyển sang Đang giao rồi Hoàn thành; nếu hủy đơn, tồn kho được "
     "hoàn lại theo đúng các biến thể đã trừ. Khách hàng theo dõi các bước này qua chức năng tra cứu đơn hàng "
     "trên web, nơi trạng thái được ánh xạ thành một thanh tiến trình bốn bước. Như vậy, mỗi phân hệ vừa là bên "
     "ghi vừa là bên đọc, và tính đúng đắn của hệ thống phụ thuộc vào việc cả hai cùng tuân thủ một tập quy tắc "
     "dữ liệu thay vì mỗi bên tự định nghĩa quy tắc riêng."),

    ("bul", [
        "Luồng đọc – hiển thị: ghi ở phân hệ này, đọc ở phân hệ kia, không cần đồng bộ vì dùng chung bảng dữ liệu.",
        "Luồng ghi có tranh chấp: điều kiện nghiệp vụ được đưa xuống câu lệnh SQL (trừ kho có điều kiện, cập nhật "
        "theo trạng thái hiện tại) để tránh mất cập nhật khi hai người thao tác cùng lúc.",
        "Luồng chuyển trạng thái: danh sách trạng thái kế tiếp được giới hạn trong mã nguồn, mỗi lần chuyển là "
        "một giao dịch có kiểm tra trạng thái nguồn.",
    ]),

    ("h3", "1.3.3. Phương pháp thực hiện"),

    ("p",
     "Nhóm áp dụng cách tiếp cận tăng dần: khảo sát nghiệp vụ, dựng cơ sở dữ liệu trước, sau đó xây dựng từng "
     "chức năng theo chiều dọc – nghĩa là mỗi chức năng được hoàn thiện từ bảng dữ liệu, câu lệnh truy vấn, xử "
     "lý nghiệp vụ đến giao diện, thay vì làm toàn bộ giao diện trước rồi mới nối dữ liệu. Cách làm này tốn "
     "công ở giai đoạn đầu nhưng giúp phát hiện sớm các thiếu sót trong thiết kế bảng, đặc biệt là thiếu sót "
     "về ràng buộc duy nhất và ràng buộc miền giá trị – những lỗi nếu phát hiện muộn sẽ buộc phải sửa cả hai "
     "phân hệ."),

    ("p",
     "Công cụ hỗ trợ gồm: Git cho quản lý phiên bản, Docker Compose cho cơ sở dữ liệu, Visual Studio cho hai dự "
     "án .NET, và Mermaid cho sơ đồ. Sơ đồ được lưu dưới dạng văn bản trong thư mục diagrams/ để có thể so sánh "
     "thay đổi bằng Git – một lợi thế mà ảnh chụp màn hình không có."),

    # ------------------------------------------------------------------ 1.4
    ("h2", "1.4. Quy trình phát triển phần mềm theo mô hình Scrum"),

    ("p",
     "Đồ án được thực hiện trong thời gian ngắn, yêu cầu thay đổi thường xuyên và nhóm làm việc phân tán về "
     "mặt thời gian, nên nhóm chọn Scrum làm khung quản lý công việc thay vì mô hình thác nước. Scrum không "
     "phải là một tập thủ tục hành chính; điểm cốt lõi của Scrum là chia nhỏ sản phẩm thành các phần có thể "
     "chạy được trong một khoảng thời gian cố định, và sau mỗi khoảng thời gian đó buộc phải có một sản phẩm "
     "thực sự hoạt động để đánh giá lại hướng đi."),

    ("h3", "1.4.1. Lý thuyết Scrum áp dụng trong đồ án"),

    ("p",
     "Scrum được xây dựng trên ba trụ cột: minh bạch, thanh tra và thích nghi. Minh bạch nghĩa là mọi thành "
     "viên nhìn thấy cùng một danh sách công việc và cùng một định nghĩa “hoàn thành”; thanh tra nghĩa là "
     "cuối mỗi Sprint có buổi đánh giá sản phẩm chạy thật; thích nghi nghĩa là kế hoạch Sprint tiếp theo được "
     "điều chỉnh dựa trên kết quả đó. Trong phạm vi đồ án, nhóm cụ thể hóa các thành phần của Scrum như sau."),

    ("bul", [
        "Product Backlog: danh sách toàn bộ yêu cầu dưới dạng User Story, có thứ tự ưu tiên và ước lượng công "
        "sức; là nguồn duy nhất để chọn việc cho từng Sprint.",
        "Sprint Backlog: tập con các User Story được chọn cho một Sprint, kèm các đầu việc kỹ thuật cần làm để "
        "hoàn thành chúng (tạo bảng, viết truy vấn, dựng giao diện, viết kịch bản kiểm thử).",
        "Sprint Planning: đầu mỗi Sprint, nhóm chọn mục tiêu Sprint, chốt phạm vi và phân rã thành đầu việc "
        "cụ thể trong khoảng một đến hai ngày công.",
        "Daily Scrum: trao đổi ngắn hằng ngày theo ba câu hỏi – đã làm gì, sẽ làm gì, đang vướng gì; tập trung "
        "vào vật cản kỹ thuật chứ không báo cáo tiến độ hình thức.",
        "Sprint Review: cuối Sprint, chạy thử luồng nghiệp vụ trên cơ sở dữ liệu thật trước sự chứng kiến của "
        "cả nhóm, ghi nhận hành vi chưa đúng kỳ vọng.",
        "Sprint Retrospective: rút ra một thay đổi quy trình cụ thể cho Sprint sau, ví dụ “mọi thay đổi cấu trúc "
        "bảng phải kèm script migration” hoặc “không hợp nhất mã nguồn khi chưa chạy lại kịch bản đặt hàng”.",
    ]),

    ("p",
     "Ba vai trò của Scrum cũng được phân định rõ dù nhóm chỉ có ba thành viên: một thành viên đảm nhận vai trò "
     "Product Owner, chịu trách nhiệm nội dung Product Backlog và thứ tự ưu tiên; một thành viên đảm nhận vai "
     "trò Scrum Master, theo dõi vật cản và bảo vệ nhịp làm việc; cả nhóm cùng là nhóm phát triển. Việc một "
     "người kiêm nhiệm nhiều vai trò là thực tế phổ biến ở đồ án sinh viên, nhưng điều quan trọng là các buổi "
     "Review và Retrospective vẫn được tổ chức đúng nhịp, vì đó là lúc các sai lệch về hiểu biết nghiệp vụ được "
     "phát hiện."),

    ("figure", {
        "caption": "1.2: Chu trình Scrum áp dụng cho đồ án",
        "file": "diagrams/02_scrum_process.mmd",
        "note": "Product Backlog → Sprint Backlog → Sprint 1–2 tuần → sản phẩm chạy được → Review và Retrospective",
        "height": 6.2,
    }),

    ("h3", "1.4.2. Kế hoạch Sprint thực tế của đồ án"),

    ("p",
     "Đồ án được chia thành bốn Sprint, mỗi Sprint dài khoảng một tuần rưỡi đến hai tuần. Cách chia này cố tình "
     "để Sprint đầu tiên hoàn toàn thuộc về cơ sở dữ liệu và phân hệ quản trị, vì nếu dữ liệu chưa đúng thì mọi "
     "giao diện xây sau đó đều phải làm lại. Ba Sprint còn lại lần lượt bổ sung khả năng bán hàng, xử lý đơn "
     "hàng và hoàn thiện – kiểm thử."),

    ("table", {
        "caption": "1.4: Phân chia Sprint và sản phẩm bàn giao của từng vòng",
        "cols": ["Sprint", "Mục tiêu Sprint", "Hạng mục bàn giao", "Kết quả đánh giá"],
        "widths": [2.4, 5.6, 5.6, 2.4],
        "font": 10.5,
        "bold_first_col": True,
        "rows": [
            ["Sprint 1\n(2 tuần)",
             "Dựng nền dữ liệu và khung quản trị: cấu trúc 8 bảng, dữ liệu mẫu, đăng nhập, dashboard",
             "CSDL_ShopQuanAo.sql, docker-compose.yml, Form1, FormMain, DatabaseConfig",
             "Đạt – đăng nhập và dashboard chạy trên dữ liệu thật"],
            ["Sprint 2\n(1,5 tuần)",
             "Quản lý danh mục, sản phẩm và biến thể theo màu – cỡ, kèm kiểm tra trùng",
             "FormDanhMuc, FormBienTheSanPham, ràng buộc UNIQUE và CHECK, migration 01_harden_database.sql",
             "Đạt – nhập 30 biến thể mẫu không phát sinh trùng"],
            ["Sprint 3\n(2 tuần)",
             "Website bán hàng: duyệt sản phẩm, chọn biến thể, giỏ hàng và đặt hàng có giao dịch",
             "HomeController, ProductsController, CartController, CheckoutController, SqlOrderRepository",
             "Đạt – đơn 378.000 đồng tạo thành công, tồn kho giảm từ 18 xuống 16"],
            ["Sprint 4\n(1,5 tuần)",
             "Xử lý đơn hàng trên desktop, tra cứu đơn trên web, kiểm thử tích hợp và hoàn thiện báo cáo",
             "FormDonHang, OrderLookupController, bộ kịch bản kiểm thử, báo cáo và thư mục diagrams/",
             "Đạt – vòng đời trạng thái và hoàn kho hoạt động đúng"],
        ],
    }),

    ("p",
     "Một điều chỉnh quan trọng phát sinh sau Sprint 3: kịch bản kiểm thử ban đầu chỉ kiểm tra trường hợp đặt "
     "hàng thành công, chưa kiểm tra trường hợp hai khách cùng mua món hàng cuối cùng. Khi bổ sung kịch bản "
     "này, nhóm phát hiện nếu tách bước đọc tồn kho và bước trừ tồn kho thành hai câu lệnh riêng thì có thể bán "
     "vượt tồn. Giải pháp được đưa vào Sprint 4 là gộp điều kiện vào chính câu lệnh cập nhật và kiểm tra số dòng "
     "bị tác động; nếu bằng không thì hủy toàn bộ giao dịch. Đây là ví dụ điển hình cho giá trị của Sprint "
     "Review: một buổi chạy thử ngắn đã phát hiện lỗi mà đọc mã nguồn không thấy."),

    ("h3", "1.4.3. Bài học rút ra từ quá trình áp dụng Scrum"),

    ("p",
     "Bài học thứ nhất là ưu tiên theo rủi ro chứ không theo cảm giác dễ chịu. Nhóm có xu hướng muốn làm giao "
     "diện trước vì nhìn thấy kết quả ngay, nhưng rủi ro lớn nhất của đồ án nằm ở cấu trúc dữ liệu và ở tính "
     "nhất quán khi hai phân hệ cùng ghi. Đặt phần khó lên trước giúp tránh được việc phải làm lại giao diện."),

    ("p",
     "Bài học thứ hai là định nghĩa “hoàn thành” phải bao gồm kiểm thử, không chỉ bao gồm việc biên dịch thành "
     "công. Trong Sprint 2, một số chức năng đã được đánh dấu hoàn thành khi giao diện hiển thị đúng, nhưng khi "
     "chạy kịch bản nhập trùng biến thể thì phát hiện thông báo lỗi không xuất hiện. Việc bổ sung tiêu chí "
     "“đã chạy qua kịch bản kiểm thử tương ứng” vào định nghĩa hoàn thành giúp giảm số lỗi phát hiện muộn."),

    ("p",
     "Bài học thứ ba là tài liệu phải được viết song song với mã nguồn. Các sơ đồ trong báo cáo được lưu dưới "
     "dạng Mermaid ngay từ khi thiết kế, nên khi cấu trúc bảng thay đổi, sơ đồ được sửa bằng vài dòng văn bản "
     "thay vì phải vẽ lại. Đây cũng là lý do thư mục diagrams/ được đặt trong kho mã nguồn và được quản lý cùng "
     "phiên bản với mã nguồn."),

    ("note",
     "Kết luận Chương 1: bài toán của đồ án không phải là số hóa một quy trình đơn giản, mà là đưa dữ liệu biến "
     "thể thời trang về một mô hình quan hệ đủ chặt để hai phân hệ cùng ghi mà không mâu thuẫn. Ba yêu cầu kỹ "
     "thuật được mang sang Chương 2 để đánh giá công nghệ: cần truy vấn quan hệ mạnh, cần giao dịch ở mức cơ sở "
     "dữ liệu, và cần hai mô hình lập trình khác nhau nhưng chia sẻ được quy tắc nghiệp vụ."),
]
