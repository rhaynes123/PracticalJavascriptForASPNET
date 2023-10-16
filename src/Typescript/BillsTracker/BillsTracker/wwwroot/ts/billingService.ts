export class BillingService {

    static postNewBill(bill: Bill): any {
        fetch('/bills', {
            method: "POST",
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(bill),
        }).then((response) => {
            if (response.ok) {
                alert("Bill Added")
            }
            return response.json();
        })
    }
    static patchBill(request: BillUpdate): any {
        fetch('/bills/paybill', {
            method: "PATCH",
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(request),
        }).then((response) => {
            if (response.ok) {
                alert("Bill Updated!");
            }
        });
    }

}
