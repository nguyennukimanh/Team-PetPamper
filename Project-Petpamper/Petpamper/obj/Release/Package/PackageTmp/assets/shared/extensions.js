(function (factory) {
    'use strict';

    if (typeof define === "function" && define.amd) {
        define(["jquery"], factory);
    }

    else if (typeof module === "object" && module.exports) {
        module.exports = factory(require("jquery"));
    }

    else {
        factory(jQuery);
    }
}

    (function ($) {

        String.prototype.format = String.prototype.f = function () {
            var s = this,
                i = arguments.length;

            while (i--) {
                s = s.replace(new RegExp('\\{' + i + '\\}', 'gm'), arguments[i]);
            }
            return s;
        };

        String.prototype.isNullOrEmpty = function () {
            var value = this;
            return !(typeof value === "string" && value.length > 0);
        };

        $.extend($.fn, {
            api: {
                get: function (url) {
                    return this.send(url, 'get');
                },
                post: function (url, data) {
                    return this.send(url, 'post', data);
                },
                put: function (url, data) {
                    return this.send(url, 'put', data);
                },
                delete: function (url) {
                    return this.send(url, 'delete');
                },
                send: function (url, method, data) {
                    var baseUrl = window.location.origin;
                    var reqOpt = {
                        type: method,
                        url: [baseUrl, '/', url].join(''),
                        headers: this.headers,
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'json'
                    };

                    if (method.toLowerCase() === 'post' || method.toLowerCase() === 'put')
                        reqOpt['data'] = JSON.stringify(data);

                    var def = $.Deferred();
                    $.ajax(reqOpt)
                        .done(function (res) {
                            def.resolve(res);
                        }).fail(function (ex) {
                            switch (ex.status) {
                                case 400:
                                    def.resolve(JSON.parse(ex.responseText));
                                    break;
                                //Unauthorized
                                case 401:
                                    if (location.pathname.indexOf('/auth/login') >= 0) {
                                        def.resolve(JSON.parse(ex.responseText));
                                    }
                                    else alert('401 - logout');
                                    break;
                            }
                            //console.log(ex);
                        });

                    return def.promise();
                }
            },
            notify: {
                info: function (message) {
                    this.show(message, null, 'info', 'la la-info-circle');
                },
                ok: function (message) {
                    this.show(message, null, 'success', 'la la-check');
                },
                error: function (message) {
                    this.show(message, null, 'error', 'la la-times-circle');
                },
                warn: function (message) {
                    this.show(message, null, 'warning', 'la la-warning');
                },
                show: function (message, title, type, icon) {
                    toastr.options = {
                        closeButton: true,
                        positionClass: 'toast-top-right'
                    };

                    toastr[type](message, title);
                },
                confirm: function (callback) {
                    bootbox.confirm({
                        message: $.i18n.get('Bạn có chắc chắn muốn xóa dữ liệu này? Bạn không thể khôi phục sau khi xóa?'),
                        buttons: {
                            confirm: {
                                label: $.i18n.get('Có'),
                                className: 'btn-success'
                            },
                            cancel: {
                                label: $.i18n.get('Không'),
                                className: 'btn-danger'
                            }
                        },
                        callback: function (result) {
                            if (result)
                                callback();
                        }
                    });
                }
            },
            button: {
                on: function (button) {
                    button.addClass('kt-spinner kt-spinner--right kt-spinner--sm kt-spinner--light').attr('disabled', true);
                },
                off: function (button) {
                    button.removeClass('kt-spinner kt-spinner--right kt-spinner--sm kt-spinner--light').attr('disabled', false);
                }
            },
            isValidUsername: function (username) {
                return /^[a-zA-Z0-9_]{5,15}$/gm.test(username);
            },
            isValidPassword: function (password) {
                return /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,16}$/gm.test(password);
            },
            isValidEmail: function (email) {
                return String(email).search(/^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/) !== -1;
            },
            isNullOrEmpty: function (obj) {
                var returnValue = false;
                if (!obj
                    || obj === null
                    || obj === 'null'
                    || obj === ''
                    || obj === '{}'
                    || obj === 'undefined'
                    || obj.length === 0) {
                    returnValue = true;
                }
                return returnValue;
            },
            toLocalTime: function (utcTime, format) {
                if (format === undefined || format === null || format === '')
                    format = 'DD/MM/YYYY HH:mm:ss';
                return moment(moment.utc(utcTime).toDate()).format(format);
            },
            getSlug: function (text) {
                //Đổi chữ hoa thành chữ thường
                var slug = text.toLowerCase();

                //Đổi ký tự có dấu thành không dấu
                slug = slug.replace(/á|à|ả|ạ|ã|ă|ắ|ằ|ẳ|ẵ|ặ|â|ấ|ầ|ẩ|ẫ|ậ/gi, 'a');
                slug = slug.replace(/é|è|ẻ|ẽ|ẹ|ê|ế|ề|ể|ễ|ệ/gi, 'e');
                slug = slug.replace(/i|í|ì|ỉ|ĩ|ị/gi, 'i');
                slug = slug.replace(/ó|ò|ỏ|õ|ọ|ô|ố|ồ|ổ|ỗ|ộ|ơ|ớ|ờ|ở|ỡ|ợ/gi, 'o');
                slug = slug.replace(/ú|ù|ủ|ũ|ụ|ư|ứ|ừ|ử|ữ|ự/gi, 'u');
                slug = slug.replace(/ý|ỳ|ỷ|ỹ|ỵ/gi, 'y');
                slug = slug.replace(/đ/gi, 'd');
                //Xóa các ký tự đặt biệt
                slug = slug.replace(/\`|\~|\!|\@|\#|\||\$|\%|\^|\&|\*|\(|\)|\+|\=|\,|\.|\/|\?|\>|\<|\'|\"|\:|\;|_/gi, '');
                //Đổi khoảng trắng thành ký tự gạch ngang
                slug = slug.replace(/ /gi, "-");
                //Đổi nhiều ký tự gạch ngang liên tiếp thành 1 ký tự gạch ngang
                //Phòng trường hợp người nhập vào quá nhiều ký tự trắng
                slug = slug.replace(/\-\-\-\-\-/gi, '-');
                slug = slug.replace(/\-\-\-\-/gi, '-');
                slug = slug.replace(/\-\-\-/gi, '-');
                slug = slug.replace(/\-\-/gi, '-');
                //Xóa các ký tự gạch ngang ở đầu và cuối
                slug = '@' + slug + '@';
                slug = slug.replace(/\@\-|\-\@|\@/gi, '');

                return slug;
            }
        });

        return $;
    }
));