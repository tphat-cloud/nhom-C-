# PHÂN CHIA CÔNG VIỆC CÒN LẠI — ĐỒ ÁN FASHION SHOP

> Tài liệu quản lý dự án — trưởng nhóm **Trịnh Tấn Phát** cập nhật.
> Nhóm: Trịnh Tấn Phát (trưởng), Trần Lê Hồng Nhân, Dương Huỳnh Nam Anh.
> Phạm vi: **chỉ liệt kê công việc còn tồn đọng**. Những phần đã hoàn thiện
> được ghi nhận ở mục A và **không phân chia lại**.

Tài liệu này được lập sau khi kiểm tra trực tiếp trên repository
(branch `main`, commit `ff5db91`, working tree sạch, `dotnet build` thành công
0 warning / 0 error).

---

## A. TỔNG QUAN TÌNH TRẠNG DỰ ÁN

### Mức độ hoàn thiện
Hệ thống **đã gần hoàn thiện và chạy được thực tế**. Kiến trúc 2 ứng dụng
dùng chung CSDL (WinForms `QuanLyDesktop` + ASP.NET Core MVC `WebBanHang`,
SQL Server 2022 qua Docker) đã hình thành đầy đủ, luồng nghiệp vụ cốt lõi
(đăng nhập, sản phẩm, danh mục, biến thể size/màu, phiếu nhập kho, duyệt đơn,
giỏ hàng, đặt hàng, tra cứu đơn) đã đi từ giao diện xuống tới dữ liệu thật.

Phần còn thiếu tập trung vào **một lỗ hổng bảo mật**, **vài trường dữ liệu
chưa chỉnh sửa được từ giao diện**, **báo cáo thống kê nâng cao**, và
**các hạng mục phục vụ nghiệm thu** (CI/CD, kiểm thử tự động, tài liệu).

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
| Web: tra cứu đơn hàng | `OrderLookupController` + `Views/OrderLookup/Index.cshtml`, lịch sử đơn qua localStorage |
| Database & migration | `CSDL_ShopQuanAo.sql` + `Scripts/migrations/01–03` (CHECK, UNIQUE, index, view tồn kho, seed idempotent) |
| Hạ tầng & chạy thực tế | `docker-compose.yml`, `.env.example`, `run-web.cmd`/`run-desktop.cmd`, cấu hình kết nối qua biến môi trường; build sạch |
| Tài liệu báo cáo/nghiệm thu (bản nháp) | `BaoCao_DoAn_...docx`, `PhanTichYeuCauNghiepVu_...pptx`, `diagrams/`, `screenshots/`, `CK_GD1_..._DONE.xlsx` |

### ⚠️ Còn thiếu / cần khắc phục (đã xác minh)

1. **Bảo mật — mật khẩu lưu/so sánh dạng thuần** (CAO). `Form1.cs` gửi thẳng
   mật khẩu người nhập so với cột `PasswordHash`; dữ liệu mẫu trong
   `CSDL_ShopQuanAo.sql` nhét chuỗi `'123456'` vào cột `PasswordHash`. Tên cột
   là "Hash" nhưng thực tế chưa hề băm.
2. **Sản phẩm không chỉnh sửa được Ảnh / Giá gốc / Nổi bật** (TRUNG BÌNH).
   `INSERT INTO Products` ghi cứng `IsFeatured = 0` và bỏ trống `ImageUrl`,
   `OriginalPrice`; `UPDATE` cũng không đụng tới 3 cột này. Web **có hiển thị**
   3 cột này → dữ liệu không bao giờ được thiết lập từ hệ thống quản lý.
3. **Dashboard thiếu báo cáo nâng cao** (TRUNG BÌNH). Chỉ có 3 chỉ số "hôm nay";
   chưa có thống kê theo tháng/quý và top sản phẩm bán chạy.
4. **Chưa có form Quản lý khách hàng** (TRUNG BÌNH). `Customers` chỉ được ghi
   tự động khi đặt hàng, không có màn hình xem/tìm/sửa trong app desktop.
5. **Chưa có form Quản lý tài khoản người dùng** (THẤP). Không thêm/tài khoản
   mới, đổi mật khẩu, bật/tắt `IsActive` từ giao diện.
6. **Web: danh sách sản phẩm chưa phân trang** (THẤP). `GetProductsAsync` trả
   toàn bộ; lượng sản phẩm lớn sẽ nặng trang.
7. **CI/CD chưa có** (THẤP–TRUNG BÌNH, tùy yêu cầu đề cương). Thư mục
   `.github/workflows/` đang trống.
8. **Kiểm thử tự động chưa có** (TRUNG BÌNH). Không có project test; kiểm thử
   hiện ghi tay trong README.
