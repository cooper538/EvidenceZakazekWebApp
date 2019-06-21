var FlashService = function ($) {

    var addFlashMsgInfo = function (msg, miliseconds) {
        buildFlashMsg("info", msg, miliseconds)
    };

    var addFlashMsgSuccess = function (msg, miliseconds) {
        buildFlashMsg("success", msg, miliseconds)
    };

    var addFlashMsgWarning = function (msg, miliseconds) {
        buildFlashMsg("warning", msg, miliseconds)
    };

    var addFlashMsgDanger = function (msg, miliseconds) {
        buildFlashMsg("danger", msg, miliseconds)
    };

    var buildFlashMsg = function (type, msg, miliseconds) {
        $('<div class="alert alert-'+ type +'">' + msg + '</div>').prependTo('.allertsContainer').delay(miliseconds).fadeOut("slow");;
    }

    return {
        addFlashMsgInfo: addFlashMsgInfo,
        addFlashMsgSuccess: addFlashMsgSuccess,
        addFlashMsgWarning: addFlashMsgWarning,
        addFlashMsgDanger: addFlashMsgDanger
    }
}(jQuery);