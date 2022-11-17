using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Features;
using SinePulse.EMS.Messages.MarkMessages;
using SinePulse.EMS.Messages.TermMessages;
using SinePulse.EMS.Site.Authorization;
using SinePulse.EMS.Site.Models;
using SinePulse.EMS.Site.Presenters;
using SinePulse.EMS.UseCases.Authorization;
using SinePulse.EMS.UseCases.Marks;
using SinePulse.EMS.UseCases.Terms;

namespace SinePulse.EMS.Site.Controllers
{
  [Authorize]
  public class TermController : BaseController
  {
    private readonly DisplayAddTermPageRequestHandler _displayAddTermPageRequestHandler;
    private readonly DisplayAddTermPageResponsePresenter _displayAddTermPageResponsePresenter;
    private readonly AddTermRequestHandler _addTermRequestHandler;
    private readonly AddTermResponsePresenter _presenter;
    private readonly ShowTermRequestHandler _showTermRequestHandler;
    private readonly ShowTermResponsePresenter _showTermResponsePresenter;
    private readonly AddTermTestMarksRequestHandler _addTermTestMarksRequestHandler;
    private readonly AddTermTestMarksResponsePresenter _addTermTestMarksPresenter;

    public TermController(
      DisplayAddTermPageRequestHandler displayAddTermPageRequestHandler,
      DisplayAddTermPageResponsePresenter displayAddTermPageResponsePresenter,
      AddTermRequestHandler addTermRequestHandler,
      AddTermResponsePresenter presenter,
      ShowTermRequestHandler showTermRequestHandler,
      ShowTermResponsePresenter showTermResponsePresenter,
      GetUserAssociationRequestHandler getUserAssociationRequestHandler, 
      AddTermTestMarksRequestHandler addTermTestMarksRequestHandler, 
      AddTermTestMarksResponsePresenter addTermTestMarksPresenter) : base(getUserAssociationRequestHandler)
    {
      _displayAddTermPageRequestHandler = displayAddTermPageRequestHandler;
      _displayAddTermPageResponsePresenter = displayAddTermPageResponsePresenter;
      _addTermRequestHandler = addTermRequestHandler;
      _presenter = presenter;
      _showTermRequestHandler = showTermRequestHandler;
      _showTermResponsePresenter = showTermResponsePresenter;
      _addTermTestMarksRequestHandler = addTermTestMarksRequestHandler;
      _addTermTestMarksPresenter = addTermTestMarksPresenter;
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Examination, (int)Feature.ExaminationEnum.ViewExamTerm)]
    public IActionResult Index()
    {
      return View();
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Examination, (int)Feature.ExaminationEnum.NewExamTerm)]
    [HttpGet]
    public ActionResult AddTerm(long branchMediumId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new DisplayAddTermPageRequestMessage
      {
        BranchMediumId = branchMediumId
      };


      var model = new AddTermViewModel
      {
        BranchMediumId = branchMediumId
      };

      var response = _displayAddTermPageRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _displayAddTermPageResponsePresenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());

      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Examination, (int)Feature.ExaminationEnum.NewExamTerm)]
    [HttpPost]
    public ActionResult AddTerm(AddTermViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new AddTermRequestMessage();

      requestMessage.TermName = model.TermName;
      requestMessage.StartDate = model.StartDate;
      requestMessage.EndDate = model.EndDate;
      requestMessage.SessionId = model.SessionId;
      requestMessage.BranchMediumId = model.BranchMediumId;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;
      var sessionsBackup = model.Sessions;

      var response = _addTermRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _presenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());
      if (!response.Result.ValidationResult.IsValid)
      {
        var displayAddTermPageRequestMessage = new DisplayAddTermPageRequestMessage
        {
          BranchMediumId = model.BranchMediumId
        };
        var displayAddTermPageResponse = _displayAddTermPageRequestHandler.Handle(displayAddTermPageRequestMessage, cancellationToken);
        var displayAddTermPageViewModel = _displayAddTermPageResponsePresenter.Handle(displayAddTermPageResponse.Result, model, ModelState, GetUserAssociationResponseMessage());
        viewModel.Sessions = displayAddTermPageViewModel.Sessions;
        return View(viewModel);
      }
      
      return Redirect("/BranchMedium/ViewBranchMedium?branchMediumId=" + model.BranchMediumId + "#tab_examTerm");
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Examination, (int)Feature.ExaminationEnum.FindExamTerm)]
    public ActionResult ViewTerm(long termId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowTermRequestMessage {TermId = termId};
      var response = _showTermRequestHandler.Handle(requestMessage, cancellationToken);
      var model = new TermViewModel();
      var viewModel = _showTermResponsePresenter.Handle(response.Result.Data, model, GetUserAssociationResponseMessage());
      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Examination, (int)Feature.ExaminationEnum.EditExamTerm)]
    [HttpGet]
    public ActionResult UpdateTerm(int? id)
    {
      return View(new AddTermViewModel());
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Examination, (int)Feature.ExaminationEnum.EditExamTerm)]
    [HttpPost]
    public ActionResult UpdateTerm()
    {
      return RedirectToAction("ViewTerm", "Term");
    }
    
    [HttpPost]
    public ActionResult AddTermTestMarks(AddTermTestMarksViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new AddTermTestMarksRequestMessage();

      requestMessage.ObtainedMarks = model.ObtainedMarks;
      requestMessage.GraceMarks = model.GraceMarks;
      requestMessage.TermId = model.TermId;
      requestMessage.TermTestId = model.TermTestId;
      requestMessage.StudentSectionIds = model.StudentSectionIds;
      requestMessage.FullMarks = model.FullMarks;
      requestMessage.ClassId = model.ClassId;
      requestMessage.Group = (GroupEnum) model.Group;
      requestMessage.SubjectId = model.SubjectId;
      requestMessage.ExamType = (ExamTypeEnum) model.ExamType;
      requestMessage.SectionId = model.SectionId;
      requestMessage.RemarksArray = model.RemarksArray;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _addTermTestMarksRequestHandler.Handle(requestMessage, cancellationToken);
      var addTermTestMarksViewModel = _addTermTestMarksPresenter.Handle(response.Result, model, ModelState);
      if (!response.Result.ValidationResult.IsValid)
      {
        addTermTestMarksViewModel.BranchMediumId = model.BranchMediumId;
        addTermTestMarksViewModel.TermId = model.TermId;
        addTermTestMarksViewModel.ActiveTestMarksAddTab = true;
      }

      var termViewModel = GetTermViewModel(model.TermId);
      termViewModel.AddTermTestMarksViewModel = addTermTestMarksViewModel;
      return View("ViewTerm", termViewModel);
    }

    private TermViewModel GetTermViewModel(long termId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowTermRequestMessage {TermId = termId};
      var response = _showTermRequestHandler.Handle(requestMessage, cancellationToken);
      var model = new TermViewModel();
      var viewModel = _showTermResponsePresenter.Handle(response.Result.Data, model, GetUserAssociationResponseMessage());
      return viewModel;
    }
  }
}