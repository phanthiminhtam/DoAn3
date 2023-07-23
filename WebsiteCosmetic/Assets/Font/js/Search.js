var search = {
    init: function () {
        search.registerEvent();
    },
    registerEvent: function () {
        $("#txtKeyword").autocomplete({
            minLength: 0,
            source: function (request, response) {
                $.ajax({
                    url: "/Home/ListName",
                    dataType: "json",
                    data: {
                        q: request.term
                    },
                    success: function (data) {
                        if (data.data.length > 0) {
                            let str = ''
                            $.each(data.data, (index, value) => {
                                str += `<li><a style="color:#fff" href="/SanPham/ChitietSP/${value.SpId}">${value.Product.ProName}</a></li>`
                            })
                            $(".result-search").html(str)
                        }
                    }
                });
            },
            focus: function (event, ui) {
                $("#txtKeyword").val(ui.item.label);
                return false;
            },
            select: function (event, ui) {
                $("#txtKeyword").val(ui.item.label)
                return false;
            }
        })
        .autocomplete("instance")._renderItem = function (ul, item) {
            return $("<li>")
                .append("<div>" + item.label+ "</div>")
                .appendTo(ul);
        };
    }
}
search.init();