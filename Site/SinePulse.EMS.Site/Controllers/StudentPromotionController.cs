using System.Linq;
using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SinePulse.EMS.Messages.StudentPromotionMessages;
using SinePulse.EMS.Site.Models;
using SinePulse.EMS.Site.Presenters;
using SinePulse.EMS.UseCases.Authorization;
using SinePulse.EMS.UseCases.StudentPromotions;

namespace SinePulse.EMS.Site.Controllers
{
  [Authorize]
  public class StudentPromotionController : BaseController
  {
    private readonly DisplayPromoteStudentOptionPageRequestHandler _displayPromoteStudentOptionPageRequestHandler;
    private readonly DisplayPromoteStudentOptionPageResponsePresenter _displayPromoteStudentOptionPageResponsePresenter;
    private readonly PromoteStudentOptionRequestHandler _promoteStudentOptionRequestHandler;
    private readonly DisplayPromoteStudentsPageRequestHandler _displayPromoteStudentsPageRequestHandler;
    private readonly DisplayPromoteStudentsPageResponsePresenter _displayPromoteStudentsPageResponsePresenter;
    private readonly PromoteStudentsRequestHandler _promoteStudentsRequestHandler;
    private readonly PromoteStudentsResponsePresenter _promoteStudentsResponsePresenter;

    public StudentPromotionController(
      DisplayPromoteStudentOptionPageRequestHandler displayPromoteStudentOptionPageRequestHandler,
      DisplayPromoteStudentOptionPageResponsePresenter displayPromoteStudentOptionPageResponsePresenter,
      PromoteStudentOptionRequestHandler promoteStudentOptionRequestHandler,
      DisplayPromoteStudentsPageRequestHandler displayPromoteStudentsPageRequestHandler,
      DisplayPromoteStudentsPageResponsePresenter displayPromoteStudentsPageResponsePresenter,
      PromoteStudentsRequestHandler promoteStudentsRequestHandler,
      PromoteStudentsResponsePresenter promoteStudentsResponsePresenter,
      GetUserAssociationRequestHandler getUserAssociationRequestHandler) : base(getUserAssociationRequestHandler)
    {
      _displayPromoteStudentOptionPageRequestHandler = displayPromoteStudentOptionPageRequestHandler;
      _displayPromoteStudentOptionPageResponsePresenter = displayPromoteStudentOptionPageResponsePresenter;
      _promoteStudentOptionRequestHandler = promoteStudentOptionRequestHandler;
      _displayPromoteStudentsPageRequestHandler = displayPromoteStudentsPageRequestHandler;
      _displayPromoteStudentsPageResponsePresenter = displayPromoteStudentsPageResponsePresenter;
      _promoteStudentsRequestHandler = promoteStudentsRequestHandler;
      _promoteStudentsResponsePresenter = promoteStudentsResponsePresenter;
    }

    [HttpGet]
    public ActionResult PromoteStudentOption(long sectionId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new DisplayPromoteStudentOptionPageRequestMessage
      {
        SectionId = sectionId
      };

      var model = new PromoteStudentOptionViewModel
      {
        SectionId = sectionId
      };

      var response = _displayPromoteStudentOptionPageRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _displayPromoteStudentOptionPageResponsePresenter.Handle(response.Result, model, ModelState,
        GetUserAssociationResponseMessage());

      return View(viewModel);
    }

    [HttpPost]
    public ActionResult PromoteStudentOption(PromoteStudentOptionViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new PromoteStudentOptionRequestMessage();

      requestMessage.NextSessionId = model.SectionId;
      requestMessage.ClassId = model.ClassId;

      var response = _promoteStudentOptionRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = model;

      if (!response.Result.ValidationResult.IsValid)
      {
        var displayPromoteStudentOptionPageRequestMessage = new DisplayPromoteStudentOptionPageRequestMessage
        {
          SectionId = model.SectionId
        };

        var displayPromoteStudentOptionPageResponseMessage =
          _displayPromoteStudentOptionPageRequestHandler.Handle(displayPromoteStudentOptionPageRequestMessage,
            cancellationToken);
        displayPromoteStudentOptionPageResponseMessage.Result.ValidationResult = response.Result.ValidationResult;
        viewModel = _displayPromoteStudentOptionPageResponsePresenter.Handle(
          displayPromoteStudentOptionPageResponseMessage.Result, model,
          ModelState, GetUserAssociationResponseMessage());
        return View(viewModel);
      }

      return RedirectToAction("PromoteStudents", new
      {
        currentSectionId = model.SectionId,
        nextSessionId = model.SessionId,
        nextClassId = model.ClassId
      });
    }

    [HttpGet]
    public ActionResult PromoteStudents(long currentSectionId, long nextSessionId, long nextClassId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new DisplayPromoteStudentsPageRequestMessage
      {
        CurrentSectionId = currentSectionId,
        NextSessionId = nextSessionId,
        NextClassId = nextClassId
      };

      var model = new PromoteStudentsViewModel
      {
        CurrentSectionId = currentSectionId,
        NextSessionId = nextSessionId,
        NextClassId = nextClassId
      };

      var response = _displayPromoteStudentsPageRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _displayPromoteStudentsPageResponsePresenter.Handle(response.Result, model, ModelState,
        GetUserAssociationResponseMessage());

      return View(viewModel);
    }

    [HttpPost]
    public ActionResult PromoteStudents(PromoteStudentsViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new PromoteStudentsRequestMessage();

      requestMessage.CurrentSectionId = model.CurrentSectionId;
      requestMessage.StudentPromotionRequests = model.CurrentSectionStudents.Where(x => x.NextSectionId > 0)
        .Select(x => new PromoteStudentsRequestMessage.StudentPromotionRequest
        {
          StudentId = x.StudentId,
          CurrentStudentSectionId = x.StudentSectionId,
          NextSectionId = x.NextSectionId
        }).ToList();
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _promoteStudentsRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel =
        _promoteStudentsResponsePresenter.Handle(response.Result, model, ModelState,
          GetUserAssociationResponseMessage());

      if (!response.Result.ValidationResult.IsValid)
      {
        var displayPromoteStudentsPageRequestMessage = new DisplayPromoteStudentsPageRequestMessage
        {
          CurrentSectionId = model.CurrentSectionId,
          NextSessionId = model.NextSessionId,
          NextClassId = model.NextClassId
        };

        var displayPromoteStudentsPageResponseMessage =
          _displayPromoteStudentsPageRequestHandler.Handle(displayPromoteStudentsPageRequestMessage, cancellationToken);
        displayPromoteStudentsPageResponseMessage.Result.ValidationResult = response.Result.ValidationResult;
        viewModel = _displayPromoteStudentsPageResponsePresenter.Handle(
          displayPromoteStudentsPageResponseMessage.Result, model,
          ModelState, GetUserAssociationResponseMessage());
        return View(viewModel);
      }

      return Redirect("/Section/ViewSection?sectionId=" + model.CurrentSectionId + "#tab_classTest");
    }
  }
}