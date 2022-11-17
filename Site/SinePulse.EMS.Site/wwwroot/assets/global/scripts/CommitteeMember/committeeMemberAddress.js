
$("#SameAsPermanentAddress").change(function () {
  if (this.checked) {
    $('#PresentAddressStreet1').val($('#PermanentAddressStreet1').val());
    $('#PresentAddressStreet2').val($('#PermanentAddressStreet2').val());
    $('#PresentAddressPostalCode').val($('#PermanentAddressPostalCode').val());
    $('#PresentAddressCity').val($('#PermanentAddressCity').val());
  }
});