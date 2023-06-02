function Validator(formSelector,btnclick) {
    var formElement = document.querySelector(formSelector);
    var formRules = {};

    function getParent(element, selector) {
        while (element.parentElement) {
            if (element.parentElement.matches(selector)) {
                return element.parentElement;
            }
            element = element.parentElement;
        }
    }

    var validatorRules = {
        required: function (value) {
            return value ? undefined : "Vui lòng nhập dữ liệu";
        },
        min: function (min) {
            return function (value) {
               
                return value.length >= min ? undefined : 'Vui lòng nhập ít nhất ' + min +' ký tự';
            }
        }
    }

    if (formElement) {

        var inputs = formElement.querySelectorAll('[name][rules]');
        

        for (var input of inputs) {
            var rules = input.getAttribute("rules").split("|");
            for (var rule of rules) {
                var ruleInfo;
                var isRuleHasValue = rule.includes(":");
                if (isRuleHasValue) {
                    ruleInfo = rule.split(":");
                    rule = ruleInfo[0];
                }
                var ruleFunc = validatorRules[rule];
                if (isRuleHasValue) {
                    ruleFunc = ruleFunc(ruleInfo[1]);
                }

                if (Array.isArray(formRules[input.name])) {
                    formRules[input.name].push(ruleFunc);
                }
                else {
                    formRules[input.name] = [ruleFunc];
                }
            }
            input.onblur = handleValidate;
            input.oninput = handleClearError;
        }

        function handleValidate(event) {
            var rules = formRules[event.target.name];
            var errorMsg ;
            rules.find(function (rule) {
                errorMsg = rule(event.target.value);
                return errorMsg;
            });
            if (errorMsg) {
                var formGroup = getParent(event.target, '.form-group');
                if (formGroup) {
                    var formMessge = formGroup.querySelector('.form-message');
                    if (formMessge) {
                        formMessge.innerText = errorMsg;
                    }
                }
            }
            return !errorMsg;
        }
        function handleClearError(event) {
            var formGroup = getParent(event.target, '.form-group');
            var formMessge = formGroup.querySelector('.form-message');
            if (formMessge) {
                formMessge.innerText = "";
            }
        }
    }
    formElement.onsubmit = function (event) {
        event.preventDefault();
        var inputs = formElement.querySelectorAll('[name][rules]');
        var isvalid = true;
        for (var input of inputs) {
            if (handleValidate({ target: input })) {
                isvalid = false;
            }
        }
        if (isvalid) {
            $(btnclick).submit();
        }
        else {
            return;
        }
    }
    //$(btnclick).click(function (event) {
    //    event.preventDefault();
    //    var inputs = formElement.querySelectorAll('[name][rules]');
    //    var isvalid = true;

    //    for (var input of inputs) {
    //        if (handleValidate({ target: input })) {
    //            isvalid = false;
    //        }
    //    }
    //    if (isvalid) {
    //        $(btnclick).click();
    //    }
    //});
}
