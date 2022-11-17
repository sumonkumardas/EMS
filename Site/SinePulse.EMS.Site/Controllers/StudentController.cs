using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Features;
using SinePulse.EMS.Messages.AttendanceMessages;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.BranchMediumMessages;
using SinePulse.EMS.Messages.ExamTermMessages;
using SinePulse.EMS.Messages.InstituteMessages;
using SinePulse.EMS.Messages.MarkMessages;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.ResultSheetMessages;
using SinePulse.EMS.Messages.SectionMessages;
using SinePulse.EMS.Messages.SessionMessages;
using SinePulse.EMS.Messages.StudentContactPersonMessages;
using SinePulse.EMS.Messages.StudentMessages;
using SinePulse.EMS.Messages.StudentSectionMessages;
using SinePulse.EMS.Site.Authorization;
using SinePulse.EMS.Site.Models;
using SinePulse.EMS.Site.Presenters;
using SinePulse.EMS.UseCases.Attendances;
using SinePulse.EMS.UseCases.Authorization;
using SinePulse.EMS.UseCases.BranchMediums;
using SinePulse.EMS.UseCases.Classes;
using SinePulse.EMS.UseCases.ExamTerms;
using SinePulse.EMS.UseCases.Institutes;
using SinePulse.EMS.UseCases.ResultSheets;
using SinePulse.EMS.UseCases.Sections;
using SinePulse.EMS.UseCases.Sessions;
using SinePulse.EMS.UseCases.StudentContactPersons;
using SinePulse.EMS.UseCases.Students;
using SinePulse.EMS.UseCases.StudentSections;
using RelationTypeEnum = SinePulse.EMS.Messages.Model.Enums.RelationTypeEnum;
using StatusEnum = SinePulse.EMS.Messages.Model.Enums.StatusEnum;

namespace SinePulse.EMS.Site.Controllers
{
  [Authorize]
  public class StudentController : BaseController
  {
    private readonly DisplayAddStudentPageRequestHandler _displayAddStudentPageRequestHandler;
    private readonly DisplayAddStudentPageResponsePresenter _displayAddStudentPageResponsePresenter;
    private readonly AddStudentRequestHandler _addStudentRequestHandler;
    private readonly AddStudentResponsePresenter _presenter;
    private readonly ShowStudentRequestHandler _showStudentRequestHandler;
    private readonly ShowStudentResponsePresenter _showStudentResponsePresenter;
    private readonly DisplayEditStudentPageResponsePresenter _displayEditStudentPageResponsePresenter;
    private readonly ShowStudentListRequestHandler _showStudentListRequestHandler;
    private readonly ShowStudentListResponsePresenter _showStudentListResponsePresenter;
    private readonly EditStudentRequestHandler _editStudentRequestHandler;
    private readonly EditStudentResponsePresenter _editStudentResponsePresenter;
    private readonly AddStudentContactPersonImageRequestHandler _addStudentContactPersonImageRequestHandler;
    private readonly IHostingEnvironment _appEnvironment;
    private readonly AddOrChangeStudentImageRequestHandler _addOrChangeStudentImageRequestHandler;
    private readonly AddStudentContactPersonRequestHandler _addStudentContactPersonRequestHandler;
    private readonly AddStudentContactPersonResponsePresenter _addStudentContactPersonResponsePresenter;

    private readonly AddStudentAttendanceRequestHandler _addStudentAttendanceRequestHandler;
    private readonly AddStudentAttendanceResponsePresenter _addStudentAttendanceResponsePresenter;
    private readonly ShowInstituteListRequestHandler _showInstituteListRequestHandler;
    private readonly ShowInstituteListResponsePresenter _showInstituteListResponsePresenter;

    private readonly ShowCurrentMonthAttendanceListRequestHandler _showCurrentMonthAttendanceListRequestHandler;
    private readonly ShowStudentAttendanceListResponsePresenter _showStudentAttendanceListResponsePresenter;
    private readonly ShowStudentAttendanceRequestHandler _showStudentAttendanceRequestHandler;
    private readonly ShowStudentAttendanceResponsePresenter _showStudentAttendanceResponsePresenter;
    private readonly EditStudentAttendanceRequestHandler _editStudentAttendanceRequestHandler;
    private readonly EditStudentAttendanceResponsePresenter _editStudentAttendanceResponsePresenter;
    private readonly ShowBranchMediumSectionListRequestHandler _showSectionListRequestHandler;
    private readonly ShowBranchMediumRequestHandler _showBranchMediumRequestHandler;
    private readonly AddStudentAddressRequestHandler _addStudentAddressRequestHandler;
    private readonly AddStudentAddressResponsePresenter _addStudentAddressResponsePresenter;
    private readonly GetBranchMediumSessionsRequestHandler _getBranchMediumSessionsRequestHandler;
    private readonly GetExamTermsRequestHandler _getExamTermsRequestHandler;
    private readonly GetExamTermMarksRequestHandler _getExamTermMarksRequestHandler;
    private readonly GetClassTestMarksRequestHandler _getClassTestMarksRequestHandler;
    private readonly GetTermResultSheetRequestHandler _getExamTermResultSheetRequestHandler;
    private readonly GetStudentAddressRequestHandler _getStudentAddressRequestHandler;
    private readonly ShowStudentAddressResponsePresenter _showStudentAddressResponsePresenter;
    private readonly GetStudentContactPersonsRequestHandler _getStudentContactPersonsRequestHandler;
    private readonly ShowStudentContactPersonsResponsePresenter _showStudentContactPersonsResponsePresenter;
    private readonly GetStudentAttendanceListResponsePresenter _getStudentAttendanceListResponsePresenter;
    private readonly ICurrentSessionProvider _currentSessionProvider;
    private readonly ShowStudentSectionRollRequestHandler _showStudentSectionRollRequestHandler;
    private readonly GetAttendanceListByDateRangeRequestHandler _getAttendanceListByDateRange;

