﻿$(document).ready(function () {
    var date = new Date();
    var year = date.getFullYear();
    var eventList = [];
    $.ajax({
                defaultDate: date,
                editable: false,
                eventLimit: false, // allow "more" link when too many events
                events: eventList
            });
    

});