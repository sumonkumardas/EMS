using System.Collections.Generic;
using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Features;
using SinePulse.EMS.Domain.Students;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.ClassMessages;
using SinePulse.EMS.Messages.ClassRoutineMessages;
using SinePulse.EMS.Messages.ClassTestMessages;
using SinePulse.EMS.Messages.RoomMessages;
using SinePulse.EMS.Messages.SectionMessages;
using SinePulse.EMS.Messages.SessionMessages;
using SinePulse.EMS.Site.Authorization;
using SinePulse.EMS.Site.Models;
using SinePulse.EMS.Site.Presenters;
using SinePulse.EMS.UseCases.Authorization;
using SinePulse.EMS.UseCases.Classes;
using SinePulse.EMS.UseCases.ClassRoutines;
using SinePulse.EMS.UseCases.ClassTests;
using SinePulse.EMS.UseCases.Rooms;
using SinePulse.EMS.UseCases.Sections;
using SinePulse.EMS.UseCases.Sessions;

namespace SinePulse.EMS.Site.Controllers
{
  [Authorize]
  public class SectionController : BaseController
  {
    private readonly AddSectionRequestHandler _addSectionRequestHandler;
    private readonly AddSectionResponsePresenter _addSectionResponsePresenter;
    private readonly ShowSectionRequestHandler _showSectionRequestHandler;
    private readonly ShowSectionResponsePresenter _showSectionResponsePresenter;
    private readonly EditSectionRequestHandler _editSectionRequestHandler;
    private readonly EditSectionResponsePresenter _editSectionResponsePresenter;
    private readonly ShowClassesListRequestHandler _showClassesListRequestHandler;
    private readonly ShowClassesListResponsePresenter _showClassesListResponsePresenter;
    private readonly ShowCurrentSessionListRequestHandler _showSessionListRequestHandler;
    private readonly ShowCurrentSessionListResponsePresenter _showSessionListResponsePresenter;
    private readonly ShowClassRoutineListRequestHandler _showClassRoutineListRequestHandler;
    private readonly ShowClassRoutineListResponsePresenter _showClassRoutineListResponsePresenter;
    private readonly ShowRoomsListRequestHandler _showRoomsListRequestHandler;
    private readonly ShowRoomsListResponsePresenter _showRoomsListResponsePresenter;
    private readonly AssignOrChangeRoomInSectionRequestHandler _assignOrChangeSectionRequestHandler;
    private readonly AssignOrChangeRoomInSectionResponsePresenter _assignOrChangeSectionResponsePresenter;
    private readonly ShowClassTestListRequestHandler _showClassTestListRequestHandler;
    private readonly ShowClassTestListResponsePresenter _showClassTestListResponsePresenter;
    private readonly ShowTermTestEventListResponsePresenter _showTermTestEventListResponsePresenter;
    public SectionController(AddSectionRequestHandler addSectionRequestHandler, AddSectionResponsePresenter presenter,
  ShowSectionRequestHandler showSectionRequestHandler, ShowSectionResponsePresenter showSectionPresenter,
  EditSectionRequestHandler editSectionRequestHandler, EditSectionResponsePresenter editSectionResponsePresenter,
  ShowClassesListRequestHandler showClassesListRequestHandler,
  ShowClassesListResponsePresenter showClassesListResponsePresenter,
  ShowCurrentSessionListRequestHandler showSessionListRequestHandler,
  ShowCurrentSessionListResponsePresenter showSessionListResponsePresenter,
  ShowClassRoutineListRequestHandler showClassRoutineListRequestHandler,
  ShowClassRoutineListResponsePresenter showClassRoutineListResponsePresenter,
  ShowRoomsListRequestHandler showRoomsListRequestHandler,
  ShowRoomsListResponsePresenter showRoomsListResponsePresenter,
  AssignOrChangeRoomInSectionRequestHandler assignOrChangeSectionRequestHandler,
  AssignOrChangeRoomInSectionResponsePresenter assignOrChangeSectionResponsePresenter,
  ShowClassTestListRequestHandler showClassTestListRequestHandler,
  ShowClassTestListResponsePresenter showClassTestListResponsePresenter,
  ShowTermTestEventListResponsePresenter showTermTestEventListResponsePresenter,
  GetUserAssociationRequestHandler getUserAssociationRequestHandler) : base(getUserAssociationRequestHandler)
    {
      _addSectionRequestHandler = addSectionRequestHandler;
      _addSectionResponsePresenter = presenter;
      _showSectionRequestHandler = showSectionRequestHandler;
      _showSectionResponsePresenter = showSectionPresenter;
      _editSectionRequestHandler = editSectionRequestHandler;
      _editSectionResponsePresenter = editSectionResponsePresenter;
      _showClassesListResponsePresenter = showClassesListResponsePresenter;
      _showClassesListRequestHandler = showClassesListRequestHandler;
      _showSessionListRequestHandler = showSessionListRequestHandler;
      _showSessionListResponsePresenter = showSessionListResponsePresenter;
      _showClassRoutineListRequestHandler = showClassRoutineListRequestHandler;
      _showClassRoutineListResponsePresenter = showClassRoutineListResponsePresenter;
      _showRoomsListRequestHandler = showRoomsListRequestHandler;
      _showRoomsListResponsePresenter = showRoomsListResponsePresenter;
      _assignOrChangeSectionRequestHandler = assignOrChangeSectionRequestHandler;
      _assignOrChangeSectionResponsePresenter = assignOrChangeSectionResponsePresenter;
      _showClassTestListRequestHandler = showClassTestListRequestHandler;
      _showClassTestListResponsePresenter = showClassTestListResponsePresenter;
      _showTermTestEventListResponsePresenter = showTermTestEventListResponsePresenter;
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Section, (int)Feature.SectionEnum.NewSection)]
    [HttpGet]
    public ActionResult AddSection(long branchMediumId)
    {
      var cancellationToken = new CancellationToken();

      var showClassesListRequestMessage = new ShowClassesListRequestMessage();
      ShowClassesListViewModel showClassesViewModel = new ShowClassesListViewModel();
      var showClassesListResponse = _showClassesListRequestHandler.Handle(showClassesListRequestMessage, cancellationToken);
      var pickedClassesListViewModel = _showClassesListResponsePresenter.Handle(showClassesListResponse.Result,
        showClassesViewModel, GetUserAssociationResponseMessage());

      var showSessionListRequestMessage = new ShowCurrentSessionListRequestMessage { BranchMediumId = branchMediumId };
      ShowSessionListViewModel showSessionViewModel = new ShowSessionListViewModel();
      var showSessionListResponse = _showSessionListRequestHandler.Handle(showSessionListRequestMessage, cancellationToken);
      var pickedSessionListViewModel = _showSessionListResponsePresenter.Handle(showSessionListResponse.Result, showSessionViewModel, GetUserAssociationResponseMessage());

      var model = new AddSectionViewModel();
      model.BranchMediumId = branchMediumId;
      model.LoginName = pickedSessionListViewModel.LoginName;
      model.AssociatedWith = pickedSessionListViewModel.AssociatedWith;
      model.LoginUsersInstituteId = pickedSessionListViewModel.LoginUsersInstituteId;
      model.LoginUsersBranchId = pickedSessionListViewModel.LoginUsersBranchId;
      model.RoleFeatures = pickedSessionListViewModel.RoleFeatures;
      model.LoginUsersBranchMediumId = branchMediumId;
      model.Classes = pickedClassesListViewModel.Classes;
      model.Sessions = pickedSessionListViewModel.Sessions;
      return View(model);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Section, (int)Feature.SectionEnum.NewSection)]
    [HttpPost]
    public ActionResult AddSection(AddSectionViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new AddSectionRequestMessage
      {
        BranchMediumId = model.BranchMediumId,
        ClassId = model.ClassId,
        DurationOfClass = model.DurationOfClass,
        NumberOfStudents = model.NumberOfStudents,
        SectionName = model.SectionName,
        TotalClasses = model.TotalClasses,
        SessionId = model.SessionId,
        Group = model.Group,
        CurrentUserName = HttpContext.User.Identity.Name
      };

      var response = _addSectionRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _addSectionResponsePresenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());

      if (response.Result.SectionId > 0)
      {
        return RedirectToAction("ViewBranchMedium", "BranchMedium", new { branchMediumId = requestMessage.BranchMediumId });
      }
      else
      {
        var showSessionViewModel = new ShowSessionListViewModel();
        var showClassesListRequestMessage = new ShowClassesListRequestMessage();
        var showClassesViewModel = new ShowClassesListViewModel();
        var showClassesListResponse = _showClassesListRequestHandler.Handle(showClassesListRequestMessage, cancellationToken);
        var pickedClassesListViewModel = _showClassesListResponsePresenter.Handle(showClassesListResponse.Result, showClassesViewModel, GetUserAssociationResponseMessage());

        var showSessionListRequestMessage = new ShowCurrentSessionListRequestMessage();
        showSessionListRequestMessage.BranchMediumId = model.BranchMediumId;
        var showSessionListResponse = _showSessionListRequestHandler.Handle(showSessionListRequestMessage, cancellationToken);
        var pickedSessionListViewModel = _showSessionListResponsePresenter.Handle(showSessionListResponse.Result, showSessionViewModel, GetUserAssociationResponseMessage());

        viewModel.LoginUsersBranchMediumId = model.LoginUsersBranchMediumId;
        viewModel.Classes = pickedClassesListViewModel.Classes;
        viewModel.Sessions = pickedSessionListViewModel.Sessions;
        return View(viewModel);
      }
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Section, (int)Feature.SectionEnum.EditSection)]
    public ActionResult UpdateSection(long sectionId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowSectionRequestMessage();
      requestMessage.SectionId = sectionId;

      var showClassesListRequestMessage = new ShowClassesListRequestMessage();
      ShowClassesListViewModel showClassesViewModel = new ShowClassesListViewModel();
      var showClassesListResponse = _showClassesListRequestHandler.Handle(showClassesListRequestMessage, cancellationToken);
      var pickedClassesListViewModel = _showClassesListResponsePresenter.Handle(showClassesListResponse.Result, showClassesViewModel, GetUserAssociationResponseMessage());

      ShowSectionViewModel model = new ShowSectionViewModel();
      var response = _showSectionRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _showSectionResponsePresenter.Handle(response.Result, model, GetUserAssociationResponseMessage());

      var showSessionListRequestMessage = new ShowCurrentSessionListRequestMessage();
      showSessionListRequestMessage.BranchMediumId = viewModel.Section.BranchMedium.Id;
      ShowSessionListViewModel showSessionViewModel = new ShowSessionListViewModel();
      var showSessionListResponse = _showSessionListRequestHandler.Handle(showSessionListRequestMessage, cancellationToken);
      var pickedSessionListViewModel = _showSessionListResponsePresenter.Handle(showSessionListResponse.Result, showSessionViewModel, GetUserAssociationResponseMessage());

      var editViewModel = new EditSectionViewModel();
      editViewModel.LoginName = viewModel.LoginName;
      editViewModel.AssociatedWith = viewModel.AssociatedWith;
      editViewModel.LoginUsersInstituteId = viewModel.LoginUsersInstituteId;
      editViewModel.LoginUsersBranchId = viewModel.LoginUsersBranchId;
      editViewModel.LoginUsersBranchMediumId = viewModel.LoginUsersBranchMediumId;
      editViewModel.RoleFeatures = viewModel.RoleFeatures;
      editViewModel.Id = viewModel.Section.Id;
      editViewModel.Classes = pickedClassesListViewModel.Classes;
      editViewModel.ClassId = viewModel.Section.Class.Id;
      editViewModel.DurationOfClass = viewModel.Section.DurationOfClass;
      editViewModel.Group = viewModel.Section.Group;
      editViewModel.NumberOfStudents = viewModel.Section.NumberOfStudents;
      editViewModel.SectionName = viewModel.Section.SectionName;
      editViewModel.SessionId = viewModel.Section.Session.Id;
      editViewModel.TotalClasses = viewModel.Section.TotalClasses;
      editViewModel.Sessions = pickedSessionListViewModel.Sessions;
      editViewModel.BranchMediumId = viewModel.Section.BranchMedium.Id;
      return View(editViewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Section, (int)Feature.SectionEnum.EditSection)]
    [HttpPost]
    public ActionResult UpdateSection(EditSectionViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new EditSectionRequestMessage();

      requestMessage.Id = model.Id;
      requestMessage.BranchMediumId = model.BranchMediumId;
      requestMessage.ClassId = model.ClassId;
      requestMessage.DurationOfClass = model.DurationOfClass;
      requestMessage.NumberOfStudents = model.NumberOfStudents;
      requestMessage.SectionName = model.SectionName;
      requestMessage.TotalClasses = model.TotalClasses;
      requestMessage.SessionId = model.SessionId;
      requestMessage.Group = model.Group;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _editSectionRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _editSectionResponsePresenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());

      if (response.Result.ValidationResult.IsValid)
      {
        return RedirectToAction("ViewSection", new { sectionId = model.Id });
      }
      else
        return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Section, (int)Feature.SectionEnum.FindSection)]
    public ActionResult ViewSection(long sectionId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowSectionRequestMessage();
      requestMessage.SectionId = sectionId;
      ShowSectionViewModel model = new ShowSectionViewModel();
      var response = _showSectionRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _showSectionResponsePresenter.Handle(response.Result, model, GetUserAssociationResponseMessage());
      //      var showClassRoutineListRequestMessage = new ShowClassRoutineListRequestMessage();
      //      showClassRoutineListRequestMessage.SectionId = sectionId;
      //      List<ClassRoutineViewModel> showClassRoutineListViewListModel = new List<ClassRoutineViewModel>();
      //      var showClassRoutineListResponse = _showClassRoutineListRequestHandler.Handle(showClassRoutineListRequestMessage, cancellationToken);
      //      var pickedClassRoutineListViewModel = _showClassRoutineListResponsePresenter.Handle(showClassRoutineListResponse.Result, showClassRoutineListViewListModel);
      return View(viewModel);
    }


    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Section, (int)Feature.SectionEnum.AssignOrChangeRoom)]
    [HttpGet]
    public ActionResult AssignOrChangeRoomInSection(long sectionId, long roomId)
    {
      var roomsList = GetRooms(sectionId);
      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();
      var assignOrChangeRoomViewModel = new AssignOrChangeRoomInSectionViewModel
      {
        Rooms = roomsList,
        SectionId = sectionId,
        RoomId = roomId,
        LoginName = userAssociationMessage.LoginName,
        AssociatedWith = userAssociationMessage.AssociatedWith,
        LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId,
        LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId,
        LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId,
        RoleFeatures = userAssociationMessage.RoleFeatures,
        InstituteBanner = userAssociationMessage.InstituteBanner
      };
      return View(assignOrChangeRoomViewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Section, (int)Feature.SectionEnum.AssignOrChangeRoom)]
    [HttpPost]
    public ActionResult AssignOrChangeRoomInSection(AssignOrChangeRoomInSectionViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new AssignOrChangeRoomInSectionRequestMessage();

      requestMessage.SectionId = model.SectionId;
      requestMessage.RoomId = model.RoomId;

      var response = _assignOrChangeSectionRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _assignOrChangeSectionResponsePresenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());

      if (!response.Result.ValidationResult.IsValid)
      {
        viewModel.Rooms = GetRooms(model.SectionId);
        viewModel.SectionId = model.SectionId;
        viewModel.RoomId = model.RoomId;
        return View(viewModel);
      }
      return RedirectToAction("ViewSection", new { sectionId = model.SectionId });
    }

    private List<RoomViewModel> GetRooms(long sectionId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowRoomsListRequestMessage();
      var model = new List<RoomViewModel>();
      requestMessage.SectionId = sectionId;
      var response = _showRoomsListRequestHandler.Handle(requestMessage, cancellationToken);
      return _showRoomsListResponsePresenter.Handle(response.Result, model, GetUserAssociationResponseMessage());
    }

    public IEnumerable<StudentSection> GetSectionStudents(long sectionId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowSectionRequestMessage();

      requestMessage.SectionId = sectionId;

      var response = _showSectionRequestHandler.Handle(requestMessage, cancellationToken);
      if (response.Result.Section != null)
        return response.Result.Section.StudentSections;
      else
        return new List<StudentSection>();

    }

    public JsonResult GetAllClassTestEvents(long sectionId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowClassTestListRequestMessage { SectionId = sectionId };
      List<EventViewModel> model = new List<EventViewModel>();
      var response = _showClassTestListRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _showClassTestListResponsePresenter.Handle(response.Result.Data, model);

      return Json(viewModel);
    }
  }
}