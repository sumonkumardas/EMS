function decreaseTotalAmount(index) {
    var otherDeductions = document.getElementsByName('OtherDeductionAmounts');
    var totalAmount = document.getElementById(index);
    if(parseFloat(otherDeductions[index].value)){
        totalAmount.value = parseFloat(totalAmount.value) - parseFloat(otherDeductions[index].value); 
    }
}

$("#monthDropDown").change( function () {
    $.ajax({
        type: "POST",
        url: "/PayRoll/GetSalarySheet?year=" + $("#yearDropDown").val() + "&month=" + $("#monthDropDown").val() +
        "&branchMediumId=" + $("#branchMediumId").val(),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function() {
            $("#loadingSpinner").css("visibility", "visible");
        },
        success: function (response) {
            $("#loadingSpinner").css("visibility", "hidden");
            populateSalarySheetTable(response);
        },
        failure: function (response) {
            alert("Failure..! Could not fetch Data using Ajax.");
        },
        error: function (response) {
            alert("Error..! Could not fetch Data using Ajax.");
        }
    });
});

$("#yearDropDown").change( function () {
    $.ajax({
        type: "POST",
        url: "/PayRoll/GetSalarySheet?year=" + $("#yearDropDown").val() + "&month=" + $("#monthDropDown").val() +
            "&branchMediumId=" + $("#branchMediumId").val(),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function() {
            $("#loadingSpinner").css("visibility", "visible");
        },
        success: function (response) {
            $("#loadingSpinner").css("visibility", "hidden");
            populateSalarySheetTable(response);
        },
        failure: function (response) {
            alert("Failure..! Could not fetch Data using Ajax.");
        },
        error: function (response) {
            alert("Error..! Could not fetch Data using Ajax.");
        }
    });
});

function populateSalarySheetTable(salarySheet) {
    populateTableHead(salarySheet[0].SalaryComponents);
    populateTableBody(salarySheet); 
}

function populateTableHead(salaryComponents){
    var tableHead = "<tr style='background-color:#0c80a7 !important'>" +
        "<th style='vertical-align: bottom; color:white; font-size:14px; font-weight:600'>Employee Name</th>" +
        "<th style='vertical-align: bottom; color:white; font-size:14px; font-weight:600''>Employee Code</th>" +
        "<th style='vertical-align: bottom; color:white; font-size:14px; font-weight:600''>Designation</th>" +
        "<th style='vertical-align: bottom; color:white; font-size:14px; font-weight:600''>A/C No</th>";
    jQuery.each(salaryComponents,
        function (index, item) {
              tableHead += 
                  "<th style='vertical-align: bottom; color:white; font-size:14px; font-weight:600''>" +
                  item.ComponentName + 
                 "<input type='hidden' name='SalaryComponentNames' value='"+item.ComponentName+"' />" +
                  "</th>";
        });
    tableHead += "<th style='vertical-align: bottom; color:white; font-size:14px; font-weight:600''>Total Amount</th>" + "</tr>";
    $("#salarySheetTable").empty();
    $("#salarySheetTable").append(tableHead);
}

function populateTableBody(salarySheet) {
    var rows = "";
    var grandTotalAmount = 0.0;
    var totalTaxAmount = 0.0;
    jQuery.each(salarySheet,
        function (index, item) {
            var salaryComponents = "";
            grandTotalAmount += item.TotalAmount;
            jQuery.each(item.SalaryComponents,
                function (index2, item) {
                    if(item.ComponentName === "Tax Deduction"){
                        totalTaxAmount +=  item.Amount;
                    }
                    if(item.ComponentName === "Other Deduction"){
                        salaryComponents += "<td>"+
                            "<input name='OtherDeductionAmounts' onchange='decreaseTotalAmount("+index+")' value='"+item.Amount.toFixed(2)+"' type='number' step='any' required style='width:90px; text-align:right; background-color:#fff'/>"+
                            "<input name='SalaryComponentAmounts' value='"+item.Amount+"' type='hidden'/>"+
                            "</td>"
                    }
                    else {
                        salaryComponents += "<td>"+
                            item.Amount.toFixed(2)+
                            "<input name='SalaryComponentAmounts' value='"+item.Amount+"' type='hidden'/>"+
                            "</td>"
                    }
                    
                });
            row = "<tr>" +
                "<td>" +
                item.EmployeeName +
                "<input name='EmployeeIds' value='"+item.EmployeeId+"' type='hidden'/>"+
                "</td>" +
                "<td>" +
                item.EmployeeCode +
                "</td>" +
                "<td>" +
                item.Designation +
                "</td>" +
                "<td>" +
                item.BankAccountNo +
                "<input name='EmployeeBankAccounts' value='"+item.BankAccountNo+"' type='hidden'/>"+
                "</td>"
                +salaryComponents+
                "<td>" +
                "<input type='number' disabled style='width:90px; text-align:right; background-color:#fff' id='"+index+"' value='"+item.TotalAmount.toFixed(2)+"'/>"  +
                "<input type='hidden' name='TotalAmounts' value='"+item.TotalAmount.toFixed(2)+"'/>"  +
                "</td>" +
                "<tr>";
            rows += row;
        });
    rows += "<input type='hidden' value='"+grandTotalAmount+"' id='grandTotalAmount'/>" +
            "<input type='hidden' value='"+totalTaxAmount+"' name='TotalTaxDeduction' id='totalTaxAmount'/>";
    $("#salarySheetTable").append(rows);
}

