function renderEmployeePersonalInfo() {
    $("#employeePersonalInfo").load(
        "/Employee/LoadEmployeePersonalInfoPartialView?employeeId=" + $('#employeeId').val(),
        function () {

        });


}