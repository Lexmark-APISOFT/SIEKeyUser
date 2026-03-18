<%@ Page Title="" Language="C#" MasterPageFile="~/_Layout.Master" AutoEventWireup="true" CodeBehind="EnrolledEmployee.aspx.cs" Inherits="SIE_KEY_USER.Views.Cursos.EnrolledEmployee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <form runat="server">

        <div class="container-fluid">
            <div class="row">
                <div class="col-md-12">
                    <div class="col-md-1">
                        <%--<button onclick="" class="d-flex align-items-center justify-content-center btn" style="">
                            <span class="glyphicon glyphicon-arrow-left" style="margin-top: 5px;"></span>
                        </button>
                            PostBackUrl="~/Views/Cursos/Search.aspx--%>
                        <asp:LinkButton ID="btnBackPage" CssClass="d-flex align-items-center justify-content-center btn btn-back" runat="server" OnClick="goBack">
                            <span class="glyphicon glyphicon-arrow-left" style="margin-top: 5px;"></span>
                        </asp:LinkButton>
                    </div>
                    <div class="col-md-2">
                        <h1 style="font-size: 45px; font-family: 'Century Gothic'; font-weight: bolder;">Empleado</h1>
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
            <div class-="row" style="margin-top: 30px;">
                <div class="col-md-1"></div>
                <div class="col-md-10">
                    <div id="EmployeeInfo" class="col-md-3 text-start text-left" style="border-right: 1px solid black;">
                        <asp:Label Text="No. Empleado" runat="server" Font-Bold="true" />
                        <asp:TextBox ID="TxtEmployeeNo" CssClass="form-control transparent-background" runat="server" Text="EmployeeNo" disabled="true" />
                        <asp:Label Text="Nombres" runat="server" Font-Bold="true" />
                        <asp:TextBox ID="TxtNames" CssClass="form-control transparent-background" runat="server" Text="Names" disabled="true" />
                        <asp:Label Text="Apellidos" runat="server" Font-Bold="true" />
                        <asp:TextBox ID="TxtLastName" CssClass="form-control transparent-background" runat="server" Text="LastName" disabled="true" />
                        <asp:Label Text="Puesto" runat="server" Font-Bold="true" />
                        <asp:TextBox ID="TxtPosition" CssClass="form-control transparent-background" runat="server" Text="Position" disabled="true" />
                        <asp:Label Text="Ultimo cambio de puesto" runat="server" Font-Bold="true" />
                        <asp:TextBox ID="TxtLastPosition" CssClass="form-control transparent-background" runat="server" Text="Last Position" disabled="true" />
                        <hr />
                        <br />
                        <h4 style="font-size: 20px; font-family: 'Century Gothic'; font-weight: lighter;">Cursos Pendientes de Inscripcion</h4>
                        <div id="DivCorrespondingCourses" runat="server">
                            
                        </div>
                    </div>
                    <div id="EnrolledCourses" class="col-md-9">
                        <div class="row text-left">
                            <h4 style="margin-left: 15px; font-size: 20px; font-family: 'Century Gothic'; font-weight: lighter;">Reprogramaciones</h4>
                        </div>
                        <div class="row">
                            <div class="col-md-12" style="margin-left: 10px; height: 250px; overflow: auto;">
                                <table class="table table-striped">
                                    <thead>
                                        <tr>
                                            <th scope="col">Curso</th>
                                            <th scope="col">No.Folio anterior</th>
                                            <th scope="col">Fecha</th>
                                            <th scope="col">No.Folio Nuevo</th>
                                            <th scope="col">Razon</th>
                                            <th scope="col">Status</th>
                                            <th scope="col">Ultima Actualizacion</th>
                                        </tr>
                                    </thead>
                                    <tbody id="ReprogrammingTable" runat="server">
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <hr />
                        <div class="row">
                            <div class="col-md-3">
                                <h4 class="text-left" style="margin-left: 20px; font-size: 20px; font-family: 'Century Gothic'; font-weight: lighter;">Sesiones Activas</h4>
                            </div>
                            <div class="col-md-8">
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-md-12" id="ActiveSesionsContainer" runat="server">
                            </div>
                        </div>
                    </div>
                    <div class="col-md-1"></div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h3 class="modal-title" id="exampleModalLabel"></h3>
                    </div>
                    <div class="modal-body" style="height:100px;">
                        <div class="container-fluid" style="height: 100%;">
                            <div class="row" id="modalContent" runat="server" style="height:100%;">
                                <select name="sessiones" id="sessionOfSelecCourse" class="ddModalSession">
                                </select>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnAccept" CssClass="btn btn-primary" Text="Aceptar" runat="server" AutoPostBack="false" OnClientClick="enrollUnenrollOrReprogram()"/>
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_JS" runat="server">
    <link href="../../Content/cursos.css" rel="stylesheet" />
    <script type="text/javascript">
        var idOfButtonWithCourseANDSession;
        var IdOfunEnrollButton;
        var typeOfButton;
        const referrerPage = document.referrer;
        var startTime = new Date().getTime();
        window.onload = function () {
            var endTime = new Date().getTime();
            var pageLoadTime = endTime - startTime;
            console.log("Tiempo de carga de la página: " + pageLoadTime + " milisegundos");
        };
        //this.document.querySelectorAll(".btn-danger").forEach((button) => {
        //    button.addEventListener("click", (event) => {
        //        let sessionId = event.target.id;



        //    });
        //});

        //this.document.querySelectorAll(".btn-default").forEach((btnAddSession) =>
        function unenrollEmployeePop(id,name) {
            typeOfButton = name;
            IdOfunEnrollButton = id;
            document.getElementById('exampleModalLabel').innerHTML = 'Desea dar de baja esta sesion?';
            document.getElementById('sessionOfSelecCourse').style.display = "none";
            console.log(sessionId);
        }


        function loadSessionsPendingCourse(id, name) {

            console.log('inscripcion');
            let courseId;
            //btnAddSession.addEventListener("click", (event) => {
            if (name == "botonReprogramar") {
                courseId = id.split('-')[1];
            }
            else {
                courseId = event.target.id;
            }
            console.log(courseId)
            document.getElementById('exampleModalLabel').innerHTML = courseId + ' - Seleccione la sesion a la que desea inscribirse';
            $.ajax({
                type: "POST",
                url: "EnrolledEmployee.aspx/AvailableSession", // Update with your actual page and method name
                data: JSON.stringify({ CourseID: courseId }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    //console.log(response.d)
                    //JSON.parse(response.d)
                    addItemsToSessionList(JSON.parse(response.d));
                },
                error: function () {
                    // Error occurred during task start
                    // You can handle the error or show an error message to the user
                    alert('No fue posible buscar las sesiones')

                },
                completed: function () {
                    console.log('completed')
                }
            });

            idOfButtonWithCourseANDSession = id;
            typeOfButton = name;

        }
            
            //});
        //});

        function addItemsToSessionList(arraySessions) {

            var selectSess = document.getElementById('sessionOfSelecCourse');

            if (arraySessions.length < 1) {

                var optn = document.createElement('option');
                optn.text = "No hay sesiones aplicables";
                optn.value="N/A"
                selectSess.appendChild(optn);
            }
            else {

                for (var i = 0; i < arraySessions.length; i++)
                {
                    var optn = document.createElement('option');
                    var date = arraySessions[i].Date.split('T')[0];
                    var day = date.split('-')[2];
                    var month = date.split('-')[1];
                    var year = date.split('-')[0];

                    var hrInicio = arraySessions[i].Time.split(' ')[0] + ' ' + arraySessions[i].Time.split(' ')[1];
                    var hrFinal = arraySessions[i].Time.split(' ')[2] + ' ' + arraySessions[i].Time.split(' ')[3];
                    optn.value = arraySessions[i].SessionID;
                    optn.text = "Session No. " + arraySessions[i].SessionID + ", Fecha y Hora: " + day + '/' + month + '/' + year + " De: " + hrInicio + " a " + hrFinal + ", Cupo: " + arraySessions[i].Space;
                    selectSess.appendChild(optn);

                }
            }
                        
        }

        $(document).on('hidden.bs.modal', '#exampleModal', function () {
            var selectSess = document.getElementById('sessionOfSelecCourse');
            selectSess.innerHTML = '';
        });

        function enrollUnenrollOrReprogram() {
            //btnAcceptEnnroll_Click->funcion de webmethod
            if (typeOfButton == "botonBaja") {

                $.ajax({
                    type: "POST",
                    url: "EnrolledEmployee.aspx/UnenrollEmployee", // Update with your actual page and method name
                    data: JSON.stringify({ SessionID: IdOfunEnrollButton }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        alert(response.d);
                    },
                    error: function () {
                        // Error occurred during task start
                        // You can handle the error or show an error message to the user
                        alert('Ocurrio un error al intentar dar de baja')
                    },
                });
            }
            else {
                var ddlSessions = document.getElementById('sessionOfSelecCourse');
                var selectedSess = ddlSessions.options[ddlSessions.selectedIndex].value;
                console.log(selectedSess);

                if (selectedSess != "N/A") {
                    if (typeOfButton == "botonInscribir") {
                        $.ajax({
                            type: "POST",
                            url: "EnrolledEmployee.aspx/btnAcceptEnnroll", // Update with your actual page and method name
                            data: JSON.stringify({ id_sessn: selectedSess }),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (response) {
                                alert(response.d);
                            },
                            error: function () {
                                // Error occurred during task start
                                // You can handle the error or show an error message to the user
                                alert('No fue posible generar la inscripcion a la sesion')

                            },
                            completed: function () {
                                alert('finished')
                            }
                        });
                    }
                    else if (typeOfButton == "botonReprogramar") {
                        idCourseANDSession = idOfButtonWithCourseANDSession.split('-');
                        $.ajax({
                            type: "POST",
                            url: "EnrolledEmployee.aspx/btnAcceptReprogramming", // Update with your actual page and method name
                            data: JSON.stringify({ newSess: selectedSess, prevSess: idCourseANDSession[0], courseCod: idCourseANDSession[1] }),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (response) {
                                alert(response.d);
                            },
                            error: function () {
                                // Error occurred during task start
                                // You can handle the error or show an error message to the user
                                alert('No fue posible generar la reprogramacion de la sesion')

                            },
                            completed: function () {
                                alert('finished')
                            }
                        });
                    }

                }

            }

        }

        //Logout clears all visited pages for Back Button
        function backPage() {
            var backCount = window.history.length + 1;

            window.history.go(-1);
        }

        

    </script>
</asp:Content>
