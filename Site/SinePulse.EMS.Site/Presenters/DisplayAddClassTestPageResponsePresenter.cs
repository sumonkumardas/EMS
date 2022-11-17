using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.ClassTestMessage;
using SinePulse.EMS.Messages.Model.Enums;
using SinePulse.EMS.Messages.Shared;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class DisplayAddClassTestPageResponsePresenter
  {
    public AddClassTestViewModel Handle(ValidatedData<DisplayAddClassTestPageResponseMessage> message,
      AddClassTestViewModel viewModel, ModelStateDictionary modelState,
      GetUserAssociationResponseMessage userAssociationMessage)
    {
      if (!message.ValidationResult.IsValid)
      {
        modelState.Clear();
        foreach (var error in message.ValidationResult.Errors)
        {
          modelState.AddModelError(error.PropertyName, error.ErrorMessage);
        }
      }
      else
      {
        modelState.Clear();
      }
      viewModel.ClassText = message.Data.ClassName;
      viewModel.Terms = message.Data.Terms.Select(ToViewModelTerm).ToList();
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

    public EditClassTestViewModel Handle(ValidatedData<DisplayAddClassTestPageResponseMessage> message,
      EditClassTestViewModel viewModel, ModelStateDictionary modelState,
      GetUserAssociationResponseMessage userAssociationMessage)
    {
      if (!message.ValidationResult.IsValid)
      {
        modelState.Clear();
        foreach (var error in message.ValidationResult.Errors)
        {
          modelState.AddModelError(error.PropertyName, error.ErrorMessage);
        }
      }
      else
      {
        modelState.Clear();
      }
      viewModel.ClassText = message.Data.ClassName;
      viewModel.Terms = message.Data.Terms.Select(ToEditViewModelTerm).ToList();
      viewModel.LoginName = userAssociationMessage.LoginName;
      viewModel.LoginImageUrl = userAssociationMessage.ImageUrl;
      viewModel.AssociatedWith = userAssociationMessage.AssociatedWith;
      viewModel.LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId;
      viewModel.LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId;
      viewModel.LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId;
      viewModel.RoleFeatures = userAssociationMessage.RoleFeatures;
      viewModel.InstituteBanner = userAssociationMessage.InstituteBanner;
      return viewModel;
    }

    private static AddClassTestViewModel.Term ToViewModelTerm(
      DisplayAddClassTestPageResponseMessage.Term responseTerm)
    {
      return new AddClassTestViewModel.Term
      {
        TermId = responseTerm.TermId,
        TermName = responseTerm.TermName,
        Groups = responseTerm.Groups.Select(ToViewModelGroup).ToList()
      };
    }

    private static EditClassTestViewModel.Term ToEditViewModelTerm(
      DisplayAddClassTestPageResponseMessage.Term responseTerm)
    {
      return new EditClassTestViewModel.Term
      {
        TermId = responseTerm.TermId,
        TermName = responseTerm.TermName,
        Groups = responseTerm.Groups.Select(ToEditViewModelGroup).ToList()
      };
    }

    private static AddClassTestViewModel.Group ToViewModelGroup(DisplayAddClassTestPageResponseMessage.Group group)
    {
      return new AddClassTestViewModel.Group
      {
        GroupName = group.GroupEnum,
        Subjects = group.Subjects.Select(ToViewModelSubject).ToList()
      };
    }

    private static EditClassTestViewModel.Group ToEditViewModelGroup(DisplayAddClassTestPageResponseMessage.Group group)
    {
      return new EditClassTestViewModel.Group
      {
        GroupName = group.GroupEnum,
        Subjects = group.Subjects.Select(ToEditViewModelSubject).ToList()
      };
    }

    private static AddClassTestViewModel.Subject ToViewModelSubject(
      DisplayAddClassTestPageResponseMessage.Subject subject)
    {
      return new AddClassTestViewModel.Subject
      {
        SubjectName = subject.SubjectName,
        ExamConfigurationId = subject.ExamConfigurationId
      };
    }

    private static EditClassTestViewModel.Subject ToEditViewModelSubject(
      DisplayAddClassTestPageResponseMessage.Subject subject)
    {
      return new EditClassTestViewModel.Subject
      {
        SubjectName = subject.SubjectName,
        ExamConfigurationId = subject.ExamConfigurationId
      };
    }

    private static string ToGroupName(GroupEnum group)
    {
      switch (group)
      {
        case GroupEnum.NoGroup:
          return "No Group";
        case GroupEnum.Arts:
          return "Arts";
        case GroupEnum.Commerce:
          return "Commerce";
        case GroupEnum.Science:
          return "Science";
        default:
          throw new ArgumentOutOfRangeException(nameof(group), group, null);
      }
    }
  }
}