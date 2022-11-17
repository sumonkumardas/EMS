using System;
using SinePulse.EMS.Constants;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.AlertProcessingMessages;
using SinePulse.EMS.Messages.ClassTestMessages;
using SinePulse.EMS.Persistence.Repositories;
using SinePulse.EMS.Utility.MessagingQueue;

namespace SinePulse.EMS.UseCases.ClassTests
{
  public class EditClassTestUseCase : IEditClassTestUseCase
  {
    private readonly IRepository _repository;

    public EditClassTestUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public void EditClassTest(EditClassTestRequestMessage requestMessage)
    {
      var examConfiguration = _repository.GetById<ExamConfiguration>(requestMessage.ExamConfigurationId);
      var section = _repository.GetById<Section>(requestMessage.SectionId);
      var classTest = _repository.GetById<ClassTest>(requestMessage.Id);

      classTest.TestName = requestMessage.TestName;
      classTest.FullMarks = requestMessage.FullMarks;
      classTest.StartTimestamp = requestMessage.Date + requestMessage.StartTime;
      classTest.EndTimestamp = requestMessage.Date + requestMessage.EndTime;
      classTest.ExamType = requestMessage.ExamType;
      classTest.Status = StatusEnum.Active;
      
      classTest.AuditFields.LastUpdatedBy = requestMessage.CurrentUserName;
      classTest.AuditFields.LastUpdatedDateTime = DateTime.Now;

      classTest.ExamConfiguration = examConfiguration;
      classTest.Section = section;
    }
  }
}