# PHÂN CHIA CÔNG VIỆC — ĐỒ ÁN FASHION SHOP

> Tài liệu quản lý dự án — trưởng nhóm **Trịnh Tấn Phát** cập nhật.
> Nhóm: Trịnh Tấn Phát (trưởng), Trần Lê Hồng Nhân, Dương Huỳnh Nam Anh,
> Nguyễn Như (**nnhu**, mới tham gia).
> Phạm vi: **chỉ liệt kê công việc còn tồn đọng**. Những phần đã hoàn thiện
> được ghi nhận ở mục A và **không phân chia lại**.
>
> Bản cập nhật **19/11/2026** (trước đó 13/11/2026): đã merge **SEC-02** (PR #10,
> `FormDoiMatKhau`), đang chờ merge **QA-01 phần 1** (PR #11 — 5 test phân trang +
> bật `dotnet test` trong CI), và bổ sung **7 tính năng giao diện nhỏ**
> (WEB-03…06, DES-05…08) cùng **1 bộ test model** (QA-03) để chia đều việc code
> cho cả nhóm — mỗi thành viên đều có commit riêng trên GitHub.

---

## A. TỔNG QUAN TÌNH TRẠNG DỰ ÁN

### Mức độ hoàn thiện
Hệ thống **đã gần hoàn thiện và chạy được thực tế**. Kiến trúc 2 ứng dụng
dùng chung CSDL (WinForms `QuanLyDesktop` + ASP.NET Core MVC `WebBanHang`,
SQL Server 2022 qua Docker) đã hình thành đầy đủ, luồng nghiệp vụ cốt lõi
(đăng nhập, sản phẩm, danh mục, biến thể size/màu, phiếu nhập kho, duyệt đơn,
giỏ hàng, đặt hàng, tra cứu đơn) đã đi từ giao diện xuống tới dữ liệu thật.

Phần còn thiếu hiện nay: **một branch web đang treo chưa mở PR** (WEB-02),
**báo cáo thống kê nâng cao**, **hai form quản lý** (khách hàng, tài khoản),
**phần test tự động còn lại**, và **các hạng mục nghiệm thu** (test case,
handbook, báo cáo/slide). Ngoài ra đợt này bổ sung **7 tính năng giao diện nhỏ**
để mỗi thành viên đều có phần code rõ ràng, demo được trước hội đồng.

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
| CI build (CI-01) | `.github/workflows/ci.yml` restore + build 2 project trên `windows-latest`; run trên `main` kết luận **success** |
| Kiểm thử tự động phần 1 (QA-01) | `WebBanHang.Tests` + 5 test phân trang (`ProductsControllerPaginationTests`), đã chạy lại tại máy: **5/5 pass**; PR #11 đang chờ merge |
| Tài liệu báo cáo/nghiệm thu (bản nháp) | trong `docs/baocao/`: `BaoCao_DoAn_...docx`, `PhanTichYeuCauNghiepVu_...pptx`, `CK_GD1_...DONE.xlsx`, `diagrams/`, `screenshots/` (gitignore) |

### ⚠ Còn thiếu / cần khắc phục (đã xác minh)

1. **WEB-02 đang treo** (CAO). Branch `web-02-validation` đã push nhưng **chưa mở PR**
   và cũ hơn `main` — dễ bị bỏ sót trước ngày nộp.
2. **Test tự động mới phủ phần phân trang** (TRUNG BÌNH). Chưa có test cho giỏ
   hàng, đặt hàng, tra cứu đơn.
3. **Dashboard thiếu báo cáo nâng cao** (TRUNG BÌNH). Chỉ có 3 chỉ số "hôm nay";
   chưa có thống kê theo tháng/quý, top bán chạy, cảnh báo sắp hết hàng.
4. **Chưa có form Quản lý khách hàng** (TRUNG BÌNH). `Customers` chỉ được ghi tự
   động khi đặt hàng, không có màn hình xem/tìm/sửa trong app desktop.
5. **Chưa có form Quản lý tài khoản** (THẤP). Không thêm tài khoản mới, bật/tắt
   `IsActive` từ giao diện.
