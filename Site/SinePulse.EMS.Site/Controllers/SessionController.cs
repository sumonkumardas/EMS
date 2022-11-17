using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Features;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.ImportSessionDataMessages;
using SinePulse.EMS.Messages.SessionMessages;
using SinePulse.EMS.Site.Authorization;
using SinePulse.EMS.Site.Models;
using SinePulse.EMS.Site.Presenters;
using SinePulse.EMS.UseCases.Authorization;
using SinePulse.EMS.UseCases.ImportSessionDatas;
using SinePulse.EMS.UseCases.Sessions;

namespace SinePulse.EMS.Site.Controllers
{
  [Authorize]
  public class SessionController : BaseController
  {
    private readonly AddSessionRequestHandler _addSessionRequestHandler;
    private readonly AddSessionResponsePresenter _presenter;
    private readonly EditSessionRequestHandler _editSessionRequestHandler;
    private readonly EditSessionResponsePresenter _editSessionPresenter;
    private readonly ShowSessionRequestHandler _showSessionRequestHandler;
    private readonly ShowSessionResponsePresenter _showPresenter;
    private readonly ShowSessionListRequestHandler _showSessionListRequestHandler;
    private readonly ShowSessionListResponsePresenter _showSessionListResponsePresenter;
    private readonly DisplayImportSessionDataPageRequestHandler _displayImportSessionDataPageRequestHandler;
    private readonly DisplayImportSessionDataPageResponsePresenter _displayImportSessionDataPageResponsePresenter;
    private readonly ImportSessionDataRequestHandler _importSessionDataRequestHandler;
    private readonly ImportSessionDataResponsePresenter _importSessionDataResponsePresenter;


