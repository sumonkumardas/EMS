function increaseTotalAmount() {
    var arr = document.getElementsByName('SalaryComponentAmounts');
    var tot = 0.0;
    for(var i = 0; i<arr.length; i++){
        if(parseFloat(arr[i].value))
            tot += parseFloat(arr[i].value);
    }
    document.getElementById('TotalAmount').value = tot;
}