6. **Thiếu các tính năng giao diện nhỏ tạo khác biệt khi demo** (THẤP–TRUNG BÌNH):
   sắp xếp sản phẩm, sản phẩm liên quan, đã xem gần đây, trang 404 thân thiện,
   xem trước ảnh, xuất CSV, hiện/ẩn mật khẩu.
7. **Chưa có trang 404** (THẤP). `Program.cs` mới chỉ có `UseExceptionHandler` cho
   lỗi 500; gõ sai URL hiện trang mặc định của ASP.NET, không có favicon.
8. **Thiếu handbook và tài liệu chưa khớp code** (CAO cho nghiệm thu). README thiên
   về cài đặt kỹ thuật; báo cáo/PPT/bảng đặc tả còn chỗ ghi sai so với code.

---

## B. DANH SÁCH CÔNG VIỆC (cập nhật 19/11/2026)

| Mã Task | Công việc | Mô tả | File chính | Người phụ trách | Ưu tiên | Trạng thái |
|---|---|---|---|---|---|---|
| SEC-01 | Băm mật khẩu | PBKDF2 + migration băm lại seed | `PasswordHasher.cs`, `Form1.cs`, `Scripts/migrations/04` | Phát | Cao | ✅ PR #5 |
| SEC-02 | Đổi mật khẩu | Form đổi mật khẩu, kiểm tra mật khẩu cũ | `FormDoiMatKhau.*` | Phát | Trung bình | ✅ PR #10 |
| DES-01 | Ảnh / Giá gốc / Nổi bật | 3 trường trong form sản phẩm | `FormMain.cs` | Nhân | Trung bình | ✅ |
| WEB-01 | Phân trang sản phẩm | `page`/`pageSize` | `ProductsController.cs`, `SqlProductRepository.cs` | Nam Anh | Thấp | ✅ PR #8 |
| DB-01 | Ràng buộc & seed | Seed khớp migration, cùng dạng hash | `CSDL_ShopQuanAo.sql`, `Scripts/migrations` | Phát | Trung bình | ✅ |
| WEB-02 | Rà soát UX & lỗi biên | Empty cart, số lượng 0/âm, `returnUrl` lạ, thông báo lỗi thân thiện | `CartController.cs`, `CheckoutController.cs`, `Views/Cart`, `Views/Checkout` | Nam Anh | **Cao** | 🔶 Branch `web-02-validation` chưa có PR, cần rebase lên `main` |
| WEB-03 | **Sắp xếp danh sách sản phẩm** | Thêm lựa chọn "Nổi bật / Mới nhất / Giá tăng dần / Giá giảm dần" ở `/Products`; `sort` truyền xuống SQL bằng `CASE` tham số hóa (không nối chuỗi); giữ `keyword`, `categoryId`, `page`, `pageSize` khi chuyển trang | `IProductRepository.cs`, `SqlProductRepository.cs`, `ProductsController.cs`, `ProductListViewModel.cs`, `Views/Products/Index.cshtml`, `WebBanHang.Tests/` (cập nhật fake) | Nam Anh | Trung bình | Chưa làm |
| WEB-04 | **Sản phẩm liên quan** | Khối "Có thể bạn cũng thích" cuối trang chi tiết: tối đa 4 SP cùng danh mục (trừ chính nó), ưu tiên còn hàng, dùng lại `_ProductCard` | `IProductRepository.cs`, `SqlProductRepository.cs`, `ProductsController.cs` (Detail), `Views/Products/Detail.cshtml` | Nam Anh | Trung bình | Chưa làm |
| WEB-05 | **Đã xem gần đây** | Lưu 6 sản phẩm vừa xem vào `localStorage`, hiện khối nhỏ ở trang chi tiết; khối tự ẩn khi rỗng; không đụng SQL | `wwwroot/js/recently-viewed.js` (mới), `Views/Shared/_RecentlyViewed.cshtml` (mới) | nnhu | Thấp | Chưa làm |
| WEB-06 | **Trang 404 + favicon** | Gõ sai URL ra trang 404 thân thiện thay vì trang mặc định; thêm favicon và meta chia sẻ mặc định | `Program.cs`, `HomeController.cs`, `Views/Home/NotFound.cshtml` (mới), `Views/Shared/Error.cshtml`, `Views/Shared/_Layout.cshtml` (head), `wwwroot/css/extras.css` (mới), `wwwroot/favicon.svg` (mới) | nnhu | Thấp | Chưa làm |
| DES-02 | Báo cáo tháng / Top bán chạy | Thống kê doanh thu theo tháng + top SP bán chạy trên dashboard | `FormMain.cs` | Nhân | Trung bình | Chưa làm |
| DES-03 | Form Quản lý khách hàng | Form mới: xem/tìm/sửa khách, xem lịch sử đơn, không xóa cứng | `FormKhachHang.cs` (mới) | Nhân | Trung bình | Chưa làm |
| DES-04 | Form Quản lý tài khoản | Thêm/sửa user, gán role, bật/tắt `IsActive` (chỉ Admin), tạo user qua `PasswordHasher` | `FormTaiKhoan.cs` (mới) | Nam Anh | Thấp | Chưa làm |
| DES-05 | **Thẻ "Sắp hết hàng" trên dashboard** | Thẻ đếm số sản phẩm có tổng tồn ≤ 5 + danh sách top 5 cần nhập thêm; đọc từ `vw_ProductStock` | `FormMain.cs` | Nhân | Trung bình | Chưa làm |
| DES-06 | **Xuất CSV đơn hàng** | Nút "Xuất CSV" trong `FormDonHang`: xuất đúng danh sách đang lọc (mã đơn, ngày, khách, SĐT, tổng tiền, trạng thái), ghi UTF-8 **có BOM** để Excel không lỗi tiếng Việt | `FormDonHang.cs`, `FormDonHang.Designer.cs` | Phát | Thấp | Chưa làm |
| DES-07 | **Xem trước ảnh sản phẩm** | `PictureBox` cạnh ô URL ảnh: dán URL/đường dẫn là xem ngay; ảnh lỗi thì hiện ảnh mặc định, **không chặn lưu** | `FormMain.cs` | Nhân | Thấp | Chưa làm |
| DES-08 | **Hiện/ẩn mật khẩu khi đăng nhập** | Nút con mắt đổi `UseSystemPasswordChar`; không đổi logic `PasswordHasher.Verify`, không ghi log mật khẩu | `Form1.cs`, `Form1.Designer.cs` | Phát | Thấp | Chưa làm |
| QA-01 | Test tự động (phần còn lại) | Đã có 5 test phân trang ở PR #11; còn: giỏ hàng (cộng/giới hạn số lượng), checkout (thiếu thông tin, quá tồn), tra cứu đơn (không tìm thấy) | `WebBanHang.Tests/` | Nam Anh | Trung bình | 🔶 PR #11 chờ merge |
| QA-03 | **Test model sản phẩm** | Viết test cho phần logic đã có sẵn: `DiscountPercent`, `StockText`, `IsInStock`, ảnh dự phòng; `TotalPages`, `HasNextPage` của `ProductListViewModel` — file test mới, không đổi interface | `WebBanHang.Tests/ProductModelTests.cs` (mới) | nnhu | Thấp | Chưa làm |
| QA-02 | Kịch bản test thủ công | Bộ test case nghiệm thu từng chức năng + checklist chạy trên SQL Server thật | `docs/` | Phát | Cao | Chưa làm |
| CI-01 | Pipeline CI | Build 2 project + chạy test; đã xanh phần build | `.github/workflows/ci.yml` | Nam Anh | Thấp | ✅ Build xanh; bước test bật trong PR #11 |
| DOC-01 | Handbook hướng dẫn sử dụng | Tài liệu vận hành cho Admin/Staff + Khách, kèm ảnh màn hình | `docs/` | nnhu | Cao | Chưa làm |
| DOC-02 | Tài liệu nghiệm thu & thuyết trình | Cập nhật báo cáo/PPT theo code thật (SEC-02, tính năng mới); chuẩn bị slide demo | `docs/baocao/` | Phát | Cao | Chưa làm |