    public SessionController(AddSessionRequestHandler addSessionRequestHandler, AddSessionResponsePresenter presenter,
      IShowSessionListUseCase sessionList, EditSessionRequestHandler editSessionRequestHandler, 
      EditSessionResponsePresenter editPresenter, ShowSessionRequestHandler showSessionRequestHandler, 
      ShowSessionResponsePresenter showPresenter, ShowSessionListRequestHandler showSessionListRequestHandler,
      ShowSessionListResponsePresenter showSessionListResponsePresenter,
      DisplayImportSessionDataPageRequestHandler displayImportSessionDataPageRequestHandler,
      DisplayImportSessionDataPageResponsePresenter displayImportSessionDataPageResponsePresenter,
      ImportSessionDataRequestHandler importSessionDataRequestHandler,
      ImportSessionDataResponsePresenter importSessionDataResponsePresenter,
      GetUserAssociationRequestHandler getUserAssociationRequestHandler) : base(getUserAssociationRequestHandler)
    {
      _addSessionRequestHandler = addSessionRequestHandler;
      _presenter = presenter;
      _editSessionRequestHandler = editSessionRequestHandler;
      _editSessionPresenter = editPresenter;
      _showSessionRequestHandler = showSessionRequestHandler;
      _showPresenter = showPresenter;
      _showSessionListRequestHandler = showSessionListRequestHandler;
      _showSessionListResponsePresenter = showSessionListResponsePresenter;
      _displayImportSessionDataPageRequestHandler = displayImportSessionDataPageRequestHandler;
      _displayImportSessionDataPageResponsePresenter = displayImportSessionDataPageResponsePresenter;
      _importSessionDataRequestHandler = importSessionDataRequestHandler;
      _importSessionDataResponsePresenter = importSessionDataResponsePresenter;
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Session, (int)Feature.SessionEnum.NewSession)]
    [HttpGet]
    public ActionResult AddSession(ObjectTypeEnum sessionType, long objectId)
    {
      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();
      return View(new AddSessionViewModel
      {
        SessionType = sessionType,
        ObjectId = objectId,
        LoginName = userAssociationMessage.LoginName,
        AssociatedWith = userAssociationMessage.AssociatedWith,
        LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId,
        LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId,
        LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId,
        RoleFeatures = userAssociationMessage.RoleFeatures,
        InstituteBanner = userAssociationMessage.InstituteBanner
      });
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Session, (int)Feature.SessionEnum.NewSession)]
    [HttpPost]
    public ActionResult AddSession(AddSessionViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new AddSessionRequestMessage();

      requestMessage.SessionName = model.SessionName;
      requestMessage.StartDate = model.StartDate;
      requestMessage.EndDate = model.EndDate;
      requestMessage.SessionType = model.SessionType;
      requestMessage.ObjectId = model.ObjectId;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _addSessionRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _presenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());

      if (!response.Result.ValidationResult.IsValid)
        return View(viewModel);

      switch (response.Result.ObjectType)
      {
        case ObjectTypeEnum.Branch:
          return RedirectToAction("ViewBranch","Branch",new {branchId = response.Result.ObjectId, activeTab = TabEnum.Sessions});
        case ObjectTypeEnum.Medium:
          return RedirectToAction("ViewMedium","Medium",new {id = response.Result.ObjectId, activeTab = TabEnum.Sessions});
        case ObjectTypeEnum.Institute:
          return RedirectToAction("ViewInstitute","Institute",new {instituteId = response.Result.ObjectId, activeTab = TabEnum.Sessions});
        case ObjectTypeEnum.BranchMedium:
          return Redirect("/BranchMedium/ViewBranchMedium?branchMediumId=" + response.Result.ObjectId + "#tab_session");
        default:
          return RedirectToAction("ViewSession", new {id = response.Result.SessionId});
      }
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Session, (int)Feature.SessionEnum.ViewSession)]
    public ActionResult Index(ObjectTypeEnum? sessionType, long? objectId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowSessionListRequestMessage();
      var model = new ShowSessionListViewModel();

      var response = _showSessionListRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _showSessionListResponsePresenter.Handle(response.Result, model, GetUserAssociationResponseMessage());
      
      switch (sessionType)
      {
        case ObjectTypeEnum.Branch:
          return RedirectToAction("ViewBranch","Branch",new {branchId = objectId});
        case ObjectTypeEnum.Institute:
          return RedirectToAction("ViewInstitute","Institute",new {instituteId = objectId});
        case ObjectTypeEnum.Medium:
          return RedirectToAction("ViewMedium","Medium",new {id = objectId});
        case ObjectTypeEnum.BranchMedium:
          return RedirectToAction("ViewBranchMedium","BranchMedium",new {branchMediumId = objectId});
      }

      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Session, (int)Feature.SessionEnum.FindSession)]
    public ActionResult ViewSession(long id, long instituteId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ViewSessionRequestMessage();
      var model = new SessionViewModel();
      requestMessage.SessionId = id;
      requestMessage.InstituteId = instituteId;

      var response = _showSessionRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _showPresenter.Handle(response.Result, model, GetUserAssociationResponseMessage());

      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Session, (int)Feature.SessionEnum.EditSession)]
    [HttpGet]
    public ActionResult UpdateSession(long id)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ViewSessionRequestMessage();
      var model = new SessionViewModel();
      requestMessage.SessionId = id;

      var response = _showSessionRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _showPresenter.Handle(response.Result, model, GetUserAssociationResponseMessage());

      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Session, (int)Feature.SessionEnum.EditSession)]
    [HttpPost]
    public ActionResult UpdateSession(SessionViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new EditSessionRequestMessage();

      requestMessage.SessionId = model.Id;
      requestMessage.SessionName = model.SessionName;
      requestMessage.StartDate = model.StartDate;
      requestMessage.EndDate = model.EndDate;
      requestMessage.Status = model.Status;
      requestMessage.IsSessionClosed = model.IsSessionClosed;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _editSessionRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _editSessionPresenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());

      if (!response.Result.ValidationResult.IsValid)
        return View(viewModel);

      return RedirectToAction("ViewSession", new {id = model.Id});
    }
    
    [HttpGet]
    public ActionResult CancelAddSession(ObjectTypeEnum sessionType, long objectId)
    {
      switch (sessionType)
      {
        case ObjectTypeEnum.Branch:
          return RedirectToAction("ViewBranch","Branch",new {branchId = objectId, activeTab = TabEnum.Sessions});
        case ObjectTypeEnum.Medium:
          return RedirectToAction("ViewMedium","Medium",new {id = objectId, activeTab = TabEnum.Sessions});
        case ObjectTypeEnum.Institute:
          return RedirectToAction("ViewInstitute","Institute",new {instituteId = objectId, activeTab = TabEnum.Sessions});
        case ObjectTypeEnum.BranchMedium:
          return RedirectToAction("ViewBranchMedium","BranchMedium",new {branchMediumId = objectId, activeTab = TabEnum.Sessions});
        default:
          return RedirectToAction("Index", "Session");
      }
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.BranchMedium, (int) Feature.BranchMediumEnum.ImportData)]
    [HttpGet]
    public ActionResult ImportSessionData(long branchMediumId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new DisplayImportSessionDataPageRequestMessage
      {
        BranchMediumId = branchMediumId
      };


      var model = new ImportSessionDataViewModel();

      var response = _displayImportSessionDataPageRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _displayImportSessionDataPageResponsePresenter.Handle(response.Result, model, ModelState,
        GetUserAssociationResponseMessage());
      viewModel.BranchMediumId = branchMediumId;
      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.BranchMedium, (int)Feature.BranchMediumEnum.ImportData)]
    [HttpPost]
    public ActionResult ImportSessionData(ImportSessionDataViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ImportSessionDataRequestMessage();

      requestMessage.OnlySectionInfo = model.OnlySectionInfo;
      requestMessage.SectionInfoWithClassRoutine = model.SectionInfoWithClassRoutine;
      requestMessage.OnlyExamTerm = model.OnlyExamTerm;
      requestMessage.ExamTermWithExamConfiguration = model.ExamTermWithExamConfiguration;
      requestMessage.PreviousSessionId = model.PreviousSessionId;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _importSessionDataRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _importSessionDataResponsePresenter.Handle(response.Result, model, ModelState,
        GetUserAssociationResponseMessage());

      if (!response.Result.ValidationResult.IsValid)
      {
        viewModel.BranchMediumId = model.BranchMediumId;
        return View(viewModel);
      }

      return RedirectToAction("ViewSession",
        new {ssessionType = ObjectTypeEnum.BranchMedium, objectId = model.CurrentSession.SessionId} );
    }
    public ActionResult ImportSessionDataReport(ImportSessionDataViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ImportSessionDataRequestMessage();

      requestMessage.OnlySectionInfo = model.OnlySectionInfo;
      requestMessage.SectionInfoWithClassRoutine = model.SectionInfoWithClassRoutine;
      requestMessage.OnlyExamTerm = model.OnlyExamTerm;
      requestMessage.ExamTermWithExamConfiguration = model.ExamTermWithExamConfiguration;
      requestMessage.PreviousSessionId = model.PreviousSessionId;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _importSessionDataRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _importSessionDataResponsePresenter.Handle(response.Result, model, ModelState,
        GetUserAssociationResponseMessage());

      if (!response.Result.ValidationResult.IsValid)
        return View(viewModel);

      return RedirectToAction("ViewSession",
        new { ssessionType = ObjectTypeEnum.BranchMedium, objectId = model.CurrentSession.SessionId });
    }
  }
}