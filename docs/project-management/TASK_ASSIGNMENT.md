# PHÂN CHIA CÔNG VIỆC CÒN LẠI — ĐỒ ÁN FASHION SHOP

> Tài liệu quản lý dự án — trưởng nhóm **Trịnh Tấn Phát** cập nhật.
> Nhóm: Trịnh Tấn Phát (trưởng), Trần Lê Hồng Nhân, Dương Huỳnh Nam Anh,
> Nguyễn Như (**nnhu**, mới tham gia).
> Phạm vi: **chỉ liệt kê công việc còn tồn đọng**. Những phần đã hoàn thiện
> được ghi nhận ở mục A và **không phân chia lại**.

Tài liệu này được lập sau khi kiểm tra trực tiếp trên repository
(branch `main`, commit `ff5db91`, working tree sạch, `dotnet build` thành công
0 warning / 0 error), và **được cập nhật lại ngày 13/11/2026** sau khi đối chiếu
toàn bộ code trên `main` tại commit `5a632bf` (đã merge SEC-01, DES-01, WEB-01).

---

## A. TỔNG QUAN TÌNH TRẠNG DỰ ÁN

### Mức độ hoàn thiện
Hệ thống **đã gần hoàn thiện và chạy được thực tế**. Kiến trúc 2 ứng dụng
dùng chung CSDL (WinForms `QuanLyDesktop` + ASP.NET Core MVC `WebBanHang`,
SQL Server 2022 qua Docker) đã hình thành đầy đủ, luồng nghiệp vụ cốt lõi
(đăng nhập, sản phẩm, danh mục, biến thể size/màu, phiếu nhập kho, duyệt đơn,
giỏ hàng, đặt hàng, tra cứu đơn) đã đi từ giao diện xuống tới dữ liệu thật.

Phần còn thiếu tập trung vào **báo cáo thống kê nâng cao**, **hai form quản lý
còn thiếu** (khách hàng, tài khoản), **form đổi mật khẩu**, và **các hạng mục
phục vụ nghiệm thu** (kiểm thử tự động, handbook, cập nhật báo cáo/slide).
Lỗ hổng bảo mật mật khẩu và ba trường Ảnh/Giá gốc/Nổi bật đã xử lý xong.

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
| Database & migration | `CSDL_ShopQuanAo.sql` + `Scripts/migrations/01–04` (CHECK, UNIQUE, index, view tồn kho, seed idempotent, băm mật khẩu) |
| Hạ tầng & chạy thực tế | `docker-compose.yml`, `.env.example`, `run-web.cmd`/`run-desktop.cmd`, cấu hình kết nối qua biến môi trường; build sạch |
| Bảo mật mật khẩu (SEC-01) | `PasswordHasher.cs` dùng PBKDF2-HMAC-SHA256 100.000 vòng; `Form1.cs` xác thực qua `PasswordHasher.Verify`; seed `CSDL_ShopQuanAo.sql` và `Scripts/migrations/04_hash_passwords.sql` đều lưu dạng hash |
| Sản phẩm: Ảnh / Giá gốc / Nổi bật (DES-01) | `FormMain.cs` có `txtImageUrl`, giá gốc, `chkIsFeatured`; cả `INSERT` (dòng 326) và `UPDATE` (dòng 430) đều ghi `ImageUrl`, `OriginalPrice`, `IsFeatured` |
| Web: phân trang danh sách (WEB-01) | `ProductsController` + `SqlProductRepository` nhận `page`/`pageSize`; PR #8 đã merge vào `main` |
| CI build (CI-01) | `.github/workflows/ci.yml` restore + build 2 project trên `windows-latest`; run #23 trên `main` kết luận **success** (bước `dotnet test` còn comment chờ QA-01) |
| Tài liệu báo cáo/nghiệm thu (bản nháp) | trong `docs/baocao/`: `BaoCao_DoAn_...docx`, `PhanTichYeuCauNghiepVu_...pptx`, `CK_GD1_..._DONE.xlsx`, `diagrams/`, `screenshots/` (gitignore) |

### ⚠️ Còn thiếu / cần khắc phục (đã xác minh)

