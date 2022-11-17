using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SinePulse.EMS.Domain.Features;
using SinePulse.EMS.Messages.SalaryComponentMessages;
using SinePulse.EMS.Site.Authorization;
using SinePulse.EMS.Site.Models;
using SinePulse.EMS.Site.Presenters;
using SinePulse.EMS.UseCases.Authorization;
using SinePulse.EMS.UseCases.SalaryComponents;

namespace SinePulse.EMS.Site.Controllers
{
  [Authorize]
  public class SalaryComponentController : BaseController
  {
    private readonly ShowSalaryComponentListRequestHandler _showSalaryComponentListRequestHandler;
    private readonly ShowSalaryComponentListResponsePresenter _showSalaryComponentListResponsePresenter;
    private readonly DisplayAddSalaryComponentRequestHandler _displayAddSalaryComponentRequestHandler;
    private readonly DisplayAddSalaryComponentResponsePresenter _displayAddSalaryComponentResponsePresenter;
    private readonly AddSalaryComponentRequestHandler _addSalaryComponentRequestHandler;
    private readonly AddSalaryComponentResponsePresenter _addSalaryComponentResponsePresenter;
    private readonly DisplayEditSalaryComponentRequestHandler _displayEditSalaryComponentRequestHandler;
    private readonly DisplayEditSalaryComponentResponsePresenter _displayEditSalaryComponentResponsePresenter;
    private readonly EditSalaryComponentRequestHandler _editSalaryComponentRequestHandler;
    private readonly EditSalaryComponentResponsePresenter _editSalaryComponentResponsePresenter;
    public SalaryComponentController(ShowSalaryComponentListRequestHandler showSalaryComponentListRequestHandler,
      ShowSalaryComponentListResponsePresenter showSalaryComponentListResponsePresenter,
      DisplayAddSalaryComponentRequestHandler displayAddSalaryComponentRequestHandler,
      DisplayAddSalaryComponentResponsePresenter displayAddSalaryComponentResponsePresenter,
      AddSalaryComponentRequestHandler addSalaryComponentRequestHandler,
      AddSalaryComponentResponsePresenter addSalaryComponentResponsePresenter,
      DisplayEditSalaryComponentRequestHandler displayEditSalaryComponentRequestHandler,
      DisplayEditSalaryComponentResponsePresenter displayEditSalaryComponentResponsePresenter,
      EditSalaryComponentRequestHandler editSalaryComponentRequestHandler,
      EditSalaryComponentResponsePresenter editSalaryComponentResponsePresenter,
     GetUserAssociationRequestHandler getUserAssociationRequestHandler) : base(getUserAssociationRequestHandler)
    {
      _showSalaryComponentListRequestHandler = showSalaryComponentListRequestHandler;
      _showSalaryComponentListResponsePresenter = showSalaryComponentListResponsePresenter;
      _addSalaryComponentRequestHandler = addSalaryComponentRequestHandler;
      _addSalaryComponentResponsePresenter = addSalaryComponentResponsePresenter;
      _displayAddSalaryComponentRequestHandler = displayAddSalaryComponentRequestHandler;
      _displayAddSalaryComponentResponsePresenter = displayAddSalaryComponentResponsePresenter;
      _displayEditSalaryComponentRequestHandler = displayEditSalaryComponentRequestHandler;
      _displayEditSalaryComponentResponsePresenter = displayEditSalaryComponentResponsePresenter;
      _editSalaryComponentRequestHandler = editSalaryComponentRequestHandler;
      _editSalaryComponentResponsePresenter = editSalaryComponentResponsePresenter;
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.SuperAdminPayroll, (int)Feature.SuperAdminPayrollEnum.FindSalaryComponent)]
    public IActionResult Index()
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowSalaryComponentListRequestMessage();

      var response = _showSalaryComponentListRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _showSalaryComponentListResponsePresenter.Handle(response.Result.Data, new ShowSalaryComponentListViewModel(),
        GetUserAssociationResponseMessage());
      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.SuperAdminPayroll, (int)Feature.SuperAdminPayrollEnum.AddSalaryComponent)]
    [HttpGet]
    public ActionResult AddSalaryComponent()
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new DisplayAddSalaryComponentRequestMessage();

      var response = _displayAddSalaryComponentRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _displayAddSalaryComponentResponsePresenter.Handle(response.Result.Data, new AddSalaryComponentViewModel(),
        GetUserAssociationResponseMessage());
      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.SuperAdminPayroll, (int)Feature.SuperAdminPayrollEnum.AddSalaryComponent)]
    [HttpPost]
    public ActionResult AddSalaryComponent(AddSalaryComponentViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new AddSalaryComponentRequestMessage();

      requestMessage.ComponentName = model.ComponentName;
      requestMessage.ComponentTypeId = model.SalaryComponentTypeId;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _addSalaryComponentRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _addSalaryComponentResponsePresenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());

      if (!response.Result.ValidationResult.IsValid)
      {
        var displayRequestMessage = new DisplayAddSalaryComponentRequestMessage();
        var displayResponse = _displayAddSalaryComponentRequestHandler.Handle(displayRequestMessage, cancellationToken);
        displayResponse.Result.ValidationResult = response.Result.ValidationResult;
        var DisplayViewModel = _displayAddSalaryComponentResponsePresenter.Handle(displayResponse.Result.Data, new AddSalaryComponentViewModel(),
          GetUserAssociationResponseMessage());
        
        return View(DisplayViewModel);
      }
      return RedirectToAction("Index");
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.SuperAdminPayroll, (int)Feature.SuperAdminPayrollEnum.EditSalaryComponent)]
    [HttpGet]
    public ActionResult EditSalaryComponent(long salaryComponentId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new DisplayEditSalaryComponentRequestMessage
      {
        SalaryComponentId = salaryComponentId
      };
      var model = new EditSalaryComponentViewModel
      {
        SalaryComponentTypeId = salaryComponentId,
      };

      var response = _displayEditSalaryComponentRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _displayEditSalaryComponentResponsePresenter.Handle(response.Result.Data, model, GetUserAssociationResponseMessage());
      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.SuperAdminPayroll, (int)Feature.SuperAdminPayrollEnum.EditSalaryComponent)]
    [HttpPost]
    public ActionResult EditSalaryComponent(EditSalaryComponentViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new EditSalaryComponentRequestMessage();

      requestMessage.ComponentId = model.SalaryComponentId;
      requestMessage.ComponentName = model.ComponentName;
      requestMessage.ComponentTypeId = model.SalaryComponentTypeId;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _editSalaryComponentRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _editSalaryComponentResponsePresenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());
      if (!response.Result.ValidationResult.IsValid)
      {
        var displayRequestMessage = new DisplayEditSalaryComponentRequestMessage();
        var displayResponse = _displayEditSalaryComponentRequestHandler.Handle(displayRequestMessage, cancellationToken);
        displayResponse.Result.ValidationResult = response.Result.ValidationResult;
        var DisplayViewModel = _displayEditSalaryComponentResponsePresenter.Handle(displayResponse.Result.Data, new EditSalaryComponentViewModel(),
          GetUserAssociationResponseMessage());

        return View(DisplayViewModel);
      }
      return RedirectToAction("Index");
    }
  }
}