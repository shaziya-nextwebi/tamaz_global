var galleryImages = [];
var currentGalleryIndex = 0;
var captchaAnswer = 0;
var thumbSwiper = null;
var isScrollSyncing = false; // prevent feedback loops

function showSnackbar(message) {
    var bar = document.getElementById("snackbar");
    bar.innerText = message;
    bar.style.opacity = "1";
    bar.style.transform = "translateY(0)";
    setTimeout(function () {
        bar.style.opacity = "0";
        bar.style.transform = "translateY(20px)";
    }, 2500);
}

$(document).ready(function () {

    ///* 1. Seed gallery with main product image */
    //var mainImg = document.getElementById('mainProductImage');
    //if (mainImg) {
    //    galleryImages = [mainImg.getAttribute('src')];
    //}

    ///* 2. Load gallery via AJAX then init Swiper */
    //var pid = $('#hdnProductId').val();
    //if (pid && pid !== '') {
    //    $.ajax({
    //        type: 'POST',
    //        url: '/Product.aspx/GetProductGallery',
    //        data: JSON.stringify({ id: pid }),
    //        contentType: 'application/json; charset=utf-8',
    //        dataType: 'json',
    //        success: function (data) {
    //            if (data.d && data.d.length > 0) {
    //                $.each(data.d, function (i, img) {
    //                    galleryImages.push('/' + img.Images);
    //                });
    //            }
    //            initThumbSwiper(); // init after all images known
    //        },
    //        error: function () {
    //            initThumbSwiper(); // init even on error
    //        }
    //    });
    //} else {
    //    initThumbSwiper();
    //}

    /* 1. Gallery starts empty — AJAX will fill it */
    galleryImages = [];

    /* 2. Load gallery via AJAX then init Swiper */
    var pid = $('#hdnProductId').val();
    if (pid && pid !== '') {
        $.ajax({
            type: 'POST',
            url: '/Product.aspx/GetProductGallery',
            data: JSON.stringify({ id: pid }),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (data) {
                if (data.d && data.d.length > 0) {
                    $.each(data.d, function (i, img) {
                        galleryImages.push('/' + img.Images);
                    });
                }

                /* If AJAX returned images, update main image to first gallery image */
                if (galleryImages.length > 0) {
                    var mainImg = document.getElementById('mainProductImage');
                    if (mainImg) {
                        mainImg.src = galleryImages[0];
                    }
                } else {
                    /* Fallback: no gallery images, use the main product image */
                    var mainImg = document.getElementById('mainProductImage');
                    if (mainImg) galleryImages = [mainImg.getAttribute('src')];
                }

                initThumbSwiper();
            },
            error: function () {
                /* Fallback on error */
                var mainImg = document.getElementById('mainProductImage');
                if (mainImg) galleryImages = [mainImg.getAttribute('src')];
                initThumbSwiper();
            }
        });
    } else {
        var mainImg = document.getElementById('mainProductImage');
        if (mainImg) galleryImages = [mainImg.getAttribute('src')];
        initThumbSwiper();
    }
    /* 3. Captcha */
    var c1 = Math.floor(Math.random() * 9) + 1;
    var c2 = Math.floor(Math.random() * 9) + 1;
    captchaAnswer = c1 + c2;
    var label = document.getElementById('captchaLabel');
    if (label) label.innerText = c1 + ' + ' + c2 + ' = ?';

    /* 4. Wholesale modal backdrop */
    var modal = document.getElementById('wholesaleModal');
    if (modal) {
        modal.addEventListener('click', function (e) {
            if (e.target === this) closeWholesaleModal();
        });
    }

    /* 5. Tab buttons */
    document.querySelectorAll('.tab-btn').forEach(function (button) {
        button.addEventListener('click', function () {
            var targetId = this.getAttribute('data-tab');
            var target = document.getElementById(targetId);
            if (target) {
                var top = target.getBoundingClientRect().top + window.pageYOffset - 190;
                window.scrollTo({ top: top, behavior: 'smooth' });
            }
            document.querySelectorAll('.tab-btn').forEach(function (b) { b.classList.remove('active'); });
            this.classList.add('active');
        });
    });
});

