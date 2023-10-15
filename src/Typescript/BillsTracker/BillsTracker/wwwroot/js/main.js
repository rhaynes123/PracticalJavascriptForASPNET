/*
https://learn.microsoft.com/en-us/aspnet/core/tutorials/web-api-javascript?view=aspnetcore-7.0
*/
import { BillingService } from "./billingService.js";
const addButton = document.getElementById("addBtn");
addButton.addEventListener('click', (event) => {
    event.preventDefault();
    const billName = document.getElementById("bill");
    const billAmount = document.getElementById("amount");
    const billPaid = document.getElementById("paid");
    let request = {
        id: "",
        amount: Number(billAmount.value),
        name: billName.value,
        paid: Boolean(billPaid.value)
    };
    BillingService.postNewBill(request);
});
//# sourceMappingURL=main.js.map