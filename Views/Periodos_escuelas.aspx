<%@ Page Title="" Language="C#" MasterPageFile="~/_Layout.Master" AutoEventWireup="true" CodeBehind="Periodos_escuelas.aspx.cs" Inherits="SIE_KEY_USER.Views.Periodos_escuelas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <form id="pocentajes_form" runat="server" style="margin-top: 69px; height: 669px;">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Path="~/Scripts/click_bttn/click_btnn.js" />
                <asp:ScriptReference Path="~/Scripts/bootstrap/modal_popup.js" />
            </Scripts>
        </asp:ScriptManager>
       
       

        <audio id="audio" src="../Content/Cick.wav" ></audio> 

        <asp:Label ID="nombre" ClientIDMode="Static" runat="server" Text=""></asp:Label>

        <img src="../Images/MenuKey/reembolso_escolar.png" id="img_deVentanas" />

        <div id="center_PorcProm">
          
          <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>           
            
        <asp:GridView ID="Grid_Periodo" runat="server" ClientIDMode="Static" PageSize="4" AllowPaging="True" CssClass="grid1" PagerStyle-CssClass="pgt"  OnRowCommand="Grid_Periodo_RowCommand" AutoGenerateColumns="False"  OnPageIndexChanging="Grid_Periodo_PageIndexChanging"  >
            <AlternatingRowStyle Wrap="True" />
            <Columns>
                <asp:BoundField DataField="Tipo" HeaderText="Tipo periodo" />
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
                    
                    <asp:GridView ID="Grid_Eperiodo" runat="server" ClientIDMode="Static" PageSize="4" AllowPaging="True" CssClass="grid1" PagerStyle-CssClass="pgt"  OnRowCommand="Grid_Eperiodo_RowCommand" AutoGenerateColumns="False" OnPageIndexChanging="Grid_Eperiodo_PageIndexChanging">
                        <Columns>
                            <asp:BoundField DataField="Periodo" HeaderText="Descripción elemento" />
                            <asp:BoundField DataField="Tipo" HeaderText="Tipo periodo" />
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
                        <PagerStyle CssClass="pgt" />
                    </asp:GridView>


                    <asp:GridView ID="Grid_Escuelas" runat="server" ClientIDMode="Static" PageSize="4" AllowPaging="True" CssClass="grid1" PagerStyle-CssClass="pgt"  OnRowCommand="Grid_Escuelas_RowCommand" AutoGenerateColumns="False"  OnPageIndexChanging="Grid_Escuelas_PageIndexChanging">
                        <Columns>
                            <asp:BoundField DataField="escuela" HeaderText="Nombre escuela" />
                            <asp:BoundField DataField="periodo" HeaderText="Tipo periodo" />
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
                        <PagerStyle CssClass="pgt" />
                    </asp:GridView>
                    <div id="botones_agreg">
                        <table id="botones_add">
                            <tr>
                                <td ><asp:Button ID="agregar_periodo" runat="server" CssClass="btn" Text="Agregar periodo" OnClick="agregar_Click" /></td>
                            </tr>
                            <tr>
                                <td ><asp:Button ID="agregar_escuela" runat="server" CssClass="btn" Text="Agregar escuela" OnClick="agregar_escuela_Click"  /></td>
                            </tr>
                            <tr>
                                <td><asp:Button ID="agregar_elemento" runat="server" CssClass="btn" Text="Agregar elemento" OnClick="agregar_elemento_Click"  /></td>
                            </tr>
                        </table>

                    </div>
                    
                    <div id="modif_agreg_periodo">
          <table id="tabla_modifperiodo" runat="server">
              <tr>
                  <td class="t_right"><bold><h4>Modificar/</h4></bold></td>
                  <td class="t_left"><bold><h4>Agregar</h4></bold></td>
              </tr>
              <tr>
                  <td>Tipo periodo:</td>
                  <td>
                      <asp:TextBox ID="Tperiodo" CssClass="txtmount" runat="server" Enabled="false" AutoCompleteType="Disabled" ></asp:TextBox></td>
              </tr>
              <tr>
                  <td>Tipo periodo</td>
                  <td><asp:DropDownList ID="DropDownList1" runat="server" DataTextField="Tipo" DataValueField="Tipo" Enabled="false"></asp:DropDownList>
                      <%--<asp:SqlDataSource ID="SqlSPPeriodos" runat="server" ConnectionString="<%$ ConnectionStrings:SIEConnectionString %>" SelectCommand="sp_verPeriodos" SelectCommandType="StoredProcedure"></asp:SqlDataSource>--%>
                      <%-- DataSourceID="SqlSPPeriodos"--%>
                      
                  </td>
                  
              </tr>
              <tr>
                  <td >Descripción<br/>elemento:</td>
                  <td>
                      <asp:TextBox ID="Delemento" CssClass="txtmount" runat="server" Enabled="false" AutoCompleteType="Disabled" ></asp:TextBox></td>
              </tr>
              <tr>
                  <td >Nombre<br/>escuela:</td>
                  <td>
                      <asp:TextBox ID="Nescuela" CssClass="txtmount" runat="server" Enabled="false" AutoCompleteType="Disabled" ></asp:TextBox></td>
              </tr>
              <tr>
                  <td>
                      <asp:Button ID="Guardar" CssClass="btn" runat="server" Text="Guardar" Enabled="false" OnClick="Guardar_Click" />
                      
                  </td>
                  <td>
                      <asp:Button ID="Cancelar" CssClass="btn" runat="server" Text="Cancelar" Enabled="false" OnClick="Cancelar_Click" /></td>
              </tr>
          </table>

      </div>
                    
                </ContentTemplate>
        </asp:UpdatePanel>    
    </div>
      

        <asp:Button ID="Button1" CssClass="boton" runat="server" Text="Regresar" OnClick="Button1_Click" OnClientClick="PlaySound(); return true" />
                    
                    <asp:Label ID="mensaje" ClientIDMode="Static" runat="server" Text=""></asp:Label>
    </form>
    
   
    
</asp:Content>