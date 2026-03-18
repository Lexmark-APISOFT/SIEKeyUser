<%@ Page Title="" Language="C#" MasterPageFile="~/_Layout.Master" AutoEventWireup="true" MaintainScrollPositionOnPostBack="true" CodeBehind="MenuCourses.aspx.cs" Inherits="SIE_KEY_USER.Cursos.Cursos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="../../Content/cursos.css" rel="stylesheet" />
        <form runat="server">
            <div class="container-fluid">
            <div class="row">
                    <div class="col-md-1">
                        <asp:LinkButton ID="btnBackPage" CssClass="d-flex align-items-center justify-content-center btn btn-back" runat="server" OnClick="btnBackPage_Click"> <%--PostBackUrl="javascript:history.go(-1);"--%>
                            <span class="glyphicon glyphicon-arrow-left" style="margin-top: 5px;"></span>
                        </asp:LinkButton>
                    </div>
                
                <div class="col-md-12">
                    <div class="col-md-offset-1 col-md-2">
                        <h1 style="font-size: 45px; font-family: 'Century Gothic'; font-weight: bolder;">Cursos</h1>
                    </div>
                    <div id="menu-actions-courses" class="col-md-6 col-md-offset-2" style="border: 1px solid black; border-width: 1px 0px 1px 0px; margin-top: 35px; display: flex; justify-content: space-between;">
                        <h6><a href="MenuCourses.aspx">Inicio</a></h6>
                        <h6><a href="Search.aspx">Busqueda</a></h6>
                        <h6><a href="Reprogramming.aspx">Reprogramaciones</a></h6>
                        <h6><a href="CoursesReports.aspx">Reportes</a></h6>
                        <h6><a href="../Cursos_regulatorios_modificar_periodo.aspx">Periodo</a></h6>
                        <h6><a href="ProgramSchedule.aspx">Calendario</a></h6>
                    </div>
                </div>
            </div>
            <div class="row" style="margin-bottom: 25px;">
                <div id="filter-applied-container" class="col-md-offset-1 col-md-8" style="margin-top: 50px; display: flex;">
                    <%--<div id="filter-applied-item" style="border: 1px solid black; display: inline-block; margin: 5px; padding: 5px; border-radius: 50px;">
                        <div style="display: flex; align-items: center;">
                            <span style="margin-right: 10px;">Puesto</span>
                            <span class="glyphicon glyphicon-remove" style="margin-top: 2px;"></span>
                        </div>
                    </div>--%>

                </div>
                <div id="apply-filters" class="col-md-offset-1 col-md-1" style="margin-top: 50px;">
                    <a>
                        <%--<p style="font-family: 'Century Gothic'; margin-top: 15px;" onclick="openNav()">Filtros ( 0 )</p>--%>
                    </a>
                </div>
                <div class="col-md-offset-1 col-md-10">
                    <hr />
                </div>
            </div>

            <div class="row">
                <div id="CoursesCardContainer" class="col-md-offset-1 col-md-10" runat="server">
                </div>
                <div id="popup_window" runat="server" class="popup-CourseDetails" style="overflow:scroll" visible="false">
                    <div style="position:relative;display:flex;flex-direction:row;flex-wrap: wrap;border-bottom: 2px solid;width:100%;height:auto;">
                        <div style="width:90%; display:flex;align-items:center">
                            <p style="font-size:60px;float:right;font-size-adjust: 0.5;">Detalles</p>
                        </div>
                        <div style="width:10%; display:flex">
                            <asp:LinkButton CssClass="btn btn-success btnBack" style="display:flex;justify-content:center;align-items:center;margin-left:5%;width:80%;height:70%;background-color:black;border-radius:100%;font-size:min(3em, 3vh)" runat="server" OnClick="closeDatails_Click">
                                <i class="glyphicon glyphicon-remove" aria-hidden="true" style=" position:relative"></i>
                            </asp:LinkButton>
                        </div>
                    </div>
                    <div id="detailsP" runat="server" style="position:relative;display:flex;flex-direction:row;flex-wrap: wrap;border-bottom: 2px solid;width:100%;height:auto;">
                        <p class="popupCourseDetailsPs" id="CodigoCur" runat="server"></p>
                        <p class="popupCourseDetailsPs" id="NombreCur" runat="server"></p>
                        <p class="popupCourseDetailsPs" id="ClasifCur" runat="server"></p>
                        <p class="popupCourseDetailsPs" id="ClaseCur" runat="server"></p>
                    </div>
                </div>  
            </div>
        </div>
        </form>  

        <div id="mySidenav" class="sidenav" style="border-right: 1px solid black;">
            <a href="javascript:void(0)" class="closebtn" onclick="closeNav()">&times;</a>
            <div id="filterOptionsContainer" runat="server">
                <div class="dropdown">
                    <button id="changeButton" class="dropdown-btn" style="background-color: none;">
                        <p style="font-size: 20px; font-family: 'Century Gothic'; margin-top: 15px; font-weight: bold;">Clasificacion&nbsp;&nbsp;<span id="glyphiconSpan" style="font-size:15px;"class="glyphicon glyphicon-chevron-right"></span> </p>
                    </button>
                    <div class="dropdown-content">
                        <label style="font-family: 'Century Gothic'; font-weight:lighter;">Hola&nbsp;&nbsp;<input type="checkbox" name="checkbox" value="value" /></label>
                        <label style="font-family: 'Century Gothic'; font-weight:lighter;">Hola&nbsp;&nbsp;<input type="checkbox" name="checkbox" value="value" /></label>
                        <label style="font-family: 'Century Gothic'; font-weight:lighter;">Hola&nbsp;&nbsp;<input type="checkbox" name="checkbox" value="value" /></label>
                        <label style="font-family: 'Century Gothic'; font-weight:lighter;">Hola&nbsp;&nbsp;<input type="checkbox" name="checkbox" value="value" /></label>
                        <label style="font-family: 'Century Gothic'; font-weight:lighter;">Hola&nbsp;&nbsp;<input type="checkbox" name="checkbox" value="value" /></label>

                    </div>
                </div>
            </div>
            <button class="btn btn-success" style="background-color:#086814 !important;">Aplicar Filtros</button>
        </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_JS" runat="server">
    <script>
        function openNav() {
            document.getElementById("mySidenav").style.width = "250px";
        }

        function closeNav() {
            document.getElementById("mySidenav").style.width = "0";
        }
    </script>
    <script>
        // Obtener todos los botones de los dropdown
        const dropdownButtons = document.querySelectorAll(".dropdown-btn");

        // Agregar un evento click a cada botón
        dropdownButtons.forEach((button) => {
            button.addEventListener("click", () => {
                // Ocultar todas las opciones de los otros dropdowns
                document.querySelectorAll(".dropdown-content").forEach((content) => {
                    if (content !== button.nextElementSibling) {
                        content.style.display = "none";
                    }
                });

                // Alternar la visibilidad de las opciones del dropdown actual
                const content = button.nextElementSibling;
                if (content.style.display === "block") {
                    content.style.display = "none";
                } else {
                    content.style.display = "block";
                }
            });
        });
        var glyphiconSpan = document.getElementById("glyphiconSpan");
        var changeButton = document.getElementById("changeButton");

        // Add a click event listener to the button
        changeButton.addEventListener("click", function () {
            // Check the current class of the glyphicon span
            if (glyphiconSpan.classList.contains("glyphicon-chevron-down")) {
                // If it has the 'glyphicon-star' class, change it to 'glyphicon-heart'
                glyphiconSpan.classList.remove("glyphicon-chevron-down");
                glyphiconSpan.classList.add("glyphicon-chevron-right");
            } else {
                // If it has the 'glyphicon-heart' class, change it back to 'glyphicon-star'
                glyphiconSpan.classList.remove("glyphicon-chevron-right");
                glyphiconSpan.classList.add("glyphicon-chevron-down");
            }
        });
    </script>
</asp:Content>
