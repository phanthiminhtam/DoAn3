var contact = {
    init: function () {
        contact.registerEvents();
    },
    registerEvents: function () {
        $('#btnSend').off('click').on('click', function (e) {
            e.preventDefault();
            var name = $('#txtName').val();
            var email = $('#txtEmail').val();
            var mobile = $('#txtMobile').val();
            var content = $('#txtContent').val();

            $.ajax({
                url: '/Home/Send',
                type: 'POST',
                dataType: 'json',
                data: {
                    name: name,
                    email: email,
                    mobile: mobile,
                    content: content
                },
                success: function (res) {
                    if (res.status == true) {
                        alert('Gửi liên hệ thành công!');
                        review.resetForm();
                    }
                }
            });
        });
    },
    resetForm: function () {
        $('#txtName').val('');
        $('#txtEmail').val('');
        $('#txtMobile').val('');
        $('#txtContent').val('');
    }
}
review.init();