using SinePulse.EMS.Messages.StudentMessages;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class ShowStudentAddressResponsePresenter
  {
    public AddressViewModel Handle(GetStudentAddressResponseMessage message, AddressViewModel viewModel)
    {
      viewModel.PresentStreet1 = message.Address.PresentStreet1;
      viewModel.PresentStreet2 = message.Address.PresentStreet2;
      viewModel.PresentPostalCode = message.Address.PresentPostalCode;
      viewModel.PresentCity = message.Address.PresentCity;
      viewModel.PermanentStreet1 = message.Address.PermanentStreet1;
      viewModel.PermanentStreet2 = message.Address.PermanentStreet2;
      viewModel.PermanentPostalCode = message.Address.PermanentPostalCode;
      viewModel.PermanentCity = message.Address.PermanentCity;
      return viewModel;
    }
  }
}