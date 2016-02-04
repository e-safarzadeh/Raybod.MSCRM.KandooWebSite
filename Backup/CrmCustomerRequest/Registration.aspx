<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Registration.aspx.cs" Inherits="CrmCustomerRequest.Registration" %>

<%@ Register TagPrefix="cc1" Namespace="WebControlCaptcha" Assembly="WebControlCaptcha" %>
<script runat="server">

</script>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="Panel1" runat="server" dir="rtl" Height="431px">
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
                                &nbsp;<asp:LinkButton ID="LinkLogin" runat="server" Style="font-size: x-small; color: #FFFFFF;
                                    text-decoration: none" CausesValidation="False" OnClick="LinkRequestList_Click">ورود به سایت</asp:LinkButton>
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
                    height: 204px;">
                    <td style="width: 100%; font-family: Tahoma; padding-top: 4px; height: 204px;" align="right">
                        <table cellspacing="5" style="width: 100%; font-family: Tahoma; height: 0px;">
                            <tr align="left">
                                <td style="font-size: x-small; font-weight: bold; text-align: left; width: 117px">
                                    <span style="font-size: x-small; font-weight: bold">&nbsp;نام&nbsp; :&nbsp;</span><span
                                        style="font-size: x-small; font-weight: bold; color: #FF0000">*</span><span style="font-size: x-small;
                                            font-weight: bold"> </span>
                                </td>
                                <td style="text-align: right;">
                                    <asp:TextBox ID="txtName" runat="server" Width="300px" CssClass="style14" Style="font-size: x-small;
                                        font-family: Tahoma;"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName"
                                        Display="Dynamic" ErrorMessage="لطفا نام را وارد كنيد." Font-Names="Tahoma" Font-Size="X-Small"
                                        ForeColor="#FF3300">لطفا نام را وارد كنيد.</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr align="left">
                                <td style="font-size: x-small; font-weight: bold; text-align: left; width: 117px">
                                    <span style="font-size: x-small; font-weight: bold">&nbsp;نام&nbsp; خانوادگی :&nbsp;</span><span
                                        style="font-size: x-small; font-weight: bold; color: #FF0000">*</span><span style="font-size: x-small;
                                            font-weight: bold"> </span>
                                </td>
                                <td style="text-align: right;">
                                    <asp:TextBox ID="txtFamily" runat="server" CssClass="style14" Style="font-size: x-small;
                                        font-family: Tahoma;" Width="300px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFamily"
                                        Display="Dynamic" ErrorMessage="لطفا نام خانوادگی را وارد كنيد." Font-Names="Tahoma"
                                        Font-Size="X-Small" ForeColor="#FF3300">لطفا نام خانوادگی را وارد كنيد.</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr align="left">
                                <td style="font-size: x-small; font-weight: bold; text-align: left; width: 117px">
                                    <span style="font-size: x-small; font-weight: bold">&nbsp;شماره پرسنلی:&nbsp;</span><span
                                        style="font-size: x-small; font-weight: bold"> </span>
                                </td>
                                <td style="text-align: right;">
                                    <asp:TextBox ID="txtEmployeeNo" runat="server" CssClass="style14" Style="font-size: x-small;
                                        font-family: Tahoma;" Width="300px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr align="left">
                                <td style="font-size: x-small; font-weight: bold; text-align: left; width: 117px">
                                    <span style="font-size: x-small; font-weight: bold">&nbsp;خطاب&nbsp; ابتدای نامه :&nbsp;</span><span
                                        style="font-size: x-small; font-weight: bold; color: #FF0000">*</span><span style="font-size: x-small;
                                            font-weight: bold"> </span>
                                </td>
                                <td style="text-align: right;">
                                    <asp:DropDownList ID="Salutation" runat="server" Font-Names="Tahoma" Font-Size="X-Small"
                                        Height="20px" Width="303px">
                                        <asp:ListItem Value="1">جناب آقاي</asp:ListItem>
                                        <asp:ListItem Value="2">جناب آقاي دكتر</asp:ListItem>
                                        <asp:ListItem Value="3">جناب آقاي مهندس</asp:ListItem>
                                        <asp:ListItem Value="4">سركار خانم</asp:ListItem>
                                        <asp:ListItem Value="5">سركار خانم دكتر</asp:ListItem>
                                        <asp:ListItem Value="6">سركار خانم مهندس</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="Salutation"
                                        Display="Dynamic" ErrorMessage="لطفا خطاب ابتدای نامه را وارد كنيد." Font-Names="Tahoma"
                                        Font-Size="X-Small" ForeColor="#FF3300">لطفا خطاب ابتدای نامه را وارد كنيد.</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr align="left">
                                <td style="font-size: x-small; font-weight: bold; text-align: left; width: 117px">
                                    <span style="font-size: x-small; font-weight: bold">&nbsp;دپارتمان :&nbsp;</span><span
                                        style="font-size: x-small; font-weight: bold; color: #FF0000">*</span><span style="font-size: x-small;
                                            font-weight: bold"> </span>
                                </td>
                                <td style="text-align: right;">
                                    <asp:DropDownList ID="Department" runat="server" Font-Names="Tahoma" Font-Size="X-Small"
                                        Height="20px" Width="303px">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="Department"
                                        Display="Dynamic" ErrorMessage="لطفا دپارتمان را وارد كنيد." Font-Names="Tahoma"
                                        Font-Size="X-Small" ForeColor="#FF3300">لطفا دپارتمان را وارد كنيد.</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr align="left">
                                <td style="font-size: x-small; font-weight: bold; text-align: left; width: 117px;
                                    height: 25px;">
                                    <span style="font-size: x-small; font-weight: bold">&nbsp;سمت سازمانی :&nbsp;</span><span
                                        style="font-size: x-small; font-weight: bold"> </span>
                                </td>
                                <td style="text-align: right; height: 25px;">
                                    <asp:TextBox ID="txtJobTitle" runat="server" CssClass="style14" Style="font-size: x-small;
                                        font-family: Tahoma;" Width="300px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr align="left">
                                <td style="font-size: x-small; font-weight: bold; text-align: left; width: 117px">
                                    <span style="font-size: x-small; font-weight: bold">&nbsp;نام کاربری :&nbsp;</span><span
                                        style="font-size: x-small; font-weight: bold; color: #FF0000">*</span><span style="font-size: x-small;
                                            font-weight: bold"> </span>
                                </td>
                                <td style="text-align: right; font-family: Tahoma; font-size: x-small; color: #333333;">
                                    فرمت نام کاربری بصورت آدرس پست الکترونیک وارد شود.مثال : test@ayco.ir<br />
                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="style14" Style="font-size: x-small;
                                        font-family: Tahoma;" Width="300px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtEmail"
                                        Display="Dynamic" ErrorMessage="لطفا آدرس پست الکترونیک را وارد كنيد." Font-Names="Tahoma"
                                        Font-Size="X-Small" ForeColor="#FF3300">لطفا آدرس پست الکترونیک را وارد كنيد.</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
                                        Display="Dynamic" ErrorMessage="فرمت پست الکترونیک صحیح نمی باشد." Font-Names="Tahoma"
                                        Font-Size="X-Small" ForeColor="#FF3300" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">فرمت پست الکترونیک صحیح نمی باشد.</asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr align="left">
                                <td style="font-size: x-small; font-weight: bold; text-align: left; width: 117px">
                                    <span style="font-size: x-small; font-weight: bold">&nbsp;تلفن همراه :&nbsp;</span><span
                                        style="font-size: x-small; font-weight: bold"> </span>
                                </td>
                                <td style="text-align: right;">
                                    <asp:TextBox ID="txtMobile" runat="server" CssClass="style14" Style="font-size: x-small;
                                        font-family: Tahoma;" Width="300px"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtMobile"
                                        Display="Dynamic" ErrorMessage="فرمت تلفن همراه صحیح نمی باشد." Font-Names="Tahoma"
                                        Font-Size="X-Small" ForeColor="#FF3300" ValidationExpression="\d{11}">فرمت تلفن همراه صحیح نمی باشد.</asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="Department"
                                        Display="Dynamic" ErrorMessage="لطفا تلفن همراه را وارد كنيد." Font-Names="Tahoma"
                                        Font-Size="X-Small" ForeColor="#FF3300">لطفا دپارتمان را وارد كنيد.</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr align="left">
                                <td style="font-size: x-small; font-weight: bold; text-align: left; width: 117px">
                                    <span style="font-size: x-small; font-weight: bold">&nbsp;رمز عبور :&nbsp;</span><span
                                        style="font-size: x-small; font-weight: bold; color: #FF0000">*</span><span style="font-size: x-small;
                                            font-weight: bold"> </span>
                                </td>
                                <td style="text-align: right;">
                                    <asp:TextBox ID="txtPassword" runat="server" CssClass="style14" Style="font-size: x-small;
                                        font-family: Tahoma;" Width="300px" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtPassword"
                                        Display="Dynamic" ErrorMessage="لطفارمز عبور را وارد كنيد." Font-Names="Tahoma"
                                        Font-Size="X-Small" ForeColor="#FF3300">لطفارمز عبور را وارد كنيد.</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr align="left">
                                <td style="font-size: x-small; font-weight: bold; text-align: left; width: 117px">
                                    تکرار رمز عبو<span style="font-size: x-small; font-weight: bold">ر :&nbsp;</span><span
                                        style="font-size: x-small; font-weight: bold; color: #FF0000">*</span><span style="font-size: x-small;
                                            font-weight: bold"> </span>
                                </td>
                                <td style="text-align: right;">
                                    <asp:TextBox ID="txtPassword1" runat="server" CssClass="style14" Style="font-size: x-small;
                                        font-family: Tahoma;" Width="300px" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtPassword1"
                                        Display="Dynamic" ErrorMessage="لطفا تکرار رمز عبور را وارد كنيد." Font-Names="Tahoma"
                                        Font-Size="X-Small" ForeColor="#FF3300">لطفا تکرار رمز عبور را وارد كنيد.</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr align="right" dir="rtl">
                                <td style="font-size: x-small; font-weight: bold;" align="right" colspan="2" dir="rtl">
                                    <span style="font-size: x-small; font-weight: bold">مایل به ثبت درخواست از طریق درگاههای
                                        زیر هستید؟ :&nbsp;</span><span style="font-size: x-small; font-weight: bold">
                                    </span>
                                    <asp:CheckBox ID="CheckBoxSms" runat="server" Font-Names="Tahoma" Font-Size="X-Small"
                                        OnCheckedChanged="CheckBoxSms_CheckedChanged" Text="پیام کوتاه" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:CheckBox ID="CheckBoxCrm" runat="server" Font-Names="Tahoma" Font-Size="X-Small"
                                        Text="نرم افزار AycoHelpDesk" />
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: x-small; font-weight: bold; text-align: left; width: 117px;
                                    height: 18px;">
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" Height="16px" ShowMessageBox="True"
                                        ShowSummary="False" Style="font-size: x-small; font-family: Tahoma; font-weight: 700"
                                        Width="16px" />
                                    &nbsp;
                                </td>
                                <td style="text-align: right; height: 18px;">
                                    <asp:Button ID="btnSave" runat="server" CssClass="style14" Font-Names="Tahoma" Height="24px"
                                        OnClick="btnSave_Click" Style="font-size: x-small; font-weight: 700;" Text="ثبت"
                                        Width="55px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="background-image: url('images/page_04.jpg'); width: 14px; background-repeat: repeat-y;
                        height: 204px;">
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