> Ghi chú trạng thái: ✅ = đã merge vào `main` và đối chiếu code;
> 🔶 = code đã có nhưng chưa vào `main`; để trống = chưa làm.
> Các mục ✅ **không phân chia lại**, chỉ còn việc chạy tay khi nghiệm thu (mục E).

### 7 tính năng giao diện mới + 1 bộ test trong đợt này

| Mã | Nơi hiển thị | Người làm | Vì sao chọn |
|---|---|---|---|
| WEB-03 | Trang `/Products` | Nam Anh | Rẻ, rõ kết quả, dùng lại dữ liệu sẵn có; dạy được `ORDER BY` an toàn |
| WEB-04 | Trang chi tiết | Nam Anh | Tăng chiều sâu demo, tái dùng `_ProductCard` |
| WEB-05 | Trang chi tiết | nnhu | Thuần JS, không đụng SQL — phù hợp thành viên mới |
| WEB-06 | Trang 404 + head layout | nnhu | Điểm cộng nhỏ nhưng ai xem demo cũng thấy; file riêng (`extras.css`) |
| DES-05 | Dashboard desktop | Nhân | Tận dụng `vw_ProductStock`, hữu ích thật cho cửa hàng |
| DES-07 | Form sản phẩm | Nhân | Nhìn thấy ngay khi nhập liệu, ít rủi ro |
| DES-08 | Form đăng nhập | Phát | Nhỏ, an toàn, ai cũng thấy lúc demo |

