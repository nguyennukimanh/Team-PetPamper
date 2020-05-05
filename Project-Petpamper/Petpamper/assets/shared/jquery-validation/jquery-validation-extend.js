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

        $.validator.addMethod("pwd_regex", function (value, element) {
            return this.optional(element) || /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{4,8}$/gm.test(value);
        }, "Password is invalid.");

        return $;
    }
));