function showBillingInfo(){
    $("#billingInfo").load(
        "/Billing/LoadBillingInfoPartialView?branchMediumId=" + $('#showBranchMediumViewId').val(),
        function () {
            $("#billingYearDropDown").change(function() {
                $.ajax({
                    type: "POST",
                    url: "/Billing/LoadBillingInfoByYear?branchMediumId=" + $('#showBranchMediumViewId').val() +
                        "&year=" + $('#billingYearDropDown').val(),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        populateBillingInfoTable(response);
                    },
                    failure: function (response) {
                        alert("Failure..! Could not Fetch Data using Ajax.");
                    },
                    error: function (response) {
                        alert("Error..! Could not Fetch Data using Ajax.");
                    }
                });
            });
        });
}

function populateBillingInfoTable(response) {
    $("#billingInfoTable").empty();
    var rows = "";
    var row = "";

    jQuery.each(response.BillingInfos,
        function (index, item) {
            row = "<tr>" +
                "<td>" +
                item.Year+
                "</td>" +
                "<td>" +
                item.Month +
                "</td>" +
                "<td>" +
                item.PaymentDate +
                "</td>" +
                "<td>" +
                item.PerStudentCharge+
                "</td>" +
                "<td>" +
                item.TotalStudents+
                "</td>" +
                "<td>" +
                item.Amount+
                "</td>" +
                "<td>" +
                item.PaymentMethod +
                "</td>" +
                "<tr>";

            rows += row;
        });
    $("#billingInfoTable").append(rows);
}