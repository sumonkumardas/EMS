using System;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Messages.ClassMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Classes
{
  public class EditClassUseCase : IEditClassUseCase
  {
    private readonly IRepository _repository;

    public EditClassUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public void EditClass(EditClassRequestMessage message)
    {
      var @class = _repository.GetById<Class>(message.ClassId);
      
      @class.ClassName = message.ClassName;
      @class.ClassCode = message.ClassCode;
      @class.Status = (Domain.Enums.StatusEnum)message.Status;
      @class.AuditFields.LastUpdatedBy = message.CurrentUserName;
      @class.AuditFields.LastUpdatedDateTime = DateTime.Now;
    }
  }
}