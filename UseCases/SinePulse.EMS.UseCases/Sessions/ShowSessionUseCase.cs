using AutoMapper;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.SessionMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Sessions
{
  public class ShowSessionUseCase : IShowSessionUseCase
  {
    private readonly IRepository _repository;
    private MapperConfiguration _autoMapperConfig;
    

    public ShowSessionUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public SessionMessageModel ShowSession(ViewSessionRequestMessage requestMessage)
    {
      var includes = new[]
      {
        nameof(BranchMedium),
        nameof(BranchMedium) + "." + nameof(Branch),
        nameof(BranchMedium) + "." + nameof(Branch) + "." + nameof(Institute)
      };
      var session = _repository.GetByIdWithInclude<Session>(requestMessage.SessionId, includes);
      _autoMapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<Session, SessionMessageModel>());
      var mapper = _autoMapperConfig.CreateMapper();
      var sessionMessageModel = mapper.Map<SessionMessageModel>(session);

      mapper = _autoMapperConfig.CreateMapper();
      if (sessionMessageModel != null)
      {
        sessionMessageModel.Institute = mapper.Map(session.BranchMedium.Branch.Institute, new InstituteMessageModel());
        return sessionMessageModel;
      }
      else
        return new SessionMessageModel();
    }
  }
}