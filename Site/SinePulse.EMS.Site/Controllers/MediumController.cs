using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SinePulse.EMS.Domain.Features;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.MediumMessages;
using SinePulse.EMS.Site.Authorization;
using SinePulse.EMS.Site.Models;
using SinePulse.EMS.Site.Presenters;
using SinePulse.EMS.UseCases.Authorization;
using SinePulse.EMS.UseCases.Mediums;

namespace SinePulse.EMS.Site.Controllers
{
  [Authorize]
  public class MediumController : BaseController
  {
    private readonly AddMediumRequestHandler _addMediumRequestHandler;
    private readonly AddMediumResponsePresenter _presenter;
    private readonly ShowMediumListRequestHandler _showMediumListRequestHandler;
    private readonly ShowMediumListResponsePresenter _showMediumListPresenter;
    private readonly ShowMediumRequestHandler _showMediumRequestHandler;
    private readonly ShowMediumResponsePresenter _showMediumResponsePresenter;
    private readonly EditMediumRequestHandler _editMediumRequestHandler;
    private readonly EditMediumResponsePresenter _editMediumResponsePresenter;

    public MediumController(AddMediumRequestHandler addMediumRequestHandler, AddMediumResponsePresenter presenter,
      ShowMediumListRequestHandler showMediumListRequestHandler,
      ShowMediumListResponsePresenter showMediumListPresenter, ShowMediumRequestHandler showMediumRequestHandler,
      ShowMediumResponsePresenter showMediumResponsePresenter, EditMediumRequestHandler editMediumRequestHandler,
      EditMediumResponsePresenter editMediumResponsePresenter,
      GetUserAssociationRequestHandler getUserAssociationRequestHandler) : base(getUserAssociationRequestHandler)
    {
      _addMediumRequestHandler = addMediumRequestHandler;
      _presenter = presenter;
      _showMediumListRequestHandler = showMediumListRequestHandler;
      _showMediumListPresenter = showMediumListPresenter;
      _showMediumRequestHandler = showMediumRequestHandler;
      _showMediumResponsePresenter = showMediumResponsePresenter;
      _editMediumRequestHandler = editMediumRequestHandler;
      _editMediumResponsePresenter = editMediumResponsePresenter;
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.AcademicSetup, (int)Feature.AcademicSetupEnum.NewMedium)]
    [HttpGet]
    public ActionResult AddMedium()
    {
      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();
      var viewModel = new AddMediumViewModel();
      viewModel.LoginName = userAssociationMessage.LoginName;
      viewModel.LoginImageUrl = userAssociationMessage.ImageUrl;
      viewModel.AssociatedWith = userAssociationMessage.AssociatedWith;
      viewModel.LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId;
      viewModel.LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId;
      viewModel.LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId;
      viewModel.RoleFeatures = userAssociationMessage.RoleFeatures;
      viewModel.InstituteBanner = userAssociationMessage.InstituteBanner;
      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.AcademicSetup, (int)Feature.AcademicSetupEnum.NewMedium)]
    [HttpPost]
    public ActionResult AddMedium(AddMediumViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new AddMediumRequestMessage();

      requestMessage.MediumName = model.MediumName;
      requestMessage.MediumCode = model.MediumCode;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _addMediumRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _presenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());

      if (response.Result.ValidationResult.IsValid)
        return RedirectToAction("Index");
      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.AcademicSetup, (int)Feature.AcademicSetupEnum.ViewMedium)]
    public ActionResult Index()
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowMediumListRequestMessage();
      var model = new ShowMediumListViewModel();

      var response = _showMediumListRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _showMediumListPresenter.Handle(response.Result, model, GetUserAssociationResponseMessage());

      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.AcademicSetup, (int)Feature.AcademicSetupEnum.EditMedium)]
    [HttpGet]
    public ActionResult UpdateMedium(long id)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowMediumRequestMessage();
      var model = new MediumViewModel();
      requestMessage.MediumId = id;

      var response = _showMediumRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _showMediumResponsePresenter.Handle(response.Result, model, GetUserAssociationResponseMessage());

      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.AcademicSetup, (int)Feature.AcademicSetupEnum.EditMedium)]
    [HttpPost]
    public ActionResult UpdateMedium(MediumViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new EditMediumRequestMessage();

      requestMessage.MediumId = model.MediumId;
      requestMessage.MediumName = model.MediumName;
      requestMessage.MediumCode = model.MediumCode;
      requestMessage.Status = (Domain.Enums.StatusEnum)model.Status;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _editMediumRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _editMediumResponsePresenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());

      if (!response.Result.ValidationResult.IsValid)
        return View(viewModel);

      return RedirectToAction("Index");
    }
  }
}