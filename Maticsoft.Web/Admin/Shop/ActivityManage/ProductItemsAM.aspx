﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/BasicNoFoot.Master" AutoEventWireup="true" CodeBehind="ProductItemsAM.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.ActivityManage.ProductItemsAM" %>
<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager, Version=7.2.0.0, Culture=neutral, PublicKeyToken=fb0a0fe055d40fd4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/admin/css/gridviewstyle.css" rel="stylesheet" type="text/css" />
    <script src="/admin/js/jquery/maticsoft.img.min.js" type="text/javascript"></script>
        <script src="/admin/js/jquery/jquery.scrollTo-min.js" type="text/javascript"></script>
    <script src="/admin/js/jquery/AMProduct.helper.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            resizeImg('.borderImage', 80, 80);
            $("[id$='ddlSupplier']").select2({ placeholder: "请选择" });
            $(".select2-container").css("vertical-align", "middle");
            resizeImg('.borderImage', 80, 80);
        });
    </script>
    <style type="text/css">
        .borderImage{width: 81px;height: 81px;border: 1px solid #CCC;text-align: center;}
    </style>
</asp:Content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div  style="background: white;width: 100%" id="relatedProc">
        <div class="advanceSearchArea clearfix">
            <!--预留显示高级搜索项区域-->
        </div>
        <div class="toptitle">
            <h3 class="title_height" style="margin-bottom: 5px">
                </h3>
        </div>
        <div class="Goodsgifts">
            <%--显示在左边的DIV--%>
            <div class="left">
                <div style="float:left"><h1><asp:Literal ID="litDesc" runat="server" Text="设置活动应用商品"></asp:Literal></h1></div>
                <%--<div  style="float:left; margin-left:5px"><h1><asp:Literal ID="litSup" runat="server" Text="设置活动应用商家"></asp:Literal></h1></div>--%>
                <div id="divLeftbuttom" style="display:block">
                    <asp:panel id="Panel1" runat="server" defaultbutton="btnSearch">
                        <table style="clear:both" id="tbSelect">
                            <tr>
                                <td colspan="3">
                                    <asp:Literal runat="server" ID="LitProductCategories" Text="商品分类" />：
                                    <abbr class="formselect">
                                    <asp:dropdownlist ID="drpProductCategory" runat="server">
                                    </asp:dropdownlist>
                                </abbr>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Literal runat="server" ID="LitProductName" Text="商品名称" />：
                                    <asp:textbox id="txtProductName" runat="server" Width="80px"/>
                                    <asp:Literal runat="server" ID="litProductNum" Text="商品编号" />：
                                    <asp:textbox id="txtProductNum" Width="80px" runat="server"/>
                                </td>
                                <td>
                                    <asp:button id="btnSearch" OnClick="btnSearch_Click" runat="server" text="查询" cssclass="adminsubmit_short" />
                                </td>
                            </tr> 
                          </table>
                    </asp:panel>
                    <div class="content">
                        <div class="youhuiproductlist searchproductslist">
                        <asp:HiddenField ID="hfCurrentAllData" runat="server"/>
                            <asp:DataList runat="server" ID="dlstSearchProducts" Width="96%" DataKeyField="ProductId" RepeatLayout="Table">
                                <ItemTemplate>
                        <table width="100%" border="0" cellspacing="0" class="conlisttd" ProductId="<%# Eval("ProductId") %>" ProductName="<%# Eval("ProductName") %>">
                             <tr>
                                <td width="14%" rowspan="3" class="img">
                                    <div class="borderImage">
                                      <a href="/Product/Detail/<%# Eval("ProductId") %>" target="_blank"><img id="ThumbnailUrl40" src="<%# @Maticsoft.Web.Components.FileHelper.GeThumbImage(Eval("ImageUrl").ToString(), "T150X150_") %>" width="80px" height="80px" /></a></div>
                                </td>
                                <td height="27" colspan="5" class="br_none"><span class="Name">
                                    <a href='/Product/Detail/<%# Eval("ProductId") %>' target="_blank"><%# Eval("ProductName") %></a>
                                </span></td>
                              </tr>
                           <tr>
                            <td  height="28" valign="top"><span class="colorC">最低价：<%# Eval("LowestSalePrice", "{0:n2}")%></span></td>
                
                            <td width="11%" align="right" valign="top">&nbsp;</td>
                            <td width="14%" align="left" valign="top" class="a_none">&nbsp;</td>
                            <td width="15%" valign="top"><a href="javascript:void(0);"><span style="color:blue; font-weight:bold;" class="AMsubmit_add">添加</span></a></td>
                          </tr>
                          <tr>
                             <td  valign="top">编号：<%# Eval("ProductCode") %></td>
                          </tr>
                       </table>
                    </itemtemplate>
                            </asp:DataList>
                        </div>
                        <div class="r">
                            <%--<div style="display:none;">
                                <asp:button runat="server" id="btnAddSearch" OnClick="btnAddSearch_Click" cssclass="adminsubmit" text="全部添加" />
                            </div>--%>
                            <div class="pagination">
                        <webdiyer:AspNetPager runat="server" ID="anpSearchProducts" CssClass="anpager" CurrentPageButtonClass="cpb"
                            OnPageChanged="AspNetPager_PageChanged" PageSize="15" FirstPageText="<%$Resources:Site,FirstPage %>"
                            LastPageText="<%$Resources:Site,EndPage %>" NextPageText="<%$Resources:Site,GVTextNext %>"
                            PrevPageText="<%$Resources:Site,GVTextPrevious %>" ShowPageIndexBox="Never" NumericButtonCount="5">
                        </webdiyer:AspNetPager>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <%--显示在右边的DIV--%>
            <div class="right">
                <h1>
                    已添加的商品</h1>
                <ul>
                    <li>
                        <asp:button runat="server" id="btnClear" cssclass="adminsubmit" text="清空列表" 
                            onclick="btnClear_Click" /> 
                    </li>
                </ul>
                <div class="content">
                    <div class="youhuiproductlist addedproductslist">
                        <asp:HiddenField ID="hfSelectedData" runat="server"/>
                        <asp:DataList runat="server" id="dlstAddedProducts"  width="96%" datakeyfield="ProductId" repeatlayout="Table">
                            <itemtemplate>
                     <table width="100%" border="0" cellspacing="0" class="conlisttd" >
                        <tr>
                            <td width="14%" rowspan="3" class="img">
                                  
                                <div class="borderImage"> <a href="/Product/Detail/<%# Eval("ProductId") %>" target="_blank"><img id="Img1" src="<%# @Maticsoft.Web.Components.FileHelper.GeThumbImage(Eval("ImageUrl").ToString(), "T150X150_") %>" width="80px" height="80px" /></a></div>
                            </td>
                            <td height="27" colspan="5" class="br_none"><span class="Name">
                                <a href='/Product/Detail/<%# Eval("ProductId") %>' target="_blank"><%# Eval("ProductName") %></a>
                            </span></td>
                          </tr>
                       <tr>
                        <td  height="28" valign="top"><span class="colorC">最低价：<%# Eval("LowestSalePrice", "{0:n2}")%></span></td>
                        <td width="11%" align="right" valign="top">&nbsp;</td>
                        <td width="14%" align="left" valign="top" class="a_none">&nbsp;</td>
                        <td width="15%" valign="top"><a href="javascript:void(0);"><span class="AMsubmit_del" style="color:blue; font-weight:bold;" ProductId="<%# Eval("ProductId") %>">删除</span></a></td>
                      </tr>
                        <tr>
                                  <td  valign="top">编号：<%# Eval("ProductCode") %></td>
                      </tr>
                      <%--<tr>
                          <td colspan="5">
                              <asp:Repeater ID="rptSKUItems" runat="server">
                                  <ItemTemplate>
                                      <div id="Div1" class="specdiv"><%# Eval("ValueStr")%></div>
                                  </ItemTemplate>
                              </asp:Repeater>
                          </td>
                      </tr>--%>
                     </table>
                </itemtemplate>
                        </asp:DataList>
                    </div>
                    <div class="r">
                        <div>
                            &nbsp;</div>
                        <div class="pagination">
                    <webdiyer:AspNetPager runat="server" ID="anpAddedProducts" CssClass="anpager" CurrentPageButtonClass="cpb"
                        OnPageChanged="AspNetPager_PageChanged" PageSize="15" FirstPageText="<%$Resources:Site,FirstPage %>"
                        LastPageText="<%$Resources:Site,EndPage %>" NextPageText="<%$Resources:Site,GVTextNext %>"
                        PrevPageText="<%$Resources:Site,GVTextPrevious %>" ShowPageIndexBox="Never" NumericButtonCount="5">
                    </webdiyer:AspNetPager>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="bntto">
            <%--<input type="button" id="btnOK" value="确定" class="adminsubmit_short" />--%>
        </div>
    </div>
</asp:content>

