using AutoMapper;
using SinePulse.EMS.Domain.Employees;
using SinePulse.EMS.Messages.EmployeeEducationalQualificationMessages;
using SinePulse.EMS.Messages.Model.Employees;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.EmployeeEducationalQualifications
{
  public class ShowEducationalQualificationUseCase : IShowEducationalQualificationUseCase
  {
    private readonly IRepository _repository;
    private readonly MapperConfiguration _mapperConfiguration;

    public ShowEducationalQualificationUseCase(IRepository repository)
    {
      _repository = repository;
      _mapperConfiguration = new MapperConfiguration(cfg =>
        cfg.CreateMap<EducationalQualification, EducationalQualificationMessageModel>());
    }

    public EducationalQualificationMessageModel ShowEducationalQualification(
      ShowEducationalQualificationRequestMessage message)
    {
      var mapper = _mapperConfiguration.CreateMapper();
      var educationalQualification = _repository.GetById<EducationalQualification>(message.EducationalQualificationId);
      return mapper.Map(educationalQualification, new EducationalQualificationMessageModel());
    }
  }
}