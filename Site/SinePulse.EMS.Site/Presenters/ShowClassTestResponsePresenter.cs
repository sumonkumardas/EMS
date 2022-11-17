using System;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.ClassTestMessages;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class ShowClassTestResponsePresenter
  {
    public ClassTestViewModel Handle(ShowClassTestResponseMessage message, ClassTestViewModel viewModel,
      GetUserAssociationResponseMessage userAssociationMessage)
    {
      viewModel.ClassTestId = message.ClassTestData.TestId;
      viewModel.ClassTestName = message.ClassTestData.TestName;
      viewModel.FullMarks = message.ClassTestData.FullMarks;
      viewModel.StartTimestamp = message.ClassTestData.StartTimestamp;
      viewModel.EndTimestamp = message.ClassTestData.EndTimestamp;
      viewModel.ExamType = message.ClassTestData.ExamType;
      viewModel.TermData = ToViewModelTerm(message.ClassTestData.Term);
      viewModel.Group = ToGroupText(message.ClassTestData.Group);
      viewModel.SubjectData = ToViewModelSubject(message.ClassTestData.Subject);
      viewModel.SectionData = ToViewModelSection(message.ClassTestData.Section);
      viewModel.ExamConfigurationId = message.ClassTestData.ExamConfigurationId;
      viewModel.Status = message.ClassTestData.Status;

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

    private ClassTestViewModel.Term ToViewModelTerm(ShowClassTestResponseMessage.Term term)
    {
      return new ClassTestViewModel.Term
      {
        TermId = term.TermId,
        TermText = term.TermName
      };
    }

    private string ToGroupText(GroupEnum group)
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

    private ClassTestViewModel.Subject ToViewModelSubject(ShowClassTestResponseMessage.Subject subject)
    {
      return new ClassTestViewModel.Subject
      {
        SubjectId = subject.SubjectId,
        SubjectText = subject.SubjectName
      };
    }

    private static ClassTestViewModel.Section ToViewModelSection(ShowClassTestResponseMessage.Section responseSection)
    {
      return new ClassTestViewModel.Section
      {
        SectionId = responseSection.SectionId,
        SectionText =
          $"{responseSection.SectionName} - [{responseSection.ClassName}]"
      };
    }
  }
}