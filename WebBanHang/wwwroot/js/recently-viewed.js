(() => {
  const section = document.querySelector('[data-recently-viewed]');
  if (!section) return;

  const storageKey = 'fashionshop.recentlyViewed';
  const historyLimit = 6;
  const { productId, productName, productPrice, productImage, detailUrl, fallbackImage } = section.dataset;
  const currentProduct = {
    id: Number(productId),
    name: productName,
    price: productPrice,
    image: productImage
  };

  function readHistory(raw) {
    let entries;
    try {
      entries = JSON.parse(raw);
    } catch {
      return [];
    }

    if (!Array.isArray(entries)) return [];

    const products = new Map();
    for (const entry of entries) {
      if (!entry || !Number.isSafeInteger(entry.id) || entry.id <= 0 ||
          typeof entry.name !== 'string' || !entry.name.trim() ||
          typeof entry.price !== 'string' || typeof entry.image !== 'string' ||
          products.has(entry.id)) continue;

      products.set(entry.id, {
        id: entry.id,
        name: entry.name,
        price: entry.price,
        image: entry.image
      });
    }

    return Array.from(products.values());
  }

  function getImageUrl(value) {
    if (!value.trim()) return fallbackImage;

    try {
      const url = new URL(value, document.baseURI);
      if (url.protocol === 'http:' || url.protocol === 'https:') return url.href;
    } catch {
      // Dùng ảnh mặc định nếu đường dẫn đã lưu không hợp lệ.
    }
    return fallbackImage;
  }

  const list = section.querySelector('[data-recently-viewed-list]');
  const template = section.querySelector('template');
  let previousProducts;
  try {
    const storage = window.localStorage;
    previousProducts = readHistory(storage.getItem(storageKey))
      .filter(product => product.id !== currentProduct.id)
      .slice(0, historyLimit - 1);
    storage.setItem(storageKey, JSON.stringify([currentProduct, ...previousProducts]));
  } catch {
    return;
  }

  if (previousProducts.length === 0) return;

  for (const product of previousProducts) {
    const card = template.content.cloneNode(true);
    const url = detailUrl.replace('PRODUCT_ID', String(product.id));
    const nameLink = card.querySelector('[data-product-link]');
    const image = card.querySelector('img');

    nameLink.href = url;
    nameLink.textContent = product.name;
    card.querySelector('.product-card__media').href = url;
    card.querySelector('.price').textContent = product.price;
    image.dataset.fallback = fallbackImage;
    image.src = getImageUrl(product.image);
    list.appendChild(card);
  }

  section.hidden = false;
})();
