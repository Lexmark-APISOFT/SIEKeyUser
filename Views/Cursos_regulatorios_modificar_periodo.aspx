<%@ Page Title="" Language="C#" MasterPageFile="~/_Layout.Master" AutoEventWireup="true" CodeBehind="Cursos_regulatorios_modificar_periodo.aspx.cs" Inherits="SIE_KEY_USER.Views.Cursos_regulatorios_modificar_periodo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Content/cursos.css" rel="stylesheet" />
    <form runat="server">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <div class="col-md-1">
                        <%--<button onclick="" class="d-flex align-items-center justify-content-center btn" style="">
                    <span class="glyphicon glyphicon-arrow-left" style="margin-top: 5px;"></span>
                </button>--%>
                        <asp:LinkButton ID="btnBackPage" CssClass="d-flex align-items-center justify-content-center btn btn-back" runat="server" PostBackUrl="~/Views/Cursos/MenuCourses.aspx">
                    <span class="glyphicon glyphicon-arrow-left" style="margin-top: 5px;"></span>
                        </asp:LinkButton>
                    </div>
                    <div class="col-md-2">
                        <h1 style="font-size: 45px; font-family: 'Century Gothic'; font-weight: bolder;">Periodo</h1>
                    </div>
                   <%-- <div id="menu-actions-courses" class="col-md-6 col-md-offset-2" style="border: 1px solid black; border-width: 1px 0px 1px 0px; margin-top: 35px; display: flex; justify-content: space-between;">
                        <h6><a href="Search.aspx">Busqueda</a></h6>
                        <h6><a href="Reprogramming.aspx">Reprogramaciones</a></h6>
                        <h6><a href="CoursesReports.aspx">Reportes</a></h6>
                        <h6><a>Periodo</a></h6>
                        <h6><a href="ProgramSchedule.aspx">Calendario</a></h6>
                    </div>--%>
                </div>
            </div>
            <div class="col-xs-4 col-xs-offset-4" style="margin-top: 4%;">
                <asp:Label ID="lavel" runat="server" Text=""></asp:Label>
                <table class="table table-hover">
                    <tr>
                        <td colspan="2">
                            <label for="s" style="font-size: 1.3em;">Periodo de inscripciones</label></td>
                    </tr>
                    <tr>
                        <td>
                            <label>Inicio</label></td>
                        <td>
                            <asp:TextBox ID="dpInicioInscripciones" runat="server" TextMode="Date" CssClass="form-control" autocomplete="off"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            <label>Cierre</label></td>
                        <td>
                            <input type="date" runat="server" id="dpFinInscripciones" name="valid_to" class="form-control" value="" autocomplete="off" /></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btnModificarInscripciones" runat="server" Text="Modificar" CssClass="btn btn-success" Style="width: 100%; margin-top: 5px;" OnClick="btnModificarInscripciones_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="col-xs-4 col-xs-offset-4">
                <table class="table table-hover">
                    <tr>
                        <td colspan="2">
                            <label for="dpInicioInscripciones" style="font-size: 1.3em;">Periodo de cursos</label></td>
                    </tr>
                    <tr>
                        <td>
                            <label>Inicio</label></td>
                        <td>
                            <input type="date" runat="server" id="dpInicioCursos" name="valid_to" class="form-control" value="2018-01-01" autocomplete="off" /></td>
                    </tr>
                    <tr>
                        <td>
                            <label>Cierre</label></td>
                        <td>
                            <input type="date" runat="server" id="dpFinCursos" name="valid_to" class="form-control" value="2018-01-01" autocomplete="off" /></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btnModificarCursos" runat="server" Text="Modificar" CssClass="btn btn-success" Style="width: 100%; margin-top: 5px;" OnClick="btnModificarCursos_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div>
            <asp:Label ID="lblMensaje" runat="server" Text="" CssClass="" Style="font-size: 1.3em;"></asp:Label>
        </div>
        <div class="col col-xs-1 col-xs-offset-2">
            <%--<asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="btn btn-warning" OnClick="btnRegresar_Click" />--%>
        </div>
    </form>
</asp:Content>
