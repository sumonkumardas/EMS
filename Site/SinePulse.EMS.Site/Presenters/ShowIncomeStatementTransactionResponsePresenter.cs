using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SinePulse.EMS.Messages.AccountHeadMessages;
using SinePulse.EMS.Messages.IncomeStatementMessages;
using SinePulse.EMS.Messages.Model.Accounts;
using SinePulse.EMS.Messages.TrialBalanceMessages;
using SinePulse.EMS.ProjectPrimitives;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class ShowIncomeStatementTransactionResponsePresenter
  {
    public IncomeStatementViewModel Handle(ShowIncomeStatementResponseMessage message,
        IncomeStatementViewModel model)
    {
      model.RevenueAccountHead = ToViewModel(message.RevenueAccountHead);
      model.ExpenseAccountHead = ToViewModel(message.ExpenseAccountHead);
      model.TotalExpense = $"{message.TotalExpense}";
      model.TotalRevenue = $"{message.TotalRevenue}";
      model.NetIncome = $"{message.NetIncome}";
      model.AccountTypeTreeUi = GenerateAccountHeadTreeUi(model);
      model.DebitTreeUi = GenerateDebitTreeUi(model);
      model.CreditTreeUi = GenerateCreditTreeUi(model);
      model.EmptyTreeUi = GenerateEmptyTreeUi(model);
      return model;
    }

    private IncomeStatementViewModel.AccountHead ToViewModel(
        ShowIncomeStatementResponseMessage.AccountHead accountHead)
    {
      return new IncomeStatementViewModel.AccountHead
      {
        AccountHeadId = accountHead.AccountHeadId,
        AccountHeadName = accountHead.AccountHeadName,
        Amount = accountHead.Balance,
        TransactionEntryType = accountHead.TransactionEntryType,
        ChildAccountHeads = accountHead.AccountHeads.Select(ToViewModel).ToList()
      };
    }

    private string GenerateAccountHeadTreeUi(IncomeStatementViewModel model)
    {
      string ui = @"<ul class=""myUL"">";

      ui += GenerateAccoundHeadNode(ui, model.RevenueAccountHead);
      ui += GenerateAccoundHeadNode("", model.ExpenseAccountHead);

      ui += "</ul>";

      return ui;
    }

    private string GenerateAccoundHeadNode(string ui, IncomeStatementViewModel.AccountHead node)
    {
      ui += "<li><span class=\"caret4\">" + node.AccountHeadName + "</span>";

      if (node.ChildAccountHeads == null || node.ChildAccountHeads.Count > 0)
      {
        ui += GnerateChildNodes(node, "");
      }

      ui += "</li>";
      return ui;
    }

    private string GenerateDebitTreeUi(IncomeStatementViewModel model)
    {
      string ui = @"<ul class=""myUL"">";

      ui += GernerateDebitTreeNode(model.RevenueAccountHead, ui);

      ui += "</ul>";

      return ui;
    }

    private string GernerateDebitTreeNode(IncomeStatementViewModel.AccountHead node, string ui)
    {
      var nodeText = string.Empty;
      nodeText = node.Amount == 0
          ? "&nbsp;"
          : node.Amount.ToString();
      ui += "<li><span>" + nodeText + "</span>";

      if (node.ChildAccountHeads == null || node.ChildAccountHeads.Count > 0)
      {
        ui += GnerateChildNodesDebitCredit(node, "");
      }

      ui += "</li>";
      return ui;
    }

    private string GenerateCreditTreeUi(IncomeStatementViewModel model)
    {
      string ui = @"<ul class=""myUL"">";
      ui += GenerateCreditTreeNode(model.ExpenseAccountHead, ui);

      ui += "</ul>";

      return ui;
    }

    private string GenerateCreditTreeNode(IncomeStatementViewModel.AccountHead node, string ui)
    {
      var nodeText = string.Empty;
      nodeText = node.Amount == 0
          ? "&nbsp;"
          : node.Amount.ToString();

      ui += "<li><span>" + nodeText + "</span>";

      if (node.ChildAccountHeads == null || node.ChildAccountHeads.Count > 0)
      {
        ui += GnerateChildNodesDebitCredit(node, "");
      }

      ui += "</li>";
      return ui;
    }

    private string GenerateEmptyTreeUi(IncomeStatementViewModel model)
    {
      string ui = @"<ul class=""myUL"">";
      var node = model.RevenueAccountHead;
      var nodeText = string.Empty;
      nodeText = "&nbsp;";

      ui += "<li><span>" + nodeText + "</span>";

      if (node.ChildAccountHeads == null || node.ChildAccountHeads.Count > 0)
      {
        ui += GnerateChildNodesEmpty(node, "");
      }

      ui += "</li>";


      ui += "</ul>";

      return ui;
    }

    private string GnerateChildNodes(IncomeStatementViewModel.AccountHead node, string ui)
    {
      if (node.ChildAccountHeads == null || node.ChildAccountHeads.Count == 0)
      {
        return ui;
      }
      else
      {
        ui += "<ul class=\"nested4\">";
        foreach (var nodeChildAccountHead in node.ChildAccountHeads)
        {
          ui += "<li><span class=\"caret4\">" + nodeChildAccountHead.AccountHeadName + "</span>";
          if (nodeChildAccountHead.ChildAccountHeads != null &&
              nodeChildAccountHead.ChildAccountHeads.Count > 0)
          {
            ui += GnerateChildNodes(nodeChildAccountHead, "");
          }

          ui += "</li>";

        }
        ui += "</ul>";
        return ui;
      }
    }

    private string GnerateChildNodesDebitCredit(IncomeStatementViewModel.AccountHead node, string ui)
    {
      if (node.ChildAccountHeads == null || node.ChildAccountHeads.Count == 0)
      {
        return ui;
      }
      else
      {
        ui += "<ul class=\"nested4\">";
        foreach (var nodeChildAccountHead in node.ChildAccountHeads)
        {
          var nodeText = string.Empty;
          nodeText = nodeChildAccountHead.Amount == 0
              ? "&nbsp;"
              : nodeChildAccountHead.Amount.ToString();
          ui += "<li><span>" + nodeText + "</span>";
          if (nodeChildAccountHead.ChildAccountHeads != null &&
              nodeChildAccountHead.ChildAccountHeads.Count > 0)
          {
            ui += GnerateChildNodesDebitCredit(nodeChildAccountHead, "");
          }

          ui += "</li>";

        }
        ui += "</ul>";
        return ui;
      }
    }
    private string GnerateChildNodesEmpty(IncomeStatementViewModel.AccountHead node, string ui)
    {
      if (node.ChildAccountHeads == null || node.ChildAccountHeads.Count == 0)
      {
        return ui;
      }
      else
      {
        ui += "<ul class=\"nested4\">";
        foreach (var nodeChildAccountHead in node.ChildAccountHeads)
        {
          var nodeText = string.Empty;
          nodeText = "&nbsp;";
          ui += "<li><span>" + nodeText + "</span>";
          if (nodeChildAccountHead.ChildAccountHeads != null &&
              nodeChildAccountHead.ChildAccountHeads.Count > 0)
          {
            ui += GnerateChildNodesEmpty(nodeChildAccountHead, "");
          }

          ui += "</li>";

        }
        ui += "</ul>";
        return ui;
      }
    }

  }
}