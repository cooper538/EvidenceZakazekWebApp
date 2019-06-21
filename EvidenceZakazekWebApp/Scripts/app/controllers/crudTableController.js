var CrudTableController = function (crudService, flashService) {

    var controllerName;
    var table;
    var targetRow;
    var itemId;
    var confirmDialog;

    var init = function (controller) {

        table = $(".crudTable");
        controllerName = controller.LowercaseFirstLetter();

        createDataTable();

        table.on('click', '.js-btn-edit', editOrDetailsItem);
        table.on('click', '.js-btn-detail', editOrDetailsItem);
        table.on('click', '.js-btn-delete', deleteItem);
    };

    var editOrDetailsItem = function (e) {
        targetRow = $(e.target).closest("td");
        itemId = targetRow.attr("data-itemId");

        var action = ($(e.target).closest("button").hasClass("js-btn-edit")) ? "/Edit/" : "/Detail/";

        window.location.href = "/" + controllerName + action + itemId;
    }

    var deleteItem = function (e) {
        targetRow = $(e.target).closest("td");
        itemId = targetRow.attr("data-itemId");

        confirmDialog = createConfirmDialog(
            "Potvrzení",
            getConfirmMessage,
            deleteFromDb);
    };

    var getConfirmMessage = function () {
        switch (controllerName) {
            case "products":
                return "<p>Opravdu chceš odstanit tento produkt?</p>";
                break;
            case "productCategories":
                return '<p>Opravdu chceš odstanit tuto kategorii?</p>' + 
                       '<p class="text-danger"><b>(Produkty patřící kategorii budou odstraněny)</b></p>';
                break;
            default:
                return "Text pro tento kontroler nebyl implementován!!"
        }
    }

    var createConfirmDialog = function (title, message, yesCallback) {
        return bootbox.dialog({
            title: title,
            message: message,
            buttons: {
                yes: {
                    label: "Ano",
                    className: 'btn-danger',
                    callback: yesCallback
                },
                no: {
                    label: "Ne",
                    className: 'btn-default'
                }
            },
            callback: function () {
                bootbox.hideAll()
            }
        });
    };

    var deleteFromDb = function () {
        crudService.deleteFromDb(controllerName, itemId, doneDelete, fail);
    }

    var doneDelete = function () {
        table.api().row(targetRow).remove().draw();
        flashService.addFlashMsgSuccess("Produkt byl smazán.", 2000)
    };

    var fail = function (e) {
        alert("Něco se pokazilo!");
    }

    var createDataTable = function () {
        table = $(table).dataTable({
            searching: false,
            paging: false,
            ordering: false,
            info: false
        });
    };

    return {
        init: init
    }
}(CrudService, FlashService);