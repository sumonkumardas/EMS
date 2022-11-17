using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SinePulse.EMS.Domain.Features;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.TermMessages;
using SinePulse.EMS.Messages.TermTestMessages;
using SinePulse.EMS.Site.Authorization;
using SinePulse.EMS.Site.Models;
using SinePulse.EMS.Site.Presenters;
using SinePulse.EMS.UseCases.Authorization;
using SinePulse.EMS.UseCases.Rooms;
using SinePulse.EMS.UseCases.Terms;
using SinePulse.EMS.UseCases.TermTests;

namespace SinePulse.EMS.Site.Controllers
{
  [Authorize]
  public class TermTestController : BaseController
  {
    private readonly AddTermTestRequestHandler _addTermTestRequestHandler;
    private readonly EditTermTestRequestHandler _editTermTestRequestHandler;
    private readonly AddTermTestResponsePresenter _presenter;
    private readonly DisplayAddTermTestPageRequestHandler _displayAddTermTestPageRequestHandler;
    private readonly ShowTermTestListRequestHandler _showTermTestListRequestHandler;
    private readonly ShowTermTestEventListResponsePresenter _showTermTestEventListResponsePresenter;
    private readonly ShowTermRequestHandler _showTermRequestHandler;
    private readonly ShowTermTestRequestHandler _showTermTestRequestHandler;

