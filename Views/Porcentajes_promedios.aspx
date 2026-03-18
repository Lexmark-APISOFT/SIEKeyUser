<%@ Page Title="" Language="C#" MasterPageFile="~/_Layout.Master" AutoEventWireup="true" CodeBehind="Porcentajes_promedios.aspx.cs" Inherits="SIE_KEY_USER.Views.Porcentajes_promedios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <form id="pocentajes_form" runat="server" style="margin-top: 69px">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Path="~/Scripts/click_bttn/click_btnn.js" />
                <asp:ScriptReference Path="~/Scripts/bootstrap/modal_popup.js" />
            </Scripts>
        </asp:ScriptManager>
        <script>
            $(document).ready(function () {
                $(".txtmount").keydown(function (e) {
                   // Allow: backspace, delete, tab, escape, enter and .
                   //if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
                   if ($.inArray(e.keyCode, [46, 8, 9, 27, 13,110]) !== -1 || // ret: 110 and 190 keycodes removed for not allowing decimal point or period
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

        <div id="center_PorcProm">
          
          <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>           
            
        <asp:GridView ID="Grid_PorcentProm" runat="server" ClientIDMode="Static" PageSize="8" AllowPaging="True" CssClass="grid1" PagerStyle-CssClass="pgt"  OnRowCommand="Grid_PorcentProm_RowCommand" AutoGenerateColumns="False" >
            <AlternatingRowStyle Wrap="True" />
            <Columns>
                <asp:BoundField DataField="Promedio mínimo" HeaderText="Promedio mínimo" />
                <asp:BoundField DataField="Promedio máximo" HeaderText="Promedio máximo" />
                <asp:BoundField DataField="Porcentaje" HeaderText="Porcentaje" />
                <asp:TemplateField HeaderText="Modificar">
                   <ItemTemplate>
                      <asp:LinkButton ID="Modif" runat="server" CommandName="mod">Modificar</asp:LinkButton>
                   </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Eliminar">
                    <ItemTemplate>
                      <asp:LinkButton ID="Elim" runat="server" CommandName="del">Eliminar</asp:LinkButton>
                   </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                            No se encontraron registros.
                        </EmptyDataTemplate>
<PagerStyle CssClass="pgt"></PagerStyle>
        </asp:GridView>
                    
                    <div id="add_prom_porcen">
                        <table id="addpromporcen">
                            <tr>
                                <td>
                                    <asp:Button ID="agregar" runat="server" CssClass="btn" Text="Agregar" OnClick="agregar_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    
                    <asp:Label ID="mensaje" ClientIDMode="Static" runat="server" Text=""></asp:Label>
                    <div id="modif_agreg" >
                        
          <table id="tabla_modifporcen" runat="server">
              <tr>
                  <td class="t_right">Modificar/</td>
                  <td class="t_left">Agregar</td>
              </tr>
              <tr>
                  <td >Promedio<br/>mínimo:</td>
                  <td>
                      <asp:TextBox ID="promMin" CssClass="txtmount" runat="server" Enabled="false" AutoCompleteType="Disabled" ></asp:TextBox></td>
              </tr>
              <tr>
                  <td >Promedio<br/>máximo</td>
                  <td>
                      <asp:TextBox ID="promMax" CssClass="txtmount" runat="server" Enabled="false" AutoCompleteType="Disabled" ></asp:TextBox></td>
              </tr>
              <tr>
                  <td >Porcentaje</td>
                  <td>
                      <asp:TextBox ID="porcent" CssClass="txtmount" runat="server" Enabled="false" AutoCompleteType="Disabled" TextMode="Number"></asp:TextBox></td>
              </tr>
              <tr>
                  <td>
                      <asp:Button ID="Guardar" CssClass="btn" runat="server" Text="Guardar" Enabled="false" OnClick="Guardar_Click" /></td>
                  <td>
                      <asp:Button ID="Cancelar" CssClass="btn" runat="server" Text="Cancelar" Enabled="false" OnClick="Cancelar_Click" /></td>
              </tr>
          </table>

      </div>
        <div id="prom_Min" >

          <table id="tb_prom_min" runat="server">
              <tr>
                  <td >Promedio mínimo para<br/>tramitar reembolso:</td>
                  <td><asp:TextBox ID="promMinReem" CssClass="txtmount" runat="server" Enabled="false" AutoCompleteType="Disabled"></asp:TextBox></td>
                  <td><asp:Button ID="ModifProm" CssClass="btn" runat="server" Text="Modificar" Enabled="true" OnClick="ModifProm_Click" /></td>
              </tr>
              <tr>
                  <td>
                      <asp:Button ID="GuardarProm" CssClass="btn" runat="server" Text="Guardar" Enabled="false" OnClick="GuardarProm_Click" /></td>
                  <td>
                      <asp:Button ID="CancelarProm" CssClass="btn" runat="server" Text="Cancelar" Enabled="false" OnClick="CancelarProm_Click" /></td>
              </tr>
          </table>
            
      </div>
                </ContentTemplate>
        </asp:UpdatePanel>    
    </div>
      
        <asp:Button ID="Button1" CssClass="boton" runat="server" Text="Regresar" OnClick="Button1_Click" OnClientClick="PlaySound(); return true" />
    </form>
    
   
    
</asp:Content>