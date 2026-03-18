<%@ Page Title="" Language="C#" MasterPageFile="~/_Layout.Master" AutoEventWireup="true" CodeBehind="SessionAttendance.aspx.cs" Inherits="SIE_KEY_USER.Cursos.Attendance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Content/cursos.css" rel="stylesheet" />

    <script type="text/javascript">
        function getTakenList() {

            document.getElementById("<%=buttonForAttendance.ClientID%>").style.display = "none";

            var checkbxs = document.getElementsByName("verifyEnrolled");

            var list = "";         

            for (var i = 0; i < checkbxs.length; i++) {
                if (checkbxs[i].checked) {
                    list += checkbxs[i].value + ",1" + ';';
                }
                else {
                    list += checkbxs[i].value + ",0" + ';';
                }
            }

            list = list.slice(0, -1);

            $.ajax({
                type: "POST",
                url: "SessionAttendance.aspx/takeList", // Update with your actual page and method name
                data: JSON.stringify({ sessionList: list }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    //console.log(response.d)
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

        }
    </script>

    <form runat="server">
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-12">
                    <div class="col-md-2">
                        <asp:LinkButton ID="btnBackPage" CssClass="d-flex align-items-center justify-content-center btn btn-back" runat="server" PostBackUrl="javascript:history.go(-1);">
    <span class="glyphicon glyphicon-arrow-left" style="margin-top: 5px;"></span>
                        </asp:LinkButton>
                    </div>
                    <div class="col-md-2">
                        <h1 style="font-size: 45px; font-family: 'Century Gothic'; font-weight: bolder;">Asistencia</h1>
                    </div>

                </div>
            </div>
            <div class="row">
                <div class="col-md-2"></div>
                <div id="SessionInfoContainer" runat="server" class="col-md-8">
                </div>
                <div class="col-md-2"></div>
            </div>
            <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-8">
                    <hr />
                </div>
                <div class="col-md-2"></div>
            </div>
            <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-8" style="height: 450px; overflow-y: auto;">
                    <table class="table table-striped" id="attendance-table">
                        <thead>
                            <tr>
                                <th scope="col">#</th>
                                <th scope="col">No. Empleado</th>
                                <th scope="col">Nombres</th>
                                <th scope="col">Apellidos</th>
                                <th scope="col">Asistio
                                    <input type="checkbox" onclick="CheckAll(this);" />
                                </th>
                            </tr>
                        </thead>
                        <tbody id="tableAttendees" runat="server">
                        </tbody>
                    </table>
                </div>

            </div>
            <div class="row" style="margin-top: 30px;">
                <div class="col-md-2"></div>
                <div class="col-md-8" style="display: flex; justify-content: center; flex-flow: row">
                    <asp:Button ID="buttonForAttendance" CssClass="btn btn-success" Text="Registrar Asistencia" runat="server" OnClientClick="getTakenList()" OnClick="registerAttendence"/>
                </div>
                <div class="col-md-2"></div>
            </div>
        </div>
    </form>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_JS" runat="server">
    <script>
        function CheckAll(oCheckBox) {
            var table = document.getElementById("attendance-table");

            for (i = 1; i < table.rows.length; i++) {
                if (oCheckBox.checked) {
                    table.rows[i].cells[4].getElementsByTagName("INPUT")[0].checked = true;
                }
                else {
                    table.rows[i].cells[4].getElementsByTagName("INPUT")[0].checked = false;
                }
                
            }
        }
    </script>
</asp:Content>
