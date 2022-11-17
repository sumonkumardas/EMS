function attendanceRefreshClicked() {
    renderEmployeeAttendance();
}

function filterAttendanceClickedClicked() {
    $.ajax({
        type: "POST",
        url: "/Employee/GetEmployeeAttendanceByDateRange?employeeId=" + $("#employeeId").val() +
            "&startDate=" + $("#AttendanceStartDate").val() +
            "&endDate=" + $("#AttendanceEndDate").val(),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.enterDateRangeErrorMessage != null) {
                alert(response.enterDateRangeErrorMessage);
                return;
            }
            if (response.employeeIdErrorMessage != null) {
                alert(response.employeeIdErrorMessage);
                return;
            }
            if(response.invalidDateRangeErrorMessage != null)
            {
                alert(response.invalidDateRangeErrorMessage);
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

function populateEmployeeAttendance(attendanceList) {
    
    $("#employeeAttendanceTable").empty();
    var rows = "";
    var row = "";

    jQuery.each(attendanceList,
        function (index, item) {
            var inTime = "-";
            var outTime = "-";
            var editOption = "";
            var status = '<p style="color:red">A</p>';
            var date = new Date(item.AttendanceDate).toLocaleDateString('en-US', {
                day: 'numeric',
                month: 'long',
                year: 'numeric',
                weekday: 'short'
            }).split(' ').join(' ');

            if(item.AttendanceId != null && item.AttendanceId !== 0) {
                editOption = "<a href='/Employee/UpdateEmployeeAttendance?attendanceId=" + item.AttendanceId + "&employeeId="+item.EmployeeId+"'>Edit</a>";
            }
            
            if (item.InTime != null) {
                inTime = item.InTime;
                status = "P";
                
            }
            if (item.OutTime != null) {
                outTime = item.OutTime;
            }

            if (item.IsPublicHoliday) {
                row = "<tr>" +
                    "<td>" +
                     date +
                    "</td>" +
                    "<td colspan='4'>" +
                    item.HolidayName +
                    "</td>" +
                    "<tr>";
            }
            else {
                row = "<tr>" +
                    "<td>" +
                     date +
                    "</td>" +
                    "<td>" +
                    inTime +
                    "</td>" +
                    "<td>" +
                    outTime +
                    "</td>" +
                    "<td>" +
                    status +
                    "</td>" +
                    "<td>" +
                    editOption  +
                    "</td>" +
                    "<tr>";
            }

            rows += row;
        });
    $("#employeeAttendanceTable").append(rows);
}

function filterLeaveClickedClicked(){
    $.ajax({
        type: "POST",
        url: "/Employee/SearchEmployeeLeaves?employeeId=" + $("#employeeId").val() +
            "&startDate=" + $("#LeaveFilterStartDate").val() +
            "&endDate=" + $("#LeaveFilterEndDate").val(),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.enterDateRangeErrorMessage != null) {
                alert(response.enterDateRangeErrorMessage);
                return;
            }
            if (response.employeeIdErrorMessage != null) {
                alert(response.employeeIdErrorMessage);
                return;
            }
            if(response.invalidDateRangeErrorMessage != null)
            {
                alert(response.invalidDateRangeErrorMessage);
                return;
            }
            populateEmployeeLeaves(response);
        },
        failure: function (response) {
            alert("Failure..! Could not Fetch Data using Ajax.");
        },
        error: function (response) {
            alert("Error..! Could not Fetch Data using Ajax.");
        }
    });
}

$(document).ready(function () {
    renderEmployeeAttendance();
});
