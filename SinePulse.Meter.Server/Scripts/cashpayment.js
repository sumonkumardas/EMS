function DisableButton($id) {
    $('#cancelCashPayment').prop('disabled', true);
    $('#cancelCashPayment').css("color", "#666666");
    $('#cancelCashPayment').css("background", "#cccccc");
    $('#payOnCash').prop('disabled', true);
    $('#payOnCash').css("color", "#666666");
    $('#payOnCash').css("background", "#cccccc");
    $($id).closest('form').submit();
}
