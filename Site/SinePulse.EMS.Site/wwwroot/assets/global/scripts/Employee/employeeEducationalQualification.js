function renderEducationalQualification() {
    $("#educationalQualification").load(
        "/Employee/LoadEmployeeEducationalQualificationPartialView?employeeId=" + $('#employeeId').val(),
        function () {

        });
}                                                                   