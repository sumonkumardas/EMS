using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.ExamConfigurationMessages;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class ShowExamConfigurationResponsePresenter
  {
    public ExamConfigurationViewModel Handle(ShowExamConfigurationResponseMessage message, ExamConfigurationViewModel viewModel,
      GetUserAssociationResponseMessage userAssociationMessage)
    {
      viewModel.ExamConfigurationId = message.ExamConfigurationData.ExamConfigurationId;
      viewModel.ClassTestPercentage = message.ExamConfigurationData.ClassTestPercentage;
      viewModel.ObjectiveFullMark = message.ExamConfigurationData.ObjectiveFullMark;
      viewModel.ObjectivePassMark = message.ExamConfigurationData.ObjectivePassMark;
      viewModel.SubjectiveFullMark = message.ExamConfigurationData.SubjectiveFullMark;
      viewModel.SubjectivePassMark = message.ExamConfigurationData.SubjectivePassMark;
      viewModel.PracticalPassMark = message.ExamConfigurationData.PracticalPassMark;
      viewModel.PracticalFullMark = message.ExamConfigurationData.PracticalFullMark;
      viewModel.Status = message.ExamConfigurationData.Status;
      viewModel.TermId = message.ExamConfigurationData.Term.TermId;
      viewModel.SubjectName = message.ExamConfigurationData.Subject.SubjectName;
      viewModel.SubjectId = message.ExamConfigurationData.Subject.SubjectId;
      viewModel.ClassName = message.ExamConfigurationData.Class.ClassName;
      viewModel.ClassId = message.ExamConfigurationData.Class.ClassId;
      //viewModel.GroupName = message.ExamConfigurationData.ClassSubject.Group.ToString();

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