function changeButtonStatus(response) {
    $("#btnSaveSalarySheet").attr('disabled', false);
    $("#accPostPrintStateAndExportButtonContainer").hide();
    $("#btnAccountPosting").attr('disabled', false);
    if(response.DisableSaveButton){
        $("#btnSaveSalarySheet").attr('disabled', true);
    }
    if(response.ShowAccPostPrintStateAndExportButton){
        $("#accPostPrintStateAndExportButtonContainer").show();
    }
    if(response.DisablePostAccount){
        $("#btnAccountPosting").attr('disabled', true); 
    }
}

$(document).ready(function() {
    var date = new Date();
    var month = date.getMonth() + 1;
    var year = date.getFullYear();
    if($("#yearDropDown").val() !== null && $("#yearDropDown").val() !== undefined){
        year = $("#yearDropDown").val();
    }
    if($("#monthDropDown").val() !== null && $("#monthDropDown").val() !== undefined){
        month = $("#monthDropDown").val();
    }
    $.ajax({
        type: "POST",
        url: "/PayRoll/GetSalarySheet?year=" + $("#yearDropDown").val() + "&month=" + $("#monthDropDown").val() +
            "&branchMediumId=" + $("#branchMediumId").val(),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function() {
            $("#loadingSpinner").css("visibility", "visible");
        },
        success: function (response) {
            $("#loadingSpinner").css("visibility", "hidden");
            populateSalarySheetTable(response);
        },
        failure: function (response) {
            alert("Failure..! Could not fetch Data using Ajax.");
        },
        error: function (response) {
            alert("Error..! Could not fetch Data using Ajax.");
        }
    });
    $.ajax({
        type: "POST",
        url: "/PayRoll/ChangeButtonStatus?year=" + year + "&month=" + month +
            "&branchMediumId=" + $("#branchMediumId").val(),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if(response != null && response !== 'undefined') {
                changeButtonStatus(response);
            }
        }
    });
    
});

$("#monthDropDown").change(function() {
    $.ajax({
        type: "POST",
        url: "/PayRoll/ChangeButtonStatus?year=" + $("#yearDropDown").val() + "&month=" + $("#monthDropDown").val() +
            "&branchMediumId=" + $("#branchMediumId").val(),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if(response != null && response !== 'undefined') {
                changeButtonStatus(response);
            }
        }
    });
});
$("#yearDropDown").change(function() {
    $.ajax({
        type: "POST",
        url: "/PayRoll/ChangeButtonStatus?year=" + $("#yearDropDown").val() + "&month=" + $("#monthDropDown").val() +
            "&branchMediumId=" + $("#branchMediumId").val(),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if(response != null && response !== 'undefined') {
                changeButtonStatus(response);
            }
        }
    });
});

$("#btnSaveSalarySheet").click(function() {
    $.ajax({
        type: "POST",
        url: "/PayRoll/ChangeButtonStatus?year=" + $("#yearDropDown").val() + "&month=" + $("#monthDropDown").val() +
            "&branchMediumId=" + $("#branchMediumId").val(),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if(response != null && response !== 'undefined') {
                changeButtonStatus(response);
            }
        }
    });
});

function postSalarySheetAccount() {
    $.ajax({
        type: "POST",
        url: "/PayRoll/PostSalarySheetAccount?year=" + $("#yearDropDown").val() + "&month=" + $("#monthDropDown").val() +
            "&branchMediumId=" + $("#branchMediumId").val(),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function() {
            $("#loadingSpinner").css("visibility", "visible");
        },
        success: function (response) {
            $("#loadingSpinner").css("visibility", "hidden");
            if(response.selectYear){
                alert("Select Year");
                return;
            }
            if(response.selectMonth){
                alert("Select Month");
                return;
            }
            if(response.AccountAlreadyPosted){
                alert("Account Already Posted");
                return;
            }
            if(response.AccountPostingSuccessful){
                alert("Account Posting Successful");
                window.location.href = "/Payroll/SaveSalarySheet?branchMediumId=" + $("#branchMediumId").val() + 
                    "&year=" + $("#yearDropDown").val() + "&month=" + $("#monthDropDown").val();
                return;
            }
            if(response.AccountPostingFailed){
                alert("Account Posting Failed");
                return;
            }
            if(response.ImportChartOfAccounts){
                alert("Import Chart Of Accounts.");
                return;
            }
        }
    });
}

function printBankStatement(){
    var year = 0;
    var month = 0;
    var bankAccount="";
    var branchMediumId =  $("#branchMediumId").val();
    month = $("#monthDropDown").val();
    year = $("#yearDropDown").val();
    bankAccount = $("#bankDropDown").val();
    if(bankAccount === null || bankAccount === undefined || bankAccount === ""){
        alert("Select Bank Account!");
        return;
    }
    if(year === 0 || year === ""){
        alert("Select Year!");
        return;
    }
    if(month === 0 || month === ""){
        alert("Select Month!");
        return;
    }
    window.location.href = "/PayRoll/PrintBankStatement?year=" + year + "&month=" + month + 
        "&branchMediumId=" + branchMediumId + "&bankAccountNumber=" + bankAccount;
}

function exportSalarySheet() {
    var year = 0;
    var month = 0;
    month = $("#monthDropDown").val();
    year = $("#yearDropDown").val();
    var branchMediumId =  $("#branchMediumId").val();
    if(year === 0 || year === ""){
        alert("Select Year!");
        return;
    }
    if(month === 0 || month === ""){
        alert("Select Month!");
        return;
    }
    window.location.href = "/PayRoll/ExportSalarySheet?year=" + year + "&month=" + month +
        "&branchMediumId=" + branchMediumId;
}