    public TermTestController(AddTermTestRequestHandler addTermTestRequestHandler,
      AddTermTestResponsePresenter presenter,
      DisplayAddTermTestPageRequestHandler displayAddTermTestPageRequestHandler,
      ShowTermTestListRequestHandler showTermTestListRequestHandler,
      ShowTermTestEventListResponsePresenter showTermTestEventListResponsePresenter,
      ShowTermRequestHandler showTermRequestHandler,
      ShowTermResponsePresenter showTermResponsePresenter,
      ShowRoomsListRequestHandler showRoomsListRequestHandler,
      ShowRoomsListResponsePresenter showRoomsListResponsePresenter,
      GetUserAssociationRequestHandler getUserAssociationRequestHandler,
      EditTermTestRequestHandler editTermTestRequestHandler,
      ShowTermTestRequestHandler showTermTestRequestHandler) : base(getUserAssociationRequestHandler)
    {
      _addTermTestRequestHandler = addTermTestRequestHandler;
      _presenter = presenter;
      _displayAddTermTestPageRequestHandler = displayAddTermTestPageRequestHandler;
      _showTermTestListRequestHandler = showTermTestListRequestHandler;
      _showTermTestEventListResponsePresenter = showTermTestEventListResponsePresenter;
      _showTermRequestHandler = showTermRequestHandler;
      _editTermTestRequestHandler = editTermTestRequestHandler;
      _showTermTestRequestHandler = showTermTestRequestHandler;
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Examination, (int)Feature.ExaminationEnum.AddExamRoutine)]
    [HttpGet]
    public ActionResult AddTermTest(long termId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowTermRequestMessage { TermId = termId };
      var response = _showTermRequestHandler.Handle(requestMessage, cancellationToken);
      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();
      var viewModel = new AddTermTestViewModel
      {
        LoginName = userAssociationMessage.LoginName,
        LoginImageUrl = userAssociationMessage.ImageUrl,
        AssociatedWith = userAssociationMessage.AssociatedWith,
        LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId,
        LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId,
        LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId,
        RoleFeatures = userAssociationMessage.RoleFeatures,
        TermId = termId,
        TermBranchId = response.Result.Data.TermData.BranchMedium.BranchId
      };

      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Examination, (int)Feature.ExaminationEnum.AddExamRoutine)]
    [HttpPost]
    public ActionResult AddTermTest(AddTermTestViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new AddTermTestRequestMessage();
      requestMessage.FullMarks = model.FullMarks;
      requestMessage.ExamType = model.ExamType;
      requestMessage.TermId = model.TermId;
      requestMessage.ClassId = model.ClassId;
      requestMessage.GroupId = model.GroupId;
      requestMessage.Date = model.Date;
       requestMessage.StartTime = model.StartTime;
      requestMessage.EndTime = model.EndTime;
      requestMessage.SubjectId = model.SubjectId;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _addTermTestRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _presenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());
      if (!response.Result.ValidationResult.IsValid)
      {
        return View(viewModel);
      }
      return Redirect("/Term/ViewTerm?termId=" + requestMessage.TermId + "#tab_examRoutine");
    }

    [HttpPost]
    public JsonResult GetClassValues(long termId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new DisplayAddTermTestPageRequestMessage();
      requestMessage.TermId = termId;
      var responseMessage = _displayAddTermTestPageRequestHandler.Handle(requestMessage, cancellationToken);

      return Json(responseMessage.Result.Data.Classes);
    }

    [HttpPost]
    public JsonResult GetRooms(long termId,long branchId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new DisplayAddTermTestPageRequestMessage {TermId = termId,BranchId = branchId};
      var responseMessage = _displayAddTermTestPageRequestHandler.Handle(requestMessage, cancellationToken);

      return Json(responseMessage.Result.Data.Rooms);
    }

    [HttpPost]
    public JsonResult GetSubjectValues(long classId, long groupId, long termId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new DisplayAddTermTestPageRequestMessage();
      requestMessage.ClassId = classId;
      requestMessage.GroupId = groupId;
      requestMessage.TermId = termId;
      var responseMessage = _displayAddTermTestPageRequestHandler.Handle(requestMessage, cancellationToken);

      return Json(responseMessage.Result.Data.Subjects);
    }

    [HttpPost]
    public JsonResult GetGroupValues(long classId, long termId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new DisplayAddTermTestPageRequestMessage();
      requestMessage.ClassId = classId;
      requestMessage.TermId = termId;
      var responseMessage = _displayAddTermTestPageRequestHandler.Handle(requestMessage, cancellationToken);

      return Json(responseMessage.Result.Data.Groups);
    }

    public JsonResult GetAllClassRoutineEvents(long termId,int year,int month,int day)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowTermTestListRequestMessage {TermId = termId};
      if (year > 0 && day > 0 && month > 0)
      {
        requestMessage.Year = year;
        requestMessage.Month = month;
        requestMessage.Day = day;
      }

      var response = _showTermTestListRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _showTermTestEventListResponsePresenter.Handle(response.Result);

      return Json(viewModel);
    }

    public ActionResult GetAllClassRoutineEventsForList(long termId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowTermTestListRequestMessage { TermId = termId };

      var response = _showTermTestListRequestHandler.Handle(requestMessage, cancellationToken);

      return PartialView("ShowTermTestList", response.Result.TermTestList);
    }

    public ActionResult ShowAddTermTestMark(long branchMediumId, long termId)
    {
      var model = new AddTermTestMarksViewModel {BranchMediumId = branchMediumId, TermId = termId};

      return PartialView("~/Views/Term/AddTermTestMarks.cshtml",model);

    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Examination, (int)Feature.ExaminationEnum.AddExamRoutine)]
    [HttpGet]
    public ActionResult UpdateTermTest(long termTestId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowTermTestRequestMessage { TermTestId = termTestId };
      var response = _showTermTestRequestHandler.Handle(requestMessage, cancellationToken);
      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();
      var viewModel = new EditTermTestViewModel
      {
        LoginName = userAssociationMessage.LoginName,
        LoginImageUrl = userAssociationMessage.ImageUrl,
        AssociatedWith = userAssociationMessage.AssociatedWith,
        LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId,
        LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId,
        LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId,
        RoleFeatures = userAssociationMessage.RoleFeatures,
        Id = termTestId,
          TermId = response.Result.PickedTermTest.TermId,
          EndTime = response.Result.PickedTermTest.EndTime.TimeOfDay,
          StartTime = response.Result.PickedTermTest.StartTime.TimeOfDay,
          ClassId = response.Result.PickedTermTest.ClassId,
          SubjectId = response.Result.PickedTermTest.SubjectId,
          Date = response.Result.PickedTermTest.StartTime.Date,
          ExamType = response.Result.PickedTermTest.ExamType,
          TermBranchId = response.Result.PickedTermTest.TermBranchId,
          GroupId = response.Result.PickedTermTest.GroupId
      };

      return View(viewModel);
    }
    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Examination, (int)Feature.ExaminationEnum.AddExamRoutine)]
    [HttpPost]
    public ActionResult UpdateTermTest(EditTermTestViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new EditTermTestRequestMessage();
      requestMessage.Id = model.Id;
      requestMessage.FullMarks = model.FullMarks;
      requestMessage.ExamType = model.ExamType;
      requestMessage.TermId = model.TermId;
      requestMessage.ClassId = model.ClassId;
      requestMessage.GroupId = model.GroupId;
      requestMessage.Date = model.Date;
      requestMessage.StartTime = model.StartTime;
      requestMessage.EndTime = model.EndTime;
      requestMessage.SubjectId = model.SubjectId;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _editTermTestRequestHandler.Handle(requestMessage, cancellationToken);
      if (!response.Result.ValidationResult.IsValid)
      {
        foreach (var error in response.Result.ValidationResult.Errors)
        {
          ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
        }
        return View(model);
      }
      return Redirect("/Term/ViewTerm?termId=" + requestMessage.TermId + "#tab_examRoutine");
    }
  }
}