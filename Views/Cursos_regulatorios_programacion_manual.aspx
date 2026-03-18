<%@ Page Title="" Language="C#" MasterPageFile="~/_Layout.Master" AutoEventWireup="true" CodeBehind="Cursos_regulatorios_programacion_manual.aspx.cs" Inherits="SIE_KEY_USER.Views.Cursos_regulatorios_programacion_manual" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <br />
        <form id="mainForm" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
        <Scripts>
            <asp:ScriptReference Path="~/Scripts/bootstrap/popover_ayuda.js" />
            <asp:ScriptReference Path="~/Scripts/click_bttn/click_btnn.js" />
        </Scripts>
    </asp:ScriptManager> 
            <div class="container"> 
                <div class="col-lg-3">
                    <div class="row"> 
                        <asp:Label Text="Numero de Reloj" runat="server" />
                    </div>
                    <div class="row"> 
                        <asp:Panel runat="server" DefaultButton="btnBuscar"> 
                        <asp:TextBox ID="txtNumReloj" style="font-size: 1.3em; align-content:center;" type="number" runat="server" min="0" autocomplete="off" CssClass="form form-control text-center"></asp:TextBox>
                        </asp:Panel>
                        <br />
                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar cursos" style="margin-top:3%;" CssClass="btn btn-success btn-lg" OnClick="btnBuscar_Click"/>
                    </div>
                </div>
                <div class="col-lg-3">
                      <div class="row"> 
                        <asp:Label Text="Nombre Completo" runat="server" />
                    </div>
                    <div class="row"> 
                        <asp:Panel runat="server" DefaultButton="btnBuscar1"> 
                        <asp:TextBox ID="txtPrettyName" style="font-size: 1.3em; align-content:center;" type="text" runat="server" CssClass="form form-control text-center"></asp:TextBox>
                        <button class="ayuda" type="button" data-toggle="popover" data-trigger="focus" title="Ayuda" data-content="Ejemplo de búsqueda: 'primer apellido' 'segundo apellido' ',' 'primer nombre' 'segundo nombre' " tabindex="4">?</button>
                        </asp:Panel>
                        <asp:Button ID="btnBuscar1" runat="server" Text="Buscar cursos" style="margin-top:3%;" CssClass="btn btn-success btn-lg" OnClick="btnBuscar2_Click"/>
                    </div>
                </div>
                <div class="col-lg-6">
                      <asp:Label id="lblNombre" runat="server" Text=""></asp:Label><br />
                      <asp:Label id="lblPuesto" runat="server" Text=""></asp:Label><br />
                      <asp:Label id="lblHorario" runat="server" Text=""></asp:Label>
                </div>
            </div>
            <br />
            <table width="90%" id="pnlGridView" runat="server" align="center" class="table table-bordered table-hover table-responsive">
                <tr>
                    <td>
                        <asp:GridView ID="gvCursos" runat="server" style="background-color: white;" class="table table-bordered table-hover table-responsive grid" OnDataBound="gvCursos_DataBound" OnRowCommand="gvCursos_RowCommand" TabIndex="1">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="lkbSeleccionar" CssClass="btn btn-primary" style="width: 100%; height: 50px; padding-top:15px; padding-bottom: 15px;" CommandName="Select" CommandArgument="<%# Container.DataItemIndex %>"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                No se encontraron cursos para el puesto del empleado
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            <asp:Label ForeColor="Red" Text="" ID="txtAviso" runat="server" />
        </form>
</asp:Content>
