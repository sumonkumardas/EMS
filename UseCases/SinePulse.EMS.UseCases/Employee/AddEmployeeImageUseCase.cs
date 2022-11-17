using System;
using SinePulse.EMS.Messages.EmployeeMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Employee
{
  public class AddEmployeeImageUseCase : IAddEmployeeImageUseCase
  {
    private readonly IRepository _repository;

    public AddEmployeeImageUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public string UploadEmployeeImage(AddEmployeeImageRequestMessage request)
    {
      var employee = _repository.GetById<Domain.Employees.Employee>(request.EmployeeId);
      var previousImage = employee.ImageUrl;
      employee.ImageUrl = request.ImageUrl;
      employee.AuditFields.LastUpdatedBy = request.CurrentUserName;
      employee.AuditFields.LastUpdatedDateTime = DateTime.Now;

      if (!string.IsNullOrEmpty(request.ImageUrl))
        return previousImage;
      return null;
    }
  }
}