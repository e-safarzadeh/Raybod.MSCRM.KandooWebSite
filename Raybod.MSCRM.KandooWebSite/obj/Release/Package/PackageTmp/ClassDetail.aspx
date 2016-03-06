<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ClassDetail.aspx.cs" Inherits="Raybod.MSCRM.KandooWebSite.ClassDetail" %>

<%@ Register Assembly="WebControlCaptcha" Namespace="WebControlCaptcha" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        td input {
            font-family: Tahoma;
            font-size: 10px;
        }

        .style1 {
            width: 106px;
            height: 28px;
        }

        .style2 {
            height: 28px;
        }

        .style3 {
            width: 106px;
            height: 15px;
        }

        .style4 {
            height: 15px;
        }

        .auto-style7 {
            width: 158px;
        }

        .auto-style12 {
            width: 685px;
        }

        .auto-style13 {
            width: 202px;
        }

        .auto-style14 {
            width: 202px;
            height: 21px;
        }

        .auto-style15 {
            height: 21px;
        }

        .auto-style16 {
            text-decoration: underline;
            font-size: x-small;
        }

        .auto-style19 {
            height: 25px;
        }

        .auto-style20 {
            width: 100%;
            height: 25px;
        }

        .auto-style21 {
            height: 24px;
            width: 95px;
        }

        </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
        ShowSummary="False" Width="16px" Style="font-size: x-small; font-family: Tahoma; font-weight: 700"
        Height="16px" />
    <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0" dir="rtl">
        <tr valign="top">
            <td width="10" class="auto-style19">
                <img width="14" height="38" src="images/page_03_1.jpg" />
            </td>
            <td align="right" style="background-image: url('images/page_02_1.jpg'); background-repeat: repeat-x; font-family: Tahoma; font-weight: bold; font-size: 0.8em; color: #FFFFFF;"
                valign="middle" class="auto-style20">
                <table style="width: 100%; height: 30px;" dir="rtl">
                    <tr>
                        <td class="auto-style7">
                            <asp:Image ID="Image3" runat="server" Height="16px" ImageAlign="Top" ImageUrl="~/images/list.jpg" />
                            &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" Style="font-size: x-small; color: #FFFFFF; text-decoration: none"
                                CausesValidation="False" OnClick="LinkClassList_Click">ليست کلاس ها</asp:LinkButton>
                        </td>
                        <td style="text-align: left" class="auto-style12">&nbsp;&nbsp;<asp:Label ID="LabelPersonnel" runat="server" Text="شخص" Style="font-size: x-small"></asp:Label>
                            &nbsp;&nbsp;
                        </td>
                        <td style="text-align: right" dir="rtl">
                            <asp:Image ID="Image4" runat="server" Height="16px" ImageAlign="Top" ImageUrl="~/images/unlock.jpg" />
                            &nbsp;<asp:LinkButton ID="LinkButton1" runat="server" Style="font-size: x-small; color: #FFFFFF; text-decoration: none"
                                OnClick="LinkSignOut_Click" CausesValidation="False">خروج</asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="14" class="auto-style19">
                <img width="14" height="38" src="images/page_01_1.jpg" />
            </td>
        </tr>
        <tr>
            <td style="background-image: url(images/page_05.jpg); width: 14px; background-repeat: repeat-y;" />
            <td style="width: 100%">
                <table style="width: 100%; font-family: Tahoma; font-size: x-small;" cellpadding="0"
                    cellspacing="5" dir="rtl" border="0">
                    <tr>
                        <td style="height: 24px; font-size: x-small; text-align: right; width: 92px;" valign="baseline"><span style="font-family: Tahoma; font-weight: bold; text-decoration: underline;" class="auto-style16">اطلاعات کلاس:</span><span style="text-decoration: underline"> </span></td>
                        <td style="height: 24px; font-size: x-small; text-align: right; width: 186px;" valign="baseline"></td>
                        <td style="height: 24px; font-size: x-small; text-align: right; width: 112px;" valign="baseline"></td>
                        <td style="height: 24px; font-size: x-small; text-align: right; width: 186px;" valign="baseline"></td>
                        <td style="font-size: x-small; text-align: right; " valign="baseline" class="auto-style21"></td>
                        <td style="height: 24px; font-size: x-small; text-align: right; width: 186px;" valign="baseline"></td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 92px">
                            <span style="font-family: Tahoma; font-size: x-small; font-weight: bold">نام کلاس : </span>
                        </td>
                        <td style="height: 24px; font-size: x-small; text-align: right; width: 186px;" valign="baseline">
                            <asp:TextBox ID="txtClassName" runat="server" Width="207px" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td style="height: 24px; font-size: x-small; text-align: right; width: 112px;" valign="baseline">
                            <span style="font-family: Tahoma; font-size: x-small; font-weight: bold">دوره مربوطه:</span>
                        </td>
                        <td style="height: 24px; font-size: x-small; text-align: right; width: 186px;" valign="baseline">
                            <asp:TextBox ID="txtCourse" runat="server" Width="216px" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td style="font-size: x-small; text-align: right; " valign="baseline" class="auto-style21">
                            <span style="font-family: Tahoma; font-size: x-small; font-weight: bold">محل برگزاری :</span>
                        </td>
                        <td style="height: 24px; font-size: x-small; text-align: right; width: 186px;" valign="baseline">
                            <asp:TextBox ID="txtLocation" runat="server" Width="208px" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 92px">
                            <span style="font-family: Tahoma; font-size: x-small; font-weight: bold">لابراتوار : </span>
                        </td>
                        <td style="height: 24px; font-size: x-small; text-align: right; width: 186px;" valign="baseline">
                            <asp:TextBox ID="txtLab" runat="server" Width="207px" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td style="height: 24px; font-size: x-small; text-align: right; width: 112px;" valign="baseline">
                            <span style="font-family: Tahoma; font-size: x-small; font-weight: bold">نوع کلاس:</span>
                        </td>
                        <td style="height: 24px; font-size: x-small; text-align: right; width: 186px;" valign="baseline">
                            <asp:TextBox ID="txtClassType" runat="server" Width="216px" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td style="font-size: x-small; text-align: right; " valign="baseline" class="auto-style21">
                            <span style="font-family: Tahoma; font-size: x-small; font-weight: bold">وضعیت کلاس :</span>
                        </td>
                        <td style="height: 24px; font-size: x-small; text-align: right; width: 186px;" valign="baseline">
                            <asp:TextBox ID="txtClassStatus" runat="server" Width="208px" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 92px">
                            <span style="font-family: Tahoma; font-size: x-small; font-weight: bold">تاریخ شروع : </span>
                        </td>
                        <td style="height: 24px; font-size: x-small; text-align: right; width: 186px;" valign="baseline">
                            <asp:TextBox ID="txtStartdate" runat="server" Width="207px" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td style="height: 24px; font-size: x-small; text-align: right; width: 112px;" valign="baseline">
                            <span style="font-family: Tahoma; font-size: x-small; font-weight: bold">ساعت شروع:</span>
                        </td>
                        <td style="height: 24px; font-size: x-small; text-align: right; width: 186px;" valign="baseline">
                            <asp:TextBox ID="txtStarttime" runat="server" Width="216px" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td style="font-size: x-small; text-align: right; " valign="baseline" class="auto-style21">
                            <span style="font-family: Tahoma; font-size: x-small; font-weight: bold">ساعت پایان :</span>
                        </td>
                        <td style="height: 24px; font-size: x-small; text-align: right; width: 186px;" valign="baseline">
                            <asp:TextBox ID="txtEndtime" runat="server" Width="208px" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 92px; text-align: right;">
                            &nbsp;
                            </td>
                    </tr>
                </table>
                <table style="width: 100%; margin-top: 0px;" cellpadding="0" dir="rtl">

                    <tr>
                        <td align="right" class="auto-style14">
                            <span style="font-family: Tahoma; font-size: x-small; font-weight: bold" __designer:mapid="3ac">نوع درخواست خود را مشخص نمایید. :</span>&nbsp;
                        </td>
                        <td align="right" dir="rtl" class="auto-style15"></td>
                    </tr>
                    <tr>
                        <td align="right" class="auto-style13">&nbsp;</td>
                        <td align="right" dir="rtl">&nbsp;</td>
                    </tr>
                    <tr>
                        <td align="left" class="auto-style13">&nbsp;
                        </td>
                        <td align="right" dir="rtl">
                            <asp:Button ID="btnteachrqst" runat="server" Font-Names="Tahoma"
                                Text="درخواست حق التدریس" Width="152px" CssClass="style14" Font-Size="0.9em" Height="24px" Style="font-size: x-small; font-weight: 700; text-align: center" OnClick="btnteachrqst_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnAttendancerqst" runat="server" Font-Names="Tahoma"
                                Text="درخواست حضور و غیاب" Width="152px" CssClass="style14" Font-Size="0.9em" Height="24px" Style="font-size: x-small; font-weight: 700; text-align: center" OnClick="btnAttendancerqst_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnTechrqst" runat="server" Font-Names="Tahoma"
                                Text="درخواست فنی" Width="152px" CssClass="style14" Font-Size="0.9em" Height="24px" Style="font-size: x-small; font-weight: 700; text-align: center" OnClick="btnTechrqst_Click" />
                        </td>
                    </tr>
                </table>
            </td>
            <td style="background-image: url(images/page_04.jpg); width: 14px; background-repeat: repeat-y;" />
        </tr>
        <tr>
            <td style="height: 20px">
                <img width="14" height="38" src="images/page_08.jpg" complete="complete" />
            </td>
            <td style="background-image: url(images/page_07.jpg); width: 100%; background-repeat: repeat-x; height: 38px;" />
            <td style="height: 38px">
                <img width="14" height="38" src="images/page_06.jpg" complete="complete" />
            </td>
        </tr>
    </table>
</asp:Content>
