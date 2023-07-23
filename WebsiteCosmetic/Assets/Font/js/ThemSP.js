var cart = {
    init: function () {
        cart.regEvents();
    },
    regEvents: function () {
        $('#btnDelete').off('click').on('click', function (e) {
            e.preventDefault();
            $.ajax({
                data: { id: $(this).data('id') },
                url: '/Home/DeleteCartMini',
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/Home/Index";
                    }
                }
            })
        })
    }
}
cart.init();