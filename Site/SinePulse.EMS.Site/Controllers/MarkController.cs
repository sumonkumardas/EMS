using System.Linq;
using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Features;
using SinePulse.EMS.Messages.MarkMessages;
using SinePulse.EMS.Messages.SessionMessages;
using SinePulse.EMS.Messages.TermTestMarkMessages;
using SinePulse.EMS.Site.Authorization;
using SinePulse.EMS.Site.Models;
using SinePulse.EMS.Site.Presenters;
using SinePulse.EMS.UseCases.Authorization;
using SinePulse.EMS.UseCases.Marks;
using SinePulse.EMS.UseCases.Sessions;
using SinePulse.EMS.UseCases.TermTestMarks;

namespace SinePulse.EMS.Site.Controllers
{
  [Authorize]
  public class MarkController : BaseController
  {
    private readonly DisplayAddMarkPageRequestHandler _displayAddMarkPageRequestHandler;
    private readonly DisplayAddMarkPageResponsePresenter _displayAddMarkPageResponsePresenter;
    private readonly AddMarkRequestHandler _addMarkRequestHandler;
    private readonly AddMarkResponsePresenter _presenter;
    private readonly ShowMarkRequestHandler _showMarkRequestHandler;
    private readonly ShowMarkResponsePresenter _showMarkResponsePresenter;
    private readonly FilterTermTestAddMarkFieldsRequestHandler _filterTermTestAddMarkFieldsRequestHandler;
    private readonly GetTermTestAddMarkObjectsRequestHandler _getTermTestAddMarkObjectsRequestHandler;
    private readonly GetTermTestMarksRequestHandler _getTermTestMarksRequestHandler;
    private readonly ShowSessionListRequestHandler _showSessionListRequestHandler;
    private readonly ShowSessionListResponsePresenter _showSessionListResponsePresenter;

