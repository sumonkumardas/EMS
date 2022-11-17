using System;
using System.Collections.Generic;
using System.Linq;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Messages.BalanceSheetMessages;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class ShowBalanceSheetTransactionResponsePresenter
  {
    public BalanceSheetViewModel Handle(ShowBalanceSheetResponseMessage message, BalanceSheetViewModel model)
    {
      model.AssetAccountHead = ToViewModel(message.AssetAccountHead);
      model.LiabilitiesAccountHead = ToViewModel(message.LiabilitiesAccountHead);
      model.EquityAccountHead = ToViewModel(message.EquityAccountHead);
      model.TotalAsset = $"{message.TotalAsset}";
      model.TotalLiabilities = $"{message.TotalLiabilities}";
      model.TotalEquity = $"{message.TotalEquity}";
      model.NetIncome = $"{message.NetIncome}";
      model.IsYearClosing = message.IsYearClosing;
      model.AssetAccountHead.ChildAccountHeads.Add(new BalanceSheetViewModel.AccountHead()
      {
        ChildAccountHeads = new List<BalanceSheetViewModel.AccountHead>(),
        AccountHeadId = 0,
        AccountHeadName = "Total Assets",
        Amount = Convert.ToDecimal(message.TotalAsset),
        TransactionEntryType = AccountTransactionTypeEnum.Debit
      });
      model.LiabilitiesAccountHead.ChildAccountHeads.Add(new BalanceSheetViewModel.AccountHead()
      {
        ChildAccountHeads = new List<BalanceSheetViewModel.AccountHead>(),
        AccountHeadId = 0,
        AccountHeadName = "Total Liabilities",
        Amount = Convert.ToDecimal(message.TotalLiabilities),
        TransactionEntryType = AccountTransactionTypeEnum.Debit
      });

      if (!model.IsYearClosing)
      {
        model.EquityAccountHead.ChildAccountHeads.Add(new BalanceSheetViewModel.AccountHead()
        {
          ChildAccountHeads = new List<BalanceSheetViewModel.AccountHead>(),
          AccountHeadId = 0,
          AccountHeadName = "Net Income",
          Amount = Convert.ToDecimal(model.NetIncome),
          TransactionEntryType = AccountTransactionTypeEnum.Credit
        });
      }

      model.AccountTypeTreeUi = GenerateAccountHeadTreeUi(model);
      model.DebitTreeUi = GenerateDebitTreeUi(model);
      model.CreditTreeUi = GenerateCreditTreeUi(model);
      model.EmptyTreeUi = GenerateEmptyTreeUi(model);
      return model;
    }

    private BalanceSheetViewModel.AccountHead ToViewModel(
      ShowBalanceSheetResponseMessage.AccountHead accountHead)
    {
      return new BalanceSheetViewModel.AccountHead
      {
        AccountHeadId = accountHead.AccountHeadId,
        AccountHeadName = accountHead.AccountHeadName,
        Amount = accountHead.Amount,
        TransactionEntryType = accountHead.TransactionEntryType,
        ChildAccountHeads = accountHead.AccountHeads.Select(ToViewModel).ToList()
      };
    }

    private string GenerateAccountHeadTreeUi(BalanceSheetViewModel model)
    {
      string ui = @"<ul class=""myUL"">";

      ui += GenerateAccoundHeadNode(ui, model.AssetAccountHead);
      ui += GenerateAccoundHeadNode("", model.LiabilitiesAccountHead);
      ui += GenerateAccoundHeadNode("", model.EquityAccountHead);
      ui += "</ul>";

      return ui;
    }

    private string GenerateAccoundHeadNode(string ui, BalanceSheetViewModel.AccountHead node)
    {
      ui += "<li><span class=\"caret5\">" + node.AccountHeadName + "</span>";

      if (node.ChildAccountHeads == null || node.ChildAccountHeads.Count > 0)
      {
        ui += GnerateChildNodes(node, "");
      }

      ui += "</li>";
      return ui;
    }

    private string GenerateDebitTreeUi(BalanceSheetViewModel model)
    {
      string ui = @"<ul class=""myUL"">";

      ui += GernerateDebitTreeNode(model.AssetAccountHead, ui);

      ui += "</ul>";

      return ui;
    }

    private string GernerateDebitTreeNode(BalanceSheetViewModel.AccountHead node, string ui)
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

    private string GenerateCreditTreeUi(BalanceSheetViewModel model)
    {
      string ui = @"<ul class=""myUL"">";
      ui += GenerateCreditTreeNode(model.LiabilitiesAccountHead, ui);
      ui += GenerateCreditTreeNode(model.EquityAccountHead, "");
      ui += "</ul>";

      return ui;
    }

    private string GenerateCreditTreeNode(BalanceSheetViewModel.AccountHead node, string ui)
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

    private string GenerateEmptyTreeUi(BalanceSheetViewModel model)
    {
      string ui = @"<ul class=""myUL"">";
      var node = model.AssetAccountHead;
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

    private string GnerateChildNodes(BalanceSheetViewModel.AccountHead node, string ui)
    {
      if (node.ChildAccountHeads == null || node.ChildAccountHeads.Count == 0)
      {
        return ui;
      }
      else
      {
        ui += "<ul class=\"nested5\">";
        foreach (var nodeChildAccountHead in node.ChildAccountHeads)
        {
          ui += "<li><span class=\"caret5\">" + nodeChildAccountHead.AccountHeadName + "</span>";
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

    private string GnerateChildNodesDebitCredit(BalanceSheetViewModel.AccountHead node, string ui)
    {
      if (node.ChildAccountHeads == null || node.ChildAccountHeads.Count == 0)
      {
        return ui;
      }
      else
      {
        ui += "<ul class=\"nested5\">";
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
    private string GnerateChildNodesEmpty(BalanceSheetViewModel.AccountHead node, string ui)
    {
      if (node.ChildAccountHeads == null || node.ChildAccountHeads.Count == 0)
      {
        return ui;
      }
      else
      {
        ui += "<ul class=\"nested5\">";
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