**QA-03** không phải tính năng mà là **bộ test model** cho nnhu: viết đúng một
file test mới, không sửa interface nên không xung đột với ai, nhưng tạo được
commit `test(web): ...` riêng và giúp lịch sử GitHub thể hiện đủ 3 mảng
(tính năng web, test, tài liệu) của thành viên mới.

---

## C. CÔNG VIỆC THEO TỪNG THÀNH VIÊN

### 🧑‍💼 Trịnh Tấn Phát — Trưởng nhóm
Phụ trách chéo (bảo mật + tích hợp + nghiệm thu) và 2 việc code nhỏ, file riêng.
- **QA-02**: soạn bộ test case thủ công, chủ trì chạy nghiệm thu end-to-end.
- **DOC-02**: cập nhật báo cáo/PPT theo code thật, chuẩn bị slide demo.
- **DES-06**: nút xuất CSV đơn hàng trong `FormDonHang.cs`.
- **DES-08**: hiện/ẩn mật khẩu ở `Form1`.
- **Duyệt merge**: xử lý branch `web-02-validation` còn treo, giữ `main` build sạch.

**Bàn giao**: bộ test case, báo cáo & slide cập nhật, CSV đơn hàng chạy được.

> Lưu ý: Phát sở hữu `FormDonHang.cs` và `Form1.cs` trong đợt này — hai file này
> không ai khác sửa.

### 🧑‍💻 Trần Lê Hồng Nhân — App Desktop (WinForms)
Phụ trách `FormMain.cs` và các form mới của desktop.
- **DES-05** (làm trước, nhỏ): thẻ + danh sách "Sắp hết hàng" trên dashboard.
- **DES-07**: xem trước ảnh sản phẩm trong form sản phẩm.
- **DES-02**: báo cáo theo tháng + top sản phẩm bán chạy.
- **DES-03**: form mới `FormKhachHang.cs`.

**Bàn giao**: dashboard có thêm cảnh báo tồn kho + báo cáo tháng, form khách hàng
hoàn chỉnh, ảnh xem trước được khi nhập sản phẩm.

> Lưu ý chống xung đột: Nhân là **chủ sở hữu duy nhất `FormMain.cs`**.
> Nếu gần hạn mà chưa kịp, DES-07 được phép cắt (không ảnh hưởng chức năng khác).

