#pragma checksum "E:\Home Programs\Web\MultiSolution\MultiSolution\MultiSolution\Views\PurchaseOrders\ListPurchaseOrders.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8ddb6cc673a3afa060337beee3deccdea6f70297"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_PurchaseOrders_ListPurchaseOrders), @"mvc.1.0.view", @"/Views/PurchaseOrders/ListPurchaseOrders.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/PurchaseOrders/ListPurchaseOrders.cshtml", typeof(AspNetCore.Views_PurchaseOrders_ListPurchaseOrders))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8ddb6cc673a3afa060337beee3deccdea6f70297", @"/Views/PurchaseOrders/ListPurchaseOrders.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c7151bfa4a517a41b78302f3d3fd56968e597610", @"/Views/_ViewImports.cshtml")]
    public class Views_PurchaseOrders_ListPurchaseOrders : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<ListPurchaseOrderDetailsViewModel>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("nav-link btn btn-outline-primary  btn-sm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Create", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "PurchaseOrders", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("width:auto;"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-outline-primary btn-sm rounded-circle "), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("width:auto"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Details", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "DeleteCustomer", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_9 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/jqueryui/jquery-ui.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_10 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/CustomScript.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_11 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/printthis/printThis.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 2 "E:\Home Programs\Web\MultiSolution\MultiSolution\MultiSolution\Views\PurchaseOrders\ListPurchaseOrders.cshtml"
  
    ViewData["Title"] = "ListPurchaseOrders";

#line default
#line hidden
            BeginContext(102, 76, true);
            WriteLiteral("    <div class=\"flex-container\">\r\n        <div class=\"column\">\r\n            ");
            EndContext();
            BeginContext(178, 200, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8ddb6cc673a3afa060337beee3deccdea6f702978363", async() => {
                BeginContext(302, 72, true);
                WriteLiteral("<i class=\"fa fa-plus-circle\"></i> <span class=\"sr-only\">(current)</span>");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(378, 606, true);
            WriteLiteral(@"
        </div>
    </div>
        <div style="" border:solid 1px black; padding:3px;"">
            <table class=""table table-hover"" id=""myTable"">
                <thead class=""thead-dark"">
                    <tr>
                        <th>Order No</th>
                        <th>Date</th>
                        <th>Supplier</th>
                        <th>Currency</th>
                        <th>Total Price</th>
                        <th style=""width:30px;""></th>
                        <th style=""width:30px;""></th>

                    </tr>
                </thead>



");
            EndContext();
#line 27 "E:\Home Programs\Web\MultiSolution\MultiSolution\MultiSolution\Views\PurchaseOrders\ListPurchaseOrders.cshtml"
                 foreach (var order in Model)
                {


#line default
#line hidden
            BeginContext(1052, 54, true);
            WriteLiteral("                    <tr>\r\n                        <td>");
            EndContext();
            BeginContext(1107, 23, false);
#line 31 "E:\Home Programs\Web\MultiSolution\MultiSolution\MultiSolution\Views\PurchaseOrders\ListPurchaseOrders.cshtml"
                       Write(order.PurchaseOrderCode);

#line default
#line hidden
            EndContext();
            BeginContext(1130, 35, true);
            WriteLiteral("</td>\r\n                        <td>");
            EndContext();
            BeginContext(1166, 46, false);
#line 32 "E:\Home Programs\Web\MultiSolution\MultiSolution\MultiSolution\Views\PurchaseOrders\ListPurchaseOrders.cshtml"
                       Write(order.PurchaseOrderDate.ToString("dd/MM/yyyy"));

#line default
#line hidden
            EndContext();
            BeginContext(1212, 35, true);
            WriteLiteral("</td>\r\n                        <td>");
            EndContext();
            BeginContext(1248, 18, false);
#line 33 "E:\Home Programs\Web\MultiSolution\MultiSolution\MultiSolution\Views\PurchaseOrders\ListPurchaseOrders.cshtml"
                       Write(order.SupplierName);

#line default
#line hidden
            EndContext();
            BeginContext(1266, 35, true);
            WriteLiteral("</td>\r\n                        <td>");
            EndContext();
            BeginContext(1302, 18, false);
#line 34 "E:\Home Programs\Web\MultiSolution\MultiSolution\MultiSolution\Views\PurchaseOrders\ListPurchaseOrders.cshtml"
                       Write(order.CurrencyName);

#line default
#line hidden
            EndContext();
            BeginContext(1320, 35, true);
            WriteLiteral("</td>\r\n                        <td>");
            EndContext();
            BeginContext(1356, 16, false);
#line 35 "E:\Home Programs\Web\MultiSolution\MultiSolution\MultiSolution\Views\PurchaseOrders\ListPurchaseOrders.cshtml"
                       Write(order.TotalPrice);

#line default
#line hidden
            EndContext();
            BeginContext(1372, 35, true);
            WriteLiteral("</td>\r\n                        <td>");
            EndContext();
            BeginContext(1407, 184, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8ddb6cc673a3afa060337beee3deccdea6f7029713268", async() => {
                BeginContext(1562, 25, true);
                WriteLiteral("<i class=\"fa fa-eye\"></i>");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_6.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-Id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 36 "E:\Home Programs\Web\MultiSolution\MultiSolution\MultiSolution\Views\PurchaseOrders\ListPurchaseOrders.cshtml"
                                                                                                                                                                WriteLiteral(order.Id);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["Id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-Id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["Id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1591, 65, true);
            WriteLiteral("</td>\r\n                        <td>\r\n                            ");
            EndContext();
            BeginContext(1656, 999, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8ddb6cc673a3afa060337beee3deccdea6f7029716210", async() => {
                BeginContext(1729, 39, true);
                WriteLiteral("\r\n                                <span");
                EndContext();
                BeginWriteAttribute("id", " id=\"", 1768, "\"", 1800, 2);
                WriteAttributeValue("", 1773, "confirmDeleteSpan_", 1773, 18, true);
#line 39 "E:\Home Programs\Web\MultiSolution\MultiSolution\MultiSolution\Views\PurchaseOrders\ListPurchaseOrders.cshtml"
WriteAttributeValue("", 1791, order.Id, 1791, 9, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(1801, 307, true);
                WriteLiteral(@" style=""display:none"">
                                    <span>Are you sure you want to delete?</span>
                                    <button type=""submit"" class=""btn btn-danger""><i class=""fa fa-check-circle""></i>Yes</button>
                                    <a href=""#"" class=""btn btn-primary""");
                EndContext();
                BeginWriteAttribute("onclick", "\r\n                                       onclick=\"", 2108, "\"", 2190, 3);
                WriteAttributeValue("", 2158, "confirmDelete(\'", 2158, 15, true);
#line 43 "E:\Home Programs\Web\MultiSolution\MultiSolution\MultiSolution\Views\PurchaseOrders\ListPurchaseOrders.cshtml"
WriteAttributeValue("", 2173, order.Id, 2173, 9, false);

#line default
#line hidden
                WriteAttributeValue("", 2182, "\',false)", 2182, 8, true);
                EndWriteAttribute();
                BeginContext(2191, 122, true);
                WriteLiteral("><i class=\"fa fa-times-circle\"></i> No</a>\r\n                                </span>\r\n                                <span");
                EndContext();
                BeginWriteAttribute("id", " id=\"", 2313, "\"", 2338, 2);
                WriteAttributeValue("", 2318, "deleteSpan_", 2318, 11, true);
#line 45 "E:\Home Programs\Web\MultiSolution\MultiSolution\MultiSolution\Views\PurchaseOrders\ListPurchaseOrders.cshtml"
WriteAttributeValue("", 2329, order.Id, 2329, 9, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(2339, 122, true);
                WriteLiteral(">\r\n                                    <a href=\"#\" class=\"btn btn-outline-danger rounded-circle btn-sm\" style=\"width:auto\"");
                EndContext();
                BeginWriteAttribute("onclick", "\r\n                                       onclick=\"", 2461, "\"", 2542, 3);
                WriteAttributeValue("", 2511, "confirmDelete(\'", 2511, 15, true);
#line 47 "E:\Home Programs\Web\MultiSolution\MultiSolution\MultiSolution\Views\PurchaseOrders\ListPurchaseOrders.cshtml"
WriteAttributeValue("", 2526, order.Id, 2526, 9, false);

#line default
#line hidden
                WriteAttributeValue("", 2535, "\',true)", 2535, 7, true);
                EndWriteAttribute();
                BeginContext(2543, 105, true);
                WriteLiteral("><i class=\"fa fa-trash\"></i></a>\r\n                                </span>\r\n\r\n                            ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_7.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-Id", "Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 38 "E:\Home Programs\Web\MultiSolution\MultiSolution\MultiSolution\Views\PurchaseOrders\ListPurchaseOrders.cshtml"
                                                                WriteLiteral(order.Id);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["Id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-Id", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["Id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            BeginContext(2655, 60, true);
            WriteLiteral("\r\n                        </td>\r\n                    </tr>\r\n");
            EndContext();
#line 53 "E:\Home Programs\Web\MultiSolution\MultiSolution\MultiSolution\Views\PurchaseOrders\ListPurchaseOrders.cshtml"

                }

#line default
#line hidden
            BeginContext(2736, 42, true);
            WriteLiteral("\r\n\r\n            </table>\r\n        </div>\r\n");
            EndContext();
            DefineSection("Scripts", async() => {
                BeginContext(2803, 16, true);
                WriteLiteral("\r\n\r\n            ");
                EndContext();
                BeginContext(2819, 55, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8ddb6cc673a3afa060337beee3deccdea6f7029722712", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_9);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(2874, 14, true);
                WriteLiteral("\r\n            ");
                EndContext();
                BeginContext(2888, 44, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8ddb6cc673a3afa060337beee3deccdea6f7029723977", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_10);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(2932, 14, true);
                WriteLiteral("\r\n            ");
                EndContext();
                BeginContext(2946, 52, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8ddb6cc673a3afa060337beee3deccdea6f7029725243", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_11);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(2998, 10, true);
                WriteLiteral("\r\n        ");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<ListPurchaseOrderDetailsViewModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
