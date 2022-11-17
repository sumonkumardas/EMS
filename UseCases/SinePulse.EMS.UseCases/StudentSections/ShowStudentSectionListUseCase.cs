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
  public class ShowStudentSectionListUseCase : IShowStudentSectionListUseCase
  {
    private readonly IRepository _repository;
    private readonly EmsDbContext _dbContext;

    public ShowStudentSectionListUseCase(IRepository repository, EmsDbContext dbContext)
    {
      _repository = repository;
      _dbContext = dbContext;
    }

    public List<ShowStudentSectionListResponseMessage.Student> ShowStudentSectionList(ShowStudentSectionListRequestMessage message)
    {
      var studentSectionList = _repository.Table<StudentSection>(new []{nameof(StudentSection.Class),nameof(StudentSection.Section),nameof(StudentSection.Student)}).Where(x=>x.Class.Id == message.ClassId && x.Section.Id == message.SectionId);

      var studentList = new List<ShowStudentSectionListResponseMessage.Student>();
      foreach (var studentSection in studentSectionList)
      {
        studentList.Add(new ShowStudentSectionListResponseMessage.Student()
        {
          DOB = studentSection.Student.BirthDate,
          FullName = studentSection.Student.FullName,
          MobileNo = studentSection.Student.MobileNo,
          StudentId = studentSection.Student.Id,
          RollNo = studentSection.RollNo
        });
      }

      return studentList;


    }
  }
}