1. **Dashboard thiếu báo cáo nâng cao** (TRUNG BÌNH). Chỉ có 3 chỉ số "hôm nay";
   chưa có thống kê theo tháng/quý và top sản phẩm bán chạy.
2. **Chưa có form Quản lý khách hàng** (TRUNG BÌNH). `Customers` chỉ được ghi
   tự động khi đặt hàng, không có màn hình xem/tìm/sửa trong app desktop.
3. **Chưa có form Quản lý tài khoản người dùng** (THẤP). Không thêm tài khoản
   mới, bật/tắt `IsActive` từ giao diện.
4. **Chưa có form đổi mật khẩu** (TRUNG BÌNH). Đăng nhập đã băm PBKDF2 nhưng
   người dùng chưa tự đổi được mật khẩu trong app.
5. **Kiểm thử tự động chưa có** (TRUNG BÌNH). Repo chỉ có 2 project; chưa có
   project test nên bước `dotnet test` trong CI vẫn đang comment.
6. **Thiếu handbook và tài liệu chưa khớp code** (CAO cho nghiệm thu). README
   thiên về cài đặt kỹ thuật; báo cáo/PPT/bảng đặc tả còn 2 chỗ ghi sai so với
   code (mục 3.4.7 ghi mật khẩu "văn bản thuần", bảng đặc tả ghi "chưa phân trang").

---

## B. DANH SÁCH CÔNG VIỆC (cập nhật 13/11/2026)

