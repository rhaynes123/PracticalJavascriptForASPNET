// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function PayBill(id) {

    let request = {
        id: id
    }

    fetch('/bills/paybill', {
        method: "PATCH",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(request),
    }).then((response) => {
        if (response.ok) {
            swal("Done!", "Bill Updated!", "success");
        }
    });
}