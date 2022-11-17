$(document).ready(function () {
    var date = new Date();
    var year = date.getFullYear();
    var eventList = [];
    $.ajax({        type: "POST",        url: "/EmployeeLeave/GetAllEmployeeHolidayOfYear",        dataType: "JSON",        contentType: "application/json;charset=utf-8",        data: JSON.stringify({ "year": year}),        success: function (data) {            if (data.length > 0) {                eventList = data;            }            $('#employeeLeaveCalendar').fullCalendar({
                defaultDate: date,
                editable: false,
                eventLimit: false, // allow "more" link when too many events
                events: eventList
            });        },        error: function (a, b, c) {                   }    });
    

});