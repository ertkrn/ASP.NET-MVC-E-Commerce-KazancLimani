$(function () {
    $('#modal_detay').on('show.bs.modal', function (e) {

        var btn = $(e.relatedTarget);
        urunid = btn.data("urun-id");

        $("#modal_detay_body").load("/Urun/UrunBaslikAl/" + urunid);
    })
});