### 🧑‍💻 Dương Huỳnh Nam Anh — Web + Test + CI
Phụ trách `WebBanHang/` và một form desktop mới.
- **WEB-02** (ưu tiên 1): rebase branch `web-02-validation` lên `main` rồi mở PR.
- **WEB-03**: sắp xếp danh sách sản phẩm.
- **WEB-04**: khối sản phẩm liên quan ở trang chi tiết.
- **DES-04**: form mới `FormTaiKhoan.cs` (dùng `PasswordHasher` có sẵn).
- **QA-01**: test còn lại cho giỏ/checkout/tra cứu đơn.

**Bàn giao**: WEB-02 vào `main`, hai tính năng web mới, form tài khoản, test xanh.

> Lưu ý chống xung đột: Nam Anh không sửa `FormMain.cs`, `FormDonHang.cs`,
> `Form1.cs`. Sau khi nâng cấp `IProductRepository`, phải cập nhật luôn
> `FakeProductRepository` trong `WebBanHang.Tests` — nếu không CI sẽ đỏ.

### 🧑‍💻 Nguyễn Như (nnhu) — Front-end nhỏ + test + tài liệu
Mới tham gia, nhận các việc **file riêng** để vừa có code vừa không xung đột.
- **WEB-05**: khối "Đã xem gần đây" bằng `localStorage` (2 file mới, không SQL).
- **WEB-06**: trang 404 thân thiện + favicon + head layout (`extras.css` riêng).
- **QA-03**: viết `ProductModelTests.cs` cho `DiscountPercent`, `StockText`,
  `IsInStock`, ảnh dự phòng và `ProductListViewModel.TotalPages`.
- **DOC-01**: handbook hướng dẫn sử dụng cho Admin/Staff và Khách, kèm ảnh.
- **Hỗ trợ QA-02**: góp test case thủ công cho những luồng đã tự chạy thử.

**Bàn giao**: 3 commit code riêng (WEB-05, WEB-06, QA-03) + handbook hoàn chỉnh,
để lịch sử GitHub thể hiện rõ phần đóng góp của thành viên mới.

> Lưu ý: nnhu **không sửa** `ProductsController`, `SqlProductRepository`,
> `ProductListViewModel` hay `Detail.cshtml` — chỉ tạo file mới và sửa
> `_Layout.cshtml`, `HomeController.cs`, `Program.cs`, `Error.cshtml`
> (bốn file này không ai khác đụng trong đợt này).
> Dòng chèn partial `_RecentlyViewed` vào `Detail.cshtml` do Nam Anh thêm
> **sau khi WEB-04 merge**.

---

## D. THỨ TỰ THỰC HIỆN

### Đợt 1 — gỡ điểm nghẽn (1–2 ngày)
1. **WEB-02** (Nam Anh): rebase + mở PR — đang treo, dễ bỏ sót.
2. **QA-02** (Phát): chốt bộ test case để cả nhóm có căn cứ nghiệm thu.
3. **DES-08** (Phát) và **DES-05** (Nhân): hai việc nhỏ, xong sớm để lấy đà.
4. **PR #11**: merge nếu CI xanh — mở đường cho WEB-03 (vì cùng đụng interface).

### Đợt 2 — tính năng giao diện (3–5 ngày)
- Nam Anh: **WEB-03** → **WEB-04** (làm tuần tự vì cùng file repository).
- Nhân: **DES-07** → **DES-02**.
- Phát: **DES-06**.
- nnhu: **WEB-05** → **WEB-06** → **QA-03** song song (file riêng, không chờ ai).

### Đợt 3 — phần nặng còn lại
- Nhân: **DES-03** (form khách hàng).
- Nam Anh: **DES-04** (form tài khoản) + **QA-01** phần còn lại.
- nnhu: **DOC-01** (handbook) — sau khi 3 việc code đã merge.
- Cả nhóm: chạy **QA-02**, hoàn thiện **DOC-01/DOC-02**.

### Phụ thuộc
- **WEB-03 ⟵ PR #11**: vì thêm tham số vào `IProductRepository.GetProductsAsync`
  và phải sửa `FakeProductRepository` cùng lúc.
