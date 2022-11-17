using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.TermTestMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.TermTests
{
  public class ShowTermTestRequestHandler
    : IRequestHandler<ShowTermTestRequestMessage, ShowTermTestResponseMessage>
  {
    private readonly IShowTermTestUseCase _useCase;
    private readonly ShowTermTestRequestMessageValidator _validator;
    private readonly EmsDbContext _dbContext;

    public ShowTermTestRequestHandler(IShowTermTestUseCase useCase,
      ShowTermTestRequestMessageValidator validator,
      EmsDbContext dbContext)
    {
      _useCase = useCase;
      _validator = validator;
      _dbContext = dbContext;
    }

    public Task<ShowTermTestResponseMessage> Handle(ShowTermTestRequestMessage request,
      CancellationToken cancellationToken)
    {
      ShowTermTestResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ShowTermTestResponseMessage(validationResult,new ShowTermTestResponseMessage.TermTest());
        return Task.FromResult(returnMessage);
      }

      var  termTest = _useCase.ShowTermTest(request);
      var convertedTermTest = new ShowTermTestResponseMessage.TermTest
      {
        Id = termTest.Id,
        Title = termTest.ExamConfiguration.Class.ClassName + "-" + termTest.ExamConfiguration.Subject.SubjectName +
                "(" + termTest.ExamType + ")",
        StartTime = termTest.StartTimestamp,
        EndTime = termTest.EndTimestamp,
        ClassCode = termTest.ExamConfiguration.Class.ClassCode,
        ClassName = termTest.ExamConfiguration.Class.ClassName,
        SubjectName = termTest.ExamConfiguration.Subject.SubjectName,
        ExamType = termTest.ExamType,
        TermId = termTest.ExamConfiguration.Term.Id,
        SubjectId = termTest.ExamConfiguration.Subject.Id,
        ClassId = termTest.ExamConfiguration.Class.Id,
        TermBranchId = termTest.ExamConfiguration.Term.Session.BranchMedium.Branch.Id,
        GroupId = (long) _dbContext.ClassSubjects.FirstOrDefault(x=>x.Class.Id == termTest.ExamConfiguration.Class.Id && x.Subject.Id == termTest.ExamConfiguration.Subject.Id).Group
      };


      returnMessage = new ShowTermTestResponseMessage(validationResult, convertedTermTest);
      return Task.FromResult(returnMessage);
    }
  }
}