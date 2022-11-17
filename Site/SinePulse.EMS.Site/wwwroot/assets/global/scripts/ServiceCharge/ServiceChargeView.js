$(function () {
  var branchDropDown = $("#branchDropDown");
  $("#instituteDropDown").change( function () {
    $.ajax({
      type: "POST",
      url: "/ServiceCharge/GetBranches?instituteId=" + $("#instituteDropDown").val(),
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      success: function (response) {
        branchDropDown.children('option:not(:first)').remove();
        jQuery.each(response, function (index, item) {
          var dropdownItem = new Option(item.BranchName, item.Id);
          branchDropDown.append(dropdownItem);
        });
      },
      failure: function (response) {
        alert("Failure..! Could not Fetch Branch Data using Ajax.");
      },
      error: function (response) {
        alert("Error..! Could not Fetch Branch Data using Ajax.");
      }
    });
  });
});


  var mediumDropDown = $("#mediumDropDown");
$("#branchDropDown").change( function () {
    $.ajax({
      type: "POST",
      url: "/ServiceCharge/GetMediums?branchId=" + $("#branchDropDown").val(),
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      success: function (response) {
        mediumDropDown.children('option:not(:first)').remove();
        jQuery.each(response, function (index, item) {
          var dropdownItem = new Option(item.MediumName, item.Id);
          mediumDropDown.append(dropdownItem);
        });
      },
      failure: function (response) {
        alert("Failure..! Could not Fetch Medium Data using Ajax.");
      },
      error: function (response) {
        alert("Error..! Could not Fetch Medium Data using Ajax.");
      }
    });
  });


$(function () {
  $("#mediumDropDown").change(function () {
    $("#showServiceCharge").empty();
    $("#showServiceCharge").load(
      "/ServiceCharge/ShowServiceCharge?branchMediumId=" + $("#mediumDropDown").val(),
      function () {

      });
  });
});

