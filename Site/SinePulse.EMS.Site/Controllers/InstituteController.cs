using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Features;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.InstituteMessages;
using SinePulse.EMS.Site.Authorization;
using SinePulse.EMS.Site.Models;
using SinePulse.EMS.Site.Presenters;
using SinePulse.EMS.UseCases.Authorization;
using SinePulse.EMS.UseCases.Institutes;

namespace SinePulse.EMS.Site.Controllers
{
  [Authorize]
  public class InstituteController : BaseController
  {
    private readonly AddInstituteRequestHandler _addInstituteRequestHandler;
    private readonly AddInstituteResponsePresenter _presenter;
    private readonly ShowInstituteListRequestHandler _showInstituteListRequestHandler;
    private readonly ShowInstituteListResponsePresenter _showInstituteListResponsePresenter;
    private readonly ShowInstituteRequestHandler _showInstituteRequestHandler;
    private readonly ShowInstituteResponsePresenter _showInstitutePresenter;
    private readonly EditInstituteRequestHandler _editInstituteRequestHandler;
    private readonly EditInstituteResponsePresenter _editInstituteResponsePresenter;

    public InstituteController(AddInstituteRequestHandler addInstituteRequestHandler, AddInstituteResponsePresenter presenter, 
      ShowInstituteListRequestHandler showInstituteListRequestHandler, ShowInstituteListResponsePresenter showInstituteListResponsePresenter, 
      ShowInstituteRequestHandler showInstituteRequestHandler, ShowInstituteResponsePresenter showInstituteResponsePresenter, 
      EditInstituteRequestHandler editInstituteRequestHandler, EditInstituteResponsePresenter editInstituteResponsePresenter,
      GetUserAssociationRequestHandler getUserAssociationRequestHandler, IHostingEnvironment appEnvironment): base(getUserAssociationRequestHandler)
    {
      _addInstituteRequestHandler = addInstituteRequestHandler;
      _presenter = presenter;
      _showInstituteListRequestHandler = showInstituteListRequestHandler;
      _showInstituteListResponsePresenter = showInstituteListResponsePresenter;
      _showInstituteRequestHandler = showInstituteRequestHandler;
      _showInstitutePresenter = showInstituteResponsePresenter;
      _editInstituteRequestHandler = editInstituteRequestHandler;
      _editInstituteResponsePresenter = editInstituteResponsePresenter;
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Institute, (int)Feature.InstituteSuperAdminEnum.NewInstitute)]
    [HttpGet]
    public ActionResult AddInstitute()
    {
      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();
      var ViewModel = new AddInstituteViewModel
      {
        LoginName = userAssociationMessage.LoginName,
        AssociatedWith = userAssociationMessage.AssociatedWith,
        LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId,
        LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId,
        LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId,
        RoleFeatures = userAssociationMessage.RoleFeatures,
        InstituteBanner = userAssociationMessage.InstituteBanner
      };
      return View(ViewModel);
    }


    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Institute, (int)Feature.InstituteSuperAdminEnum.NewInstitute)]
    [HttpPost]
    public ActionResult AddInstitute(AddInstituteViewModel model)
    {
      var bannerImageFile = HttpContext.Request.Form.Files.FirstOrDefault();
      var banner = GetBannerImageBytes(bannerImageFile);
      var cancellationToken = new CancellationToken();
      var requestMessage = new AddInstituteRequestMessage();

      requestMessage.Banner = banner;
      requestMessage.Description = model.Description;
      requestMessage.Domain = model.Domain;
      requestMessage.Email = model.Email;
      requestMessage.FacebookPageURL = model.FacebookPageURL;
      requestMessage.Favicon = model.Favicon;
      requestMessage.Logo = model.Logo;
      requestMessage.OrganisationName = model.OrganisationName;
      requestMessage.ShortName = model.ShortName;
      requestMessage.Slogan = model.Slogan;
      requestMessage.WhyChooseInstitue = model.WhyChooseInstitue;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _addInstituteRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _presenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());
      if (response.Result.Id > 0)
      {
        return RedirectToAction("ViewInstitute", new {instituteId = response.Result.Id});
      }
      else
        return View(viewModel);
    }

    private byte[] GetBannerImageBytes(IFormFile bannerImageFile)
    {
      if (bannerImageFile == null)
      {
        return null;
      }
      var memoryStream = new MemoryStream(); 
      bannerImageFile.OpenReadStream().CopyTo(memoryStream);
      return memoryStream.ToArray();
    }


    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Institute, (int)Feature.InstituteEnum.ViewInstitute)]
    public ActionResult Index()
    {
      var cancellationToken = new CancellationToken();

      var requestMessage = new ShowInstituteListRequestMessage();
      ShowInstituteListViewModel model = new ShowInstituteListViewModel();

      var response = _showInstituteListRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _showInstituteListResponsePresenter.Handle(response.Result, model, GetUserAssociationResponseMessage());

      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Institute, (int)Feature.InstituteEnum.ViewInstitute)]
    public ActionResult ViewInstitute(long instituteId, TabEnum activeTab)
    {
      var cancellationToken = new CancellationToken();

      var requestMessage = new ShowInstituteRequestMessage();
      requestMessage.InstituteId = instituteId;
      ShowInstituteViewModel model = new ShowInstituteViewModel();
      var response = _showInstituteRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _showInstitutePresenter.Handle(response.Result, model, GetUserAssociationResponseMessage());
      viewModel.ActiveTab = activeTab;
      return View(viewModel);
    }


    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Institute, (int)Feature.InstituteSuperAdminEnum.EditInstitute)]
   [HttpGet]
    public ActionResult UpdateInstitute(long instituteId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowInstituteRequestMessage();
      requestMessage.InstituteId = instituteId;

      ShowInstituteViewModel model = new ShowInstituteViewModel();
      var response = _showInstituteRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _showInstitutePresenter.Handle(response.Result, model, GetUserAssociationResponseMessage());

      var editViewModel = new EditInstituteViewModel();
      editViewModel.LoginName = viewModel.LoginName;
      editViewModel.AssociatedWith = viewModel.AssociatedWith;
      editViewModel.LoginUsersInstituteId = viewModel.LoginUsersInstituteId;
      editViewModel.LoginUsersBranchId = viewModel.LoginUsersBranchId;
      editViewModel.LoginUsersBranchMediumId = viewModel.LoginUsersBranchMediumId;
      editViewModel.RoleFeatures = viewModel.RoleFeatures;
      editViewModel.Description = viewModel.Institute.Description;
      editViewModel.Domain = viewModel.Institute.Domain;
      editViewModel.Email = viewModel.Institute.Email;
      editViewModel.FacebookPageURL = viewModel.Institute.FacebookPageURL;
      editViewModel.Favicon = viewModel.Institute.Favicon;
      editViewModel.Id = viewModel.Institute.Id;
      editViewModel.Logo = viewModel.Institute.Logo;
      editViewModel.OrganisationName = viewModel.Institute.OrganisationName;
      editViewModel.ShortName = viewModel.Institute.ShortName;
      editViewModel.Slogan = viewModel.Institute.Slogan;
      editViewModel.WhyChooseInstitue = viewModel.Institute.WhyChooseInstitue;

      return View(editViewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Institute, (int)Feature.InstituteSuperAdminEnum.EditInstitute)]
    [HttpPost]
    public ActionResult UpdateInstitute(EditInstituteViewModel model)
    {
      var bannerImageFile = HttpContext.Request.Form.Files.FirstOrDefault();
      var banner = GetBannerImageBytes(bannerImageFile);
      var cancellationToken = new CancellationToken();
      var requestMessage = new EditInstituteRequestMessage();

      requestMessage.Id = model.Id;
      requestMessage.Banner = banner;
      requestMessage.Description = model.Description;
      requestMessage.Domain = model.Domain;
      requestMessage.Email = model.Email;
      requestMessage.FacebookPageURL = model.FacebookPageURL;
      requestMessage.Favicon = model.Favicon;
      requestMessage.Logo = model.Logo;
      requestMessage.OrganisationName = model.OrganisationName;
      requestMessage.ShortName = model.ShortName;
      requestMessage.Slogan = model.Slogan;
      requestMessage.WhyChooseInstitue = model.WhyChooseInstitue;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _editInstituteRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _editInstituteResponsePresenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());
      if (response.Result.ValidationResult.IsValid)
      {
        return RedirectToAction("ViewInstitute", new {instituteId = model.Id});
      }

      viewModel.Id = model.Id;
      return View(viewModel);
    }


    public ActionResult ViewMedium()
    {
      return View();
    }

    [HttpPost]
    public IEnumerable<Branch> GetBranches(long instituteId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowInstituteRequestMessage();

      requestMessage.InstituteId = instituteId;

      var response = _showInstituteRequestHandler.Handle(requestMessage, cancellationToken);
      if (response.Result.Institute != null)
        return response.Result.Institute.Branches;
      else
        return new List<Branch>();
    }
  }
}