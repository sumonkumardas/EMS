using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SinePulse.EMS.Domain.Accounts;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Messages.BranchMediumAccountsHeadMessages;
using SinePulse.EMS.Messages.LeaveTypeMessages;
using SinePulse.EMS.Messages.Model.Accounts;
using SinePulse.EMS.Messages.Model.Examinations;
using SinePulse.EMS.Messages.Model.Leaves;
using SinePulse.EMS.Messages.RoomMessages;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class ShowBranchMediumAccountsHeadListResponsePresenter
  {

    public string Handle(ShowBranchMediumAccountsHeadListResponseMessage message, long accountType,int rowNumber)
    {
      var list = new List<TreeViewModel>();
      foreach (var item in message.BranchMediumAccountsHeadList.Where(x=>x.AccountType.AccountTypeName == (ChartOfAccountTypeEnum) accountType))
      {
        var treeModel = new TreeViewModel();
        treeModel.Id = item.Id;
        treeModel.Name = item.AccountHeadName;
        treeModel.ParentId = item.ParentHead == null ? 0 : item.ParentHead.Id;
        list.Add(treeModel);
      }
      List<string> uiArray = new List<string>();
      string ui = "<div class=\"col-sm-1\"><div id=\"tree_"+rowNumber+"\" class=\"tree-demo\"><ul><li id=\"accheadCombo_" + rowNumber+"\" >Select Account Head";
      ui += "<ul>";
      //string ui = @"<div class=""dropdown""><button id=""accheadCombo_" + rowNumber + @""" class=""btn btn-secondary dropdown-toggle"" type=""button"" id=""dropdownMenu1"" data-toggle=""dropdown"" aria-haspopup=""true"" aria-expanded=""true"" value="""">
      //      Account Head
      //  </button> <span class=""invisible"" id=""accHeadComboSpan_" + rowNumber + @"""></span>" + @"<span class=""badge badge-danger"" id=""accHeadAlert_" + rowNumber + @"""></span> ";
      //ui += @"<ul class=""dropdown-menu multi-level"" role=""menu"" aria-labelledby=""dropdownMenu"" >";
      foreach (var tree in list.Where(x => x.ParentId == 0))
      {
        if (list.Where(x => x.ParentId == tree.Id).Count() == 0)
        {
          ui += @"<li onclick = ""accountHeadComboOnchange(" + rowNumber + ",'" + tree.Name + @"'," + tree.Id + @" )"" >" + tree.Name;
        }
        else
        {
          ui += "<li>" + tree.Name;
        }

        ui += GenerateUI(tree, "", list, 0, rowNumber);
        ui += "</li>";
      }
      ui += "</ul></li></ul></div></div><span class=\"invisible\" id=\"accHeadComboSpan_" + rowNumber+ "\"></span>"+ "<span class=\"badge badge-danger\" id=\"accHeadAlert_"+rowNumber+ "\" ></span>";

      return ui;
    }

    public string Handle(ShowBranchMediumAccountsHeadListResponseMessage message)
    {
      var list = new List<TreeViewModel>();
      foreach (var item in message.BranchMediumAccountsHeadList)
      {
        var treeModel = new TreeViewModel();
        treeModel.Id = item.Id;
        treeModel.Name = item.AccountHeadName;
        treeModel.ParentId = item.ParentHead == null ? 0 : item.ParentHead.Id;
        list.Add(treeModel);
      }
      List<string> uiArray = new List<string>();
      string ui = @"<ul id=""myUL""><li><p class=""showAccountChild"">Account Head</p>";
      foreach (var tree in list.Where(x => x.ParentId == 0))
      {
        if (list.Where(x => x.ParentId == tree.Id).Count() > 0)
        {
          ui += @"<ul class="" master2""><li onclick=""viewNode(" + tree.Id + @")""><span class=""caret2"">" + tree.Name + @"</span>";
          ui += GenerateUI(tree, "", list, 0);
          ui += "</li></ul>";
        }
        else
        {
          {
            ui += @"<li onclick=""viewNode(" + tree.Id + @")"">" + tree.Name + @"</li>";
            
          }
        }
        
      }
      ui += "</li></ul>";

      return ui;
    }

    private string GenerateUI(TreeViewModel tree, string ending, List<TreeViewModel> list, int max, int rowNumber)
    {
      if (list.Where(x => x.ParentId == tree.Id).Count() == 0)
      {
        return ending;
      }
      else
      {
        ending += "<ul>";


        foreach (var tree2 in list.Where(x => x.ParentId == tree.Id))
        {
          if (list.Where(x => x.ParentId == tree2.Id).Count() > 0)
          {
            ending += "<li>";
          }
          else
          {
            ending += @"<li onclick = ""accountHeadComboOnchange(" + rowNumber + ",'" + tree2.Name + @"'," + tree2.Id + @")"" >";
          }
          ending += tree2.Name;
          ending += GenerateUI(tree2, "", list, max, rowNumber);
          ending += "</li>";
        }

        ending += "</ul>";
        return ending;
      }
    }

    private string GenerateUI(TreeViewModel tree, string ending, List<TreeViewModel> list, int max)
    {
      if (list.Where(x => x.ParentId == tree.Id).Count() == 0)
      {
        return ending;
      }
      else
      {
        ending += "<ul class=\"nested2\">";


        foreach (var tree2 in list.Where(x => x.ParentId == tree.Id))
        {
          if (list.Where(x => x.ParentId == tree2.Id).Count() == 0)
          {
            ending += "<li onclick=\"viewNode(" + tree2.Id+")\">"+tree2.Name+"</li>";
          }
          else
          {
            ending += @"<li onclick=""viewNode(" + tree2.Id + @")""><span class=""caret2"">" + tree2.Name + @"</span>";
          }
          
          ending += GenerateUI(tree2, "", list, max);
          if (list.Where(x => x.ParentId == tree2.Id).Count() > 0)
            ending += "</li>";
        }

        ending += "</ul>";
        return ending;
      }
    }

  }
}