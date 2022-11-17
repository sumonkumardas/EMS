using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Employees;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.EmployeeMessages;
using SinePulse.EMS.Messages.Model.Employees;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Employee
{
  public class ShowEmployeeUseCase : IShowEmployeeUseCase
  {
    private readonly IRepository _repository;
    private readonly MapperConfiguration _automapperConfig;

    public ShowEmployeeUseCase(IRepository repository)
    {
      _repository = repository;
      _automapperConfig = new MapperConfiguration(cfg =>
        cfg.CreateMap<Domain.Employees.Employee, EmployeeMessageModel>());
    }

    public EmployeeMessageModel ShowEmployee(ShowEmployeeRequestMessage request)
    {
      var dbEmployee = _repository.Table<Domain.Employees.Employee>()
        .Include(nameof(BranchMedium))
        .Include(nameof(Branch))
        .Include(nameof(Institute))
        .Include(nameof(BranchMedium) + "." + nameof(Medium))
        .Include(nameof(BranchMedium) + "." + nameof(Shift))
        .Include(nameof(Domain.Employees.Employee.ReportTo))
        .Include(nameof(Designation))
        .Include(nameof(JobType))
        .Include(nameof(Grade))
        .FirstOrDefault(e => e.Id == request.EmployeeId);
      var mapper = _automapperConfig.CreateMapper();
      return mapper.Map<EmployeeMessageModel>(dbEmployee);
    }
  }
}