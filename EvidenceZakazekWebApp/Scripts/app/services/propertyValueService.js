var PropertyValueService = function () {

    var getPropertyValuesFormByCategory = function (categoryId, done, fail) {
        $.get({
            url: "/PropertyValues/GetPropertyValuesFormByCategory",
            data: { categoryId: categoryId },
            dataType: "html"
        })
        .done(done)
        .fail(fail);
    };

    return {
        getPropertyValuesFormByCategory: getPropertyValuesFormByCategory
    }
}();