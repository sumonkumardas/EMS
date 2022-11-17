using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.InstituteMessages;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Site.Models;
using System.Collections.Generic;
using SinePulse.EMS.Messages.StudentSectionMessages;

namespace SinePulse.EMS.Site.Presenters
{

  public class ShowStudentSectionListResponsePresenter
  {
    public ShowStudentListViewModel Handle(ShowStudentSectionListResponseMessage message, ShowStudentListViewModel viewModel, GetUserAssociationResponseMessage userAssociationMessage)
    {
      viewModel.Students = new List<StudentViewModel>();
      foreach (var messageStudent in message.Students)
      {
        viewModel.Students.Add(new StudentViewModel()
        {
          Id = messageStudent.StudentId,
          BirthDate = messageStudent.DOB,
          FullName = messageStudent.FullName,
          MobileNo = messageStudent.MobileNo,
          Roll = messageStudent.RollNo
        });
      }

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
  }
}
