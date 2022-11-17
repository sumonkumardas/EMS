using System.Collections.Generic;
using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Features;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.EmployeeGradeMessages;
using SinePulse.EMS.Site.Authorization;
using SinePulse.EMS.Site.Models;
using SinePulse.EMS.Site.Presenters;
using SinePulse.EMS.UseCases.Authorization;
using SinePulse.EMS.UseCases.EmployeeGrade;

namespace SinePulse.EMS.Site.Controllers
{
  [Authorize]
  public class EmployeeGradeController : BaseController
  {
    private readonly AddEmployeeGradeRequestHandler _addEmployeeGradeRequestHandler;
    private readonly AddEmployeeGradeResponsePresenter _addEmployeeGradeResponsePresenter;
    private readonly EditEmployeeGradeRequestHandler _editEmployeeGradeRequestHandler;
    private readonly EditEmployeeGradeResponsePresenter _editEmployeeGradeResponsePresenter;
    private readonly ShowEmployeeGradeRequestHandler _showEmployeeGradeRequestHandler;
    private readonly ShowEmployeeGradeResponsePresenter _showEmployeeGradeResponsePresenter;
    private readonly ShowEmployeeGradeListRequestHandler _showEmployeeGradeListRequestHandler;
    private readonly ShowEmployeeGradeListResponsePresenter _showEmployeeGradeListResponsePresenter;

    public EmployeeGradeController(AddEmployeeGradeRequestHandler addEmployeeGradeRequestHandler, 
      AddEmployeeGradeResponsePresenter addEmployeeGradeResponsePresenter, EditEmployeeGradeRequestHandler editEmployeeGradeRequestHandler, 
      EditEmployeeGradeResponsePresenter editEmployeeGradeResponsePresenter, ShowEmployeeGradeRequestHandler showEmployeeGradeRequestHandler, 
      ShowEmployeeGradeResponsePresenter showEmployeeGradeResponsePresenter, ShowEmployeeGradeListRequestHandler showEmployeeGradeListRequestHandler, 
      ShowEmployeeGradeListResponsePresenter showEmployeeGradeListResponsePresenter,
      GetUserAssociationRequestHandler getUserAssociationRequestHandler) : base(getUserAssociationRequestHandler)
    {
      _addEmployeeGradeRequestHandler = addEmployeeGradeRequestHandler;
      _addEmployeeGradeResponsePresenter = addEmployeeGradeResponsePresenter;
      _editEmployeeGradeRequestHandler = editEmployeeGradeRequestHandler;
      _editEmployeeGradeResponsePresenter = editEmployeeGradeResponsePresenter;
      _showEmployeeGradeRequestHandler = showEmployeeGradeRequestHandler;
      _showEmployeeGradeResponsePresenter = showEmployeeGradeResponsePresenter;
      _showEmployeeGradeListRequestHandler = showEmployeeGradeListRequestHandler;
      _showEmployeeGradeListResponsePresenter = showEmployeeGradeListResponsePresenter;
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.EmployeeSetup, (int)Feature.EmployeeSetupEnum.NewGrade)]
    [HttpGet]
    public ActionResult AddEmployeeGrade()
    {
      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();
      var viewModel = new AddEmployeeGradeViewModel();
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

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.EmployeeSetup, (int)Feature.EmployeeSetupEnum.NewGrade)]
    [HttpPost]
    public ActionResult AddEmployeeGrade(AddEmployeeGradeViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new AddEmployeeGradeRequestMessage();

      requestMessage.GradeTitle = model.GradeTitle;
      requestMessage.MaxSalary = model.MaxSalary;
      requestMessage.MinSalary = model.MinSalary;
      requestMessage.Status = Messages.Model.Enums.StatusEnum.Active;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _addEmployeeGradeRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _addEmployeeGradeResponsePresenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());

      if (response.Result.EmployeeGradeId > 0)
      {
        return RedirectToAction("Index");
      }
      else
        return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.EmployeeSetup, (int)Feature.EmployeeSetupEnum.ViewGrade)]
    public ActionResult Index()
    {
      var cancellationToken = new CancellationToken();
      var showEmployeeGradeListRequestMessage = new ShowEmployeeGradeListRequestMessage();
      GradeListViewModel model = new GradeListViewModel();
      
      var showEmployeeGradeListResponse = _showEmployeeGradeListRequestHandler.Handle(showEmployeeGradeListRequestMessage, cancellationToken);
      var pickedEmployeeGradeListViewModel = _showEmployeeGradeListResponsePresenter.Handle(showEmployeeGradeListResponse.Result, model, GetUserAssociationResponseMessage());

      return View(pickedEmployeeGradeListViewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.EmployeeSetup, (int)Feature.EmployeeSetupEnum.EditGrade)]
    [HttpGet]
    public ActionResult UpdateEmployeeGrade(int gradeId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowEmployeeGradeRequestMessage();
      var model = new GradeViewModel();
      requestMessage.GradeId = gradeId;

      var response = _showEmployeeGradeRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _showEmployeeGradeResponsePresenter.Handle(response.Result, model, GetUserAssociationResponseMessage());

      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.EmployeeSetup, (int)Feature.EmployeeSetupEnum.EditGrade)]
    [HttpPost]
    public ActionResult UpdateEmployeeGrade(GradeViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new EditEmployeeGradeRequestMessage();

      requestMessage.Id = model.Id;
      requestMessage.GradeTitle = model.GradeTitle;
      requestMessage.MaxSalary = model.MaxSalary;
      requestMessage.MinSalary = model.MinSalary;
      requestMessage.Status = Messages.Model.Enums.StatusEnum.Active;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _editEmployeeGradeRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _editEmployeeGradeResponsePresenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());

      if (response.Result.ValidationResult.IsValid)
      {
        return RedirectToAction("Index");
      }
      else
        return View(viewModel);
    }
  }
}