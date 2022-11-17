function renderEmployeeAttendance() {
       $.ajax({
        type: "POST",
        url: "/Employee/GetEmployeeAttendance?employeeId=" + $("#employeeId").val(),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.employeeIdErrorMessage != null) {
                alert(response.employeeIdErrorMessage);
                return;
            }
            populateEmployeeAttendance(response);
        },
        failure: function (response) {
            alert("Failure..! Could not Fetch Data using Ajax.");
        },
        error: function (response) {
            alert("Error..! Could not Fetch Data using Ajax.");
        }
    });
}