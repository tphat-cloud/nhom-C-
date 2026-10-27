/*
 * Fashion Shop — tương tác giao diện.
 * Nguyên tắc: mọi chức năng quan trọng (chọn size/màu, thêm giỏ, cập nhật số lượng)
 * đều chạy được khi KHÔNG có JavaScript; file này chỉ nâng cấp trải nghiệm.
 */
(function () {
  'use strict';

  // Đánh dấu "đã có JS" để CSS chuyển từ <select> dự phòng sang chip/swatch.
  document.documentElement.classList.remove('no-js');
  document.documentElement.classList.add('js');

  var ready = function (fn) {
    if (document.readyState === 'loading') {
      document.addEventListener('DOMContentLoaded', fn, { once: true });
    } else {
      fn();
    }
  };

  /* ---------- Ảnh lỗi (mất mạng / URL hỏng) → ảnh dự phòng ---------- */

  document.addEventListener(
    'error',
    function (event) {
      var img = event.target;
      if (!(img instanceof HTMLImageElement)) return;

      var fallback = img.getAttribute('data-fallback');
      if (!fallback || img.dataset.fallbackApplied === '1') return;

      img.dataset.fallbackApplied = '1';
      img.src = fallback;
      img.classList.add('is-fallback');
    },
    true
  );

  /* ---------- Thông báo: đóng + tự ẩn ---------- */

  function dismissAlert(alert) {
    alert.classList.add('is-hiding');
    window.setTimeout(function () {
      alert.remove();
    }, 260);
  }

  document.addEventListener('click', function (event) {
    var closeButton = event.target.closest('[data-dismiss="alert"]');
    if (closeButton) {
      var alert = closeButton.closest('.alert');
      if (alert) dismissAlert(alert);
    }
  });

  ready(function () {
    document.querySelectorAll('.alert[data-autohide]').forEach(function (alert) {
      var delay = Number(alert.getAttribute('data-autohide')) || 6000;
      window.setTimeout(function () {
        if (document.body.contains(alert)) dismissAlert(alert);
      }, delay);
    });
  });

  /* ---------- Header: đổ bóng khi cuộn ---------- */

  ready(function () {
    var header = document.querySelector('.site-header');
    if (!header) return;

    var ticking = false;

    function update() {
      header.classList.toggle('is-stuck', window.scrollY > 8);
      ticking = false;
    }

    window.addEventListener(
      'scroll',
      function () {
        if (!ticking) {
          ticking = true;
          window.requestAnimationFrame(update);
        }
      },
      { passive: true }
    );

    update();
  });

  /* ---------- Nút tăng/giảm số lượng ---------- */

  ready(function () {
    document.querySelectorAll('.qty').forEach(function (stepper) {
      var input = stepper.querySelector('input[type="number"], input');
      if (!input) return;

      var min = Number(input.getAttribute('min')) || 1;
      var max = Number(input.getAttribute('max')) || 99;

      function clamp(value) {
        if (Number.isNaN(value)) return min;
        return Math.min(max, Math.max(min, value));
      }

      stepper.querySelectorAll('button[data-step]').forEach(function (button) {
        button.addEventListener('click', function (event) {
          event.preventDefault();

          var step = Number(button.getAttribute('data-step')) || 1;
          input.value = clamp((Number(input.value) || min) + step);
          input.dispatchEvent(new Event('change', { bubbles: true }));
        });
      });

      input.addEventListener('change', function () {
        input.value = clamp(Number(input.value));
      });
    });
  });

  /* ---------- Giỏ hàng: tự cập nhật khi đổi số lượng ---------- */

  ready(function () {
    var timers = new WeakMap();

    document.querySelectorAll('input[data-autosubmit]').forEach(function (input) {
      var form = input.form;
      if (!form) return;

      input.addEventListener('change', function () {
        window.clearTimeout(timers.get(input));

        timers.set(
          input,
          window.setTimeout(function () {
            if (typeof form.requestSubmit === 'function') {
              form.requestSubmit();
            } else {
              form.submit();
            }
          }, 650)
        );
      });
    });
  });

  /* ---------- Hành động cần xác nhận ---------- */

  document.addEventListener('click', function (event) {
    var trigger = event.target.closest('[data-confirm]');
    if (!trigger) return;

    if (!window.confirm(trigger.getAttribute('data-confirm'))) {
      event.preventDefault();
      event.stopPropagation();
    }
  });

  /* ---------- Chống bấm gửi hai lần ---------- */

  ready(function () {
    document.querySelectorAll('form[data-submit-once]').forEach(function (form) {
      form.addEventListener('submit', function () {
        window.setTimeout(function () {
          form.querySelectorAll('button[type="submit"]').forEach(function (button) {
            button.disabled = true;
          });
        }, 30);
      });
    });
  });

  /* ---------- Sao chép mã đơn hàng ---------- */

  ready(function () {
    document.querySelectorAll('[data-copy]').forEach(function (button) {
      button.addEventListener('click', function () {
        var value = button.getAttribute('data-copy') || '';
        var original = button.textContent;

        function done() {
          button.textContent = 'Đã chép';
          window.setTimeout(function () {
            button.textContent = original;
          }, 1800);
        }

        function fallbackCopy() {
          var area = document.createElement('textarea');
          area.value = value;
          area.setAttribute('readonly', 'readonly');
          area.style.position = 'fixed';
          area.style.opacity = '0';
          document.body.appendChild(area);
          area.select();
          try {
            document.execCommand('copy');
            done();
          } catch (error) {
            /* Trình duyệt chặn copy: khách vẫn đọc được mã trên màn hình. */
          }
          document.body.removeChild(area);
        }

        if (navigator.clipboard && window.isSecureContext) {
          navigator.clipboard.writeText(value).then(done, fallbackCopy);
        } else {
          fallbackCopy();
        }
      });
    });
  });

  /* ---------- Hiệu ứng xuất hiện khi cuộn ---------- */

  ready(function () {
    var items = document.querySelectorAll('.reveal');
    if (items.length === 0) return;

    var prefersReduced = window.matchMedia('(prefers-reduced-motion: reduce)').matches;
    if (prefersReduced || !('IntersectionObserver' in window)) {
      items.forEach(function (item) {
        item.classList.add('is-visible');
      });
      return;
    }

    var observer = new IntersectionObserver(
      function (entries) {
        entries.forEach(function (entry) {
          if (entry.isIntersecting) {
            entry.target.classList.add('is-visible');
            observer.unobserve(entry.target);
          }
        });
      },
      { rootMargin: '0px 0px -8% 0px', threshold: 0.08 }
    );

    items.forEach(function (item) {
      observer.observe(item);
    });
  });

  /* ---------- Chọn size / màu ở trang chi tiết ----------
     Dữ liệu biến thể do Razor đẩy xuống trong <script type="application/json" id="variantData">.
     Nếu không có JS: form vẫn dùng <select name="variantId"> và gửi bình thường.
  ------------------------------------------------------------------ */

  ready(function () {
    var dataElement = document.getElementById('variantData');
    var form = document.getElementById('pdpAddForm');
    if (!dataElement || !form) return;

    var variants;
    try {
      variants = JSON.parse(dataElement.textContent || '[]');
    } catch (error) {
      return;
    }

    if (!Array.isArray(variants) || variants.length === 0) return;

    var select = form.querySelector('select[name="variantId"]');
    var sizeGroup = form.querySelector('[data-size-group]');
    var colorGroup = form.querySelector('[data-color-group]');
    var note = form.querySelector('[data-picker-note]');
    var sizeValue = form.querySelector('[data-size-value]');
    var colorValue = form.querySelector('[data-color-value]');
    var addButtons = document.querySelectorAll('[data-add-button]');

    if (!select || !sizeGroup || !colorGroup) return;

    var sizeInputs = Array.prototype.slice.call(sizeGroup.querySelectorAll('input[type="radio"]'));
    var colorInputs = Array.prototype.slice.call(colorGroup.querySelectorAll('input[type="radio"]'));

    function checkedValue(inputs) {
      var checked = inputs.filter(function (input) {
        return input.checked;
      })[0];
      return checked ? checked.value : '';
    }

    function setChecked(inputs, value) {
      inputs.forEach(function (input) {
        input.checked = input.value === value;
      });
    }

    function findVariant(size, color) {
      return (
        variants.filter(function (variant) {
          return variant.size === size && variant.color === color;
        })[0] || null
      );
    }

    function stockState(size, color) {
      return variants.some(function (variant) {
        return variant.size === size && variant.color === color && variant.available;
      });
    }

    function markAvailability(size, color) {
      sizeInputs.forEach(function (input) {
        var available = stockState(input.value, color);
        input.closest('.chip').classList.toggle('is-unavailable', !available);
      });

      colorInputs.forEach(function (input) {
        var available = stockState(size, input.value);
        input.closest('.swatch').classList.toggle('is-unavailable', !available);
      });
    }

    function setButtonsEnabled(enabled) {
      addButtons.forEach(function (button) {
        button.disabled = !enabled;
      });
    }

    function refresh() {
      var size = checkedValue(sizeInputs);
      var color = checkedValue(colorInputs);
      var variant = findVariant(size, color);

      if (sizeValue) sizeValue.textContent = size || '—';
      if (colorValue) colorValue.textContent = color || '—';

      markAvailability(size, color);

      if (!variant) {
        select.value = '';
        setButtonsEnabled(false);
        if (note) {
          note.textContent = 'Kết hợp size/màu này chưa có trong cửa hàng. Vui lòng chọn lựa chọn khác.';
          note.classList.add('is-out');
        }
        return;
      }

      select.value = String(variant.id);
      setButtonsEnabled(variant.available);

      if (note) {
        note.classList.toggle('is-out', !variant.available);
        note.textContent = 'Size ' + variant.size + ' · ' + variant.color + ' — ' +
          (variant.available ? (variant.low ? 'Sắp hết hàng' : 'Còn hàng')
                             : 'Hết hàng, vui lòng chọn lựa chọn khác.');
      }
    }

    sizeInputs.forEach(function (input) {
      input.addEventListener('change', refresh);
    });

    colorInputs.forEach(function (input) {
      input.addEventListener('change', refresh);
    });

    // Server luôn chọn sẵn một biến thể; nếu thiếu thì chọn biến thể còn hàng đầu tiên.
    if (!checkedValue(sizeInputs) || !checkedValue(colorInputs)) {
      var first =
        variants.filter(function (variant) {
          return variant.available;
        })[0] || variants[0];

      setChecked(sizeInputs, first.size);
      setChecked(colorInputs, first.color);
    }

    refresh();
  });

  /* ---------- "Đơn hàng gần đây của bạn" (LocalStorage) ----------
     - Trang "Đặt hàng thành công" và trang kết quả tra cứu đẩy cặp
       { orderCode, phone, orderDate, totalAmount } xuống qua
       <script type="application/json" id="recentOrderData">.
     - Trang tra cứu dựng lại danh sách để khách bấm "Xem nhanh" tra cứu 1 cú bấm.
     - Lịch sử chỉ nằm trên trình duyệt của khách, tối đa 5 đơn gần nhất.
  ------------------------------------------------------------------ */

  var RecentOrders = (function () {
    var STORAGE_KEY = 'fashionshop.recentOrders';
    var MAX_ORDERS = 5;

    function storage() {
      try {
        // Truy cập localStorage có thể ném lỗi (chế độ riêng tư của một số trình duyệt)
        return window.localStorage || null;
      } catch (error) {
        return null;
      }
    }

    // Chuẩn hóa 1 bản ghi: loại bỏ dữ liệu rác do khách sửa tay trong DevTools
    function normalize(entry) {
      if (!entry || typeof entry !== 'object') return null;

      var orderCode = typeof entry.orderCode === 'string' ? entry.orderCode.trim() : '';
      var phone = typeof entry.phone === 'string' ? entry.phone.trim() : '';
      if (!orderCode || !phone) return null;

      var totalAmount = Number(entry.totalAmount);

      return {
        orderCode: orderCode,
        phone: phone,
        orderDate: typeof entry.orderDate === 'string' ? entry.orderDate : '',
        totalAmount: isFinite(totalAmount) ? totalAmount : 0
      };
    }

    function read() {
      var store = storage();
      if (!store) return [];

      try {
        var raw = store.getItem(STORAGE_KEY);
        if (!raw) return [];

        var parsed = JSON.parse(raw);
        if (!Array.isArray(parsed)) return [];

        var list = [];
        parsed.forEach(function (entry) {
          var order = normalize(entry);
          if (order) list.push(order);
        });

        return list.slice(0, MAX_ORDERS);
      } catch (error) {
        // Dữ liệu hỏng (phiên bản cũ, khách sửa tay) -> bỏ qua, không làm sập trang
        return [];
      }
    }

    function write(list) {
      var store = storage();
      if (!store) return;

      try {
        store.setItem(STORAGE_KEY, JSON.stringify(list.slice(0, MAX_ORDERS)));
      } catch (error) {
        // Hết dung lượng hoặc bị chặn: đây chỉ là tiện ích, không cần báo lỗi
      }
    }

    // Lưu đơn mới lên đầu; đơn đã có (cùng mã) thì cập nhật và đẩy lên đầu
    function save(order) {
      var entry = normalize(order);
      if (!entry) return;

      var code = entry.orderCode.toLowerCase();
      var list = read().filter(function (item) {
        return item.orderCode.toLowerCase() !== code;
      });

      list.unshift(entry);
      write(list);
    }

    function formatDate(value) {
      var date = new Date(value);
      if (isNaN(date.getTime())) return '';

      function pad(number) {
        return number < 10 ? '0' + number : String(number);
      }

      return pad(date.getDate()) + '/' + pad(date.getMonth() + 1) + '/' + date.getFullYear();
    }

    function formatAmount(value) {
      var amount = Number(value);
      if (!isFinite(amount) || amount <= 0) return '';

      try {
        return amount.toLocaleString('vi-VN') + ' đ';
      } catch (error) {
        return String(amount) + ' đ';
      }
    }

    // Chỉ hiển thị 3 số đầu/cuối để lộ trình duyệt chung không thấy toàn bộ SĐT
    function maskPhone(phone) {
      var digits = phone.replace(/\D/g, '');
      if (digits.length < 7) return phone;
      return digits.slice(0, 3) + '***' + digits.slice(-3);
    }

    // Điền sẵn Mã đơn + SĐT rồi gửi form tra cứu (khách không phải gõ gì)
    function quickLookup(order) {
      var codeInput = document.getElementById('orderCode');
      var phoneInput = document.getElementById('phoneNumber');
      if (!codeInput || !phoneInput) return;

      var form = codeInput.form || phoneInput.form;
      if (!form) return;

      codeInput.value = order.orderCode;
      phoneInput.value = order.phone;

      // Báo cho các listener khác biết giá trị vừa đổi (đồng bộ với thao tác gõ tay)
      codeInput.dispatchEvent(new Event('input', { bubbles: true }));
      phoneInput.dispatchEvent(new Event('input', { bubbles: true }));

      if (typeof form.requestSubmit === 'function') {
        form.requestSubmit();
      } else {
        form.submit();
      }
    }

    function render() {
      var container = document.querySelector('[data-recent-orders]');
      if (!container) return;

      var listElement = container.querySelector('[data-recent-orders-list]');
      var template = document.getElementById('recentOrderTemplate');
      if (!listElement || !template) return;

      var orders = read();
      listElement.textContent = '';

      if (orders.length === 0) {
        container.hidden = true;
        return;
      }

      orders.forEach(function (order) {
        var fragment = template.content.cloneNode(true);
        var row = fragment.querySelector('.recent-order');
        if (!row) return;

        row.querySelector('[data-field="orderCode"]').textContent = order.orderCode;

        var meta = [];
        var date = formatDate(order.orderDate);
        if (date) meta.push(date);

        var amount = formatAmount(order.totalAmount);
        if (amount) meta.push(amount);

        meta.push('SĐT ' + maskPhone(order.phone));
        row.querySelector('[data-field="meta"]').textContent = meta.join(' · ');

        var button = row.querySelector('[data-quick-lookup]');
        if (button) {
          button.setAttribute('aria-label', 'Xem nhanh đơn hàng ' + order.orderCode);
          button.addEventListener('click', function () {
            quickLookup(order);
          });
        }

        listElement.appendChild(fragment);
      });

      container.hidden = false;
    }

    function clear() {
      var store = storage();
      if (store) {
        try {
          store.removeItem(STORAGE_KEY);
        } catch (error) {
          /* bị chặn: bỏ qua */
        }
      }

      render();
    }

    // Đọc dữ liệu server đẩy xuống (trang đặt hàng thành công / kết quả tra cứu)
    function saveFromPage() {
      var dataElement = document.getElementById('recentOrderData');
      if (!dataElement) return;

      try {
        save(JSON.parse(dataElement.textContent || '{}'));
      } catch (error) {
        /* JSON hỏng: bỏ qua */
      }
    }

    return { saveFromPage: saveFromPage, render: render, clear: clear };
  })();

  ready(function () {
    // Trang xác nhận đặt hàng / trang có kết quả tra cứu: ghi lại cặp Mã đơn + SĐT
    RecentOrders.saveFromPage();

    // Trang tra cứu: dựng danh sách "Lịch sử đơn hàng trên thiết bị này"
    RecentOrders.render();

    var clearButton = document.querySelector('[data-recent-orders-clear]');
    if (clearButton) {
      clearButton.addEventListener('click', function () {
        if (!window.confirm('Xóa toàn bộ lịch sử đơn hàng đã lưu trên thiết bị này?')) return;
        RecentOrders.clear();
      });
    }
  });
})();


