#pragma checksum "E:\Home Programs\Web\MultiSolution\MultiSolution\MultiSolution\Views\Settings\ListMainItems.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0f63ae306c0d6a8c994ce7a7be32329a27b12e41"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Settings_ListMainItems), @"mvc.1.0.view", @"/Views/Settings/ListMainItems.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Settings/ListMainItems.cshtml", typeof(AspNetCore.Views_Settings_ListMainItems))]
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
#line 1 "E:\Home Programs\Web\MultiSolution\MultiSolution\MultiSolution\Views\_ViewImports.cshtml"
using MSIS.ViewModels;

#line default
#line hidden
#line 2 "E:\Home Programs\Web\MultiSolution\MultiSolution\MultiSolution\Views\_ViewImports.cshtml"
using MSIS.Models;

#line default
#line hidden
#line 4 "E:\Home Programs\Web\MultiSolution\MultiSolution\MultiSolution\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0f63ae306c0d6a8c994ce7a7be32329a27b12e41", @"/Views/Settings/ListMainItems.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c7151bfa4a517a41b78302f3d3fd56968e597610", @"/Views/_ViewImports.cshtml")]
    public class Views_Settings_ListMainItems : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<MainItemViewModel>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-outline-primary mb-3"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("width:auto"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "CreateMainItem", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Settings", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-outline-primary btn-sm rounded-circle"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "MainItemDetails", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "DeleteSupplier", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/CustomScript.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "E:\Home Programs\Web\MultiSolution\MultiSolution\MultiSolution\Views\Settings\ListMainItems.cshtml"
  
    ViewData["Title"] = "Item Category List";