function initThumbSwiper() {
    var thumbGrid = document.getElementById('thumbGrid');
    if (!thumbGrid) return;

    /* Clear existing content and rebuild as Swiper structure */
    thumbGrid.innerHTML = '';
    thumbGrid.classList.add('swiper', 'thumb-swiper');

    var wrapper = document.createElement('div');
    wrapper.className = 'swiper-wrapper';

    galleryImages.forEach(function (src, i) {
        var slide = document.createElement('div');
        slide.className = 'swiper-slide';

        var btn = document.createElement('button');
        btn.type = 'button';
        btn.className = 'thumb-btn' + (i === 0 ? ' active-thumb' : '');
        btn.dataset.index = i;

        var img = document.createElement('img');
        img.src = src;
        img.alt = 'Thumbnail ' + (i + 1);

        btn.appendChild(img);
        btn.addEventListener('click', function () {
            setMainImage(i);
        });

        slide.appendChild(btn);
        wrapper.appendChild(slide);
    });

    thumbGrid.appendChild(wrapper);

    /* Swiper navigation buttons */
    var prevBtn = document.createElement('div');
    prevBtn.className = 'swiper-button-prev thumb-nav-prev';

    var nextBtn = document.createElement('div');
    nextBtn.className = 'swiper-button-next thumb-nav-next';

    thumbGrid.appendChild(prevBtn);
    thumbGrid.appendChild(nextBtn);

    /* Init Swiper */
    thumbSwiper = new Swiper('#thumbGrid', {
        slidesPerView: 4,
        spaceBetween: 10,
        watchSlidesProgress: true,
        slideToClickedSlide: false,
        navigation: {
            nextEl: '.thumb-nav-next',
            prevEl: '.thumb-nav-prev',
        },
        breakpoints: {
            0: { slidesPerView: 4, spaceBetween: 8 },
            768: { slidesPerView: 4, spaceBetween: 10 }
        }
    });

    /* ---- Scroll on main image area to change gallery image ---- */
    var mainWrap = document.querySelector('.relative.w-full.aspect-square');
    if (mainWrap) {
        mainWrap.addEventListener('wheel', function (e) {
            e.preventDefault();
            if (isScrollSyncing) return;
            var direction = e.deltaY > 0 ? 1 : -1;
            navigateGallery(direction);
        }, { passive: false });

        /* Touch swipe on main image */
        var touchStartX = 0;
        var touchStartY = 0;
        mainWrap.addEventListener('touchstart', function (e) {
            touchStartX = e.touches[0].clientX;
            touchStartY = e.touches[0].clientY;
        }, { passive: true });

        mainWrap.addEventListener('touchend', function (e) {
            var dx = e.changedTouches[0].clientX - touchStartX;
            var dy = e.changedTouches[0].clientY - touchStartY;
            if (Math.abs(dx) > Math.abs(dy) && Math.abs(dx) > 30) {
                navigateGallery(dx < 0 ? 1 : -1);
            }
        }, { passive: true });
    }
}
function setMainImage(index) {
    if (!galleryImages || galleryImages.length === 0) return;

    currentGalleryIndex = (index + galleryImages.length) % galleryImages.length;
    var src = galleryImages[currentGalleryIndex];

    /* Update main image */
    var mainImg = document.getElementById('mainProductImage');
    if (mainImg) {
        mainImg.style.opacity = '0.6';
        mainImg.src = src;
        mainImg.onload = function () { mainImg.style.opacity = '1'; };
        // fallback if already cached
        if (mainImg.complete) mainImg.style.opacity = '1';
    }

    /* Sync active thumb button */
    document.querySelectorAll('.thumb-btn').forEach(function (btn, i) {
        btn.classList.toggle('active-thumb', i === currentGalleryIndex);
    });

    /* Slide Swiper so active thumb is visible */
    if (thumbSwiper) {
        isScrollSyncing = true;
        thumbSwiper.slideTo(currentGalleryIndex, 300);
        setTimeout(function () { isScrollSyncing = false; }, 400);
    }
}
function switchMainImage(src, btn) {
    var idx = galleryImages.indexOf(src);
    if (idx === -1) idx = 0;
    setMainImage(idx);
}

