/*
https://learn.microsoft.com/en-us/aspnet/core/tutorials/web-api-javascript?view=aspnetcore-7.0
https://www.youtube.com/watch?v=EpOPR03z4Vw
*/
import { BillingService } from "./billingService.js";

const addButton = document.getElementById("addBtn") as HTMLButtonElement;

addButton.addEventListener('click', (event) => {
    event.preventDefault();
    const billName = document.getElementById("bill") as HTMLInputElement;
    const billAmount = document.getElementById("amount") as HTMLInputElement;
    const billPaid = document.getElementById("paid") as HTMLInputElement;

    let request: Bill = {
        id:"",
        amount:  Number(billAmount.value),
        name: billName.value,
        paid: Boolean(billPaid.value)
    }
    BillingService.postNewBill(request);

});

