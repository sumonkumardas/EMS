using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.TermTestMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.TermTests
{
  public class ShowTermTestListRequestHandler
    : IRequestHandler<ShowTermTestListRequestMessage, ShowTermTestListResponseMessage>
  {
    private readonly IShowTermTestListUseCase _useCase;
    private readonly ShowTermTestListRequestMessageValidator _validator;
    private readonly EmsDbContext _dbContext;

    public ShowTermTestListRequestHandler(IShowTermTestListUseCase useCase,
      ShowTermTestListRequestMessageValidator validator,
      EmsDbContext dbContext)
    {
      _useCase = useCase;
      _validator = validator;
      _dbContext = dbContext;
    }

    public Task<ShowTermTestListResponseMessage> Handle(ShowTermTestListRequestMessage request,
      CancellationToken cancellationToken)
    {
      ShowTermTestListResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ShowTermTestListResponseMessage(validationResult,new List<ShowTermTestListResponseMessage.TermTest>());
        return Task.FromResult(returnMessage);
      }

      var list = _useCase.ShowTermTestList(request);
      List<ShowTermTestListResponseMessage.TermTest> responseMessageClassTestList =
          new List<ShowTermTestListResponseMessage.TermTest>();

      foreach (var termTest in list)
      {
        var ct = new ShowTermTestListResponseMessage.TermTest
        {
          Id = termTest.Id,
          Title = termTest.ExamConfiguration.Class.ClassName + "-" + termTest.ExamConfiguration.Subject.SubjectName + "(" + termTest.ExamType+")",  
          StartTime = termTest.StartTimestamp,
          EndTime = termTest.EndTimestamp,
          ClassCode = termTest.ExamConfiguration.Class.ClassCode,
          ClassName = termTest.ExamConfiguration.Class.ClassName,
          SubjectName = termTest.ExamConfiguration.Subject.SubjectName,
          ExamType = termTest.ExamType.ToString()
        };
        responseMessageClassTestList.Add(ct);
      }


      returnMessage = new ShowTermTestListResponseMessage(validationResult, responseMessageClassTestList);
      return Task.FromResult(returnMessage);
    }
  }
}