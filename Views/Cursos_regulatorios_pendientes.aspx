<%@ Page Title="" Language="C#" MasterPageFile="~/_Layout.Master" AutoEventWireup="true" CodeBehind="Cursos_regulatorios_pendientes.aspx.cs" Inherits="SIE_KEY_USER.Views.Cursos_regulatorios_pendientes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <form id="mainForm" runat="server">
            <table id="pnlGridView" runat="server" align="center" class="table table-bordered table-hover table-responsive">
                <tr>
                    <td>
                        <asp:GridView ID="gvPendientes" runat="server" class="table table-bordered table-hover table-responsive grid">
                            <EmptyDataTemplate>
                                No hay empleados pendientes
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="btn btn-warning btn-lg" OnClick="btnRegresar_Click" />
        </form>
</asp:Content>
