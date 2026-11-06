## Mô tả
<!-- Task nào? (vd DES-01, WEB-01, SEC-01) Tóm tắt ngắn gọn đã làm gì. -->

## Loại thay đổi
- [ ] Tính năng mới
- [ ] Sửa lỗi
- [ ] Bảo mật / refactor
- [ ] Tài liệu

## Phạm vi file
<!-- Liệt kê file đã sửa; xác nhận KHÔNG đụng file ngoài phạm vi task. -->

## Đã kiểm chứng chưa? (bắt buộc — xem AGENTS.md mục 5)
- [ ] `dotnet build QuanLyShopQuanAo.slnx` sạch (0 warning / 0 error)
- [ ] Đã **chạy thử tính năng này thật** trên DB (không chỉ vì đã viết code)
- [ ] Không phá tính năng cũ (web đặt hàng, desktop duyệt đơn, tồn kho)
- [ ] Đã bật `[ValidateAntiForgeryToken]` / SQL tham số hóa / mật khẩu băm (nếu liên quan)

## Cách reviewer test lại
<!-- Bước cụ thể: lệnh docker/dotnet run, luồng bấm, dữ liệu mẫu cần có. -->

## Ảnh / log (nếu có)
<!-- screenshots hoặc output chạy thử chứng minh tính năng hoạt động. -->

## Checklist trưởng nhóm (Phát) trước khi merge
- [ ] Đúng phạm vi task & không giẫm file người khác
- [ ] CI xanh (workflow `CI`)
- [ ] Feature liên thông 2 app / bảo mật: đã có xác nhận của người phụ trách module kia
- [ ] Merge vào `main`, sau đó báo cả nhóm `git pull`

Closes #<!-- số issue, nếu có -->
