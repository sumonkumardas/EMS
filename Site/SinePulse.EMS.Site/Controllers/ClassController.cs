using System.Collections.Generic;
using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Features;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.ClassMessages;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Persistence.Repositories;
using SinePulse.EMS.Site.Authorization;
using SinePulse.EMS.Site.Models;
using SinePulse.EMS.Site.Presenters;
using SinePulse.EMS.UseCases.Authorization;
using SinePulse.EMS.UseCases.Classes;

namespace SinePulse.EMS.Site.Controllers
{
  [Authorize]
  public class ClassController : BaseController
  {
    private readonly AddClassRequestHandler _addClassRequestHandler;
    private readonly AddClassResponsePresenter _presenter;
    private readonly AddSubjectInClassResponsePresenter _addSubjectInClassResponsePresenter;
    private readonly AddSubjectInClassRequestHandler _addSubjectInClassRequestHandler;
    private readonly ShowClassesListRequestHandler _showClassesListRequestHandler;
    private readonly ShowClassesListResponsePresenter _showClassesListResponsePresenter;
    private readonly ShowClassRequestHandler _showClassRequestHandler;
    private readonly ShowClassResponsePresenter _showClassResponsePresenter;
    private readonly EditClassRequestHandler _editClassRequestHandler;
    private readonly EditClassResponsePresenter _editClassResponsePresenter;
    private readonly GetSubjectsToAddInClassRequestHandler _getSubjectsToAddInClassRequestHandler;
    private readonly GetAddedSubjectsOfClassRequestHandler _getAddedSubjectsOfClassRequestHandler;

    public ClassController(AddClassRequestHandler addClassRequestHandler, AddClassResponsePresenter presenter,
      AddSubjectInClassResponsePresenter addSubjectInClassResponsePresenter,
      AddSubjectInClassRequestHandler addSubjectInClassRequestHandler, IConfiguration config,
      ShowClassesListRequestHandler showClassesListRequestHandler,
      ShowClassesListResponsePresenter showClassesListResponsePresenter,
      ShowClassRequestHandler showClassRequestHandler, ShowClassResponsePresenter showClassResponsePresenter,
      EditClassRequestHandler editClassRequestHandler, EditClassResponsePresenter editClassResponsePresenter, 
      IRepository repository, GetSubjectsToAddInClassRequestHandler getSubjectsToAddInClassRequestHandler, 
      GetAddedSubjectsOfClassRequestHandler getAddedSubjectsOfClassRequestHandler,
      GetUserAssociationRequestHandler getUserAssociationRequestHandler) : base(getUserAssociationRequestHandler)
    {
      _addClassRequestHandler = addClassRequestHandler;
      _presenter = presenter;
      _addSubjectInClassResponsePresenter = addSubjectInClassResponsePresenter;
      _addSubjectInClassRequestHandler = addSubjectInClassRequestHandler;
      _showClassesListRequestHandler = showClassesListRequestHandler;
      _showClassesListResponsePresenter = showClassesListResponsePresenter;
      _showClassRequestHandler = showClassRequestHandler;
      _showClassResponsePresenter = showClassResponsePresenter;
      _editClassRequestHandler = editClassRequestHandler;
      _editClassResponsePresenter = editClassResponsePresenter;
      _getSubjectsToAddInClassRequestHandler = getSubjectsToAddInClassRequestHandler;
      _getAddedSubjectsOfClassRequestHandler = getAddedSubjectsOfClassRequestHandler;
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.AcademicSetup, (int)Feature.AcademicSetupEnum.NewClass)]
    [HttpGet]
    public ActionResult AddClass()
    {
      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();
      var classViewModel = new AddClassViewModel
      {
        LoginName = userAssociationMessage.LoginName,
        AssociatedWith = userAssociationMessage.AssociatedWith,
        LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId,
        LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId,
        LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId,
        RoleFeatures = userAssociationMessage.RoleFeatures,
        InstituteBanner = userAssociationMessage.InstituteBanner
      };
      return View(classViewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.AcademicSetup, (int)Feature.AcademicSetupEnum.NewClass)]
    [HttpPost]
    public ActionResult AddClass(AddClassViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new AddClassRequestMessage();

      requestMessage.ClassName = model.ClassName;
      requestMessage.ClassCode = model.ClassCode;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _addClassRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _presenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());

      if (!response.Result.ValidationResult.IsValid)
        return View(viewModel);

