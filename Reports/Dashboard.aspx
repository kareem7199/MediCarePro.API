<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Reports.Dashboard" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:Label ID="lblWelcomeMessage" runat="server" Text="Welcome User!" /><br />
        <asp:Button ID="btnLogout" CssClass="btn btn-danger" runat="server" Text="Logout" OnClick="btnLogout_Click" />
    </div>
</asp:Content>
