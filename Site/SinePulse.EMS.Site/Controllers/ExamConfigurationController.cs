using System.Linq;
using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SinePulse.EMS.Domain.Features;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.ExamConfigurationMessages;
using SinePulse.EMS.Site.Authorization;
using SinePulse.EMS.Site.Models;
using SinePulse.EMS.Site.Presenters;
using SinePulse.EMS.UseCases.Authorization;
using SinePulse.EMS.UseCases.ExamConfigurations;

namespace SinePulse.EMS.Site.Controllers
{
  [Authorize]
  public class ExamConfigurationController : BaseController
  {
    private readonly AddExamConfigurationRequestHandler _addExamConfigurationRequestHandler;
    private readonly AddExamConfigurationResponsePresenter _addExamConfigurationResponsePresenter;
    private readonly ShowExamConfigurationRequestHandler _showExamConfigurationRequestHandler;
    private readonly ShowExamConfigurationResponsePresenter _showExamConfigurationResponsePresenter;
    private readonly DisplayAddExamTypePageRequestHandler _displayAddExamTypePageRequestHandler;
    private readonly EditExamConfigurationRequestHandler _editExamConfigurationRequestHandler;
    private readonly EditExamConfigurationResponsePresenter _editExamConfigurationResponsePresenter;

    public ExamConfigurationController(
      AddExamConfigurationRequestHandler addExamConfigurationRequestHandler,
      AddExamConfigurationResponsePresenter addExamConfigurationResponsePresenter,
      ShowExamConfigurationRequestHandler showExamConfigurationRequestHandler,
      ShowExamConfigurationResponsePresenter showExamConfigurationResponsePresenter,
      GetUserAssociationRequestHandler getUserAssociationRequestHandler,
      DisplayAddExamTypePageRequestHandler displayAddExamTypePageRequestHandler,
      EditExamConfigurationRequestHandler editExamConfigurationRequestHandler,
      EditExamConfigurationResponsePresenter editExamConfigurationResponsePresenter) : base(getUserAssociationRequestHandler)
    {
      _addExamConfigurationRequestHandler = addExamConfigurationRequestHandler;
      _addExamConfigurationResponsePresenter = addExamConfigurationResponsePresenter;
      _showExamConfigurationRequestHandler = showExamConfigurationRequestHandler;
      _showExamConfigurationResponsePresenter = showExamConfigurationResponsePresenter;
      _displayAddExamTypePageRequestHandler = displayAddExamTypePageRequestHandler;
      _editExamConfigurationRequestHandler = editExamConfigurationRequestHandler;
      _editExamConfigurationResponsePresenter = editExamConfigurationResponsePresenter;
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Examination, (int)Feature.ExaminationEnum.ViewExamConfiguration)]
    [HttpGet]
    public IActionResult Index()
    {
      return View();
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Examination, (int)Feature.ExaminationEnum.AddExamConfiguration)]
    [HttpGet]
    public ActionResult AddExamConfiguration(long termId)
    {
      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();
      var requestMessage = new AddExamConfigurationRequestMessage
      {
        TermId = termId
      };

      var model = new AddExamConfigurationViewModel
      {
        LoginName = userAssociationMessage.LoginName,
        LoginImageUrl = userAssociationMessage.ImageUrl,
        AssociatedWith = userAssociationMessage.AssociatedWith,
        LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId,
        LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId,
        LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId,
        RoleFeatures = userAssociationMessage.RoleFeatures,
        TermId = termId
      };

      return View(model);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Examination, (int)Feature.ExaminationEnum.AddExamConfiguration)]
    [HttpPost]
    public ActionResult AddExamConfiguration(AddExamConfigurationViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new AddExamConfigurationRequestMessage();

      requestMessage.ClassTestPercentage = model.ClassTestPercentage;
      requestMessage.ObjectiveFullMark = model.ObjectiveFullMark;
      requestMessage.ObjectivePassMark = model.ObjectivePassMark;
      requestMessage.SubjectiveFullMark = model.SubjectiveFullMark;
      requestMessage.SubjectivePassMark = model.SubjectivePassMark;
      requestMessage.PracticalFullMark = model.PracticalFullMark;
      requestMessage.PracticalPassMark = model.PracticalPassMark;
      requestMessage.TermId = model.TermId;
      requestMessage.ClassId = model.ClassId;
      requestMessage.SubjectId = model.SubjectId;
      requestMessage.GroupId = model.GroupId;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _addExamConfigurationRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _addExamConfigurationResponsePresenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());
      if (!response.Result.ValidationResult.IsValid)
      {
        return View(viewModel);
      }
      return Redirect("/Term/ViewTerm?termId=" + requestMessage.TermId + "#examType");
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Examination, (int)Feature.ExaminationEnum.FindExamConfiguration)]
    public ActionResult ViewExamConfiguration(long examConfigurationId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowExamConfigurationRequestMessage { ExamConfigurationId = examConfigurationId };
      var response = _showExamConfigurationRequestHandler.Handle(requestMessage, cancellationToken);
      var model = new ExamConfigurationViewModel();
      var viewModel = _showExamConfigurationResponsePresenter.Handle(response.Result.Data, model, GetUserAssociationResponseMessage());
      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Examination, (int)Feature.ExaminationEnum.EditExamConfiguration)]
    [HttpGet]
    public ActionResult UpdateExamConfiguration(int examConfigurationId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowExamConfigurationRequestMessage { ExamConfigurationId = examConfigurationId };
      var response = _showExamConfigurationRequestHandler.Handle(requestMessage, cancellationToken);
      var model = new ExamConfigurationViewModel();
      var viewModel = _showExamConfigurationResponsePresenter.Handle(response.Result.Data, model, GetUserAssociationResponseMessage());
      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Examination, (int)Feature.ExaminationEnum.EditExamConfiguration)]
    [HttpPost]
    public ActionResult UpdateExamConfiguration(ExamConfigurationViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new EditExamConfigurationRequestMessage
      {
        Id = model.ExamConfigurationId,
        ClassTestPercentage = model.ClassTestPercentage,
        ObjectiveFullMark = model.ObjectiveFullMark,
        ObjectivePassMark = model.ObjectivePassMark,
        SubjectiveFullMark = model.SubjectiveFullMark,
        SubjectivePassMark = model.SubjectivePassMark,
        PracticalFullMark = model.PracticalFullMark,
        PracticalPassMark = model.PracticalPassMark,
        TermId = model.TermId,
        ClassId = model.ClassId,
        SubjectId = model.SubjectId,
        GroupId = model.GroupId,
        CurrentUserName = HttpContext.User.Identity.Name
      };

      var response = _editExamConfigurationRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _editExamConfigurationResponsePresenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());
      if (!response.Result.ValidationResult.IsValid)
      {
        return View(viewModel);
      }
      return Redirect("/Term/ViewTerm?termId=" + requestMessage.TermId + "#examType");
    }

    [HttpPost]
    public JsonResult GetClassValues()
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new DisplayAddExamConfigurationPageRequestMessage();
      var responseMessage = _displayAddExamTypePageRequestHandler.Handle(requestMessage, cancellationToken);

      return Json(responseMessage.Result.Data.Classes);
    }

    [HttpPost]
    public JsonResult GetSubjectValues(long classId, long groupId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new DisplayAddExamConfigurationPageRequestMessage();
      requestMessage.ClassId = classId;
      requestMessage.GroupId = groupId;
      var responseMessage = _displayAddExamTypePageRequestHandler.Handle(requestMessage, cancellationToken);

      return Json(responseMessage.Result.Data.Subjects);
    }

    [HttpPost]
    public JsonResult GetGroupValues(long classId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new DisplayAddExamConfigurationPageRequestMessage();
      requestMessage.ClassId = classId;
      var responseMessage = _displayAddExamTypePageRequestHandler.Handle(requestMessage, cancellationToken);

      return Json(responseMessage.Result.Data.Groups);
    }
  }
}