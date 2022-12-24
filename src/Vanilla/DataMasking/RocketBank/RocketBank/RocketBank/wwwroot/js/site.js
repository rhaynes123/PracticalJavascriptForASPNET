// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// https://www.javascripttutorial.net/javascript-dom/javascript-toggle-password-visibility/
// https://www.w3schools.com/howto/howto_js_toggle_password.asp

const showSocialBtn = document.getElementById('showSocial');
const toggleRoutingNumber = document.getElementById("toggleRoutingNumber");
const toggleAccountNumber = document.getElementById("toggleAccountingNumber");

showSocialBtn.addEventListener('click', function (e) {
    e.preventDefault();
    maskSsn();
});

toggleRoutingNumber.addEventListener('click', function (e) {
    e.preventDefault();
    maskRoutingAndAccountNumber('RoutingNumber', 'originalRoutingNumber');
});

toggleAccountNumber.addEventListener('click', function (e) {
    e.preventDefault();
    maskRoutingAndAccountNumber('AccountingNumber','originalAccountingNumber')
});

const maskSsn = function () {
    let social = document.getElementById('SocialSecurityNumber');
    if (social.type === "password") {
        social.type = "text";
    }
    else {
        social.type = "password";
    }

};

const maskRoutingAndAccountNumber = function (currentElement, originalElement) {
    let currentNumberElement = document.getElementById(currentElement);
    let originalNumberValue = document.getElementById(originalElement).value;
    if (currentNumberElement.value === originalNumberValue) {
        currentNumberElement.value = "*********";
    }
    else {
        currentNumberElement.value = originalNumberValue;
    }
};