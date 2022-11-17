using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SinePulse.EMS.Domain.Features;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.Model.Enums;
using SinePulse.EMS.Messages.StudentMessages;
using SinePulse.EMS.Messages.SubjectMessages;
using SinePulse.EMS.Site.Authorization;
using SinePulse.EMS.Site.Models;
using SinePulse.EMS.Site.Presenters;
using SinePulse.EMS.UseCases.Authorization;
using SinePulse.EMS.UseCases.Students;
using SinePulse.EMS.UseCases.Subjects;
using StatusEnum = SinePulse.EMS.Domain.Enums.StatusEnum;

namespace SinePulse.EMS.Site.Controllers
{
  [Authorize]
  public class SubjectController : BaseController
  {
    private readonly AddSubjectRequestHandler _addSubjectRequestHandler;
    private readonly AddSubjectResponsePresenter _presenter;
    private readonly ShowSubjectListRequestHandler _showSubjectListRequestHandler;
    private readonly ShowSubjectListResponsePresenter _showSubjectListResponsePresenter;
    private readonly ShowSubjectRequestHandler _showSubjectRequestHandler;
    private readonly ShowSubjectResponsePresenter _showSubjectResponsePresenter;
    private readonly EditSubjectRequestHandler _editSubjectRequestHandler;
    private readonly EditSubjectResponsePresenter _editSubjectResponsePresenter;
    private readonly GetOptionalSubjectListRequestHandler _getOptionalSubjectListRequestHandler;
    private readonly ShowStudentRequestHandler _showStudentRequestHandler;
    private readonly AddOptionalSubjectRequestHandler _addOptionalSubjectRequestHandler;
    private readonly AddOptionalSubjectResponsePresenter _addOptionalSubjectResponsePresenter;

    public SubjectController(AddSubjectRequestHandler addSubjectRequestHandler, AddSubjectResponsePresenter presenter,
      ShowSubjectListRequestHandler showSubjectListRequestHandler,
      ShowSubjectListResponsePresenter showSubjectListResponsePresenter,
      ShowSubjectRequestHandler showSubjectRequestHandler, ShowSubjectResponsePresenter showSubjectResponsePresenter,
      EditSubjectRequestHandler editSubjectRequestHandler,
      EditSubjectResponsePresenter editSubjectResponsePresenter,
      GetUserAssociationRequestHandler getUserAssociationRequestHandler, 
      GetOptionalSubjectListRequestHandler getOptionalSubjectListRequestHandler, 
      ShowStudentRequestHandler showStudentRequestHandler, 
      AddOptionalSubjectRequestHandler addOptionalSubjectRequestHandler,
      AddOptionalSubjectResponsePresenter addOptionalSubjectResponsePresenter) : base(getUserAssociationRequestHandler)
    {
      _addSubjectRequestHandler = addSubjectRequestHandler;
      _presenter = presenter;
      _showSubjectListRequestHandler = showSubjectListRequestHandler;
      _showSubjectListResponsePresenter = showSubjectListResponsePresenter;
      _showSubjectRequestHandler = showSubjectRequestHandler;
      _showSubjectResponsePresenter = showSubjectResponsePresenter;
      _editSubjectRequestHandler = editSubjectRequestHandler;
      _editSubjectResponsePresenter = editSubjectResponsePresenter;
      _getOptionalSubjectListRequestHandler = getOptionalSubjectListRequestHandler;
      _showStudentRequestHandler = showStudentRequestHandler;
      _addOptionalSubjectRequestHandler = addOptionalSubjectRequestHandler;
      _addOptionalSubjectResponsePresenter = addOptionalSubjectResponsePresenter;
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.AcademicSetup, (int)Feature.AcademicSetupEnum.NewSubject)]
    [HttpGet]
    public ActionResult AddSubject()
    {
      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();
      var addSubjectViewModel = new AddSubjectViewModel
      {
        LoginName = userAssociationMessage.LoginName,
        AssociatedWith = userAssociationMessage.AssociatedWith,
        LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId,
        LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId,
        LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId,
        RoleFeatures = userAssociationMessage.RoleFeatures,
        InstituteBanner = userAssociationMessage.InstituteBanner
      };

      return View(addSubjectViewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.AcademicSetup, (int)Feature.AcademicSetupEnum.NewSubject)]
    [HttpPost]
    public ActionResult AddSubject(AddSubjectViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new AddSubjectRequestMessage();

      requestMessage.SubjectName = model.SubjectName;
      requestMessage.SubjectCode = model.SubjectCode;
      
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _addSubjectRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _presenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());

      if (!response.Result.ValidationResult.IsValid)
      {
        return View(viewModel);
      }

