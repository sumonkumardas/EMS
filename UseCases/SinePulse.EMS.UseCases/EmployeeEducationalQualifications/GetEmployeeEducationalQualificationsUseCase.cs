using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Employees;
using SinePulse.EMS.Messages.EmployeeEducationalQualificationMessages;
using SinePulse.EMS.Messages.Model.Employees;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.EmployeeEducationalQualifications
{
  public class GetEmployeeEducationalQualificationsUseCase : IGetEmployeeEducationalQualificationsUseCase
  {
    private readonly IRepository _repository;
    private readonly MapperConfiguration _mapperConfiguration;

    public GetEmployeeEducationalQualificationsUseCase(IRepository repository)
    {
      _repository = repository;
      _mapperConfiguration = new MapperConfiguration(cfg =>
        cfg.CreateMap<EducationalQualification, EducationalQualificationMessageModel>());
    }

    public IEnumerable<EducationalQualificationMessageModel> GetEducationalQualifications(
      GetEmployeeEducationalQualificationsRequestMessage message)
    {
      var educationalQualifications = _repository.Table<EducationalQualification>()
        .Where(e => e.Employee.Id == message.EmployeeId)
        .ToList();
      var mapper = _mapperConfiguration.CreateMapper();
      return mapper.Map(educationalQualifications, new List<EducationalQualificationMessageModel>());
    }
  }
}