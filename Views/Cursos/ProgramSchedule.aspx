<%@ Page Title="" Language="C#" MasterPageFile="~/_Layout.Master" AutoEventWireup="true" CodeBehind="ProgramSchedule.aspx.cs" Inherits="SIE_KEY_USER.Views.ProgramSchedule" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <link href="../../Content/cursos.css" rel="stylesheet" />

    <form id="form1" runat="server">
        <style>
            body{
                background-image:none;
            }
            /* Estilo para el control Calendar */
            #<%= Calendar1.ClientID %> {
                width: 100%; /* Hace que el control ocupe toda la pantalla */
                font-family: Arial, sans-serif;
            }

            /* Estilo para las celdas */
            #<%= Calendar1.ClientID %> td {
                width: 14.28%; /* Divide el calendario en 7 columnas para una cuadrícula de 7 días */
                height: 100px; /* Altura de las celdas */
                text-align: right; /* Alinea el número en la esquina superior derecha */
                vertical-align: top; /* Alinea el número en la esquina superior derecha */
                padding: 5px;
                    border: 1px solid #ccc; 
            }

            /* Estilo para los números de día */
            #<%= Calendar1.ClientID %> td a {
                text-decoration: none;
                color: #333;
            }

            /* Estilo para los números de día en hover */
            #<%= Calendar1.ClientID %> td a:hover {
                background-color: #f0f0f0;
            }

            /* Estilo para los días del mes que no están en el mes actual */
            #<%= Calendar1.ClientID %> td.other-month {
                color: #ccc;
            }
        </style>
        <div class="container-fluid">
            <div id="popup_window" runat="server" class="popup-Details" visible="false">
                <div style="display:flex;flex-direction:row;flex-wrap: wrap;border-bottom: 2px solid;min-width:100%;min-height:10%;height:fit-content;">
                    <div style="min-width:90%; display:flex;align-items:center;height: 100%;">
                        <p id="titleDay" runat="server" style="font-size:30px;float:right;font-size-adjust: 0.5;"></p>
                    </div>
                    <div style="width:10%; display:flex;justify-content:center;align-items:center;height: 100%;">
                        <asp:LinkButton style="display:flex;justify-content:center;align-items:center;width:4vh;height:4vh;background-color:black;border-radius:100%;font-size:2vh" runat="server" OnClick="closePop" OnClientClick="">
                            <i class="glyphicon glyphicon-remove" aria-hidden="true" style="color:white"></i>
                        </asp:LinkButton>
                    </div>
                </div>
                <div id="detailsP" runat="server" class="detailsPSection">
                   
                    <%--<div class="divEachEventDetails">
                        <p class="popupDetailsPs" style="font-weight:600" id="courseSess" runat="server">17668 - Trabajos jdnjenbcoienenkxneuauauuaauauauauauauuauauaujqqhbbauqbaqu jijoles mano ya te pasastesssssssssss(187908)</p>
                        <p class="popupDetailsPs" style="font-size:min(30em, 1.5vw);margin-left:10%;color:gray;font-style:italic" id="dateEvent" runat="server">- Hoy</p>
                        <p class="popupDetailsPs" style="font-size:min(30em, 1.5vw);margin-left:10%;color:gray;font-style:italic" id="hrTohr" runat="server">- 12:00 - 1:00</p>
                        <p class="popupDetailsPs" style="font-size:min(30em, 1.5vw);margin-left:10%;color:gray;font-style:italic" id="room" runat="server">- Sala 2</p>
                    </div>--%>
                    
                </div>
            </div> 
            <div class="col-md-2">
                <div class="col-md-1">
                    <asp:LinkButton ID="btnBackPage" CssClass="d-flex align-items-center justify-content-center btn btn-back" runat="server" PostBackUrl="~/Views/Cursos/MenuCourses.aspx">
                        <span class="glyphicon glyphicon-arrow-left" style="margin-top: 5px;"></span>
                    </asp:LinkButton>
                    <table class="tableColors">
                    <tr>
                        <td style="background-color:#5f9ea0;width:50%;min-width:20px;"></td>
                        <td>Periodo de inscripciones</td>
                    </tr>
                    <tr>
                        <td style="background-color:#ff7f50;width:50%;min-width:20px;"></td>
                        <td>Inicio/Fin de cursos</td>
                    </tr>
                    <tr>
                        <td style="background-color:#fff1ae;width:50%;min-width:20px;"></td>
                        <td>Periodo de cursos</td>
                    </tr>
                </table>
                </div>
            </div>
            <div class="col-md-8">
                <asp:Calendar ID="Calendar1" runat="server" OnDayRender="Calendar1_DayRender" OnSelectionChanged="getEvents">

                </asp:Calendar>
                 
            </div>
            <div class="col-md-2">
               
            </div>
        </div>
    </form>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_JS" runat="server">
    <link href="../Content/Calendar.css" rel="stylesheet" />

    <script type="text/javascript">
var startTime = new Date().getTime();
window.onload = function () {
    var endTime = new Date().getTime();
    var pageLoadTime = endTime - startTime;
    console.log("Tiempo de carga de la página: " + pageLoadTime + " milisegundos");
};
    </script>
</asp:Content>
