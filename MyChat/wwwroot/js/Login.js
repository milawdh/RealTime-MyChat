function SendLogin(form) {
    var data = new FormData(form);
    $.ajax({
        url: '/Login/LoginAction',
        type: 'POST',
        data: data,
        processData: false,
        contentType: false,
        //xhr: function () {
        //    var xhr = new window.XMLHttpRequest();
        //    xhr.upload.addEventListener("progress", function (evt) {
        //        if (evt.lengthComputable) {
        //            var percentComplete = evt.loaded / evt.total;
        //            $('.progress-bar').css({
        //                width: percentComplete * 100 + '%'
        //            });
        //            if (percentComplete === 1) {
        //                $('.progress-bar').addClass('bg-success');
        //            }
        //        }
        //    }, false);
        //    return xhr;
        //},
        success: function (data) {
            SetSuccess()
        },
        error: function (data) {
            ShowErrors(data.responseJSON.errors)
        }
    });
}
function SendRegister(form) {
    var data = new FormData(form);
    $.ajax({
        url: '/Login/RegisterAction',
        type: 'POST',
        data: data,
        processData: false,
        contentType: false,
        success: function (data) {
            UIkit.notification({
                message: `<span class="_MyFont">Succeed!</span>`,
                status: 'success',
                timeout: 2000,
                pos: 'top-right'
            })
            setTimeout(function () {
                window.location.href = "/Chat"
            }, 2000)
        },
        error: function (data) {
            ShowErrors(data.responseJSON.errors)
        }
    });
}

