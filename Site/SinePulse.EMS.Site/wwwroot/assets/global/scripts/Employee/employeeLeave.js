function renderEmployeeLeaves() {
    $.ajax({
        type: "POST",
        url: "/Employee/LoadEmployeeLeaves?employeeId=" + $("#employeeId").val(),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.employeeIdErrorMessage != null) {
                alert(response.employeeIdErrorMessage);
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

function populateEmployeeLeaves(employeeLeaves) {
    $("#employeeLeaveTable").empty();
    var rows = "";
    jQuery.each(employeeLeaves,
        function (index, item) {
            var leaveStatus = getLeaveStatus(item.LeaveStatus);
            var startDate = new Date(item.StartDate).toLocaleDateString('en-US', {
                day: 'numeric',
                month: 'long',
                year: 'numeric'
            }).split(' ').join(' ');
            var endDate = new Date(item.EndDate).toLocaleDateString('en-US', {
                day: 'numeric',
                month: 'long',
                year: 'numeric'
            }).split(' ').join(' ');
            rows +=
                "<tr>" +
                "<td>" +
                "<a href='/LeaveType/ViewLeaveType?leaveTypeId=" + item.LeaveType.Id + "'>" + item.LeaveType.LeaveName + "</a>" +
                "</td>" +
                "<td>" +
                item.Reason +
                "</td>" +
                "<td>" +
                startDate +
                "</td>" +
                "<td>" +
                endDate +
                "</td>" +
                "<td>" +
                leaveStatus +
                "</td>" +
                "<tr>";
        });
    $("#employeeLeaveTable").append(rows);
}

function getLeaveStatus(leaveStatus) {
    if (leaveStatus === 1) {
        return "Pending";
    }
    if (leaveStatus === 2) {
        return "Approved";
    }
    if (leaveStatus === 3) {
        return "Cancelled";
    }
}

function renderPendingLeaves() {
    $.ajax({
        type: "POST",
        url: "/Employee/GetEmployeePendingLeaves?employeeId=" + $("#employeeId").val(),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.employeeIdErrorMessage != null) {
                alert(response.employeeIdErrorMessage);
                return;
            }
            populatePendingEmployeeLeaves(response);
        },
        failure: function (response) {
            alert("Failure..! Could not Fetch Data using Ajax.");
        },
        error: function (response) {
            alert("Error..! Could not Fetch Data using Ajax.");
        }
    });
}

function populatePendingEmployeeLeaves(employeeLeaves) {
    $("#employeePendingLeaveTable").empty();
    var rows = "";
    jQuery.each(employeeLeaves,
        function (index, item) {
            var startDate = new Date(item.StartDate).toLocaleDateString('en-US', {
                day: 'numeric',
                month: 'long',
                year: 'numeric'
            }).split(' ').join(' ');
            var endDate = new Date(item.EndDate).toLocaleDateString('en-US', {
                day: 'numeric',
                month: 'long',
                year: 'numeric'
            }).split(' ').join(' ');
            rows +=
                "<tr>" +
                "<td>" +
                "<a href='/Employee/ViewEmployee?employeeId=" + item.EmployeeId + "'>" + item.EmployeeName + "</a>" +
                "</td>" +
                "<td>" +
                item.LeaveTypeName +
                "</td>" +
                "<td>" +
                item.Reason +
                "</td>" +
                "<td>" +
                startDate +
                "</td>" +
                "<td>" +
                endDate +
                "</td>" +
                "<td>" +
                item.LeaveStatus +
                "</td>" +
                "<td>" +
                "<button type='button' id='" + item.PendingLeaveId + "' onclick='approveLeave(this)'>Approve</button>&nbsp; | &nbsp;" +
                "<button type='button' id='" + item.PendingLeaveId + "' onclick='cancelLeave(this)'>Cancel</button>" +
                "</td>" +
                "<tr>";
        });
    $("#employeePendingLeaveTable").append(rows);
}

function approveLeave(approveParameter) {
    $.ajax({
        type: "POST",
        url: "/EmployeeLeave/ApproveLeaveDone?employeeLeaveId=" + approveParameter.id,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.successMessage != null) {
                alert(response.successMessage);
            }
            if (response.invalidLeveId != null) {
                alert(response.invalidLeveId);
            }
            renderPendingLeaves();
        },
        failure: function (response) {
            alert("Ajax call failed!");
        },
        error: function (response) {
            alert("Error in Ajax call!");
        }
    });
}

function cancelLeave(cancelParameter) {
    $.ajax({
        type: "POST",
        url: "/EmployeeLeave/ApproveLeaveCancel?employeeLeaveId=" + cancelParameter.id,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.successMessage != null) {
                alert(response.successMessage);
            }
            if (response.invalidLeveId != null) {
                alert(response.invalidLeveId);
            }
            renderPendingLeaves();
        },
        failure: function (response) {
            alert("Ajax call failed!");
        },
        error: function (response) {
            alert("Error in Ajax call!");
        }
    });
}