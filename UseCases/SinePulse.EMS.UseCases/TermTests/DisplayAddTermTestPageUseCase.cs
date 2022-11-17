using System.Linq;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Messages.TermTestMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.TermTests
{
  public class DisplayAddTermTestPageUseCase : IDisplayAddTermTestPageUseCase
  {
    private readonly IRepository _repository;

    public DisplayAddTermTestPageUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public DisplayAddTermTestPageResponseMessage DisplayAddTermTestPage(
      DisplayAddTermTestPageRequestMessage requestMessage)
    {
      var includes = new[]
      {
        nameof(ExamConfiguration.Term),
        nameof(ExamConfiguration.Subject),
        nameof(ExamConfiguration.Class)
      };

      if (requestMessage.ClassId == null && requestMessage.GroupId == null && requestMessage.SubjectId == null)
      {
        var roomIncludes = new[] {nameof(Room.Branch)};
        var classes = _repository.Table<ExamConfiguration>(includes)
          .Where(x => x.Term.Id == requestMessage.TermId)
          .Select(x => x.Class);
        var enumerable = classes.Distinct().ToList();
        var roomsList = _repository.Table<Room>(roomIncludes)
          .Where(x => requestMessage.BranchId != null &&
                      x.Branch.Id == requestMessage.BranchId);
        return new DisplayAddTermTestPageResponseMessage(enumerable.Select(ToRequestClass).ToList(), null, null,
          roomsList.Select(ToRequestRoom).ToList());
      }

      if (requestMessage.ClassId != null && requestMessage.SubjectId == null && requestMessage.GroupId == null)
      {
        var groups = _repository.Table<ClassSubject>()
          .Where(x => x.Class.Id == requestMessage.ClassId)
          .Select(x => x.Group);
        var enumerable = groups.Distinct().ToList();
        return new DisplayAddTermTestPageResponseMessage(null, null, enumerable.Select(ToRequestGroup).ToList(), null);
      }

      if (requestMessage.ClassId != null && requestMessage.GroupId != null && requestMessage.SubjectId == null)
      {
        var subjects = _repository.Table<ClassSubject>()
          .Where(x => x.Class.Id == requestMessage.ClassId &&
                      x.Group == (GroupEnum) requestMessage.GroupId)
          .Select(x => x.Subject);
        var examConfigSubjects = _repository.Table<ExamConfiguration>(includes)
          .Where(x => x.Term.Id == requestMessage.TermId && 
                      x.Class.Id == requestMessage.ClassId)
          .Select(x => x.Subject);
        var enumerable = examConfigSubjects.Intersect(subjects).Distinct().ToList();
        return new DisplayAddTermTestPageResponseMessage(null, enumerable.Select(ToRequestSubject).ToList(), null,
          null);
      }

      return new DisplayAddTermTestPageResponseMessage(null, null, null, null);
      ;
    }

    private DisplayAddTermTestPageResponseMessage.Subject ToRequestSubject(Subject subject)
    {
      return new DisplayAddTermTestPageResponseMessage.Subject
      {
        SubjectId = subject.Id,
        SubjectName = subject.SubjectName
      };
    }

    private static DisplayAddTermTestPageResponseMessage.Class ToRequestClass(Class @class)
    {
      return new DisplayAddTermTestPageResponseMessage.Class
      {
        ClassId = @class.Id,
        ClassName = @class.ClassName
      };
    }

    private static DisplayAddTermTestPageResponseMessage.Room ToRequestRoom(Room room)
    {
      return new DisplayAddTermTestPageResponseMessage.Room
      {
        RoomId = room.Id,
        RoomName = room.RoomNo
      };
    }

    private static DisplayAddTermTestPageResponseMessage.Group ToRequestGroup(GroupEnum group)
    {
      return new DisplayAddTermTestPageResponseMessage.Group
      {
        GroupId = (long) group,
        GroupName = group.ToString()
      };
    }
  }
}