using System.Collections.Generic;
using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Features;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.BranchMediumMessages;
using SinePulse.EMS.Messages.BranchMessages;
using SinePulse.EMS.Site.Authorization;
using SinePulse.EMS.Site.Models;
using SinePulse.EMS.Site.Presenters;
using SinePulse.EMS.UseCases.Authorization;
using SinePulse.EMS.UseCases.Branches;
using SinePulse.EMS.UseCases.BranchMediums;

namespace SinePulse.EMS.Site.Controllers
{
  [Authorize]
  public class BranchController : BaseController
  {
    private readonly AddBranchRequestHandler _addBranchRequestHandler;
    private readonly AddBranchResponsePresenter _presenter;
    private readonly ShowBranchRequestHandler _showBranchRequestHandler;
    private readonly ShowBranchResponsePresenter _showBranchPresenter;
    private readonly EditBranchRequestHandler _editBranchRequestHandler;
    private readonly EditBranchResponsePresenter _editBranchResponsePresenter;
    private readonly ShowBranchMediumListRequestHandler _showBranchMediumListRequestHandler;

    public BranchController(AddBranchRequestHandler addBranchRequestHandler, AddBranchResponsePresenter presenter, 
      ShowBranchRequestHandler showBranchRequestHandler, ShowBranchResponsePresenter showBranchPresenter, 
      EditBranchRequestHandler editBranchRequestHandler, EditBranchResponsePresenter editBranchResponsePresenter, 
      ShowBranchMediumListRequestHandler showBranchMediumListRequestHandler,
      GetUserAssociationRequestHandler getUserAssociationRequestHandler) : base(getUserAssociationRequestHandler)
    {
      _addBranchRequestHandler = addBranchRequestHandler;
      _presenter = presenter;
      _showBranchRequestHandler = showBranchRequestHandler;
      _showBranchPresenter = showBranchPresenter;
      _editBranchRequestHandler = editBranchRequestHandler;
      _editBranchResponsePresenter = editBranchResponsePresenter;
      _showBranchMediumListRequestHandler = showBranchMediumListRequestHandler;
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Branch, (int)Feature.BranchEnum.AddBranch)]
    [HttpGet]
    public ActionResult AddBranch(long instituteId)
    {
      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();
      var model = new AddBranchViewModel();
      model.InstituteId = instituteId;
      model.LoginName = userAssociationMessage.LoginName;
      model.AssociatedWith = userAssociationMessage.AssociatedWith;
      model.LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId;
      model.LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId;
      model.LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId;
      model.RoleFeatures = userAssociationMessage.RoleFeatures;
      model.InstituteBanner = userAssociationMessage.InstituteBanner;
      model.LoginUsersInstituteId = instituteId;
      return View(model);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Branch, (int)Feature.BranchEnum.AddBranch)]
    [HttpPost]
    public ActionResult AddBranch(AddBranchViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new AddBranchRequestMessage();

      requestMessage.BranchName = model.BranchName;
      requestMessage.BranchCode = model.BranchCode;
      requestMessage.MobileNo = model.MobileNo;
      requestMessage.Email = model.Email;
      requestMessage.Fax = model.Fax;
      requestMessage.Status = model.Status;
      requestMessage.InstituteId = model.InstituteId;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _addBranchRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _presenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());
      if (response.Result.BranchId > 0)
      {
        return RedirectToAction("ViewBranch", new { branchId = response.Result.BranchId });
      }
      else
        return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Branch, (int)Feature.BranchEnum.ViewBranch)]
    public ActionResult ViewBranch(long branchId, TabEnum activeTab)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowBranchRequestMessage();
      requestMessage.BranchId = branchId;
      ShowBranchViewModel model = new ShowBranchViewModel();
      var response = _showBranchRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _showBranchPresenter.Handle(response.Result, model, GetUserAssociationResponseMessage());
      var getBranchMediumListRequestMessage = new ShowBranchMediumListRequestMessage{BranchId = branchId};
      var branchMediumListResponse = _showBranchMediumListRequestHandler.Handle(getBranchMediumListRequestMessage, cancellationToken);
      viewModel.InstituteSessions = response.Result.Sessions;
      viewModel.BranchMediums = branchMediumListResponse.Result.BranchMediumList;
      viewModel.ActiveTab = activeTab;
      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Branch, (int)Feature.BranchEnum.EditBranch)]
    public ActionResult UpdateBranch(long branchId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowBranchRequestMessage();
      requestMessage.BranchId = branchId;

      ShowBranchViewModel model = new ShowBranchViewModel();
      var response = _showBranchRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _showBranchPresenter.Handle(response.Result, model, GetUserAssociationResponseMessage());

      var editViewModel = new EditBranchViewModel();
      editViewModel.LoginName = viewModel.LoginName;
      editViewModel.AssociatedWith = viewModel.AssociatedWith;
      editViewModel.LoginUsersInstituteId = viewModel.LoginUsersInstituteId;
      editViewModel.LoginUsersBranchId = viewModel.LoginUsersBranchId;
      editViewModel.LoginUsersBranchMediumId = viewModel.LoginUsersBranchMediumId;
      editViewModel.RoleFeatures = viewModel.RoleFeatures;

      editViewModel.BranchName = viewModel.Branch.BranchName;
      editViewModel.BranchCode = viewModel.Branch.BranchCode;
      editViewModel.MobileNo = viewModel.Branch.MobileNo;
      editViewModel.Email = viewModel.Branch.Email;
      editViewModel.Fax = viewModel.Branch.Fax;
      editViewModel.Status = viewModel.Branch.Status;
      editViewModel.LoginUsersInstituteId = viewModel.Branch.Institute.Id;
      editViewModel.Id = viewModel.Branch.Id;
      return View(editViewModel);
    }
    
    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Branch, (int)Feature.BranchEnum.EditBranch)]
    [HttpPost]
    public ActionResult UpdateBranch(EditBranchViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new EditBranchRequestMessage();

      requestMessage.Id = model.Id;
      requestMessage.BranchName = model.BranchName;
      requestMessage.BranchCode = model.BranchCode;
      requestMessage.MobileNo = model.MobileNo;
      requestMessage.Email = model.Email;
      requestMessage.Fax = model.Fax;
      requestMessage.MapIFrame = model.MapIFrame;
      requestMessage.Status = model.Status;
      requestMessage.InstituteId = (long)model.LoginUsersInstituteId;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _editBranchRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _editBranchResponsePresenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());
      if (response.Result.ValidationResult.IsValid)
      {
        return RedirectToAction("ViewBranch", new { branchId = model.Id });
      }
      else
        return View(viewModel);
    }
    
    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Branch, (int)Feature.BranchEnum.ViewBranch)]
    public IActionResult Index()
    {
      return View();
    }

    public IEnumerable<BranchMedium> GetMediums(long branchId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowBranchRequestMessage();

      requestMessage.BranchId = branchId;

      var response = _showBranchRequestHandler.Handle(requestMessage, cancellationToken);
      if (response.Result.Branch != null)
        return response.Result.Branch.Mediums;
      else
        return new List<BranchMedium>();
    }
  } 
}