using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Features;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.BranchMessages;
using SinePulse.EMS.Messages.InstituteMessages;
using SinePulse.EMS.Messages.MediumMessages;
using SinePulse.EMS.Messages.SectionMessages;
using SinePulse.EMS.Messages.SessionMessages;
using SinePulse.EMS.Messages.StudentSectionMessages;
using SinePulse.EMS.Site.Authorization;
using SinePulse.EMS.Site.Models;
using SinePulse.EMS.Site.Presenters;
using SinePulse.EMS.UseCases.Authorization;
using SinePulse.EMS.UseCases.Branches;
using SinePulse.EMS.UseCases.Institutes;
using SinePulse.EMS.UseCases.Mediums;
using SinePulse.EMS.UseCases.Sections;
using SinePulse.EMS.UseCases.Sessions;
using SinePulse.EMS.UseCases.StudentSections;

namespace SinePulse.EMS.Site.Controllers
{
  [Authorize]
  public class StudentSectionController : BaseController
  {
    private readonly ShowInstituteListRequestHandler _showInstituteListRequestHandler;
    private readonly ShowBranchListRequestHandler _showBranchListRequestHandler;
    private readonly ShowMediumListRequestHandler _showMediumListRequestHandler;
    private readonly ShowSessionListRequestHandler _showSessionListRequestHandler;
    private readonly AddStudentSectionRequestHandler _addStudentSectionRequestHandler;
    private readonly AddStudentSectionResponsePresenter _addStudentSectionResponsePresenter;
    private readonly ShowBranchMediumSectionListRequestHandler _showBranchMediumSectionListRequestHandler;

    public StudentSectionController(
      ShowInstituteListRequestHandler showInstituteListRequestHandler, 
      ShowBranchListRequestHandler showBranchListRequestHandler, 
      ShowMediumListRequestHandler showMediumListRequestHandler, 
      ShowSessionListRequestHandler showSessionListRequestHandler, 
      AddStudentSectionRequestHandler addStudentSectionRequestHandler, 
      AddStudentSectionResponsePresenter addStudentSectionResponsePresenter, 
      ShowBranchMediumSectionListRequestHandler showBranchMediumSectionListRequestHandler,
      GetUserAssociationRequestHandler getUserAssociationRequestHandler) : base(getUserAssociationRequestHandler)
    {
      _showInstituteListRequestHandler = showInstituteListRequestHandler;
      _showBranchListRequestHandler = showBranchListRequestHandler;
      _showMediumListRequestHandler = showMediumListRequestHandler;
      _showSessionListRequestHandler = showSessionListRequestHandler;
      _addStudentSectionRequestHandler = addStudentSectionRequestHandler;
      _addStudentSectionResponsePresenter = addStudentSectionResponsePresenter;
      _showBranchMediumSectionListRequestHandler = showBranchMediumSectionListRequestHandler;
    }

    [HttpGet]
    public ActionResult AddStudentSection(long studentId)
    {
      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();
      var viewModel = new AddStudentSectionViewModel();
      viewModel.LoginName = userAssociationMessage.LoginName;
      viewModel.LoginImageUrl = userAssociationMessage.ImageUrl;
      viewModel.AssociatedWith = userAssociationMessage.AssociatedWith;
      viewModel.LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId;
      viewModel.LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId;
      viewModel.LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId;
      viewModel.RoleFeatures = userAssociationMessage.RoleFeatures;
      viewModel.InstituteBanner = userAssociationMessage.InstituteBanner;

      viewModel.StudentId = studentId;
      viewModel.Institutes = GetInstitutes();
      return View(viewModel);
     
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Section, (int)Feature.SectionEnum.NewSection)]
    [HttpPost]
    public ActionResult AddStudentSection(AddStudentSectionViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new AddStudentSectionRequestMessage();

      requestMessage.RollNo = model.RollNo;
      requestMessage.StudentId = Convert.ToInt64(model.StudentId);
      requestMessage.SectionId = Convert.ToInt64(model.SectionId);
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _addStudentSectionRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _addStudentSectionResponsePresenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());
      viewModel.Institutes = GetInstitutes(); 
      return View(viewModel);
    }
    
    private IEnumerable<Institute> GetInstitutes()
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowInstituteListRequestMessage();
      var response = _showInstituteListRequestHandler.Handle(requestMessage, cancellationToken);
      return response.Result.InstituteList;
    }
    
    [HttpPost]
    public JsonResult GetBranches(long instituteId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowBranchListRequestMessage();
      requestMessage.InstituteId = instituteId;
      var response = _showBranchListRequestHandler.Handle(requestMessage, cancellationToken);
      return Json(response.Result.Branches);
    }
    
    [HttpPost]
    public JsonResult GetMediums(long branchId)
    {
      if (branchId > 0)
      {
        var cancellationToken = new CancellationToken();
        var requestMessage = new ShowMediumListRequestMessage();
        requestMessage.BranchId = branchId;
        var response = _showMediumListRequestHandler.Handle(requestMessage, cancellationToken);
        return Json(response.Result.Mediums);
      }
      return Json(null);
    }

    [HttpPost]
    public JsonResult GetSessions(long mediumId, long branchId)
    {
      if (mediumId > 0 && branchId > 0)
      {
        var cancellationToken = new CancellationToken();
        var requestMessage = new ShowSessionListRequestMessage();
        requestMessage.MediumId = mediumId;
        requestMessage.BranchId = branchId;
        var response = _showSessionListRequestHandler.Handle(requestMessage, cancellationToken);
        return Json(response.Result.Sessions); 
      }
      return Json(null);
    }
    
    [HttpPost]
    public JsonResult GetSections(long mediumId, long branchId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowBranchMediumSectionListRequestMessage();
      requestMessage.MediumId = mediumId;
      requestMessage.BranchId = branchId;
      var response = _showBranchMediumSectionListRequestHandler.Handle(requestMessage, cancellationToken);
      return Json(response.Result.Sections);
    }
  }
}