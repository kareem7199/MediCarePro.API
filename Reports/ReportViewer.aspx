<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReportViewer.aspx.cs" Inherits="Reports.ReportViewer" Async="true" %>


<%@ Register TagPrefix="rsweb" Namespace="Microsoft.Reporting.WebForms" Assembly="Microsoft.ReportViewer.WebForms" %>
<%@ Register TagPrefix="rsweb" Namespace="Microsoft.Reporting.WebForms" Assembly="Microsoft.ReportViewer.WebForms" %>
<%@ Register TagPrefix="rsweb" Namespace="Microsoft.Reporting.WebForms" Assembly="Microsoft.ReportViewer.WebForms" %>
<%@ Register TagPrefix="rsweb" Namespace="Microsoft.Reporting.WebForms" Assembly="Microsoft.ReportViewer.WebForms" %>
<%@ Register TagPrefix="rsweb" Namespace="Microsoft.Reporting.WebForms" Assembly="Microsoft.ReportViewer.WebForms" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="min-vh-100">
        <rsweb:ReportViewer runat="server" InternalBorderStyle="Solid" InternalBorderColor="204, 204, 204" InternalBorderWidth="1px" ToolBarItemBorderStyle="Solid"  ToolBarItemBorderWidth="1px" ToolBarItemPressedBorderStyle="Solid" ToolBarItemPressedBorderColor="51, 102, 153" ToolBarItemPressedBorderWidth="1px" ToolbarHoverBackgroundColor="" ToolBarItemHoverBackColor="" HighlightBackgroundColor="" ToolBarItemPressedHoverBackColor="153, 187, 226" SplitterBackColor="" ToolbarForegroundDisabledColor="" LinkDisabledColor="" ToolbarForegroundColor="" LinkActiveColor="" ToolbarHoverForegroundColor="" LinkActiveHoverColor="" PrimaryButtonBackgroundColor="" PrimaryButtonForegroundColor="" PrimaryButtonHoverBackgroundColor="" PrimaryButtonHoverForegroundColor="" SecondaryButtonBackgroundColor="" SecondaryButtonForegroundColor="" SecondaryButtonHoverBackgroundColor="" SecondaryButtonHoverForegroundColor="" ID="ctl14" ClientIDMode="AutoID" Height="500px">
            <LocalReport ReportPath="ReportTemplate.rdlc"></LocalReport>
        </rsweb:ReportViewer>

    </div>
</asp:Content>
