$(function () {
    $('#inputPanel').hide();
    $('#MonthLabelId').hide();
    $('#DayLabelId').hide();
});

var drillDownModule = (function (window, undefined) {
    //#region Variables
    var interval;
    var chart;
    //#end region Variables
    function validate() {
        var valid = true;

        if ($('#SelectedMeterId').val() <= 0) {
            $("#SelectedMeterId").addClass("input-validation-error");
            $("#meteridvalid").text("Meter Id required");
            valid = false;

        } else {
            $("#SelectedMeterId").addClass("valid");
            $("#meteridvalid").text("");
        }


        if ($('#ReportType').val() <= 0) {
            $("#ReportType").addClass("input-validation-error");
            $("#reporttypevalid").text("Report Type required");
            valid = false;

        } else {
            $("#ReportType").addClass("valid");
            $("#reporttypevalid").text("");
        }

        if ($('#Year').val() < 2016) {
            $("#Year").addClass("input-validation-error");
            $("#yearvalid").text("Year Required");
            valid = false;

        } else {
            $("#Year").addClass("valid");
            $("#yearvalid").text("");
        }

       

        $('#chartContainer').empty();
        $('#chart_div').empty();
        clearInterval(interval);

        return valid;
    }
    function setModel() {

        var model = {
            ReportType: $('#ReportType').val(),
            Month: $('#Month').val(),
            Year: $('#Year').val(),
            Day: $('#Day').val(),
            SelectedMeterId: $('#SelectedMeterId').val()
           
        }
        return model;
    }

    function changeMonth() {
        validate();
        var monthValue = $('#Month').val();
        if (monthValue == 4 || monthValue == 6 || monthValue == 9 || monthValue == 11) {
            $("#Day option[value='31']").hide();
        } else if (monthValue == 2) {
            $("#Day option[value='30']").hide();
            $("#Day option[value='31']").hide();

            if ($('#Year').val() > 0) {
                if (!leapYear($('#Year').val()))
                    $("#Day option[value='29']").hide();
                else {
                    $("#Day option[value='29']").show();
                }
            }
        } else {
            $("#Day option[value='29']").show();
            $("#Day option[value='30']").show();
            $("#Day option[value='31']").show();
        }
    }

    function changeReportType() { 
       // validate();
        if (interval != null)
            clearInterval(interval);
        var reportType = $('#ReportType').val();

        $('#inputPanel').show();

        if (reportType == 3) {
            $('#Month').show();
            $('#Day').show();
            $('#Year').show();
            $('#MonthLabelId').show();
            $('#DayLabelId').show();
        }
        if (reportType == 2) {
            $('#Month').show();
            $('#Day').hide();
            $('#Year').show();
            $('#MonthLabelId').show();
            $('#DayLabelId').hide();
        }
        if (reportType == 1) {
            $('#Day').hide();
            $('#MonthLabelId').hide();
            $('#DayLabelId').hide();
            $('#Year').show();
            $('#Month').hide();
        }
      
    }
    function showReport(e) {

        if (validate()) {
            if (interval != null)
                clearInterval(interval);

            var model = setModel();

            $.ajax({
                type: "POST",
                url: $("#callUsageReportURL").val(),
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ model: model }),
                dataType: "json",
                success:
                    function (data) {

                        if (data.IsSuccess) {
                           
                                $('#chartContainer').empty();
                                //console.log(data);
                                showChart(data);
                                
                        }
                        else {
                            alert("Please enter correct Meter Id !!")
                        }

                    },
                error: function (xhr, status, error) {
                    //alert(xhr.responseText);
                }
            });
        }

        return false;
    }

    function showChart(data) {
        var categories = data.Data.XaxisCategory;
       // console.log(data.Data.Series);
        $('#chartContainer').highcharts({
            chart: {
                type: 'column'
            },
            title: {
                text: data.Data.GraphTitle
            },
            subtitle: {
                text: data.Data.GraphSubTitle
            },
            xAxis: {
               categories: categories
            },
            yAxis: {
                title: {
                    text: 'Usage in Kwh'
                }

            },
            credits: {
                text: 'Aplombtech BD',
                href: 'http://www.aplombtechbd.com'
            },
            legend: {
                enabled: true
            },
           
            plotOptions: {
                series: {
                    dataLabels: {
                                    enabled: true,
                                    format: '{point.y:.2f}'
                                },
                    cursor: 'pointer',
                    point: {
                        events: {
                            click: function () {
                                if ($('#ReportType').val() == 2) {

                                   
                                    $("#Day").val(this.category);

                                    var meterval = $('#SelectedMeterId').val();
                                    $('#ReportType').val('3').change();
                                    $('#SelectedMeterId').val(meterval);
                                }

                                if ($('#ReportType').val() == 1) {

                                    var months = ["January","February","March","April","May","June","July","August","September","October","November","December"];

                                    $('#Month').val(months.indexOf(this.category)+1);
                 
                                    var meterval = $('#SelectedMeterId').val();
                                    $('#ReportType').val('2').change();
                                    $('#SelectedMeterId').val(meterval);
                                }

                              
                                validate();
                                var model = setModel();
                                $.ajax({
                                    type: "POST",
                                    url: $("#callUsageReportURL").val(),
                                    contentType: "application/json; charset=utf-8",
                                    data: JSON.stringify({ model: model }),
                                    dataType: "json",
                                    success:
                                        function (data) {
                                            if (data.IsSuccess) {
                                                showChart(data);
                                            }
                                        },
                                    error: function () { }
                                });
                            }
                        }
                    }
                }
            },

            tooltip: {
                headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                pointFormat: '<span style="color:{point.color}">{point.name}</span><br>Kwh : <b>{point.y:.2f}</b> Used<br/>'
            },

            series:[{
                name: 'Usage',
                colorByPoint: true,
                data: data.Data.Series
            }]
 

        });


    }

    return {
        ShowReport: showReport,
        ChangeMonth: changeMonth,
        ChangeReportType: changeReportType
    };

})(window);