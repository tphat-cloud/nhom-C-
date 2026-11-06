# PROMPT MẪU CHO AGENT TỪNG THÀNH VIÊN

> Dùng khi bạn giao việc cho AI agent trong IDE. Copy khối "Mẫu tổng",
> điền task của bạn vào các chỗ `{{...}}`, rồi gửi cho agent.
> Nguyên tắc: **mỗi lần chỉ giao 1 task nhỏ**, yêu cầu agent đọc code thật
> trước khi sửa, và **đi qua Pull Request — không push thẳng `main`**.
>
> Bối cảnh dự án: WinForms `QuanLyDesktop` + ASP.NET Core MVC `WebBanHang`
> + SQL Server 2022 (Docker), tầng dữ liệu **ADO.NET thuần** (không EF).

---

## 1. Mẫu tổng (điền task của bạn)

```text
Bạn là lập trình viên trong đồ án "Fashion Shop" (WinForms QuanLyDesktop +
ASP.NET Core MVC WebBanHang + SQL Server Docker, ADO.NET thuần).

BỐI CẢNH — đọc trước khi code:
- File phân công: docs/project-management/TASK_ASSIGNMENT.md (mục B + C + D).
- README.md (mục 2, 3): cách build/run, biến môi trường FASHIONSHOP_SQL_PASSWORD,
  luồng dữ liệu, tài khoản mẫu admin/123456.
- Code thật của module liên quan tới task bên dưới. KHÔNG sửa file ngoài phạm vi.

TASK của tôi: {{MÃ TASK}} — {{MÔ TẢ NGẮN}}
Phạm vi file được đụng tới: {{DANH SÁCH FILE}}
Tiêu chí "xong":
- {{ACCEPTANCE_1}}
- {{ACCEPTANCE_2}}
- Build solution sạch: `dotnet build QuanLyShopQuanAo.slnx` → 0 warning / 0 error.

QUY TẮC LÀM VIỆC:
1. Chỉ làm ĐÚNG task này, không "tiện tay" refactor chỗ khác.
2. Trước khi sửa, tóm tắt: sẽ sửa file nào, hướng làm, điểm còn mơ hồ →
   chờ tôi xác nhận nếu có câu hỏi.
3. Tuân thủ conventions: ADO.NET tham số hóa 100%, không hard-code mật
   khẩu/chuỗi kết nối, không EF, giữ COLLATE Vietnamese_CI_AI + escape LIKE,
   format giá vi-VN, mật khẩu phải băm (không lưu thuần).
4. Kiểm chứng: nêu cách chạy thử (lệnh docker / dotnet run + bước kiểm tra)
   hoặc thêm/cập nhật test nếu task có QA.

QUY TẮC GIT (bắt buộc):
- KHÔNG push thẳng vào main, KHÔNG force-push, KHÔNG tự merge.
- Tạo branch: {{TEN_BRANCH}} (xuất phát từ main mới nhất: `git checkout main;
  git pull; git checkout -b {{TEN_BRANCH}}`).
- Commit message rõ, ví dụ: "feat(web): phân trang danh sách sản phẩm (WEB-01)".
- Kết thúc: báo tôi diff + hướng mở Pull Request vào main để trưởng nhóm review.
```

---

## 2. Ví dụ đã điền sẵn (theo từng người)

### 2.1. Nam Anh — WEB-01: phân trang sản phẩm

```text
TASK của tôi: WEB-01 — phân trang danh sách sản phẩm ở trang /Products.
Phạm vi file: WebBanHang/Data/SqlProductRepository.cs, IProductRepository.cs,
Controllers/ProductsController.cs, Models/ProductListViewModel.cs,
Views/Products/Index.cshtml.
Tiêu chí "xong":
- Thêm page/pageSize (mặc định 12), dùng OFFSET/FETCH NEXT trong SQL, vẫn giữ
  search keyword + lọc categoryId + thứ tự IsFeatured DESC, Name.
- View hiển thị "Trang x / y" + link Prev/Next giữ được bộ lọc hiện tại.
- Tìm kiếm không dấu & escape LIKE không bị phá.
- Build sạch, chạy web thử phân trang đúng.
Branch: web-01-phan-trang
```

### 2.2. Nam Anh — WEB-02: rà soát UX/lỗi biên

```text
TASK của tôi: WEB-02 — kiểm tra và sửa lỗi biên ở giỏ hàng, checkout, tra cứu đơn.
Phạm vi file: WebBanHang/Controllers/CartController.cs, CheckoutController.cs,
OrderLookupController.cs và các Views tương ứng.
Tiêu chí "xong":
- Giỏ trống / số lượng 0 / vượt tồn: hiển thị thông báo thân thiện, không lỗi 500.
- returnUrl chỉ nhận URL nội bộ; mọi POST vẫn có [ValidateAntiForgeryToken].
- Liệt kê các case đã test + kết quả (nêu rõ nếu chưa đủ dữ liệu để kết luận).
Branch: web-02-ux-bien
```

### 2.3. Nhân — DES-01 (chia nhỏ): checkbox "Nổi bật"

