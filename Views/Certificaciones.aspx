<%@ Page Title="" Language="C#" MasterPageFile="~/_Layout.Master" AutoEventWireup="true" CodeBehind="Certificaciones.aspx.cs" Inherits="SIE_KEY_USER.Views.Certificaciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">

        .logo {
            display: inline;
        }
        #datosSupervisor {
            display: block;
            position: relative;
            max-width: 100%;
            padding-left: 13%;
            font-family:Calibri Light;
            color: rgb(127, 127, 127);
        }
        .td_left {
            text-align: right;
        }
        .botonVerde {
            background-color:rgb(0,128,0);
            border-color:rgb(0,128,0);
            color:#fff;
            font-weight:bold;
            font-family:Calibri Light;
            font-size:16px;
            padding-bottom:5px;
            padding-top:5px;
        }
        .Contenido {
            display: block;
            position: relative;
            max-width: 100%;
            padding-top: 5px;
            padding-bottom: 5px;
        }
        #gridCertificaciones {
            display: block;
            position: relative;
            max-width: 100%;
            padding-left: 10%;
            padding-right: 10%;
            padding-top: 5px;
            padding-bottom: 5px;
        }
    .imgPerfil {
        max-height: 150px;
        max-width: 150px;
        }


        
    </style>
    <form id="Formulario" runat="server">
        <div class="Contenido" align="center">
            <table>
                <tr>
                    <td align="right" height="33"><asp:Label ID="label1" runat="server" Text="NOMBRE:&nbsp" CssClass="t_right"></asp:Label></td>
                    <td align="left"><asp:Label ID="lblNombre" runat="server" Text="Label" CssClass="t_right"></asp:Label></td>
                    <td class="imgPerfil" rowspan="6"><asp:Image ID="imgEmpleado" runat="server" CssClass="imgEmpleado" Height="200px" ImageAlign="AbsMiddle" Width="200px" /></td>

                </tr>
                <tr>
                    <td align="right" height="33"><asp:Label ID="Label6" runat="server" Text="FECHA DE INGRESO:&nbsp" CssClass="t_right"></asp:Label></td>
                    <td align="left"><asp:Label ID="lblFechaIngreso" runat="server" Text="Label" CssClass="t_right"></asp:Label></td>
                    
                </tr>
                <tr>
                    <td align="right" height="33"><asp:Label ID="Label2" runat="server" Text="NO. RELOJ:&nbsp" CssClass="t_right"></asp:Label></td>
                    <td align="left"><asp:Label ID="lblNoReloj" runat="server" Text="Label" CssClass="t_right"></asp:Label></td>
                    
                </tr>
                <tr>
                    <td align="right" height="33"><asp:Label ID="Label3" runat="server" Text="TURNO:&nbsp" CssClass="t_right"></asp:Label></td>
                    <td align="left"><asp:Label ID="lblTurno" runat="server" Text="Label" CssClass="t_right"></asp:Label></td>
                    
                </tr>
                <tr>
                    <td align="right" height="33"><asp:Label ID="Label4" runat="server" Text="SUPERVISOR:&nbsp" CssClass="t_right"></asp:Label></td>
                    <td align="left"><asp:Label ID="lblSupervisor" runat="server" Text="Label" CssClass="t_right"></asp:Label></td>
                </tr>
                <tr>
                    <td align="right" height="33"><asp:Label ID="Label5" runat="server" Text="ÁREA:&nbsp" CssClass="t_right"></asp:Label></td>
                    <td align="left"><asp:Label ID="lblArea" runat="server" Text="Label" CssClass="t_right"></asp:Label></td>
                    
                </tr>

            </table>
            
            
        </div>

        <div class="Contenido">
            <asp:TextBox ID="txtCodigo" runat="server" Width="150" placeholder="Número de reloj"></asp:TextBox>
           
            <asp:TextBox ID="TextBox1" runat="server" Width="150" placeholder="Nombre de Planta"></asp:TextBox>
            
           
        </div>
        <div class="Contenido">
            <asp:Button ID="btnBuscar" ClientIDMode="Static" cssClass="botonVerde" runat="server" OnClick="btnBuscar_Click" Text="Buscar certificaciones"/>

        </div>
        <div id="gridCertificaciones">
            <asp:GridView ID="grdCertificaciones" runat="server" CssClass="grid" Width="100%"  AllowPaging="True" PageSize="14" OnPageIndexChanging="GridView2_PageIndexChanging">
                <AlternatingRowStyle CssClass="AltRow" />
            </asp:GridView>
        </div>
        <div class="Contenido">
            <asp:Label ID="lblMensaje" runat="server" Text="" ForeColor="Red"></asp:Label><br />
            <asp:Button ID="btnBack" runat="server" Text="Menú" cssClass="botonVerde" OnClick="btnMenu" OnClientClick="PlaySound(); return true" />

        </div>
    </form>
    
</asp:Content>
