<%@ Page Title="" Language="C#" MasterPageFile="~/_Layout.Master" AutoEventWireup="true" CodeBehind="Reembolso_escolar.aspx.cs" Inherits="SIE_KEY_USER.Views.Reembolso_escolar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <form id="prestamos_form" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Path="~/Scripts/click_bttn/click_btnn.js" />
                <asp:ScriptReference Path="~/Scripts/bootstrap/modal_popup.js" />
            </Scripts>
        </asp:ScriptManager>

       <script>
           $(document).ready(function () {
               $("#<%=TextBox1.ClientID%>").keydown(function (e) {
                   // Allow: backspace, delete, tab, escape, enter and .
                   //if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
                   if ($.inArray(e.keyCode, [46, 8, 9, 27, 13]) !== -1 || // ret: 110 and 190 keycodes removed for not allowing decimal point or period
                       // Allow: Ctrl+A
                       (e.keyCode == 65 && e.ctrlKey === true) ||
                       // Allow: Ctrl+C
                       (e.keyCode == 67 && e.ctrlKey === true) ||
                       // Allow: Ctrl+X
                       (e.keyCode == 88 && e.ctrlKey === true) ||
                       // Allow: home, end, left, right
                       (e.keyCode >= 35 && e.keyCode <= 39)) {
                       // let it happen, don't do anything
                       return;
                   }
                   // Ensure that it is a number and stop the keypress
                   if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                       e.preventDefault();
                   }
               });
           });
       </script>

        <audio id="audio" src="../Content/Cick.wav" ></audio> 

        <asp:Label ID="nombre" ClientIDMode="Static" runat="server" Text=""></asp:Label>

        <img src="../Images/MenuKey/reembolso_escolar.png" id="img_deVentanas" />

        <div id="center_confPrestamo">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table  id="tb_bemp">
                        <tr>
                            <td class="t_right">Empleado:</td>
                            <td class="t_left">
                                <asp:TextBox ID="TextBox1" runat="server" CssClass="CajaTexto" TextMode="Number" placeholder="Número de empleado" AutoCompleteType="Disabled"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="des_buscar" ClientIDMode="Static" runat="server" Text="Buscar" OnClick="des_buscar_Click" OnClientClick="PlaySound(); return true" />
                            </td>
                            <td id="wdtd" style="width: 87px">
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0">
                                    <ProgressTemplate>
                                        <asp:Image ID="loading" ClientIDMode="Static" runat="server" ImageUrl="~/Images/loading.gif" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </td>
                        </tr>
                    </table>
            
        <asp:GridView ID="GridView1" runat="server" ClientIDMode="Static" PageSize="5" AllowPaging="True" CssClass="grid" PagerStyle-CssClass="pgt" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCommand="GridView1_RowCommand" Height="269px" Width="1038px">
            <AlternatingRowStyle Wrap="True" />
            <Columns>
                <asp:TemplateField HeaderText="Ver">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="select" >Ver</asp:LinkButton>
                    </ItemTemplate>

                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                            No se encontraron registros.
                        </EmptyDataTemplate>
<PagerStyle CssClass="pgt"></PagerStyle>
        </asp:GridView>
                    <asp:Label ID="mensaje" ClientIDMode="Static" runat="server" Text=""></asp:Label>

                </ContentTemplate>
            </asp:UpdatePanel>
       

        

       
    </div>
        <asp:Button ID="Aceptados" runat="server" ClientIDMode="Static" Text="Ver Aprobados" OnClientClick="PlaySound(); return true" OnClick="Aceptados_Click"/>
        <asp:Button ID="Rechazados" runat="server" ClientIDMode="Static" Text="Ver Rechazados" OnClientClick="PlaySound(); return true" OnClick="Rechazados_Click"/>
        <asp:Button ID="PeryEsc" runat="server" ClientIDMode="Static" Text="Administrar periodos y escuelas" OnClientClick="PlaySound(); return true" OnClick="PeryEsc_Click"/>
        <asp:Button ID="PorYPro" runat="server" ClientIDMode="Static" Text="Administrar porcentajes y promedios" OnClientClick="PlaySound(); return true" OnClick="PorYPro_Click"/>
        <asp:Button ID="Button1" CssClass="boton" runat="server" Text="Menú" OnClick="Button1_Click" OnClientClick="PlaySound(); return true" />
    </form>
    
   
    
</asp:Content>
