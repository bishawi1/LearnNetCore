#pragma checksum "E:\Home Programs\Web\MultiSolution\MultiSolution\MultiSolution\Views\Suppliers\ListSuppliers.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6f43d29797b913474076196084928414a6e0ae4a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Suppliers_ListSuppliers), @"mvc.1.0.view", @"/Views/Suppliers/ListSuppliers.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Suppliers/ListSuppliers.cshtml", typeof(AspNetCore.Views_Suppliers_ListSuppliers))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6f43d29797b913474076196084928414a6e0ae4a", @"/Views/Suppliers/ListSuppliers.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c7151bfa4a517a41b78302f3d3fd56968e597610", @"/Views/_ViewImports.cshtml")]
    public class Views_Suppliers_ListSuppliers : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Supplier>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-outline-primary mb-3"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("width:auto"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Create", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Suppliers", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-outline-primary btn-sm rounded-circle"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Details", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 2 "E:\Home Programs\Web\MultiSolution\MultiSolution\MultiSolution\Views\Suppliers\ListSuppliers.cshtml"
  
    ViewData["Title"] = "ListCustomers";

#line default
#line hidden
            BeginContext(72, 50, true);
            WriteLiteral("\r\n<h3 class=\"text-center\">List of Suppliers</h3>\r\n");
            EndContext();
            BeginContext(122, 143, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6f43d29797b913474076196084928414a6e0ae4a7118", async() => {
                BeginContext(228, 33, true);
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
            BeginContext(265, 498, true);
            WriteLiteral(@"
<div style=""border:solid 1px black; padding:3px;"">
    <table class=""table table-hover gridtable"" id=""myTable"">
        <thead class=""thead-dark"">
            <tr>
                <th>Name</th>
                <th>Addresss</th>
                <th>MobileNo</th>
                <th>phone</th>
                <th>Fax</th>
                <th>Email</th>
                <th style=""width:30px;""></th>
                <th style=""width:30px;""></th>

            </tr>
        </thead>
");
            EndContext();
#line 23 "E:\Home Programs\Web\MultiSolution\MultiSolution\MultiSolution\Views\Suppliers\ListSuppliers.cshtml"
         foreach (var supplier in Model)
        {

#line default
#line hidden
            BeginContext(816, 22, true);
            WriteLiteral("    <tr>\r\n        <td>");
            EndContext();
            BeginContext(839, 21, false);
#line 26 "E:\Home Programs\Web\MultiSolution\MultiSolution\MultiSolution\Views\Suppliers\ListSuppliers.cshtml"
       Write(supplier.SupplierName);

#line default
#line hidden
            EndContext();
            BeginContext(860, 19, true);
            WriteLiteral("</td>\r\n        <td>");
            EndContext();
            BeginContext(880, 16, false);
#line 27 "E:\Home Programs\Web\MultiSolution\MultiSolution\MultiSolution\Views\Suppliers\ListSuppliers.cshtml"
       Write(supplier.Address);

#line default
#line hidden
            EndContext();
            BeginContext(896, 19, true);
            WriteLiteral("</td>\r\n        <td>");
            EndContext();
            BeginContext(916, 17, false);
#line 28 "E:\Home Programs\Web\MultiSolution\MultiSolution\MultiSolution\Views\Suppliers\ListSuppliers.cshtml"
       Write(supplier.MobileNo);

#line default
#line hidden
            EndContext();
            BeginContext(933, 19, true);
            WriteLiteral("</td>\r\n        <td>");
            EndContext();
            BeginContext(953, 14, false);
#line 29 "E:\Home Programs\Web\MultiSolution\MultiSolution\MultiSolution\Views\Suppliers\ListSuppliers.cshtml"
       Write(supplier.Phone);

#line default
#line hidden
            EndContext();
            BeginContext(967, 19, true);
            WriteLiteral("</td>\r\n        <td>");
            EndContext();
            BeginContext(987, 12, false);
#line 30 "E:\Home Programs\Web\MultiSolution\MultiSolution\MultiSolution\Views\Suppliers\ListSuppliers.cshtml"
       Write(supplier.Fax);

#line default
#line hidden
            EndContext();
            BeginContext(999, 19, true);
            WriteLiteral("</td>\r\n        <td>");
            EndContext();
            BeginContext(1019, 14, false);
#line 31 "E:\Home Programs\Web\MultiSolution\MultiSolution\MultiSolution\Views\Suppliers\ListSuppliers.cshtml"
       Write(supplier.Email);

#line default
#line hidden
            EndContext();
            BeginContext(1033, 19, true);
            WriteLiteral("</td>\r\n        <td>");
            EndContext();
            BeginContext(1052, 181, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6f43d29797b913474076196084928414a6e0ae4a11922", async() => {
                BeginContext(1204, 25, true);
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
#line 32 "E:\Home Programs\Web\MultiSolution\MultiSolution\MultiSolution\Views\Suppliers\ListSuppliers.cshtml"
                                                                                                                       WriteLiteral(supplier.Id);

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
            BeginContext(1233, 33, true);
            WriteLiteral("</td>\r\n        <td>\r\n            ");
            EndContext();
            BeginContext(1266, 1068, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6f43d29797b913474076196084928414a6e0ae4a14785", async() => {
                BeginContext(1342, 23, true);
                WriteLiteral("\r\n                <span");
                EndContext();
                BeginWriteAttribute("id", " id=\"", 1365, "\"", 1400, 2);
                WriteAttributeValue("", 1370, "confirmDeleteSpan_", 1370, 18, true);
#line 35 "E:\Home Programs\Web\MultiSolution\MultiSolution\MultiSolution\Views\Suppliers\ListSuppliers.cshtml"
WriteAttributeValue("", 1388, supplier.Id, 1388, 12, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(1401, 259, true);
                WriteLiteral(@" style=""display:none"">
                    <span>Are you sure you want to delete?</span>
                    <button type=""submit"" class=""btn btn-danger""><i class=""fa fa-check-circle""></i>Yes</button>
                    <a href=""#"" class=""btn btn-primary""");
                EndContext();
                BeginWriteAttribute("onclick", "\r\n                       onclick=\"", 1660, "\"", 1729, 3);
                WriteAttributeValue("", 1694, "confirmDelete(\'", 1694, 15, true);
#line 39 "E:\Home Programs\Web\MultiSolution\MultiSolution\MultiSolution\Views\Suppliers\ListSuppliers.cshtml"
WriteAttributeValue("", 1709, supplier.Id, 1709, 12, false);

#line default
#line hidden
                WriteAttributeValue("", 1721, "\',false)", 1721, 8, true);
                EndWriteAttribute();
                BeginContext(1730, 90, true);
                WriteLiteral("><i class=\"fa fa-times-circle\"></i> No</a>\r\n                </span>\r\n                <span");
                EndContext();
                BeginWriteAttribute("id", " id=\"", 1820, "\"", 1848, 2);
                WriteAttributeValue("", 1825, "deleteSpan_", 1825, 11, true);
#line 41 "E:\Home Programs\Web\MultiSolution\MultiSolution\MultiSolution\Views\Suppliers\ListSuppliers.cshtml"
WriteAttributeValue("", 1836, supplier.Id, 1836, 12, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(1849, 107, true);
                WriteLiteral(">\r\n                    <a href=\"#\" class=\"btn btn-outline-danger  btn-sm rounded-circle\" style=\"width:auto\"");
                EndContext();
                BeginWriteAttribute("onclick", "\r\n                       onclick=\"", 1956, "\"", 2024, 3);
                WriteAttributeValue("", 1990, "confirmDelete(\'", 1990, 15, true);
#line 43 "E:\Home Programs\Web\MultiSolution\MultiSolution\MultiSolution\Views\Suppliers\ListSuppliers.cshtml"
WriteAttributeValue("", 2005, supplier.Id, 2005, 12, false);

#line default
#line hidden
                WriteAttributeValue("", 2017, "\',true)", 2017, 7, true);
                EndWriteAttribute();
                BeginContext(2025, 59, true);
                WriteLiteral("><i class=\"fa fa-trash\"></i></a>\r\n                </span>\r\n");
                EndContext();
                BeginContext(2313, 14, true);
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
#line 34 "E:\Home Programs\Web\MultiSolution\MultiSolution\MultiSolution\Views\Suppliers\ListSuppliers.cshtml"
                                                WriteLiteral(supplier.Id);

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
            BeginContext(2334, 28, true);
            WriteLiteral("\r\n        </td>\r\n    </tr>\r\n");
            EndContext();
#line 53 "E:\Home Programs\Web\MultiSolution\MultiSolution\MultiSolution\Views\Suppliers\ListSuppliers.cshtml"
        }

#line default
#line hidden
            BeginContext(2373, 24, true);
            WriteLiteral("    </table>\r\n</div>\r\n\r\n");
            EndContext();
            DefineSection("Scripts", async() => {
                BeginContext(2414, 6, true);
                WriteLiteral("\r\n    ");
                EndContext();
                BeginContext(2420, 44, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6f43d29797b913474076196084928414a6e0ae4a21091", async() => {
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
                BeginContext(2464, 2, true);
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Supplier>> Html { get; private set; }
    }
}
#pragma warning restore 1591
