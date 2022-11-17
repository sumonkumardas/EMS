using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SinePulse.EMS.Messages.AccountHeadMessages;
using SinePulse.EMS.Messages.Model.Accounts;
using SinePulse.EMS.Messages.TrialBalanceMessages;
using SinePulse.EMS.ProjectPrimitives;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class ShowTrialBalanceTransactionResponsePresenter
  {
    public TrialBalanceViewModel Handle(ShowTrialBalanceResponseMessage message, TrialBalanceViewModel model)
    {
      model.AccountHeads = message.AccountHeads.Select(ToViewModel).ToList();
      model.TotalDebit = $"{message.TotalDebit}";
      model.TotalCredit = $"{message.TotalCredit}";
      model.AccountTypeTreeUi = GenerateAccountHeadTreeUi(model);
      model.DebitTreeUi = GenerateDebitTreeUi(model);
      model.CreditTreeUi = GenerateCreditTreeUi(model);
      model.EmptyTreeUi = GenerateEmptyTreeUi(model);
      return model;
    }

    private TrialBalanceViewModel.AccountHead ToViewModel(ShowTrialBalanceResponseMessage.AccountHead accountHead)
    {
      return new TrialBalanceViewModel.AccountHead
      {
        AccountHeadId = accountHead.AccountHeadId,
        AccountHeadName = accountHead.AccountHeadName,
        Amount = accountHead.Balance,
        TransactionEntryType = accountHead.TransactionEntryType,
        ChildAccountHeads = accountHead.AccountHeads.Select(ToViewModel).ToList()
      };
    }

    private string GenerateAccountHeadTreeUi(TrialBalanceViewModel model)
    {
      string ui = @"<ul class=""myUL"">";
      foreach (var node in model.AccountHeads.Where(w => w.TransactionEntryType == Domain.Enums.AccountTransactionTypeEnum.Debit))
      {
        ui += "<li><span class=\"caret3\">" + node.AccountHeadName + "</span>";

        if (node.ChildAccountHeads == null || node.ChildAccountHeads.Count > 0)
        {
          ui += GnerateChildNodes(node, "");
        }

        ui += "</li>";
      }
      foreach (var node in model.AccountHeads.Where(w => w.TransactionEntryType == Domain.Enums.AccountTransactionTypeEnum.Credit))
      {
        ui += "<li><span class=\"caret3\">" + node.AccountHeadName + "</span>";

        if (node.ChildAccountHeads == null || node.ChildAccountHeads.Count > 0)
        {
          ui += GnerateChildNodes(node, "");
        }

        ui += "</li>";
      }
      ui += "</ul>";

      return ui;
    }
    private string GenerateDebitTreeUi(TrialBalanceViewModel model)
    {
      string ui = @"<ul class=""myUL"">";
      foreach (var node in model.AccountHeads.Where(w => w.TransactionEntryType == Domain.Enums.AccountTransactionTypeEnum.Debit))
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
      }

      ui += "</ul>";

      return ui;
    }
    private string GenerateCreditTreeUi(TrialBalanceViewModel model)
    {
      string ui = @"<ul class=""myUL"">";
      foreach (var node in model.AccountHeads.Where(w => w.TransactionEntryType == Domain.Enums.AccountTransactionTypeEnum.Credit))
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
      }

      ui += "</ul>";

      return ui;
    }
    private string GenerateEmptyTreeUi(TrialBalanceViewModel model)
    {
      string ui = @"<ul class=""myUL"">";
      foreach (var node in model.AccountHeads.Where(w => w.TransactionEntryType == Domain.Enums.AccountTransactionTypeEnum.Debit))
      {
        var nodeText = string.Empty;
        nodeText = "&nbsp;";

        ui += "<li><span>" + nodeText + "</span>";

        if (node.ChildAccountHeads == null || node.ChildAccountHeads.Count > 0)
        {
          ui += GnerateChildNodesEmpty(node, "");
        }

        ui += "</li>";
      }

      ui += "</ul>";

      return ui;
    }

    private string GnerateChildNodes(TrialBalanceViewModel.AccountHead node, string ui)
    {
      if (node.ChildAccountHeads == null || node.ChildAccountHeads.Count == 0)
      {
        return ui;
      }
      else
      {
        ui += "<ul class=\"nested3\">";
        foreach (var nodeChildAccountHead in node.ChildAccountHeads)
        {
          ui += "<li><span class=\"caret3\">" + nodeChildAccountHead.AccountHeadName + "</span>";
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

    private string GnerateChildNodesDebitCredit(TrialBalanceViewModel.AccountHead node, string ui)
    {
      if (node.ChildAccountHeads == null || node.ChildAccountHeads.Count == 0)
      {
        return ui;
      }
      else
      {
        ui += "<ul class=\"nested3\">";
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
    private string GnerateChildNodesEmpty(TrialBalanceViewModel.AccountHead node, string ui)
    {
      if (node.ChildAccountHeads == null || node.ChildAccountHeads.Count == 0)
      {
        return ui;
      }
      else
      {
        ui += "<ul class=\"nested3\">";
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