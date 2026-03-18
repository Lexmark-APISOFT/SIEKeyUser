<%@ Page Title="" Language="C#" MasterPageFile="~/_Layout.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="SIE_KEY_USER.Views.Cursos.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


    <form runat="server">
        <div class="col-md-1" style="margin-left:100px;margin-bottom:20px;width:100%;display:flex;">
            <asp:LinkButton ID="btnBackPage" CssClass="d-flex align-items-center justify-content-center btn btn-back" runat="server" PostBackUrl="~/Views/Cursos/MenuCourses.aspx">
                <span class="glyphicon glyphicon-arrow-left" style="margin-top: 5px;"></span>
            </asp:LinkButton>
        </div>
        <div class="container-fluid justify-content-center" style="height: 100%">
            <div class="row" style="margin-top:50px;">
                <div class="col-md-4">
                </div>
                <div class="col-md-4">
                    <asp:Label runat="server" Text="Ingrese el numero de reloj" CssClass="header-titles"/>
                </div>
                <div class="col-md-4">
                </div>
            </div>
            <div class="row" style="margin-top:20px;">
                <div class="col-md-4">
                </div>
                <div class="col-md-4 align-middle">
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtID" />
                </div>
                <div class="col-md-4">
                </div>
            </div>
            <div class="row" style="margin-top:20px;">
                <div class="col-md-5">
                </div>
                <div class="col-md-2 align-middle">
                    <asp:Button ID="btnSearch" Text="Buscar" runat="server" CssClass="btn btn-success" OnClick="btnSearch_Click" />
                </div>
                <div class="col-md-5">
                </div>
            </div>
        </div>
    </form>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_JS" runat="server">
     <link href="../../Content/cursos.css" rel="stylesheet" />
</asp:Content>