      return RedirectToAction("Index");
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.AcademicSetup, (int)Feature.AcademicSetupEnum.ViewSubject)]
    public ActionResult Index()
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowSubjectListRequestMessage();
      var model = new ShowSubjectListViewModel();

      var response = _showSubjectListRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _showSubjectListResponsePresenter.Handle(response.Result, model, GetUserAssociationResponseMessage());

      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.AcademicSetup, (int)Feature.AcademicSetupEnum.EditSubject)]
    [HttpGet]
    public ActionResult UpdateSubject(long id)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowSubjectRequestMessage();
      var model = new SubjectViewModel();
      requestMessage.SubjectId = id;

      var response = _showSubjectRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _showSubjectResponsePresenter.Handle(response.Result, model, GetUserAssociationResponseMessage());

      var updateSubjectViewModel = new EditSubjectViewModel
      {
        SubjectId = viewModel.SubjectId,
        SubjectName = viewModel.SubjectName,
        Status = viewModel.Status,
        SubjectCode = viewModel.SubjectCode,

        LoginName = viewModel.LoginName,
        LoginImageUrl = viewModel.LoginImageUrl,
        AssociatedWith = viewModel.AssociatedWith,
        LoginUsersInstituteId = viewModel.LoginUsersInstituteId,
        LoginUsersBranchId = viewModel.LoginUsersBranchId,
        LoginUsersBranchMediumId = viewModel.LoginUsersBranchMediumId,
        RoleFeatures = viewModel.RoleFeatures
      };

      return View(updateSubjectViewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.AcademicSetup, (int)Feature.AcademicSetupEnum.EditSubject)]
    [HttpPost]
    public ActionResult UpdateSubject(EditSubjectViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new EditSubjectRequestMessage();

      requestMessage.SubjectId = model.SubjectId;
      requestMessage.SubjectName = model.SubjectName;
      requestMessage.SubjectCode = model.SubjectCode;
      requestMessage.Status = model.Status;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _editSubjectRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _editSubjectResponsePresenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());

      if (!response.Result.ValidationResult.IsValid)
      {
        return View(viewModel);
      }

      return RedirectToAction("Index");
    }

    private IEnumerable<SubjectMessageModel> GetSubjects()
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowSubjectListRequestMessage();
      var response = _showSubjectListRequestHandler.Handle(requestMessage, cancellationToken);
      return response.Result.Subjects;
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Student, (int)Feature.StudentEnum.AddOptionalSubject)]
    [HttpGet]
    public ActionResult AddOptionalSubject(long studentId, long branchMediumId)
    {
      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();
      var studentData = GetStudentData(studentId, branchMediumId);
      var viewModel = new AddOptionalSubjectViewModel();
      viewModel.StudentId = studentId;
      viewModel.ClassId = studentData.ClassId;
      viewModel.Group = (Domain.Enums.GroupEnum) studentData.Group;
      viewModel.BranchMediumId = branchMediumId;
      viewModel.StudentName = studentData.FullName;
      viewModel.ClassName = studentData.ClassName;
      viewModel.SectionId = studentData.SectionId;
      viewModel.SectionName = studentData.SectionName;
      viewModel.Roll = studentData.Roll;
      viewModel.OptionalSubjects = GetOptionalSubjects(studentData.ClassId, studentData.Group);

      viewModel.LoginName = userAssociationMessage.LoginName;
      viewModel.AssociatedWith = userAssociationMessage.AssociatedWith;
      viewModel.LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId;
      viewModel.LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId;
      viewModel.LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId;
      viewModel.RoleFeatures = userAssociationMessage.RoleFeatures;
      viewModel.InstituteBanner = userAssociationMessage.InstituteBanner;

      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Student, (int)Feature.StudentEnum.AddOptionalSubject)]
    [HttpPost]
    public ActionResult AddOptionalSubject(AddOptionalSubjectViewModel model)
    {
      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();
      var cancellationToken = new CancellationToken();
      var requestMessage = new AddOptionalSubjectRequestMessage();

      requestMessage.StudentId = model.StudentId;
      requestMessage.ClassId = model.ClassId;
      requestMessage.Status = StatusEnum.Active;
      requestMessage.Group = model.Group;
      requestMessage.OptionalSubjectIds = model.OptionalSubjectIds;
      requestMessage.SectionId = model.SectionId;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _addOptionalSubjectRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _addOptionalSubjectResponsePresenter.Handle(response.Result, model, ModelState, userAssociationMessage);

      if (!response.Result.ValidationResult.IsValid)
      {
        viewModel.OptionalSubjects = GetOptionalSubjects(model.ClassId, (GroupEnum) model.Group);
        viewModel.BranchMediumId = model.BranchMediumId;
        viewModel.StudentName = model.StudentName;
        viewModel.StudentId = model.StudentId;
        viewModel.ClassName = model.ClassName;
        viewModel.ClassId = model.ClassId;
        viewModel.SectionId = model.SectionId;
        viewModel.SectionName = model.SectionName;
        viewModel.Roll = model.Roll;
        viewModel.Group = model.Group;

        viewModel.LoginName = userAssociationMessage.LoginName;
        viewModel.AssociatedWith = userAssociationMessage.AssociatedWith;
        viewModel.LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId;
        viewModel.LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId;
        viewModel.LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId;
        viewModel.RoleFeatures = userAssociationMessage.RoleFeatures;
        viewModel.InstituteBanner = userAssociationMessage.InstituteBanner;
        return View(viewModel);
      }

      return RedirectToAction("ViewStudent", "Student", new {studentId = model.StudentId, branchMediumId = model.BranchMediumId});
    }

    private ShowStudentResponseMessage.Student GetStudentData(long studentId, long branchMediumId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowStudentRequestMessage
      {
        StudentId = studentId,
        BranchMediumId = branchMediumId
      };
      var response = _showStudentRequestHandler.Handle(requestMessage, cancellationToken);
      return response.Result.Data.StudentData;
    }

    private IEnumerable<GetOptionalSubjectListResponseMessage.Subject> GetOptionalSubjects(long classId, GroupEnum group)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new GetOptionalSubjectListRequestMessage
      {
        ClassId =  classId,
        Group = (Domain.Enums.GroupEnum) group
      };
      var response = _getOptionalSubjectListRequestHandler.Handle(requestMessage, cancellationToken);
      return response.Result.Subjects;
    }
  }
}