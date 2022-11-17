using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SinePulse.EMS.Domain.Features;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.EmployeePersonalInfoMessages;
using SinePulse.EMS.Site.Authorization;
using SinePulse.EMS.Site.Models;
using SinePulse.EMS.Site.Presenters;
using SinePulse.EMS.UseCases.Authorization;
using SinePulse.EMS.UseCases.EmployeePersonalInfo;

namespace SinePulse.EMS.Site.Controllers
{
  [Authorize]
  public class EmployeePersonalInfoController : BaseController
  {
    private readonly AddEmployeePersonalInfoRequestHandler _addEmployeePersonalInfoRequestHandler;
    private readonly AddEmployeePersonalInfoResponsePresenter _presenter;
    private readonly GetEmployeePersonalInfoRequestHandler _getEmployeePersonalInfoRequestHandler;

    public EmployeePersonalInfoController(AddEmployeePersonalInfoRequestHandler addEmployeePersonalInfoRequestHandler, 
      AddEmployeePersonalInfoResponsePresenter presenter,
      GetUserAssociationRequestHandler getUserAssociationRequestHandler, 
      GetEmployeePersonalInfoRequestHandler getEmployeePersonalInfoRequestHandler) : base(getUserAssociationRequestHandler)
    {
      _addEmployeePersonalInfoRequestHandler = addEmployeePersonalInfoRequestHandler;
      _presenter = presenter;
      _getEmployeePersonalInfoRequestHandler = getEmployeePersonalInfoRequestHandler;
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Employee, (int)Feature.EmployeeEnum.AddPersonalDetail)]
    [HttpGet]
    public ActionResult AddEmployeePersonalInfo(long employeeId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new GetEmployeePersonalInfoRequestMessage
      {
        EmployeeId = employeeId
      };
      var response = _getEmployeePersonalInfoRequestHandler.Handle(requestMessage, cancellationToken);
      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();

      var viewModel = new AddEmployeePersonalInfoViewModel();
      viewModel.EmployeeId = employeeId;

      viewModel.LoginName = userAssociationMessage.LoginName;
      viewModel.LoginImageUrl = userAssociationMessage.ImageUrl;
      viewModel.AssociatedWith = userAssociationMessage.AssociatedWith;
      viewModel.LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId;
      viewModel.LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId;
      viewModel.LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId;
      viewModel.RoleFeatures = userAssociationMessage.RoleFeatures;
      viewModel.InstituteBanner = userAssociationMessage.InstituteBanner;
      viewModel.Gender = response.Result.EmployeePersonalInfos.GenderEnum;
      viewModel.BloodGroup = response.Result.EmployeePersonalInfos.BloodGroupEnum;
      viewModel.Religion = response.Result.EmployeePersonalInfos.ReligionEnum;
      viewModel.FatherName = response.Result.EmployeePersonalInfos.FathersName;
      viewModel.MotherName = response.Result.EmployeePersonalInfos.MothersName;
      viewModel.SpouseName = response.Result.EmployeePersonalInfos.SpouseName;
      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Employee, (int)Feature.EmployeeEnum.AddPersonalDetail)]
    [HttpPost]
    public ActionResult AddEmployeePersonalInfo(AddEmployeePersonalInfoViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new AddEmployeePersonalInfoRequestMessage();

      requestMessage.BloodGroup = model.BloodGroup;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;
      requestMessage.FatherName = model.FatherName;
      requestMessage.Gender = model.Gender;
      requestMessage.MotherName = model.MotherName;
      requestMessage.Religion = model.Religion;
      requestMessage.SpouseName = model.SpouseName;
      requestMessage.EmployeeId = model.EmployeeId;

      var response = _addEmployeePersonalInfoRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _presenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());

      if (response.Result.ValidationResult.IsValid)
      {
        return Redirect("/Employee/ViewEmployee?employeeId="+model.EmployeeId+"#tab_personalInfo");
      }
      return View(viewModel);
    }
    
    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Employee, (int)Feature.EmployeeEnum.ViewPersonalDetail)]
    public ActionResult Index()
    {
            return View();
    }
  }
}