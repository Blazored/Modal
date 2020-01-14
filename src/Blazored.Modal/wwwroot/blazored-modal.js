var blazoredModal = (function () {

    var elementReference = null;

    return {
        registerEscListener: function (element) {
            elementReference = element;
            document.addEventListener('keydown', blazoredModal.onKeyDownEvent);
        },

        unRegisterEscListener: function () {
            document.removeEventListener('keydown', blazoredModal.onKeyDownEvent);
            elementReference = null;
        },

        onKeyDownEvent: function (args) {
            if (elementReference != null && args.key == "Escape") {
                elementReference.invokeMethodAsync('JsInvokeCloseModal');
            }
        }
    };
})();  