| Mã Task | Công việc | Mô tả | Người phụ trách | Ưu tiên | Trạng thái |
|---|---|---|---|---|---|
| SEC-01 | Băm mật khẩu | Triển khai PBKDF2/SHA-256+Salt cho đăng nhập & tạo user; migration mã hóa lại dữ liệu mẫu; cập nhật `Form1.cs`, `FormBienThe...`(không), seed SQL | Phát | Cao | ✅ Hoàn thành (PR #5: `PasswordHasher.cs` + migration 04 + seed hash) |
| SEC-02 | Đổi mật khẩu an toàn | Form đổi mật khẩu cho người đăng nhập, kiểm tra mật khẩu cũ | Phát | Trung bình | Chưa hoàn thành |
| DES-01 | Ảnh / Giá gốc / Nổi bật | Bổ sung 3 trường vào form sản phẩm; sửa `INSERT`/`UPDATE` trong `FormMain.cs`; hỗ trợ chọn đường dẫn/URL ảnh | Nhân | Trung bình | ✅ Hoàn thành (`FormMain.cs` ghi đủ 3 cột ở cả INSERT và UPDATE) |
| DES-02 | Báo cáo tháng / Top bán chạy | Thêm thống kê doanh thu theo tháng & top SP bán chạy vào dashboard (`FormMain.cs`) | Nhân | Trung bình | Chưa hoàn thành |
| DES-03 | Form Quản lý khách hàng | Màn hình mới `FormKhachHang.cs` xem/tìm/sửa khách, lịch sử đơn | Nhân | Trung bình | Chưa hoàn thành |
| DES-04 | Form Quản lý tài khoản | Màn hình mới `FormTaiKhoan.cs` thêm/sửa user, gán role, bật/tắt (chỉ Admin), dùng SEC-01 để băm khi tạo user | Nam Anh | Thấp | Chưa hoàn thành |
| WEB-01 | Phân trang sản phẩm | `SqlProductRepository` + `ProductsController` + view: `page`/`pageSize`, hiển thị số trang | Nam Anh | Thấp | ✅ Hoàn thành (PR #8 đã merge) |
| WEB-02 | Rà soát UX & lỗi biên | Kiểm tra form giỏ/checkout/tra cứu: empty cart, số lượng 0, URL lạ, thông báo lỗi thân thiện | Nam Anh | Trung bình | 🔶 Chưa merge — branch `web-02-validation` đã push (commit `09a745d`) nhưng chưa mở PR và đang cũ hơn `main`, cần rebase |
| DB-01 | Rà soát ràng buộc & seed | Đối chiếu seed `CSDL_ShopQuanAo.sql` với migration; đảm bảo dữ liệu mẫu nhất quán sau SEC-01 | Phát | Trung bình | ✅ Hoàn thành (seed + migration 04 cùng dạng hash PBKDF2) |
| QA-01 | Test tự động (unit/integration) | Project test cho tầng Repository/Service (web) và logic tồn kho/đơn (desktop hoặc qua test DB) | Nam Anh | Trung bình | Chưa hoàn thành |
| QA-02 | Kịch bản test thủ công | Bộ test case nghiệm thu từng chức năng + checklist chạy trên SQL Server thật | Phát | Trung bình | Chưa hoàn thành |
| CI-01 | Pipeline CI | `.github/workflows/ci.yml`: restore + build 2 project trên `windows`, chạy QA-01 | Nam Anh | Thấp | ✅ Hoàn thành phần build (run #23 xanh); bước `dotnet test` còn comment chờ QA-01 |
| DOC-01 | Handbook hướng dẫn sử dụng | Tài liệu vận hành cho Admin/Staff + Khách (kèm ảnh màn hình từ `docs/baocao/screenshots/`) | nnhu | Cao | Chưa hoàn thành |
| DOC-02 | Tài liệu nghiệm thu & thuyết trình | Cập nhật báo cáo/PPT theo code thực tế (đặc biệt sau SEC-01, DES-01); chuẩn bị slide demo | Phát | Cao | Chưa hoàn thành |

> Ghi chú trạng thái: ✅ = đã merge vào `main` và đối chiếu code;
> 🔶 = code đã có nhưng chưa vào `main`; để trống = chưa làm.
> Các mục ✅ **không phân chia lại**, chỉ còn việc chạy tay khi nghiệm thu (mục E).

---

## C. CÔNG VIỆC THEO TỪNG THÀNH VIÊN

### 🧑‍💼 Trịnh Tấn Phát — Trưởng nhóm
Phụ trách chéo (bảo mật + tích hợp + nghiệm thu).
- ~~SEC-01, DB-01~~: **đã xong** (băm PBKDF2 + seed khớp hash, đã merge).
- **SEC-02**: form đổi mật khẩu cho người đang đăng nhập, kiểm tra mật khẩu cũ.
- **QA-02**: soạn bộ test case thủ công, chủ trì chạy nghiệm thu end-to-end.
- **DOC-02**: cập nhật báo cáo/PPT theo code thật (hiện còn sai ở mục mật khẩu
  "văn bản thuần" và "chưa phân trang"), chuẩn bị slide demo.
- **Duyệt merge**: xử lý branch `web-02-validation` còn treo, giữ `main` build sạch.

**Bàn giao**: form đổi mật khẩu, bộ test case, báo cáo & slide cập nhật.

### 🧑‍💻 Trần Lê Hồng Nhân — App Desktop (WinForms)
Phụ trách mảng desktop, tập trung vào `FormMain.cs` và các form mới.
- ~~DES-01~~: **đã xong** (Ảnh / Giá gốc / Nổi bật ghi được ở cả `INSERT`/`UPDATE`).
- **DES-02**: Báo cáo theo tháng + top sản phẩm bán chạy trên dashboard.
- **DES-03**: form mới `FormKhachHang.cs` (quản lý khách hàng).

**Bàn giao**: dashboard có báo cáo tháng + top bán chạy, form quản lý khách hàng
hoàn chỉnh.

> Lưu ý chống xung đột: Nhân là **chủ sở hữu duy nhất `FormMain.cs`** trong
> đợt này (DES-02 dùng lại file này). Không ai khác sửa file này.

### 🧑‍💻 Dương Huỳnh Nam Anh — Web + Test + CI
Phụ trách `WebBanHang/` và một form desktop mới (không đụng `FormMain.cs`).
- ~~WEB-01~~: **đã xong** (PR #8).
- **WEB-02**: rà soát UX/lỗi biên giỏ hàng, checkout, tra cứu đơn.
- **DES-04**: form mới `FormTaiKhoan.cs` (quản lý tài khoản) — dùng API băm của SEC-01.
- **QA-01**: project test tự động cho tầng repository/service.
- ~~CI-01~~: **đã xong phần build** — còn bật bước `dotnet test` sau khi có QA-01.

**Bàn giao**: WEB-02 rebase + merge, form tài khoản mới, project test chạy
được, pipeline chạy cả test.

> Lưu ý chống xung đột: Nam Anh làm **file mới** cho desktop (`FormTaiKhoan.cs`)
> để không sửa chồng lên `FormMain.cs` của Nhân. Trên web chỉ Nam Anh chỉnh.

### 🧑💻 Nguyễn Như (nnhu) — Tài liệu & hỗ trợ nghiệm thu
Mới tham gia; nhận phần tài liệu để Phát dồn vào báo cáo/slide.
- **DOC-01**: handbook hướng dẫn sử dụng cho Admin/Staff và Khách, kèm ảnh chụp
  màn hình (nguồn ảnh ở `docs/baocao/screenshots/`).
- **Hỗ trợ QA-02**: góp test case thủ công cho những luồng đã tự chạy thử.

**Bàn giao**: handbook hoàn chỉnh (Markdown hoặc `.docx`), có ảnh và từng bước thao tác.

> Lưu ý: nnhu chỉ **tạo file tài liệu mới**, không sửa code — tránh xung đột
> với Nhân và Nam Anh.

---

## D. THỨ TỰ THỰC HIỆN

### Phải làm trước
1. ~~**SEC-01**~~ — **đã xong**, nên DES-04 và DB-01 không còn bị chặn.
2. **WEB-02** (Nam Anh): rebase branch lên `main` rồi mở PR — đang treo, dễ bỏ sót.
3. **QA-02** (Phát): chốt bộ test case để cả nhóm có căn cứ nghiệm thu.

### Phụ thuộc
- **DES-04** (form tài khoản) ⟵ sau **SEC-01** (dùng chung hàm băm).
- ~~**DB-01** ⟵ SEC-01~~: **đã xong** (seed + migration cùng dạng hash).
- **CI-01** ⟵ sau **QA-01** (pipeline cần có test để chạy).
- **DOC-02 / báo cáo** ⟵ sau **DES-01, SEC-01** (mô tả tính năng phải khớp code thật).

### Có thể làm song song (không chồng file)
- Nhân: DES-02 → DES-03 (desktop; DES-01 đã xong).
- Nam Anh: WEB-02 (rebase + mở PR), DES-04, QA-01.
- Phát: SEC-02, QA-02, DOC-02.
- nnhu: DOC-01 (chỉ viết tài liệu, không chạm code).

### Cần cả nhóm phối hợp
- **SEC-01 ⟷ DES-04**: ~~Phát cung cấp hàm băm~~ đã xong — Nam Anh dùng lại `PasswordHasher` có sẵn khi tạo user.
- ~~**DES-01 ⟷ WEB**: Nhân bổ sung trường ảnh/giá gốc, Nam Anh xác nhận web render đúng~~ — **đã xong**.
- **QA-02 / nghiệm thu**: cả 4 người cùng chạy luồng end-to-end (đặt hàng web → duyệt đơn desktop → hoàn tồn khi hủy).
- **DOC-02**: Phát tổng hợp, mỗi người xác nhận phần module mình; nnhu viết handbook song song.

### Mốc trước ngày nộp
- ~~SEC-01, DES-01~~ **đã xong** — cả hai đã merge vào `main`.
- **WEB-02** phải được rebase rồi mở PR/merge (branch đang treo, chưa có PR).
- **QA-02** chạy xong, mọi mục trong checklist E được tick bằng kết quả chạy thật.
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
>
> Trạng thái 13/11/2026: SEC-01, DES-01, WEB-01, CI-01 đã merge và CI xanh,
> **nhưng vẫn phải chạy tay** từng mục dưới đây trước khi tick.

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
- [ ] Kịch bản demo phân vai theo 4 người, chạy thử suôn sẻ
- [ ] dữ liệu demo chuẩn bị sẵn (đơn, phiếu nhập, tài khoản)