9. **Tài liệu hướng dẫn sử dụng & kịch bản thuyết trình** (CAO cho nghiệm thu).
   README thiên về cài đặt kỹ thuật; chưa có handbook vận hành cho
   admin/nhân viên và kịch bản demo phân công theo người.

---

## B. DANH SÁCH CÔNG VIỆC CÒN LẠI

| Mã Task | Công việc | Mô tả | Người phụ trách | Ưu tiên | Trạng thái |
|---|---|---|---|---|---|
| SEC-01 | Băm mật khẩu | Triển khai PBKDF2/SHA-256+Salt cho đăng nhập & tạo user; migration mã hóa lại dữ liệu mẫu; cập nhật `Form1.cs`, `FormBienThe...`(không), seed SQL | Phát | Cao | Chưa hoàn thành |
| SEC-02 | Đổi mật khẩu an toàn | Form đổi mật khẩu cho người đăng nhập, kiểm tra mật khẩu cũ | Phát | Trung bình | Chưa hoàn thành |
| DES-01 | Ảnh / Giá gốc / Nổi bật | Bổ sung 3 trường vào form sản phẩm; sửa `INSERT`/`UPDATE` trong `FormMain.cs`; hỗ trợ chọn đường dẫn/URL ảnh | Nhân | Trung bình | Chưa hoàn thành |
| DES-02 | Báo cáo tháng / Top bán chạy | Thêm thống kê doanh thu theo tháng & top SP bán chạy vào dashboard (`FormMain.cs`) | Nhân | Trung bình | Chưa hoàn thành |
| DES-03 | Form Quản lý khách hàng | Màn hình mới `FormKhachHang.cs` xem/tìm/sửa khách, lịch sử đơn | Nhân | Trung bình | Chưa hoàn thành |
| DES-04 | Form Quản lý tài khoản | Màn hình mới `FormTaiKhoan.cs` thêm/sửa user, gán role, bật/tắt (chỉ Admin), dùng SEC-01 để băm khi tạo user | Nam Anh | Thấp | Chưa hoàn thành |
| WEB-01 | Phân trang sản phẩm | `SqlProductRepository` + `ProductsController` + view: `page`/`pageSize`, hiển thị số trang | Nam Anh | Thấp | Chưa hoàn thành |
| WEB-02 | Rà soát UX & lỗi biên | Kiểm tra form giỏ/checkout/tra cứu: empty cart, số lượng 0, URL lạ, thông báo lỗi thân thiện | Nam Anh | Trung bình | Cần kiểm tra thêm |
| DB-01 | Rà soát ràng buộc & seed | Đối chiếu seed `CSDL_ShopQuanAo.sql` với migration; đảm bảo dữ liệu mẫu nhất quán sau SEC-01 | Phát | Trung bình | Cần kiểm tra thêm |
| QA-01 | Test tự động (unit/integration) | Project test cho tầng Repository/Service (web) và logic tồn kho/đơn (desktop hoặc qua test DB) | Nam Anh | Trung bình | Chưa hoàn thành |
| QA-02 | Kịch bản test thủ công | Bộ test case nghiệm thu từng chức năng + checklist chạy trên SQL Server thật | Phát | Trung bình | Chưa hoàn thành |
| CI-01 | Pipeline CI | `.github/workflows/build.yml`: restore + build 2 project trên `ubuntu`/`windows`, chạy QA-01 | Nam Anh | Thấp | Chưa hoàn thành |
| DOC-01 | Handbook hướng dẫn sử dụng | Tài liệu vận hành cho Admin/Staff + Khách (kèm ảnh màn hình từ `screenshots/`) | Phát | Cao | Chưa hoàn thành |
| DOC-02 | Tài liệu nghiệm thu & thuyết trình | Cập nhật báo cáo/PPT theo code thực tế (đặc biệt sau SEC-01, DES-01); chuẩn bị slide demo | Phát | Cao | Chưa hoàn thành |

> Ghi chú: "Cần kiểm tra thêm" = cần cả nhóm chạy thử trên môi trường thật để
> chốt Có/Không trước khi quy về "Chưa hoàn thành".

---

## C. CÔNG VIỆC THEO TỪNG THÀNH VIÊN

### 🧑‍💼 Trịnh Tấn Phát — Trưởng nhóm
Phụ trách chéo (bảo mật + tích hợp + nghiệm thu).
- **SEC-01, SEC-02**: bảo mật mật khẩu (chạm `Form1.cs`, tầng tạo user, migration băm lại seed).
- **DB-01**: rà soát ràng buộc/seed sau khi đổi cách lưu mật khẩu.
- **QA-02**: soạn bộ test case thủ công, chủ trì chạy nghiệm thu end-to-end.
- **DOC-01, DOC-02**: handbook + cập nhật báo cáo/thuyết trình.
- **Phối hợp**: duyệt sự hợp nhất (merge) giữa 3 người, giữ `main` luôn build sạch.

