using System.Collections.Generic;
using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Features;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.JobTypeMessages;
using SinePulse.EMS.Site.Authorization;
using SinePulse.EMS.Site.Models;
using SinePulse.EMS.Site.Presenters;
using SinePulse.EMS.UseCases.Authorization;
using SinePulse.EMS.UseCases.JobTypes;

namespace SinePulse.EMS.Site.Controllers
{
  [Authorize]
  public class JobTypeController : BaseController
  {
    private readonly AddJobTypeRequestHandler _requestHandler;
    private readonly AddJobTypeResponsePresenter _presenter;
    private readonly ShowJobTypeListRequestHandler _showJobTypeListRequestHandler;
    private readonly ShowJobTypeListResponsePresenter _showJobTypeListResponsePresenter;
    private readonly ShowJobTypeRequestHandler _showJobTypeRequestHandler;
    private readonly ShowJobTypeResponsePresenter _showJobTypeResponsePresenter;
    private readonly EditJobTypeRequestHandler _editJobTypeRequestHandler;
    private readonly EditJobTypeResponsePresenter _editJobTypeResponsePresenter;

    public JobTypeController(AddJobTypeRequestHandler requestHandler, AddJobTypeResponsePresenter presenter, 
      ShowJobTypeListRequestHandler showJobTypeListRequestHandler, 
      ShowJobTypeListResponsePresenter showJobTypeListResponsePresenter, 
      ShowJobTypeRequestHandler showJobTypeRequestHandler, 
      ShowJobTypeResponsePresenter showJobTypeResponsePresenter, 
      EditJobTypeRequestHandler editJobTypeRequestHandler, 
      EditJobTypeResponsePresenter editJobTypeResponsePresenter,
      GetUserAssociationRequestHandler getUserAssociationRequestHandler) : base(getUserAssociationRequestHandler)
    {
      _requestHandler = requestHandler;
      _presenter = presenter;
      _showJobTypeListRequestHandler = showJobTypeListRequestHandler;
      _showJobTypeListResponsePresenter = showJobTypeListResponsePresenter;
      _showJobTypeRequestHandler = showJobTypeRequestHandler;
      _showJobTypeResponsePresenter = showJobTypeResponsePresenter;
      _editJobTypeRequestHandler = editJobTypeRequestHandler;
      _editJobTypeResponsePresenter = editJobTypeResponsePresenter;
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.EmployeeSetup, (int)Feature.EmployeeSetupEnum.ViewJobType)]
    public IActionResult Index()
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowJobTypeListRequestMessage();
      var model = new JobTypeListViewModel();

      var response = _showJobTypeListRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _showJobTypeListResponsePresenter.Handle(response.Result, model, GetUserAssociationResponseMessage());

      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.EmployeeSetup, (int)Feature.EmployeeSetupEnum.NewJobType)]
    [HttpGet]
    public ActionResult AddJobType()
    {
      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();
      var viewModel = new AddJobTypeViewModel();
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

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.EmployeeSetup, (int)Feature.EmployeeSetupEnum.NewJobType)]
    [HttpPost]
    public ActionResult AddJobType(AddJobTypeViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new AddJobTypeRequestMessage();

      requestMessage.Title = model.Title;
      requestMessage.HasOverTime = model.HasOverTime;
      requestMessage.Status = StatusEnum.Active;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _requestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _presenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());

      if(!response.Result.ValidationResult.IsValid)
       return View(viewModel);
      
      return RedirectToAction("Index");
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.EmployeeSetup, (int)Feature.EmployeeSetupEnum.EditJobType)]
    [HttpGet]
    public ActionResult UpdateJobType(long jobTypeId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowJobTypeRequestMessage();
      var model = new JobTypeViewModel();
      
      requestMessage.JobTypeId = jobTypeId;

      var response = _showJobTypeRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _showJobTypeResponsePresenter.Handle(response.Result, model, GetUserAssociationResponseMessage());

      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.EmployeeSetup, (int)Feature.EmployeeSetupEnum.EditJobType)]
    [HttpPost]
    public ActionResult UpdateJobType(JobTypeViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new EditJobTypeRequestMessage();

      requestMessage.JobTypeId = model.Id;
      requestMessage.JobTitle = model.JobTitle;
      requestMessage.HasOverTime = model.HasOverTime;
      requestMessage.Status = model.Status;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _editJobTypeRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _editJobTypeResponsePresenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());

      if (!response.Result.ValidationResult.IsValid)
        return View(viewModel);

      return RedirectToAction("Index");
    }
  }
}