
$(document).on('click', '.bookModalIcon', function (e) {
    e.preventDefault();
    let url = $(this).attr("href");
    fetch(url)
        .then(response => response.text())
        .then(data => {
            $("#quickModal .modal-dialog").html(data);
            $("#quickModal").modal("show");

            // Инициализация слайдера после загрузки
            $("#quickModal .product-details-slider").slick({
                slidesToShow: 1,
                arrows: false,
                fade: true,
                draggable: false,
                swipe: false,
                asNavFor: "#quickModal .product-slider-nav"
            });
            $("#quickModal .product-slider-nav").slick({
                infinite: true,
                slidesToShow: 4,
                arrows: true,
                focusOnSelect: true,
                asNavFor: "#quickModal .product-details-slider"
            });
        });
});