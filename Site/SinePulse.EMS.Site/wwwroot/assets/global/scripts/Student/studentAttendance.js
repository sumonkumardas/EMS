function renderStudentAttendance() {
    $.ajax({
        type: "POST",
        url: "/Student/LoadStudentAttendance?studentId=" + $("#studentId").val(),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.studentIdErrorMessage != null) {
                alert(response.studentIdErrorMessage);
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

function populateStudentAttendance(attendanceList) {

    $("#studentAttendanceTable").empty();
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
                editOption = "<a href='/Student/UpdateStudentAttendance?attendanceId=" + item.AttendanceId + "&studentId="+item.StudentId+ "&branchMediumId="+item.BranchMediumId+"'>Edit</a>";
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
    $("#studentAttendanceTable").append(rows);
}