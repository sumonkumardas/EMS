using System.Collections.Generic;
using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SinePulse.EMS.Domain.Features;
using SinePulse.EMS.Messages.ResultGradeMessages;
using SinePulse.EMS.Site.Authorization;
using SinePulse.EMS.Site.Models;
using SinePulse.EMS.Site.Presenters;
using SinePulse.EMS.UseCases.Authorization;
using SinePulse.EMS.UseCases.ResultGrades;

namespace SinePulse.EMS.Site.Controllers
{
  [Authorize]
  public class ResultGradeController : BaseController
  {
    private readonly DisplayAddResultGradePageRequestHandler _displayAddResultGradePageRequestHandler;
    private readonly DisplayAddResultGradePageResponsePresenter _displayAddResultGradePageResponsePresenter;
    private readonly AddResultGradeRequestHandler _addResultGradeRequestHandler;
    private readonly AddResultGradeResponsePresenter _presenter;
    private readonly ShowResultGradeRequestHandler _showResultGradeRequestHandler;
    private readonly ShowResultGradeResponsePresenter _showResultGradeResponsePresenter;
    private readonly EditResultGradeRequestHandler _editResultGradeRequestHandler;
    private readonly EditResultGradeResponsePresenter _editResultGradeResponsePresenter;
    private readonly ShowResultGradeListRequestHandler _showResultGradeListRequestHandler;
    private readonly ShowResultGradeListResponsePresenter _showResultGradeListResponsePresenter;

    public ResultGradeController(
      DisplayAddResultGradePageRequestHandler displayAddResultGradePageRequestHandler,
      DisplayAddResultGradePageResponsePresenter displayAddResultGradePageResponsePresenter,
      AddResultGradeRequestHandler addResultGradeRequestHandler,
      AddResultGradeResponsePresenter presenter,
      ShowResultGradeRequestHandler showResultGradeRequestHandler,
      ShowResultGradeResponsePresenter showResultGradeResponsePresenter,
      EditResultGradeRequestHandler editResultGradeRequestHandler,
      EditResultGradeResponsePresenter editResultGradeResponsePresenter,
      ShowResultGradeListRequestHandler showResultGradeListRequestHandler,
      ShowResultGradeListResponsePresenter showResultGradeListResponsePresenter,
      GetUserAssociationRequestHandler getUserAssociationRequestHandler) : base(getUserAssociationRequestHandler)
    {
      _displayAddResultGradePageRequestHandler = displayAddResultGradePageRequestHandler;
      _displayAddResultGradePageResponsePresenter = displayAddResultGradePageResponsePresenter;
      _addResultGradeRequestHandler = addResultGradeRequestHandler;
      _presenter = presenter;
      _showResultGradeRequestHandler = showResultGradeRequestHandler;
      _showResultGradeResponsePresenter = showResultGradeResponsePresenter;
      _editResultGradeRequestHandler = editResultGradeRequestHandler;
      _editResultGradeResponsePresenter = editResultGradeResponsePresenter;
      _showResultGradeListRequestHandler = showResultGradeListRequestHandler;
      _showResultGradeListResponsePresenter = showResultGradeListResponsePresenter;
    }


    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Examination, (int)Feature.ExaminationEnum.AddResultGrade)]
    [HttpGet]
    public ActionResult AddResultGrade(long branchMediumId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new DisplayAddResultGradePageRequestMessage
      {
        BranchMediumId = branchMediumId
      };

      var model = new AddResultGradeViewModel
      {
        BranchMediumId = branchMediumId
      };

      var response = _displayAddResultGradePageRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _displayAddResultGradePageResponsePresenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());
      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Examination, (int)Feature.ExaminationEnum.AddResultGrade)]
    [HttpPost]
    public ActionResult AddResultGrade(AddResultGradeViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new AddResultGradeRequestMessage();

      requestMessage.GradeLetter = model.GradeLetter;
      requestMessage.GradePoint = model.GradePoint;
      requestMessage.StartMark = model.StartMark;
      requestMessage.EndMark = model.EndMark;
      requestMessage.BranchMediumId = model.BranchMediumId;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _addResultGradeRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _presenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());
      if (!response.Result.ValidationResult.IsValid)
      {
        viewModel.BranchMediumId = model.BranchMediumId;
        return View(viewModel);
      }
      return Redirect("/BranchMedium/ViewBranchMedium?branchMediumId=" + model.BranchMediumId + "#tab_resultGrade");
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Examination, (int)Feature.ExaminationEnum.FindResultGrade)]
    public ActionResult ViewResultGrade(long resultGradeId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowResultGradeRequestMessage { ResultGradeId = resultGradeId };
      var response = _showResultGradeRequestHandler.Handle(requestMessage, cancellationToken);
      var model = new ResultGradeViewModel();
      var viewModel = _showResultGradeResponsePresenter.Handle(response.Result.Data, model, GetUserAssociationResponseMessage());
      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Examination, (int)Feature.ExaminationEnum.EditResultGrade)]
    [HttpGet]
    public ActionResult UpdateResultGrade(long resultGradeId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowResultGradeRequestMessage();
      requestMessage.ResultGradeId = resultGradeId;

      var response = _showResultGradeRequestHandler.Handle(requestMessage, cancellationToken);
      var model = new ResultGradeViewModel();
      var viewModel = _showResultGradeResponsePresenter.Handle(response.Result.Data, model, GetUserAssociationResponseMessage());
      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Examination, (int)Feature.ExaminationEnum.EditResultGrade)]
    [HttpPost]
    public ActionResult UpdateResultGrade(ResultGradeViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new EditResultGradeRequestMessage();

      requestMessage.Id = model.Id;
      requestMessage.GradeLetter = model.GradeLetter;
      requestMessage.GradePoint = model.GradePoint;
      requestMessage.StartMark = model.StartMark;
      requestMessage.EndMark = model.EndMark;
      requestMessage.BranchMediumId = model.BranchMedium.Id;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _editResultGradeRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _editResultGradeResponsePresenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());
      if (!response.Result.ValidationResult.IsValid)
      {
        return View(viewModel);
      }
      return Redirect("/BranchMedium/ViewBranchMedium?branchMediumId=" + model.BranchMedium.Id+ "#tab_resultGrade");
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Examination, (int)Feature.ExaminationEnum.ViewResultGrade)]
    public ActionResult Index(long branchMediumId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowResultGradeListRequestMessage();
      requestMessage.BranchMediumId = branchMediumId;
      ResultGradeListViewModel model = new ResultGradeListViewModel();


      var response = _showResultGradeListRequestHandler.Handle(requestMessage, cancellationToken);
      var responseMessage = new ShowResultGradeListResponseMessage(response.Result.Data.ResultGradeList);
      var viewModel = _showResultGradeListResponsePresenter.Handle(responseMessage, model, GetUserAssociationResponseMessage());
      viewModel.BranchMediumId = branchMediumId;

      return PartialView(viewModel);
    }
  }
}