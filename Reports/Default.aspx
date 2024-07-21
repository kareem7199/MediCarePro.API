<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Reports._Default" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="min-vh-100 w-100 d-flex align-items-center justify-content-center">
        <div>
            <h2>Login</h2>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtEmail">Email:</asp:Label>
                <asp:TextBox runat="server" class="form-control" ID="txtEmail" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEmail" ErrorMessage="Email is required." />
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtPassword">Password:</asp:Label>
                <asp:TextBox runat="server" class="form-control" ID="txtPassword" TextMode="Password" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPassword" ErrorMessage="Password is required." />
            </div>
            <p>
                <asp:Button runat="server" ID="btnLogin" Text="Login" OnClick="btnLogin_Click" class="btn btn-primary" />
            </p>
            <asp:Literal runat="server" ID="lblMessage" Visible="false"></asp:Literal>
        </div>
    </div>

</asp:Content>
