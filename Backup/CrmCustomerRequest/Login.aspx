<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="CrmCustomerRequest.Login" %>

<%@ Register TagPrefix="cc1" Namespace="WebControlCaptcha" Assembly="WebControlCaptcha" %>
<script runat="server">

</script>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="Panel1" runat="server" dir="rtl" DefaultButton="btnLogin" Height="245px">
        <table style="width: 100%; height: 242px;" border="0" cellpadding="0" cellspacing="0">
            <tr valign="top">
                <td width="10">
                    <img width="14" height="38" src="images/page_03.jpg" complete="complete" style="padding: 0px;
                        margin: 0px" />
                </td>
                <td align="right" class="page_title" style="padding: 0px; margin: 0px; background-image: url('images/page_02.jpg');
                    width: 100%; background-repeat: repeat-x; font-family: Tahoma; font-weight: bold;
                    font-size: x-small; color: #FFFFFF;" valign="middle">
                    <table style="width: 100%; height: 30px;" dir="rtl">
                        <tr>
                            <td class="style15" style="width: 134px">
                                <asp:Image ID="Image1" runat="server" Height="16px" ImageAlign="Top" ImageUrl="~/images/list.jpg" />
                                &nbsp;<asp:LinkButton ID="LinkRegistration" runat="server" Style="font-size: x-small;
                                    color: #FFFFFF; text-decoration: none" CausesValidation="False" OnClick="LinkRegistration_Click">ثبت نام در سایت</asp:LinkButton>
                            </td>
                            <td style="text-align: left" dir="rtl">
                                &nbsp;&nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="14" style="padding: 0px; margin: 0px;" valign="middle">
                    <img width="14" height="38" src="images/page_01.jpg" complete="complete" style="padding: 0px;
                        margin: 0px" />
                </td>
            </tr>
            <tr>
                <td style="background-image: url('images/page_05.jpg'); width: 14px; background-repeat: repeat-y;
                    height: 162px;">
                    <td style="width: 100%; font-family: Tahoma; padding-top: 4px; height: 162px;" align="right"
                        valign="top">
                        <table cellspacing="5" style="width: 100%; font-family: Tahoma; height: 0px;">
                            <tr align="left">
                                <td style="font-size: x-small; font-weight: bold; text-align: left; width: 132px">
                                    <span style="font-size: x-small; font-weight: bold">&nbsp;آدرس پست الکترونیک :&nbsp;</span><span
                                        style="font-size: x-small; font-weight: bold; color: #FF0000">*</span><span style="font-size: x-small;
                                            font-weight: bold"> </span>
                                </td>
                                <td style="text-align: right;">
                                    <asp:TextBox ID="txtUsername" runat="server" Width="300px" CssClass="style14" Style="font-size: x-small;
                                        font-family: Tahoma;"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: x-small; font-weight: bold; text-align: left; width: 132px;">
                                    رمز عبور :<span style="font-size: x-small; font-weight: bold; color: #FF0000"> *</span>
                                </td>
                                <td style="text-align: right;">
                                    <asp:TextBox ID="txtPassword" runat="server" CssClass="style14" Style="font-size: x-small;
                                        font-family: Tahoma;" Width="300px" TextMode="Password"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: x-small; font-weight: bold; text-align: left; width: 132px;
                                    height: 18px;">
                                    &nbsp;
                                </td>
                                <td style="text-align: right; height: 18px;">
                                    <asp:Button ID="btnLogin" runat="server" CssClass="style14" Font-Names="Tahoma" Height="24px"
                                        OnClick="btnLogin_Click" Style="font-size: x-small; font-weight: 700;" Text="ورود"
                                        Width="55px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="background-image: url('images/page_04.jpg'); width: 14px; background-repeat: repeat-y;
                        height: 162px;">
            </tr>
            <tr>
                <td>
                    <img width="14" height="38" src="images/page_08.jpg" complete="complete" />
                </td>
                <td style="background-image: url(images/page_07.jpg); width: 100%; background-repeat: repeat-x;
                    height: 38px;" />
                &nbsp;<td>
                    <img width="14" height="38" src="images/page_06.jpg" complete="complete" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
