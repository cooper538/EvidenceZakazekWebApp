var ProductCategoryService = function () {

    var getPropertyValuesForm = function (categoryId, done, fail) {
        $.get({
            url: '/ProductCategories/GetPropertyValuesForm',
            data: { categoryId: categoryId },
            dataType: 'html',
        })
        .done(done)
        .fail(fail);
    };

    return {
        getPropertyValuesForm: getPropertyValuesForm
    }
}();