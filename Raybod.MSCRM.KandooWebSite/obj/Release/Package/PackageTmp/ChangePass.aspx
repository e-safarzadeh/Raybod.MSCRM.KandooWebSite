<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ChangePass.aspx.cs" Inherits="Raybod.MSCRM.KandooWebSite.ChangePass" %>


<%@ Register TagPrefix="cc1" Namespace="WebControlCaptcha" Assembly="WebControlCaptcha" %>
<script runat="server">

</script>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="Panel1" runat="server" dir="rtl" DefaultButton="btnLogin" Height="342px">
        <table style="width: 100%; height: 242px;" border="0" cellpadding="0" cellspacing="0">
            <tr valign="top">
                <td width="10">
                    <img width="14" height="38" src="images/page_03_1.jpg" complete="complete" style="padding: 0px; margin: 0px" />
                </td>
                <td align="right" class="page_title" style="padding: 0px; margin: 0px; background-image: url('images/page_02_1.jpg'); width: 100%; background-repeat: repeat-x; font-family: Tahoma; font-weight: bold; font-size: x-small; color: #ffffff;"
                    valign="middle">تغییر&nbsp; رمز عبور</td>
                <td width="14" style="padding: 0px; margin: 0px;" valign="middle">
                    <img width="14" height="38" src="images/page_01_1.jpg" complete="complete" style="padding: 0px; margin: 0px" />
                </td>
            </tr>
            <tr>
                <td style="background-image: url('images/page_05.jpg'); width: 14px; background-repeat: repeat-y; height: 204px;">
                    <td style="width: 100%; font-family: Tahoma; padding-top: 4px; height: 204px;" align="right">
                        <table cellspacing="5" style="width: 100%; font-family: Tahoma; height: 0px;">
                            <tr align="left">
                                <td style="font-size: x-small; font-weight: bold; text-align: left; width: 99px">
                                    <span style="font-size: x-small; font-weight: bold">&nbsp;رمز عبور&nbsp;قبلی:&nbsp;</span><span
                                        style="font-size: x-small; font-weight: bold; color: #FF0000">*</span><span style="font-size: x-small; font-weight: bold"> </span>
                                </td>
                                <td style="text-align: right;" colspan="2">
                                    <asp:TextBox ID="txtPassword" runat="server" Width="300px" CssClass="style14"
                                        Style="font-size: x-small; font-family: Tahoma;" TextMode="Password"></asp:TextBox>
                                </td>
                                <td style="text-align: right; height: 1px;" dir="rtl" rowspan="1"
                                    valign="middle" width="51%"></td>
                            </tr>
                            <tr align="left">
                                <td style="font-size: x-small; font-weight: bold; text-align: left; width: 99px">
                                    <span style="font-size: x-small; font-weight: bold">&nbsp;رمز عبور&nbsp;جدید:&nbsp;</span><span
                                        style="font-size: x-small; font-weight: bold; color: #FF0000">*</span><span style="font-size: x-small; font-weight: bold"> </span>
                                </td>
                                <td style="text-align: right;" colspan="2">
                                    <asp:TextBox ID="txtPassNew" runat="server" Width="300px" CssClass="style14"
                                        Style="font-size: x-small; font-family: Tahoma;" TextMode="Password"></asp:TextBox>
                                </td>
                                <td style="text-align: right; height: 1px;" dir="rtl" rowspan="1"
                                    valign="middle" width="51%"></td>
                            </tr>
                            <tr align="left">
                                <td style="font-size: x-small; font-weight: bold; text-align: left; width: 99px">
                                    <span style="font-size: x-small; font-weight: bold">تأیید&nbsp;رمز عبور :&nbsp;</span><span
                                        style="font-size: x-small; font-weight: bold; color: #FF0000">*</span><span style="font-size: x-small; font-weight: bold"> </span>
                                </td>
                                <td style="text-align: right;" colspan="2">
                                    <asp:TextBox ID="txtPassConfirm" runat="server" Width="300px" CssClass="style14"
                                        Style="font-size: x-small; font-family: Tahoma;" TextMode="Password"></asp:TextBox>
                                </td>
                                <td style="text-align: right; height: 1px;" dir="rtl" rowspan="1"
                                    valign="middle" width="51%"></td>
                            </tr>
                            <tr align="right" dir="rtl">
                                <td style="font-size: x-small; font-weight: bold; text-align: left; width: 99px; height: 76px;"
                                    dir="rtl" valign="top">
                                    <span style="font-size: x-small; font-weight: bold">&nbsp;كد امنيتي : <span style="color: #FF0000">*</span> </span>
                                </td>
                                <td style="padding: 0px; margin: 0px; height: 76px; width: 187px;" align="right"
                                    dir="rtl" valign="top">
                                    <cc1:CaptchaControl ID="CaptchaControl1" runat="server" CaptchaBackgroundNoise="Medium"
                                        CaptchaFontWarping="Medium" CssClass="style14" Font-Names="Tahoma" Font-Size="X-Small"
                                        ForeColor="Black" Height="98px" LayoutStyle="Vertical" Style="text-align: right; font-family: Tahoma; font-size: x-small;"
                                        Text="لطفا كد امنيتي را وارد كنيد:"
                                        Width="204px" CaptchaMaxTimeout="120" CaptchaMinTimeout="10" ViewStateMode="Disabled" />
                                </td>
                                <td align="right" dir="rtl" style="padding: 0px; margin: 0px; height: 76px; width: 127px;"
                                    valign="middle">
                                    <asp:ImageButton ID="ImageButton3" runat="server" Height="20px" ImageUrl="images/1315465692_refresh.jpg"
                                        ToolTip="ايجاد كد امنيتي جديد" Width="20px" />
                                </td>
                                <td style="text-align: right; height: 76px;" dir="rtl" rowspan="1" valign="top"
                                    width="51%"></td>
                            </tr>
                            <tr>
                                <td style="font-size: x-small; font-weight: bold; text-align: left; width: 99px; height: 33px;">&nbsp;
                                </td>
                                <td style="padding: 0px; margin: 0px; text-align: right; height: 33px; font-size: x-small;"
                                    colspan="2">
                                    <ul style="padding: 0px; margin: 0px 25px 0px 0px; font-family: tahoma; font-size: x-small; font-weight: bold; color: #000000;"
                                        type="disc">
                                        <li>كد امنيتي نسبت به حروف كوچك و بزرگ حساس نمي باشد. </li>
                                    </ul>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: x-small; font-weight: bold; text-align: left; width: 99px; height: 18px;">&nbsp;
                                </td>
                                <td style="text-align: right; height: 18px;" colspan="2">
                                    <asp:Button ID="btnLogin" runat="server" CssClass="style14" Font-Names="Tahoma" Height="24px"
                                        OnClick="btnLogin_Click" Style="font-size: x-small; font-weight: 700;" Text="تأیید"
                                        Width="55px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="background-image: url('images/page_04.jpg'); width: 14px; background-repeat: repeat-y; height: 204px;">
            </tr>
            <tr>
                <td>
                    <img width="14" height="38" src="images/page_08.jpg" complete="complete" />
                </td>
                <td style="background-image: url(images/page_07.jpg); width: 100%; background-repeat: repeat-x; height: 38px;" />
                &nbsp;<td>
                    <img width="14" height="38" src="images/page_06.jpg" complete="complete" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
