

var review = {
    init: function () {
        review.registerEvents();
    },
    registerEvents: function () {
        $('#btnSend').off('click').on('click', function (e) {
            e.preventDefault();
            var phonenumber = $('#txtPhoneNumber').val();
            var email = $('#txtEmail').val();
            var vote = $('#Vote').val();
            var content = $('#txtContent').val();

            $.ajax({
                url: '/SanPham/SendReview',
                type: 'POST',
                dataType: 'json',
                data: {
                    phonenumber: phonenumber,
                    email: email,
                    vote: vote,
                    content: content
                },
                success: function (res) {
                    if (res.status == true) {
                        alert('Gửi đánh giá thành công! Vui lòng chờ duyệt!');
                        review.resetForm();
                    }
                    else {
                        alert('Tài khoản của bạn chưa được phép đánh giá!');
                        review.resetForm();
                    }
                }
            });
        });
    },
    resetForm: function () {
        $('#txtPhoneNumber').val('');
        $('#txtEmail').val('');
        $('#Vote').val('');
        $('#txtContent').val('');
    }
}
review.init();