function navigateGallery(direction) {
    setMainImage(currentGalleryIndex + direction);
}
function openImageModal(index) {
    currentGalleryIndex = (index !== undefined) ? index : currentGalleryIndex;
    var modal = document.getElementById('imageModal');
    var img = document.getElementById('modalImage');
    img.src = galleryImages[currentGalleryIndex];
    updateModalCounter();
    modal.style.display = 'flex';
    document.body.style.overflow = 'hidden';
}

function closeImageModal() {
    document.getElementById('imageModal').style.display = 'none';
    document.body.style.overflow = 'auto';
}

function navigateModal(direction) {
    if (!galleryImages || galleryImages.length === 0) return;
    currentGalleryIndex = (currentGalleryIndex + direction + galleryImages.length) % galleryImages.length;
    var img = document.getElementById('modalImage');
    img.style.opacity = '0';
    setTimeout(function () {
        img.src = galleryImages[currentGalleryIndex];
        img.style.opacity = '1';
        updateModalCounter();
    }, 150);
    /* Sync thumbnails */
    document.querySelectorAll('.thumb-btn').forEach(function (btn, i) {
        btn.classList.toggle('active-thumb', i === currentGalleryIndex);
    });
    if (thumbSwiper) thumbSwiper.slideTo(currentGalleryIndex, 300);
}

function updateModalCounter() {
    var counter = document.getElementById('modalCounter');
    if (counter && galleryImages.length > 1) {
        counter.innerText = (currentGalleryIndex + 1) + ' / ' + galleryImages.length;
        counter.style.display = 'block';
    } else if (counter) {
        counter.style.display = 'none';
    }
}

document.addEventListener('keydown', function (e) {
    var modal = document.getElementById('imageModal');
    if (!modal || modal.style.display === 'none') return;
    if (e.key === 'ArrowRight') navigateModal(1);
    if (e.key === 'ArrowLeft') navigateModal(-1);
    if (e.key === 'Escape') closeImageModal();
});

document.addEventListener('DOMContentLoaded', function () {
    var imgModal = document.getElementById('imageModal');
    if (imgModal) imgModal.addEventListener('click', function (e) {
        if (e.target === this) closeImageModal();
    });
});
function openWholesaleModal() {
    var modal = document.getElementById('wholesaleModal');
    if (!modal) return;
    modal.classList.add('open');
    document.getElementById('modalContent').style.transform = 'scale(1)';
    document.getElementById('modalContent').style.opacity = '1';
    document.body.style.overflow = 'hidden';
}

function closeWholesaleModal() {
    var modal = document.getElementById('wholesaleModal');
    var content = document.getElementById('modalContent');
    if (!modal) return;
    content.style.transform = 'scale(0.95)';
    content.style.opacity = '0';
    setTimeout(function () {
        modal.classList.remove('open');
        document.body.style.overflow = 'auto';
    }, 300);
}

document.addEventListener('click', function (e) {
    var btn = e.target.closest('.faq-question');
    if (btn) {
        var answer = btn.nextElementSibling;
        var icon = btn.querySelector('.faq-icon');
        if (answer) answer.classList.toggle('hidden');
        if (icon) icon.classList.toggle('rotate-180');
    }
});

function getQtyInputs() {
    return document.querySelectorAll('.productQty');
}

function increaseQty() {
    let inputs = getQtyInputs();
    let value = parseInt(inputs[0].value) || 1;
    value++;

    inputs.forEach(i => i.value = value);

    resetCartBtn();
}

