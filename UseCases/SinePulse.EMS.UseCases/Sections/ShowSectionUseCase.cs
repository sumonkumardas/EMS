using System.Linq;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Accounts;
using SinePulse.EMS.Domain.Employees;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Domain.Students;
using SinePulse.EMS.Messages.SectionMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Sections
{
  public class ShowSectionUseCase : IShowSectionUseCase
  {
    private readonly IRepository _repository;

    public ShowSectionUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public Section ShowSection(ShowSectionRequestMessage request)
    {
      var includes = new string[] {
        nameof(Section.Class) +"."+ nameof(Class.AcademicFees) +"." +nameof(AccountHead),
        nameof(BranchMedium),
        nameof(Section.BranchMedium) + "." + nameof(Section.BranchMedium.Branch) +"."+nameof(Institute),
        nameof(Section.BranchMedium) +"."+nameof(Section.BranchMedium.Medium),
        nameof(Section.BranchMedium) +"."+nameof(Section.BranchMedium.Shift),
        nameof(Section.Session),nameof(Class),
        nameof(Section.StudentSections),nameof(Section.Routines),nameof(Section.ClassTests),
        nameof(Section.Room),
        nameof(Section.Routines)+"."+nameof(Subject),
        nameof(Section.StudentSections)+"."+nameof(Student),
        nameof(Section.ClassTests)+"."+nameof(ExamConfiguration),
        nameof(Section.ClassTests)+"."+nameof(ExamConfiguration)+"."+nameof(ExamConfiguration.Term),
        nameof(Section.ClassTests)+"."+nameof(ExamConfiguration)+"."+nameof(ExamConfiguration.Class),
        nameof(Section.ClassTests)+"."+nameof(ExamConfiguration)+"."+nameof(ExamConfiguration.Subject),
        nameof(Section.Routines)+"."+nameof(Teacher)
      };
      
      var section = _repository.GetByIdWithInclude<Section>(request.SectionId, includes);
      return section;
    }

    public BreakTime ShowBreakTime(ShowSectionRequestMessage request)
    {
      var section = _repository.GetByIdWithInclude<Section>(request.SectionId, new []{nameof(Section.BranchMedium) });

      return _repository.Table<BreakTime>(new []
      {
        nameof(BreakTime.BranchMedium)
      }).FirstOrDefault(x=>x.BranchMedium.Id == section.BranchMedium.Id);
    }
  }
}