<%@ Page Title="विकीपिडिया नेपाली अनुवादक" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="WikipediaNepaliTranslator._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">

</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
    <asp:Table ID="Table1" runat="server">
            <asp:TableRow Width="800px">
                <asp:TableCell Width="400px">हिन्दी</asp:TableCell><asp:TableCell Width="400px">नेपाली</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow Width="800px">
                <asp:TableCell Width="400px"></asp:TableCell><asp:TableCell Width="400px">
                    <asp:Button ID="translateButton" runat="server" Text="Translate" OnClick="translateButton_Click" />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow Width="800px">
                <asp:TableCell Width="400px">
                    <asp:Panel ID="PanelHindi" runat="server" Width="300px">
                        <textarea id="richTextBoxHindi" rows="50" cols="40" runat="server"></textarea>
                    </asp:Panel>
                </asp:TableCell>
                <asp:TableCell Width="400px">
                <asp:Panel ID="PanelNepali" runat="server" Width="300px">
                        <textarea id="richTextBoxNepali" rows="50" cols="40" runat="server"></textarea>                 
                </asp:Panel>
                    
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow Width="800px">
                <asp:TableCell Width="400px">हिन्दी</asp:TableCell><asp:TableCell Width="400px">नेपाली</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow Width="800px">
                <asp:TableCell Width="400px"></asp:TableCell><asp:TableCell Width="400px">
                    <asp:Button ID="btnTranslate1" runat="server" Text="Translate" OnClick="translateButton_Click" /></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        

        
        <asp:HiddenField ID="referrerfield" runat="server" />
    </h2>
    <p>
        &nbsp;</p>
</asp:Content>
