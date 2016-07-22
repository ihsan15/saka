<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SAKA.Web.UI.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:GridView ID="SCORECARD" AutoGenerateColumns="false" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
            <Columns>
                <asp:BoundField HeaderText="Name" DataField="Name" ReadOnly="true" runat="server" />
                <asp:BoundField HeaderText="Value" DataField="Value" ReadOnly="true" runat="server" />
                <asp:BoundField HeaderText="Period" DataField="Period" ReadOnly="true" runat="server" />
                <asp:TemplateField HeaderText="Statu" >
                    <ItemTemplate >
                        <asp:Image runat="server" ImageUrl='<%# Eval("Statu") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>
        <br />
        <asp:PlaceHolder ID="Tablo" runat="server"></asp:PlaceHolder>
    </div>
    </form>
</body>
</html>
