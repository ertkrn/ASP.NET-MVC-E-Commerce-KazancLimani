function readURL(input) {
    var url = input.value;
    var ext = url.substring(url.lastIndexOf('.') + 1).toLowerCase();
    if (input.files && input.files[0] && (ext == "gif" || ext == "png" || ext == "jpg" || ext == "jpeg")) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#gorsel').attr('src', e.target.result);
        }
        reader.readAsDataURL(input.files[0]);
        if (input.files.length > 1 && input.files.length < 6) {
            for (var i = 1; i < input.files.length; i++) {
                var okunan = new FileReader();
                okunan.onload = function (e) {
                    $('#kayit').after('<div class="thumbnail col-md-3 col-sm-12"><img src="' + e.target.result + '" style="width:140px;height:75px;" id="gorsel"/></div>')
                }
                okunan.readAsDataURL(input.files[i]);
                //document.getElementById("kayit").insertAdjacentHTML('afterbegin', '<div class="thumbnail span2"><img src="' +  + '" style="width:140px;height:75px;" id="gorsel"/></div >');
            }
        }
        else if (input.files.length >= 6) {
            alert("5 fotoğraftan fazla yükleyemezsiniz.");
            location.reload();
        }
    }
    else {
        $('#gorsel').attr('src', '~/Content/Image/fotoyok.jpg');
    }
}