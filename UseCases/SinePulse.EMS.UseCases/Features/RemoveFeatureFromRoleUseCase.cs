using System.Linq;
using SinePulse.EMS.Domain.Features;
using SinePulse.EMS.Messages.FeatureMessages;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Features
{
  public class RemoveFeatureFromRoleUseCase : IRemoveFeatureFromRoleUseCase
  {
    private readonly IRepository _repository;
    private readonly EmsDbContext _dbContext;

    public RemoveFeatureFromRoleUseCase(IRepository repository, EmsDbContext dbContext)
    {
      _repository = repository;
      _dbContext = dbContext;
    }

    public void RemoveFeatureFromRole(RemoveFeatureFromRoleRequestMessage message)
    {
      foreach (var featureId in message.FeatureIds)
      {
        var roleFeature = _repository
          .Table<RoleFeature>()
          .FirstOrDefault(rf => rf.Feature.Id == featureId
                                && rf.Role.Id == message.RoleId);
        if (roleFeature != null)
        {
          _repository.Delete(roleFeature);
          _dbContext.SaveChanges();
        }
      }
    }
  }
}