using SinePulse.EMS.Messages.EmployeePersonalInfoMessages;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class ShowEmployeePersonalInfoResponsePresenter
  {
    public EmployeePersonalInfoViewModel Handle(GetEmployeePersonalInfoResponseMessage message,
      EmployeePersonalInfoViewModel viewModel)
    {
      viewModel.DOB = message.EmployeePersonalInfos.DOB;
      viewModel.IsAccountRegistered = message.EmployeePersonalInfos.IsAccountRegistered;
      viewModel.NationalId = message.EmployeePersonalInfos.NationalId;
      viewModel.MobileNo = message.EmployeePersonalInfos.MobileNo;
      viewModel.EmailAddress = message.EmployeePersonalInfos.EmailAddress;
      viewModel.JoiningDate = message.EmployeePersonalInfos.JoiningDate;
      viewModel.BankAccountNo = message.EmployeePersonalInfos.BankAccountNo;
      viewModel.EmployeeType = message.EmployeePersonalInfos.EmployeeType;
      viewModel.ReportTo = message.EmployeePersonalInfos.ReportTo;
      viewModel.ReportToEmployeeId = message.EmployeePersonalInfos.ReportToEmployeeId;
      viewModel.FathersName = message.EmployeePersonalInfos.FathersName;
      viewModel.MothersName = message.EmployeePersonalInfos.MothersName;
      viewModel.SpouseName = message.EmployeePersonalInfos.SpouseName;
      viewModel.Gender = message.EmployeePersonalInfos.Gender;
      viewModel.Religion = message.EmployeePersonalInfos.Religion;
      viewModel.BloodGroup = message.EmployeePersonalInfos.BloodGroup;
      return viewModel;
    }
  }
}