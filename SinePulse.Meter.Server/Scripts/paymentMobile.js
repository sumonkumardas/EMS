var paymentModuleMobile = (function (window, undefined) {

    function isValid() {
        $("#payOnline").prop('disabled', true);
        if (!parseFloat($('#amount').val())) {
            $('#amountError').text('Invalid amount');
        }
        isAmountSufficient($('#amount').val());

    }
    function isAmountSufficient(amount) {
        var IsSufficientAmount = false;
            $.ajax({
                type: "POST",
                url: $("#sufficientAmountURL").val(),
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ meterNo: $('#meterNo').val(), amount: $('#amount').val() }),
                dataType: "json",
                async:false,
                success:
                    function (data) {
                        IsSufficientAmount =  data.IsSufficientAmount;
                        if (data.IsSufficientAmount) {
                            $("#payOnline").prop('disabled', true);
                            $('#amountError').text('');

                            
                            $("#payOnline").css("color", "#666666");
                            $("#payOnline").css("background", "#cccccc");
                            $.ajax({
                                type: "POST",
                                url: $("#onlinePaymentMobileUrl").val(),
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
                                            $("#payOnline").prop('disabled', false);
                                        }
                                        else {
                                            window.location.href = data.FailUrl;
                                        }

                                    },
                                error: function () { $("#payOnline").prop('disabled', false); }
                            });
                        } else {
                            $("#payOnline").prop('disabled', false);
                            $('#amountError').text(data.Message);
                        }
                    },
                error: function () {
                    $("#payOnline").prop('disabled', false);
                    IsSufficientAmount = false;
                }
            });

    }
    function callPayOnline() {

        isValid();

    }

    var bindFunctions = function () {
        $("#payOnline").on("click", callPayOnline);
    };

    var initMobile = function () {
        bindFunctions();
    };

    return {
        InitMobile: initMobile
    };
})(window);