<%@ Page Title="" Language="C#" MasterPageFile="~/_Layout.Master" AutoEventWireup="true" CodeBehind="Cursos_regulatorios.aspx.cs" Inherits="SIE_KEY_USER.Views.Cursos_regulatorios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <div class="col-md-2"></div>
    <div class="col-md-8">
        <form id="mainForm" runat="server">
            
            <div>
                <asp:Button ID="btnPeriodo" runat="server" Text="Modificar fechas" style="margin-top: 1%; margin-left: 15px; width: 190px;" CssClass="btn btn-success btn-lg" OnClick="btnPeriodo_Click"/>
                <asp:Button ID="btnBuscar" runat="server" Text="Búsqueda por empleado" style="margin-top: 1%; margin-left: 15px; width: 240px;" CssClass="btn btn-success btn-lg" OnClick="btnBuscar_Click"/>
                <asp:Button ID="btnPendientes" runat="server" Text="Pendientes" style="margin-top: 1%; margin-left: 15px; width: 190px;" CssClass="btn btn-success btn-lg" OnClick="btnPendientes_Click"/>
            </div>
            <div>
                <asp:Button ID="btnAceptar" runat="server" Text="Aprobar" style="margin-top: 2%; margin-right: 15px; width: 150px;" CssClass="btn btn-success btn-lg" OnClick="btnAceptar_Click"/>
                <asp:Button ID="btnRechazar" runat="server" Text="Rechazar" style="margin-top: 2%; width: 150px;" CssClass="btn btn-warning btn-lg" OnClick="btnRechazar_Click"/><br/><br/>
             </div>
            <table id="pnlGridView" runat="server" align="center" class="table table-bordered table-hover table-responsive">
                <tr>
                    <td>
                        <asp:GridView ID="gvReprogramaciones" runat="server" class="table table-bordered table-hover table-responsive grid" OnDataBound="gvReprogramaciones_DataBound" OnRowDataBound="gvReprogramaciones_RowDataBound">
                            <Columns>
                                 <asp:TemplateField HeaderText="Seleccionar">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSeleccionar" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                No hay reprogramaciones pendientes
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            <asp:Button ID="btnReprogramaciones" runat="server" Text="Lista de Reprogramacion" CssClass="btn btn-success btn-lg" OnClick="btnReprogramaciones_Click"/> <br /> <br />
            <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="btn btn-warning btn-lg" OnClick="btnRegresar_Click" />
        </form>
    </div>
</asp:Content>
