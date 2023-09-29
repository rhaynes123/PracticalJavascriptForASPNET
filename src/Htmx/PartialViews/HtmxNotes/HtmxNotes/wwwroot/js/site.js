// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.body.addEventListener("htmx:confirm", (event) => {

    if (event.detail.path === '/Notes?handler=Clear') {
        event.preventDefault();
        swal({
            title: "",
            text: "Are you sure you want to delete all?",
            icon: "warning",
            button: "Proceed"
        }).then((confirmed) => {
            if (confirmed) {
                event.detail.issueRequest();
            }
        })
    }

});

document.body.addEventListener("htmx:beforeSwap", (event) => {
    // should swap will be true on any 200 range response
    if (!event.detail.shouldSwap) {
        swal({
            title: "Error?",
            text: "An error occurred in submitting the request!",
            icon: "warning",
            dangerMode: true,
        })
    }
    else {
        swal("Done!","Request was successful","success");
    }
});