- **WEB-04 ⟵ WEB-03**: cùng sửa `SqlProductRepository.cs` / `ProductsController.cs`.
- **WEB-05 ⟵ WEB-04**: dòng chèn partial vào `Detail.cshtml` do Nam Anh thêm sau.
- **WEB-06, QA-03**: độc lập, không chờ ai — nhưng **QA-03 ⟵ PR #11** (phải có
  project `WebBanHang.Tests` trên `main` trước khi thêm file test mới).
- **DES-04 ⟵ SEC-01**: dùng chung `PasswordHasher` (đã xong).
- **DOC-02 ⟵ tất cả tính năng mới**: mô tả trong báo cáo phải khớp code thật.
- **CI-01 ⟵ QA-01**: pipeline đã bật test ở PR #11, các test thêm sau tự chạy.

### Có thể làm song song (không chồng file)
- Nhân: `FormMain.cs` (DES-05, DES-07, DES-02) → `FormKhachHang.cs` (DES-03).
- Nam Anh: web (`Products*`, `IProductRepository`) → `FormTaiKhoan.cs` (DES-04).
- Phát: `FormDonHang.cs` (DES-06) → `Form1.cs` (DES-08) → tài liệu.
- nnhu: `recently-viewed.js` (WEB-05) → `extras.css` + trang 404 (WEB-06) →
  `ProductModelTests.cs` (QA-03) → handbook.

### Mốc trước ngày nộp
- **WEB-02** phải rebase, mở PR và merge.
- 7 tính năng mới (WEB-03…06, DES-05…08) + QA-03 merge xong, mỗi cái chạy thật 1 lần.
- **QA-02** chạy hết, mọi mục trong checklist E được tick bằng kết quả thật.
- **DOC-01, DOC-02** chốt bản cuối cho thuyết trình.

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
| WEB-02 | `web-02-validation` (đã có) | `fix(web): chặn số lượng không hợp lệ và làm rõ thông báo lỗi` |
| WEB-03 | `web-03-sap-xep` | `feat(web): sắp xếp sản phẩm theo giá và độ mới` |
| WEB-04 | `web-04-san-pham-lien-quan` | `feat(web): hiển thị sản phẩm liên quan ở trang chi tiết` |
| WEB-05 | `web-05-da-xem-gan-day` | `feat(web): lưu và hiển thị sản phẩm đã xem gần đây` |
| WEB-06 | `web-06-trang-loi` | `feat(web): trang 404 thân thiện và favicon` |
| QA-01 | `test/web-checkout-order` | `test(web): kiểm thử giỏ hàng, đặt hàng và tra cứu đơn` |
| QA-03 | `test/model-san-pham` | `test(web): kiểm thử model sản phẩm và phân trang` |
| DES-02 | `des-02-bao-cao-thang` | `feat(desktop): báo cáo doanh thu theo tháng và top bán chạy` |
| DES-03 | `des-03-khach-hang` | `feat(desktop): thêm form quản lý khách hàng` |
| DES-04 | `des-04-tai-khoan` | `feat(desktop): thêm form quản lý tài khoản` |
| DES-05 | `des-05-sap-het-hang` | `feat(desktop): cảnh báo sản phẩm sắp hết hàng trên dashboard` |
| DES-06 | `des-06-xuat-csv` | `feat(desktop): xuất danh sách đơn hàng ra CSV` |
| DES-07 | `des-07-xem-anh` | `feat(desktop): xem trước ảnh sản phẩm trong form` |
| DES-08 | `des-08-hien-mat-khau` | `feat(desktop): thêm nút hiện/ẩn mật khẩu khi đăng nhập` |
| DOC-01 | `docs/handbook` | `docs: thêm handbook hướng dẫn sử dụng` |
| DOC-02 | `docs/cap-nhat-bao-cao` | `docs: cập nhật báo cáo và slide theo code thật` |
| QA-02 | `docs/test-case` | `docs: bổ sung bộ test case nghiệm thu` |

Lưu ý khi commit: không gộp nhiều task vào một commit; không commit `bin/obj`,
`.env`, ảnh chụp tạm; mỗi PR chỉ sửa file thuộc task của mình (xem cột
"File chính" ở mục B).

---

## E. CHECKLIST NGHIỆM THU CUỐI CÙNG

