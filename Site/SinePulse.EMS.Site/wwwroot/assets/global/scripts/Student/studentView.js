$(document).ready(function () {
    renderStudentAttendance();
});

function searchStudentAttendanceClicked() {
    $.ajax({
        type: "POST",
        url: "/Student/GetStudentAttendanceByDateRange?studentId=" + $("#studentId").val() +
            "&startDate=" + $("#AttendanceStartDate").val() +
            "&endDate=" + $("#AttendanceEndDate").val(),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.enterDateRangeErrorMessage != null) {
                alert(response.enterDateRangeErrorMessage);
                return;
            }
            if (response.studentIdErrorMessage != null) {
                alert(response.employeeIdErrorMessage);
                return;
            }
            if(response.invalidDateRangeErrorMessage != null)
            {
                alert(response.invalidDateRangeErrorMessage);
                return;
            }
            populateStudentAttendance(response);
        },
        failure: function (response) {
            alert("Failure..! Could not Fetch Data using Ajax.");
        },
        error: function (response) {
            alert("Error..! Could not Fetch Data using Ajax.");
        }
    });
}