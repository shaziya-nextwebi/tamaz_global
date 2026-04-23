var galleryImages = [];
var captchaAnswer = 0;

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
   
    var mainImg = document.getElementById('mainProductImage');
    if (mainImg) {
        galleryImages = [mainImg.src];
    }

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
                        var src = '/' + img.Images;
                        galleryImages.push(src);
                        var btn = $('<button type="button" class="thumb-btn"></button>');
                        btn.attr('onclick', "switchMainImage('" + src + "', this)");
                        btn.append('<img src="' + src + '" alt="Gallery ' + (i + 1) + '" />');
                        $('#thumbGrid').append(btn);
                    });
                }
            }
        });
    }
});

function switchMainImage(src, btn) {
    document.getElementById('mainProductImage').src = src;
    document.querySelectorAll('.thumb-btn').forEach(function (b) {
        b.classList.remove('active-thumb');
    });
    btn.classList.add('active-thumb');

    // ← ADD THIS: keep index in sync
    var idx = galleryImages.indexOf(src);
    if (idx !== -1) currentGalleryIndex = idx;
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


    document.querySelectorAll('.thumb-btn').forEach(function (btn, i) {
        btn.classList.toggle('active-thumb', i === currentGalleryIndex);
    });
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
// Keyboard navigation for modal
document.addEventListener('keydown', function (e) {
    var modal = document.getElementById('imageModal');
    if (!modal || modal.style.display === 'none') return;
    if (e.key === 'ArrowRight') navigateModal(1);
    if (e.key === 'ArrowLeft') navigateModal(-1);
    if (e.key === 'Escape') closeImageModal();
});

// Click backdrop to close
document.getElementById('imageModal').addEventListener('click', function (e) {
    if (e.target === this) closeImageModal();
});
function openWholesaleModal() {
    var modal = document.getElementById('wholesaleModal');
    if (!modal) return;
    modal.classList.add('open');
    var content = document.getElementById('modalContent');
    content.style.transform = 'scale(1)';
    content.style.opacity = '1';
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

document.addEventListener("DOMContentLoaded", function () {

    var modal = document.getElementById('wholesaleModal');
    if (modal) {
        modal.addEventListener('click', function (e) {
            if (e.target === this) closeWholesaleModal();
        });
    }

    document.querySelectorAll('.tab-btn').forEach(function (button) {
        button.addEventListener('click', function () {
            var targetId = this.getAttribute('data-tab');
            var target = document.getElementById(targetId);
            if (target) {
                var headerOffset = 190;
                var top = target.getBoundingClientRect().top + window.pageYOffset - headerOffset;
                window.scrollTo({ top: top, behavior: 'smooth' });
            }
            document.querySelectorAll('.tab-btn').forEach(function (b) { b.classList.remove('active'); });
            this.classList.add('active');
        });
    });

    var c1 = Math.floor(Math.random() * 9) + 1;
    var c2 = Math.floor(Math.random() * 9) + 1;
    captchaAnswer = c1 + c2;
    var label = document.getElementById('captchaLabel');
    if (label) {
        label.innerText = c1 + ' + ' + c2 + ' = ?';
    }
});

document.addEventListener('click', function (e) {
    var btn = e.target.closest('.faq-question');
    if (btn) {
        var answer = btn.nextElementSibling;
        var icon = btn.querySelector('.faq-icon');
        if (answer) answer.classList.toggle('hidden');
        if (icon) icon.classList.toggle('rotate-180');
    }
});

function increaseQty() {
    var qty = document.getElementById('productQty');
    qty.value = parseInt(qty.value) + 1;
    resetCartBtn();
}

function decreaseQty() {
    var qty = document.getElementById('productQty');
    if (parseInt(qty.value) > 1) qty.value = parseInt(qty.value) - 1;
    resetCartBtn();
}

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

    // Desktop badge
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

    // Mobile badge
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

    var qty = parseInt(document.getElementById('productQty').value) || 1;

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
                btn.classList.add('view-cart-btn-product', 'bg-[#162e7d]');
                document.getElementById('btnAddToCartText').innerText = 'View Cart';

                var newCount = result.cartCount ? parseInt(result.cartCount) : 0;
                updateCartBadge(newCount);
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

function showErr(id, msg) {
    document.getElementById(id).innerText = msg;
}

function clearEnqErrors() {
    document.querySelectorAll("[id^='errEnq'], #errCaptcha").forEach(function (e) { e.innerText = ""; });
}

function submitEnquiry(e) {
    if (e) e.preventDefault();
    clearEnqErrors();

    var name = document.getElementById('txtEnqName').value.trim();
    var city = document.getElementById('txtEnqCity').value.trim();
    var phone = document.getElementById('txtEnqPhone').value.trim();
    var email = document.getElementById('txtEnqEmail').value.trim();
    var message = document.getElementById('txtEnqMessage').value.trim();
    var captcha = document.getElementById('txtCaptcha').value.trim();
    var isValid = true;

    if (!name) { showErr("errEnqName", "Name is required"); isValid = false; }
    if (!phone) { showErr("errEnqPhone", "Phone is required"); isValid = false; }
    if (!email) { showErr("errEnqEmail", "Email is required"); isValid = false; }
    if (!captcha) { showErr("errCaptcha", "Captcha required"); isValid = false; }
    if (parseInt(captcha) !== captchaAnswer) { showErr("errCaptcha", "Incorrect captcha"); isValid = false; }
    if (!isValid) return;

    $.ajax({
        type: 'POST',
        url: '/Product.aspx/SubmitEnquiry',
        data: JSON.stringify({
            name: name, city: city, phone: phone, email: email, message: message,
            product: document.getElementById('hdnProductName').value
        }),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (res) {
            if (res.d === 'Success') {
                showSnackbar("Thank you! We will contact you soon.");
                document.getElementById('txtEnqName').value = "";
                document.getElementById('txtEnqCity').value = "";
                document.getElementById('txtEnqPhone').value = "";
                document.getElementById('txtEnqEmail').value = "";
                document.getElementById('txtEnqMessage').value = "";
                document.getElementById('txtCaptcha').value = "";
                var c1 = Math.floor(Math.random() * 9) + 1;
                var c2 = Math.floor(Math.random() * 9) + 1;
                captchaAnswer = c1 + c2;
                document.getElementById('captchaLabel').innerText = c1 + ' + ' + c2 + ' = ?';

                window.location.href = '/thank-you.aspx';

            } else {
                showSnackbar("Something went wrong. Try again.");
            }
        }
    });
}

function showError(id, msg) {
    document.getElementById(id).innerText = msg;
}

function clearErrors() {
    document.querySelectorAll("[id^='errWs']").forEach(function (e) { e.innerText = ""; });
}

function submitWholesale(e) {
    if (e) e.preventDefault();
    clearErrors();

    var name = document.getElementById('txtWsName').value.trim();
    var city = document.getElementById('txtWsCity').value.trim();
    var phone = document.getElementById('txtWsPhone').value.trim();
    var email = document.getElementById('txtWsEmail').value.trim();
    var message = document.getElementById('txtWsMessage').value.trim();
    var isValid = true;

    if (!name) { showError("errWsName", "Name is required"); isValid = false; }
    if (!phone) { showError("errWsPhone", "Phone is required"); isValid = false; }
    if (!email) { showError("errWsEmail", "Email is required"); isValid = false; }
    if (!isValid) return;

    $.ajax({
        type: 'POST',
        url: '/Product.aspx/SubmitWholesaleEnquiry',
        data: JSON.stringify({
            name: name, city: city, phone: phone, email: email, message: message,
            product: document.getElementById('hdnProductName').value
        }),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (res) {
            if (res.d === 'Success') {
                showSnackbar("Thank you! We will contact you soon.");
                setTimeout(function () { closeWholesaleModal(); }, 2000);
                document.getElementById('txtWsName').value = "";
                document.getElementById('txtWsCity').value = "";
                document.getElementById('txtWsPhone').value = "";
                document.getElementById('txtWsEmail').value = "";
                document.getElementById('txtWsMessage').value = "";

                window.location.href = '/thank-you.aspx';
            } else {
                showSnackbar("Something went wrong. Try again.");
            }
        }
    });
}
var currentGalleryIndex = 0;

function navigateGallery(direction) {
    if (!galleryImages || galleryImages.length === 0) return;

    currentGalleryIndex = (currentGalleryIndex + direction + galleryImages.length) % galleryImages.length;

    var newSrc = galleryImages[currentGalleryIndex];
    document.getElementById('mainProductImage').src = newSrc;

    // Sync active thumbnail
    var thumbBtns = document.querySelectorAll('.thumb-btn');
    thumbBtns.forEach(function (btn, i) {
        btn.classList.toggle('active-thumb', i === currentGalleryIndex);
    });
}