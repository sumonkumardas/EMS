

function SuccessMessage() {
    alert("Succcess Post");
}
function FailMessage() {
    alert("Fail Post");
}

function GetFormattedDate() {
    var todayTime = new Date();
    var month = format(todayTime.getMonth() + 1);
    var day = format(todayTime.getDate());
    var year = format(todayTime.getFullYear());
    return month + "/" + day + "/" + year;
}

$(document).ready(function () {
    $('#Start_date').datepicker({
       dateFormat: "dd/MM/yy",
        changeMonth: true,
        changeYear: true

    });

    $('#To_date').datepicker({
        dateFormat: "dd/MM/yy",
        //defaultValue : new Date(),
        changeMonth: true,
        changeYear: true


    });

    var d = new Date();
    var monthNames = ["January", "February", "March", "April", "May", "June","July", "August", "September", "October", "November", "December"];

    $('#payment').val('5').change();
    $('#Start_date').val(d.getDate() + "/" + monthNames[d.getMonth()] + "/" + d.getFullYear()).change();
    $('#To_date').val(d.getDate() + "/" + monthNames[d.getMonth()] + "/" + d.getFullYear()).change();

});

$(document).ready(function () {


});