```text
TASK của tôi: DES-01 (một phần) — cho phép tick "Sản phẩm nổi bật" khi
thêm/sửa sản phẩm, bỏ ghi cứng IsFeatured = 0.
Phạm vi file: QuanLyDesktop/FormMain.cs, FormMain.Designer.cs.
Tiêu chí "xong":
- Thêm CheckBox "Nổi bật"; INSERT/UPDATE Products ghi đúng IsFeatured từ checkbox.
- Load ngược giá trị hiện có khi mở sửa một sản phẩm.
- Web /Products và trang chủ hiển thị đúng sản phẩm được tick nổi bật.
Lưu ý: FormMain.cs chỉ mình tôi sửa trong đợt này — không đụng file người khác.
Branch: des-01-isfeatured
```

### 2.4. Nhân — DES-01 (chia nhỏ): Ảnh & Giá gốc

```text
TASK của tôi: DES-01 (một phần) — thêm trường Ảnh và Giá gốc cho sản phẩm.
Phạm vi file: QuanLyDesktop/FormMain.cs, FormMain.Designer.cs.
Tiêu chí "xong":
- Ô nhập ImageUrl (đường dẫn/URL) + OriginalPrice (decimal, có thể trống);
  INSERT/UPDATE Products ghi đúng; validate OriginalPrice >= Price nếu có nhập.
- Web chi tiết sản phẩm hiển thị giá gốc gạch ngang + ảnh (fallback no-image.svg).
Branch: des-01-anh-giagoc
```

### 2.5. Nhân — DES-03: form quản lý khách hàng

```text
TASK của tôi: DES-03 — màn hình mới quản lý khách hàng (xem/tìm/sửa).
Phạm vi file: tạo mới QuanLyDesktop/FormKhachHang.cs + .Designer.cs; thêm nút mở
form trong FormMain.cs (chỉ sửa phần menu nút, không đụng logic sản phẩm).
Tiêu chí "xong":
- Danh sách Customers, tìm theo SĐT/tên, xem lịch sử đơn của khách.
- Chỉ đọc + sửa thông tin liên hệ cơ bản; không xóa khách có đơn.
Branch: des-03-khach-hang
```

### 2.6. Phát — SEC-01: băm mật khẩu (task chốt, làm trước)

```text
TASK của tôi: SEC-01 — chuyển từ mật khẩu thuần sang băm (PBKDF2 hoặc SHA-256+salt).
Phạm vi file: util bảo mật mới trong QuanLyDesktop/, Form1.cs (đăng nhập),
migration mới trong Scripts/migrations/ để băm lại dữ liệu mẫu, cập nhật
CSDL_ShopQuanAo.sql (ghi chú cách sinh hash).
Tiêu chí "xong":
- Đăng nhập băm đầu vào rồi đối chiếu, KHÔNG so sánh chuỗi thô.
- Có hàm/util tái sử dụng cho SEC-02 và DES-04 (tạo/đổi mật khẩu).
- Migration idempotent; admin/nhanvien vẫn đăng nhập được sau khi băm.
Branch: sec-01-bam-mat-khau
```

### 2.7. Phát — SEC-02: đổi mật khẩu

```text
TASK của tôi: SEC-02 — form đổi mật khẩu cho người đang đăng nhập.
Phạm vi file: form mới trong QuanLyDesktop/.
Tiêu chí "xong":
- Nhập mật khẩu cũ + mới + xác nhận; validate độ dài; dùng chung util băm SEC-01.
- So sánh mật khẩu cũ bằng cách băm đầu vào rồi đối chiếu.
Branch: sec-02-doi-mat-khau
```

### 2.8. Nam Anh — DES-04: form quản lý tài khoản

```text
TASK của tôi: DES-04 — màn hình mới quản lý tài khoản (thêm/sửa user, gán role,
bật/tắt IsActive), CHỈ Admin dùng được.
Phạm vi file: tạo mới QuanLyDesktop/FormTaiKhoan.cs + .Designer.cs. Khi tạo/đổi
mật khẩu user, GỌI UTIL BĂM của SEC-01 (không tự viết, không lưu thuần).
Tiêu chí "xong":
- Thêm user mới với mật khẩu đã băm; đổi role; tắt/mở IsActive.
- chặn mở form khi đang đăng nhập vai trò Staff.
Branch: des-04-tai-khoan
```

### 2.9. Nam Anh — QA-01 + CI-01: test & pipeline

```text
TASK của tôi: QA-01 + CI-01 — thêm project test cho tầng Repository/Service web
và pipeline build.
Phạm vi file: project test mới (vd WebBanHang.Tests/), .github/workflows/build.yml.
Tiêu chí "xong":
- Test logic ToLikePattern/escape LIKE và mapping repository (mock được thì mock).
- workflow restore + build 2 project + chạy test; nêu rõ nếu cần DB để chạy.
Branch: qa01-ci01-test-pipeline
```

---

## 3. Nhắc chung cho cả nhóm

- Task nào thuộc mục "Cần kiểm tra thêm" → chạy thử trên SQL Server thật rồi
  mới báo Hoàn thành; **không đánh dấu xong chỉ vì code đã viết**.
- Mỗi task = 1 branch = 1 Pull Request. Trưởng nhóm (Phát) là người merge chính.
- Bật branch protection cho `main` (PR + review + CI pass) để không ai push thẳng.
