<%@ Page Title="" Language="C#" MasterPageFile="~/_Layout.Master" AutoEventWireup="true" CodeBehind="MenuSessions.aspx.cs" Inherits="SIE_KEY_USER.Views.Cursos.MenuSessions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


    <form runat="server">
        <div class="container-fluid">
            <div class="row" style="margin-bottom: 50px;">
                <div class="col-md-12">
                    <div class="col-md-1">
                        <asp:LinkButton ID="btnBackPage" CssClass="d-flex align-items-center justify-content-center btn btn-back" runat="server" OnClick="btnBackPage_Click">
                            <span class="glyphicon glyphicon-arrow-left" style="margin-top: 5px;"></span>
                        </asp:LinkButton>
                    </div>
                    <div class="col-md-3" style="display: flex; justify-content: space-between; margin-top: 15px;">
                        <p style="font-size: 45px; font-family: 'Century Gothic'; font-weight: bolder;">Sesiones</p>
                        <p id="DivSubtitleCourse" runat="server" style="font-size: 25px; font-family: 'Century Gothic'; font-weight: lighter; margin-top: 20px; padding-left: 20px"></p>
                    </div>
                   <%-- <div id="menu-actions-courses" class="col-md-6 col-md-offset-1" style="border: 1px solid black; border-width: 1px 0px 1px 0px; margin-top: 35px; display: flex; justify-content: space-between;">
                        <h6><a href="MenuCourses.aspx">Inicio</a></h6>
                        <h6><a href="Search.aspx">Busqueda</a></h6>
                        <h6><a href="Reprogramming.aspx">Reprogramaciones</a></h6>
                        <h6><a href="CoursesReports.aspx">Reportes</a></h6>
                        <h6><a href="../Cursos_regulatorios_modificar_periodo.aspx">Periodo</a></h6>
                        <h6><a href="ProgramSchedule.aspx">Calendario</a></h6>
                    </div>--%>
                </div>
            </div>

            <div class="row">
                <div class="col-md-1"></div>
                <div id="SessionCardContainer" class="col-md-10" runat="server">
                </div>
                <div class="col-md-1"></div>
            </div>
        </div>

    </form>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_JS" runat="server">
    <link href="../../Content/cursos.css" rel="stylesheet" />
    <script>
        function disablePage() {
            document.getElementById("<%=SessionCardContainer.ClientID%>").style.zIndex = "-100";
            document.getElementById("<%=SessionCardContainer.ClientID%>").style.opacity = "0.5";
        }
    </script>
</asp:Content>
