
function AddCart(id) {
    $.ajax({
        url: '/Home/AddCart/' + id,
        dataType: 'json',
        type: 'GET',
        success: function (res) {
            let box = $(".offcanvas-cart")
            let str = ""
            $.each(res.list, (index, value) => {
                str += `<li class="offcanvas-cart-item-single">
            <div class="offcanvas-cart-item-block">
                <a href="#" class="offcanvas-cart-item-image-link">
                    <img src="${value.SpecificProduct.Image}" alt="" class="offcanvas-cart-image">
                </a>
                <div class="offcanvas-cart-item-content">
                    <a href="#" class="offcanvas-cart-item-link">${value.SpecificProduct.Product.ProName}</a>
                    <div class="offcanvas-cart-item-details">
                        <span class="offcanvas-cart-item-details-quantity">${value.Quantity} x </span>
                            <span class="offcanvas-cart-item-details-price price">${value.SpecificProduct.DiscountPrice}</span>
                    </div>
                </div>
            </div>
            <div class="offcanvas-cart-item-delete text-right">
                <a href="#" class="offcanvas-cart-item-delete" id="btnDelete" data-id="${id}"><i class="fa fa-trash-o"></i></a>
            </div>
            </li>`
            })
            $(box).html(str)
            $(".offcanvas-cart-total-price-text span").text(res.totalMoney)
            $(".quantity-minicart").text(res.list.length)
        }
    })
}