function decreaseQty() {
    let inputs = getQtyInputs();
    let value = parseInt(inputs[0].value) || 1;

    if (value > 1) value--;

    inputs.forEach(i => i.value = value);

    resetCartBtn();
}
//function increaseQty() {
//    var qty = document.getElementById('productQty');
//    qty.value = parseInt(qty.value) + 1;
//    resetCartBtn();
//}

//function decreaseQty() {
//    var qty = document.getElementById('productQty');
//    if (parseInt(qty.value) > 1) qty.value = parseInt(qty.value) - 1;
//    resetCartBtn();
//}

function resetCartBtn() {
    var btn = document.getElementById('btnAddToCart');
    var txt = document.getElementById('btnAddToCartText');
    if (btn && btn.classList.contains('view-cart-btn-product')) {
        btn.classList.remove('view-cart-btn-product', 'bg-[#162e7d]');
        btn.classList.add('bg-[#B91C1C]', 'hover:bg-red-700');
        txt.innerText = 'Add to Cart';
        btn.disabled = false;
    }
}
function updateCartBadge(newCount) {
    if (newCount <= 0) return;
    var countEl = document.getElementById('cartCount');
    if (countEl) {
        countEl.innerText = newCount;
        countEl.style.display = 'flex';
    } else {
        var cartIcon = document.querySelector('.relative.p-3');
        if (cartIcon) {
            var span = document.createElement('span');
            span.id = 'cartCount';
            span.className = 'absolute -top-1 -right-1 w-5 h-5 added-cart-count text-white text-xs rounded-full flex items-center justify-center';
            span.style.textAlign = 'center';
            span.innerText = newCount;
            cartIcon.appendChild(span);
        }
    }
    var countElMobile = document.getElementById('cartCountMobile');
    if (countElMobile) {
        countElMobile.innerText = newCount;
        countElMobile.style.display = 'flex';
    } else {
        var mobileCartIcon = document.querySelector('.mobile-cart-icon');
        if (mobileCartIcon) {
            var spanM = document.createElement('span');
            spanM.id = 'cartCountMobile';
            spanM.className = 'absolute -top-1 -right-1 w-5 h-5 added-cart-count text-white text-xs rounded-full flex items-center justify-center';
            spanM.innerText = newCount;
            mobileCartIcon.appendChild(spanM);
        }
    }
}

function addToCartProduct(btn) {
    if (btn.classList.contains('view-cart-btn-product')) {
        window.location.href = '/Cart.aspx';
        return;
    }
    var productId = document.getElementById('hdnProductId').value;
    if (!productId) return;
    var qty = parseInt(document.querySelector('.productQty').value) || 1;
    btn.disabled = true;
    document.getElementById('btnAddToCartText').innerText = 'Adding...';
    fetch('/Default.aspx/AddToCart', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ productId: productId.toString(), qty: qty })
    })
        .then(function (r) { return r.json(); })
        .then(function (data) {
            var result = data.d || data;
            if (result.success) {
                btn.disabled = false;
                btn.classList.remove('bg-[#B91C1C]', 'hover:bg-red-700');
                btn.classList.add('view-cart-btn-product', 'bg-[#e51c4c]');
                document.getElementById('btnAddToCartText').innerText = 'View Cart';
                updateCartBadge(result.cartCount ? parseInt(result.cartCount) : 0);
            } else {
                btn.disabled = false;
                document.getElementById('btnAddToCartText').innerText = 'Add to Cart';
            }
        })
        .catch(function () {
            btn.disabled = false;
            document.getElementById('btnAddToCartText').innerText = 'Add to Cart';
        });
}
function showErr(id, msg) { document.getElementById(id).innerText = msg; }
function showError(id, msg) { document.getElementById(id).innerText = msg; }
function clearEnqErrors() { document.querySelectorAll("[id^='errEnq'], #errCaptcha").forEach(function (e) { e.innerText = ""; }); }
function clearErrors() { document.querySelectorAll("[id^='errWs']").forEach(function (e) { e.innerText = ""; }); }

