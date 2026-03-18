<%@ Page Title="" Language="C#" MasterPageFile="~/_Layout.Master" AutoEventWireup="true" CodeBehind="Sessions.aspx.cs" Inherits="SIE_KEY_USER.Cursos.Sessions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Content/cursos.css" rel="stylesheet" />
    <script type="text/javascript">

        function unenroll(id_employee) {

            document.getElementById("<%=pageDocument.ClientID%>").style.opacity = "0.5";
            document.getElementById("<%=btnAttendance.ClientID%>").disabled = true;
            document.getElementById("gradesBtn").disabled = true;

            var unEnrollBtns = document.getElementsByName("unenrollButtons");
            var detailsEmpBtns = document.getElementsByName("detailsEmpButton");

            for (var i = 0; i < unEnrollBtns.length; i++) {
                document.getElementById(unEnrollBtns[i].id).style.zIndex = "-100";                
                document.getElementById(detailsEmpBtns[i].id).style.zIndex = "-100";                
            }


            console.log(id_employee);

            $.ajax({
                type: "POST",
                url: "Sessions.aspx/unEnrollFromSessAjax", // Update with your actual page and method name
                data: JSON.stringify({ idEmploy: id_employee }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    //console.log(response.d)
                    //JSON.parse(response.d)
                    alert(response.d)
                },
                error: function () {
                    // Error occurred during task start
                    // You can handle the error or show an error message to the user
                    alert('Error al intentar completar la operacion')

                },
                completed: function () {
                    console.log('completed')
                }
            });

            location.reload();
        }

    </script>
    <form id="pageDocument" runat="server">
        <div class="container-fluid">
            <div class="row" style="margin-bottom: 50px;">
                <div class="col-md-12">
                    <div class="col-md-1">
                        <asp:LinkButton ID="btnBackPage" CssClass="d-flex align-items-center justify-content-center btn btn-back" runat="server" OnClick="goBack">
                            <span class="glyphicon glyphicon-arrow-left" style="margin-top: 5px;"></span>
                        </asp:LinkButton>
                    </div>
                    <div class="col-md-2" style="display: flex; justify-content: space-between; margin-top: 15px;">
                        <p style="font-size: 45px; font-family: 'Century Gothic'; font-weight: bolder;">Sesion</p>
                        <p id="DivSubtitleSession" runat="server" style="font-size: 25px; font-family: 'Century Gothic'; font-weight: lighter; margin-top: 20px; padding-left: 20px"></p>
                    </div>
                    <%--<div id="menu-actions-courses" class="col-md-6 col-md-offset-2" style="border: 1px solid black; border-width: 1px 0px 1px 0px; margin-top: 35px; display: flex; justify-content: space-between;">
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
                <div class="col-md-offset-1 col-md-10">
                    <div class="col-md-2 " style="display: inline-flex;">
                        <p style="font-size: 15px; font-family: 'Century Gothic'; font-weight: lighter;">
                            Sesion:
                            <asp:Label ID="LblSession" runat="server" Font-Size="15px" font-family="'Century Gothic'" />
                        </p>
                        <%--</div>
                    <div class="col-md-2 text-left">--%>
                        <%-- <div class="dropdown">
                            <button class="btn btn-default dropdown-toggle" type="button" id="dropdownMenu1" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
                                Dropdown
                                <span class="caret"></span>
                            </button>
                            <ul class="dropdown-menu" aria-labelledby="dropdownMenu1">
                                <li><a href="#">Action</a></li>
                                <li><a href="#">Another action</a></li>
                                <li><a href="#">Something else here</a></li>
                            </ul>
                        </div>--%>
                    </div>
                    <div class="col-md-3">
                        <p style="font-size: 15px; font-family: 'Century Gothic'; font-weight: lighter;">
                            Curso:
                            <asp:Label ID="LblCurso" runat="server" Font-Size="15px" font-family="'Century Gothic'" />
                        </p>
                    </div>
                    <div class="col-md-2">
                        <p style="font-size: 15px; font-family: 'Century Gothic'; font-weight: lighter;">
                            Fecha y Horario:
                            <asp:Label ID="LblDateTime" runat="server" Font-Size="15px" font-family="'Century Gothic'" />
                        </p>

                    </div>
                    <div class=" col-md-2">
                        <p style="font-size: 15px; font-family: 'Century Gothic'; font-weight: lighter;">
                            Status:
                            <asp:Label ID="LblStatus" runat="server" Font-Size="15px" font-family="'Century Gothic'" />
                        </p>


                    </div>

                </div>
            </div>
            <div class="col-md-offset-1 col-md-10">
                <div class="col-md-9">
                    <hr />
                </div>
            </div>
            <div class="row">
                <div class="col-md-offset-1 col-md-10">
                    <div class="col-md-9" style="height: 300px; overflow-y: auto;">
                        <table class="table table-striped">
                            <thead>
                                <tr>
                                    <th scope="col">Numero de empleado</th>
                                    <th scope="col">Nombre</th>
                                    <th scope="col">Fecha Inscripcion</th>
                                    <th scope="col">Actions</th>
                                </tr>
                            </thead>
                            <tbody id="DivTableBody" runat="server">
                            </tbody>
                        </table>

                    </div>
                    <div class="col-md-3 text-start text-left" style="border-left: 1px solid black; border-right: 1px solid black;">
                        <asp:Label Text="Impartido por" runat="server" Font-Bold="true" />
                        <asp:TextBox ID="TxtImpartedBy" CssClass="form-control transparent-background" runat="server" Text="Imparted By" disabled="true" />

                        <asp:Label Text="Fecha" runat="server" Font-Bold="true" />
                        <asp:TextBox ID="TxtDate" CssClass="form-control transparent-background" runat="server" Text="Date" disabled="true" />

                        <asp:Label Text="Lugar" runat="server" Font-Bold="true" />
                        <asp:TextBox ID="TxtPlace" CssClass="form-control transparent-background" runat="server" Text="Place" disabled="true" />

                        <asp:Label Text="No. Inscritos" runat="server" Font-Bold="true" />
                        <asp:TextBox ID="TxtEnrolled" CssClass="form-control transparent-background" runat="server" Text="Enrolled" disabled="true" />

                        <asp:Label Text="Cupo" runat="server" Font-Bold="true" />
                        <asp:TextBox ID="TxtSpce" CssClass="form-control transparent-background" runat="server" Text="Space" disabled="true" />

                        <asp:Label Text="Lugares Disponibles" runat="server" Font-Bold="true" />
                        <asp:TextBox ID="TxtAvailable" CssClass="form-control transparent-background" runat="server" Text="Available" disabled="true" />

                        <%--<button type="button" class="btn btn-success" style="margin-top: 15px;">Cerrar Sesion</button>--%>
                        <button id="gradesBtn" type="button" class="btn btn-success" style="margin-top: 15px;display:none">Calificaciones</button>
                    </div>

                </div>
            </div>
            <div class="row">
                <div class="col-md-10">
                    <div class="justify-content-center">
                        <asp:Button ID="btnAttendance" Text="Asistencia" runat="server" OnClick="btnAttendance_Click" CssClass="btn btn-success" />
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_JS" runat="server">
</asp:Content>
