$(function () {
    var featureDropDownRemove = $("#featureDropDownRemove");
    $("#featureTypeDropDownRemove").change( function () {
        $.ajax({
            type: "POST",
            url: "/Role/GetFeaturesOfRole?roleId=" + $("#RoleId").val() +
                "&featureTypeId=" + $("#featureTypeDropDownRemove").val(),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if(response.noDataFound != null || response.noDataFound !== undefined){
                    featureDropDownRemove.children('option:not(:first)').remove();
                    return;
                }
                featureDropDownRemove.children('option:not(:first)').remove();
                jQuery.each(response, function (index, item) {
                    var dropdownItem = new Option(item.FeatureName, item.FeatureId);
                    featureDropDownRemove.append(dropdownItem);
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
    var featureDropDownRemove = $("#featureDropDownRemove");
    $.ajax({
        type: "POST",
        url: "/Role/GetFeaturesOfRole?roleId=" + $("#RoleId").val() +
            "&featureTypeId=" + $("#featureTypeDropDownRemove").val(),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if(response.noDataFound != null || response.noDataFound !== undefined){
                featureDropDownRemove.children('option:not(:first)').remove();
                return;
            }
            featureDropDownRemove.children('option:not(:first)').remove();
            jQuery.each(response, function (index, item) {
                var dropdownItem = new Option(item.FeatureName, item.FeatureId);
                featureDropDownRemove.append(dropdownItem);
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