    public MarkController(
      DisplayAddMarkPageRequestHandler displayAddMarkPageRequestHandler,
      DisplayAddMarkPageResponsePresenter displayAddMarkPageResponsePresenter,
      AddMarkRequestHandler addMarkRequestHandler,
      AddMarkResponsePresenter presenter,
      ShowMarkRequestHandler showMarkRequestHandler,
      ShowMarkResponsePresenter showMarkResponsePresenter,
      GetUserAssociationRequestHandler getUserAssociationRequestHandler,
      FilterTermTestAddMarkFieldsRequestHandler filterTermTestAddMarkFieldsRequestHandler,
      GetTermTestAddMarkObjectsRequestHandler getTermTestAddMarkObjectsRequestHandler,
      AddTermTestMarksRequestHandler addTermTestMarksRequestHandler,
      AddTermTestMarksResponsePresenter addTermTestMarksPresenter,
      GetTermTestMarksRequestHandler getTermTestMarksRequestHandler,
      ShowSessionListRequestHandler showSessionListRequestHandler,
      ShowSessionListResponsePresenter showSessionListResponsePresenter) : base(getUserAssociationRequestHandler)
    {
      _displayAddMarkPageRequestHandler = displayAddMarkPageRequestHandler;
      _displayAddMarkPageResponsePresenter = displayAddMarkPageResponsePresenter;
      _addMarkRequestHandler = addMarkRequestHandler;
      _presenter = presenter;
      _showMarkRequestHandler = showMarkRequestHandler;
      _showMarkResponsePresenter = showMarkResponsePresenter;
      _filterTermTestAddMarkFieldsRequestHandler = filterTermTestAddMarkFieldsRequestHandler;
      _getTermTestAddMarkObjectsRequestHandler = getTermTestAddMarkObjectsRequestHandler;
      _getTermTestMarksRequestHandler = getTermTestMarksRequestHandler;
      _showSessionListRequestHandler = showSessionListRequestHandler;
      _showSessionListResponsePresenter = showSessionListResponsePresenter;
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Examination, (int)Feature.ExaminationEnum.ViewMark)]
    public IActionResult Index()
    {
      return View();
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Examination, (int)Feature.ExaminationEnum.AddMark)]
    [HttpGet]
    public ActionResult AddMark(long branchMediumId,long sectionId)
    {
      var cancellationToken = new CancellationToken();
      var sessionListRequestMessage = new ShowSessionListRequestMessage()
      {
        BranchMediumId = branchMediumId
      };

      var sessionListResponseMessage =
        _showSessionListRequestHandler.Handle(sessionListRequestMessage, cancellationToken);
      var sessionListModel =
        _showSessionListResponsePresenter.Handle(sessionListResponseMessage.Result, new ShowSessionListViewModel(), GetUserAssociationResponseMessage());

      var addMarkViewModel = new AddMarkViewModel();
      addMarkViewModel.Sessions = sessionListModel.Sessions.ToList();
      addMarkViewModel.StudentSectionId = sectionId;
      return PartialView("AddMark", addMarkViewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Examination, (int)Feature.ExaminationEnum.AddMark)]
    [HttpPost]
    public JsonResult AddMark([FromBody]AddMarkViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new AddMarkRequestMessage();

      requestMessage.ObtainedMark = model.ObtainedMark;
      requestMessage.GraceMark = model.GraceMark;
      requestMessage.Comment = model.Comment;
      requestMessage.TestId = model.TestId;
      requestMessage.StudentSectionId = model.StudentSectionId;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _addMarkRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _presenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());
      return Json(response.Result.ValidationResult.IsValid);
      //if (!response.Result.ValidationResult.IsValid)
      //{
      //return View(viewModel);
      //}

      //return RedirectToAction("ViewMark", new { markId = response.Result.Data.MarkId });
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Examination, (int)Feature.ExaminationEnum.FindMark)]
    public ActionResult ViewMark(long markId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowMarkRequestMessage { MarkId = markId };
      var response = _showMarkRequestHandler.Handle(requestMessage, cancellationToken);
      var model = new MarkViewModel();
      var viewModel = _showMarkResponsePresenter.Handle(response.Result.Data, model, GetUserAssociationResponseMessage());
      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Examination, (int)Feature.ExaminationEnum.EditMark)]
    [HttpGet]
    public ActionResult UpdateMark(int? id)
    {
      return View(new AddMarkViewModel());
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Examination, (int)Feature.ExaminationEnum.EditMark)]
    [HttpPost]
    public ActionResult UpdateMark()
    {
      return RedirectToAction("ViewMark", "Mark");
    }

    public JsonResult FilterTermTestAddMarkFields(long termId, long branchMediumId, long classId, long group,
      long subjectId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new FilterTermTestAddMarkFieldsRequestMessage
      {
        TermId = termId,
        BranchMediumId = branchMediumId,
        ClassId = classId,
        Group = group,
        SubjectId = subjectId
      };
      var response = _filterTermTestAddMarkFieldsRequestHandler.Handle(requestMessage, cancellationToken);

      return Json(response.Result.TermTestsAddMarkObjects);
    }

    public JsonResult GetTermTestAddMarkObjects(long termId, long branchMediumId, long classId, long group,
      long subjectId, long examType, long sectionId)
    {
      if (termId > 0 && branchMediumId > 0 && classId > 0 && subjectId > 0 && sectionId > 0 && examType > 0)
      {
        var termTestMarks = GetTermTestMarks(termId, branchMediumId, classId, group, subjectId, examType, sectionId);
        if (termTestMarks.StudentMarks != null && termTestMarks.StudentMarks.Any())
        {
          return Json(termTestMarks);
        }
        else
        {
          var cancellationToken = new CancellationToken();
          var requestMessage = new GetTermTestAddMarkObjectsRequestMessage
          {
            TermId = termId,
            BranchMediumId = branchMediumId,
            ClassId = classId,
            Group = group,
            SubjectId = subjectId,
            SectionId = sectionId,
            ExamType = examType
          };
          var response = _getTermTestAddMarkObjectsRequestHandler.Handle(requestMessage, cancellationToken);
          if (response.Result.AddMarkObjects.Students.Any())
            return Json(response.Result.AddMarkObjects);
          return Json(new { disableAddButton = true });
        }
      }

      return Json(new { disableAddButton = true });
    }

    private GetTermTestMarksResponseMessage.TermTestMarks GetTermTestMarks(long termId, long branchMediumId, long classId, long group,
      long subjectId, long examType, long sectionId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new GetTermTestMarksRequestMessage
      {
        TermId = termId,
        BranchMediumId = branchMediumId,
        ClassId = classId,
        Group = (GroupEnum)@group,
        SubjectId = subjectId,
        SectionId = sectionId,
        ExamType = (ExamTypeEnum)examType
      };
      var response = _getTermTestMarksRequestHandler.Handle(requestMessage, cancellationToken);
      return response.Result.TestMarks;
    }

    [HttpGet]
    public JsonResult GetTerm(long sessionId)
    {
      var cancellationToken = new CancellationToken();
      var displayAddMarkPageRequestMessage = new DisplayAddMarkPageRequestMessage
      {
        SessionId = sessionId
      };

      var response =
        _displayAddMarkPageRequestHandler.Handle(displayAddMarkPageRequestMessage, cancellationToken);

      return Json(response.Result.Data.Terms);
    }

    public JsonResult GetSubject(long sessionId, long termId,long sectionId)
    {
      var cancellationToken = new CancellationToken();
      var displayAddMarkPageRequestMessage = new DisplayAddMarkPageRequestMessage
      {
        SessionId = sessionId,
        TermId = termId,
        SectionId = sectionId
      };

      var response =
        _displayAddMarkPageRequestHandler.Handle(displayAddMarkPageRequestMessage, cancellationToken);

      return Json(response.Result.Data.Subjects);
    }

    public JsonResult GetExamType(long sessionId, long termId,long subjectId)
    {
      var cancellationToken = new CancellationToken();
      var displayAddMarkPageRequestMessage = new DisplayAddMarkPageRequestMessage
      {
        SessionId = sessionId,
        TermId = termId,
        SubjectId = subjectId
      };

      var response =
        _displayAddMarkPageRequestHandler.Handle(displayAddMarkPageRequestMessage, cancellationToken);

      return Json(response.Result.Data.ExamTypes);
    }

    public JsonResult GetClassTest(long sessionId, long termId,long subjectId,long examTypeId)
    {
      var cancellationToken = new CancellationToken();
      var displayAddMarkPageRequestMessage = new DisplayAddMarkPageRequestMessage
      {
        SessionId = sessionId,
        TermId = termId,
        SubjectId = subjectId,
        ExamType = (ExamTypeEnum?) examTypeId
      };

      var response =
        _displayAddMarkPageRequestHandler.Handle(displayAddMarkPageRequestMessage, cancellationToken);

      return Json(response.Result.Data.ClassTests);
    }

    public JsonResult GetStudents(long classTestId)
    {
      var cancellationToken = new CancellationToken();
      var displayAddMarkPageRequestMessage = new DisplayAddMarkPageRequestMessage
      {
        TestId = classTestId
      };

      var response =
        _displayAddMarkPageRequestHandler.Handle(displayAddMarkPageRequestMessage, cancellationToken);

      return Json(response.Result.Data.Students);
    }
    public JsonResult GetOtherSectionsStudents(long classTestId)
    {
      var cancellationToken = new CancellationToken();
      var displayAddMarkPageRequestMessage = new DisplayAddMarkPageRequestMessage
      {
        TestId = classTestId
      };

      var response =
        _displayAddMarkPageRequestHandler.Handle(displayAddMarkPageRequestMessage, cancellationToken);

      return Json(response.Result.Data.OtherSectionStudents);
    }

  }
}