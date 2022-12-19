// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// https://stackoverflow.com/questions/37663674/using-fetch-api-to-access-json
// https://www.vitoshacademy.com/javascript-create-ul-and-li-elements-with-js-with-chained-function/
// https://stackoverflow.com/questions/3390396/how-can-i-check-for-undefined-in-javascript
// https://www.w3schools.com/jsref/met_element_setattribute.asp

let validateName = function (name) {
    let studentName = String(name);
    let numbersRegex = /^[0-9]+$/;
    return studentName === undefined || studentName.length < 4 || studentName.match(numbersRegex);
};

let getSelectedItems = function (selector) {
    let chosenItems = document.querySelectorAll(selector);
    let items = []
    for (let index = 0; index < chosenItems.length; index++) {
        const element = chosenItems[index];
        if (element.checked) {
            items.push(element.value);
        }
    }
    return items;
};

let buildErrorList = function (errors) {
    
    if (errors === undefined || errors === null || errors.length === 0) {
        return;
    }
    else {
        let errorsList = document.getElementById('errorsListId');
        let unorderedErrors = document.createElement('ul');
        errorsList.appendChild(unorderedErrors);

        errors.forEach(function (error) {
            let listItem = document.createElement('li');
            unorderedErrors.appendChild(listItem);
            listItem.innerHTML += error;

        });
    }

};

let register = function (url, student) {
    //document.getElementById('loadingSpinnerId').removeAttribute('hidden');

    fetch(url, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(student)
    }).then((response) => {
        //document.getElementById('loadingSpinnerId').setAttribute('hidden');
        if (response.status !== 200) {
            let json = response.json();
            return json;
        }
        else {
            document.getElementById('successAlertId').removeAttribute('hidden');
        }
    }).then((json) => {
        let errorMessage = json.error === undefined ? "Oops something went wrong" : json.error;
        let errorAlert = document.getElementById('errorAlertId');
        errorAlert.innerText = errorMessage;
        errorAlert.removeAttribute('hidden');
        buildErrorList(json.errors);
    } );
};

let validateStudent = function (student) {
    let valid = true;
    let now = moment().format("YYYY-MM-DD HH:mm");

    if (validateName(student.firstName)) {
        document.getElementById('firstNameErrors').removeAttribute('hidden');
        valid = false;
    }

    if (validateName(student.lastName)) {
        document.getElementById('lastNameErrors').removeAttribute('hidden');
        valid = false;
    }

    if (student.registrationDate < now) {
        document.getElementById('dateError').removeAttribute('hidden');
        valid = false;
    }

    if (student.courses.length < 3) {
        document.getElementById('coursesError').removeAttribute('hidden');
        valid = false;
    }

    return valid;
};

let registerStudent = function (url, student) {
    let studentValid = validateStudent(student);

    if (studentValid) {
        register(url, student);
    }
};

