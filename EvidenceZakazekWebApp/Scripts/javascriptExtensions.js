// https://stackoverflow.com/a/9354310/2756329
Object.defineProperty(String.prototype, "LowercaseFirstLetter", {
    value: function LowercaseFirstLetter() {
        return this.charAt(0).toLocaleLowerCase() + this.slice(1);
    }
});