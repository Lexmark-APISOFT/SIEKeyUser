<%@Page Title="" Language="C#" MasterPageFile="~/_Layout.Master" AutoEventWireup="true" CodeBehind="Mantenimiento_Catalogos_Sig.aspx.cs" Inherits="SIE_KEY_USER.Views.Mantenimiento_Catalogos_Sig" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <form id="mant_catalogos_form" runat="server" >

        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Path="~/Scripts/click_bttn/click_btnn.js" />
                <asp:ScriptReference Path="~/Scripts/bootstrap/modal_popup.js" />
            </Scripts>
        </asp:ScriptManager>
       
       

        <audio id="audio" src="../Content/Cick.wav" ></audio> 

        <asp:Label ID="nombre" ClientIDMode="Static" runat="server" Text=""></asp:Label>

        <img src="../Images/MenuKey/catalogos.png" id="img_deVentanas" />

        <div id="center_ClinCorr">
          
          <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>           
            
        <asp:GridView ID="Grid_IMSS" runat="server" ClientIDMode="Static" PageSize="4" AllowPaging="True" CssClass="grid1" PagerStyle-CssClass="pgt"  OnRowCommand="Grid_IMSS_RowCommand" AutoGenerateColumns="False"  OnPageIndexChanging="Grid_IMSS_PageIndexChanging"  >
            <AlternatingRowStyle Wrap="True" />
            <Columns>
                <asp:BoundField DataField="horario" HeaderText="Descripción horario" />
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
                    
                    <asp:GridView ID="Grid_Desparent" runat="server" ClientIDMode="Static" PageSize="6" AllowPaging="True" CssClass="grid1" PagerStyle-CssClass="pgt"  OnRowCommand="Grid_Desparent_RowCommand" AutoGenerateColumns="False" OnPageIndexChanging="Grid_Desparent_PageIndexChanging">
                        <Columns>
                            <asp:BoundField DataField="desparent" HeaderText="Descripción parentesco" />
                            <asp:TemplateField HeaderText="Modificar">
                   <ItemTemplate>
                      <asp:LinkButton ID="LinkButton1" runat="server" CommandName="mod">Modificar</asp:LinkButton>
                   </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Eliminar">
                    <ItemTemplate>
                      <asp:LinkButton ID="LinkButton2" runat="server" CommandName="del">Eliminar</asp:LinkButton>
                   </ItemTemplate>
                </asp:TemplateField>
                        </Columns>
                         <EmptyDataTemplate>
                            No se encontraron registros.
                        </EmptyDataTemplate>
                        <PagerStyle CssClass="pgt" />
                    </asp:GridView>


                    <div id="botones_agreg_catalogosimss">
                        <table id="botones_add_catalogos">
                            <tr>
                                <td ><asp:Button ID="agregar_horario_imss" runat="server" CssClass="btn" Text="Agregar horario IMSS" OnClick="agregar_horario_imss_Click" /></td>
                            </tr>                           
                        </table>

                    </div>
                    <div id="botones_agreg_dominioparent">
                        <table id="botones_add_dominio">
                            <tr>
                                <td ><asp:Button ID="agregar_parentesco" runat="server" CssClass="btn" Text="Agregar parentesco"  OnClick="agregar_parentesco_Click" /></td>
                            </tr>                           
                        </table>

                    </div>
                    <div id="botones_catalogos">
                        <table id="enlCatalogos">
                            <tr>
                                <td>
                                    <asp:Button ID="cat_sig" runat="server" CssClass ="btnCAT " Text="Catálogos anteriores" OnClick="cat_sig_Click"   />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="cat_tress" runat="server" CssClass ="btnCAT "  Text="Catálogos TRESS" OnClick="cat_tress_Click"   />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="modif_agreg_catalogos">
          <table id="tabla_modifcatalogo" runat="server">
              <tr>
                  <td class="t_right"><bold><h4>Modificar/</h4></bold></td>
                  <td class="t_left"><bold><h4>Agregar</h4></bold></td>
              </tr>
              <tr>
                  <td colspan ="2">
                      <bold><h5>Recuerda: si modificas la tabla parentesco familiar en el SIE<br />debes hacer la modificación también en el sistema TRESS.</h5></bold>
                  </td>
              </tr>
              <tr>
                  <td>Descripción horario:</td>
                  <td>
                      <asp:TextBox ID="Dhorario"  runat="server" Enabled="false" AutoCompleteType="Disabled" ></asp:TextBox></td>
              </tr>
              <tr>
                  <td>Descripción parentesco:</td>
                     <td> <asp:TextBox ID="Dparentesco"  runat="server" Enabled="false" AutoCompleteType="Disabled" ></asp:TextBox></td>
              </tr>
              <tr>
                  <td>
                      <asp:Button ID="Guardar" CssClass="btn" runat="server" Text="Guardar" Enabled="false" OnClick="Guardar_Click" />
                      
                  </td>
                  <td>
                      <asp:Button ID="Cancelar" CssClass="btn" runat="server" Text="Cancelar" Enabled="false" OnClick="Cancelar_Click"/>
                  </td>
              </tr>
          </table>

      </div>
                    
                </ContentTemplate>
        </asp:UpdatePanel>    
    </div>
      

        <asp:Button ID="Button1" CssClass="boton" runat="server" Text="Menú" OnClick="Button1_Click" OnClientClick="PlaySound(); return true" />
                    
                    
                    
                    <asp:Label ID="mensaje" ClientIDMode="Static" runat="server" Text=""></asp:Label>
    </form>
    
   
    
</asp:Content>