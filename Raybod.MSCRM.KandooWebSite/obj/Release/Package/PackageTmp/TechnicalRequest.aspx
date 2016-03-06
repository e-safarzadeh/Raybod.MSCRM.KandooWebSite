<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="TechnicalRequest.aspx.cs" Inherits="Raybod.MSCRM.KandooWebSite.TechnicalRequest" %>

<%@ Register Assembly="WebControlCaptcha" Namespace="WebControlCaptcha" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        td input {
            font-family: Tahoma;
            font-size: 10px;
            height: 12px;
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

        .auto-style4 {
            height: 70px;
        }

        .auto-style5 {
            width: 158px;
            height: 28px;
        }

        .auto-style7 {
            width: 158px;
        }

        .auto-style11 {
            height: 70px;
            width: 158px;
        }

        .auto-style12 {
            width: 685px;
        }
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
        ShowSummary="False" Width="16px" Style="font-size: x-small; font-family: Tahoma; font-weight: 700"
        Height="16px" />
    <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0" dir="rtl">
        <tr valign="top">
            <td width="10" style="height: 25px">
                <img width="14" height="38" src="images/page_03_1.jpg" />
            </td>
            <td align="right" style="background-image: url('images/page_02_1.jpg'); width: 100%; background-repeat: repeat-x; height: 25px; font-family: Tahoma; font-weight: bold; font-size: 0.8em; color: #FFFFFF;"
                valign="middle">
                <table style="width: 100%; height: 30px;" dir="rtl">
                    <tr>
                        <td class="auto-style7">
                            &nbsp;<asp:LinkButton ID="linkRequest" runat="server" Style="font-size: x-small; color: #FFFFFF; text-decoration: none"
                                CausesValidation="False" OnClick="linkRequest_Click">بازگشت</asp:LinkButton>
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
            <td width="14" style="height: 25px">
                <img width="14" height="38" src="images/page_01_1.jpg" />
            </td>
        </tr>
        <tr>
            <td style="background-image: url(images/page_05.jpg); width: 14px; background-repeat: repeat-y;" />
            <td style="width: 100%">
                <table style="width: 100%; margin-top: 10px;" cellpadding="0" dir="rtl">
                    <tr>
                        <td align="left" class="auto-style5">
                            <span style="font-family: Tahoma; font-size: x-small; font-weight: bold">کلاس مربوطه :</span><span
                                class="style13" style="font-family: Tahoma; font-size: x-small; font-weight: bold">
                            </span>
                            &nbsp;</td>
                        <td align="right" dir="rtl">
                            <asp:TextBox ID="txtClass" runat="server" Font-Names="Tahoma" Style="text-align: left; font-size: x-small;"
                                Width="296px" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table style="width: 100%; margin-top: 0px;" cellpadding="0" dir="rtl">
                    <tr>
                        <td align="left" valign="top" class="auto-style11">
                            <span class="style13" style="font-family: Tahoma; font-size: x-small; font-weight: bold">
                                <span style="font-family: Tahoma; font-size: x-small; font-weight: bold">شرح درخواست :</span>&nbsp;
                            </span><span style="font-family: Tahoma; font-size: x-small; font-weight: bold"><span
                                class="style13" style="font-family: Tahoma; font-size: x-small; font-weight: bold"></span><span style="color: #FF0000">*</span></span>
                        </td>
                        <td align="right" dir="rtl" class="auto-style4">
                            <asp:TextBox ID="txtDescription" runat="server" Height="60px" TextMode="MultiLine" Width="496px"
                                Font-Names="Tahoma" Style="font-size: x-small"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtDescription"
                                Display="Dynamic" ErrorMessage="لطفا شرح درخواست را وارد كنيد." Font-Bold="False"
                                Font-Names="Tahoma" ForeColor="#FF3300" CssClass="style14" Style="font-size: x-small"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="auto-style7">&nbsp;
                        </td>
                        <td align="right" dir="rtl">
                            <asp:Button ID="btnSave" runat="server" Font-Names="Tahoma"
                                Text="ثبت" Width="55px" CssClass="style14" Font-Size="0.9em" Height="24px" Style="font-size: x-small; font-weight: 700; text-align: center" OnClick="btnSave_Click" />
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
