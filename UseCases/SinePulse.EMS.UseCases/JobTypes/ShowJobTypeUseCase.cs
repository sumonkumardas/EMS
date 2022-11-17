using AutoMapper;
using SinePulse.EMS.Domain.Employees;
using SinePulse.EMS.Messages.JobTypeMessages;
using SinePulse.EMS.Messages.Model.Employees;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.JobTypes
{
  public class ShowJobTypeUseCase : IShowJobTypeUseCase
  {
    private readonly IRepository _repository;
    private readonly MapperConfiguration _autoMapperConfig;

    public ShowJobTypeUseCase(IRepository repository)
    {
      _repository = repository;
      _autoMapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<JobType, JobTypeMessageModel>());
    }

    public JobTypeMessageModel ShowJobType(ShowJobTypeRequestMessage message)
    {
      var jobType = _repository.GetById<JobType>(message.JobTypeId);
      var mapper = _autoMapperConfig.CreateMapper();
      return mapper.Map<JobTypeMessageModel>(jobType);
    }
  }
}