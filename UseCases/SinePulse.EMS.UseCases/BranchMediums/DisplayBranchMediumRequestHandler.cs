using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.BranchMediumMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.BranchMediums
{
  public class
    DisplayBranchMediumRequestHandler : IRequestHandler<DisplayBranchMediumRequestMessage, DisplayBranchMediumResponseMessage>
  {
    private readonly DisplayBranchMediumRequestMessageValidator _validator;
    private readonly IDisplayBranchMediumUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public DisplayBranchMediumRequestHandler(DisplayBranchMediumRequestMessageValidator validator,
      IDisplayBranchMediumUseCase useCase, EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<DisplayBranchMediumResponseMessage> Handle(DisplayBranchMediumRequestMessage request,
      CancellationToken cancellationToken)
    {
      DisplayBranchMediumResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new DisplayBranchMediumResponseMessage(validationResult, new DisplayBranchMediumResponseMessage.Institute(), new DisplayBranchMediumResponseMessage.Branch(), new DisplayBranchMediumResponseMessage.BranchMedium(),new List<DisplayBranchMediumResponseMessage.Session>());
        return Task.FromResult(returnMessage);
      }

      var pickedBranchMedium = _useCase.ShowBranchMedium(request);

      var institute = new DisplayBranchMediumResponseMessage.Institute()
      {
        InstituteName = pickedBranchMedium.Branch.Institute.OrganisationName,
        InstituteId = pickedBranchMedium.Branch.Institute.Id
      };

      var branch = new DisplayBranchMediumResponseMessage.Branch()
      {
        BranchName = pickedBranchMedium.Branch.BranchName,
        BranchId = pickedBranchMedium.Branch.Id
      };

      var branchMedium = new DisplayBranchMediumResponseMessage.BranchMedium()
      {
        BranchMediumName = pickedBranchMedium.Medium.MediumName,
        BranchMediumId = pickedBranchMedium.Id,
        ShiftName = pickedBranchMedium.Shift.ShiftName
      };
      var sessions = new List<DisplayBranchMediumResponseMessage.Session>();
      if (request.LoadSessions)
      {
        
        DisplayBranchMediumResponseMessage.Session session;
        foreach (var instituteSession in pickedBranchMedium.Branch.Institute.Sessions)
        {
          session = new DisplayBranchMediumResponseMessage.Session
          {
            SessionName = instituteSession.SessionName,
            SessionId = instituteSession.Id
          };

          sessions.Add(session);
        }

        foreach (var branchSession in pickedBranchMedium.Branch.Sessions)
        {
          session = new DisplayBranchMediumResponseMessage.Session
          {
            SessionName = branchSession.SessionName,
            SessionId = branchSession.Id
          };

          sessions.Add(session);
        }

        foreach (var mediuSession in pickedBranchMedium.Sessions)
        {
          session = new DisplayBranchMediumResponseMessage.Session
          {
            SessionName = mediuSession.SessionName,
            SessionId = mediuSession.Id
          };

          sessions.Add(session);
        }
      }

      returnMessage = new DisplayBranchMediumResponseMessage(validationResult, institute, branch, 
        branchMedium,sessions);
      return Task.FromResult(returnMessage);
    }
  }
}