> Cả nhóm đánh dấu `[x]` khi từng hạng mục đã chạy thật và được nghiệm thu.

### Chức năng
- [ ] Đăng nhập Admin/Staff, phân quyền đúng; nút hiện/ẩn mật khẩu hoạt động (DES-08)
- [ ] Sản phẩm: thêm/sửa/xóa (soft delete) + **Ảnh/Giá gốc/Nổi bật** chỉnh được và hiển thị trên web (DES-01); ảnh xem trước đúng (DES-07)
- [ ] Danh mục, biến thể size/màu/tồn kho
- [ ] Phiếu nhập kho: lưu cộng tồn, hủy hoàn tồn (chỉ Admin)
- [ ] Duyệt đơn: chuyển trạng thái, hoàn tồn khi hủy; xuất CSV đúng danh sách đang lọc (DES-06)
- [ ] Dashboard: 3 chỉ số hôm nay + báo cáo tháng/top bán chạy (DES-02) + cảnh báo sắp hết hàng (DES-05)
- [ ] Quản lý khách hàng: tìm/sửa/xem lịch sử đơn (DES-03)
- [ ] Quản lý tài khoản: thêm user, gán role, bật/tắt (DES-04)
- [ ] Web: trang chủ / danh sách (phân trang WEB-01, sắp xếp WEB-03) / chi tiết (liên quan WEB-04, đã xem gần đây WEB-05)
- [ ] Web: giỏ hàng & đặt hàng (transaction, chặn quá tồn)
- [ ] Web: tra cứu đơn + lịch sử localStorage

### Backend
- [ ] Không còn so sánh/lưu mật khẩu thuần (SEC-01)
- [ ] Ghi đơn & nhập/xuất kho an toàn giao dịch, không bán quá tồn
- [ ] SQL tham số hóa 100%, kể cả `ORDER BY` của sắp xếp mới (WEB-03)

### Frontend
- [ ] UX giỏ/checkout/tra cứu thân thiện, báo lỗi rõ (WEB-02)
- [ ] Ảnh sản phẩm & mặc định `no-image.svg` hoạt động
- [ ] Định dạng giá Việt Nam nhất quán
- [ ] Gõ sai URL ra trang 404 thân thiện, favicon hiển thị trên tab (WEB-06)
- [ ] Khối "Đã xem gần đây" ẩn khi rỗng và không lỗi khi localStorage bị chặn

### Database
- [ ] Chạy `CSDL_ShopQuanAo.sql` trên máy mới thành công
- [ ] Migration 01–04 idempotent, không lỗi khi chạy lại
- [ ] Ràng buộc (CHECK/UNIQUE/index) và seed nhất quán sau SEC-01 (DB-01)

### Tích hợp hệ thống
- [ ] Web đặt hàng → Desktop thấy đơn & trừ kho đúng
- [ ] Desktop hủy đơn → hoàn tồn, web hiển thị đúng tình trạng hàng
- [ ] Build solution sạch (0 warning / 0 error)

### Kiểm thử
- [ ] Test case thủ công QA-02 chạy hết, đạt
- [ ] `dotnet test` xanh: 5 test phân trang + model sản phẩm (QA-03) + test giỏ/checkout/tra cứu (QA-01)

### CI/CD và triển khai (nếu đề cương yêu cầu)
- [ ] GitHub Actions build 2 project + chạy test (CI-01)
- [ ] `docker compose up -d` + `run-web.cmd` chạy được trên máy mới

### Tài liệu báo cáo
- [ ] Báo cáo `.docx` cập nhật khớp code thật (kể cả 7 tính năng mới)
- [ ] Handbook hướng dẫn sử dụng (DOC-01)
- [ ] Sơ đồ/Use case/ERD/screenshot đối chiếu được

### Chuẩn bị thuyết trình
- [ ] Slide (PPT) cập nhật, có phần bảo mật & tính năng mới
- [ ] Kịch bản demo phân vai theo 4 người, chạy thử suôn sẻ
- [ ] Dữ liệu demo chuẩn bị sẵn (đơn, phiếu nhập, tài khoản)
