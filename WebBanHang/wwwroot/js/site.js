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
})();


