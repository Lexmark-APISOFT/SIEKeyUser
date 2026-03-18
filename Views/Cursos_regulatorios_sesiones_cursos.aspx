<%@ Page Title="" Language="C#" MasterPageFile="~/_Layout.Master" AutoEventWireup="true" CodeBehind="Cursos_regulatorios_sesiones_cursos.aspx.cs" Inherits="SIE_KEY_USER.Views.Cursos_regulatorios_sesiones_cursos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <div class="col-md-2"></div>
    <div class="col-md-8" style="padding-top: 5%;">
    <form id="mainForm" runat="server">
        <asp:Button ID="btnSwitch" runat="server" CssClass="btn btn-success btn-lg pull-left" Text="Cursos fuera de Horario" OnClick="btnSwitch_Click" />
        
        <asp:GridView ID="gvSesiones" runat="server" style="background-color: white;" CssClass="table table-bordered table-hover table-responsive grid" OnDataBound="gvSesiones_DataBound" OnRowCommand="gvSesiones_RowCommand">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton runat="server" ID="lkbSeleccionar" CssClass="btn btn-primary" style="width: 100%; height: 50px; padding-top:15px; padding-bottom: 15px;" CommandName="Select" CommandArgument="<%# Container.DataItemIndex %>"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                No se encontraron sesiones disponibles
            </EmptyDataTemplate>
        </asp:GridView>
        
    <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="btn btn-warning btn-lg" OnClick="btnRegresar_Click" />
    </form>
    </div>
</asp:Content>
