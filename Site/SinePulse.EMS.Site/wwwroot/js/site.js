// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    showLevelOnType();
    $('.show-dropdown-on-hover').on('hover', function () {
        var target = $(this).data('toggle');
        $('.' + target).addClass('open');
    });

    $('.dropdown').on('mouseleave', function () {
        $('.dropdown').removeClass('open');
    });

    $('table tfoot').each(function () {
        $(this).insertAfter($(this).siblings('thead'));
    });

    $('tfoot').each(function () {
        $(this).insertAfter($(this).siblings('thead'));
    });

    var total_th = $('#table-header-5 th').length;
    var per_th = 100 / total_th;
    per_th = per_th.toFixed(2);
    $('#table-header-5 th').css({ "width": per_th + "%"});
    //$('#sample_5 th').css({ "width": per_th + "%", "text-align": "left" });

    $('.custom-table').each(function () {
        var custom_table_h = $(this).height();
        var line_height = custom_table_h - 20;
        $(this).closest('.custom-row').find('.title').css({ "height": custom_table_h + "px", "line-height": line_height + "px" });

    });
});
function showLevelOnType() {
    $(document).on('keypress', 'input', function () {
        // Does some stuff and logs the event to the console
       var placeholder = $(this).data('placeholder');
        console.log(placeholder);
    });
}

    function printDiv(divname)
    {

        //$("#printableDiv").clone().appendTo("#print-me");
        ////Apply some styles to hide everything else while printing.
        //$("body").addClass("printing");
        ////Print the window.
        //window.print();
        ////Restore the styles.
        //$("body").removeClass("printing");
        ////Clear up the div.
        //$("#print-me").empty();

        var printContents = document.getElementById(divname).innerHTML;
        var originalContents = document.body.innerHTML;
        document.body.innerHTML = printContents;
        window.print();
        document.body.innerHTML = originalContents;

    //var style = '@page { margin: 0 } @media print { ul, .myUL {list - style - type: none;}.myUL {margin: 0;padding: 0;}.caret2 {cursor: pointer;-webkit - user - select: none; /* Safari 3.1+ */-moz - user - select: none; /* Firefox 2+ */-ms - user - select: none; /* IE 10+ */user - select: none;}#printableDiv {background - color: aliceblue!important;}.active {display: block;} }';

    //    printJS({
    //        printable: 'printableDiv',
    //        type: 'html',
    //        style: style,
    //        scanStyles: false
    //    });

    }