      return RedirectToAction("ViewClass", new {classId = response.Result.ClassId});
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.AcademicSetup, (int)Feature.AcademicSetupEnum.ViewClass)]
    public ActionResult Index()
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowClassesListRequestMessage();
      var model = new ShowClassesListViewModel();

      var response = _showClassesListRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _showClassesListResponsePresenter.Handle(response.Result, model, GetUserAssociationResponseMessage());

      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.AcademicSetup, (int)Feature.AcademicSetupEnum.AddSubject)]
    [HttpGet]
    public ActionResult AddSubjectInClass(long classId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowClassRequestMessage();
      requestMessage.ClassId = classId;
      var @class = _showClassRequestHandler.Handle(requestMessage, cancellationToken).Result.Class;
      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();
      return View(new AddSubjectInClassViewModel
      {
        ClassId = classId,
        ClassName = @class.ClassName,
        ClassCode = @class.ClassCode,
        Subjects = GetSubjects(classId),
        LoginName = userAssociationMessage.LoginName,
        AssociatedWith = userAssociationMessage.AssociatedWith,
        LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId,
        LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId,
        LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId,
        RoleFeatures = userAssociationMessage.RoleFeatures,
        InstituteBanner = userAssociationMessage.InstituteBanner
      });
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.AcademicSetup, (int)Feature.AcademicSetupEnum.AddSubject)]
    [HttpPost]
    public ActionResult AddSubjectInClass(AddSubjectInClassViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new AddSubjectInClassRequestMessage();
      requestMessage.ClassId = model.ClassId;
      requestMessage.SubjectIds = model.SubjectIds;
      requestMessage.Status = StatusEnum.Active;
      requestMessage.IsOptional = model.IsOptional;
      requestMessage.Group = model.Group;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _addSubjectInClassRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _addSubjectInClassResponsePresenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());
      
//      if (response.Result.ValidationResult.IsValid)
//        return RedirectToAction("ViewClass",new {classId = model.ClassId});
      
      viewModel.Subjects = GetSubjects(model.ClassId);
      viewModel.ClassId = model.ClassId;
      viewModel.ClassName = model.ClassName;
      viewModel.ClassCode = model.ClassCode;
      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.AcademicSetup, (int)Feature.AcademicSetupEnum.AddSubject)]
    public ActionResult AddSubjectInClass()
    {
      return View();
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.AcademicSetup, (int)Feature.AcademicSetupEnum.ViewClass)]
    public ActionResult ViewClass(long classId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowClassRequestMessage();
      var model = new ClassViewModel();
      requestMessage.ClassId = classId;

      var response = _showClassRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _showClassResponsePresenter.Handle(response.Result, model, GetUserAssociationResponseMessage());
      
      var subjectsRequestMessage = new GetAddedSubjectsOfClassRequestMessage();
      subjectsRequestMessage.ClassId = classId;
      var subjects = _getAddedSubjectsOfClassRequestHandler.Handle(subjectsRequestMessage, cancellationToken).Result.SubjectsWithClass;
      viewModel.Subjects = subjects;
      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.AcademicSetup, (int)Feature.AcademicSetupEnum.EditClass)]
    [HttpGet]
    public ActionResult UpdateClass(long classId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowClassRequestMessage();
      var model = new ClassViewModel();
      requestMessage.ClassId = classId;

      var response = _showClassRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _showClassResponsePresenter.Handle(response.Result, model, GetUserAssociationResponseMessage());

      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.AcademicSetup, (int)Feature.AcademicSetupEnum.EditClass)]
    [HttpPost]
    public ActionResult UpdateClass(ClassViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new EditClassRequestMessage();

      requestMessage.ClassId = model.Id;
      requestMessage.ClassName = model.ClassName;
      requestMessage.ClassCode = model.ClassCode;
      requestMessage.Status = model.Status;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _editClassRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _editClassResponsePresenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());

      if (!response.Result.ValidationResult.IsValid)
        return View(viewModel);

      return RedirectToAction("ViewClass", new {classId = model.Id});
    }
    
    private IEnumerable<SubjectMessageModel> GetSubjects(long classId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new GetSubjectsToAddInClassRequestMessage();
      requestMessage.ClassId = classId;
      var response = _getSubjectsToAddInClassRequestHandler.Handle(requestMessage, cancellationToken);
      return response.Result.Subjects;
    }
  }
}