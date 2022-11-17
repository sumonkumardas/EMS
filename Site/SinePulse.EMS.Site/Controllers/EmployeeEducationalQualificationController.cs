using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SinePulse.EMS.Domain.Features;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.EmployeeEducationalQualificationMessages;
using SinePulse.EMS.Site.Authorization;
using SinePulse.EMS.Site.Models;
using SinePulse.EMS.Site.Presenters;
using SinePulse.EMS.UseCases.Authorization;
using SinePulse.EMS.UseCases.EmployeeEducationalQualifications;

namespace SinePulse.EMS.Site.Controllers
{
  [Authorize]
  public class EmployeeEducationalQualificationController : BaseController
  {
    private readonly AddEmployeeEducationalQualificationRequestHandler _addEmployeeEducationalQualificationRequestHandler;
    private readonly AddEmployeeEducationalQualificationResponsePresenter _presenter;
    private readonly ShowEducationalQualificationRequestHandler _showEducationalQualificationRequestHandler;
    private readonly ShowEducationalQualificationResponsePresenter _showEducationalQualificationPresenter;
    private readonly EditEducationalQualificationRequestHandler _editEducationalQualificationRequestHandler;
    private readonly EditEducationalQualificationResponsePresenter _editEducationalQualificationPresenter;

    public EmployeeEducationalQualificationController(AddEmployeeEducationalQualificationRequestHandler addEmployeeEducationalQualificationRequestHandler, 
      AddEmployeeEducationalQualificationResponsePresenter presenter,
      GetUserAssociationRequestHandler getUserAssociationRequestHandler, 
      ShowEducationalQualificationRequestHandler showEducationalQualificationRequestHandler, 
      ShowEducationalQualificationResponsePresenter showEducationalQualificationPresenter, 
      EditEducationalQualificationRequestHandler editEducationalQualificationRequestHandler, 
      EditEducationalQualificationResponsePresenter editEducationalQualificationPresenter) : base(getUserAssociationRequestHandler)
    {
      _addEmployeeEducationalQualificationRequestHandler = addEmployeeEducationalQualificationRequestHandler;
      _presenter = presenter;
      _showEducationalQualificationRequestHandler = showEducationalQualificationRequestHandler;
      _showEducationalQualificationPresenter = showEducationalQualificationPresenter;
      _editEducationalQualificationRequestHandler = editEducationalQualificationRequestHandler;
      _editEducationalQualificationPresenter = editEducationalQualificationPresenter;
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Employee, (int)Feature.EmployeeEnum.AddEducationalInfo)]
    [HttpGet]
    public ActionResult AddEmployeeEducationalQualification(long employeeId)
    {
      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();

      var viewModel = new AddEmployeeEducationalQualificationViewModel();
      viewModel.LoginName = userAssociationMessage.LoginName;
      viewModel.LoginImageUrl = userAssociationMessage.ImageUrl;
      viewModel.AssociatedWith = userAssociationMessage.AssociatedWith;
      viewModel.LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId;
      viewModel.LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId;
      viewModel.LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId;
      viewModel.RoleFeatures = userAssociationMessage.RoleFeatures;
      viewModel.InstituteBanner = userAssociationMessage.InstituteBanner;
      viewModel.EmployeeId = employeeId;
      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Employee, (int)Feature.EmployeeEnum.AddEducationalInfo)]
    [HttpPost]
    public ActionResult AddEmployeeEducationalQualification(AddEmployeeEducationalQualificationViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new AddEmployeeEducationalQualificationRequestMessage();

      requestMessage.DegreeName = model.DegreeName;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;
      requestMessage.InstituteName = model.InstituteName;
      requestMessage.MajorSubject = model.MajorSubject;
      requestMessage.PassingYear = model.PassingYear;
      requestMessage.EmployeeId = model.EmployeeId;

      var response = _addEmployeeEducationalQualificationRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _presenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());

      if (response.Result.ValidationResult.IsValid)
      {
        return Redirect("/Employee/ViewEmployee?employeeId="+model.EmployeeId+"#tab_educationalQualification");
      }
      return View(viewModel);
    }
    
    [HttpGet]
    public ActionResult EditEducationalQualification(long educationalQualificationId, long employeeId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowEducationalQualificationRequestMessage
      {
        EducationalQualificationId = educationalQualificationId
      };
      var model = new EducationalQualificationViewModel();
      var response = _showEducationalQualificationRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _showEducationalQualificationPresenter.Handle(response.Result, model);
      viewModel.EmployeeId = employeeId;
      viewModel.EducationalQualificationId = educationalQualificationId;
      return View(viewModel);
    }
    
    [HttpPost]
    public ActionResult EditEducationalQualification(EducationalQualificationViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new EditEducationalQualificationRequestMessage();

      requestMessage.DegreeName = model.DegreeName;
      requestMessage.PassingYear = model.PassingYear;
      requestMessage.InstituteName = model.InstituteName;
      requestMessage.MajorSubject = model.MajorSubject;
      requestMessage.EducationalQualificationId = model.EducationalQualificationId;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _editEducationalQualificationRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _editEducationalQualificationPresenter.Handle(response.Result, model, ModelState);
      viewModel.EmployeeId = model.EmployeeId;
      if (!response.Result.ValidationResult.IsValid)
      {
        viewModel.EducationalQualificationId = model.EducationalQualificationId;
        viewModel.EmployeeId = model.EmployeeId;
        return View(viewModel);
      }

      return Redirect("/Employee/ViewEmployee?employeeId="+model.EmployeeId+"#tab_educationalQualification");
    }

  }
}