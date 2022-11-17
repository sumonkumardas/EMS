using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SinePulse.EMS.Domain.Features;
using SinePulse.EMS.Messages.SeatPlanMessages;
using SinePulse.EMS.Site.Authorization;
using SinePulse.EMS.Site.Models;
using SinePulse.EMS.Site.Presenters;
using SinePulse.EMS.UseCases.Authorization;
using SinePulse.EMS.UseCases.SeatPlans;

namespace SinePulse.EMS.Site.Controllers
{
  [Authorize]
  public class SeatPlanController : BaseController
  {
    private readonly DisplayAddSeatPlanPageRequestHandler _displayAddSeatPlanPageRequestHandler;
    private readonly DisplayAddSeatPlanPageResponsePresenter _displayAddSeatPlanPageResponsePresenter;
    private readonly AddSeatPlanRequestHandler _addSeatPlanRequestHandler;
    private readonly AddSeatPlanResponsePresenter _presenter;
    private readonly DisplayEditSeatPlanPageRequestHandler _displayEditSeatPlanPageRequestHandler;
    private readonly DisplayEditSeatPlanPageResponsePresenter _displayEditSeatPlanPageResponsePresenter;
    private readonly EditSeatPlanRequestHandler _editSeatPlanRequestHandler;
    private readonly EditSeatPlanResponsePresenter _editSeatPlanResponsePresenter;
    private readonly ShowSeatPlanRequestHandler _showSeatPlanRequestHandler;
    private readonly ShowSeatPlanResponsePresenter _showSeatPlanResponsePresenter;
    private readonly ShowSeatPlanListRequestHandler _showSeatPlanListRequestHandler;
    private readonly ShowSeatPlanListResponsePresenter _showSeatPlanListResponsePresenter;

    public SeatPlanController(
      DisplayAddSeatPlanPageRequestHandler displayAddSeatPlanPageRequestHandler,
      DisplayAddSeatPlanPageResponsePresenter displayAddSeatPlanPageResponsePresenter,
      AddSeatPlanRequestHandler addSeatPlanRequestHandler,
      AddSeatPlanResponsePresenter presenter,
      DisplayEditSeatPlanPageRequestHandler displayEditSeatPlanPageRequestHandler,
      DisplayEditSeatPlanPageResponsePresenter displayEditSeatPlanPageResponsePresenter,
      EditSeatPlanRequestHandler editSeatPlanRequestHandler,
      EditSeatPlanResponsePresenter editSeatPlanResponsePresenter,
      ShowSeatPlanRequestHandler showSeatPlanRequestHandler,
      ShowSeatPlanResponsePresenter showSeatPlanResponsePresenter,
      GetUserAssociationRequestHandler getUserAssociationRequestHandler,
      ShowSeatPlanListRequestHandler showSeatPlanListRequestHandler,
      ShowSeatPlanListResponsePresenter showSeatPlanListResponsePresenter) : base(getUserAssociationRequestHandler)
    {
      _displayAddSeatPlanPageRequestHandler = displayAddSeatPlanPageRequestHandler;
      _displayAddSeatPlanPageResponsePresenter = displayAddSeatPlanPageResponsePresenter;
      _addSeatPlanRequestHandler = addSeatPlanRequestHandler;
      _presenter = presenter;
      _displayEditSeatPlanPageRequestHandler = displayEditSeatPlanPageRequestHandler;
      _displayEditSeatPlanPageResponsePresenter = displayEditSeatPlanPageResponsePresenter;
      _editSeatPlanRequestHandler = editSeatPlanRequestHandler;
      _editSeatPlanResponsePresenter = editSeatPlanResponsePresenter;
      _showSeatPlanRequestHandler = showSeatPlanRequestHandler;
      _showSeatPlanResponsePresenter = showSeatPlanResponsePresenter;
      _showSeatPlanListRequestHandler = showSeatPlanListRequestHandler;
      _showSeatPlanListResponsePresenter = showSeatPlanListResponsePresenter;
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Examination, (int)Feature.ExaminationEnum.ViewSeatPlan)]
    public IActionResult Index()
    {
      return View();
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Examination, (int)Feature.ExaminationEnum.AddSeatPlan)]
    [HttpGet]
    public ActionResult AddSeatPlan(long testId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new DisplayAddSeatPlanPageRequestMessage
      {
        TestId = testId
      };

      var model = new AddSeatPlanViewModel
      {
        TestId = testId
      };

      var response = _displayAddSeatPlanPageRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _displayAddSeatPlanPageResponsePresenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());

      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Examination, (int)Feature.ExaminationEnum.AddSeatPlan)]
    [HttpPost]
    public ActionResult AddSeatPlan(AddSeatPlanViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new AddSeatPlanRequestMessage();

      requestMessage.RollRange = model.RollRange;
      requestMessage.TotalStudent = model.TotalStudent;
      requestMessage.TestId = model.TestId;
      requestMessage.RoomId = model.RoomId;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _addSeatPlanRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _presenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());
      if (!response.Result.ValidationResult.IsValid)
      {
        return View(viewModel);
      }
      return Redirect("/Term/ViewTerm?termId=" + model.TermId + "#tab_seatPlan");
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Examination, (int)Feature.ExaminationEnum.FindSeatPlan)]
    public ActionResult ViewSeatPlan(long seatPlanId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowSeatPlanRequestMessage {SeatPlanId = seatPlanId};
      var response = _showSeatPlanRequestHandler.Handle(requestMessage, cancellationToken);
      var model = new SeatPlanViewModel();
      var viewModel = _showSeatPlanResponsePresenter.Handle(response.Result.Data, model, GetUserAssociationResponseMessage());
      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Examination, (int)Feature.ExaminationEnum.EditSeatPlan)]
    [HttpGet]
    public ActionResult EditSeatPlan(long seatPlanId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new DisplayEditSeatPlanPageRequestMessage
      {
        SeatPlanId = seatPlanId
      };

      var model = new EditSeatPlanViewModel
      {
        SeatPlanId = seatPlanId
      };

      var response = _displayEditSeatPlanPageRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _displayEditSeatPlanPageResponsePresenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());

      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Examination, (int)Feature.ExaminationEnum.EditSeatPlan)]
    [HttpPost]
    public ActionResult EditSeatPlan(EditSeatPlanViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new EditSeatPlanRequestMessage();

      requestMessage.SeatPlanId = model.SeatPlanId;
      requestMessage.RollRange = model.RollRange;
      requestMessage.TotalStudent = model.TotalStudent;
      requestMessage.TestId = model.TestId;
      requestMessage.RoomId = model.RoomId;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _editSeatPlanRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _editSeatPlanResponsePresenter.Handle(response.Result, model, ModelState,
        GetUserAssociationResponseMessage());
      if (!response.Result.ValidationResult.IsValid)
      {
        return View(viewModel);
      }

      return Redirect("/Term/ViewTerm?termId=" + model.TermId + "#tab_seatPlan");
    }
    public ActionResult ShowSeatPlanList(long termId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowSeatPlanListRequestMessage { TermId = termId };

      var response = _showSeatPlanListRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _showSeatPlanListResponsePresenter.Handle(response.Result.Data, new ShowSeatPlanListViewModel(),
        GetUserAssociationResponseMessage());

      return PartialView("~/Views/SeatPlan/ShowSeatPlanList.cshtml", viewModel);
    }
  }
}