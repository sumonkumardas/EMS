using System;
using System.Collections.Generic;
using AutoMapper;
using SinePulse.EMS.Domain.Routines;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.ClassRoutineMessages;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.Model.Employees;
using SinePulse.EMS.Messages.Model.Routines;
using SinePulse.EMS.Messages.SessionMessages;
using SinePulse.EMS.Messages.TermMessages;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class ShowClassRoutineResponsePresenter
  {
    public ClassRoutineViewModel Handle(ShowClassRoutineResponseMessage message, ClassRoutineViewModel viewModel, GetUserAssociationResponseMessage userAssociationMessage)
    {
      
      viewModel = MapClassRoutine(message.ClassRoutine);
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

    private ClassRoutineViewModel MapClassRoutine(ClassRoutineMessageModel classRoutine)
    {
      var routine = new ClassRoutineViewModel();
      if (classRoutine != null)
      {
        routine.Section = new SectionViewModel()
        {
          Id = classRoutine.Section.Id,
          SectionName = classRoutine.Section.SectionName
        };
        routine.EndTime = classRoutine.EndTime;
        routine.IsBreakTime = classRoutine.IsBreakTime;
        routine.Room = new RoomViewModel()
        {
          Id = classRoutine.Room.Id,
          RoomNo = classRoutine.Room.RoomNo
        };
        routine.StartTime = classRoutine.StartTime;
        routine.Teacher = new EmployeeViewModel()
        {
          Id = classRoutine.Teacher.Id,
          FullName = classRoutine.Teacher.FullName
        };
        routine.WeekDay = classRoutine.WeekDay;
        routine.Status = classRoutine.Status;
        routine.Subject = new SubjectViewModel()
        {
          Id = classRoutine.Subject.Id,
          SubjectName = classRoutine.Subject.SubjectName
        };
        
      }

      return routine;
    }
  }
}