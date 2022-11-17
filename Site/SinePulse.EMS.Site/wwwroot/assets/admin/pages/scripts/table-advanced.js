function configTable(tableID) {
    var table = $(tableID);
    $(tableID + ' thead.filterCriteria th').each(function (i) {
        var title = $(tableID+ ' thead th').eq($(this).index()).text().trim();

        if (title == "Srl." || title == "Action") {
            $(this).html('<p>' + title + '</p><input class="form-control filter-input hide-but-take-space" type="text"   />');
        }
        else if (title == "Status") {
            var statusHtml = '<p>' + title + '</p><select class="form-control filter-input data-index="' + i + '">';
            statusHtml += ' <option value="">All</option>';
            statusHtml += ' <option value="Active">Active</option>';
            statusHtml += ' <option value="Inactive">Inactive</option>';
            statusHtml += '</select>';
            $(this).html(statusHtml);
        }
        else if (title == "Final Result Type ?") {
            var FinalResultTypehtml = '<p>' + title + '</p><select class="form-control filter-input data-index="' + i + '">';
            FinalResultTypehtml += ' <option value="">All</option>';
            FinalResultTypehtml += ' <option value="Percentage">Percentage</option>';
            FinalResultTypehtml += ' <option value="Average">Average</option>';
            FinalResultTypehtml += '</select>';
            $(this).html(FinalResultTypehtml);
        }
        else if (title == "Is Current") {
            var IsCurrenthtml = '<p>' + title + '</p><select class="form-control filter-input data-index="' + i + '">';
            IsCurrenthtml += ' <option value="">All</option>';
            IsCurrenthtml += ' <option value="Yes">Yes</option>';
            IsCurrenthtml += ' <option value="No">No</option>';
            IsCurrenthtml += '</select>';
            $(this).html(IsCurrenthtml);
        }
        else if (title == "Medium Name") {
            var MediumNamehtml = '<p>' + title + '</p><select class="form-control filter-input data-index="' + i + '">';
            MediumNamehtml += ' <option value="">All</option>';
            MediumNamehtml += ' <option value="English">English</option>';
            MediumNamehtml += ' <option value="Bangla">Bangla</option>';
            MediumNamehtml += '</select>';
            $(this).html(MediumNamehtml);
        }
        else if (title == "Result Type") {
            var ResultTypehtml = '<p>' + title + '</p><select class="form-control filter-input data-index="' + i + '">';
            ResultTypehtml += ' <option value="">All</option>';
            ResultTypehtml += ' <option value="Percentage">Percentage</option>';
            ResultTypehtml += ' <option value="Average">Average</option>';
            ResultTypehtml += '</select>';
            $(this).html(ResultTypehtml);
        }
        else if (title == "Subject Type") {
            var SubjectType = '<p>' + title + '</p><select class="form-control filter-input data-index="' + i + '">';
            SubjectType += ' <option value="">All</option>';
            SubjectType += ' <option value="Compulsory">Compulsory</option>';
            SubjectType += ' <option value="Optional">Optional</option>';
            SubjectType += '</select>';
            $(this).html(SubjectType);
        }
        else if (title == "Start Time") {
            $(this).html('<p>' + title + '</p><input class="form-control filter-input" type="time" placeholder="Search By ' + title + '" data-index="' + i + '" />');
        }
        else if (title == "End Time") {
            $(this).html('<p>' + title + '</p><input class="form-control filter-input" type="time" placeholder="Search By ' + title + '" data-index="' + i + '" />');
        }
        else {
            $(this).html('<p>' + title + '</p><input class="form-control filter-input" type="text" placeholder="Search By ' + title + '" data-index="' + i + '" />');

        }

    });


    /* Fixed header extension: http://datatables.net/extensions/scroller/ */

    var oTable = table.DataTable({
        "dom": "<'row'<'col-md-6 col-sm-12'l><'col-md-6 col-sm-12'f>r>t<'row'<'col-md-5 col-sm-12'i><'col-md-7 col-sm-12'p>>", // datatable layout without  horizobtal scroll
        //"scrollY": "300",
        "deferRender": true,
        "lengthMenu": [
            [5, 15, 20, -1],
            [5, 15, 20, "All"] // change per page values here
        ],
        "pageLength": 10 // set the initial value            
    });

    oTable.columns().every(function () {
        var dataTabaleColumn = this;
        var searchTextBoxes = $(this.header()).find('input');
        var searchSelectBoxes = $(this.header()).find('select');

        searchTextBoxes.on('keyup change', function () {
            dataTabaleColumn.search(this.value).draw();
        });
        searchSelectBoxes.on('keyup change', function () {
            if (this.value == '') {
                dataTabaleColumn.search(this.value).draw();
            } else {
                dataTabaleColumn.search("^" + this.value + "$", true, false, true).draw();
            }
        });

        searchTextBoxes.on('click', function (e) {
            e.stopPropagation();
        });
        searchSelectBoxes.on('click', function (e) {
            e.stopPropagation();
        });

    });

    var tableWrapper = $(tableID + '_wrapper'); // datatable creates the table wrapper by adding with id {your_table_jd}_wrapper
    tableWrapper.find('.dataTables_length select').select2(); // initialize select2 dropdown
}

var TableAdvanced = function () {

    var initTable1 = function () {
        configTable('#sample_1');
    }
    var initTable2 = function () {
        configTable('#sample_2');
    }
    var initTable3 = function () {
        configTable('#sample_3');
    }
    var initTable4 = function () {
        configTable('#sample_4');
    }
    var initTable5 = function () {
        configTable('#sample_5');
    }
    var initTable6 = function () {
        configTable('#sample_6');
    }

    return {
        //main function to initiate the module
        init: function () {
            if (!jQuery().dataTable) {
                return;
            }
            initTable1();
            initTable2();
            initTable3();
            initTable4();
            initTable5();
            initTable6();
        }

    };

}();