    public StudentController(
      DisplayAddStudentPageRequestHandler displayAddStudentPageRequestHandler,
      DisplayAddStudentPageResponsePresenter displayAddStudentPageResponsePresenter,
      AddStudentRequestHandler addStudentRequestHandler,
      AddStudentResponsePresenter presenter,
      ShowStudentRequestHandler showStudentRequestHandler,
      ShowStudentResponsePresenter showStudentResponsePresenter, 
      ShowStudentListRequestHandler showStudentListRequestHandler, 
      ShowStudentListResponsePresenter showStudentListResponsePresenter, 
      EditStudentRequestHandler editStudentRequestHandler, 
      EditStudentResponsePresenter editStudentResponsePresenter,
      AddStudentContactPersonImageRequestHandler addStudentContactPersonImageRequestHandler,
      AddOrChangeStudentImageRequestHandler addOrChangeStudentImageRequestHandler,
      AddStudentContactPersonRequestHandler addStudentContactPersonRequestHandler,
      AddStudentContactPersonResponsePresenter addStudentContactPersonResponsePresenter,
      AddStudentAttendanceRequestHandler addStudentAttendanceRequestHandler,
      AddStudentAttendanceResponsePresenter addStudentAttendanceResponsePresenter,
      ShowInstituteListRequestHandler showInstituteListRequestHandler,
      ShowInstituteListResponsePresenter showInstituteListResponsePresenter,
      ShowCurrentMonthAttendanceListRequestHandler showCurrentMonthAttendanceListRequestHandler,
      ShowStudentAttendanceListResponsePresenter showStudentAttendanceListResponsePresenter,
      ShowStudentAttendanceRequestHandler showStudentAttendanceRequestHandler,
      ShowStudentAttendanceResponsePresenter showStudentAttendanceResponsePresenter,
      EditStudentAttendanceRequestHandler editStudentAttendanceRequestHandler,
      EditStudentAttendanceResponsePresenter editStudentAttendanceResponsePresenter,
      IHostingEnvironment appEnvironment, ShowClassesListRequestHandler showClassesListRequestHandler,
      ShowBranchMediumSectionListRequestHandler showSectionListRequestHandler, 
      ShowBranchMediumRequestHandler showBranchMediumRequestHandler, 
      AddStudentAddressRequestHandler addStudentAddressRequestHandler, 
      AddStudentAddressResponsePresenter addStudentAddressResponsePresenter,
      GetUserAssociationRequestHandler getUserAssociationRequestHandler, 
      GetBranchMediumSessionsRequestHandler getBranchMediumSessionsRequestHandler, 
      GetExamTermsRequestHandler getExamTermsRequestHandler, 
      GetExamTermMarksRequestHandler getExamTermMarksRequestHandler,
      GetClassTestMarksRequestHandler getClassTestMarksRequestHandler,
      GetTermResultSheetRequestHandler getExamTermResultSheetRequestHandler, 
      GetStudentAddressRequestHandler getStudentAddressRequestHandler, 
      ShowStudentAddressResponsePresenter showStudentAddressResponsePresenter, 
      GetStudentContactPersonsRequestHandler getStudentContactPersonsRequestHandler, 
      ShowStudentContactPersonsResponsePresenter showStudentContactPersonsResponsePresenter, 
      GetStudentAttendanceListResponsePresenter getStudentAttendanceListResponsePresenter,
      ICurrentSessionProvider currentSessionProvider,
      DisplayEditStudentPageResponsePresenter displayEditStudentPageResponsePresenter,
      ShowStudentSectionRollRequestHandler showStudentSectionRollRequestHandler, 
      GetAttendanceListByDateRangeRequestHandler getAttendanceListByDateRange) : base(getUserAssociationRequestHandler
      )

