using System.Linq;
using SinePulse.EMS.Messages.OnlinePaymentMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.OnlinePayments
{
  public class GetUserInfoUseCase : IGetUserInfoUseCase
  {
    private readonly IRepository _repository;

    public GetUserInfoUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public GetUserInfoResponseMessage.UserInfo GetUserInfo(GetUserInfoRequestMessage message)
    {
      var employee = _repository.Table<Domain.Employees.Employee>()
        .FirstOrDefault(e => message.EmployeeCode != null && e.EmployeeCode == message.EmployeeCode);
      if (employee != null)
      {
        return new GetUserInfoResponseMessage.UserInfo
        {
          Fullname = employee.FullName,
          PhoneNo = employee.MobileNo,
          EmailAddress = employee.EmailAddress
        };
      }
      return new GetUserInfoResponseMessage.UserInfo();
    }
  }
}