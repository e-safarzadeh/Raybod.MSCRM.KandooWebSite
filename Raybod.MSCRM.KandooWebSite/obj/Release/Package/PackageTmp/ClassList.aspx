<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="ClassList.aspx.cs" Inherits="Raybod.MSCRM.KandooWebSite.ClassList" %>

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
                        <td style="width: 134px">&nbsp;
                        <asp:LinkButton ID="LinkMeetingList" runat="server" CausesValidation="False" OnClick="LinkChangeUser_Click" Style="font-size: x-small; color: #FFFFFF; ">تغییر نام کاربری</asp:LinkButton>
                        </td>
                        <td style="width: 712px; text-align: left">
                    <asp:LinkButton ID="LinkMeetingList0" runat="server" CausesValidation="False" OnClick="LinkChangePass_Click" Style="font-size: x-small; color: #FFFFFF; text-decoration: none underline">تغییر رمز عبور</asp:LinkButton>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;<asp:Label ID="LabelUser" runat="server" Text="شخص" Style="font-size: x-small"></asp:Label>
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
                        <td style="height: 24px; font-size: x-small; text-align: right" valign="baseline">
                            <asp:ImageButton ID="RefreshBtn" runat="server" Height="16px" ImageAlign="Middle" ImageUrl="~/images/1315464896_Refresh.jpg" OnClick="RefreshBtn_Click" Width="18px" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; height: 19px;" valign="top">
                            <asp:UpdatePanel ID="AjaxUpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="RequestClassView" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                        Width="100%" Style="margin-top: 0px" DataKeyNames="classid" OnRowCommand="RequestClassView_RowCommand"
                                        OnRowDataBound="RequestClassView_RowDataBound" AllowPaging="True" AllowCustomPaging="true"
                                        OnPageIndexChanging="RequestClassView_PageIndexChanging"
                                        OnRowCancelingEdit="RequestClassView_RowCancelingEdit">
                                        <AlternatingRowStyle BackColor="#E9DFD1" />
                                        <Columns>
                                            <asp:BoundField DataField="name" HeaderText="نام کلاس">
                                                <HeaderStyle HorizontalAlign="center" />
                                                <ItemStyle Width="400px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="typeofclass" HeaderText="نوع کلاس">
                                                <HeaderStyle HorizontalAlign="center" />
                                                <ItemStyle Width="300px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="showtimes" HeaderText="سانس">
                                                <HeaderStyle HorizontalAlign="center" />
                                                <ItemStyle Width="300px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="classstatus" HeaderText="وضعیت کلاس">
                                                <HeaderStyle HorizontalAlign="center" />
                                                <ItemStyle Width="300px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="vmname" HeaderText="نام ماشین مجازی">
                                                <HeaderStyle HorizontalAlign="center" />
                                                <ItemStyle Width="300px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="teacherpassword" HeaderText="رمز عبور استاد">
                                                <HeaderStyle HorizontalAlign="center" />
                                                <ItemStyle Width="300px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="studentpassword" HeaderText="رمز عبور دانشجویان">
                                                <HeaderStyle HorizontalAlign="center" />
                                                <ItemStyle Width="300px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="holdinglocationid" HeaderText="محل برگزاری">
                                                <HeaderStyle HorizontalAlign="center" />
                                                <ItemStyle Width="250px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="relatedlabid" HeaderText="لابراتوار">
                                                <ItemStyle HorizontalAlign="Center" Width="250px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="startdate" HeaderText="تاریخ شروع">
                                                <HeaderStyle HorizontalAlign="center" />
                                                <ItemStyle Width="200px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="starttime" HeaderText="ساعت شروع">
                                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="endtime" HeaderText="ساعت پایان">
                                                <ItemStyle Width="100px" />
                                            </asp:BoundField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="Open" runat="server" CommandArgument='<%# Eval("classid") %>'
                                                        CommandName="Open" Text="انتخاب" />
                                                </ItemTemplate>
                                                <ControlStyle Font-Names="Tahoma" Font-Size="8pt" />
                                                <ItemStyle />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="classid" HeaderText="classid" Visible="false"></asp:BoundField>
                                        </Columns>
                                        <HeaderStyle BackColor="#5B3726" Font-Bold="True" ForeColor="White" Height="25px" />
                                        <PagerSettings Mode="NextPrevious" />
                                        <PagerStyle BackColor="#80242C" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                            VerticalAlign="Middle" />
                                    </asp:GridView>
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
                </table>
            </td>
            <td style="background-image: url('images/page_04.jpg'); width: 14px; background-repeat: repeat-y; height: 176px"></td>
        </tr>
        <tr>
            <td style="height: 38px">
                <img width="14" height="38" src="images/page_08.jpg" complete="complete" />
            </td>
            <td style="background-image: url('images/page_07.jpg'); width: 100%; background-repeat: repeat-x; height: 38px"></td>
            <td style="height: 38px">
                <img width="14" height="38" src="images/page_06.jpg" complete="complete" />
            </td>
        </tr>
    </table>
</asp:Content>
