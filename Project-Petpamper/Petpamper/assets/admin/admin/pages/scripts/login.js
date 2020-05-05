var Login = function () {

    var handleLogin = function () {

        var txtUsername = $('#txtUsername');
        var txtPassword = $('#txtPassword');
        var chkRememberMe = $('#chkRememberMe');
        var txtReturnUrl = $('#return_url');

        $('#btnSignIn').click(function (e) {
            e.preventDefault();

            var username = txtUsername.val();
            if ($.fn.isNullOrEmpty(username)) {
                notify.error('Tên tài khoản là bắt buộc.');
                txtUsername.focus();
                return false;
            }
            //if (!$.fn.isValidUsername(username)) {
            //    notify.error(l.get('InvalidUsernameRegex'));
            //    txtUsername.focus();
            //    return false;
            //}

            var password = txtPassword.val();
            if ($.fn.isNullOrEmpty(password)) {
                notify.error('Mật khẩu là bắt buộc.');
                txtPassword.focus();
                return false;
            }

            if (password.length < 6) {
                notify.error('Mật khẩu phải dài hơn 6 kí tự.');
                txtPassword.focus();
                return false;
            }

            //if (!$.fn.isValidPassword(password)) {
            //    notify.error(l.get('InvalidPasswordRegex'));
            //    txtPassword.focus();
            //    return false;
            //}

            _.post('Admin/User/SignIn', {
                username: username,
                password: password,
                remember_me: chkRememberMe.prop('checked'),
                redirect_uri: txtReturnUrl.val()
            })
                .done(function (res) {
                    if (!res.Status) {
                        notify.error(res.ErrorMessage);
                    }
                    else {
                        notify.ok('Bạn đã đăng nhập thành công.');
                        setTimeout(function () {
                            window.location.href = window.location.origin + (txtReturnUrl.val() === '/' ? '/Admin' : txtReturnUrl.val());
                        }, 1000);
                    }
                });
        });

        $('.login-form input').keypress(function (e) {
            if (e.which === 13) {
                $('#btnSignIn').trigger('click');
            }
        });
    };

    var handleForgetPassword = function () {
        //$('.forget-form').validate({
        //    errorElement: 'span', //default input error message container
        //    errorClass: 'help-block', // default input error message class
        //    focusInvalid: false, // do not focus the last invalid input
        //    ignore: "",
        //    rules: {
        //        email: {
        //            required: true,
        //            email: true
        //        }
        //    },

        //    messages: {
        //        email: {
        //            required: "Email is required."
        //        }
        //    },

        //    invalidHandler: function(event, validator) { //display error alert on form submit   

        //    },

        //    highlight: function(element) { // hightlight error inputs
        //        $(element)
        //            .closest('.form-group').addClass('has-error'); // set error class to the control group
        //    },

        //    success: function(label) {
        //        label.closest('.form-group').removeClass('has-error');
        //        label.remove();
        //    },

        //    errorPlacement: function(error, element) {
        //        error.insertAfter(element.closest('.input-icon'));
        //    },

        //    submitHandler: function(form) {
        //        form.submit();
        //    }
        //});

        //$('.forget-form input').keypress(function(e) {
        //    if (e.which == 13) {
        //        if ($('.forget-form').validate().form()) {
        //            $('.forget-form').submit();
        //        }
        //        return false;
        //    }
        //});

        jQuery('#forget-password').click(function () {
            jQuery('.login-form').hide();
            jQuery('.forget-form').show();
        });

        jQuery('#back-btn').click(function () {
            jQuery('.login-form').show();
            jQuery('.forget-form').hide();
        });

    };

    return {
        //main function to initiate the module
        init: function () {

            handleLogin();
            handleForgetPassword();

        }
    };
}();