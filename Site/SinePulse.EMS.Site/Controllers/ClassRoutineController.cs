using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SinePulse.EMS.Domain.Features;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.ClassRoutineMessages;
using SinePulse.EMS.Site.Authorization;
using SinePulse.EMS.Site.Models;
using SinePulse.EMS.Site.Presenters;
using SinePulse.EMS.UseCases.Authorization;
using SinePulse.EMS.UseCases.ClassRoutines;

namespace SinePulse.EMS.Site.Controllers
{
  [Authorize]
  public class ClassRoutineController : BaseController
  {
    private readonly AddClassRoutineRequestHandler _addClassRoutineRequestHandler;
    private readonly AddClassRoutineResponsePresenter _presenter;
    private readonly GetAddClassRoutineViewModelDataRequestHandler _getAddClassRoutineViewModelDataRequestHandler;
    private readonly EditClassRoutineRequestHandler _editClassRoutineRequestHandler;
    private readonly EditClassRoutineResponsePresenter _editClassRoutineResponsePresenter;
    private readonly ShowClassRoutineRequestHandler _showClassRoutineRequestHandler;
    private readonly ShowClassRoutineResponsePresenter _showClassRoutineResponsePresenter;

    public ClassRoutineController(AddClassRoutineRequestHandler addClassRoutineRequestHandler, 
      AddClassRoutineResponsePresenter presenter, 
      GetAddClassRoutineViewModelDataRequestHandler getAddClassRoutineViewModelDataRequestHandler,
       GetUserAssociationRequestHandler getUserAssociationRequestHandler,
      EditClassRoutineRequestHandler editClassRoutineRequestHandler,
      EditClassRoutineResponsePresenter editClassRoutineResponsePresenter,
      ShowClassRoutineRequestHandler showClassRoutineRequestHandler,
      ShowClassRoutineResponsePresenter showClassRoutineResponsePresenter) : base(getUserAssociationRequestHandler)
    {
      _addClassRoutineRequestHandler = addClassRoutineRequestHandler;
      _presenter = presenter;
      _getAddClassRoutineViewModelDataRequestHandler = getAddClassRoutineViewModelDataRequestHandler;
      _editClassRoutineRequestHandler = editClassRoutineRequestHandler;
      _editClassRoutineResponsePresenter = editClassRoutineResponsePresenter;
      _showClassRoutineRequestHandler = showClassRoutineRequestHandler;
      _showClassRoutineResponsePresenter = showClassRoutineResponsePresenter;
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Section, (int)Feature.SectionEnum.AddClassRoutine)]
    [HttpGet]
    public ActionResult AddClassRoutine(long sectionId)
    {
      var viewModel = new AddClassRoutineViewModel();
      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();
      
      viewModel.LoginName = userAssociationMessage.LoginName;
      viewModel.LoginImageUrl = userAssociationMessage.ImageUrl;
      viewModel.AssociatedWith = userAssociationMessage.AssociatedWith;
      viewModel.LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId;
      viewModel.LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId;
      viewModel.LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId;
      viewModel.RoleFeatures = userAssociationMessage.RoleFeatures;
      viewModel.InstituteBanner = userAssociationMessage.InstituteBanner;

      var response = GetAddClassRoutineViewModelData(sectionId);
      viewModel.SectionId = sectionId;
      viewModel.Rooms = response.Rooms;
      viewModel.Subjects = response.Subjects;
      viewModel.Teachers = response.Teachers;
      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Section, (int)Feature.SectionEnum.AddClassRoutine)]
    [HttpPost]
    public ActionResult AddClassRoutine(AddClassRoutineViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new AddClassRoutineRequestMessage();

      requestMessage.WeekDay = model.WeekDay;
      requestMessage.StartTime = model.StartTime;
      requestMessage.EndTime = model.EndTime;
      requestMessage.IsBreakTime = model.IsBreakTime;
      requestMessage.SubjectId = model.SubjectId;
      requestMessage.TeacherId = model.TeacherId;
      requestMessage.SectionId = model.SectionId;
      requestMessage.RoomId = model.RoomId;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _addClassRoutineRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _presenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());
      var viewModelData = GetAddClassRoutineViewModelData(model.SectionId);
      viewModel.SectionId = model.SectionId;
      viewModel.Rooms = viewModelData.Rooms;
      viewModel.Subjects = viewModelData.Subjects;
      viewModel.Teachers = viewModelData.Teachers;
      if(response.Result.ValidationResult.IsValid)
        return Redirect("/Section/ViewSection?sectionId=" + model.SectionId + "#tab_classRoutine");
      return View(viewModel);
    }

    public GetAddClassRoutineViewModelDataResponseMessage GetAddClassRoutineViewModelData(long sectionId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new GetAddClassRoutineViewModelDataRequestMessage();
      requestMessage.SectionId = sectionId;
      return _getAddClassRoutineViewModelDataRequestHandler.Handle(requestMessage, cancellationToken).Result;
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Section, (int)Feature.SectionEnum.EditClassRoutine)]
    [HttpGet]
    public ActionResult UpdateClassRoutine(long classRoutineId)
    {

      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowClassRoutineRequestMessage();
      requestMessage.ClassRoutineId = classRoutineId;
      var response = _showClassRoutineRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _showClassRoutineResponsePresenter.Handle(response.Result, new ClassRoutineViewModel(),
        GetUserAssociationResponseMessage());
      
      var model = new EditClassRoutineViewModel();

      var comboDataResponse = GetAddClassRoutineViewModelData(viewModel.Section.Id);
      model.SectionId = viewModel.Section.Id;
      model.RoomId = viewModel.Room.Id;
      model.TeacherId = viewModel.Teacher.Id;
      model.SubjectId = viewModel.Subject.Id;
      model.Rooms = comboDataResponse.Rooms;
      model.Subjects = comboDataResponse.Subjects;
      model.Teachers = comboDataResponse.Teachers;
      model.Id = classRoutineId;
      model.EndTime = viewModel.EndTime;
      model.StartTime = viewModel.StartTime;
      model.IsBreakTime = viewModel.IsBreakTime;
      model.WeekDay = viewModel.WeekDay;
      return View(model);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Section, (int)Feature.SectionEnum.EditClassRoutine)]
    [HttpPost]
    public ActionResult UpdateClassRoutine(EditClassRoutineViewModel model)
    {

      var cancellationToken = new CancellationToken();
      var requestMessage = new EditClassRoutineRequestMessage();
      requestMessage.Id = model.Id;
      requestMessage.WeekDay = model.WeekDay;
      requestMessage.StartTime = model.StartTime;
      requestMessage.EndTime = model.EndTime;
      requestMessage.IsBreakTime = model.IsBreakTime;
      requestMessage.SubjectId = model.SubjectId;
      requestMessage.TeacherId = model.TeacherId;
      requestMessage.SectionId = model.SectionId;
      requestMessage.RoomId = model.RoomId;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;
      var response = _editClassRoutineRequestHandler.Handle(requestMessage, cancellationToken);
      if (response.Result.ValidationResult.IsValid)
        return Redirect("/Section/ViewSection?sectionId=" + model.SectionId + "#tab_classRoutine");
      else
      {
        var comboDataResponse = GetAddClassRoutineViewModelData(model.SectionId);
        model.Rooms = comboDataResponse.Rooms;
        model.Subjects = comboDataResponse.Subjects;
        model.Teachers = comboDataResponse.Teachers;
        foreach (var error in response.Result.ValidationResult.Errors)
        {
          ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
        }
        return View(model);
      }
    }
  }
}