**Bàn giao**: code băm mật khẩu + migration, bộ test case, handbook, báo cáo & slide cập nhật.

### 🧑‍💻 Trần Lê Hồng Nhân — App Desktop (WinForms)
Phụ trách mảng desktop, tập trung vào `FormMain.cs` và các form mới.
- **DES-01**: Ảnh / Giá gốc / Nổi bật trong form sản phẩm (sửa `INSERT`/`UPDATE`).
- **DES-02**: Báo cáo theo tháng + top sản phẩm bán chạy trên dashboard.
- **DES-03**: form mới `FormKhachHang.cs` (quản lý khách hàng).

**Bàn giao**: các form desktop hoàn chỉnh, ảnh/giá gốc/nổi bật thiết lập được
và hiển thị đúng trên web, dashboard có thống kê.

> Lưu ý chống xung đột: Nhân là **chủ sở hữu duy nhất `FormMain.cs`** trong
> đợt này (DES-01 + DES-02 cùng file). Không ai khác sửa file này.

### 🧑‍💻 Dương Huỳnh Nam Anh — Web + Test + CI
Phụ trách `WebBanHang/` và một form desktop mới (không đụng `FormMain.cs`).
- **WEB-01**: phân trang sản phẩm.
- **WEB-02**: rà soát UX/lỗi biên giỏ hàng, checkout, tra cứu đơn.
- **DES-04**: form mới `FormTaiKhoan.cs` (quản lý tài khoản) — dùng API băm của SEC-01.
- **QA-01**: project test tự động cho tầng repository/service.
- **CI-01**: workflow GitHub Actions build + chạy test.

**Bàn giao**: web có phân trang, UX mượt, form tài khoản mới, project test chạy
được, pipeline xanh.

> Lưu ý chống xung đột: Nam Anh làm **file mới** cho desktop (`FormTaiKhoan.cs`)
> để không sửa chồng lên `FormMain.cs` của Nhân. Trên web chỉ Nam Anh chỉnh.

---

## D. THỨ TỰ THỰC HIỆN

### Phải làm trước
1. **SEC-01** (Phát) — vì DES-04 (tạo tài khoản) và DB-01 phụ thuộc cách lưu mật khẩu mới.
2. **WEB-02 / QA-02** chạy nhanh để chốt các mục "Cần kiểm tra thêm".

### Phụ thuộc
- **DES-04** (form tài khoản) ⟵ sau **SEC-01** (dùng chung hàm băm).
- **DB-01** ⟵ sau **SEC-01** (seed phải nhất quán với lưu dạng hash).
- **CI-01** ⟵ sau **QA-01** (pipeline cần có test để chạy).
- **DOC-02 / báo cáo** ⟵ sau **DES-01, SEC-01** (mô tả tính năng phải khớp code thật).

### Có thể làm song song (không chồng file)
- Nhân: DES-01 → DES-02 → DES-03 (toàn bộ ở desktop, riêng `FormMain.cs`).
- Nam Anh: WEB-01, WEB-02 (chỉ `WebBanHang/`).
- Phát: SEC-01, QA-02, DOC-01.

### Cần cả nhóm phối hợp
- **SEC-01 ⟷ DES-04**: Phát cung cấp hàm băm/util, Nam Anh tích hợp vào form tài khoản.
- **DES-01 ⟷ WEB**: Nhân bổ sung trường ảnh/giá gốc, Nam Anh xác nhận web render đúng.
- **QA-02 / nghiệm thu**: cả 3 cùng chạy luồng end-to-end (đặt hàng web → duyệt đơn desktop → hoàn tồn khi hủy).
- **DOC-02**: Phát tổng hợp, mỗi người xác nhận phần module mình.

### Mốc trước ngày nộp
- Hoàn tất **SEC-01** và **DES-01** (2 thay đổi ảnh hưởng trực tiếp tới dữ liệu & nghiệp vụ).
- **QA-02** chạy xong, mọi "Cần kiểm tra thêm" về "Đã hoàn thành".
- **DOC-01, DOC-02** chốt bản cuối cho thuyết trình.

### Chiến lược merge (áp dụng cho cả nhóm)
**Nguyên tắc: merge sớm, merge nhỏ theo từng tính năng — KHÔNG đợi xong hết mới gộp.**
Lý do: tránh xung đột tích tụ, bắt lỗi tích hợp sớm, và giữ `main` luôn chạy được để demo/nghiệm thu.

