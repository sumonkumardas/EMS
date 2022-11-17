$("#unpaidYearDropDown").click(function () {
    $.ajax({
        type: "POST",
        url: "/OnlinePayment/GetUnpaidMonths?branchMediumId=" + $("#branchMediumId").val() +
        "&year=" + $("#unpaidYearDropDown").val(),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var unpaidMonthDropDown = $("#unpaidMonthDropDown");
            unpaidMonthDropDown.children('option:not(:first)').remove();
            jQuery.each(response,
                function(index, item) {
                    var dropdownItem = new Option(item.MonthName, item.MonthEnum);
                    unpaidMonthDropDown.append(dropdownItem);
                });
        },
        failure: function (response) {
            alert("Failure..! Could not Fetch Data using Ajax.");
        },
        error: function (response) {
            alert("Error..! Could not Fetch Data using Ajax.");
        }
    });
});

$(document).ready(function () {
    $("#unpaidYearDropDown").prop('selectedIndex',"");
    // $('#name2').prop('selectedIndex',0);
});

// $("#unpaidMonthDropDown").click(function () {
//     $.ajax({
//         type: "POST",
//         url: "/OnlinePayment/GetDueAmount?branchMediumId=" + $("#branchMediumId").val() +
//             "&year=" + $("#unpaidYearDropDown").val() +
//             "&month=" + $("#unpaidMonthDropDown").val(),
//         contentType: "application/json; charset=utf-8",
//         dataType: "json",
//         success: function (response) {
//             if(response != null && response !== undefined){
//                 $("#dueAmount").val(response)
//             }
//         },
//         failure: function (response) {
//             alert("Failure..! Could not Fetch Data using Ajax.");
//         },
//         error: function (response) {
//             alert("Error..! Could not Fetch Data using Ajax.");
//         }
//     });
// });
