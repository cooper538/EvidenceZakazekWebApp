var CrudService = function () {

    deleteFromDb = function (controllerName, itemId, done, fail) {
        $.ajax({
            url: "/api/" + controllerName + "/" + itemId,
            method: "Delete"
        })
        .done(done)
        .fail(fail);
    }

    return {
        deleteFromDb: deleteFromDb
    }
}();