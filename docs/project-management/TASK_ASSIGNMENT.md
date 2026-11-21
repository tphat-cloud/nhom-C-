# PHÂN CHIA CÔNG VIỆC — ĐỒ ÁN FASHION SHOP

> Tài liệu quản lý dự án — trưởng nhóm **Trịnh Tấn Phát** cập nhật.
> Nhóm: Trịnh Tấn Phát (trưởng), Trần Lê Hồng Nhân, Dương Huỳnh Nam Anh, Võ Thị Ngọc Như
> Phạm vi: **chỉ liệt kê công việc còn tồn đọng**. Những phần đã hoàn thiện
> được ghi nhận ở mục A và **không phân chia lại**.
>
> Bản cập nhật **21/11/2026 (lần 2)** — đối chiếu trực tiếp với `main`: **toàn bộ
> task code đã merge xong** (PR #21–#32). Trước đó trong ngày đã merge **QA-01
> phần 1** (PR #11), **WEB-02** (PR #15), **WEB-06** (PR #14), phần Như phụ trách
> của **WEB-05** (PR #16), **QA-03** (PR #17), **DOC-01** (PR #18) và 8 ca Như
> đóng góp cho **QA-02** (PR #19). Đợt này chỉ còn **QA-02** (chạy tay trên DB
> thật) và **DOC-02** (báo cáo/slide).

---

## A. TỔNG QUAN TÌNH TRẠNG DỰ ÁN

### Mức độ hoàn thiện
Hệ thống **đã gần hoàn thiện và chạy được thực tế**. Kiến trúc 2 ứng dụng
dùng chung CSDL (WinForms `QuanLyDesktop` + ASP.NET Core MVC `WebBanHang`,
SQL Server 2022 qua Docker) đã hình thành đầy đủ, luồng nghiệp vụ cốt lõi
(đăng nhập, sản phẩm, danh mục, biến thể size/màu, phiếu nhập kho, duyệt đơn,
giỏ hàng, đặt hàng, tra cứu đơn) đã đi từ giao diện xuống tới dữ liệu thật.

Tính đến 21/11/2026, **toàn bộ công việc code đã hoàn tất và merge vào `main`**:
dashboard có báo cáo tháng, top bán chạy và cảnh báo sắp hết hàng; desktop có
form khách hàng, form tài khoản, xuất CSV đơn hàng và xem trước ảnh; web có sắp
xếp sản phẩm, sản phẩm liên quan, khối đã xem gần đây, trang 404 và favicon.
Phần còn lại chỉ là **nghiệm thu** (chạy QA-02 trên DB thật) và **tài liệu**
(DOC-02).

### ✅ Đã hoàn thành (đã kiểm tra bằng code, không chỉ thấy file)

| Khu vực | Bằng chứng đã xác minh |
|---|---|
| Đăng nhập & phân quyền | `Form1.cs` truy vấn `Users` JOIN `Roles`, kiểm tra `IsActive`; vai trò Admin/Staff |
| Dashboard tổng quan | `FormMain.cs` `LoadDashboardSummary()` — đếm SP đang bán, số đơn hôm nay, doanh thu hôm nay (đơn hoàn thành/đã thanh toán, loại đơn hủy) |
| CRUD sản phẩm + soft delete | `FormMain.cs`: thêm/sửa, xóa đặt `IsActive = 0`, tìm kiếm theo tên/mã |
| Quản lý danh mục | `FormDanhMuc.cs` |
| Biến thể size/màu/tồn kho | `FormBienTheSanPham.cs` |
| Phiếu nhập kho | `FormNhapKho.cs` + `StockReceipts`/`StockReceiptDetails`, cộng tồn trong transaction, hủy phiếu hoàn tồn (chỉ Admin) |
| Duyệt đơn hàng | `FormDonHang.cs`: máy trạng thái (Chờ xử lý → Đang giao → Hoàn thành / Đã hủy), chuyển trạng thái hợp lệ, **hoàn tồn khi hủy trong cùng transaction** |
| Web: trang chủ / danh sách / chi tiết | `HomeController`, `ProductsController`, `SqlProductRepository` — SQL tham số hóa 100%, tìm kiếm không dấu (`COLLATE Vietnamese_CI_AI`), escape `LIKE` |
| Web: giỏ hàng & đặt hàng | `CartController`/`CheckoutController` + `SqlOrderRepository` — ghi đơn trong 1 transaction (khách → đơn → chi tiết → trừ kho có điều kiện), `[ValidateAntiForgeryToken]`, chặn đặt quá tồn |
| Web: tra cứu đơn hàng | `OrderLookupController` + `Views/OrderLookup/Index.cshtml`, lịch sử đơn qua localStorage, badge trạng thái |
| Database & migration | `CSDL_ShopQuanAo.sql` + `Scripts/migrations/01–04` (CHECK, UNIQUE, index, view `vw_ProductStock`, seed idempotent, băm mật khẩu) |
| Hạ tầng & chạy thực tế | `docker-compose.yml`, `.env.example`, `run-web.cmd`/`run-desktop.cmd`, cấu hình kết nối qua biến môi trường; build sạch |
| Bảo mật mật khẩu (SEC-01) | `PasswordHasher.cs` dùng PBKDF2-HMAC-SHA256 100.000 vòng; `Form1.cs` xác thực qua `PasswordHasher.Verify`; seed và migration 04 đều lưu dạng hash |
| Đổi mật khẩu (SEC-02) | `FormDoiMatKhau.cs` + `.Designer.cs`; PR #10 đã merge vào `main` (`6280b06`) |
| Sản phẩm: Ảnh / Giá gốc / Nổi bật (DES-01) | `FormMain.cs` có `txtImageUrl`, giá gốc, `chkIsFeatured`; cả `INSERT` và `UPDATE` đều ghi 3 cột; web đã hiện `-x%` và giá gạch ngang |
| Web: phân trang danh sách (WEB-01) | `ProductsController` + `SqlProductRepository` nhận `page`/`pageSize`; PR #8 đã merge |
| Web: xử lý lỗi biên (WEB-02) | PR #15 đã merge, cập nhật giỏ hàng, đặt hàng và tra cứu đơn, kèm 7 test trong `Web02EdgeCaseTests.cs` |
| Web: trang 404 và favicon (WEB-06) | PR #14 đã merge, URL sai hiển thị trang 404 tiếng Việt và layout dùng favicon SVG |
| CI build và test (CI-01) | `.github/workflows/ci.yml` build 2 ứng dụng và chạy `dotnet test` trên `windows-latest` từ PR #11 |
| Kiểm thử tự động phần 1 (QA-01) | PR #11 đã merge với 5 test phân trang (`ProductsControllerPaginationTests`) |
| Kiểm thử model sản phẩm (QA-03) | PR #17 đã merge với 27 ca xUnit trong `ProductModelTests.cs` |
| Handbook hướng dẫn sử dụng (DOC-01) | PR #18 đã merge với [hướng dẫn Admin/Staff và khách hàng](../handbook/README.md) kèm 7 ảnh màn hình |
| Desktop: báo cáo tháng + top bán chạy (DES-02) | PR #26; `FormMain.cs` `LoadMonthlyReport()` được gọi khi mở dashboard |
| Desktop: cảnh báo sắp hết hàng (DES-05) | PR #24; thẻ `SẢN PHẨM SẮP HẾT HÀNG` + lưới top 5 đọc từ `vw_ProductStock` |
| Desktop: form quản lý khách hàng (DES-03) | PR #27; `FormKhachHang.cs` + menu `Quản lý khách hàng` |
| Desktop: form quản lý tài khoản (DES-04) | PR #29; `FormTaiKhoan.cs`, tạo user qua `PasswordHasher` |
| Desktop: xuất CSV đơn hàng (DES-06) | PR #32; `FormDonHang.cs` `btnXuatCsv_Click`, ghi UTF-8 **có BOM** |
| Desktop: xem trước ảnh sản phẩm (DES-07) | PR #25; `FormMain.cs` PictureBox cạnh ô URL ảnh |
| Web: sắp xếp sản phẩm (WEB-03) | PR #23; `SqlProductRepository.cs` ORDER BY bằng `CASE` tham số hóa |
| Web: sản phẩm liên quan (WEB-04) | PR #28; `ProductsController` (Detail) + `Detail.cshtml` |
| Web: đã xem gần đây (WEB-05) | PR #16 (JS + partial), đã gắn vào `Detail.cshtml:268` |
| Test giỏ / checkout / tra cứu đơn (QA-01) | PR #30; `Web02EdgeCaseTests.cs` |
| Tài liệu báo cáo/nghiệm thu (bản nháp) | trong `docs/baocao/`: `BaoCao_DoAn_...docx`, `PhanTichYeuCauNghiepVu_...pptx`, `CK_GD1_...DONE.xlsx`, `diagrams/`, `screenshots/` (gitignore) |

### ⚠ Còn lại (cập nhật 21/11/2026 — không còn việc code)

Toàn bộ task code ở mục B đã merge vào `main` qua PR #21–#32. Ba hạng mục còn
lại đều **không phải viết code**:

1. **QA-02 — chạy nghiệm thu thủ công** (CAO). Bộ ca đã soạn đủ trong
   `docs/qa/QA-02.md` nhưng cột "Kết quả thực tế" còn trống; phải chạy trên
   SQL Server thật và điền kết quả. Lưu ý: ca G-02 phải giả lập `IsPaid = 1`
   bằng SQL, vì máy trạng thái không cho hủy đơn đã `Hoàn thành`.
2. **DOC-02 — cập nhật báo cáo/PPT theo code thật** (CAO). Báo cáo, slide và
   bảng đặc tả chưa mô tả các tính năng mới (DES-02…DES-08, WEB-03…WEB-06).
3. **Cập nhật tài liệu quản lý** (THẤP). Bảng tiến độ và checklist nghiệm thu
   phải phản ánh đúng trạng thái đã merge (bản này đã cập nhật).

---

## B. DANH SÁCH CÔNG VIỆC (cập nhật 21/11/2026 — lần 2, đã đối chiếu `main`)

| Mã Task | Công việc | Mô tả | File chính | Người phụ trách | Ưu tiên | Trạng thái |
|---|---|---|---|---|---|---|
| SEC-01 | Băm mật khẩu | PBKDF2 + migration băm lại seed | `PasswordHasher.cs`, `Form1.cs`, `Scripts/migrations/04` | Phát | Cao | ✅ PR #5 |
| SEC-02 | Đổi mật khẩu | Form đổi mật khẩu, kiểm tra mật khẩu cũ | `FormDoiMatKhau.*` | Phát | Trung bình | ✅ PR #10 |
| DES-01 | Ảnh / Giá gốc / Nổi bật | 3 trường trong form sản phẩm | `FormMain.cs` | Nhân | Trung bình | ✅ |
| WEB-01 | Phân trang sản phẩm | `page`/`pageSize` | `ProductsController.cs`, `SqlProductRepository.cs` | Nam Anh | Thấp | ✅ PR #8 |
| DB-01 | Ràng buộc & seed | Seed khớp migration, cùng dạng hash | `CSDL_ShopQuanAo.sql`, `Scripts/migrations` | Phát | Trung bình | ✅ |
| WEB-02 | Rà soát UX & lỗi biên | Empty cart, số lượng 0/âm, `returnUrl` lạ, thông báo lỗi thân thiện | `CartController.cs`, `CheckoutController.cs`, `Views/Cart`, `Views/Checkout` | Nam Anh | **Cao** | ✅ PR #15 |
| WEB-03 | **Sắp xếp danh sách sản phẩm** | Thêm lựa chọn "Nổi bật / Mới nhất / Giá tăng dần / Giá giảm dần" ở `/Products`; `sort` truyền xuống SQL bằng `CASE` tham số hóa (không nối chuỗi); giữ `keyword`, `categoryId`, `page`, `pageSize` khi chuyển trang | `IProductRepository.cs`, `SqlProductRepository.cs`, `ProductsController.cs`, `ProductListViewModel.cs`, `Views/Products/Index.cshtml`, `WebBanHang.Tests/` (cập nhật fake) | Nam Anh | Trung bình | ✅ PR #23 |
| WEB-04 | **Sản phẩm liên quan** | Khối "Có thể bạn cũng thích" cuối trang chi tiết: tối đa 4 SP cùng danh mục (trừ chính nó), ưu tiên còn hàng, dùng lại `_ProductCard` | `IProductRepository.cs`, `SqlProductRepository.cs`, `ProductsController.cs` (Detail), `Views/Products/Detail.cshtml` | Nam Anh | Trung bình | ✅ PR #28 |
| WEB-05 | **Đã xem gần đây** | Lưu 6 sản phẩm vừa xem vào `localStorage`, hiện khối nhỏ ở trang chi tiết; khối tự ẩn khi rỗng; không đụng SQL | `wwwroot/js/recently-viewed.js` , `Views/Shared/_RecentlyViewed.cshtml`  | Như | Thấp | ✅ PR #16; partial đã gắn vào `Detail.cshtml:268` ở PR #28 |
| WEB-06 | **Trang 404 + favicon** | Gõ sai URL ra trang 404 thân thiện thay vì trang mặc định; thêm favicon và meta chia sẻ mặc định | `Program.cs`, `HomeController.cs`, `Views/Home/NotFound.cshtml` , `Views/Shared/Error.cshtml`, `Views/Shared/_Layout.cshtml` (head), `wwwroot/css/extras.css` , `wwwroot/favicon.svg` | Như | Thấp | ✅ PR #14 |
| DES-02 | Báo cáo tháng / Top bán chạy | Thống kê doanh thu theo tháng + top SP bán chạy trên dashboard | `FormMain.cs` | Nhân | Trung bình | ✅ PR #26 |
| DES-03 | Form Quản lý khách hàng | Form mới: xem/tìm/sửa khách, xem lịch sử đơn, không xóa cứng | `FormKhachHang.cs` (mới) | Nhân | Trung bình | ✅ PR #27 |
| DES-04 | Form Quản lý tài khoản | Thêm/sửa user, gán role, bật/tắt `IsActive` (chỉ Admin), tạo user qua `PasswordHasher` | `FormTaiKhoan.cs` (mới) | Nam Anh | Thấp | ✅ PR #29 |
| DES-05 | **Thẻ "Sắp hết hàng" trên dashboard** | Thẻ đếm số sản phẩm có tổng tồn ≤ 5 + danh sách top 5 cần nhập thêm; đọc từ `vw_ProductStock` | `FormMain.cs` | Nhân | Trung bình | ✅ PR #24 |
| DES-06 | **Xuất CSV đơn hàng** | Nút "Xuất CSV" trong `FormDonHang`: xuất đúng danh sách đang lọc (mã đơn, ngày, khách, SĐT, tổng tiền, trạng thái), ghi UTF-8 **có BOM** để Excel không lỗi tiếng Việt | `FormDonHang.cs`, `FormDonHang.Designer.cs` | Phát | Thấp | ✅ PR #32 |
| DES-07 | **Xem trước ảnh sản phẩm** | `PictureBox` cạnh ô URL ảnh: dán URL/đường dẫn là xem ngay; ảnh lỗi thì hiện ảnh mặc định, **không chặn lưu** | `FormMain.cs` | Nhân | Thấp | ✅ PR #25 |
| DES-08 | **Hiện/ẩn mật khẩu khi đăng nhập** | Nút Hiện/Ẩn cạnh ô mật khẩu, đổi `PasswordChar` giữa `'●'` và hiện chữ; không đổi logic `PasswordHasher.Verify`, không ghi log mật khẩu | `Form1.cs`, `Form1.Designer.cs` | Phát | Thấp | ✅ PR #21 |
| QA-01 | Test tự động (phần còn lại) | Còn các ca giỏ hàng (cộng/giới hạn số lượng), checkout (thiếu thông tin, quá tồn), tra cứu đơn (không tìm thấy) | `WebBanHang.Tests/` | Nam Anh | Trung bình | ✅ PR #11 + #15 + #30 |
| QA-03 | **Test model sản phẩm** | Viết test cho phần logic đã có sẵn: `DiscountPercent`, `StockText`, `IsInStock`, ảnh dự phòng; `TotalPages`, `HasNextPage` của `ProductListViewModel` — file test mới, không đổi interface | `WebBanHang.Tests/ProductModelTests.cs` | Như | Thấp | ✅ PR #17 |
| QA-02 | Kịch bản test thủ công | Bộ test case nghiệm thu từng chức năng + checklist chạy trên SQL Server thật | `docs/qa/QA-02.md` | Phát | Cao | 🔶 Đã tổng hợp `docs/qa/QA-02.md`; cần chạy thật và điền kết quả (Như góp 8 ca ở PR #19) |
| CI-01 | Pipeline CI | Build 2 project + chạy test | `.github/workflows/ci.yml` | Nam Anh | Thấp | ✅ Bật bước test ở PR #11 |
| DOC-01 | Handbook hướng dẫn sử dụng | Tài liệu vận hành cho Admin/Staff + Khách, kèm ảnh màn hình | `docs/handbook/README.md` | Như | Cao | ✅ PR #18 |
| DOC-02 | Tài liệu nghiệm thu & thuyết trình | Cập nhật báo cáo/PPT theo code thật (SEC-02, tính năng mới); chuẩn bị slide demo | `docs/baocao/` | Phát | Cao | Chưa làm |

> Ghi chú trạng thái: ✅ = đã merge vào `main` và đối chiếu kết quả.
> 🔶 = đã có một phần kết quả hoặc đang chờ merge/tích hợp.
> Các mục ✅ **không phân chia lại**, chỉ còn việc chạy tay khi nghiệm thu (mục E).

### 7 tính năng giao diện + 1 bộ test của đợt này (đã hoàn tất)

| Mã | Nơi hiển thị | Người làm | Vì sao chọn |
|---|---|---|---|
| WEB-03 | Trang `/Products` | Nam Anh | Rẻ, rõ kết quả, dùng lại dữ liệu sẵn có; dạy được `ORDER BY` an toàn |
| WEB-04 | Trang chi tiết | Nam Anh | Tăng chiều sâu demo, tái dùng `_ProductCard` |
| WEB-05 | Trang chi tiết | Như | Thuần JS, không đụng SQL — phù hợp thành viên mới |
| WEB-06 | Trang 404 + head layout | Như | Điểm cộng nhỏ nhưng ai xem demo cũng thấy; file riêng (`extras.css`) |
| DES-05 | Dashboard desktop | Nhân | Tận dụng `vw_ProductStock`, hữu ích thật cho cửa hàng |
| DES-07 | Form sản phẩm | Nhân | Nhìn thấy ngay khi nhập liệu, ít rủi ro |
| DES-08 | Form đăng nhập | Phát | Nhỏ, an toàn, ai cũng thấy lúc demo |

**QA-03** không phải tính năng mà là **bộ test model** cho nnhu: viết đúng một
file test mới, không sửa interface nên không xung đột với ai, nhưng tạo được
commit `test(web): ...` riêng và giúp lịch sử GitHub thể hiện đủ 3 mảng
(tính năng web, test, tài liệu) của thành viên mới.

**Tình trạng 21/11/2026**: cả 7 tính năng (WEB-03, WEB-04, WEB-05, WEB-06,
DES-05, DES-07, DES-08) và QA-03 đã merge vào `main` qua PR #14, #16, #17, #21,
#23, #24, #25, #28.

---

## C. CÔNG VIỆC THEO TỪNG THÀNH VIÊN

### 🧑‍💼 Trịnh Tấn Phát — Trưởng nhóm
Phụ trách chéo (bảo mật + tích hợp + nghiệm thu). **Việc code đã xong:**
- **DES-06**: ✅ PR #32 — nút xuất CSV trong `FormDonHang.cs`, ghi UTF-8 có BOM.
- **DES-08**: ✅ PR #21 — hiện/ẩn mật khẩu ở `Form1`.
- **SEC-01 / SEC-02 / DB-01**: ✅ PR #5, #10 và migration 04.
- **Duyệt merge**: đã merge PR #23–#32; `main` có đủ 7 tính năng mới.

**Còn lại (không phải code):**
- **QA-02**: chạy bộ test case trên DB thật, điền cột "Kết quả thực tế".
- **DOC-02**: cập nhật báo cáo/PPT theo code thật, chuẩn bị slide demo.

**Bàn giao**: kết quả QA-02 đã điền, báo cáo & slide cập nhật.

> Lưu ý: Phát sở hữu `FormDonHang.cs` và `Form1.cs` trong đợt này — hai file này
> không ai khác sửa.

### 🧑‍💻 Trần Lê Hồng Nhân — App Desktop (WinForms)
Phụ trách `FormMain.cs` và các form mới của desktop — **đã bàn giao đủ**:
- **DES-05**: ✅ PR #24 — thẻ + danh sách "Sắp hết hàng" trên dashboard.
- **DES-07**: ✅ PR #25 — xem trước ảnh trong form sản phẩm.
- **DES-02**: ✅ PR #26 — báo cáo theo tháng + top sản phẩm bán chạy.
- **DES-03**: ✅ PR #27 — form `FormKhachHang.cs`.

**Bàn giao**: dashboard có cảnh báo tồn kho + báo cáo tháng, form khách hàng và
ảnh xem trước — tất cả đã trên `main`.

> Lưu ý chống xung đột: Nhân là **chủ sở hữu duy nhất `FormMain.cs`**.
> DES-07 từng nằm trong diện được phép cắt nếu thiếu thời gian, nhưng đã xong ở PR #25.

### 🧑‍💻 Dương Huỳnh Nam Anh — Web + Test + CI
Phụ trách `WebBanHang/` và một form desktop mới — **đã bàn giao đủ**:
- **WEB-02**: ✅ PR #15 — rà soát UX và lỗi biên.
- **WEB-03**: ✅ PR #23 — sắp xếp danh sách sản phẩm (`CASE` tham số hóa).
- **WEB-04**: ✅ PR #28 — khối sản phẩm liên quan ở trang chi tiết.
- **DES-04**: ✅ PR #29 — form `FormTaiKhoan.cs` (dùng `PasswordHasher`).
- **QA-01**: ✅ PR #30 — test giỏ/checkout/tra cứu đơn.

**Bàn giao**: cả 5 việc đã có trên `main`, CI build + test chạy theo PR.

> Lưu ý chống xung đột: Nam Anh không sửa `FormMain.cs`, `FormDonHang.cs`,
> `Form1.cs`. Sau khi nâng cấp `IProductRepository`, phải cập nhật luôn
> `FakeProductRepository` trong `WebBanHang.Tests` — nếu không CI sẽ đỏ.

### 🧑‍💻 Võ Thị Ngọc Như — Front-end nhỏ + test + tài liệu
Như đã bàn giao các phần sau trên `main`.

- **WEB-05**: partial và JavaScript cho khối "Đã xem gần đây" (PR #16).
- **WEB-06**: trang 404 thân thiện, favicon và meta chia sẻ mặc định (PR #14).
- **QA-03**: 27 ca kiểm thử model sản phẩm và phân trang (PR #17).
- **DOC-01**: [handbook cho Admin/Staff và khách hàng](../handbook/README.md),
  kèm 7 ảnh màn hình (PR #18).
- **Hỗ trợ QA-02**: [8 ca kiểm thử thủ công](../qa/QA-02-Ngoc-Nhu.md) để Phát
  tổng hợp vào bộ nghiệm thu (PR #19).

> Tất cả hạng mục của Như đã merge vào `main`; không còn việc tồn.

> Lưu ý: Như **không sửa** `ProductsController`, `SqlProductRepository`,
> `ProductListViewModel` hay `Detail.cshtml` — chỉ tạo file mới và sửa
> `_Layout.cshtml`, `HomeController.cs`, `Program.cs`, `Error.cshtml`
> (bốn file này không ai khác đụng trong đợt này).
> Dòng chèn partial `_RecentlyViewed` vào `Detail.cshtml` do Nam Anh thêm
> **sau khi WEB-04 merge**.

---

## D. THỨ TỰ THỰC HIỆN

### Đợt 1 — gỡ điểm nghẽn ✅
1. **WEB-02** (Nam Anh): ✅ PR #15.
2. **QA-02** (Phát): đã soạn xong bộ test case; còn chạy và điền kết quả.
3. **DES-08** (Phát): ✅ PR #21. **DES-05** (Nhân): ✅ PR #24.
4. **PR #11**: ✅ bộ test phân trang + bước chạy test trong CI.

### Đợt 2 — tính năng giao diện ✅
- Nam Anh: **WEB-03** ✅ PR #23 → **WEB-04** ✅ PR #28.
- Nhân: **DES-07** ✅ PR #25 → **DES-02** ✅ PR #26.
- Phát: **DES-06** ✅ PR #32.
- Như: **WEB-05** ✅ PR #16, **WEB-06** ✅ PR #14, **QA-03** ✅ PR #17.

### Đợt 3 — phần nặng ✅
- Nhân: **DES-03** ✅ PR #27 (form khách hàng).
- Nam Anh: **DES-04** ✅ PR #29 + **QA-01** ✅ PR #30.
- Như: **DOC-01** ✅ PR #18.

### Đợt 4 — nghiệm thu (còn lại)
- **Phát**: chạy **QA-02** theo thứ tự gộp ca, điền cột "Kết quả thực tế".
- **Phát**: hoàn thiện **DOC-02** (báo cáo + slide theo code thật).
- **Cả nhóm**: thuyết trình thử theo kịch bản phân vai.

### Phụ thuộc
- **WEB-03 ⟵ PR #11**: vì thêm tham số vào `IProductRepository.GetProductsAsync`
  và phải sửa `FakeProductRepository` cùng lúc.
- **WEB-04 ⟵ WEB-03**: cùng sửa `SqlProductRepository.cs` / `ProductsController.cs`.
- **WEB-05 ⟵ WEB-04**: dòng chèn partial vào `Detail.cshtml` do Nam Anh thêm sau (đã xong ở PR #28).
- **WEB-06, QA-03**: độc lập, không chờ ai — nhưng **QA-03 ⟵ PR #11** (phải có
  project `WebBanHang.Tests` trên `main` trước khi thêm file test mới).
- **DES-04 ⟵ SEC-01**: dùng chung `PasswordHasher` (đã xong).
- **DOC-02 ⟵ tất cả tính năng mới**: mô tả trong báo cáo phải khớp code thật.
- **CI-01 ⟵ QA-01**: pipeline đã bật test ở PR #11, các test thêm sau tự chạy.

### Có thể làm song song (không chồng file)
- Nhân: `FormMain.cs` (DES-05, DES-07, DES-02) → `FormKhachHang.cs` (DES-03).
- Nam Anh: web (`Products*`, `IProductRepository`) → `FormTaiKhoan.cs` (DES-04).
- Phát: `FormDonHang.cs` (DES-06) → `Form1.cs` (DES-08) → tài liệu.

### Mốc trước ngày nộp
- ✅ 7 tính năng mới (WEB-03…06, DES-05…08) + QA-03 đã merge; còn chạy thật 1 lần khi nghiệm thu.
- **QA-02** chạy hết, mọi mục trong checklist E được tick bằng kết quả thật.
- **DOC-02** chốt bản cuối cho thuyết trình.

### Chiến lược merge (áp dụng cho cả nhóm)
**Nguyên tắc: merge sớm, merge nhỏ theo từng tính năng — KHÔNG đợi xong hết mới gộp.**

Một PR chỉ được merge khi đủ 5 điều kiện:
1. `dotnet build QuanLyShopQuanAo.slnx` sạch (0 warning / 0 error).
2. Đã **chạy thử tính năng đó thật** (AGENTS.md mục 5), không đánh dấu xong chỉ vì đã viết code.
3. Không phá tính năng cũ (web đặt hàng, desktop duyệt đơn, tồn kho vẫn chạy).
4. Đúng phạm vi file của task, không "tiện tay" sửa file người khác.
5. Được trưởng nhóm (Phát) hoặc chủ module review.

Riêng thay đổi **liên thông 2 app, bảo mật, hoặc đổi interface dùng chung**
(`IProductRepository`) thì gom thành 1 PR trọn vẹn: code + test cùng lúc, để
không có commit lửng làm CI đỏ.

### Lịch sử commit gợi ý (để GitHub nhìn "sạch" và đủ đóng góp)

Quy ước: **1 task = 1 branch = 1 PR**; trong PR có thể 2–3 commit nhỏ nhưng
mỗi commit phải build được. Tên PR trùng commit chính.

| Task | Branch | Commit / PR title |
|---|---|---|
| WEB-02 | `fix/web-02-validation` (PR #15) | `fix(web): hoan thien validation gio hang checkout va tra cuu` |
| WEB-03 | `web-03-sap-xep` (PR #23) | `feat(web): sắp xếp sản phẩm theo giá và độ mới` — ✅ |
| WEB-04 | `web-04-san-pham-lien-quan` (PR #28) | `feat(web): hiển thị sản phẩm liên quan ở trang chi tiết` — ✅ |
| WEB-05 | `web-05-da-xem-gan-day` (PR #16) | `feat(web): lưu và hiển thị sản phẩm đã xem gần đây` — ✅ |
| WEB-06 | `web-06-trang-loi` (PR #14) | `feat(web): trang 404 thân thiện và favicon` — ✅ |
| QA-01 | `test/web-checkout-order` (PR #30) | `test(web): kiểm thử giỏ hàng, đặt hàng và tra cứu đơn` — ✅ |
| QA-03 | `test/model-san-pham` (PR #17) | `test(web): kiểm thử model sản phẩm và phân trang` — ✅ |
| DES-02 | `des-02-bao-cao-thang` (PR #26) | `feat(desktop): báo cáo doanh thu theo tháng và top bán chạy` — ✅ |
| DES-03 | `des-03-khach-hang` (PR #27) | `feat(desktop): thêm form quản lý khách hàng` — ✅ |
| DES-04 | `des-04-tai-khoan` (PR #29) | `feat(desktop): thêm form quản lý tài khoản` — ✅ |
| DES-05 | `des-05-sap-het-hang` (PR #24) | `feat(desktop): cảnh báo sản phẩm sắp hết hàng trên dashboard` — ✅ |
| DES-06 | `des-06-xuat-csv` (PR #32) | `feat(desktop): xuất danh sách đơn hàng ra CSV` — ✅ |
| DES-07 | `des-07-xem-anh` (PR #25) | `feat(desktop): xem trước ảnh sản phẩm trong form` — ✅ |
| DES-08 | `des-08-hien-mat-khau` (PR #21) | `feat(desktop): thêm nút hiện/ẩn mật khẩu khi đăng nhập` — ✅ |
| DOC-01 | `docs/handbook` (PR #18) | `docs: thêm handbook hướng dẫn sử dụng` — ✅ |
| DOC-02 | `docs/cap-nhat-bao-cao` | `docs: cập nhật báo cáo và slide theo code thật` — chưa làm |
| QA-02 | `docs/test-case` | `docs: bổ sung bộ test case nghiệm thu` — đã soạn, còn chạy |

Lưu ý khi commit: không gộp nhiều task vào một commit; không commit `bin/obj`,
`.env`, ảnh chụp tạm; mỗi PR chỉ sửa file thuộc task của mình (xem cột
"File chính" ở mục B).

---

## E. CHECKLIST NGHIỆM THU CUỐI CÙNG

> Quy ước (21/11/2026): `[x]` = đã xác minh bằng code trên `main`, test tự động
> hoặc PR đã merge (ghi rõ nguồn). `[ ]` = còn phải chạy tay trên DB thật trong
> đợt QA-02, hoặc chưa làm (ghi rõ lý do).

### Chức năng
- [x] Đăng nhập Admin/Staff, phân quyền đúng; nút hiện/ẩn mật khẩu hoạt động (DES-08) — PR #21, `Form1.cs`; kiểm tay ở QA-02 I-04
- [x] Sản phẩm: thêm/sửa/xóa (soft delete) + **Ảnh/Giá gốc/Nổi bật** chỉnh được và hiển thị trên web (DES-01); ảnh xem trước đúng (DES-07) — `FormMain.cs`, PR #25
- [x] Danh mục, biến thể size/màu/tồn kho — `FormDanhMuc.cs`, `FormBienTheSanPham.cs`
- [x] Phiếu nhập kho: lưu cộng tồn, hủy hoàn tồn (chỉ Admin) — `FormNhapKho.cs:619`
- [x] Duyệt đơn: chuyển trạng thái, hoàn tồn khi hủy; xuất CSV đúng danh sách đang lọc (DES-06) — `FormDonHang.cs:187, 418`, PR #32
- [x] Dashboard: 3 chỉ số hôm nay + báo cáo tháng/top bán chạy (DES-02) + cảnh báo sắp hết hàng (DES-05) — `FormMain.cs:295-297`, PR #24, #26
- [x] Quản lý khách hàng: tìm/sửa/xem lịch sử đơn (DES-03) — PR #27
- [x] Quản lý tài khoản: thêm user, gán role, bật/tắt (DES-04) — PR #29
- [x] Web: trang chủ / danh sách (phân trang WEB-01, sắp xếp WEB-03) / chi tiết (liên quan WEB-04, đã xem gần đây WEB-05) — `Detail.cshtml:268`, PR #23, #28
- [x] Web: giỏ hàng & đặt hàng (transaction, chặn quá tồn) — `SqlOrderRepository.cs:62-89`
- [x] Web: tra cứu đơn + lịch sử localStorage — `OrderLookupController.cs`, `Views/OrderLookup/Index.cshtml`

### Backend
- [x] Không còn so sánh/lưu mật khẩu thuần (SEC-01) — `Form1.cs:50` (`PasswordHasher.Verify`); seed + migration 04 đều lưu hash
- [x] Ghi đơn & nhập/xuất kho an toàn giao dịch, không bán quá tồn — `SqlOrderRepository.cs:62-89` (rollback khi thiếu tồn)
- [x] SQL tham số hóa 100%, kể cả `ORDER BY` của sắp xếp mới (WEB-03) — `SqlProductRepository.cs:125-150`

### Frontend
- [x] UX giỏ/checkout/tra cứu thân thiện, báo lỗi rõ (WEB-02) — PR #15
- [x] Ảnh sản phẩm & mặc định `no-image.svg` hoạt động — `wwwroot/images/no-image.svg`
- [x] Định dạng giá Việt Nam nhất quán — `Program.cs:49`, `site.js:481` (`vi-VN`)
- [x] Gõ sai URL ra trang 404 thân thiện, favicon hiển thị trên tab (WEB-06) — PR #14
- [x] Khối "Đã xem gần đây" ẩn khi rỗng và không lỗi khi localStorage bị chặn — `recently-viewed.js:17-19`

### Database
- [ ] Chạy `CSDL_ShopQuanAo.sql` trên máy mới thành công — chờ QA-02 chặng 0 (J-01)
- [ ] Migration 01–04 idempotent, không lỗi khi chạy lại — chờ QA-02 (J-02)
- [x] Ràng buộc (CHECK/UNIQUE/index) và seed nhất quán sau SEC-01 (DB-01) — `CSDL_ShopQuanAo.sql:74, 106-109, 226-227`

### Tích hợp hệ thống
- [ ] Web đặt hàng → Desktop thấy đơn & trừ kho đúng — chờ QA-02 (J-03)
- [ ] Desktop hủy đơn → hoàn tồn, web hiển thị đúng tình trạng hàng — chờ QA-02 (J-04)
- [ ] Build solution sạch (0 warning / 0 error) — **chưa chạy lại** trong phiên cập nhật tài liệu; CI build ở PR #11

### Kiểm thử
- [ ] Test case thủ công QA-02 chạy hết, đạt — **đang mở**: cột kết quả còn trống
- [ ] `dotnet test` xanh: 5 test phân trang + model sản phẩm (QA-03) + test giỏ/checkout/tra cứu (QA-01) — **chưa chạy lại** trong phiên này

### CI/CD và triển khai (nếu đề cương yêu cầu)
- [x] GitHub Actions build 2 project + chạy test (CI-01) — `.github/workflows/ci.yml`, bật bước test ở PR #11
- [ ] `docker compose up -d` + `run-web.cmd` chạy được trên máy mới — chờ QA-02 chặng 0

### Tài liệu báo cáo
- [ ] Báo cáo `.docx` cập nhật khớp code thật (kể cả 7 tính năng mới) — DOC-02 chưa làm
- [x] Handbook hướng dẫn sử dụng (DOC-01) — PR #18
- [ ] Sơ đồ/Use case/ERD/screenshot đối chiếu được — chờ DOC-02

### Chuẩn bị thuyết trình
- [ ] Slide (PPT) cập nhật, có phần bảo mật & tính năng mới — chờ DOC-02
- [ ] Kịch bản demo phân vai theo 4 người, chạy thử suôn sẻ — chưa làm
- [ ] Dữ liệu demo chuẩn bị sẵn (đơn, phiếu nhập, tài khoản) — chưa làm
