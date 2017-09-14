// http://blog.rebuildall.net/2011/03/02/jQuery_validate_and_the_comma_decimal_separator
// decimal separator override
// dot. => comma,

// overridide range validation method
$.validator.methods.range = function (value, element, param) {
    var globalizedValue = value.replace(",", ".");
    return this.optional(element) || (globalizedValue >= param[0] && globalizedValue <= param[1]);
}

// overridide number validation method
$.validator.methods.number = function (value, element) {
    return this.optional(element) || /^-?(?:\d+|\d{1,3}(?:[\s\.,]\d{3})+)(?:[\.,]\d+)?$/.test(value);
}