#line default
#line hidden
            BeginContext(86, 56, true);
            WriteLiteral("\r\n<h3 class=\"text-center\">List of Item Categories</h3>\r\n");
            EndContext();
            BeginContext(142, 150, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0f63ae306c0d6a8c994ce7a7be32329a27b12e417145", async() => {
                BeginContext(255, 33, true);
                WriteLiteral("<i class=\"fa fa-plus-circle\"></i>");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(292, 447, true);
            WriteLiteral(@"
<div style=""border:solid 1px black; padding:3px;"">
    <table class=""table table-hover gridtable"" id=""myTable"">
        <thead class=""thead-dark"">
            <tr>
                <th>Id</th>
                <th>Name</th>
                <th>Category Name</th>
                <th>Other Information</th>
                <th style=""width:30px;""></th>
                <th style=""width:30px;""></th>

            </tr>
        </thead>
");
            EndContext();
#line 21 "E:\Home Programs\Web\MultiSolution\MultiSolution\MultiSolution\Views\Settings\ListMainItems.cshtml"
         foreach (var mainItem in Model)
        {

#line default
#line hidden
            BeginContext(792, 22, true);
            WriteLiteral("    <tr>\r\n        <td>");
            EndContext();
            BeginContext(815, 11, false);
#line 24 "E:\Home Programs\Web\MultiSolution\MultiSolution\MultiSolution\Views\Settings\ListMainItems.cshtml"
       Write(mainItem.Id);

#line default
#line hidden
            EndContext();
            BeginContext(826, 19, true);
            WriteLiteral("</td>\r\n        <td>");
            EndContext();
            BeginContext(846, 21, false);
#line 25 "E:\Home Programs\Web\MultiSolution\MultiSolution\MultiSolution\Views\Settings\ListMainItems.cshtml"
       Write(mainItem.MainItemName);

#line default
#line hidden
            EndContext();
            BeginContext(867, 19, true);
            WriteLiteral("</td>\r\n        <td>");
            EndContext();
            BeginContext(887, 21, false);
#line 26 "E:\Home Programs\Web\MultiSolution\MultiSolution\MultiSolution\Views\Settings\ListMainItems.cshtml"
       Write(mainItem.CategoryName);

#line default
#line hidden
            EndContext();
            BeginContext(908, 19, true);
            WriteLiteral("</td>\r\n        <td>");
            EndContext();
            BeginContext(928, 25, false);
#line 27 "E:\Home Programs\Web\MultiSolution\MultiSolution\MultiSolution\Views\Settings\ListMainItems.cshtml"
       Write(mainItem.OtherInformation);

#line default
#line hidden
            EndContext();
            BeginContext(953, 19, true);
            WriteLiteral("</td>\r\n        <td>");
            EndContext();
            BeginContext(972, 188, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0f63ae306c0d6a8c994ce7a7be32329a27b12e4111172", async() => {
                BeginContext(1131, 25, true);
                WriteLiteral("<i class=\"fa fa-eye\"></i>");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-Id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 28 "E:\Home Programs\Web\MultiSolution\MultiSolution\MultiSolution\Views\Settings\ListMainItems.cshtml"
                                                                                                                              WriteLiteral(mainItem.Id);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["Id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-Id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["Id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1160, 33, true);
            WriteLiteral("</td>\r\n        <td>\r\n            ");
            EndContext();
            BeginContext(1193, 1068, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0f63ae306c0d6a8c994ce7a7be32329a27b12e4114041", async() => {
                BeginContext(1269, 23, true);
                WriteLiteral("\r\n                <span");
                EndContext();
                BeginWriteAttribute("id", " id=\"", 1292, "\"", 1327, 2);
                WriteAttributeValue("", 1297, "confirmDeleteSpan_", 1297, 18, true);
#line 31 "E:\Home Programs\Web\MultiSolution\MultiSolution\MultiSolution\Views\Settings\ListMainItems.cshtml"
WriteAttributeValue("", 1315, mainItem.Id, 1315, 12, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(1328, 259, true);
                WriteLiteral(@" style=""display:none"">
                    <span>Are you sure you want to delete?</span>
                    <button type=""submit"" class=""btn btn-danger""><i class=""fa fa-check-circle""></i>Yes</button>
                    <a href=""#"" class=""btn btn-primary""");
                EndContext();
                BeginWriteAttribute("onclick", "\r\n                       onclick=\"", 1587, "\"", 1656, 3);
                WriteAttributeValue("", 1621, "confirmDelete(\'", 1621, 15, true);
#line 35 "E:\Home Programs\Web\MultiSolution\MultiSolution\MultiSolution\Views\Settings\ListMainItems.cshtml"
WriteAttributeValue("", 1636, mainItem.Id, 1636, 12, false);

#line default
#line hidden
                WriteAttributeValue("", 1648, "\',false)", 1648, 8, true);
                EndWriteAttribute();
                BeginContext(1657, 90, true);
                WriteLiteral("><i class=\"fa fa-times-circle\"></i> No</a>\r\n                </span>\r\n                <span");
                EndContext();
                BeginWriteAttribute("id", " id=\"", 1747, "\"", 1775, 2);
                WriteAttributeValue("", 1752, "deleteSpan_", 1752, 11, true);
#line 37 "E:\Home Programs\Web\MultiSolution\MultiSolution\MultiSolution\Views\Settings\ListMainItems.cshtml"
WriteAttributeValue("", 1763, mainItem.Id, 1763, 12, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(1776, 107, true);
                WriteLiteral(">\r\n                    <a href=\"#\" class=\"btn btn-outline-danger  btn-sm rounded-circle\" style=\"width:auto\"");
                EndContext();
                BeginWriteAttribute("onclick", "\r\n                       onclick=\"", 1883, "\"", 1951, 3);
                WriteAttributeValue("", 1917, "confirmDelete(\'", 1917, 15, true);
#line 39 "E:\Home Programs\Web\MultiSolution\MultiSolution\MultiSolution\Views\Settings\ListMainItems.cshtml"
WriteAttributeValue("", 1932, mainItem.Id, 1932, 12, false);

#line default
#line hidden
                WriteAttributeValue("", 1944, "\',true)", 1944, 7, true);
                EndWriteAttribute();
                BeginContext(1952, 59, true);
                WriteLiteral("><i class=\"fa fa-trash\"></i></a>\r\n                </span>\r\n");
                EndContext();
                BeginContext(2240, 14, true);
                WriteLiteral("\r\n            ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_6.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-Id", "Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 30 "E:\Home Programs\Web\MultiSolution\MultiSolution\MultiSolution\Views\Settings\ListMainItems.cshtml"
                                                WriteLiteral(mainItem.Id);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["Id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-Id", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["Id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_7.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2261, 28, true);
            WriteLiteral("\r\n        </td>\r\n    </tr>\r\n");
            EndContext();
#line 49 "E:\Home Programs\Web\MultiSolution\MultiSolution\MultiSolution\Views\Settings\ListMainItems.cshtml"
        }

#line default
#line hidden
            BeginContext(2300, 24, true);
            WriteLiteral("    </table>\r\n</div>\r\n\r\n");
            EndContext();
            DefineSection("Scripts", async() => {
                BeginContext(2341, 6, true);
                WriteLiteral("\r\n    ");
                EndContext();
                BeginContext(2347, 44, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0f63ae306c0d6a8c994ce7a7be32329a27b12e4120341", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_8);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(2391, 2, true);
                WriteLiteral("\r\n");
                EndContext();
            }
            );
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<MainItemViewModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
