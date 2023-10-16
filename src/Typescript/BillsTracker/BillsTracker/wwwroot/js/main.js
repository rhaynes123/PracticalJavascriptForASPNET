/*
https://learn.microsoft.com/en-us/aspnet/core/tutorials/web-api-javascript?view=aspnetcore-7.0
https://www.youtube.com/watch?v=EpOPR03z4Vw
*/
import { BillingService } from "./billingService.js";
const addButton = document.getElementById("addBtn");
if (addButton) {
    addButton.addEventListener('click', (event) => {
        event.preventDefault();
        const billName = document.getElementById("bill");
        const billAmount = document.getElementById("amount");
        const billPaid = document.getElementById("paid");
        let request = {
            id: "",
            amount: Number(billAmount.value),
            name: billName.value,
            paid: billPaid.checked
        };
        BillingService.postNewBill(request);
    });
}
const checkboxes = document.querySelectorAll('input[type=checkbox]');
for (let index = 0; index < checkboxes.length; index++) {
    let element = checkboxes[index];
    let itemId = element.getAttribute('data-itemId');
    if (itemId) {
        element.addEventListener("click", () => {
            let request = {
                id: itemId,
                paid: element.checked
            };
            BillingService.patchBill(request);
        });
    }
}
//# sourceMappingURL=main.js.map