<%@ Page Title="" Language="C#" MasterPageFile="~/_Layout.Master" AutoEventWireup="true" CodeBehind="MenuKey.aspx.cs" Inherits="SIE_KEY_USER.Views.MenuKey" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<script type="text/javascript">
        $(document).ready(function () {

            $('#<%= LinkButton3.ClientID %>').click(function () {

                $('#Content1').block({
                    //mainDiv
                    message: '<h3>Estamos obteniendo sus datos, un momento por favor...</h3>'
                    , css: { border: '3px solid #ffffff' }

                });
                LinkButton3.setAttribute('disabled', 'disabled');
            });


        });

    </script>--%>
    <form id="menukey_form" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Path="~/Scripts/click_bttn/click_btnn.js" />
            </Scripts>
        </asp:ScriptManager>

        <audio id="audio" src="../Content/Cick.wav" ></audio>

        <asp:Label ID="nombre" ClientIDMode="Static" runat="server" Text="" ></asp:Label>
        
        <div id="center_menu_key">

            <table id="tb_menu_key">
                <tr>
                    <td id="td1" runat="server">
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" OnClientClick="PlaySound(); return true" >
                            <img id="img1" src="../Images/MenuKey/desbloqueo.png" class="img_menu" runat="server" />
                            <img id="img1_1" src="../Images/MenuKey/no diponible/desbloqueo.png" class="img_menu" runat="server" visible="false" />
                        </asp:LinkButton></td>
                    <td id="td2" runat="server">
                        <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click" OnClientClick="PlaySound(); return true" >
                            <img id="img2" src="../Images/MenuKey/prestamos.png" class="img_menu" runat="server" />
                            <img id="img2_2" src="../Images/MenuKey/no diponible/prestamos.png" class="img_menu" runat="server" visible="false" />
                        </asp:LinkButton>

                    </td>
                </tr>
                <tr>
                    <td id="td3" runat="server">
                        <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click" OnClientClick="PlaySound(); return true" >
                            <img id="img3" src="../Images/MenuKey/actualizacion.png" class="img_menu" runat="server" />
                            <img id="img3_3" src="../Images/MenuKey/no diponible/actualizacion.png" class="img_menu" runat="server" visible="false" />
                        </asp:LinkButton></td>
                    <td id="td4" runat="server">
                        <asp:LinkButton ID="LinkButton4" runat="server" OnClientClick="PlaySound(); return true" OnClick="LinkButton4_Click" >
                            <img id="img4" src="../Images/MenuKey/reembolso_escolar.png" class="img_menu" runat="server" />
                            <img id="img4_4" src="../Images/MenuKey/no diponible/reembolso_escolar.png" class="img_menu" runat="server" visible="false" />
                        </asp:LinkButton></td>
                </tr>
                <tr>
                    <td id="td5" runat="server">
                        <asp:LinkButton ID="LinkButton5" runat="server" OnClick="LinkButton5_Click" OnClientClick="PlaySound(); return true" >
                            <img id="img5" src="../Images/MenuKey/vacaciones.png" class="img_menu" runat="server" />
                            <img id="img5_5" src="../Images/MenuKey/no diponible/vacaciones.png" class="img_menu" runat="server" visible="false" />
                        </asp:LinkButton></td>
                    <td id="td6" runat="server">
                        <asp:LinkButton ID="LinkButton6" runat="server" OnClick="LinkButton6_Click" >
                            <img id="img6" src="../Images/MenuKey/catalogos.png" class="img_menu" runat="server" />
                            <img id="img6_6" src="../Images/MenuKey/no diponible/catalogos.png" class="img_menu" runat="server" visible="false" />
                        </asp:LinkButton></td>
                </tr>
                <tr>
                    <td id="td7" runat="server">
                        <asp:LinkButton ID="LinkButton7" runat="server" OnClick="LinkButton7_Click" OnClientClick="PlaySound(); return true" >
                            <img id="img7" src="../Images/MenuKey/agregar_familiares.png" class="img_menu" runat="server" />
                            <img id="img7_7" src="../Images/MenuKey/no diponible/agregar_familiares.png" class="img_menu" runat="server" visible="false" />
                        </asp:LinkButton></td>
                    <td id="td8" runat="server">
                        <asp:LinkButton ID="LinkButton8" runat="server" OnClick="LinkButton8_Click" OnClientClick="PlaySound(); return true" >
                            <img id="img8" src="../Images/MenuKey/opciones_kiosko.png" class="img_menu" runat="server" />
                            <img id="img8_8" src="../Images/MenuKey/no diponible/opciones_kiosko.png" class="img_menu" runat="server" visible="false" />
                        </asp:LinkButton></td>
                </tr>
                <tr>
                    <td id="td9" runat="server">
                        <asp:LinkButton ID="LinkButton9" runat="server" OnClick="LinkButton9_Click" OnClientClick="PlaySound(); return true" >
                            <img id="img9" src="../Images/MenuKey/reimpresion_cartas.png" class="img_menu" runat="server" />
                            <img id="img9_9" src="../Images/MenuKey/no diponible/reimpresion_cartas.png" class="img_menu" runat="server" visible="false" />
                        </asp:LinkButton></td>
                    <td id="td10" runat="server">
                        <asp:LinkButton ID="LinkButton10" runat="server" OnClick="LinkButton10_Click" OnClientClick="PlaySound(); return true" >
                            <img id="img10" src="../Images/MenuKey/modificar_carta.png" class="img_menu" runat="server" />
                            <img id="img10_10" src="../Images/MenuKey/no diponible/modificar_carta.png" class="img_menu" runat="server" visible="false" />
                        </asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td id="td11" runat="server">
                        <asp:LinkButton ID="LinkButton11" runat="server" OnClick="LinkButton11_Click" OnClientClick="PlaySound(); return true" >
                            <img id="img11" src="../Images/MenuKey/actualizar_ubicacion.png" class="img_menu" runat="server" />
                            <img id="img11_11" src="../Images/MenuKey/no diponible/reimpresion_cartas.png" class="img_menu" runat="server" visible="false" />
                        </asp:LinkButton></td>
                    <td id="td16" runat="server">
                        <asp:LinkButton ID="LinkButton16" runat="server" OnClick="LinkButton16_Click" OnClientClick="PlaySound(); return true" >
                            <img id="img16" src="../Images/MenuKey/escanearSolicitudes.png" class="img_menu" runat="server" visible="false"/>
                            <img id="img16_16" src="~/Images/MenuKey/no diponible/escanearSolicitudes.png" class="img_menu" runat="server" visible="false" />
                        </asp:LinkButton></td>
                    <td id="td13" runat="server">
                        <asp:LinkButton ID="LinkButton13" runat="server" OnClick="LinkButton13_Click" OnClientClick="PlaySound(); return true" >
                            <img id="img12" src="../Images/MenuKey/Catalogo_de_certificaciones.png" class="img_menu" runat="server" />
                        </asp:LinkButton></td>
                    
                </tr>
                <tr>
                    <td id="td14" runat="server">
                        <asp:LinkButton ID="LinkButton14" runat="server" OnClick="LinkButton14_Click" OnClientClick="PlaySound(); return true" >
                            <img id="img15" src="../Images/MenuKey/cursos_regulatorios.png" class="img_menu" runat="server" />
                        </asp:LinkButton></td>
                   
                     <!--<td id="td15" runat="server">
                        <asp:LinkButton ID="LinkButton12" runat="server" OnClick="LinkButton15_Click" >
                            <img id="img13" src="../Images/MenuKey/vacaciones.png" class="img_menu" runat="server" />
                            <img id="img14" src="../Images/MenuKey/no diponible/modulo_vacaciones.png" class="img_menu" runat="server" visible="false" />
                        </asp:LinkButton></td>-->

                </tr>
                <%--<tr>
                    <td id="td11" runat="server" colspan="2">
                        <asp:LinkButton runat="server" ID="LinkButton11" OnClick="LinkButton11_Click" OnClientClick="PlaySound(); return true" >
                            <img id="img11" src="../Images/MenuKey/modificar_carta.png" class="img_menu" runat="server" />
                        </asp:LinkButton>

                    </td>
                </tr>--%>
            </table>

        </div>

        <asp:Button ID="Button1" runat="server" Text="Salir" CssClass="botonn" OnClick="Button1_Click" OnClientClick=" PlaySound(); return true" />

    </form>

</asp:Content>
