using System;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Domain.Students;
using SinePulse.EMS.Messages.Model.Enums;
using SinePulse.EMS.Messages.Model.Shared;
using SinePulse.EMS.Messages.Model.Students;
using SinePulse.EMS.Messages.StudentMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Students
{
  public class ShowStudentUseCase : IShowStudentUseCase
  {
    private readonly IRepository _repository;

    public ShowStudentUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public ShowStudentResponseMessage ShowStudent(ShowStudentRequestMessage message)
    {
      var studentEntity = _repository.GetByIdWithInclude<Student>(message.StudentId, 
        new []
        {
          nameof(Student.ContactPersons),
          nameof(BranchMedium) +"."+ nameof(Shift)
        });
      var branchMedium = _repository.Table<BranchMedium>()
        .Include(nameof(BranchMedium.Branch) +"."+ nameof(Institute))
        .Include(nameof(BranchMedium.Medium))
        .FirstOrDefault(b => b.Id == message.BranchMediumId);
      var studentSection = _repository.Table<StudentSection>()
        .Include(nameof(Student))
        .Include(nameof(Class))
        .Include(nameof(Section))
        .FirstOrDefault(s => s.Student.Id == message.StudentId);
        
      return new ShowStudentResponseMessage(StudentEntity(studentEntity, branchMedium, studentSection));
    }

    private ShowStudentResponseMessage.Student StudentEntity(Student studentEntity, BranchMedium branchMedium,
      StudentSection studentSection)
    {
      var mapper = new MapperConfiguration(c => c.CreateMap<ContactPerson, ContactPersonMessageModel>()).CreateMapper();
      var addressMapper = new MapperConfiguration(c => c.CreateMap<Address, AddressMessageModel>()).CreateMapper();
      return new ShowStudentResponseMessage.Student
      {
        StudentId = studentEntity.Id,
        FullName = studentEntity.FullName,
        BirthDate = studentEntity.BirthDate,
        Email = studentEntity.Email,
        MobileNo = studentEntity.MobileNo,
        Status = ConvertToMessageStatus(studentEntity.Status),
        ImageUrl = studentEntity.ImageUrl,
        Guardian = (RelationTypeEnum) studentEntity.Guardian,
        InstituteId = branchMedium.Branch.Institute.Id,
        InstituteName = branchMedium.Branch.Institute.OrganisationName,
        BranchId = branchMedium.Branch.Id,
        BranchName = branchMedium.Branch.BranchName,
        MediumId = branchMedium.Medium.Id,
        MediumName = branchMedium.Medium.MediumName,
        Group = (GroupEnum) studentSection.Group,
        ClassId = studentSection.Class.Id,
        ClassName = studentSection.Class.ClassName,
        SectionId = studentSection.Section?.Id ?? 0,
        SectionName = studentSection.Section?.SectionName,
        Roll = studentSection.RollNo,
        BranchMediumId = branchMedium.Id,
        ShiftName = studentEntity.BranchMedium.Shift.ShiftName
      };
    }

    private static StatusEnum ConvertToMessageStatus(Domain.Enums.StatusEnum entityStatus)
    {
      switch (entityStatus)
      {
        case Domain.Enums.StatusEnum.Active:
          return StatusEnum.Active;
        case Domain.Enums.StatusEnum.Inactive:
          return StatusEnum.Inactive;
        case Domain.Enums.StatusEnum.Pending:
          return StatusEnum.Pending;
        case Domain.Enums.StatusEnum.Transferred:
          return StatusEnum.Transferred;
        case Domain.Enums.StatusEnum.Passed:
          return StatusEnum.Passed;
        default:
          throw new ArgumentOutOfRangeException(nameof(entityStatus), entityStatus, null);
      }
    }
  }
}