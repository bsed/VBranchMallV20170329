﻿<%@ Page Title="图片重新生成" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
  CodeBehind="ImageReGen.aspx.cs" Inherits="Maticsoft.Web.Admin.SNS.Photos.ImageReGen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/jquery/maticsoft.jquery.min.js" type="text/javascript"></script>
  <link href="/Scripts/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <script src="/admin/js/jqueryui/JqueryDataPicker4CN.js" type="text/javascript"></script>
       <link href="/Scripts/jBox/Skins/Blue/jbox.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jBox/jquery.jBox-2.3.min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.cookie.js" type="text/javascript"></script>
    <script src="/admin/js/jquery/ImageReGen.js" type="text/javascript"></script>
     <script src="/admin/js/jquery/SelectCategoryAuto.helper.js" type="text/javascript"></script>
    <script src="/Scripts/jquery/maticsoft.img.min.js" type="text/javascript"></script>
    <style type="text/css">
        .style1
        {
            width: 32px;
        }
        .style2
        {
            width: 72px;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $("a.iframe").colorbox({ width: "auto", height: "auto", inline: true, href: "#divModal" });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--Title -->
    <asp:HiddenField ID="txtTaskCount" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="txtTaskCount_C" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="txtIsStatic" runat="server"></asp:HiddenField>
      <input id="hfTaskType" type="hidden" value="2"/>
    <div class="newslistabout">
      
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal3" runat="server" Text="缩略图生成" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal4" runat="server" Text="请选择分享图片的时间进行缩略图生成。" />
                    </td>
                </tr>
            </table>
        </div>

        <table border="0" cellspacing="1" style="width: 100%; height: 100%;" cellpadding="3"
            class="borderkuang" id="tabTask" >
            <tr>
                <td height="18px;">
                </td>
                <td colspan="2">
                    <h3>
                        缩略图生成</h3>
                </td>
            </tr>
            <tr>
                <td style="width: 8px;">
                </td>
                <td  style="width: 80px"  class="td_class">
                    添加时间：
                </td>
                <td style="text-align: left">
                   <asp:TextBox ID="txtFrom" runat="server"  name="from" style="width: 90px" ></asp:TextBox>
                    --
                     <asp:TextBox ID="txtTo" runat="server"  name="to" style="width: 90px" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 8px;">
                </td>
                <td>
                </td>
                <td style="text-align: left">
                    <input id="btnToStatic" type="button" value="缩略图生成" class="adminsubmit" />
                </td>
            </tr>
            <tr style="height: 60px">
                <td colspan="3" id="probar" style="display: none;">
                    <div id="progressbar" style="width: 560px;">
                    </div>
                    <div style="width: 560px; text-align: center">
                        <span id="txtCount"></span>/共<span id="txtTotalCount"></span></div>
                </td>
            </tr>
        </table>
        <br />
        <table border="0" cellspacing="1" style="width: 100%; height: 100%; display: none;"
            cellpadding="3" class="borderkuang" id="txtRemain">
            <tr>
                <td>
                缩略图生成任务
                </td>
                <td colspan="2">
                    <div>
                        上次任务断点时间：<asp:Literal ID="txtTaskDate" runat="server"></asp:Literal>
                        &nbsp;&nbsp;任务ID：<asp:Literal ID="txtTaskId" runat="server"></asp:Literal>
                        &nbsp;&nbsp;剩余条数：<asp:Literal ID="txtTaskReCount" runat="server"></asp:Literal></div>
                    <div style="padding-left: 160px">
                        <input id="btnContinue" type="button" value="继续任务" class="adminsubmit" />
                        &nbsp;&nbsp;
                        <input id="btnRemove" type="button" value="清除任务" class="adminsubmit" /></div>
                </td>
            </tr>
        </table>
    </div>
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
