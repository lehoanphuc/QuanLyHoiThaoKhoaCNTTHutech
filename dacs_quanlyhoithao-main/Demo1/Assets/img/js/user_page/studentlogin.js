var _loginLoading, _loginContent;

document.addEventListener("DOMContentLoaded", function (event) {
    loadDataLogin();
    loadEventLogin();
});

function loadDataLogin() {
    _loginLoading = $("#loginLoading");
    _loginContent = $("#loginContent");
}

function loadEventLogin() {
    document.getElementById('regCV').addEventListener('change', function (e) {
        var fileName = document.getElementById("regCV").files[0].name;
        var nextSibling = e.target.nextElementSibling
        nextSibling.innerText = fileName
    })
}

function openRegTab() {
    $('#loginTab a[href="#regContentTab"]').tab('show')
}

function openLoginTab() {
    $('#loginTab a[href="#loginContentTab"]').tab('show')
}

function openForgotPasswordTab() {
    $('#loginTab a[href="#forgotpassword"]').tab('show')
}

function resetPassword() {
    let username = $("#forgotUsername").val();
    let email = $("#forgotEmail").val();

    if (username == "" || email == "") {
        alert("Vui lòng nhập đủ thông tin bên trên");
        return;
    }

    $("#resetSubmit").prop('disabled', true);
    _loginLoading.show();
    _loginContent.hide();

    grecaptcha.ready(function () {
        grecaptcha.execute('6Le2hPsUAAAAALzwDvu8vWcd7ImuBl_l49f927gN', { action: 'submit' }).then(function (token) {
            // Call api dologin
            $.ajax({
                url: _API_FORGOTPASSWORD,
                type: 'POST',
                data: {
                    username,
                    email,
                    token
                }
            }).done(function (result) {
                alert(result.message);

                if (result.success) {
                    location.reload(); zz
                }

                $("#resetSubmit").prop('disabled', false);
                _loginLoading.hide();
                _loginContent.show();
            });
        });
    });
}

function login() {
    let username = $("#loginUsername").val();
    let password = $("#loginPassword").val();

    if (username == "" || password == "") {
        alert("Vui lòng nhập đủ thông tin bên trên");
        return;
    }

    $("#loginSubmit").prop('disabled', true);
    _loginLoading.show();
    _loginContent.hide();

    grecaptcha.ready(function () {
        grecaptcha.execute('6Le2hPsUAAAAALzwDvu8vWcd7ImuBl_l49f927gN', { action: 'submit' }).then(function (token) {
            // Call api dologin
            $.ajax({
                url: _API_STUDENT_LOGIN,
                type: 'POST',
                data: {
                    username,
                    password,
                    token,
                    student: true
                }
            }).done(function (result) {
                if (result.Success) {
                    alert("Đăng nhập thành công\nBấm OK để reload lại trang");
                    location.reload();
                }
                else {
                    alert(result.Message);
                }

                $("#loginSubmit").prop('disabled', false);
                _loginLoading.hide();
                _loginContent.show();
            });
        });
    });
}

function reg() {
    let username = $("#regUsername").val();
    let password = $("#regPassword").val();
    let repassword = $("#regRePassword").val();
    let fullname = $("#regName").val();
    let email = $("#regEmail").val();
    let phone = $("#regPhone").val();

    if (username == "" || password == "" || repassword == "" ||
        fullname == "" || email == "" || phone == "") {
        alert("Vui lòng điền đủ thông tin bên trên");
        return;
    }

    if (username.length < 9) {
        alert("Vui lòng nhập đúng MSSV hoặc CMND");
        return;
    }

    if (password != repassword) {
        alert("Mật khẩu và xác nhận lại mật khẩu không trùng khớp");
        return;
    }

    $('#regSubmit').prop('disabled', true);
    _loginLoading.show();
    _loginContent.hide();

    let form = document.getElementById('regForm');
    let formData = new FormData(form);

    grecaptcha.ready(function () {
        grecaptcha.execute('6Le2hPsUAAAAALzwDvu8vWcd7ImuBl_l49f927gN', { action: 'submit' }).then(function (token) {
            $.ajax({
                url: _API_STUDENT_REG + "?token=" + token,
                type: 'POST',
                contentType: false,
                processData: false,
                data: formData
            }).done(function (result) {
                $('#regSubmit').prop('disabled', false);
                _loginLoading.hide();
                _loginContent.show();

                if (result.success) {
                    alert("Đăng ký thành công, bạn có thể sử dụng tài khoản này để đăng nhập");
                    openLoginTab();
                    resetRegInput();
                }
                else {
                    alert(result.message);
                }
            });
        });
    });
}

function resetRegInput() {
    $("#regUsername").val("");
    $("#regPassword").val("");
    $("#regRePassword").val("");
    $("#regName").val("");
    $("#regEmail").val("");
    $("#regPhone").val("");
}

// https://www.tutorialspoint.com/how-to-capitalize-the-first-letter-of-each-word-in-a-string-using-javascript#:~:text=Courses-,How%20to%20capitalize%20the%20first%20letter%20of,in%20a%20string%20using%20JavaScript%3F&text=At%20first%2C%20you%20need%20to,()%20for%20the%20extracted%20character.
function autoBeautyFullname() {
    let _name = $("#regName");
    var words = _name.val();

    if (words == "") {
        return;
    }

    var separateWord = words.toLowerCase().split(' ');
    for (var i = 0; i < separateWord.length; i++) {
        separateWord[i] = separateWord[i].charAt(0).toUpperCase() +
            separateWord[i].substring(1);
    }

    _name.val(separateWord.join(' '));
    console.log("Debug beauty name");
}