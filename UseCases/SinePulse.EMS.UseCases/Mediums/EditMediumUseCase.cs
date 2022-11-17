using System;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Messages.MediumMessages;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Mediums
{
  public class EditMediumUseCase : IEditMediumUseCase
  {
    private readonly IRepository _repository;
    private readonly EmsDbContext _dbContext;

    public EditMediumUseCase(IRepository repository, EmsDbContext dbContext)
    {
      _repository = repository;
      _dbContext = dbContext;
    }

    public void UpdateMedium(EditMediumRequestMessage message)
    {
      var medium = _repository.GetById<Medium>(message.MediumId);

      medium.MediumName = message.MediumName;
      medium.MediumCode = message.MediumCode;
      medium.Status = message.Status;
      medium.AuditFields.LastUpdatedBy = message.CurrentUserName;
      medium.AuditFields.LastUpdatedDateTime = DateTime.Now;

      _dbContext.SaveChanges();
    }
  }
}