function submitEnquiry(e) {
    if (e) e.preventDefault();
    clearEnqErrors();
    var name = document.getElementById('txtEnqName').value.trim();
    var city = document.getElementById('txtEnqCity').value.trim();
    var phone = document.getElementById('txtEnqPhone').value.trim();
    var email = document.getElementById('txtEnqEmail').value.trim();
    var message = document.getElementById('txtEnqMessage').value.trim();
    var captcha = document.getElementById('txtCaptcha').value.trim();
    var sourcePage = window.location.href;
    var isValid = true;
    if (!name) { showErr("errEnqName", "Name is required"); isValid = false; }
    if (!phone) { showErr("errEnqPhone", "Phone is required"); isValid = false; }
    if (!email) { showErr("errEnqEmail", "Email is required"); isValid = false; }
    if (!captcha) { showErr("errCaptcha", "Captcha required"); isValid = false; }
    if (parseInt(captcha) !== captchaAnswer) { showErr("errCaptcha", "Incorrect captcha"); isValid = false; }
    if (!isValid) return;
    var btn = document.querySelector('#enquiry button[onclick="submitEnquiry()"]');
    if (btn) { btn.disabled = true; btn.innerText = 'Please Wait...'; }
    $.ajax({
        type: 'POST', url: '/Product.aspx/SubmitEnquiry',
        data: JSON.stringify({ name: name, city: city, phone: phone, email: email, message: message, product: document.getElementById('hdnProductName').value, sourcePage: sourcePage }),
        contentType: 'application/json; charset=utf-8', dataType: 'json',
        success: function (res) {
            if (res.d === 'Success') {
                showSnackbar("Thank you! We will contact you soon.");
                ['txtEnqName', 'txtEnqCity', 'txtEnqPhone', 'txtEnqEmail', 'txtEnqMessage', 'txtCaptcha'].forEach(function (id) { document.getElementById(id).value = ''; });
                var c1 = Math.floor(Math.random() * 9) + 1, c2 = Math.floor(Math.random() * 9) + 1;
                captchaAnswer = c1 + c2;
                document.getElementById('captchaLabel').innerText = c1 + ' + ' + c2 + ' = ?';
                window.location.href = '/thank-you.aspx';
            } else { showSnackbar("Something went wrong. Try again."); }
        }
    });
}

function submitWholesale(e) {
    if (e) e.preventDefault();
    clearErrors();
    var name = document.getElementById('txtWsName').value.trim();
    var city = document.getElementById('txtWsCity').value.trim();
    var phone = document.getElementById('txtWsPhone').value.trim();
    var email = document.getElementById('txtWsEmail').value.trim();
    var message = document.getElementById('txtWsMessage').value.trim();
    var sourcePage = window.location.href;
    var isValid = true;
    if (!name) { showError("errWsName", "Name is required"); isValid = false; }
    if (!phone) { showError("errWsPhone", "Phone is required"); isValid = false; }
    if (!email) { showError("errWsEmail", "Email is required"); isValid = false; }
    if (!isValid) return;
    var btn = document.querySelector('#wholesaleModal button[onclick="submitWholesale()"]');
    if (btn) { btn.disabled = true; btn.innerText = 'Please Wait...'; }
    $.ajax({
        type: 'POST', url: '/Product.aspx/SubmitWholesaleEnquiry',
        data: JSON.stringify({ name: name, city: city, phone: phone, email: email, message: message, product: document.getElementById('hdnProductName').value, sourcePage: sourcePage }),
        contentType: 'application/json; charset=utf-8', dataType: 'json',
        success: function (res) {
            if (res.d === 'Success') {
                showSnackbar("Thank you! We will contact you soon.");
                setTimeout(function () { closeWholesaleModal(); }, 2000);
                ['txtWsName', 'txtWsCity', 'txtWsPhone', 'txtWsEmail', 'txtWsMessage'].forEach(function (id) { document.getElementById(id).value = ''; });
                window.location.href = '/thank-you.aspx';
            } else { showSnackbar("Something went wrong. Try again."); }
        }
    });
}