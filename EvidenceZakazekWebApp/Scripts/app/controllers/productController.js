﻿var ProductController = function (propertyValueService) {

    var init = function () {
        $(".productCategory").change(showPropertiesByCategory);
    };

    showPropertiesByCategory = function (e) {
        var categorySelect = e.target;
        var categoryId = categorySelect.value;
        propertyValueService.getPropertyValuesFormByCategory(categoryId, done, fail);
    };

    var done = function (data) {
        $(".propertyValues").html(data);
        validateDynamicContentIn(".products");
    }

    var fail = function () {
        alert("Property forms was not loaded!");
    }

    function validateDynamicContentIn(formSelector) {
        var form = $(formSelector);
        form.removeData("validator");
        form.removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse(form);
        form.validate(); // This line is important and added for client side validation to trigger, without this it didn't fire client side errors.
    }

    return {
        init: init
    }
}(PropertyValueService);