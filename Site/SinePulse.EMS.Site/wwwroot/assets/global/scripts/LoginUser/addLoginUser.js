function populateInstituteDropdown(Institutes) {
    var instituteDropDown = $("#instituteDropDown");
    instituteDropDown.children('option:not(:first)').remove();
    jQuery.each(Institutes,
        function (index, item) {
            var dropdownItem = new Option(item.OrganisationName, item.Id);
            instituteDropDown.append(dropdownItem);
        });
}

function populateBranchDropdown(Branches) {
    var branchDropDown = $("#branchDropDown");
    branchDropDown.children('option:not(:first)').remove();
    jQuery.each(Branches,
        function (index, item) {
            var dropdownItem = new Option(item.BranchName, item.Id);
            branchDropDown.append(dropdownItem);
        });
}

function populateBranchMediumDropdown(BranchMediums) {
    var branchMediumDropDown = $("#branchMediumDropDown");
    branchMediumDropDown.children('option:not(:first)').remove();
    jQuery.each(BranchMediums,
        function (index, item) {
            var dropdownItem = new Option(item.Medium.MediumName, item.Id);
            branchMediumDropDown.append(dropdownItem);
        });
}

function setInstituteDropDown() {
    $("#instituteDropDown").get(0).selectedIndex = 1;
}


function resetDropDowns() {
    $("#instituteDropDown").get(0).selectedIndex = 0;
    $("#branchDropDown").get(0).selectedIndex = 0;
    $("#branchMediumDropDown").get(0).selectedIndex = 0;
}

function hideAllRequiredSign() {
    $("#instituteRequired").hide();
    $("#branchRequired").hide();
    $("#branchMediumRequired").hide();
}

function showInstituteRequiredSign() {
    $("#instituteRequired").show();
}

function showBranchRequiredSign() {
    $("#branchRequired").show();
}

function showBranchMediumRequiredSign() {
    $("#branchMediumRequired").show();
}

$(document).ready(function() {
    $("#roleDropDown").get(0).selectedIndex = 0;
});

$(function () {
    $("#roleDropDown").change(function () {
        $.ajax({
            type: "POST",
            url: "/LoginUser/PopulateDropdownsOfLoginUser?roleId=" + $("#roleDropDown").val() +
                "&employeeId=" + $("#employeeId").val(),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                hideAllRequiredSign();
                enableAllDropDowns();
                if(response.NoData === true || response.InvalidParameter === true){
                    resetDropDowns();
                    hideAllRequiredSign();
                    return;
                }
                if (response.RoleType === "SuperAdmin") {
                    resetDropDowns();
                    disableInstituteDropdown();
                    disableBranchDropdown();
                    disableBranchMediumDropdown();
                }
                if (response.RoleType === "InstituteAdmin") {
                    resetDropDowns();
                    populateInstituteDropdown(response.Institutes);
                    setInstituteDropDown();
                    showInstituteRequiredSign();
                    disableBranchDropdown();
                    disableBranchMediumDropdown();
                }
                if (response.RoleType === "BranchAdmin") {
                    resetDropDowns();
                    populateInstituteDropdown(response.Institutes);
                    populateBranchDropdown(response.Branches);
                    setInstituteDropDown();
                    showBranchRequiredSign();
                    showInstituteRequiredSign();
                    disableBranchMediumDropdown();
                }
                if (response.RoleType === "BranchMediumAdmin") {
                    resetDropDowns();
                    showBranchMediumRequiredSign();
                    showBranchRequiredSign();
                    showInstituteRequiredSign();
                    populateInstituteDropdown(response.Institutes);
                    setInstituteDropDown();
                    populateBranchDropdown(response.Branches);
                    populateBranchMediumDropdown(response.BranchMediums);
                }
                if (response.RoleType === "BranchMediumUser") {
                    resetDropDowns();
                    showBranchMediumRequiredSign();
                    showBranchRequiredSign();
                    showInstituteRequiredSign();
                    populateInstituteDropdown(response.Institutes);
                    setInstituteDropDown();
                    populateBranchDropdown(response.Branches);
                    populateBranchMediumDropdown(response.BranchMediums);
                }
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

$(function () {
    $("#branchDropDown").change(function () {
        $.ajax({
            type: "POST",
            url: "/LoginUser/GetBranchMediumList?branchId=" + $("#branchDropDown").val()+
                "&employeeId=" + $("#employeeId").val(),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
               populateBranchMediumDropdown(response) 
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

function disableInstituteDropdown() {
    $('#instituteDropDown').attr('disabled', true);
}

function disableBranchDropdown() {
    $('#branchDropDown').attr('disabled', true);
}

function disableBranchMediumDropdown() {
    $('#branchMediumDropDown').attr('disabled', true);
}

function enableAllDropDowns() {
    $('#branchMediumDropDown').attr('disabled', false);
    $('#branchDropDown').attr('disabled', false);
    $('#instituteDropDown').attr('disabled', false);
}