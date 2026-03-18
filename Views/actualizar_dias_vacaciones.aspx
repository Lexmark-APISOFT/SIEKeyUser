<%@ Page Title="" Language="C#" MasterPageFile="~/_Layout.Master" AutoEventWireup="true" CodeBehind="actualizar_dias_vacaciones.aspx.cs" Inherits="SIE_KEY_USER.Views.actualizar_dias_vacaciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  
    <asp:Label ID="lblNombreEmpleado" Text="" runat="server" style="position:absolute;right:10px; top:18px; font-weight:normal; font-size:18px; "/>
    <form runat="server" >
   
          <h3>Modificar cantidad de dias </h3>
        <h4>Cantidad de dias seleccionables actualmente:  <asp:Label ID="CantidadDiasMax" Text="text" runat="server" /></h4>
  

         <h5>Capturar cantidad  </h5>
               <asp:Panel runat="server" DefaultButton="btnActualizar"> 
                         <asp:TextBox id="txtActualizar" runat="server" type="number" /> 
               </asp:Panel>
                
             <asp:Button ID="btnActualizar" OnClick="btnActualizar_Click" runat="server"  BackColor="#33CC33" ForeColor="White" Height="34px" Text="Actualizar" Width="74px" />
                 <br />
                <br />
                <asp:Button ID="btnRegresar" Text="Volver" runat="server"  OnClick="btnRegresar_Click" style="background-color:red; font-size:14px; width:110px; height:34px; color:white;  "/>
            

       
   </form>
</asp:Content>

<%--<asp:Content ID="Content2" ContentPlaceHolderID="cph_JS" runat="server">

             <h5>Capturar cantidad
                 <asp:TextBox id="txtSearch" runat="server" type="text" /><asp:Button ID="Button1" runat="server"  BackColor="#33CC33" ForeColor="White" Height="34px" Text="Buscar" Width="74px" />
                 <br />
                 <asp:Button Text="text" runat="server" />
             </h5>
   
</asp:Content>--%>
