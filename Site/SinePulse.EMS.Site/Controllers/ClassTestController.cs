using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SinePulse.EMS.Domain.Features;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.ClassTestMessages;
using SinePulse.EMS.Site.Authorization;
using SinePulse.EMS.Site.Models;
using SinePulse.EMS.Site.Presenters;
using SinePulse.EMS.UseCases.Authorization;
using SinePulse.EMS.UseCases.ClassTests;

namespace SinePulse.EMS.Site.Controllers
{
  [Authorize]
  public class ClassTestController : BaseController
  {
    private readonly DisplayAddClassTestPageRequestHandler _displayAddClassTestPageRequestHandler;
    private readonly DisplayAddClassTestPageResponsePresenter _displayAddClassTestPageResponsePresenter;
    private readonly AddClassTestRequestHandler _addClassTestRequestHandler;
    private readonly AddClassTestResponsePresenter _presenter;
    private readonly ShowClassTestRequestHandler _showClassTestRequestHandler;
    private readonly ShowClassTestResponsePresenter _showClassTestResponsePresenter;
    private readonly EditClassTestRequestHandler _editClassTestRequestHandler;
    

    public ClassTestController(
      DisplayAddClassTestPageRequestHandler displayAddClassTestPageRequestHandler,
      DisplayAddClassTestPageResponsePresenter displayAddClassTestPageResponsePresenter,
      AddClassTestRequestHandler addClassTestRequestHandler,
      AddClassTestResponsePresenter presenter,
      ShowClassTestRequestHandler showClassTestRequestHandler,
      ShowClassTestResponsePresenter showClassTestResponsePresenter,
      GetUserAssociationRequestHandler getUserAssociationRequestHandler,
      EditClassTestRequestHandler editClassTestRequestHandler) : base(getUserAssociationRequestHandler)
    {
      _displayAddClassTestPageRequestHandler = displayAddClassTestPageRequestHandler;
      _displayAddClassTestPageResponsePresenter = displayAddClassTestPageResponsePresenter;
      _addClassTestRequestHandler = addClassTestRequestHandler;
      _presenter = presenter;
      _showClassTestRequestHandler = showClassTestRequestHandler;
      _showClassTestResponsePresenter = showClassTestResponsePresenter;
      _editClassTestRequestHandler = editClassTestRequestHandler;
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Examination, (int)Feature.ExaminationEnum.AddClassTest)]
    [HttpGet]
    public ActionResult AddClassTest(long sectionId)
    {
      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();
      var cancellationToken = new CancellationToken();
      var requestMessage = new DisplayAddClassTestPageRequestMessage
      {
        SectionId = sectionId
      };

      var model = new AddClassTestViewModel
      {
        SectionId = sectionId
      };

      var response = _displayAddClassTestPageRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _displayAddClassTestPageResponsePresenter.Handle(response.Result, model, ModelState,
        GetUserAssociationResponseMessage());

      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Examination, (int)Feature.ExaminationEnum.AddClassTest)]
    [HttpPost]
    public ActionResult AddClassTest(AddClassTestViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new AddClassTestRequestMessage();

      requestMessage.TestName = model.TestName;
      requestMessage.FullMarks = model.FullMarks;
      requestMessage.Date = model.Date;
      requestMessage.StartTime = model.StartTime;
      requestMessage.EndTime = model.EndTime;
      requestMessage.ExamType = model.ExamType;
      requestMessage.ExamConfigurationId = model.ExamConfigurationId;
      requestMessage.SectionId = model.SectionId;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _addClassTestRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _presenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());

      if (!response.Result.ValidationResult.IsValid)
      {
        var displayAddClassTestPageRequestMessage = new DisplayAddClassTestPageRequestMessage
        {
          SectionId = model.SectionId
        };

        var displayAddClassTestPageResponseMessage = _displayAddClassTestPageRequestHandler.Handle(displayAddClassTestPageRequestMessage, cancellationToken);
        displayAddClassTestPageResponseMessage.Result.ValidationResult = response.Result.ValidationResult;
        viewModel = _displayAddClassTestPageResponsePresenter.Handle(displayAddClassTestPageResponseMessage.Result, model,
          ModelState, GetUserAssociationResponseMessage());
                return View(viewModel);
      }
      return Redirect("/Section/ViewSection?sectionId=" + model.SectionId + "#tab_classTest");
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Examination, (int)Feature.ExaminationEnum.ViewClassTest)]
    public ActionResult ViewClassTest(long classTestId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowClassTestRequestMessage {ClassTestId = classTestId};
      var response = _showClassTestRequestHandler.Handle(requestMessage, cancellationToken);
      var model = new ClassTestViewModel();
      var viewModel = _showClassTestResponsePresenter.Handle(response.Result.Data, model,
        GetUserAssociationResponseMessage());
      return View(viewModel);
    }
    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Examination, (int)Feature.ExaminationEnum.EditClassTest)]
    [HttpGet]
    public ActionResult UpdateClassTest(long classTestId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowClassTestRequestMessage { ClassTestId = classTestId };
      var response = _showClassTestRequestHandler.Handle(requestMessage, cancellationToken);
      var model = new EditClassTestViewModel();
      var viewModel = _showClassTestResponsePresenter.Handle(response.Result.Data, new ClassTestViewModel(), 
        GetUserAssociationResponseMessage());

      model.TestName = viewModel.ClassTestName;
      model.FullMarks = (int) viewModel.FullMarks;
      model.Date = viewModel.StartTimestamp.Date;
      model.StartTime = viewModel.StartTimestamp.TimeOfDay;
      model.EndTime = viewModel.EndTimestamp.TimeOfDay;
      model.ExamType = viewModel.ExamType;
      model.ExamConfigurationId = viewModel.ExamConfigurationId;
      model.SectionId = viewModel.SectionData.SectionId;
      model.TermId = viewModel.TermData.TermId;
      model.GroupName = viewModel.Group.Replace(" ","");
      model.Id = viewModel.ClassTestId;

      var displayAddClassTestPageRequestMessage = new DisplayAddClassTestPageRequestMessage
      {
        SectionId = model.SectionId
      };
      var displayAddClassTestPageResponse = _displayAddClassTestPageRequestHandler.Handle(displayAddClassTestPageRequestMessage, cancellationToken);
      var displayAddClassTestPageResponseModel = _displayAddClassTestPageResponsePresenter.Handle(displayAddClassTestPageResponse.Result, model, ModelState,
        GetUserAssociationResponseMessage());

      return View(displayAddClassTestPageResponseModel);
    }
    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Examination, (int)Feature.ExaminationEnum.EditClassTest)]
    [HttpPost]
    public ActionResult UpdateClassTest(EditClassTestViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new EditClassTestRequestMessage();

      requestMessage.Id = model.Id;
      requestMessage.TestName = model.TestName;
      requestMessage.FullMarks = model.FullMarks;
      requestMessage.Date = model.Date;
      requestMessage.StartTime = model.StartTime;
      requestMessage.EndTime = model.EndTime;
      requestMessage.ExamType = model.ExamType;
      requestMessage.ExamConfigurationId = model.ExamConfigurationId;
      requestMessage.SectionId = model.SectionId;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _editClassTestRequestHandler.Handle(requestMessage, cancellationToken);
      if (!response.Result.ValidationResult.IsValid)
      {
        var displayAddClassTestPageRequestMessage = new DisplayAddClassTestPageRequestMessage
        {
          SectionId = model.SectionId
        };
        var displayAddClassTestPageResponse = _displayAddClassTestPageRequestHandler.Handle(displayAddClassTestPageRequestMessage, cancellationToken);
        var displayAddClassTestPageResponseModel = _displayAddClassTestPageResponsePresenter.Handle(displayAddClassTestPageResponse.Result, model, ModelState,
          GetUserAssociationResponseMessage());

        return View(displayAddClassTestPageResponseModel);
      }

      return Redirect("/Section/ViewSection?sectionId=" + model.SectionId + "#tab_classTest");
    }
  }
}