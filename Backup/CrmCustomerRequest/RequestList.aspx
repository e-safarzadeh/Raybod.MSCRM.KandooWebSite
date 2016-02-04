<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="RequestList.aspx.cs" Inherits="CrmCustomerRequest.RequestList" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table style="width: 100%; height: 1px;" border="0" cellpadding="0" cellspacing="0"
        dir="rtl">
        <tr align="top">
            <td width="10" style="height: 4px">
                <img width="14" height="38" src="images/page_03.jpg" complete="complete" />
            </td>
            <td align="right" style="background-image: url('images/page_02.jpg'); width: 100%;
                background-repeat: repeat-x; font-family: Tahoma; font-weight: bold; font-size: 0.8em;
                color: #FFFFFF; height: 4px;" valign="middle">
                <table style="width: 100%; height: 30px;" dir="rtl">
                    <tr>
                        <td style="width: 134px">
                            <asp:Image ID="Image1" runat="server" Height="16px" ImageAlign="Top" ImageUrl="~/images/list.jpg" />
                            &nbsp;<asp:LinkButton ID="LinkAddRequest" runat="server" Style="font-size: x-small;
                                color: #FFFFFF; text-decoration: none" CausesValidation="False" OnClick="LinkAddRequest_Click">ثبت درخواست جديد</asp:LinkButton>
                        </td>
                        <td style="width: 712px; text-align: left">
                            &nbsp;&nbsp;<asp:Label ID="LabelPersonnel" runat="server" Text="شخص" Style="font-size: x-small"></asp:Label>
                            &nbsp;&nbsp;
                        </td>
                        <td style="text-align: right" dir="rtl">
                            <asp:Image ID="Image2" runat="server" Height="16px" ImageAlign="Top" ImageUrl="~/images/unlock.jpg" />
                            &nbsp;<asp:LinkButton ID="LinkSignOut" runat="server" Style="font-size: x-small;
                                color: #FFFFFF; text-decoration: none" OnClick="LinkSignOut_Click" CausesValidation="False">خروج</asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="14" style="height: 4px">
                <img width="14" height="38" src="images/page_01.jpg" complete="complete" />
            </td>
        </tr>
        <tr>
            <td style="background-image: url('images/page_05.jpg'); width: 14px; background-repeat: repeat-y;
                height: 176px">
            </td>
            <td style="width: 100%; height: 176px" valign="top">
                <table style="width: 100%; font-family: Tahoma; font-size: x-small;" cellpadding="0"
                    cellspacing="5" dir="rtl" border="0">
                    <tr>
                        <td style="height: 24px; font-size: x-small; text-align: right" valign="baseline">
                            <b>ليست درخواست ها</b>&nbsp;&nbsp;&nbsp;
                            <asp:DropDownList ID="SelectType" runat="server" Font-Names="Tahoma" Font-Size="8pt"
                                OnSelectedIndexChanged="SelectType_SelectedIndexChanged" AutoPostBack="True">
                                <asp:ListItem Value="1">همه درخواست ها</asp:ListItem>
                                <asp:ListItem Value="2">درخواست هاي در حال  انجام</asp:ListItem>
                                <asp:ListItem Value="3">درخواست هاي انجام شده يا كنسل شده</asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;
                            <asp:ImageButton ID="RefreshBtn" runat="server" Height="16px" ImageAlign="Middle"
                                ImageUrl="~/images/1315464896_Refresh.jpg" OnClick="RefreshBtn_Click" Width="18px" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 19px;" valign="top">
                            <asp:UpdatePanel ID="AjaxUpdatePanel" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="RequestView" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                        Width="100%" Style="margin-top: 0px" DataKeyNames="incidentid" OnRowCommand="RequestView_RowCommand"
                                        OnRowDataBound="RequestView_RowDataBound" AllowPaging="True" 
                                        OnPageIndexChanging="RequestView_PageIndexChanging" 
                                        onrowcancelingedit="RequestView_RowCancelingEdit">
                                        <AlternatingRowStyle BackColor="#FFF2F2" />
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <img alt="" src='<%# GetIcon((string)Eval("state")) %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ticketnumber" HeaderText="كد درخواست ">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="title" HeaderText="عنوان درخواست">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle Width="300px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="casetypecode" HeaderText="نوع">
                                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="description" HeaderText="شرح">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle Width="40%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="statecode" HeaderText="وضعيت">
                                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="helpdesk_statusdescription" HeaderText="شرح وضعیت">
                                                <ItemStyle Width="300px" />
                                            </asp:BoundField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="Confirm" runat="server" CommandArgument='<%# Eval("incidentid") %>'
                                                        CommandName="Confirm" />
                                                </ItemTemplate>
                                                <ControlStyle Font-Names="Tahoma" Font-Size="8pt" />
                                                <ItemStyle Width="200px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="Cancel" runat="server" CommandArgument='<%# Eval("incidentid") %>'
                                                        CommandName="Cancel" />
                                                </ItemTemplate>
                                                <ControlStyle Font-Names="Tahoma" Font-Size="8pt" />
                                                <ItemStyle Width="200px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="incidentid" HeaderText="incidentid" Visible="False" />
                                            <asp:BoundField DataField="state" HeaderText="state" Visible="False" />
                                            <asp:BoundField DataField="helpdesk_requesterconfirm" HeaderText="helpdesk_requesterconfirm"
                                                Visible="False" />
                                        </Columns>
                                        <HeaderStyle BackColor="#80242C" Font-Bold="True" ForeColor="White" Height="25px" />
                                        <PagerSettings Mode="NumericFirstLast" />
                                        <PagerStyle BackColor="#80242C" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                            VerticalAlign="Middle" />
                                    </asp:GridView>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="RefreshBtn" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="SelectType" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0">
                                <ProgressTemplate>
                                    <div id="IMGDIV" align="center" valign="middle" runat="server" style="border: medium solid #80242C;
                                        position: absolute; left: 50%; top: 40%; visibility: visible; vertical-align: middle;
                                        background-color: #c8d1d4;">
                                        <asp:Image ID="AjaxLoader" runat="server" ImageUrl="~/images/ajax_load_red.gif" Height="78px"
                                            Width="82px" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="background-image: url('images/page_04.jpg'); width: 14px; background-repeat: repeat-y;
                height: 176px">
            </td>
        </tr>
        <tr>
            <td style="height: 38px">
                <img width="14" height="38" src="images/page_08.jpg" complete="complete" />
            </td>
            <td style="background-image: url('images/page_07.jpg'); width: 100%; background-repeat: repeat-x;
                height: 38px">
            </td>
            <td style="height: 38px">
                <img width="14" height="38" src="images/page_06.jpg" complete="complete" />
            </td>
        </tr>
    </table>
</asp:Content>
