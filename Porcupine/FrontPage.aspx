<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrontPage.aspx.cs" Inherits="Porcupine.FrontPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ProjectOverview</title>
</head>

<body>
    <form id="SelectMainProject" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"></asp:ScriptManager>
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:DropDownList ID="ddl_MainProjects" runat="server" AutoPostBack="True" OnSelectedIndexChanged ="SelectedIndexChange"
                        SelectMethod="GetProject" AppendDataBoundItems ="true" DataTextField="Name" DataValueField="Id">
                        <asp:ListItem Value="" Text="(All)" />
                    </asp:DropDownList> 
                    <br />
                    <br />
                    <br />
                    <asp:Label ID="lbl_Debug" Text="Nothing to debug yet" runat ="server" Visible ="false"></asp:Label>
                    
                    <br />
                    <br />
                    
                    <asp:GridView ID="partsGrid" runat="server" DataKeyNames="Id" AllowPaging =" true" AllowSorting ="true" AutoGenerateColumns="false"
                        SelectMethod="partsGrid_GetData" AutoGenerateEditButton ="true" UpdateMethod="partsGrid_UpdateItem" ModelType="Models.Projects.Part">
                        <AlternatingRowStyle BackColor="#CCCCCC" BorderColor="#666699" BorderStyle="Solid" />
                        <Columns>
                            <asp:BoundField DataField="Id" HeaderText ="Id" ReadOnly="True" />
                            <asp:BoundField DataField="ProjectId" HeaderText ="ProjectId" ReadOnly="True" />
                            <asp:BoundField DataField="Name" HeaderText = "Name" ReadOnly="True" />
                            <asp:BoundField DataField="StartDate" HeaderText="StartDate" DataFormatString="{0:d}" />
                            <asp:BoundField DataField="NumOfDays" HeaderText="NumOfDays" />
                            <asp:BoundField DataField="OnlyWorkDays" HeaderText ="OnlyWorkDays" />
                        </Columns>
                        
                    </asp:GridView>
                    <asp:ValidationSummary runat="server" ShowModelStateErrors="true" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_MainProjects" />
                </Triggers>
            </asp:UpdatePanel>

        </div>
        <div>


        </div>
    </form>
</body>
</html>
