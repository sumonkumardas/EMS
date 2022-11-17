using System.Linq;
using AutoMapper;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.EmployeeMessages;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.Model.Employees;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class ShowEmployeeResponsePresenter
  {
    private MapperConfiguration _automapperConfig;

    public EmployeeViewModel Handle(ShowEmployeeResponseMessage message, EmployeeViewModel viewModel,
      GetUserAssociationResponseMessage userAssociationMessage)
    {
      _automapperConfig = new MapperConfiguration(
        cfg =>
        {
          cfg.CreateMap<BranchMessageModel, BranchViewModel>();
          cfg.CreateMap<MediumMessageModel, MediumViewModel>();
          cfg.CreateMap<EmployeeMessageModel, EmployeeViewModel>();
          cfg.CreateMap<GradeMessageModel, GradeViewModel>();
          cfg.CreateMap<JobTypeMessageModel, JobTypeViewModel>();
        });
      var mapper = _automapperConfig.CreateMapper();
      viewModel = new EmployeeViewModel();
      viewModel = MapModel(message.Employee);
      viewModel.LoginName = userAssociationMessage.LoginName;
      viewModel.LoginImageUrl = userAssociationMessage.ImageUrl;
      viewModel.AssociatedWith = userAssociationMessage.AssociatedWith;
      viewModel.LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId;
      viewModel.LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId;
      viewModel.LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId;
      viewModel.RoleFeatures = userAssociationMessage.RoleFeatures;
      viewModel.InstituteBanner = userAssociationMessage.InstituteBanner;
      viewModel.PendingAmount = userAssociationMessage.PendingAmount;
      viewModel.IsBillDueDateOver = userAssociationMessage.IsBillDueDateOver;
      return viewModel;
    }

    private EmployeeViewModel MapModel(EmployeeMessageModel messageModel)
    {
      EmployeeViewModel viewModel = new EmployeeViewModel();
      if (messageModel != null)
      {
        viewModel.Id = messageModel.Id;
        viewModel.FullName = messageModel.FullName;
        viewModel.EmployeeCode = messageModel.EmployeeCode;
        viewModel.DOB = messageModel.DOB;
        viewModel.NationalId = messageModel.NationalId;
        viewModel.MobileNo = messageModel.MobileNo;
        viewModel.EmailAddress = messageModel.EmailAddress;
        viewModel.JoiningDate = messageModel.JoiningDate;
        viewModel.ImageUrl = messageModel.ImageUrl;
        viewModel.BankAccountNo = messageModel.BankAccountNo;
        viewModel.EmployeeType = messageModel.EmployeeType;
        viewModel.Designation = messageModel.Designation;
        viewModel.Institute = MapInstituteViewModel(messageModel.Institute);
        viewModel.Branch = MapBranchViewModel(messageModel.Branch);
        viewModel.BranchMedium = MapBranchMediumViewModel(messageModel.BranchMedium);
        viewModel.Grade = MapGradeViewModel(messageModel.Grade);
        viewModel.ReportTo = MapModel(messageModel.ReportTo);
        viewModel.isAccountRegistered = messageModel.isAccountRegistered;
        viewModel.ObjectId = messageModel.ObjectId;
        viewModel.ReportToId = messageModel.ReportToId;
        viewModel.JobType = MapJobTypeViewModel(messageModel.JobType);
        viewModel.Status = messageModel.Status;
        viewModel.AssociatType = messageModel.AssociatedWith;
      }

      return viewModel;
    }

    private BranchViewModel MapBranchViewModel(BranchMessageModel messageModel)
    {
      BranchViewModel viewModel = new BranchViewModel();
      if (messageModel != null)
      {
        viewModel.Id = messageModel.Id;
        viewModel.BranchName = messageModel.BranchName;
        viewModel.BranchCode = viewModel.BranchCode;
        viewModel.MobileNo = messageModel.MobileNo;
        viewModel.Email = messageModel.Email;
        viewModel.Fax = messageModel.Fax;
        viewModel.Status = messageModel.Status;
        viewModel.MobileNo = messageModel.MobileNo;
        viewModel.Institute = MapInstituteViewModel(messageModel.Institute);
      }

      return viewModel;
    }

    private InstituteViewModel MapInstituteViewModel(InstituteMessageModel messageModel)
    {
      InstituteViewModel viewModel = new InstituteViewModel();
      if (messageModel != null)
      {
        viewModel.Id = messageModel.Id;
        viewModel.OrganisationName = messageModel.OrganisationName;
        viewModel.ShortName = messageModel.ShortName;
        viewModel.Slogan = messageModel.Slogan;
        viewModel.Domain = messageModel.Domain;
        viewModel.Email = messageModel.Email;
      }

      return viewModel;
    }

    private BranchMediumViewModel MapBranchMediumViewModel(BranchMediumMessageModel messageModel)
    {
      BranchMediumViewModel viewModel = new BranchMediumViewModel();
      if (messageModel != null)
      {
        viewModel.Id = messageModel.Id;
        viewModel.WeeklyHolidays = messageModel.WeeklyHolidays;
        viewModel.Medium = MapMediumViewModel(messageModel.Medium);
        viewModel.Shift = MapShiftViewModel(messageModel.Shift);
        viewModel.Branch = MapBranchViewModel(messageModel.Branch);
      }

      return viewModel;
    }

    private ShiftViewModel MapShiftViewModel(ShiftMessageModel messageModel)
    {
      ShiftViewModel viewModel = new ShiftViewModel();
      if (messageModel != null)
      {
        viewModel.ShiftId = messageModel.Id;
        viewModel.ShiftName = messageModel.ShiftName;
        viewModel.StartTime = messageModel.StartTime;
        viewModel.EndTime = messageModel.EndTime;
      }

      return viewModel;
    }

    private MediumViewModel MapMediumViewModel(MediumMessageModel messageModel)
    {
      MediumViewModel viewModel = new MediumViewModel();
      if (messageModel != null)
      {
        viewModel.MediumId = messageModel.Id;
        viewModel.MediumName = messageModel.MediumName;
        viewModel.MediumCode = messageModel.MediumCode;
      }

      return viewModel;
    }

    private GradeViewModel MapGradeViewModel(GradeMessageModel messageModel)
    {
      GradeViewModel viewModel = new GradeViewModel();
      if (messageModel != null)
      {
        viewModel.Id = messageModel.Id;
        viewModel.GradeTitle = messageModel.GradeTitle;
        viewModel.MinSalary = messageModel.MinSalary;
        viewModel.MaxSalary = messageModel.MaxSalary;
        viewModel.Status = messageModel.Status;
      }

      return viewModel;
    }

    private JobTypeViewModel MapJobTypeViewModel(JobTypeMessageModel messageModel)
    {
      JobTypeViewModel viewModel = new JobTypeViewModel();
      if (messageModel != null)
      {
        viewModel.Id = messageModel.Id;
        viewModel.JobTitle = messageModel.JobTitle;
        viewModel.HasOverTime = messageModel.HasOverTime;
      }

      return viewModel;
    }
  }
}