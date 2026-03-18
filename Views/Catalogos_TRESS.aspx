<%@ Page Title="" Language="C#" MasterPageFile="~/_Layout.Master" AutoEventWireup="true" CodeBehind="Catalogos_TRESS.aspx.cs" Inherits="SIE_KEY_USER.Views.Catalogos_TRESS" %>

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

        <div id="center_cat_TRESS">
          
          <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>           
             
<div style="overflow-y:auto;  height: 305px; width:595px; position:absolute; top:212px; left:384px;"  >
        <asp:GridView ID="Grid_TRESS" runat="server" ClientIDMode="Static"   AllowPaging="false" CssClass="grid"  OnRowDataBound="Grid_TRESS_RowDataBound" >
            <AlternatingRowStyle Wrap="True" />
            <Columns>
                <asp:TemplateField HeaderText="Visible">
                        <ItemTemplate><asp:SqlDataSource ID="opcion" DataSourceMode="DataReader" ConnectionString="<%$ ConnectionStrings:db %>" SelectCommand="select a.[id_opcion], a.[descripcion] from [SIE].[dbo].[status_tress] a" runat="server"></asp:SqlDataSource>
                            <asp:DropDownList ID="seleccionar" runat="server" DataSourceID="opcion" DataTextField="descripcion" DataValueField="id_opcion" ></asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
            </Columns>
                        <EmptyDataTemplate>
                            No se encontraron registros.
                        </EmptyDataTemplate>
        </asp:GridView>
                </div> 

                    <div id="btnGuardarTRESS">
                        <asp:Button ID="GuardarTRESS" CssClass ="btn " runat="server" Text="Guadar" OnClick="GuardarTRESS_Click" Visible="False" />
                    </div>
                     <div id="btnActualizarTRESS">
                        <asp:Button ID="actualizarTRESS" CssClass ="btn " runat="server" Text="Actualizar elementos"  Visible="True" OnClick="actualizarTRESS_Click" />
                    </div>
                    <div id="botones_TRESS">
                        <table id="CatTRESS">
                            <tr>
                                <td>
                                    <asp:Button ID="vive_con" CssClass ="btnTRESS " runat="server"  Text="Vive con" OnClick="Button_Click"  onClientClick="return true" />
                                </td>
                            </tr>
                            <tr> 
                                <td>
                                    <asp:Button ID="vive_en" CssClass ="btnTRESS " runat="server"  Text="Vive en" OnClick="Button_Click"   />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="transpor" CssClass ="btnTRESS " runat="server"  Text="Transporte" OnClick="Button_Click"   />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="talla" CssClass ="btnTRESS " runat="server"  Text="Talla camisa" OnClick="Button_Click"   />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="relacion" CssClass ="btnTRESS " runat="server"  Text="Relación contacto" OnClick="Button_Click"    />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="adj" CssClass ="btnTRESS " runat="server"  Text="SIE" OnClick="Button_Click"     />
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