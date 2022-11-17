var paymentModule = (function (window, undefined) {

    function isValid() {
        if (!parseFloat($('#amount').val())) {
            $('#amountError').text('invalid amount');
            return false;
        }
        else if (!isAmountSufficient($('#amount').val())) {
            
        } else {
            $('#amountError').text('');
            return true;
        }

    }
    function isAmountSufficient(amount) {
        var IsSufficientAmount = false;
        $.ajax({
            type: "POST",
            url: $("#sufficientAmountURL").val(),
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ meterNo: $('#meterNo').val(), amount: $('#amount').val() }),
            async: false,
            dataType: "json",
            success:
                function (data) {
                    IsSufficientAmount = data.IsSufficientAmount;
                    if (!data.IsSufficientAmount) {
                        $('#amountError').text(data.Message);
                    }
                },
            error: function () {
                IsSufficientAmount = false;
            }
        });

        return IsSufficientAmount;
    }
    function callPayOnline() {
        
        if (isValid()) {
            $("#payOnline").prop('disabled', true);
            $("#payOnline").css("color", "#666666");
            $("#payOnline").css("background", "#cccccc");
            $.ajax({
                type: "POST",
                url: $("#onlinePaymentUrl").val(),
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ meterNo: $('#meterNo').val(), amount: $('#amount').val() }),
                dataType: "json",
                success:
                    function (data) {
                        if (data.IsSuccess) {
                            $('input[name=total_amount]').val(data.Amount);
                            $('input[name=tran_id]').val(data.TransactionNo);
                            $('input[name=success_url]').val(data.SuccessUrl);
                            $('input[name=fail_url]').val(data.CancelUrl);
                            $('input[name=cancel_url]').val(data.FailUrl);
                            $("#payment_gw").submit();
                        } else {
                            window.location.href = data.FailUrl;
                        }

                    },
                error: function () { $("#payOnline").prop('disabled', false); }
            });
        }
    }

    var bindFunctions = function () {
        $("#payOnline").on("click", callPayOnline);
    };

    var init = function () {
        bindFunctions();
    };

    return {
        Init: init
    };
})(window);

