<%@ Page Title="" Language="C#" MasterPageFile="~/_Layout.Master" AutoEventWireup="true" CodeBehind="Cursos_regulatorios_elegir_sesion.aspx.cs" Inherits="SIE_KEY_USER.Views.Cursos_regulatorios_elegir_sesion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<form id="mainForm" runat="server">
        <div id="container" style="font-size: 1.3em; padding-top: 10%;">
            <div id="blank_space" class="col-sm-2"></div>
                <div id="details" class="col-sm-8">
                <table class="table table-bordered table-hover table-responsive">
                    <tr style="background: #00ad21; color: #fff;">
                        <th style="text-align: center;">Estás a punto de <b>darte de alta</b> en el siguiente curso</th>
                    <tr>
                        <td>
                            <asp:Label ID="lblFolio" runat="server" Text="Folio"></asp:Label>
                        </td>
                    </tr>
                    </tr>
                        <td style="background: #e6e6f0;">
                            <asp:Label ID="lblCodigo" runat="server" Text="Código"></asp:Label>
                        </td>
                    <tr>
                        <td>
                            <asp:Label ID="lblNombre" runat="server" Text="Nombre"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="background: #e6e6f0">
                            <asp:Label ID="lblFecha" runat="server" Text="Fecha"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                             <asp:Label ID="lblHorario" runat="server" Text="Horario"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="background: #e6e6f0">
                            <asp:Label ID="lblLugar" runat="server" Text="Lugar"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCupo" runat="server" Text="Cupo"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblAdvertencia" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnRegresar" runat="server" Text="Cancelar" CssClass="btn btn-warning btn-lg" OnClick="btnRegresar_Click" />
                            <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar" CssClass="btn btn-success btn-lg" OnClick="btnConfirmar_Click"/>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</asp:Content>
