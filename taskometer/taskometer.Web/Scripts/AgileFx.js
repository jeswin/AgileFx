/*  ---------------------------------
AgileFX Library Functions
---------------------------------  */
//nofx_ prefixed functions are JS Framework independent

//Scroll to element (no JS Framework dependency)
function nofx_scrollToElement(elem) {
    var x = elem.offsetLeft;
    var y = elem.offsetTop > 20 ? elem.offsetTop - 20 : elem.offsetTop;

    //To keep IE happy!
    var strScroll = 'window.scrollTo(' + x + ',' + y + ')';
    window.setTimeout(strScroll, 1);

    //  This works in all browsers except IE6 (Huh!)
    //  window.scrollTo(x, y);
}

var AgileFx = Class.create(true);
AgileFx.namespace({
    MessageBox: Class.create({
        init: function() { },

        addMessage: function(message) {
            $('#messageList')
                .css('display', 'block')
                .append("<li>" + message + "</li>");
            nofx_scrollToElement($('#messageList')[0]);
        },

        addError: function(message) {
            var a = $('#errorList')
                .css('display', 'block')
                .append("<li>" + message + "</li>");
            nofx_scrollToElement($('#errorList')[0]);
        },

        clearErrors: function() {
            var a = $('#errorList')
                .css('display', 'none')
                .empty();
        },

        clearMessages: function() {
            var a = $('#messageList')
                .css('display', 'none')
                .empty();
        },

        clearAll: function() {
            var a = $('#errorList,#messageList')
                .css('display', 'none')
                .empty();
        }
    }),

    Validator: Class.create({
        init: function() { },

        validate: function(formId) {
            var hasErrors = false;
            var mbox = new AgileFx.MessageBox();
            mbox.clearErrors();

            var elem = ".validated";
            if (formId != null) {
                elem = "#" + formId + " " + elem;
            }
            $(elem).filter(function() { return !this.disabled; }).each(function(x) {
                var elem = $(this);
                var rules = $(elem.attr("validation").split(','));
                rules.each(function(y) {
                    var rule = this + '';
                    if (rule == 'required') {
                        if (null == elem.val() || '' == elem.val()) {
                            hasErrors = true;
                            mbox.addError("Required field '" + elem.attr("displayname") + "' is missing.");
                        }
                    } else if (rule == 'email') {
                        var regEmail = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
                        if (null != elem.val() && '' != elem.val() && !regEmail.test(elem.val())) {
                            hasErrors = true;
                            mbox.addError("'" + elem.attr("displayname") + "' is not valid.");
                        }
                    } else if (rule.match(/min\(/)) {
                        var minLength = rule.substr(4, rule.length - 5);
                        if (null != elem.val() && '' != elem.val() && elem.val().length < minLength) {
                            hasErrors = true;
                            mbox.addError("'" + elem.attr("displayname") + "' cannot be less than " + minLength + " characters.");
                        }
                    } else if (rule.match(/max\(/)) {
                        var maxLength = rule.substr(4, rule.length - 5);
                        if (null != elem.val() && '' != elem.val() && elem.val().length > maxLength) {
                            hasErrors = true;
                            mbox.addError("'" + elem.attr("displayname") + "' cannot be more than " + maxLength + " characters.");
                        }
                    } else if (rule.match(/range\(/)) {
                        var range = rule.substr(6, rule.length - 7).split(/\:/);
                        if (null != elem.val() && '' != elem.val() && (isNaN(elem.val()) || elem.val() < parseInt(range[0]) || elem.val() > parseInt(range[1]))) {
                            hasErrors = true;
                            mbox.addError("'" + elem.attr("displayname") + "' should be a number between " + range[0] + " and " + range[1] + ".");
                        }
                    }
                });
            });
            return !hasErrors;
        }
    })
});
