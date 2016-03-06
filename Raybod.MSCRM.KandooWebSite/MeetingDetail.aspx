<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="MeetingDetail.aspx.cs" Inherits="Raybod.MSCRM.KandooWebSite.MeetingDetail" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table style="width: 100%; height: 1px;" border="0" cellpadding="0" cellspacing="0"
        dir="rtl">
        <tr align="top">
            <td width="10" style="height: 4px">
                <img width="14" height="38" src="images/page_03_1.jpg" complete="complete" />
            </td>
            <td align="right" style="background-image: url('images/page_02_1.jpg'); width: 100%; background-repeat: repeat-x; font-family: Tahoma; font-weight: bold; font-size: 0.8em; color: #FFFFFF; height: 4px;"
                valign="middle">
                <table style="width: 100%; height: 30px;" dir="rtl">
                    <tr>
                        <td class="auto-style7">
                            <asp:LinkButton ID="LinkMeetingList" runat="server" Style="font-size: x-small; color: #FFFFFF; text-decoration: none"
                                CausesValidation="False" OnClick="LinkMeetingList_Click">بازگشت</asp:LinkButton>
                        </td>
                        <td style="width: 712px; text-align: left">&nbsp;&nbsp;<asp:Label ID="LabelUser" runat="server" Text="شخص" Style="font-size: x-small"></asp:Label>
                            &nbsp;&nbsp;
                        </td>
                        <td style="text-align: right" dir="rtl">
                            <asp:Image ID="ImageExit" runat="server" Height="16px" ImageAlign="Top" ImageUrl="~/images/unlock.jpg" />
                            &nbsp;<asp:LinkButton ID="LinkSignOut" runat="server" Style="font-size: x-small; color: #FFFFFF; text-decoration: none"
                                OnClick="LinkSignOut_Click" CausesValidation="False">خروج</asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="14" style="height: 4px">
                <img width="14" height="38" src="images/page_01_1.jpg" complete="complete" />
            </td>
        </tr>
        <tr>
            <td style="background-image: url('images/page_05.jpg'); width: 14px; background-repeat: repeat-y; height: 176px"></td>
            <td style="width: 100%; height: 176px" valign="top">
                <table style="width: 100%; font-family: Tahoma; font-size: x-small;" cellpadding="0"
                    cellspacing="5" dir="rtl" border="0">
                    <tr>
                        <td style="height: 24px; font-size: x-small; text-align: right; width: 92px;" valign="baseline"><span style="font-family: Tahoma; font-weight: bold; text-decoration: underline;" class="auto-style16">اطلاعات جلسه:</span><span style="text-decoration: underline"> </span></td>
                        <td style="height: 24px; font-size: x-small; text-align: right; width: 186px;" valign="baseline"></td>
                        <td style="height: 24px; font-size: x-small; text-align: right; width: 112px;" valign="baseline"></td>
                        <td style="height: 24px; font-size: x-small; text-align: right; width: 186px;" valign="baseline"></td>
                        <td style="height: 24px; font-size: x-small; text-align: right; width: 74px;" valign="baseline"></td>
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
                            <span style="font-family: Tahoma; font-size: x-small; font-weight: bold">نام جلسه:</span>
                        </td>
                        <td style="height: 24px; font-size: x-small; text-align: right; width: 186px;" valign="baseline">
                            <asp:TextBox ID="txtMeetingName" runat="server" Width="216px" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td style="height: 24px; font-size: x-small; text-align: right; width: 74px;" valign="baseline">
                            <span style="font-family: Tahoma; font-size: x-small; font-weight: bold">وضعیت :</span>
                        </td>
                        <td style="height: 24px; font-size: x-small; text-align: right; width: 186px;" valign="baseline">
                            <asp:TextBox ID="txtStatus" runat="server" Width="208px" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 92px">
                            <span style="font-family: Tahoma; font-size: x-small; font-weight: bold">ساعت شروع:

                            </span>
                        </td>
                        <td style="height: 24px; font-size: x-small; text-align: right; width: 186px;" valign="baseline">
                            <asp:DropDownList ID="cmbStarttime" runat="server" Font-Names="Tahoma" Font-Size="X-Small"
                                Height="20px" Width="207px">
                                <asp:ListItem Value="0">00</asp:ListItem>
                                <asp:ListItem Value="1">01</asp:ListItem>
                                <asp:ListItem Value="2">02</asp:ListItem>
                                <asp:ListItem Value="3">03</asp:ListItem>
                                <asp:ListItem Value="4">04</asp:ListItem>
                                <asp:ListItem Value="5">05</asp:ListItem>
                                <asp:ListItem Value="6">06</asp:ListItem>
                                <asp:ListItem Value="7">07</asp:ListItem>
                                <asp:ListItem Value="8">08</asp:ListItem>
                                <asp:ListItem Value="9">09</asp:ListItem>
                                <asp:ListItem Value="10">10</asp:ListItem>
                                <asp:ListItem Value="11">11</asp:ListItem>
                                <asp:ListItem Value="12">12</asp:ListItem>
                                <asp:ListItem Value="13">13</asp:ListItem>
                                <asp:ListItem Value="14">14</asp:ListItem>
                                <asp:ListItem Value="15">15</asp:ListItem>
                                <asp:ListItem Value="16">16</asp:ListItem>
                                <asp:ListItem Value="17">17</asp:ListItem>
                                <asp:ListItem Value="18">18</asp:ListItem>
                                <asp:ListItem Value="19">19</asp:ListItem>
                                <asp:ListItem Value="20">20</asp:ListItem>
                                <asp:ListItem Value="21">21</asp:ListItem>
                                <asp:ListItem Value="22">22</asp:ListItem>
                                <asp:ListItem Value="23">23</asp:ListItem>
                                <asp:ListItem Value="24">24</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="height: 24px; font-size: x-small; text-align: right; width: 112px;" valign="baseline">
                            <span style="font-family: Tahoma; font-size: x-small; font-weight: bold">دقیقه:</span>&nbsp; <span style="font-family: Tahoma; font-size: x-small; font-weight: bold"><span style="color: #FF0000">*</span></span></td>
                        <td style="height: 24px; font-size: x-small; text-align: right; width: 186px;" valign="baseline">
                            <asp:DropDownList ID="cmbMinutesStart" runat="server" Font-Names="Tahoma" Font-Size="X-Small"
                                Height="20px" Width="216px">
                                <asp:ListItem Value="100" Selected="True">بدون انتخاب</asp:ListItem>
                                <asp:ListItem Value="0" Selected ="False">00</asp:ListItem>
                                <asp:ListItem Value="5" Selected ="False">05</asp:ListItem>
                                <asp:ListItem Value="10" Selected ="False">10</asp:ListItem>
                                <asp:ListItem Value="15" Selected ="False">15</asp:ListItem>
                                <asp:ListItem Value="20" Selected ="False">20</asp:ListItem>
                                <asp:ListItem Value="25" Selected ="False">25</asp:ListItem>
                                <asp:ListItem Value="30" Selected ="False">30</asp:ListItem>
                                <asp:ListItem Value="35" Selected ="False">35</asp:ListItem>
                                <asp:ListItem Value="40" Selected ="False">40</asp:ListItem>
                                <asp:ListItem Value="45" Selected ="False">45</asp:ListItem>
                                <asp:ListItem Value="50" Selected ="False">50</asp:ListItem>
                                <asp:ListItem Value="55" Selected ="False">55</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="height: 24px; font-size: x-small; text-align: right; width: 74px;" valign="baseline"></td>
                        <td style="height: 24px; font-size: x-small; text-align: right; width: 186px;" valign="baseline">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="cmbMinutesStart"
                                Display="Dynamic" InitialValue="100" ErrorMessage="لطفا زمان شروع را وارد كنيد." Font-Bold="False"
                                Font-Names="Tahoma" ForeColor="#FF3300" CssClass="style14" Style="font-size: x-small"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 92px">
                            <span style="font-family: Tahoma; font-size: x-small; font-weight: bold">ساعت پایان: </span>
                        </td>
                        <td style="height: 24px; font-size: x-small; text-align: right; width: 186px;" valign="baseline">
                            <asp:DropDownList ID="cmbEndTime" runat="server" Font-Names="Tahoma" Font-Size="X-Small"
                                Height="20px" Width="207px">
                                <asp:ListItem Value="0">00</asp:ListItem>
                                <asp:ListItem Value="1">01</asp:ListItem>
                                <asp:ListItem Value="2">02</asp:ListItem>
                                <asp:ListItem Value="3">03</asp:ListItem>
                                <asp:ListItem Value="4">04</asp:ListItem>
                                <asp:ListItem Value="5">05</asp:ListItem>
                                <asp:ListItem Value="6">06</asp:ListItem>
                                <asp:ListItem Value="7">07</asp:ListItem>
                                <asp:ListItem Value="8">08</asp:ListItem>
                                <asp:ListItem Value="9">09</asp:ListItem>
                                <asp:ListItem Value="10">10</asp:ListItem>
                                <asp:ListItem Value="11">11</asp:ListItem>
                                <asp:ListItem Value="12">12</asp:ListItem>
                                <asp:ListItem Value="13">13</asp:ListItem>
                                <asp:ListItem Value="14">14</asp:ListItem>
                                <asp:ListItem Value="15">15</asp:ListItem>
                                <asp:ListItem Value="16">16</asp:ListItem>
                                <asp:ListItem Value="17">17</asp:ListItem>
                                <asp:ListItem Value="18">18</asp:ListItem>
                                <asp:ListItem Value="19">19</asp:ListItem>
                                <asp:ListItem Value="20">20</asp:ListItem>
                                <asp:ListItem Value="21">21</asp:ListItem>
                                <asp:ListItem Value="22">22</asp:ListItem>
                                <asp:ListItem Value="23">23</asp:ListItem>
                                <asp:ListItem Value="24">24</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="height: 24px; font-size: x-small; text-align: right; width: 112px;" valign="baseline">
                            <span style="font-family: Tahoma; font-size: x-small; font-weight: bold">دقیقه:</span>&nbsp; <span style="font-family: Tahoma; font-size: x-small; font-weight: bold"><span style="color: #FF0000">*</span></span></td>
                        <td style="height: 24px; font-size: x-small; text-align: right; width: 186px;" valign="baseline">
                            <asp:DropDownList ID="cmdMinutesEnd" runat="server" Font-Names="Tahoma" Font-Size="X-Small"
                                Height="20px" Width="216px">
                                <asp:ListItem Value="100" Selected="True">بدون انتخاب</asp:ListItem>
                                <asp:ListItem Value="0">00</asp:ListItem>
                                <asp:ListItem Value="5">05</asp:ListItem>
                                <asp:ListItem Value="10">10</asp:ListItem>
                                <asp:ListItem Value="15">15</asp:ListItem>
                                <asp:ListItem Value="20">20</asp:ListItem>
                                <asp:ListItem Value="25">25</asp:ListItem>
                                <asp:ListItem Value="30">30</asp:ListItem>
                                <asp:ListItem Value="35">35</asp:ListItem>
                                <asp:ListItem Value="40">40</asp:ListItem>
                                <asp:ListItem Value="45">45</asp:ListItem>
                                <asp:ListItem Value="50">50</asp:ListItem>
                                <asp:ListItem Value="55">55</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="height: 24px; font-size: x-small; text-align: right; width: 74px;" valign="baseline"></td>
                        <td style="height: 24px; font-size: x-small; text-align: right; width: 186px;" valign="baseline">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="cmdMinutesEnd"
                                Display="Dynamic" InitialValue="100" ErrorMessage="لطفا زمان پایان را وارد كنيد." Font-Bold="False"
                                Font-Names="Tahoma" ForeColor="#FF3300" CssClass="style14" Style="font-size: x-small"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 92px; text-align: right;">&nbsp;
                            <asp:ImageButton ID="RefreshBtn" runat="server" Height="16px" ImageAlign="Middle" ImageUrl="~/images/1315464896_Refresh.jpg" OnClick="RefreshBtn_Click" Width="18px" />
                        </td>
                    </tr>
                </table>
                <table style="width: 100%; font-family: Tahoma; font-size: x-small; height: 383px;" cellpadding="0"
                    cellspacing="5" dir="rtl" border="0">
                    <tr>
                        <td style="text-align: center; height: 19px;" valign="top">
                            <asp:UpdatePanel ID="AjaxUpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div style="overflow: auto">
                                        <%--<div style="overflow-y: scroll;">--%>
                                        <asp:GridView ID="RequestMeetingDetailView" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                            Width="100%" Style="margin-top: 0px" DataKeyNames="attendanceitemsid" OnRowCommand="RequestMeetingDetailView_RowCommand"
                                            OnRowDataBound="RequestMeetingDetailView_RowDataBound"
                                            OnRowCancelingEdit="RequestMeetingDetailView_RowCancelingEdit">
                                            <AlternatingRowStyle BackColor="#E9DFD1" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="ردیف">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="gendercode" HeaderText="جنسیت">
                                                    <HeaderStyle HorizontalAlign="center" />
                                                    <ItemStyle Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="studentnumber" HeaderText="شماره دانشجویی">
                                                    <HeaderStyle HorizontalAlign="center" />
                                                    <ItemStyle Width="300px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="name" HeaderText="نام دانشجو">
                                                    <HeaderStyle HorizontalAlign="center" />
                                                    <ItemStyle Width="300px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="relatedclass" HeaderText="کلاس مربوطه">
                                                    <HeaderStyle HorizontalAlign="center" />
                                                    <ItemStyle Width="200px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="relatedcompanyid" HeaderText="سازمان مربوطه">
                                                    <HeaderStyle HorizontalAlign="center" />
                                                    <ItemStyle Width="250px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="attendanceitemsid" HeaderText="new_attendaceitemsid" Visible="False" />
                                                <%--<asp:TemplateField HeaderText="حاضر">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:RadioButton runat="server" ID="chkBar" onclick="javascript:GridSelectAllColumn(this, 'chkHigh');" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="غایب">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:RadioButton runat="server" ID="chkLine" onclick="javascript:GridSelectAllColumn(this, 'chkMid');" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Text="حاضر" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="غایب" Value="2"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle BackColor="#5B3726" Font-Bold="True" ForeColor="White" Height="25px" />
                                            <PagerSettings Mode="NumericFirstLast" />
                                            <PagerStyle BackColor="#80242C" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                                        </asp:GridView>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="RefreshBtn" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0">
                                <ProgressTemplate>
                                    <div id="IMGDIV" align="center" valign="middle" runat="server" style="border: medium solid #80242C; position: absolute; left: 50%; top: 40%; visibility: visible; vertical-align: middle; background-color: #c8d1d4;">
                                        <asp:Image ID="AjaxLoader" runat="server" ImageUrl="~/images/ajax_load_red.gif" Height="78px"
                                            Width="82px" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 24px; font-size: x-small; text-align: right; width: 112px;" valign="baseline">&nbsp;</td>
                    </tr>
                </table>
            </td>
            <td style="background-image: url('images/page_04.jpg'); width: 14px; background-repeat: repeat-y; height: 176px"></td>
        </tr>
        <tr>
            <td style="height: 38px">
                <img width="14" height="38" src="images/page_08.jpg" complete="complete" />
            </td>
            <td style="background-image: url('images/page_07.jpg'); width: 100%; background-repeat: repeat-x; height: 38px">
                <asp:Button ID="btnSave" runat="server" Font-Names="Tahoma" OnClick="btnSubmit_Click"
                    Text="ثبت" Width="55px" CssClass="style14" Font-Size="0.9em" Height="24px" Style="font-size: x-small; font-weight: 700; text-align: center" />
            </td>
            <td style="height: 38px">
                <img width="14" height="38" src="images/page_06.jpg" complete="complete" />
            </td>
        </tr>
    </table>
</asp:Content>
