/* Set focus on the first form field on a page. */
$(function() {
    $(window).load(function() {
        $(':input[type!=submit]:visible:enabled:not(.keywords):first').focus();
    });
})

function validateForm(formId) {
    var validator = new AgileFx.Validator();
    cleanClearOnClick(formId);
    return validator.validate(formId);
}

function clearMessages() {
    var mbox = new AgileFx.MessageBox();
    mbox.clearErrors();
    mbox.clearMessages();
}

function cleanJSONNull(variable) {
    if (variable != null) {
        return variable;
    } else {
        return "";
    }
}

function formatJSONDate(date) {
    if (date == null || date == '') return '';
    else return date.substr(5, 2) + '/' + date.substr(8, 2) + '/' + date.substr(0, 4);
}

/* clear on click */
function setupClearOnClick() {
    //bind to the textBoxes that need to be cleared on click
    $('.clearonclick').focus(function(e) {
        if (this.value == $(this).attr("clearonclick")) this.value = "";
    });
    $('.clearonclick').blur(function(e) {
        if (this.value == "") this.value = $(this).attr("clearonclick");
    });
}

function ClearOnClickFieldGetValue(field) {
    if ($(field).val() == $(field).attr('clearonclick')) {
        return "";
    } else {
        return $(field).val();
    }
}

function cleanClearOnClick(formId) {
    var elem = ".clearonclick";
    if (formId != null) {
        elem = "#" + formId + " " + elem;
    }
    $(elem).each(function(x) {
        var elem = $(this);
        var text = elem.attr("clearonclick");
        if ($.trim($(this).val()) == $.trim(text)) {
            $(this).val('');
        }
    });
}
/* end clear on click */

function showExplanationLink(id){
    var container = '.Timesheet_' + id;
    var view = '<a href="javascript:showExplanation(\''+ id +'\');">View Explanation</a>';
    var add = '<a href="javascript:showExplanation(\''+ id +'\');">Add Explanation</a>';

    if ($("#JustificationView_" + id).val().length > 0) {
        $(container + ' .explanation').html(view);
    } else {
        $(container + ' .explanation').html(add);
    }
}

function showExplanation(id) {
    $('#ExplanationDialog_' + id).dialog('open');
}

/* ---- Tool Tip --------------------- */

var TipCorners = {
   BL:'bottomLeft', BR:'bottomRight', BM:'bottomMiddle',
   TR:'topRight', TL:'topLeft', TM:'topMiddle',
   LM:'leftMiddle', LT:'leftTop', LB:'leftBottom',
   RM:'rightMiddle', RB:'rightBottom', TR:'rightTop'
};

function toolTipParams(content) {
    return {
        content: content,
        position: {
            corner: {
                target: TipCorners.LB
            }
        },
        style: {
            border: {
                width: 5,
                radius: 10
            },
            padding: 10,
            textAlign: 'center',
            tip: true, // Give it a speech bubble tip with automatic corner detection
            name: 'cream' // Style it according to the preset 'cream' style
        }
    }
}

/* ---- Tool Tip --------------------- */
