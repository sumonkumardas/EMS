#pragma checksum "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Accounting\ShowTransaction.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1f907d35ed5d24edf59bf965d9ee888d49815c47"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Accounting_ShowTransaction), @"mvc.1.0.view", @"/Views/Accounting/ShowTransaction.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Accounting/ShowTransaction.cshtml", typeof(AspNetCore.Views_Accounting_ShowTransaction))]
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
#line 2 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Accounting\ShowTransaction.cshtml"
using Microsoft.AspNetCore.Mvc.Localization;

#line default
#line hidden
#line 3 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Accounting\ShowTransaction.cshtml"
using SinePulse.EMS.Domain.Enums;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1f907d35ed5d24edf59bf965d9ee888d49815c47", @"/Views/Accounting/ShowTransaction.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9011cacf54d8ae45691a3deaab04c36239ace056", @"/Views/_ViewImports.cshtml")]
    public class Views_Accounting_ShowTransaction : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ShowTransactionViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", "hidden", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("runat", new global::Microsoft.AspNetCore.Html.HtmlString("server"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 5 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Accounting\ShowTransaction.cshtml"
  
    ViewData["Title"] = "ShowTransaction";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
            BeginContext(246, 223, true);
            WriteLiteral("\r\n<div class=\"page-container\">\r\n    <!-- BEGIN CONTENT -->\r\n    <div class=\"page-content-wrapper\">\r\n        <div class=\"page-content\">\r\n            <div class=\"portlet box form-conatiner\">\r\n               \r\n                ");
            EndContext();
            BeginContext(469, 72, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "1f907d35ed5d24edf59bf965d9ee888d49815c474899", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#line 16 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Accounting\ShowTransaction.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.TransactionViewModel.Id);

#line default
#line hidden
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(541, 328, true);
            WriteLiteral(@"
                <div class=""portlet-body form"">
                  
                    <div class=""container-fluid"">
                        <div class=""row-fluid "">
                            <div class=""col-md-6"">
                                <div class=""col-md-6 text-center"">
                                    ");
            EndContext();
            BeginContext(870, 40, false);
#line 23 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Accounting\ShowTransaction.cshtml"
                               Write(Model.TransactionViewModel.TransactionNo);

#line default
#line hidden
            EndContext();
            BeginContext(910, 122, true);
            WriteLiteral("\r\n                                    <div class=\"uppercase profile-stat-title\">\r\n                                        ");
            EndContext();
            BeginContext(1033, 42, false);
#line 25 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Accounting\ShowTransaction.cshtml"
                                   Write(Localizer["ShowTransaction.TransactionNo"]);

#line default
#line hidden
            EndContext();
            BeginContext(1075, 190, true);
            WriteLiteral("\r\n                                    </div>\r\n                                </div>\r\n                                <div class=\"col-md-6 text-center\">\r\n                                    ");
            EndContext();
            BeginContext(1266, 42, false);
#line 29 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Accounting\ShowTransaction.cshtml"
                               Write(Model.TransactionViewModel.TransactionDate);

#line default
#line hidden
            EndContext();
            BeginContext(1308, 122, true);
            WriteLiteral("\r\n                                    <div class=\"uppercase profile-stat-title\">\r\n                                        ");
            EndContext();
            BeginContext(1431, 44, false);
#line 31 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Accounting\ShowTransaction.cshtml"
                                   Write(Localizer["ShowTransaction.TransactionDate"]);

#line default
#line hidden
            EndContext();
            BeginContext(1475, 220, true);
            WriteLiteral("\r\n                                    </div>\r\n\r\n                                </div>\r\n                            </div>\r\n                            <div class=\"col-md-6 text-center\">\r\n                                ");
            EndContext();
            BeginContext(1696, 38, false);
#line 37 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Accounting\ShowTransaction.cshtml"
                           Write(Model.TransactionViewModel.Description);

#line default
#line hidden
            EndContext();
            BeginContext(1734, 114, true);
            WriteLiteral("\r\n                                <div class=\"uppercase profile-stat-title\">\r\n                                    ");
            EndContext();
            BeginContext(1849, 40, false);
#line 39 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Accounting\ShowTransaction.cshtml"
                               Write(Localizer["ShowTransaction.Description"]);

#line default
#line hidden
            EndContext();
            BeginContext(1889, 575, true);
            WriteLiteral(@"
                                </div>
                            </div>
                        </div>
                        <div class=""clearfix""></div>
                        <div class=""row-fluid "" style=""margin-top:15px !important;"">
                            <div class=""form-group pull-right"">

                            </div>
                            <table id=""coaTable"" class="" table order-list"">
                                <thead class=""lite_bg"">
                                    <tr>

                                        <th>");
            EndContext();
            BeginContext(2465, 40, false);
#line 52 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Accounting\ShowTransaction.cshtml"
                                       Write(Localizer["ShowTransaction.AccountType"]);

#line default
#line hidden
            EndContext();
            BeginContext(2505, 51, true);
            WriteLiteral("</th>\r\n                                        <th>");
            EndContext();
            BeginContext(2557, 40, false);
#line 53 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Accounting\ShowTransaction.cshtml"
                                       Write(Localizer["ShowTransaction.AccountHead"]);

#line default
#line hidden
            EndContext();
            BeginContext(2597, 299, true);
            WriteLiteral(@"</th>
                                        <th>Debit</th>
                                        <th>Credit</th>
                                        <th></th>
                                    </tr>

                                </thead>
                                <tbody>
");
            EndContext();
#line 61 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Accounting\ShowTransaction.cshtml"
                                     foreach (var transactionEntryModel in Model.TransactionViewModel.TransactionEntries)
                                    {

#line default
#line hidden
            BeginContext(3058, 146, true);
            WriteLiteral("                                        <tr>\r\n\r\n                                            <td>\r\n                                                ");
            EndContext();
            BeginContext(3205, 88, false);
#line 66 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Accounting\ShowTransaction.cshtml"
                                           Write(Html.DisplayFor(model => @transactionEntryModel.AccountHead.AccountType.AccountTypeName));

#line default
#line hidden
            EndContext();
            BeginContext(3293, 151, true);
            WriteLiteral("\r\n                                            </td>\r\n                                            <td>\r\n                                                ");
            EndContext();
            BeginContext(3445, 76, false);
#line 69 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Accounting\ShowTransaction.cshtml"
                                           Write(Html.DisplayFor(model => @transactionEntryModel.AccountHead.AccountHeadName));

#line default
#line hidden
            EndContext();
            BeginContext(3521, 103, true);
            WriteLiteral("\r\n                                            </td>\r\n                                            <td>\r\n");
            EndContext();
#line 72 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Accounting\ShowTransaction.cshtml"
                                                 if (@transactionEntryModel.AccountHead.AccountType.TransactionType == AccountTransactionTypeEnum.Debit)
                                                {
                                                    

#line default
#line hidden
            BeginContext(3882, 55, false);
#line 74 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Accounting\ShowTransaction.cshtml"
                                               Write(Html.DisplayFor(model => @transactionEntryModel.Amount));

#line default
#line hidden
            EndContext();
#line 74 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Accounting\ShowTransaction.cshtml"
                                                                                                            
                                                }

#line default
#line hidden
            BeginContext(3990, 101, true);
            WriteLiteral("                                            </td>\r\n                                            <td>\r\n");
            EndContext();
#line 78 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Accounting\ShowTransaction.cshtml"
                                                 if (@transactionEntryModel.AccountHead.AccountType.TransactionType == AccountTransactionTypeEnum.Credit)
                                                {
                                                    

#line default
#line hidden
            BeginContext(4350, 55, false);
#line 80 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Accounting\ShowTransaction.cshtml"
                                               Write(Html.DisplayFor(model => @transactionEntryModel.Amount));

#line default
#line hidden
            EndContext();
#line 80 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Accounting\ShowTransaction.cshtml"
                                                                                                            
                                                }

#line default
#line hidden
            BeginContext(4458, 203, true);
            WriteLiteral("                                                \r\n                                            </td>\r\n                                            <td></td>\r\n                                        </tr>\r\n");
            EndContext();
#line 86 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Accounting\ShowTransaction.cshtml"
                                    }

#line default
#line hidden
            BeginContext(4700, 216, true);
            WriteLiteral("                                        <tr>\r\n                                            <td></td>\r\n                                            <td><b>Total</b></td>\r\n                                            <td>");
            EndContext();
            BeginContext(4917, 42, false);
#line 90 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Accounting\ShowTransaction.cshtml"
                                           Write(Html.DisplayFor(model => model.TotalDebit));

#line default
#line hidden
            EndContext();
            BeginContext(4959, 55, true);
            WriteLiteral("</td>\r\n                                            <td>");
            EndContext();
            BeginContext(5015, 43, false);
#line 91 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Accounting\ShowTransaction.cshtml"
                                           Write(Html.DisplayFor(model => model.TotalCredit));

#line default
#line hidden
            EndContext();
            BeginContext(5058, 491, true);
            WriteLiteral(@"</td>
                                            <td></td>
                                        </tr>
                                </tbody>

                            </table>
                        </div>
                        <div class=""clearfix""></div>
                        <div class=""row-fluid "">
                            <div class=""form-group"">
                                <button type=""button"" class=""btn custom-btn custom-cancel-btn pull-center-btn""");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 5549, "\"", 5648, 3);
            WriteAttributeValue("", 5559, "location.href=\'/BranchMedium/ViewAccount?branchMediumId=", 5559, 56, true);
#line 101 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Accounting\ShowTransaction.cshtml"
WriteAttributeValue("", 5615, Model.TransactionBranchMediumId, 5615, 32, false);

#line default
#line hidden
            WriteAttributeValue("", 5647, "\'", 5647, 1, true);
            EndWriteAttribute();
            BeginContext(5649, 192, true);
            WriteLiteral(">Back</button>\r\n                            </div>\r\n\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ShowTransactionViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591