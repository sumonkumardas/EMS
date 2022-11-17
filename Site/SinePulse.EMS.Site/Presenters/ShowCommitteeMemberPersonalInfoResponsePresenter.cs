using SinePulse.EMS.Messages.CommitteeMemberPersonalInfoMessages;
using SinePulse.EMS.Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SinePulse.EMS.Site.Presenters
{
  public class ShowCommitteeMemberPersonalInfoResponsePresenter
  {
    public CommitteeMemberPersonalInfoViewModel Handle(GetCommitteeMemberPersonalInfoResponseMessage message,
      CommitteeMemberPersonalInfoViewModel viewModel)
    {
      viewModel.FathersName = message.CommitteeMemberPersonalInfos.FathersName;
      viewModel.MothersName = message.CommitteeMemberPersonalInfos.MothersName;
      viewModel.SpouseName = message.CommitteeMemberPersonalInfos.SpouseName;
      viewModel.Gender = message.CommitteeMemberPersonalInfos.Gender;
      viewModel.Religion = message.CommitteeMemberPersonalInfos.Religion;
      viewModel.BloodGroup = message.CommitteeMemberPersonalInfos.BloodGroup;
      return viewModel;
    }
  }
}
