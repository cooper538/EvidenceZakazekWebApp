; (function (window) {

    // Common functions

    // hint http://callmenick.com/post/javascript-objects-building-javascript-component-part-2
    function fnExtend(a, b) {
        for (var key in b) {
            if (b.hasOwnProperty(key)) {
                a[key] = b[key];
            }
        }
        return a;
    }

    function fnCapitalizeFirstLetter(string) {
        return string.charAt(0).toUpperCase() + string.slice(1).toLowerCase();
    }

    // Factory functions
    function CreateConfirmBootboxDialog(title, message, yesFunction, noFunction) {                   
        return bootbox.dialog({
            title: title,
            message: message,
            buttons: {
                yes: {
                    label: "Ano",
                    className: 'btn-danger',
                    callback: function () {
                        yesFunction();
                    }
                },
                no: {
                    label: "Ne",
                    className: 'btn-default',
                    callback: function () {
                        noFunction();
                        bootbox.hideAll();
                    }
                }
            }
        });
    };

    // Register functions to global namespace

    window.fnExtend = fnExtend;
    window.fnCapitalizeFirstLetter = fnCapitalizeFirstLetter;
    window.CreateConfirmBootboxDialog = CreateConfirmBootboxDialog;

})( window );