    {
      _displayAddStudentPageRequestHandler = displayAddStudentPageRequestHandler;
      _displayAddStudentPageResponsePresenter = displayAddStudentPageResponsePresenter;
      _addStudentRequestHandler = addStudentRequestHandler;
      _presenter = presenter;
      _showStudentRequestHandler = showStudentRequestHandler;
      _showStudentResponsePresenter = showStudentResponsePresenter;
      _showStudentListRequestHandler = showStudentListRequestHandler;
      _showStudentListResponsePresenter = showStudentListResponsePresenter;
      _editStudentRequestHandler = editStudentRequestHandler;
      _editStudentResponsePresenter = editStudentResponsePresenter;
      _addStudentContactPersonImageRequestHandler = addStudentContactPersonImageRequestHandler;
      _appEnvironment = appEnvironment;
      _showSectionListRequestHandler = showSectionListRequestHandler;
      _showBranchMediumRequestHandler = showBranchMediumRequestHandler;
      _addStudentAddressRequestHandler = addStudentAddressRequestHandler;
      _addStudentAddressResponsePresenter = addStudentAddressResponsePresenter;
      _getBranchMediumSessionsRequestHandler = getBranchMediumSessionsRequestHandler;
      _getExamTermsRequestHandler = getExamTermsRequestHandler;
      _getExamTermMarksRequestHandler = getExamTermMarksRequestHandler;
      _addOrChangeStudentImageRequestHandler = addOrChangeStudentImageRequestHandler;
      _addStudentContactPersonRequestHandler = addStudentContactPersonRequestHandler;
      _addStudentContactPersonResponsePresenter = addStudentContactPersonResponsePresenter;
      _addStudentAttendanceRequestHandler = addStudentAttendanceRequestHandler;
      _addStudentAttendanceResponsePresenter = addStudentAttendanceResponsePresenter;
      _showInstituteListRequestHandler = showInstituteListRequestHandler;
      _showInstituteListResponsePresenter = showInstituteListResponsePresenter;
      _showCurrentMonthAttendanceListRequestHandler = showCurrentMonthAttendanceListRequestHandler;
      _showStudentAttendanceListResponsePresenter = showStudentAttendanceListResponsePresenter;
      _showStudentAttendanceRequestHandler = showStudentAttendanceRequestHandler;
      _showStudentAttendanceResponsePresenter = showStudentAttendanceResponsePresenter;
      _editStudentAttendanceRequestHandler = editStudentAttendanceRequestHandler;
      _editStudentAttendanceResponsePresenter = editStudentAttendanceResponsePresenter;
      _getClassTestMarksRequestHandler = getClassTestMarksRequestHandler;
      _getExamTermResultSheetRequestHandler = getExamTermResultSheetRequestHandler;
      _getStudentAddressRequestHandler = getStudentAddressRequestHandler;
      _showStudentAddressResponsePresenter = showStudentAddressResponsePresenter;
      _getStudentContactPersonsRequestHandler = getStudentContactPersonsRequestHandler;
      _showStudentContactPersonsResponsePresenter = showStudentContactPersonsResponsePresenter;
      _getStudentAttendanceListResponsePresenter = getStudentAttendanceListResponsePresenter;
      _currentSessionProvider = currentSessionProvider;
      _showStudentSectionRollRequestHandler = showStudentSectionRollRequestHandler;
      _getAttendanceListByDateRange = getAttendanceListByDateRange;
      _displayEditStudentPageResponsePresenter = displayEditStudentPageResponsePresenter;
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Student, (int)Feature.StudentEnum.NewAdmission)]
    [HttpGet]
    public ActionResult AddStudent(long branchMediumId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new DisplayAddStudentPageRequestMessage();

      var model = new AddStudentViewModel
      {
        LoginUsersBranchMediumId = branchMediumId,
        BranchMediumId = branchMediumId
      };

      var response = _displayAddStudentPageRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _displayAddStudentPageResponsePresenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());
      long currentSessionId = _currentSessionProvider.GetCurrentSession(branchMediumId).Id;
      viewModel.Sections = GetSectionOfBranchMedium(branchMediumId).Where(s => s.Session.Id == currentSessionId);
      viewModel.Group = (GroupEnum) (-1);
      viewModel.Groups = GetFilteredGroupBySection(viewModel.Sections);
      return View(viewModel);
    }

    private List<AddStudentViewModel.FilteredGroup> GetFilteredGroupBySection(IEnumerable<SectionMessageModel> sections)
    {
      var groups = new List<AddStudentViewModel.FilteredGroup>();
      foreach (var grp in sections.Select(x => x.Group).Distinct().ToList())
      {
        groups.Add(new AddStudentViewModel.FilteredGroup()
        {
          GroupId = (long)grp,
          GroupName = grp.ToString()
        });
      }

      return groups;
    }

    private List<EditStudentViewModel.FilteredGroup> GetFilteredGroupBySectionOfEdit(IEnumerable<SectionMessageModel> sections)
    {
      var groups = new List<EditStudentViewModel.FilteredGroup>();
      foreach (var grp in sections.Select(x => x.Group).Distinct().ToList())
      {
        groups.Add(new EditStudentViewModel.FilteredGroup()
        {
          GroupId = (long)grp,
          GroupName = grp.ToString()
        });
      }

      return groups;
    }
    public List<AddStudentViewModel.FilteredGroup> GetFilteredGroupBySectionByClass(long branchMediumId,long classId)
    {
      long currentSessionId = _currentSessionProvider.GetCurrentSession(branchMediumId).Id;
      var sections = GetSectionOfBranchMedium(branchMediumId).Where(s => s.Session.Id == currentSessionId);
      var groups = new List<AddStudentViewModel.FilteredGroup>();
      foreach (var grp in sections.Where(x=>x.Class.Id == classId).Select(x => x.Group).Distinct().ToList())
      {
        groups.Add(new AddStudentViewModel.FilteredGroup()
        {
          GroupId = (long)grp,
          GroupName = grp.ToString()
        });
      }

      return groups;
    } 

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Student, (int)Feature.StudentEnum.NewAdmission)]
    [HttpPost]
    public ActionResult AddStudent(AddStudentViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new AddStudentRequestMessage();

      requestMessage.FullName = model.FullName;
      requestMessage.Guardian = model.Guardian;
      requestMessage.BirthDate = model.BirthDate;
      requestMessage.Email = model.Email;
      requestMessage.MobileNo = model.MobileNo;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;
      requestMessage.BranchMediumId = model.BranchMediumId;
      requestMessage.ClassId = model.ClassId;
      requestMessage.SectionId = Convert.ToInt64(model.SectionId);
      requestMessage.Group = model.Group;
      requestMessage.RollNo = model.RollNo;


      var response = _addStudentRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _presenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());
      if (!response.Result.ValidationResult.IsValid)
      {
        long currentSessionId = _currentSessionProvider.GetCurrentSession(model.BranchMediumId).Id;
        viewModel.Sections = GetSectionOfBranchMedium(model.BranchMediumId).Where(s => s.Session.Id == currentSessionId);
        viewModel.Group = (GroupEnum)(-1);
        viewModel.BranchMediumId = model.BranchMediumId;
        viewModel.Groups = GetFilteredGroupBySection(viewModel.Sections);
        return View(viewModel);
      }

      return RedirectToAction("ViewStudent", new {studentId = response.Result.Data.StudentId, branchMediumId = model.BranchMediumId});
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Student, (int)Feature.StudentEnum.FindStudent)]
    public ActionResult ViewStudent(long studentId, long branchMediumId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowStudentRequestMessage
      {
        StudentId = studentId,
        BranchMediumId = branchMediumId
      };
      var response = _showStudentRequestHandler.Handle(requestMessage, cancellationToken);
      var model = new StudentViewModel();
      var viewModel = _showStudentResponsePresenter.Handle(response.Result.Data, model, GetUserAssociationResponseMessage());
      viewModel.BranchMediumId = branchMediumId;
      return View(viewModel);
    }

    private IEnumerable<ShowCurrentMonthAttendanceListResponseMessage.Attendance> GetStudentAttendanceList(long studentId)
    {
      var cancellationToken = new CancellationToken();

      var showStudentAttendanceListRequestMessage = new ShowCurrentMonthAttendanceListRequestMessage();
      showStudentAttendanceListRequestMessage.StudentId = studentId;
      List<StudentAttendanceViewModel> showStudentAttendanceListModel = new List<StudentAttendanceViewModel>();
      var response = _showCurrentMonthAttendanceListRequestHandler.Handle(showStudentAttendanceListRequestMessage, cancellationToken);
      return response.Result.AttendanceList;
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Student, (int)Feature.StudentEnum.UpdateStudent)]
    [HttpGet]
    public ActionResult UpdateStudent(long studentId,long branchMediumId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowStudentRequestMessage {StudentId = studentId,BranchMediumId = branchMediumId};
      var response = _showStudentRequestHandler.Handle(requestMessage, cancellationToken);
      
      var viewModel = _showStudentResponsePresenter.Handle(response.Result.Data, new StudentViewModel(), GetUserAssociationResponseMessage());
      var model = _displayEditStudentPageResponsePresenter.Handle(viewModel,new EditStudentViewModel(), GetUserAssociationResponseMessage());
      long currentSessionId = _currentSessionProvider.GetCurrentSession(branchMediumId).Id;
      model.Sections = GetSectionOfBranchMedium(branchMediumId).Where(s => s.Session.Id == currentSessionId);
      model.Groups = GetFilteredGroupBySectionOfEdit(model.Sections);
      model.BranchMediumId = branchMediumId;
      model.Id = studentId;
      return View(model);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Student, (int)Feature.StudentEnum.UpdateStudent)]
    [HttpPost]
    public ActionResult UpdateStudent(EditStudentViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new EditStudentRequestMessage();

      requestMessage.StudentId = model.Id;
      requestMessage.Guardian = (RelationTypeEnum) model.Guardian;
      requestMessage.BirthDate = model.BirthDate;
      requestMessage.FullName = model.FullName;
      requestMessage.Email = model.Email;
      requestMessage.ClassId = model.ClassId;
      requestMessage.SectionId = Convert.ToInt64(model.SectionId);
      requestMessage.Group = (GroupEnum) model.Group;
      requestMessage.MobileNo = model.MobileNo;
      requestMessage.RollNo = model.RollNo;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _editStudentRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _editStudentResponsePresenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());
      if (!response.Result.ValidationResult.IsValid)
      {
        long currentSessionId = _currentSessionProvider.GetCurrentSession(model.BranchMediumId).Id;
        viewModel.Sections = GetSectionOfBranchMedium(model.BranchMediumId).Where(s => s.Session.Id == currentSessionId);
        viewModel.Groups = GetFilteredGroupBySectionOfEdit(model.Sections);
        viewModel.BranchMediumId = model.BranchMediumId;
        viewModel.Id = model.Id;
        return View(viewModel);
      }

      return RedirectToAction("ViewStudent", new {studentId = model.Id, branchMediumId = model.BranchMediumId });
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Student, (int)Feature.StudentEnum.NewAdmission)]
    [HttpGet]
    public ActionResult UploadStudentImage(long studentId, long branchMediumId)
    {
      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();
      return View( new AddOrChangeStudentImageViewModel
      {
        StudentId = studentId,
        LoginName = userAssociationMessage.LoginName,
        AssociatedWith = userAssociationMessage.AssociatedWith,
        LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId,
        LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId,
        RoleFeatures = userAssociationMessage.RoleFeatures,
        LoginUsersBranchMediumId = branchMediumId,
        BranchMediumId = branchMediumId
      });
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Student, (int)Feature.StudentEnum.NewAdmission)]
    [HttpPost]
    public ActionResult UploadStudentImage(AddOrChangeStudentImageViewModel model)
    {
      var files = HttpContext.Request.Form.Files;
      foreach (var image in files)
      {
        if (image != null && image.Length > 0)
        {
          var file = image;
          var uploads = Path.Combine(_appEnvironment.WebRootPath, "Uploads\\Student\\");
          if (!Directory.Exists(uploads))
            Directory.CreateDirectory(uploads);
          if (file.Length > 0)
          {
            var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
            using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
            {
              file.CopyTo(fileStream);
              var cancellationToken = new CancellationToken();
              var requestMessage = new AddOrChangeStudentImageRequestMessage();
              requestMessage.StudentId = model.StudentId;
              requestMessage.ImageUrl = fileName;
              requestMessage.CurrentUserName = HttpContext.User.Identity.Name;
              var response = _addOrChangeStudentImageRequestHandler.Handle(requestMessage, cancellationToken);
              if (!string.IsNullOrEmpty(response.Result.PreviousImage))
              {
                var previousImageFile = Path.Combine(_appEnvironment.WebRootPath, "Uploads\\Student\\") + response.Result.PreviousImage;
                if(System.IO.File.Exists(previousImageFile)){ System.IO.File.Delete(previousImageFile);}
              }
            }
          }
        }
      }

      return RedirectToAction("ViewStudent", new {studentId = model.StudentId, branchMediumId = model.BranchMediumId});
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Student, (int)Feature.StudentEnum.AddContactPerson)]
    public ActionResult ContactPersonImageUpload(long contactPersonId)
    {
      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();
      var model = new StudentContactPersonViewModel
      {
        Id = contactPersonId,
        LoginName = userAssociationMessage.LoginName,
        AssociatedWith = userAssociationMessage.AssociatedWith,
        LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId,
        LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId,
        LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId,
        RoleFeatures = userAssociationMessage.RoleFeatures
      };
      return View(model);

    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Student, (int)Feature.StudentEnum.AddContactPerson)]
    [HttpPost]
    public ActionResult ContactPersonImageUpload(StudentContactPersonViewModel model)
    {
      var files = HttpContext.Request.Form.Files;
      foreach (var image in files)
      {
        if (image != null && image.Length > 0)
        {
          var file = image;
          var uploads = Path.Combine(_appEnvironment.WebRootPath, "Uploads\\ContactPerson\\");
          if (!Directory.Exists(uploads))
            Directory.CreateDirectory(uploads);
          if (file.Length > 0)
          {
            var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
            using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
            {
              file.CopyTo(fileStream);
              var cancellationToken = new CancellationToken();
              var requestMessage = new AddStudentContactPersonImageRequestMessage();
              requestMessage.ContactPersonId = model.Id;
              requestMessage.ImageUrl = fileName;
              requestMessage.CurrentUserName = HttpContext.User.Identity.Name;
              var response = _addStudentContactPersonImageRequestHandler.Handle(requestMessage, cancellationToken);
              if (!string.IsNullOrEmpty(response.Result.PreviousImage))
              {
                var previousImageFile = Path.Combine(_appEnvironment.WebRootPath, "Uploads\\ContactPerson\\") + response.Result.PreviousImage;
                if(System.IO.File.Exists(previousImageFile)){ System.IO.File.Delete(previousImageFile);}
              }
            }

          }
        }
      }
      return RedirectToAction("ViewStudent", new {studentId = model.StudentId, branchMediumId = 2});
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Student, (int)Feature.StudentEnum.AddContactPerson)]
    public ActionResult AddStudentContactPerson(long studentId, long branchMediumId)
    {
      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();
      var model = new AddStudentContactPersonViewModel
      {
        StudentId = studentId,
        LoginName = userAssociationMessage.LoginName,
        AssociatedWith = userAssociationMessage.AssociatedWith,
        LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId,
        LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId,
        RoleFeatures = userAssociationMessage.RoleFeatures,
        LoginUsersBranchMediumId = branchMediumId,
        BranchMediumId = branchMediumId
      };
      return View(model);

    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Student, (int)Feature.StudentEnum.AddContactPerson)]
    [HttpPost]
    public ActionResult AddStudentContactPerson(AddStudentContactPersonViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new AddStudentContactPersonRequestMessage();
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;
      requestMessage.Designation = model.Designation;
      requestMessage.EmailAddress = model.EmailAddress;
      requestMessage.Name = model.Name;
      requestMessage.NID = model.NID;
      requestMessage.OfficeContactNo = model.OfficeContactNo;
      requestMessage.OfficeNameAddress = model.OfficeNameAddress;
      requestMessage.PhoneNo = model.PhoneNo;
      requestMessage.Profession = model.Profession;
      requestMessage.RelationType = model.RelationType;
      requestMessage.Status = StatusEnum.Active;
      requestMessage.StudentId = model.StudentId;

      var response = _addStudentContactPersonRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _addStudentContactPersonResponsePresenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());
      if (!response.Result.ValidationResult.IsValid)
      {
        viewModel.LoginUsersBranchMediumId = model.LoginUsersBranchMediumId;
        viewModel.BranchMediumId = model.BranchMediumId;
        return View(viewModel);
      }

      return Redirect("/Student/ViewStudent?studentId="+model.StudentId+"&branchMediumId="+model.BranchMediumId+"#tab_contactPerson");
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Student, (int)Feature.StudentEnum.AddAttendance)]
    public ActionResult AddStudentAttendance(long studentId, long branchMediumId)
    {
      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();
      var model = new AddStudentAttendanceViewModel
      {
        StudentId = studentId,
        LoginName = userAssociationMessage.LoginName,
        AssociatedWith = userAssociationMessage.AssociatedWith,
        LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId,
        LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId,
        RoleFeatures = userAssociationMessage.RoleFeatures,
        LoginUsersBranchMediumId = branchMediumId,
        BranchMediumId = branchMediumId
      };

      return View(model);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Student, (int)Feature.StudentEnum.AddAttendance)]
    [HttpPost]
    public ActionResult AddStudentAttendance(AddStudentAttendanceViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new AddStudentAttendanceRequestMessage();
      requestMessage.InTime = model.InTime;
      requestMessage.OutTime = model.OutTime;
      requestMessage.AttendanceDate = model.AttendanceDate;
      requestMessage.StudentId = model.StudentId;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _addStudentAttendanceRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _addStudentAttendanceResponsePresenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());
      if (!response.Result.ValidationResult.IsValid)
      {
        viewModel.LoginUsersBranchMediumId = model.LoginUsersBranchMediumId;
        viewModel.BranchMediumId = model.BranchMediumId;
        return View(viewModel);
      }

      return Redirect("/Student/ViewStudent?studentId="+model.StudentId+"&branchMediumId="+model.BranchMediumId+"#tab_attendance");
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Student, (int)Feature.StudentEnum.ViewAttendance)]
    public ActionResult ShowStudentAttendance()
    {
      var cancellationToken = new CancellationToken();
      var model = new StudentAttendanceListViewModel();

      var showInstituteListRequestMessage = new ShowInstituteListRequestMessage();
      ShowInstituteListViewModel showInstituteListModel = new ShowInstituteListViewModel();
      var showInstituteListResponse = _showInstituteListRequestHandler.Handle(showInstituteListRequestMessage, cancellationToken);
      var pickedInstituteListViewModel = _showInstituteListResponsePresenter.Handle(showInstituteListResponse.Result, showInstituteListModel, GetUserAssociationResponseMessage());

      model.LoginName = pickedInstituteListViewModel.LoginName;
      model.AssociatedWith = pickedInstituteListViewModel.AssociatedWith;
      model.LoginUsersInstituteId = pickedInstituteListViewModel.LoginUsersInstituteId;
      model.LoginUsersBranchId = pickedInstituteListViewModel.LoginUsersBranchId;
      model.LoginUsersBranchMediumId = pickedInstituteListViewModel.LoginUsersBranchMediumId;
      model.RoleFeatures = pickedInstituteListViewModel.RoleFeatures;
      model.InstituteList = pickedInstituteListViewModel.InstituteViewModelList;
      return View(model);
    }

    [HttpPost]
    public IEnumerable<StudentAttendanceViewModel> ShowStudentAttendanceList([FromBody]StudentAttendanceListViewModel model)
    {
      var cancellationToken = new CancellationToken();

      var showStudentAttendanceListRequestMessage = new ShowCurrentMonthAttendanceListRequestMessage();
      showStudentAttendanceListRequestMessage.StudentId = model.StudentId;
      showStudentAttendanceListRequestMessage.AttendanceStartDate = model.AttendanceStartDate;
      showStudentAttendanceListRequestMessage.AttendanceEndDate = model.AttendanceEndDate;
      List<StudentAttendanceViewModel> showStudentAttendanceListModel = new List<StudentAttendanceViewModel>();
      var response = _showCurrentMonthAttendanceListRequestHandler.Handle(showStudentAttendanceListRequestMessage, cancellationToken);
      var pickedStudentAttendanceListViewModel = _showStudentAttendanceListResponsePresenter.Handle(response.Result, showStudentAttendanceListModel);

      if (pickedStudentAttendanceListViewModel == null)
        return new List<StudentAttendanceViewModel>();
      else
        return pickedStudentAttendanceListViewModel;
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Student, (int)Feature.StudentEnum.EditAttendace)]
    [HttpGet]
    public ActionResult UpdateStudentAttendance(long attendanceId, long studentId, long branchMediumId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowStudentAttendanceRequestMessage();
      var model = new StudentAttendanceViewModel();
      requestMessage.StudentAttendanceId = attendanceId;

      var response = _showStudentAttendanceRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _showStudentAttendanceResponsePresenter.Handle(response.Result, model, GetUserAssociationResponseMessage());
      viewModel.StudentId = studentId;
      viewModel.BranchMediumId = branchMediumId;
      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Student, (int)Feature.StudentEnum.EditAttendace)]
    [HttpPost]
    public ActionResult UpdateStudentAttendance(StudentAttendanceViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new EditStudentAttendanceRequestMessage();

      requestMessage.Id = model.Id;
      requestMessage.AttendanceDate = model.AttendanceDate;
      requestMessage.StudentId = model.Student.Id;
      requestMessage.InTime = model.InTime;
      requestMessage.OutTime = model.OutTime;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _editStudentAttendanceRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _editStudentAttendanceResponsePresenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());

      if (response.Result.ValidationResult.IsValid)
      {
        return RedirectToAction("ViewStudent", new { studentId = model.StudentId, branchMediumId = model.BranchMediumId });
      }

      viewModel.StudentId = model.StudentId;
      viewModel.BranchMediumId = model.BranchMediumId;
      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Student, (int)Feature.StudentEnum.AddAddress)]
    [HttpGet]
    public ActionResult AddStudentAddress(long studentId, long branchMediumId)
    {
      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();
      var model = new AddStudentAddressViewModel
      {
        LoginName = userAssociationMessage.LoginName,
        AssociatedWith = userAssociationMessage.AssociatedWith,
        LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId,
        LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId,
        RoleFeatures = userAssociationMessage.RoleFeatures,
        StudentId = studentId,
        LoginUsersBranchMediumId = branchMediumId,
        BranchMediumId = branchMediumId
      };
      return View(model);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Student, (int)Feature.StudentEnum.AddAddress)]
    [HttpPost]
    public ActionResult AddStudentAddress(AddStudentAddressViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new AddStudentAddressRequestMessage();
      requestMessage.StudentId = model.StudentId;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;
      requestMessage.PermanentAddressCity = model.PermanentAddressCity;
      requestMessage.PermanentAddressPostalCode = model.PermanentAddressPostalCode;
      requestMessage.PermanentAddressStreet1 = model.PermanentAddressStreet1;
      requestMessage.PermanentAddressStreet2 = model.PermanentAddressStreet2;
      requestMessage.PresentAddressCity = model.PresentAddressCity;
      requestMessage.PresentAddressPostalCode = model.PresentAddressPostalCode;
      requestMessage.PresentAddressStreet1 = model.PresentAddressStreet1;
      requestMessage.PresentAddressStreet2 = model.PresentAddressStreet2;
      requestMessage.SameAsPermanentAddress = model.SameAsPermanentAddress;
      
      var response = _addStudentAddressRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _addStudentAddressResponsePresenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());

      if (response.Result.ValidationResult.IsValid)
      {
        return Redirect("/Student/ViewStudent?studentId="+model.StudentId+"&branchMediumId="+model.BranchMediumId+"#tab_address");
      }

      viewModel.BranchMediumId = model.BranchMediumId;
      viewModel.StudentId = model.StudentId;
      viewModel.LoginUsersBranchMediumId = model.LoginUsersBranchMediumId;
      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Student, (int)Feature.StudentEnum.FindAttendance)]
    public ActionResult ViewStudentAttendance(long studentAttendanceId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowStudentAttendanceRequestMessage();
      requestMessage.StudentAttendanceId = studentAttendanceId;
      StudentAttendanceViewModel model = new StudentAttendanceViewModel();

      var response = _showStudentAttendanceRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _showStudentAttendanceResponsePresenter.Handle(response.Result, model, GetUserAssociationResponseMessage());

      return View(viewModel);
    }
    
    private IEnumerable<SectionMessageModel> GetSectionOfBranchMedium(long branchMediumId)
    {
      var cancellationToken = new CancellationToken();
      var branchMediumRequestMessage = new ShowBranchMediumRequestMessage{BranchMediumId = branchMediumId};
      var branchMedium = _showBranchMediumRequestHandler.Handle(branchMediumRequestMessage, cancellationToken).Result.BranchMedium;
      var sectionListRequestMessage = new ShowBranchMediumSectionListRequestMessage
      {
        BranchId = branchMedium.Branch.Id,
        MediumId = branchMedium.Medium.Id
      };

      return _showSectionListRequestHandler.Handle(sectionListRequestMessage, cancellationToken).Result.Sections;
    }
    

    private IEnumerable<SessionMessageModel> GetBranchMediumSessions(long branchMediumId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new GetBranchMediumSessionsRequestMessage
      {
        BranchMediumId = branchMediumId
      };
      var branchMediumSessions = _getBranchMediumSessionsRequestHandler.Handle(requestMessage, cancellationToken)
        .Result
        .Sessions;
      if (branchMediumSessions != null) 
        return branchMediumSessions;
      return new List<SessionMessageModel>();
    }

    public JsonResult GetExamTerms(long sessionId, long branchMediumId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new GetExamTermsRequestMessage
      {
        BranchMediumId = branchMediumId,
        SessionId = sessionId
      };
      var response = _getExamTermsRequestHandler.Handle(requestMessage, cancellationToken);
      return Json(response.Result.ExamTerms);
    }
    
    public JsonResult GetExamTermMarks(long studentId,  long examTermId, long sectionId, long classId)
    {
      if (examTermId > 0 && studentId > 0 && sectionId > 0 && classId > 0)
      {
        var cancellationToken = new CancellationToken();
        var requestMessage = new GetExamTermMarksRequestMessage
        {
          ExamTermId = examTermId,
          StudentId = studentId,
          SectionId = sectionId,
          ClassId = classId
        };
        var response = _getExamTermMarksRequestHandler.Handle(requestMessage, cancellationToken)
          .Result
          .TermMarksResponseObject;

        return Json(response);
      }

      return Json(null);
    }

    public ActionResult ShowStudentClassTestMark(long branchMediumId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new GetBranchMediumSessionsRequestMessage
      {
        BranchMediumId = branchMediumId
      };
      var response = _getBranchMediumSessionsRequestHandler.Handle(requestMessage, cancellationToken);
      var displayClassTestMarkViewModel = new DisplayClassTestMarkViewModel();
      displayClassTestMarkViewModel.Sessions = response.Result.Sessions as List<SessionMessageModel>;

      return PartialView("ShowStudentClassTestMark", displayClassTestMarkViewModel);
    }

    public JsonResult GetClassTestMarks(long studentId, long termId)
    {
      if (termId > 0 && studentId > 0)
      {
        var cancellationToken = new CancellationToken();
        var requestMessage = new GetClassTestMarksRequestMessage
        {
          TermId = termId,
          StudentId = studentId
        };
        var response = _getClassTestMarksRequestHandler.Handle(requestMessage, cancellationToken)
          .Result
          .ClassTestResponseObject;

        return Json(response);
      }

      return Json(null);
    }

    public JsonResult GetTermResultSheet(long sessionId, long examTermId, long studentId)
    {
      if (examTermId > 0 && studentId > 0 && sessionId > 0)
      {
        var cancellationToken = new CancellationToken();
        var requestMessage = new GetTermResultSheetRequestMessage
        {
          ExamTermId = examTermId,
          StudentId = studentId,
          SessionId = sessionId
        };
        var response = _getExamTermResultSheetRequestHandler.Handle(requestMessage, cancellationToken).Result.TermResult;

        return Json(response);
      }

      return Json(null);
    }
    
    public ActionResult LoadStudentAddressPartialView(long studentId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new GetStudentAddressRequestMessage
      {
        StudentId = studentId
      };
      var response = _getStudentAddressRequestHandler.Handle(requestMessage, cancellationToken);
      var studentAddressModel = _showStudentAddressResponsePresenter.Handle(response.Result, new AddressViewModel());

      return PartialView("StudentAddress", studentAddressModel);
    }
    
    public ActionResult LoadStudentContactPersons(long studentId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new GetStudentContactPersonsRequestMessage
      {
        StudentId = studentId
      };
      var response = _getStudentContactPersonsRequestHandler.Handle(requestMessage, cancellationToken);
      var studentAddressModel = _showStudentContactPersonsResponsePresenter.Handle(response.Result, new ContactPersonViewModel());

      return PartialView("StudentContactPerson", studentAddressModel);
    }
    
    public JsonResult LoadStudentAttendance(long studentId)
    { 
      if (studentId > 0)
      {
        var cancellationToken = new CancellationToken();
        var requestMessage = new ShowCurrentMonthAttendanceListRequestMessage()
        {
          StudentId = studentId
        };
        var response = _showCurrentMonthAttendanceListRequestHandler.Handle(requestMessage, cancellationToken);
        return Json(response.Result.AttendanceList);
      }

      return Json(new {studentIdErrorMessage = "Invalid Student Id : "+studentId});

    }
    
    public JsonResult GetStudentAttendanceByDateRange(long studentId, DateTime? startDate, DateTime? endDate)
    {
      if (studentId > 0)
      {
        if (startDate != null && endDate != null)
        {
          if (startDate > endDate)
            return Json(new {invalidDateRangeErrorMessage = "End Date must be greater than Start Date"});
          var cancellationToken = new CancellationToken();
          var requestMessage = new GetAttendanceListByDateRangeRequestMessage
          {
            StudentId = studentId,
            AttendanceEndDate = (DateTime) endDate,
            AttendanceStartDate = (DateTime) startDate
          };
          var response = _getAttendanceListByDateRange.Handle(requestMessage, cancellationToken);
          if (response.Result.AttendanceList != null && response.Result.AttendanceList.Any())
          {
            return Json(response.Result.AttendanceList);
          }
        }
        else
        {
          return Json(new {enterDateRangeErrorMessage = "Enter Date Range"});
        }
      }
      else
      {
        return Json(new {studentIdErrorMessage = "Invalid Student Id : "+studentId});
      }

      return null;
    }
    
    public ActionResult LoadTermTestMarksPartialView(long branchMediumId, long studentId)
    {
      var branchMediumSessions = GetBranchMediumSessions(branchMediumId);
      var studentMarkSheetViewModel = new StudentMarkSheetViewModel
      {
        Sessions = branchMediumSessions,
        BranchMediumId = branchMediumId,
        StudentId = studentId
      };
      return PartialView("TermTestMarkSheet", studentMarkSheetViewModel);
    }
    
    public ActionResult LoadTermTestResultSheetPartialView(long branchMediumId, long studentId)
    {
      var branchMediumSessions = GetBranchMediumSessions(branchMediumId);
      var studentResultSheetViewModel = new StudentResultSheetViewModel()
      {
        Sessions = branchMediumSessions,
        BranchMediumId = branchMediumId,
        StudentId = studentId
      };
      return PartialView("StudentResultSheet", studentResultSheetViewModel);
    }

    public JsonResult GetStudentRoll(long sectionId, long classId,GroupEnum group)
    {
      if (sectionId > 0 && classId > 0)
      {
        var cancellationToken = new CancellationToken();
        var requestMessage = new ShowStudentSectionRollRequestMessage()
        {
          ClassId = classId,
          SectionId = sectionId,
          Group = group
        };
        var response = _showStudentSectionRollRequestHandler.Handle(requestMessage, cancellationToken)
          .Result
          .Roll;

        return Json(response);
      }

      return Json(0);
    }

  }
}