Một PR chỉ được merge khi đủ 5 điều kiện:
1. `dotnet build QuanLyShopQuanAo.slnx` sạch (0 warning / 0 error).
2. Đã **chạy thử tính năng đó thật** (xem AGENTS.md mục 5), không đánh dấu xong chỉ vì đã viết code.
3. Không phá tính năng cũ (web đặt hàng, desktop duyệt đơn, tồn kho vẫn chạy).
4. Đúng phạm vi file của task, không "tiện tay" sửa file người khác.
5. Được trưởng nhóm (Phát) hoặc chủ module review.

Ngoại lệ — feature **liên thông 2 app hoặc bảo mật** thì gom thành 1 PR trọn vẹn rồi merge:
- **SEC-01**: code băm + migration băm lại seed + cập nhật `CSDL_ShopQuanAo.sql` merge **cùng một lần** (không merge lửng gây hỏng đăng nhập). Sau khi vào `main`, **mọi người pull ngay** rồi mới làm DES-04/SEC-02.
- **DES-01**: Nhân làm desktop xong, **Nam Anh xác nhận web hiển thị đúng Ảnh/Giá gốc/Nổi bật** rồi mới merge trọn feature.

Nhịp độ thường ngày: unit nhỏ tách được thì tách và merge liền (vd DES-01 chia: "Nổi bật" → merge, rồi "Ảnh + Giá gốc" → merge). Sau mỗi merge, Phát báo cả nhóm `git pull` `main` để rebase branch, không bị lệch.

---

## E. CHECKLIST NGHIỆM THU CUỐI CÙNG

> Cả nhóm đánh dấu `[x]` khi từng hạng mục đã chạy thật và được nghiệm thu.

### Chức năng
- [ ] Đăng nhập Admin/Staff, phân quyền đúng
- [ ] Sản phẩm: thêm/sửa/xóa (soft delete) + **Ảnh/Giá gốc/Nổi bật** chỉnh được và hiển thị trên web (DES-01)
- [ ] Danh mục, biến thể size/màu/tồn kho
- [ ] Phiếu nhập kho: lưu cộng tồn, hủy hoàn tồn (chỉ Admin)
- [ ] Duyệt đơn: chuyển trạng thái, hoàn tồn khi hủy
- [ ] Web: trang chủ / danh sách (có phân trang - WEB-01) / chi tiết
- [ ] Web: giỏ hàng & đặt hàng (transaction, chặn quá tồn)
- [ ] Web: tra cứu đơn + lịch sử localStorage

### Backend
- [ ] Không còn so sánh/lưu mật khẩu thuần (SEC-01)
- [ ] Ghi đơn & nhập/xuất kho an toàn giao dịch, không bán quá tồn
- [ ] SQL tham số hóa 100%, không có chuỗi nối tham số người dùng

### Frontend
- [ ] UX giỏ/checkout/tra cứu thân thiện, báo lỗi rõ (WEB-02)
- [ ] Ảnh sản phẩm & mặc định `no-image.svg` hoạt động
- [ ] Định dạng giá Việt Nam nhất quán

### Database
- [ ] Chạy `CSDL_ShopQuanAo.sql` trên máy mới thành công
- [ ] Migration 01–03 idempotent, không lỗi khi chạy lại
- [ ] Ràng buộc (CHECK/UNIQUE/index) và seed nhất quán sau SEC-01 (DB-01)

### Tích hợp hệ thống
- [ ] Web đặt hàng → Desktop thấy đơn & trừ kho đúng
- [ ] Desktop hủy đơn → hoàn tồn, web hiển thị đúng tình trạng hàng
- [ ] Build solution sạch (0 warning / 0 error)

### Kiểm thử
- [ ] Test case thủ công QA-02 chạy hết, đạt
- [ ] Test tự động QA-01 xanh (nếu làm)

### CI/CD và triển khai (nếu đề cương yêu cầu)
- [ ] GitHub Actions build 2 project (CI-01)
- [ ] `docker compose up -d` + `run-web.cmd` chạy được trên máy mới

### Tài liệu báo cáo
- [ ] Báo cáo `.docx` cập nhật khớp code thật
- [ ] Handbook hướng dẫn sử dụng (DOC-01)
- [ ] Sơ đồ/Use case/ERD/screenshot đối chiếu được

### Chuẩn bị thuyết trình
- [ ] Slide (PPT) cập nhật, có phần bảo mật & tính năng mới
- [ ] Kịch bản demo phân vai theo 3 người, chạy thử suôn sẻ
- [ ] dữ liệu demo chuẩn bị sẵn (đơn, phiếu nhập, tài khoản)
