#pragma checksum "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\ViewBankInfo.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e22c9ed0435c8bf28054244d895a9a13f5b15099"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_BankInfo_ViewBankInfo), @"mvc.1.0.view", @"/Views/BankInfo/ViewBankInfo.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/BankInfo/ViewBankInfo.cshtml", typeof(AspNetCore.Views_BankInfo_ViewBankInfo))]
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
#line 1 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\ViewBankInfo.cshtml"
using SinePulse.EMS.Domain.Features;

#line default
#line hidden
#line 2 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\ViewBankInfo.cshtml"
using Microsoft.AspNetCore.Mvc.Localization;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e22c9ed0435c8bf28054244d895a9a13f5b15099", @"/Views/BankInfo/ViewBankInfo.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9011cacf54d8ae45691a3deaab04c36239ace056", @"/Views/_ViewImports.cshtml")]
    public class Views_BankInfo_ViewBankInfo : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ShowBankViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(118, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 5 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\ViewBankInfo.cshtml"
  
  ViewData["Title"] = "ViewBankInfo";
  Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
            BeginContext(211, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(239, 824, true);
            WriteLiteral(@"<div class=""page-container"">
  <!-- BEGIN CONTENT -->
  <div class=""page-content-wrapper"">
    <div class=""page-content"">
      <!-- END PAGE HEADER-->
      <!-- BEGIN PAGE CONTENT-->
      <div class=""row"">
        <div class=""col-md-12"">
          <!-- BEGIN PROFILE SIDEBAR -->
          <div class=""profile-sidebar"">
            <!-- PORTLET MAIN -->
              <div class=""portlet light profile-sidebar-portlet"">
                  <!-- SIDEBAR USERPIC -->
                  <div class=""profile-userpic"">
                      <img src=""../../img/school.png"" class=""img-responsive"" alt="""">
                  </div>
                  <!-- END SIDEBAR USERPIC -->
                  <!-- SIDEBAR USER TITLE -->
                  <div class=""profile-usertitle-name small-title"">
                      ");
            EndContext();
            BeginContext(1064, 45, false);
#line 30 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\ViewBankInfo.cshtml"
                 Write(Html.DisplayFor(model => model.Bank.BankName));

#line default
#line hidden
            EndContext();
            BeginContext(1109, 101, true);
            WriteLiteral("\r\n                  </div>\r\n                  <div class=\"profile-stat-text\">\r\n                      ");
            EndContext();
            BeginContext(1211, 30, false);
#line 33 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\ViewBankInfo.cshtml"
                 Write(Localizer["ViewBankInfo.Bank"]);

#line default
#line hidden
            EndContext();
            BeginContext(1241, 110, true);
            WriteLiteral("\r\n                  </div>\r\n                  <hr />\r\n                  <div class=\"profile-usertitle-name\">\r\n");
            EndContext();
#line 37 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\ViewBankInfo.cshtml"
                       if (Model.HasPermission(Feature.InstituteEnum.ViewInstitute.ToString()))
                      {

#line default
#line hidden
            BeginContext(1473, 49, true);
            WriteLiteral("                          <a class=\"primary-link\"");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 1522, "\"", 1610, 2);
            WriteAttributeValue("", 1529, "/Institute/ViewInstitute?instituteId=", 1529, 37, true);
#line 39 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\ViewBankInfo.cshtml"
WriteAttributeValue("", 1566, Model.Bank.BranchMedium.Branch.Institute.Id, 1566, 44, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1611, 33, true);
            WriteLiteral(">\r\n                              ");
            EndContext();
            BeginContext(1645, 83, false);
#line 40 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\ViewBankInfo.cshtml"
                         Write(Html.DisplayFor(model => model.Bank.BranchMedium.Branch.Institute.OrganisationName));

#line default
#line hidden
            EndContext();
            BeginContext(1728, 34, true);
            WriteLiteral("\r\n                          </a>\r\n");
            EndContext();
#line 42 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\ViewBankInfo.cshtml"
                      }
                      else
                      {
                          

#line default
#line hidden
            BeginContext(1867, 83, false);
#line 45 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\ViewBankInfo.cshtml"
                     Write(Html.DisplayFor(model => model.Bank.BranchMedium.Branch.Institute.OrganisationName));

#line default
#line hidden
            EndContext();
#line 45 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\ViewBankInfo.cshtml"
                                                                                                              
                      }

#line default
#line hidden
            BeginContext(1977, 99, true);
            WriteLiteral("                  </div>\r\n                  <div class=\"profile-stat-text\">\r\n                      ");
            EndContext();
            BeginContext(2077, 35, false);
#line 49 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\ViewBankInfo.cshtml"
                 Write(Localizer["ViewBankInfo.Institute"]);

#line default
#line hidden
            EndContext();
            BeginContext(2112, 110, true);
            WriteLiteral("\r\n                  </div>\r\n                  <hr />\r\n                  <div class=\"profile-usertitle-name\">\r\n");
            EndContext();
#line 53 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\ViewBankInfo.cshtml"
                       if (Model.HasPermission(Feature.BranchEnum.ViewBranch.ToString()))
                      {

#line default
#line hidden
            BeginContext(2338, 49, true);
            WriteLiteral("                          <a class=\"primary-link\"");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 2387, "\"", 2456, 2);
            WriteAttributeValue("", 2394, "/Branch/ViewBranch?branchId=", 2394, 28, true);
#line 55 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\ViewBankInfo.cshtml"
WriteAttributeValue("", 2422, Model.Bank.BranchMedium.Branch.Id, 2422, 34, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(2457, 33, true);
            WriteLiteral(">\r\n                              ");
            EndContext();
            BeginContext(2491, 67, false);
#line 56 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\ViewBankInfo.cshtml"
                         Write(Html.DisplayFor(model => model.Bank.BranchMedium.Branch.BranchName));

#line default
#line hidden
            EndContext();
            BeginContext(2558, 34, true);
            WriteLiteral("\r\n                          </a>\r\n");
            EndContext();
#line 58 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\ViewBankInfo.cshtml"
                      }
                      else
                      {
                          

#line default
#line hidden
            BeginContext(2697, 67, false);
#line 61 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\ViewBankInfo.cshtml"
                     Write(Html.DisplayFor(model => model.Bank.BranchMedium.Branch.BranchName));

#line default
#line hidden
            EndContext();
#line 61 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\ViewBankInfo.cshtml"
                                                                                              
                      }

#line default
#line hidden
            BeginContext(2791, 99, true);
            WriteLiteral("                  </div>\r\n                  <div class=\"profile-stat-text\">\r\n                      ");
            EndContext();
            BeginContext(2891, 32, false);
#line 65 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\ViewBankInfo.cshtml"
                 Write(Localizer["ViewBankInfo.Branch"]);

#line default
#line hidden
            EndContext();
            BeginContext(2923, 110, true);
            WriteLiteral("\r\n                  </div>\r\n                  <hr />\r\n                  <div class=\"profile-usertitle-name\">\r\n");
            EndContext();
#line 69 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\ViewBankInfo.cshtml"
                       if (Model.HasPermission(Feature.BranchMediumEnum.ViewBranchMedium.ToString()))
                      {

#line default
#line hidden
            BeginContext(3161, 49, true);
            WriteLiteral("                          <a class=\"primary-link\"");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 3210, "\"", 3297, 2);
            WriteAttributeValue("", 3217, "/BranchMedium/ViewBranchMedium?branchMediumId=", 3217, 46, true);
#line 71 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\ViewBankInfo.cshtml"
WriteAttributeValue("", 3263, Model.Bank.BranchMedium.Medium.Id, 3263, 34, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(3298, 33, true);
            WriteLiteral(">\r\n                              ");
            EndContext();
            BeginContext(3332, 67, false);
#line 72 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\ViewBankInfo.cshtml"
                         Write(Html.DisplayFor(model => model.Bank.BranchMedium.Medium.MediumName));

#line default
#line hidden
            EndContext();
            BeginContext(3399, 34, true);
            WriteLiteral("\r\n                          </a>\r\n");
            EndContext();
#line 74 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\ViewBankInfo.cshtml"
                      }
                      else
                      {
                          

#line default
#line hidden
            BeginContext(3538, 67, false);
#line 77 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\ViewBankInfo.cshtml"
                     Write(Html.DisplayFor(model => model.Bank.BranchMedium.Medium.MediumName));

#line default
#line hidden
            EndContext();
#line 77 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\ViewBankInfo.cshtml"
                                                                                              
                      }

#line default
#line hidden
            BeginContext(3632, 99, true);
            WriteLiteral("                  </div>\r\n                  <div class=\"profile-stat-text\">\r\n                      ");
            EndContext();
            BeginContext(3732, 32, false);
#line 81 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\ViewBankInfo.cshtml"
                 Write(Localizer["ViewBankInfo.Medium"]);

#line default
#line hidden
            EndContext();
            BeginContext(3764, 132, true);
            WriteLiteral("\r\n                  </div>\r\n                  <hr />\r\n                  <div class=\"profile-usertitle-name\">\r\n                      ");
            EndContext();
            BeginContext(3897, 65, false);
#line 85 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\ViewBankInfo.cshtml"
                 Write(Html.DisplayFor(model => model.Bank.BranchMedium.Shift.ShiftName));

#line default
#line hidden
            EndContext();
            BeginContext(3962, 99, true);
            WriteLiteral("\r\n                  </div>\r\n                  <div class=\"profile-stat-text\">\r\n                    ");
            EndContext();
            BeginContext(4062, 31, false);
#line 88 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\ViewBankInfo.cshtml"
               Write(Localizer["ViewBankInfo.Shift"]);

#line default
#line hidden
            EndContext();
            BeginContext(4093, 1229, true);
            WriteLiteral(@"
                  </div>
                  <hr />
                  <br />
              </div>

          </div>
          <!-- END BEGIN PROFILE SIDEBAR -->
          <!-- BEGIN PROFILE CONTENT -->
          <div class=""profile-content"">
            <div class=""row"">

              <div class=""col-md-12"">
                <!-- BEGIN PORTLET -->
                <div class=""portlet light"">
                  <div class=""portlet-title tabbable-line"">
                    <div class=""page-toolbar custom-page-menu-bar"">
                      <div class=""btn-group"">
                        <ul style=""padding:0px;"">

                          <li class=""custom-page-menu dropdown primary-menu-item-li"">
                            <a href=""#"" class=""show-dropdown-on-hover"" data-toggle=""custom-page-menu"">
                              <button type=""button"" class=""btn btn-fit-height dark-bg dropdown-toggle"" data-toggle=""dropdown"" data-hover=""dropdown"" data-delay=""100"" data-close-others=""true"">
   ");
            WriteLiteral("                             <i class=\"fa fa-bars\"></i>\r\n                              </button>\r\n                            </a>\r\n                            <ul class=\"dropdown-menu light-arrow-only\">\r\n");
            EndContext();
#line 115 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\ViewBankInfo.cshtml"
                               if (Model.HasPermission(Feature.BankInfoEnum.AddBankBranch.ToString()))
                              {

#line default
#line hidden
            BeginContext(5459, 74, true);
            WriteLiteral("                                <li>\r\n                                  <a");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 5533, "\"", 5585, 2);
            WriteAttributeValue("", 5540, "/BankInfo/AddBankBranch?bankId=", 5540, 31, true);
#line 118 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\ViewBankInfo.cshtml"
WriteAttributeValue("", 5571, Model.Bank.Id, 5571, 14, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(5586, 40, true);
            WriteLiteral(">\r\n                                     ");
            EndContext();
            BeginContext(5627, 35, false);
#line 119 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\ViewBankInfo.cshtml"
                                Write(Localizer["ViewBankInfo.AddBranch"]);

#line default
#line hidden
            EndContext();
            BeginContext(5662, 81, true);
            WriteLiteral("\r\n                                  </a>\r\n                                </li>\r\n");
            EndContext();
#line 122 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\ViewBankInfo.cshtml"
                              }

#line default
#line hidden
            BeginContext(5776, 420, true);
            WriteLiteral(@"                            </ul>
                          </li>

                        </ul>

                      </div>
                    </div>
                    <ul class=""nav nav-tabs custom-page-tab"">
                      <li class=""active"">
                        <a href=""#tab_branch"" data-toggle=""tab"">
                          <h5 class=""caption-subject font-blue-madison bold uppercase"">");
            EndContext();
            BeginContext(6197, 32, false);
#line 133 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\ViewBankInfo.cshtml"
                                                                                  Write(Localizer["ViewBankInfo.Branch"]);

#line default
#line hidden
            EndContext();
            BeginContext(6229, 68, true);
            WriteLiteral("</h5>\r\n                        </a>\r\n                      </li>\r\n\r\n");
            EndContext();
            BeginContext(6572, 505, true);
            WriteLiteral(@"                    </ul>
                  </div>


                  <div class=""portlet-body"">
                    <!--BEGIN TABS-->
                    <div class=""tab-content"">
                      <div class=""active tab-pane"" id=""tab_branch"">
                        <div style=""min-height: 337px;"">
                          <table class=""table table-light"">
                            <thead>
                              <tr class=""uppercase"">
                                <th> ");
            EndContext();
            BeginContext(7078, 36, false);
#line 154 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\ViewBankInfo.cshtml"
                                Write(Localizer["ViewBankInfo.BranchName"]);

#line default
#line hidden
            EndContext();
            BeginContext(7114, 43, true);
            WriteLiteral("</th>\r\n                                <th>");
            EndContext();
            BeginContext(7158, 35, false);
#line 155 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\ViewBankInfo.cshtml"
                               Write(Localizer["ViewBankInfo.AccountNo"]);

#line default
#line hidden
            EndContext();
            BeginContext(7193, 43, true);
            WriteLiteral("</th>\r\n                                <th>");
            EndContext();
            BeginContext(7237, 37, false);
#line 156 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\ViewBankInfo.cshtml"
                               Write(Localizer["ViewBankInfo.AccountType"]);

#line default
#line hidden
            EndContext();
            BeginContext(7274, 43, true);
            WriteLiteral("</th>\r\n                                <th>");
            EndContext();
            BeginContext(7318, 32, false);
#line 157 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\ViewBankInfo.cshtml"
                               Write(Localizer["ViewBankInfo.Action"]);

#line default
#line hidden
            EndContext();
            BeginContext(7350, 121, true);
            WriteLiteral("</th>\r\n                              </tr>\r\n                            </thead>\r\n\r\n                            <tbody>\r\n");
            EndContext();
#line 162 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\ViewBankInfo.cshtml"
                                
                                if (Model.Bank.BankBranches != null && Model.Bank.BankBranches.Any())
                                {
                                  foreach (var bankBranch in Model.Bank.BankBranches)
                                  {
                                    if (bankBranch.BankAccounts.Any())
                                    {
                                      foreach (var bankAccount in bankBranch.BankAccounts)
                                      {

#line default
#line hidden
            BeginContext(8011, 96, true);
            WriteLiteral("                                        <tr>\r\n\r\n                                          <td>\r\n");
            EndContext();
#line 174 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\ViewBankInfo.cshtml"
                                             if (Model.HasPermission(Feature.BankInfoEnum.ViewBankBranch.ToString()))
                                            {

#line default
#line hidden
            BeginContext(8273, 48, true);
            WriteLiteral("                                              <a");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 8321, "\"", 8376, 2);
            WriteAttributeValue("", 8328, "/BankInfo/ViewBankBranch?branchId=", 8328, 34, true);
#line 176 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\ViewBankInfo.cshtml"
WriteAttributeValue("", 8362, bankBranch.Id, 8362, 14, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(8377, 51, true);
            WriteLiteral(">\r\n                                                ");
            EndContext();
            BeginContext(8429, 47, false);
#line 177 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\ViewBankInfo.cshtml"
                                           Write(Html.DisplayFor(model => bankBranch.BranchName));

#line default
#line hidden
            EndContext();
            BeginContext(8476, 54, true);
            WriteLiteral("\r\n                                              </a>\r\n");
            EndContext();
#line 179 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\ViewBankInfo.cshtml"
                                            }
                                            else
                                            {
                                              

#line default
#line hidden
            BeginContext(8721, 47, false);
#line 182 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\ViewBankInfo.cshtml"
                                         Write(Html.DisplayFor(model => bankBranch.BranchName));

#line default
#line hidden
            EndContext();
#line 182 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\ViewBankInfo.cshtml"
                                                                                              
                                            }

#line default
#line hidden
            BeginContext(8817, 141, true);
            WriteLiteral("                                          </td>\r\n                                          <td>\r\n                                            ");
            EndContext();
            BeginContext(8959, 51, false);
#line 186 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\ViewBankInfo.cshtml"
                                       Write(Html.DisplayFor(model => bankAccount.AccountNumber));

#line default
#line hidden
            EndContext();
            BeginContext(9010, 143, true);
            WriteLiteral("\r\n                                          </td>\r\n                                          <td>\r\n                                            ");
            EndContext();
            BeginContext(9154, 49, false);
#line 189 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\ViewBankInfo.cshtml"
                                       Write(Html.DisplayFor(model => bankAccount.AccountType));

#line default
#line hidden
            EndContext();
            BeginContext(9203, 99, true);
            WriteLiteral("\r\n                                          </td>\r\n                                          <td>\r\n");
            EndContext();
#line 192 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\ViewBankInfo.cshtml"
                                             if (Model.HasPermission(Feature.BankInfoEnum.EditBankBranch.ToString()))
                                            {

#line default
#line hidden
            BeginContext(9468, 48, true);
            WriteLiteral("                                              <a");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 9516, "\"", 9577, 2);
            WriteAttributeValue("", 9523, "/BankInfo/UpdateBankBranch?bankBranchId=", 9523, 40, true);
#line 194 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\ViewBankInfo.cshtml"
WriteAttributeValue("", 9563, bankBranch.Id, 9563, 14, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(9578, 65, true);
            WriteLiteral(" class=\"action-link\"><i class=\"fa fa-edit action-icon\"></i></a>\r\n");
            EndContext();
#line 195 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\ViewBankInfo.cshtml"
                                            }

#line default
#line hidden
            BeginContext(9690, 96, true);
            WriteLiteral("                                          </td>\r\n                                        </tr>\r\n");
            EndContext();
#line 198 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\ViewBankInfo.cshtml"
                                      }
                                    }
                                    else
                                    {

#line default
#line hidden
            BeginContext(9947, 90, true);
            WriteLiteral("                                      <tr>\r\n                                        <td>\r\n");
            EndContext();
#line 204 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\ViewBankInfo.cshtml"
                                           if (Model.HasPermission(Feature.BankInfoEnum.ViewBankBranch.ToString()))
                                          {

#line default
#line hidden
            BeginContext(10199, 46, true);
            WriteLiteral("                                            <a");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 10245, "\"", 10300, 2);
            WriteAttributeValue("", 10252, "/BankInfo/ViewBankBranch?branchId=", 10252, 34, true);
#line 206 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\ViewBankInfo.cshtml"
WriteAttributeValue("", 10286, bankBranch.Id, 10286, 14, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(10301, 49, true);
            WriteLiteral(">\r\n                                              ");
            EndContext();
            BeginContext(10351, 47, false);
#line 207 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\ViewBankInfo.cshtml"
                                         Write(Html.DisplayFor(model => bankBranch.BranchName));

#line default
#line hidden
            EndContext();
            BeginContext(10398, 52, true);
            WriteLiteral("\r\n                                            </a>\r\n");
            EndContext();
#line 209 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\ViewBankInfo.cshtml"
                                          }
                                          else
                                          {
                                            

#line default
#line hidden
            BeginContext(10633, 47, false);
#line 212 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\ViewBankInfo.cshtml"
                                       Write(Html.DisplayFor(model => bankBranch.BranchName));

#line default
#line hidden
            EndContext();
#line 212 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\ViewBankInfo.cshtml"
                                                                                            
                                          }

#line default
#line hidden
            BeginContext(10727, 369, true);
            WriteLiteral(@"                                        </td>
                                        <td>
                                          -
                                        </td>
                                        <td>
                                          -
                                        </td>
                                        <td>
");
            EndContext();
#line 222 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\ViewBankInfo.cshtml"
                                           if (Model.HasPermission(Feature.BankInfoEnum.EditBankBranch.ToString()))
                                          {

#line default
#line hidden
            BeginContext(11258, 46, true);
            WriteLiteral("                                            <a");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 11304, "\"", 11365, 2);
            WriteAttributeValue("", 11311, "/BankInfo/UpdateBankBranch?bankBranchId=", 11311, 40, true);
#line 224 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\ViewBankInfo.cshtml"
WriteAttributeValue("", 11351, bankBranch.Id, 11351, 14, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(11366, 65, true);
            WriteLiteral(" class=\"action-link\"><i class=\"fa fa-edit action-icon\"></i></a>\r\n");
            EndContext();
#line 225 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\ViewBankInfo.cshtml"
                                          }

#line default
#line hidden
            BeginContext(11476, 92, true);
            WriteLiteral("                                        </td>\r\n                                      </tr>\r\n");
            EndContext();
#line 228 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\ViewBankInfo.cshtml"
                                    }
                                  }
                                }
                              

#line default
#line hidden
            BeginContext(11712, 633, true);
            WriteLiteral(@"                            </tbody>
                          </table>
                        </div>
                      </div>
                      <div class=""tab-pane"" id=""tab_address"">
                        <div style=""min-height: 337px;"">
                          <div class=""row-fluid"">
                            <div class=""col-md-6"">
                              Abdus Sattar bhuiyan <br />
                              2-D, Jahangir Tower, <br />
                              Badda, Dhaka-1212
                              <div class=""uppercase profile-stat-title"">
                                ");
            EndContext();
            BeginContext(12346, 40, false);
#line 244 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\ViewBankInfo.cshtml"
                           Write(Localizer["ViewBankInfo.PresentAddress"]);

#line default
#line hidden
            EndContext();
            BeginContext(12386, 401, true);
            WriteLiteral(@"
                              </div>
                            </div>
                            <div class=""col-md-6"">
                              Abdus Sattar bhuiyan <br />
                              2-D, Jahangir Tower, <br />
                              Badda, Dhaka-1212
                              <div class=""uppercase profile-stat-title"">
                                ");
            EndContext();
            BeginContext(12788, 42, false);
#line 252 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\ViewBankInfo.cshtml"
                           Write(Localizer["ViewBankInfo.PermanentAddress"]);

#line default
#line hidden
            EndContext();
            BeginContext(12830, 483, true);
            WriteLiteral(@"
                              </div>
                            </div>
                          </div>
                        </div>
                      </div>
                    </div>

                  </div>
                  <!--END TABS-->
                </div>
              </div>
              <!-- END PORTLET -->
            </div>
          </div>

        </div>
        <!-- END PROFILE CONTENT -->
      </div>
    </div>
  </div>
</div>
");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ShowBankViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591