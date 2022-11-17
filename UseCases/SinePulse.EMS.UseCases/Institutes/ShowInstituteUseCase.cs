using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.InstituteMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Institutes
{
  public class ShowInstituteUseCase : IShowInstituteUseCase
  {
    private readonly IRepository _repository;

    public ShowInstituteUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public Institute ShowInstitute(ShowInstituteRequestMessage request)
    {
      var includes = new[] { nameof(Institute.Branches),  
        nameof(Institute.Sessions), 
        nameof(Institute.Employees),
        nameof(Institute.CommitteeMembers) +"."+ nameof(Designation)
      };
      var institute = _repository.GetByIdWithInclude<Institute>(request.InstituteId, includes);
      return institute;
    }
  }
}