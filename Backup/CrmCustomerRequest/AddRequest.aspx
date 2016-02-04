<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AddRequest.aspx.cs" Inherits="CrmCustomerRequest.AddRequest" %>

<%@ Register Assembly="WebControlCaptcha" Namespace="WebControlCaptcha" TagPrefix="cc1" %>
<%@ Register Assembly="PersiaGuide" Namespace="PersiaGuide" TagPrefix="cc11" %>
<%@ Register Assembly="pcal" Namespace="pcal" TagPrefix="cc2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        td input
        {
            font-family: Tahoma;
            font-size: 10px;
            height: 12px;
        }
        .style1
        {
            width: 106px;
            height: 28px;
        }
        .style2
        {
            height: 28px;
        }
        .style3
        {
            width: 106px;
            height: 15px;
        }
        .style4
        {
            height: 15px;
        }
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
        ShowSummary="False" Width="16px" Style="font-size: x-small; font-family: Tahoma;
        font-weight: 700" Height="16px" />
    <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0" dir="rtl">
        <tr valign="top">
            <td width="10" style="height: 25px">
                <img width="14" height="38" src="images/page_03.jpg" />
            </td>
            <td align="right" style="background-image: url('images/page_02.jpg'); width: 100%;
                background-repeat: repeat-x; height: 25px; font-family: Tahoma; font-weight: bold;
                font-size: 0.8em; color: #FFFFFF;" valign="middle">
                <table style="width: 100%; height: 30px;" dir="rtl">
                    <tr>
                        <td class="style15" style="width: 134px">
                            <asp:Image ID="Image3" runat="server" Height="16px" ImageAlign="Top" ImageUrl="~/images/list.jpg" />
                            &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" Style="font-size: x-small;
                                color: #FFFFFF; text-decoration: none" CausesValidation="False" OnClick="LinkRequestList_Click">ليست درخواست ها</asp:LinkButton>
                        </td>
                        <td style="width: 712px; text-align: left">
                            &nbsp;&nbsp;<asp:Label ID="LabelPersonnel" runat="server" Text="شخص" Style="font-size: x-small"></asp:Label>
                            &nbsp;&nbsp;
                        </td>
                        <td style="text-align: right" dir="rtl">
                            <asp:Image ID="Image4" runat="server" Height="16px" ImageAlign="Top" ImageUrl="~/images/unlock.jpg" />
                            &nbsp;<asp:LinkButton ID="LinkButton1" runat="server" Style="font-size: x-small;
                                color: #FFFFFF; text-decoration: none" OnClick="LinkSignOut_Click" CausesValidation="False">خروج</asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="14" style="height: 25px">
                <img width="14" height="38" src="images/page_01.jpg" />
            </td>
        </tr>
        <tr>
            <td style="background-image: url(images/page_05.jpg); width: 14px; background-repeat: repeat-y;" />
            <td style="width: 100%">
                <asp:UpdatePanel ID="AjaxUpdatePanel" runat="server">
                    <ContentTemplate>
                        <table style="width: 100%; margin-top: 10px;" cellpadding="0" dir="rtl">
                            <tr>
                                <td align="left" width="120">
                                    <span style="font-family: Tahoma; font-size: x-small; font-weight: bold; margin-bottom: 5px; margin-left: 5px;">نوع :</span><span
                                        class="style13" style="font-family: Tahoma; font-size: x-small; font-weight: bold">
                                        <span style="color: #FF0000">*</span></span>
                                </td>
                                <td align="right" dir="rtl">
                                    <asp:DropDownList ID="cmbType" runat="server" Height="20px" Width="302px" Font-Names="Tahoma"
                                        CssClass="style14" Style="font-size: x-small">
                                        <asp:ListItem Value="1">اعلام خطا</asp:ListItem>
                                        <asp:ListItem Value="2">درخواست</asp:ListItem>
                                        <asp:ListItem Value="3">سؤال</asp:ListItem>
                                        <asp:ListItem Value="4">اعلام بروز مشكل</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="cmbType"
                                        Display="Dynamic" ErrorMessage="لطفا نوع درخواست را وارد كنيد." Font-Bold="False"
                                        Font-Names="Tahoma" ForeColor="#FF3300" Style="font-size: x-small"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" width="120">
                                    <span style="font-family: Tahoma; font-size: x-small; font-weight: bold">از طرف :</span><span
                                        class="style13" style="font-family: Tahoma; font-size: x-small; font-weight: bold">
                                    </span>
                                </td>
                                <td align="right" dir="rtl">
                                    <asp:DropDownList ID="cmbFrom" runat="server" Height="20px" Width="302px" Font-Names="Tahoma"
                                        CssClass="style14" Style="font-size: x-small">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" width="120">
                                    <span style="font-family: Tahoma; font-size: x-small; font-weight: bold" dir="rtl">عنوان1
                                        :</span><span class="style13" style="font-family: Tahoma; font-size: x-small; font-weight: bold">
                                            <span style="color: #FF0000">*</span></span>
                                </td>
                                <td align="right" dir="rtl">
                                    <asp:DropDownList ID="cmbCT1" runat="server" Height="20px" Width="302px" Font-Names="Tahoma"
                                        CssClass="style14" Style="font-size: x-small" OnSelectedIndexChanged="cmbCT1_SelectedIndexChanged"
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" dir="rtl" width="120">
                                    <span style="font-family: Tahoma; font-size: x-small; font-weight: bold" dir="rtl">عنوان2
                                        :</span><span style="font-family: Tahoma; font-size: x-small; font-weight: bold">
                                    </span>
                                </td>
                                <td align="right" dir="rtl">
                                    <asp:DropDownList ID="cmbCT2" runat="server" Height="20px" Width="302px" Font-Names="Tahoma"
                                        Style="font-size: x-small" OnSelectedIndexChanged="cmbCT2_SelectedIndexChanged"
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" width="120">
                                    <span style="font-family: Tahoma; font-size: x-small; font-weight: bold">عنوان3 :</span><span
                                        class="style13" style="font-family: Tahoma; font-size: x-small; font-weight: bold">
                                    </span>
                                </td>
                                <td align="right" dir="rtl">
                                    <asp:DropDownList ID="cmbCT3" runat="server" Height="20px" Width="302px" Font-Names="Tahoma"
                                        CssClass="style14" Style="font-size: x-small" OnSelectedIndexChanged="cmbCT3_SelectedIndexChanged"
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" width="120">
                                    <span style="font-family: Tahoma; font-size: x-small; font-weight: bold">عنوان4 :</span><span
                                        class="style13" style="font-family: Tahoma; font-size: x-small; font-weight: bold">
                                    </span>
                                </td>
                                <td align="right" dir="rtl">
                                    <asp:DropDownList ID="cmbCT4" runat="server" Height="20px" Width="302px" Font-Names="Tahoma"
                                        CssClass="style14" Style="font-size: x-small">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" width="120">
                                    <span style="font-family: Tahoma; font-size: x-small; font-weight: bold">اولویت :</span>
                                </td>
                                <td align="right" dir="rtl">
                                    <asp:DropDownList ID="cmbPriority" runat="server" Height="20px" Width="302px" Font-Names="Tahoma"
                                        CssClass="style14" Style="font-size: x-small" AutoPostBack="True">
                                        <asp:ListItem Value="1">بالا</asp:ListItem>
                                        <asp:ListItem Value="2">نرمال</asp:ListItem>
                                        <asp:ListItem Value="3">پایین</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cmbCT1" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="cmbCT2" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="cmbCT3" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0">
                    <ProgressTemplate>
                        <div id="IMGDIV" align="center" valign="middle" runat="server" style="border: medium solid #80242C;
                            position: absolute; left: 50%; top: 50%; visibility: visible; vertical-align: middle;
                            background-color: #c8d1d4;">
                            <asp:Image ID="AjaxLoader" runat="server" ImageUrl="~/images/ajax_load_red.gif" Height="78px"
                                Width="82px" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <table style="width: 100%; margin-top: 0px;" cellpadding="0" dir="rtl">
                    <tr>
                        <td align="left" width="120">
                            <span style="font-family: Tahoma; font-size: x-small; font-weight: bold">مهلت پيشنهادي
                                انجام :</span>
                        </td>
                        <td align="right" dir="rtl">
                            <cc2:FarsiDatePicker ID="FarsiDatePicker5" runat="server" Font-Names="Tahoma" Font-Size="8px" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" width="120">
                            <span class="style13" style="font-family: Tahoma; font-size: x-small; font-weight: bold">
                                <span style="font-family: Tahoma; font-size: x-small; font-weight: bold">شرح :</span>&nbsp;
                            </span><span style="font-family: Tahoma; font-size: x-small; font-weight: bold"><span
                                class="style13" style="font-family: Tahoma; font-size: x-small; font-weight: bold">
                                <span style="color: #FF0000">*</span></span></span>
                        </td>
                        <td align="right" dir="rtl">
                            <asp:TextBox ID="txtTitle" runat="server" Height="60px" TextMode="MultiLine" Width="496px"
                                Font-Names="Tahoma" Style="font-size: x-small"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtTitle"
                                Display="Dynamic" ErrorMessage="لطفا شرح درخواست را وارد كنيد." Font-Bold="False"
                                Font-Names="Tahoma" ForeColor="#FF3300" CssClass="style14" Style="font-size: x-small"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="120">
                            <span style="font-family: Tahoma; font-size: x-small; font-weight: bold">فايل1 :</span>
                        </td>
                        <td align="right" dir="rtl">
                            <asp:FileUpload ID="FileUpload1" runat="server" Height="21px" Width="496px" Font-Names="Tahoma"
                                CssClass="style14" Style="font-size: x-small" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="120">
                            <span style="font-family: Tahoma; font-size: x-small; font-weight: bold">فايل2 :</span>
                        </td>
                        <td align="right" dir="rtl">
                            <asp:FileUpload ID="FileUpload2" runat="server" Height="21px" Width="496px" Font-Names="Tahoma"
                                CssClass="style14" Style="font-size: x-small" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="120">
                            <span style="font-family: Tahoma; font-size: x-small; font-weight: bold">فايل3 :</span>
                        </td>
                        <td align="right" dir="rtl">
                            <asp:FileUpload ID="FileUpload3" runat="server" Height="21px" Width="496px" Font-Names="Tahoma"
                                CssClass="style14" Style="font-size: x-small" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="120">
                            &nbsp;
                        </td>
                        <td align="right" dir="rtl">
                            <ul style="padding: 0px; margin: 0px 15px 0px 0px; font-family: tahoma; font-size: 0.9em;
                                font-weight: bold; color: #000000;" type="disc">
                                <li style="font-size: x-small">اندازه فايل انتخاب شده بايد كمتر از 5M باشد. </li>
                            </ul>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="120">
                            &nbsp;
                        </td>
                        <td align="right" dir="rtl">
                            <asp:Button ID="btnSave" runat="server" Font-Names="Tahoma" OnClick="btnSave_Click"
                                Text="ثبت" Width="55px" CssClass="style14" Font-Size="0.9em" Height="24px" Style="font-size: x-small;
                                font-weight: 700; text-align: center" />
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
            <td style="background-image: url(images/page_07.jpg); width: 100%; background-repeat: repeat-x;
                height: 38px;" />
            <td style="height: 38px">
                <img width="14" height="38" src="images/page_06.jpg" complete="complete" />
            </td>
        </tr>
    </table>
</asp:Content>
