<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="RequestConfirmTecnical.aspx.cs" Inherits="Raybod.MSCRM.KandooWebSite.RequestConfirmTecnical" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width: 100%; height: 154px;" border="0" cellpadding="0" cellspacing="0"
        __designer:mapid="31" dir="rtl">
        <tr __designer:mapid="32" valign="top">
            <td width="10" style="height: 25px" __designer:mapid="33">
                <img width="14" height="38" src="images/page_03_1.jpg" complete="complete" __designer:mapid="34" />
            </td>
            <td align="right" class="page_title" style="background-image: url('images/page_02_1.jpg');
                width: 100%; background-repeat: repeat-x; height: 25px; font-family: Tahoma;
                font-weight: bold; font-size: 0.8em; color: #FFFFFF;" __designer:mapid="35" valign="middle">
                <table style="width: 100%; height: 30px;" dir="rtl">
                    <tr>
                        <td class="style15" style="width: 134px">
                            <asp:LinkButton ID="LinkButton2" runat="server" Style="font-size: x-small;
                                color: #FFFFFF; text-decoration: none" CausesValidation="False" OnClick="LinkRequestList_Click">بازگشت</asp:LinkButton>
                        </td>
                        <td style="width: 712px; text-align: left">
                            &nbsp;&nbsp;<asp:Label ID="LabelPersonnel" 
                                runat="server" Text="شخص" Style="font-size: x-small"></asp:Label>
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
            <td width="14" style="height: 25px" __designer:mapid="36">
                <img width="14" height="38" src="images/page_01_1.jpg" complete="complete" __designer:mapid="37" />
            </td>
        </tr>
        <tr __designer:mapid="38">
            <td style="background-image: url(images/page_05.jpg); width: 14px; background-repeat: repeat-y;"
                __designer:mapid="39">
                <td style="width: 100%" __designer:mapid="3a" valign="bottom">
                    <table style="width: 100%; height: auto; font-family: Tahoma; font-size: x-small;"
                        cellpadding="0" cellspacing="5" dir="rtl" border="0">
                        <tr>
                            <td colspan="2" style="height: 9px; font-size: x-small; text-align: right">
                                &nbsp;<span class="style13" style="text-align: left">ثبت درخواست شما با موفقيت انجام
                                    شد.&nbsp;</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="style6" align="right" dir="rtl" style="height: 5px; width: 68px">
                                &nbsp; كد درخواست :
                            </td>
                            <td class="style10" style="height: 5px; text-align: right">
                                <asp:TextBox ID="txtTechnicalNumber" runat="server" Width="296px" ReadOnly="True" Font-Names="Tahoma"
                                    Style="margin-right: 0px; text-align: left; font-size: x-small;" CssClass="style15"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style14" colspan="2" style="text-align: right; height: 17px;">
                                <b>&nbsp; &nbsp;<asp:LinkButton ID="LinkAddRequest" runat="server" OnClick="LinkAddRequest_Click">درخواست فنی جديد</asp:LinkButton>
                                </b>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="background-image: url(images/page_04.jpg); width: 14px; background-repeat: repeat-y;"
                    __designer:mapid="55">
        </tr>
        <tr __designer:mapid="56">
            <td __designer:mapid="57" style="height: 38px">
                <img width="14" height="38" src="images/page_08.jpg" complete="complete" __designer:mapid="58" />
            </td>
            <td style="background-image: url(images/page_07.jpg); width: 100%; background-repeat: repeat-x;
                height: 38px;" __designer:mapid="59" />
            <td __designer:mapid="5a" style="height: 38px">
                <img width="14" height="38" src="images/page_06.jpg" complete="complete" __designer:mapid="5b" />
            </td>
        </tr>
    </table>
</asp:Content>
