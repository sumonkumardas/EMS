using System;
using System.Collections.Generic;
using System.Linq;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Domain.Students;
using SinePulse.EMS.Messages.StudentSectionMessages;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.StudentSections
{
  public class ShowStudentSectionRollUseCase : IShowStudentSectionRollUseCase
  {
    private readonly IRepository _repository;
    private readonly EmsDbContext _dbContext;

    public ShowStudentSectionRollUseCase(IRepository repository, EmsDbContext dbContext)
    {
      _repository = repository;
      _dbContext = dbContext;
    }

    public int ShowStudentSectionRoll(ShowStudentSectionRollRequestMessage message)
    {
      var studentSectionList =  _repository.Table<StudentSection>(new []
      {
        nameof(StudentSection.Class),nameof(StudentSection.Section)
      }).Where(x=>x.Class.Id == message.ClassId && x.Section.Id == message.SectionId && x.Group == message.Group);

      if (studentSectionList == null || studentSectionList.Count() == 0)
        return 1;
      else
      {
        return studentSectionList.Max(x => x.RollNo) + 1;
      }

    }
  }
}