$(function () {
    var featureDropDown = $("#featureDropDown");
    $("#featureTypeDropDown").change( function () {
        $.ajax({
            type: "POST",
            url: "/Role/GetFeaturesToAddInRole?roleId=" + $("#RoleId").val() +
            "&featureTypeId=" + $("#featureTypeDropDown").val(),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if(response.noDataFound != null || response.noDataFound !== undefined){
                    featureDropDown.children('option:not(:first)').remove();
                    return;
                }
                featureDropDown.children('option:not(:first)').remove();
                jQuery.each(response, function (index, item) {
                    var dropdownItem = new Option(item.FeatureName, item.FeatureId);
                    featureDropDown.append(dropdownItem);
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
});

$(document).ready(function() {
    var featureDropDown = $("#featureDropDown");
    $.ajax({
        type: "POST",
        url: "/Role/GetFeaturesToAddInRole?roleId=" + $("#RoleId").val() +
            "&featureTypeId=" + $("#featureTypeDropDown").val(),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if(response.noDataFound != null || response.noDataFound !== undefined){
                featureDropDown.children('option:not(:first)').remove();
                return;
            }
            featureDropDown.children('option:not(:first)').remove();
            jQuery.each(response, function (index, item) {
                var dropdownItem = new Option(item.FeatureName, item.FeatureId);
                featureDropDown.append(dropdownItem);
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