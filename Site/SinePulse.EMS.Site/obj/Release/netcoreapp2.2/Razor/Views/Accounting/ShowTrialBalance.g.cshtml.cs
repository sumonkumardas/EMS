#pragma checksum "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Accounting\ShowTrialBalance.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a7ad556649a76076210d7184db1e7ed16ccbd785"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Accounting_ShowTrialBalance), @"mvc.1.0.view", @"/Views/Accounting/ShowTrialBalance.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Accounting/ShowTrialBalance.cshtml", typeof(AspNetCore.Views_Accounting_ShowTrialBalance))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\_ViewImports.cshtml"
using SinePulse.EMS.Site;

#line default
#line hidden
#line 2 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\_ViewImports.cshtml"
using SinePulse.EMS.Site.Models;

#line default
#line hidden
#line 1 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Accounting\ShowTrialBalance.cshtml"
using Microsoft.AspNetCore.Mvc.Localization;

#line default
#line hidden
#line 4 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Accounting\ShowTrialBalance.cshtml"
using SinePulse.EMS.ProjectPrimitives;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a7ad556649a76076210d7184db1e7ed16ccbd785", @"/Views/Accounting/ShowTrialBalance.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9011cacf54d8ae45691a3deaab04c36239ace056", @"/Views/_ViewImports.cshtml")]
    public class Views_Accounting_ShowTrialBalance : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<AccountDisplayModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("text-danger"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", "hidden", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("branchMediumId"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("runat", new global::Microsoft.AspNetCore.Html.HtmlString("server"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("selected", new global::Microsoft.AspNetCore.Html.HtmlString("selected"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("monthType"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-control"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "ShowTrialBalance", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_9 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-horizontal"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationSummaryTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.SelectTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(148, 766, true);
            WriteLiteral(@"<style>

  ul, .myUL {
    list-style-type: none;
  }

  .myUL {
    margin: 0;
    padding: 0;
  }

  .caret3 {
    cursor: pointer;
    -webkit-user-select: none; /* Safari 3.1+ */
    -moz-user-select: none; /* Firefox 2+ */
    -ms-user-select: none; /* IE 10+ */
    user-select: none;
  }

    .caret3::before {
      content: ""\25B6"";
      color: black;
      display: inline-block;
      margin-right: 6px;
    }

  .caret-down3::before {
    -ms-transform: rotate(90deg); /* IE 9 */
    -webkit-transform: rotate(90deg); /* Safari */
    ' transform: rotate(90deg);
  }

  .nested3 {
  }

  #printableDiv {
  }

  .active {
    display: block;
  }
</style>
<div class=""portlet-body form"">
  <!-- BEGIN FORM-->
  ");
            EndContext();
            BeginContext(914, 1357, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a7ad556649a76076210d7184db1e7ed16ccbd7858592", async() => {
                BeginContext(974, 37, true);
                WriteLiteral("\r\n    <div class=\"form-body\">\r\n      ");
                EndContext();
                BeginContext(1011, 66, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a7ad556649a76076210d7184db1e7ed16ccbd7859014", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationSummaryTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper);
#line 51 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Accounting\ShowTrialBalance.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper.ValidationSummary = global::Microsoft.AspNetCore.Mvc.Rendering.ValidationSummary.ModelOnly;

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-validation-summary", __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper.ValidationSummary, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(1077, 8, true);
                WriteLiteral("\r\n      ");
                EndContext();
                BeginContext(1085, 119, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "a7ad556649a76076210d7184db1e7ed16ccbd78510841", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
#line 52 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Accounting\ShowTrialBalance.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => Model.TrialBalanceViewModel.Branch.BranchMediumId);

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(1204, 113, true);
                WriteLiteral("\r\n      <div class=\"form-group\">\r\n        <div class=\"col-md-12\">\r\n          <div class=\"col-md-4\">\r\n            ");
                EndContext();
                BeginContext(1318, 223, false);
#line 56 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Accounting\ShowTrialBalance.cshtml"
       Write(Html.DropDownListFor(x => x.CurrentSession.SessionId, new SelectList(@Model.Sessions, "SessionId", "SessionName"), @Localizer["ShowTrialBalance.SelectSession"].Value, new { @class = "form-control", id = "sessionDropDown" }));

#line default
#line hidden
                EndContext();
                BeginContext(1541, 139, true);
                WriteLiteral("\r\n            <span id=\"sessionDropDownError\" class=\"text-danger\"></span>\r\n          </div>\r\n          <div class=\"col-md-4\">\r\n            ");
                EndContext();
                BeginContext(1680, 238, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("select", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a7ad556649a76076210d7184db1e7ed16ccbd78513776", async() => {
                    BeginContext(1788, 16, true);
                    WriteLiteral("\r\n              ");
                    EndContext();
                    BeginContext(1804, 91, false);
                    __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a7ad556649a76076210d7184db1e7ed16ccbd78514197", async() => {
                        BeginContext(1842, 44, false);
#line 61 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Accounting\ShowTrialBalance.cshtml"
                                              Write(Localizer["ShowTrialBalance.SelectEndMonth"]);

#line default
#line hidden
                        EndContext();
                    }
                    );
                    __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                    __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                    __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
                    __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_5.Value;
                    __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
                    await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                    if (!__tagHelperExecutionContext.Output.IsContentModified)
                    {
                        await __tagHelperExecutionContext.SetOutputContentAsync();
                    }
                    Write(__tagHelperExecutionContext.Output);
                    __tagHelperExecutionContext = __tagHelperScopeManager.End();
                    EndContext();
                    BeginContext(1895, 14, true);
                    WriteLiteral("\r\n            ");
                    EndContext();
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.SelectTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
#line 60 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Accounting\ShowTrialBalance.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.Month);

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_7);
#line 60 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Accounting\ShowTrialBalance.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Items = Html.GetEnumSelectList<MonthType>();

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-items", __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Items, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(1918, 236, true);
                WriteLiteral("\r\n            <span id=\"monthTypeError\" class=\"text-danger\"></span>\r\n          </div>\r\n          <div class=\"col-md-2\">\r\n            <button type=\"button\" onclick=\"showTrialBalance()\" class=\"btn custom-btn custom-cancel-btn pull-right\">");
                EndContext();
                BeginContext(2155, 34, false);
#line 66 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Accounting\ShowTrialBalance.cshtml"
                                                                                                              Write(Localizer["ShowTrialBalance.Show"]);

#line default
#line hidden
                EndContext();
                BeginContext(2189, 75, true);
                WriteLiteral("</button>\r\n          </div>\r\n        </div>\r\n\r\n      </div>\r\n    </div>\r\n  ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_8.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_9);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2271, 338, true);
            WriteLiteral(@"
  <!-- END FORM-->
</div>
<button type=""button"" class=""btn custom-btn custom-cancel-btn pull-left"" onclick=""showBar(this)""><span class=""ui-button-text"">Show Bar</span></button>
<div id=""trialBalanceTransaction"" >
  <div id=""printableDiv"">
      <div class=""row"">
          <div class=""col-xs-12"" align=""center"">
              <p>");
            EndContext();
            BeginContext(2610, 43, false);
#line 80 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Accounting\ShowTrialBalance.cshtml"
            Write(Model.AccountDisplayInstitute.InstituteName);

#line default
#line hidden
            EndContext();
            BeginContext(2653, 23, true);
            WriteLiteral("</p>\r\n              <p>");
            EndContext();
            BeginContext(2677, 37, false);
#line 81 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Accounting\ShowTrialBalance.cshtml"
            Write(Model.AccountDisplayBranch.BranchName);

#line default
#line hidden
            EndContext();
            BeginContext(2714, 23, true);
            WriteLiteral("</p>\r\n              <p>");
            EndContext();
            BeginContext(2738, 49, false);
#line 82 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Accounting\ShowTrialBalance.cshtml"
            Write(Model.AccountDisplayBranchMedium.BranchMediumName);

#line default
#line hidden
            EndContext();
            BeginContext(2787, 23, true);
            WriteLiteral("</p>\r\n              <p>");
            EndContext();
            BeginContext(2811, 42, false);
#line 83 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Accounting\ShowTrialBalance.cshtml"
            Write(Localizer["ShowTrialBalance.TrialBalance"]);

#line default
#line hidden
            EndContext();
            BeginContext(2853, 45, true);
            WriteLiteral("</p>\r\n              <p id=\"trialBalanceDate\">");
            EndContext();
            BeginContext(2899, 57, false);
#line 84 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Accounting\ShowTrialBalance.cshtml"
                                  Write(Model.TrialBalanceViewModel.StartDate.ToShortDateString());

#line default
#line hidden
            EndContext();
            BeginContext(2956, 3, true);
            WriteLiteral(" - ");
            EndContext();
            BeginContext(2960, 55, false);
#line 84 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Accounting\ShowTrialBalance.cshtml"
                                                                                               Write(Model.TrialBalanceViewModel.EndDate.ToShortDateString());

#line default
#line hidden
            EndContext();
            BeginContext(3015, 289, true);
            WriteLiteral(@"</p>
          </div>
          <div id=""tbloader"" style=""display: none;"">
              <i class=""fa fa-circle-o-notch fa-spin"" style=""font-size:24px""></i>
          </div>
          <br />
      </div>
    <div class=""row"">
      <div class=""col-xs-4"" align=""left"">
        <h4>");
            EndContext();
            BeginContext(3305, 42, false);
#line 93 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Accounting\ShowTrialBalance.cshtml"
       Write(Localizer["ShowTrialBalance.AccountTitle"]);

#line default
#line hidden
            EndContext();
            BeginContext(3347, 55, true);
            WriteLiteral("</h4>\r\n        <div id=\"accountTypeTree\">\r\n            ");
            EndContext();
            BeginContext(3403, 56, false);
#line 95 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Accounting\ShowTrialBalance.cshtml"
       Write(Html.Raw(@Model.TrialBalanceViewModel.AccountTypeTreeUi));

#line default
#line hidden
            EndContext();
            BeginContext(3459, 91, true);
            WriteLiteral("\r\n        </div>\r\n\r\n      </div>\r\n      <div class=\"col-xs-3\" align=\"left\">\r\n          <h4>");
            EndContext();
            BeginContext(3551, 35, false);
#line 100 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Accounting\ShowTrialBalance.cshtml"
         Write(Localizer["ShowTrialBalance.Debit"]);

#line default
#line hidden
            EndContext();
            BeginContext(3586, 49, true);
            WriteLiteral("</h4>\r\n          <div id=\"debitTree\">\r\n          ");
            EndContext();
            BeginContext(3636, 50, false);
#line 102 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Accounting\ShowTrialBalance.cshtml"
     Write(Html.Raw(@Model.TrialBalanceViewModel.DebitTreeUi));

#line default
#line hidden
            EndContext();
            BeginContext(3686, 103, true);
            WriteLiteral("\r\n          </div>\r\n          \r\n      </div>\r\n      <div class=\"col-xs-3\" align=\"center\">\r\n        <h4>");
            EndContext();
            BeginContext(3790, 36, false);
#line 107 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Accounting\ShowTrialBalance.cshtml"
       Write(Localizer["ShowTrialBalance.Credit"]);

#line default
#line hidden
            EndContext();
            BeginContext(3826, 51, true);
            WriteLiteral("</h4>\r\n        <div id=\"creditEmptyTree\">\r\n        ");
            EndContext();
            BeginContext(3878, 50, false);
#line 109 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Accounting\ShowTrialBalance.cshtml"
   Write(Html.Raw(@Model.TrialBalanceViewModel.EmptyTreeUi));

#line default
#line hidden
            EndContext();
            BeginContext(3928, 57, true);
            WriteLiteral("\r\n        </div>\r\n        <div id=\"creditTree\">\r\n        ");
            EndContext();
            BeginContext(3986, 51, false);
#line 112 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Accounting\ShowTrialBalance.cshtml"
   Write(Html.Raw(@Model.TrialBalanceViewModel.CreditTreeUi));

#line default
#line hidden
            EndContext();
            BeginContext(4037, 122, true);
            WriteLiteral("\r\n        </div>\r\n      </div>\r\n    </div>\r\n    <div class=\"row\">\r\n      <div class=\"col-xs-4\" align=\"left\">\r\n        <h3>");
            EndContext();
            BeginContext(4160, 35, false);
#line 118 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Accounting\ShowTrialBalance.cshtml"
       Write(Localizer["ShowTrialBalance.Total"]);

#line default
#line hidden
            EndContext();
            BeginContext(4195, 131, true);
            WriteLiteral("</h3>\r\n      </div>\r\n      <div class=\"col-xs-3\" align=\"left\" style=\"border-top: 5px black solid;\">\r\n          <h3 id=\"totalDebit\">");
            EndContext();
            BeginContext(4327, 49, false);
#line 121 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Accounting\ShowTrialBalance.cshtml"
                         Write(Html.Raw(@Model.TrialBalanceViewModel.TotalDebit));

#line default
#line hidden
            EndContext();
            BeginContext(4376, 146, true);
            WriteLiteral("</h3>\r\n\r\n      </div>\r\n      <div class=\"col-xs-3\" align=\"right\" style=\"border-top:5px black solid;\">\r\n          <h3 id=\"totalCredit\">\r\n          ");
            EndContext();
            BeginContext(4523, 50, false);
#line 126 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Accounting\ShowTrialBalance.cshtml"
     Write(Html.Raw(@Model.TrialBalanceViewModel.TotalCredit));

#line default
#line hidden
            EndContext();
            BeginContext(4573, 225, true);
            WriteLiteral("\r\n          </h3>\r\n      </div>\r\n    </div>\r\n\r\n  </div>\r\n</div>\r\n<div class=\"row\">\r\n  <div class=\"col-xs-4\" align=\"left\">\r\n    <button type=\"button\" onclick=\"printDiv(\'printableDiv\')\" class=\"btn custom-btn custom-cancel-btn\">");
            EndContext();
            BeginContext(4799, 35, false);
#line 135 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Accounting\ShowTrialBalance.cshtml"
                                                                                                 Write(Localizer["ShowTrialBalance.Print"]);

#line default
#line hidden
            EndContext();
            BeginContext(4834, 62, true);
            WriteLiteral("</button>\r\n\r\n  </div>\r\n</div>\r\n<div id=\"print-me\"></div>\r\n\r\n\r\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public IViewLocalizer Localizer { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<AccountDisplayModel> Html { get; private set; }
    }
}
#pragma warning restore 1591