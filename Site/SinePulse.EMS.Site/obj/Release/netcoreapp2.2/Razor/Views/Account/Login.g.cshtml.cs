#pragma checksum "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Account\Login.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8e7ec9f82e219b8bd45ef7fa412b684a115d24da"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Account_Login), @"mvc.1.0.view", @"/Views/Account/Login.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Account/Login.cshtml", typeof(AspNetCore.Views_Account_Login))]
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
#line 2 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Account\Login.cshtml"
using Microsoft.AspNetCore.Mvc.Localization;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8e7ec9f82e219b8bd45ef7fa412b684a115d24da", @"/Views/Account/Login.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9011cacf54d8ae45691a3deaab04c36239ace056", @"/Views/_ViewImports.cshtml")]
    public class Views_Account_Login : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<SinePulse.EMS.Site.Models.LoginViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-control placeholder-no-fix"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", "text", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("autocomplete", new global::Microsoft.AspNetCore.Html.HtmlString("off"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", "password", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Login", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("login-form"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("forget-form"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("action", new global::Microsoft.AspNetCore.Html.HtmlString("index.html"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(129, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 5 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Account\Login.cshtml"
  
  ViewData["Title"] = "Login";
  Layout = "~/Views/Shared/_LoginLayout.cshtml";

#line default
#line hidden
            BeginContext(220, 98, true);
            WriteLiteral("<div class=\"content\">\r\n  <!-- BEGIN LOGO -->\r\n  <!-- END LOGO -->\r\n  <!-- BEGIN LOGIN FORM -->\r\n  ");
            EndContext();
            BeginContext(318, 1619, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8e7ec9f82e219b8bd45ef7fa412b684a115d24da7164", async() => {
                BeginContext(362, 29, true);
                WriteLiteral("\r\n    <h3 class=\"form-title\">");
                EndContext();
                BeginContext(392, 33, false);
#line 14 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Account\Login.cshtml"
                      Write(Localizer["Login.LoginToAccount"]);

#line default
#line hidden
                EndContext();
                BeginContext(425, 138, true);
                WriteLiteral("</h3>\r\n    <div class=\"alert alert-danger display-hide\">\r\n      <button class=\"close\" data-close=\"alert\"></button>\r\n      <span>\r\n        ");
                EndContext();
                BeginContext(564, 40, false);
#line 18 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Account\Login.cshtml"
   Write(Localizer["Login.EnterUsernamePassword"]);

#line default
#line hidden
                EndContext();
                BeginContext(604, 214, true);
                WriteLiteral("\r\n      </span>\r\n    </div>\r\n    <div class=\"form-group\">\r\n      <!--ie8, ie9 does not support html5 placeholder, so we just show field title for that-->\r\n      <label class=\"control-label visible-ie8 visible-ie9\">");
                EndContext();
                BeginContext(819, 27, false);
#line 23 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Account\Login.cshtml"
                                                      Write(Localizer["Login.UserName"]);

#line default
#line hidden
                EndContext();
                BeginContext(846, 86, true);
                WriteLiteral("</label>\r\n      <div class=\"input-icon\">\r\n        <i class=\"fa fa-user\"></i>\r\n        ");
                EndContext();
                BeginContext(932, 142, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "8e7ec9f82e219b8bd45ef7fa412b684a115d24da9265", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
#line 26 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Account\Login.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.UserName);

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
                BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "placeholder", 1, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#line 26 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Account\Login.cshtml"
AddHtmlAttributeValue("", 1042, Localizer["Login.UserName"], 1042, 28, false);

#line default
#line hidden
                EndAddHtmlAttributeValues(__tagHelperExecutionContext);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(1074, 117, true);
                WriteLiteral("\r\n      </div>\r\n    </div>\r\n    <div class=\"form-group\">\r\n      <label class=\"control-label visible-ie8 visible-ie9\">");
                EndContext();
                BeginContext(1192, 27, false);
#line 30 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Account\Login.cshtml"
                                                      Write(Localizer["Login.Password"]);

#line default
#line hidden
                EndContext();
                BeginContext(1219, 86, true);
                WriteLiteral("</label>\r\n      <div class=\"input-icon\">\r\n        <i class=\"fa fa-lock\"></i>\r\n        ");
                EndContext();
                BeginContext(1305, 146, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "8e7ec9f82e219b8bd45ef7fa412b684a115d24da12406", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
#line 33 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Account\Login.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.Password);

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_3.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
                BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "placeholder", 1, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#line 33 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Account\Login.cshtml"
AddHtmlAttributeValue("", 1419, Localizer["Login.Password"], 1419, 28, false);

#line default
#line hidden
                EndAddHtmlAttributeValues(__tagHelperExecutionContext);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(1451, 161, true);
                WriteLiteral("\r\n\r\n      </div>\r\n    </div>\r\n    <div class=\"form-actions\">\r\n      <label class=\"rememberme check\">\r\n        <input type=\"checkbox\" name=\"remember\" value=\"1\" />");
                EndContext();
                BeginContext(1613, 27, false);
#line 39 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Account\Login.cshtml"
                                                      Write(Localizer["Login.Remember"]);

#line default
#line hidden
                EndContext();
                BeginContext(1640, 91, true);
                WriteLiteral("\r\n      </label>\r\n      <button type=\"submit\" class=\"btn btn-success uppercase pull-right\">");
                EndContext();
                BeginContext(1732, 24, false);
#line 41 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Account\Login.cshtml"
                                                                    Write(Localizer["Login.Login"]);

#line default
#line hidden
                EndContext();
                BeginContext(1756, 11, true);
                WriteLiteral("</button>\r\n");
                EndContext();
                BeginContext(1912, 18, true);
                WriteLiteral("\r\n    </div>\r\n\r\n  ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1937, 70, true);
            WriteLiteral("\r\n  <!-- END LOGIN FORM -->\r\n  <!-- BEGIN FORGOT PASSWORD FORM -->\r\n  ");
            EndContext();
            BeginContext(2007, 630, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8e7ec9f82e219b8bd45ef7fa412b684a115d24da17614", async() => {
                BeginContext(2067, 10, true);
                WriteLiteral("\r\n    <h3>");
                EndContext();
                BeginContext(2078, 33, false);
#line 50 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Account\Login.cshtml"
   Write(Localizer["Login.ForgetPassword"]);

#line default
#line hidden
                EndContext();
                BeginContext(2111, 22, true);
                WriteLiteral("</h3>\r\n    <p>\r\n      ");
                EndContext();
                BeginContext(2134, 44, false);
#line 52 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Account\Login.cshtml"
 Write(Localizer["Login.EnterEmailToResetPassword"]);

#line default
#line hidden
                EndContext();
                BeginContext(2178, 125, true);
                WriteLiteral("\r\n    </p>\r\n    <div class=\"form-group\">\r\n      <input class=\"form-control placeholder-no-fix\" type=\"text\" autocomplete=\"off\"");
                EndContext();
                BeginWriteAttribute("placeholder", " placeholder=\"", 2303, "\"", 2342, 1);
#line 55 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Account\Login.cshtml"
WriteAttributeValue("", 2317, Localizer["Login.Email"], 2317, 25, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(2343, 128, true);
                WriteLiteral(" name=\"email\" />\r\n    </div>\r\n    <div class=\"form-actions\">\r\n      <button type=\"button\" id=\"back-btn\" class=\"btn btn-default\">");
                EndContext();
                BeginContext(2472, 23, false);
#line 58 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Account\Login.cshtml"
                                                             Write(Localizer["Login.Back"]);

#line default
#line hidden
                EndContext();
                BeginContext(2495, 84, true);
                WriteLiteral("</button>\r\n      <button type=\"submit\" class=\"btn btn-success uppercase pull-right\">");
                EndContext();
                BeginContext(2580, 25, false);
#line 59 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Account\Login.cshtml"
                                                                    Write(Localizer["Login.Submit"]);

#line default
#line hidden
                EndContext();
                BeginContext(2605, 25, true);
                WriteLiteral("</button>\r\n    </div>\r\n  ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_7);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_8.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2637, 51, true);
            WriteLiteral("\r\n  <!-- END FORGOT PASSWORD FORM -->\r\n\r\n</div>\r\n\r\n");
            EndContext();
            DefineSection("Scripts", async() => {
                BeginContext(2706, 2, true);
                WriteLiteral("\r\n");
                EndContext();
#line 67 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Account\Login.cshtml"
    await Html.RenderPartialAsync("_ValidationScriptsPartial");

#line default
#line hidden
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<SinePulse.EMS.Site.Models.LoginViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
