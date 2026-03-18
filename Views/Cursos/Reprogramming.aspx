<%@ Page Title="" Language="C#" MasterPageFile="~/_Layout.Master" AutoEventWireup="true" CodeBehind="Reprogramming.aspx.cs" Inherits="SIE_KEY_USER.Cursos.Reprogramming" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <form runat="server">
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-1">
                    <asp:LinkButton ID="btnBackPage" CssClass="d-flex align-items-center justify-content-center btn btn-back" runat="server" PostBackUrl="~/Views/Cursos/MenuCourses.aspx">
                        <span class="glyphicon glyphicon-arrow-left" style="margin-top: 5px;"></span>
                    </asp:LinkButton>
                </div>
                <div class="col-md-2" style="display: flex; justify-content: space-between; margin-top: 15px;">
                    <p style="font-size: 45px; font-family: 'Century Gothic'; font-weight: bolder;">Reprogramaciones</p>
                </div>
            </div>
            <div class="row">
                <div class="col-md-1"></div>
                <div class="col-md-10">
                    <asp:GridView ID="GvRepprogrammings" runat="server" AutoGenerateColumns="true" CssClass="table table-striped">
                        <Columns>
                            <asp:TemplateField HeaderText="Seleccionar">
                                <HeaderTemplate>
                                    <input  id="CheckBox1" type="checkbox" runat="server" onclick="CheckAll(this);" />
                                    <asp:Label ID="Label1" runat="server" Text="Seleccionar"  />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="seleccionar" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="col-md-1"></div>

            </div>
            <div class="row">
                <div class="col-md-4"></div>
                <div class="col-md-4 center-block">
                    <asp:Label runat="server" ID="NoSolLabel" Visible="false">No hay solicitudes</asp:Label>
                    <asp:Button ID="btnAcceptReprogramming" Text="Aceptar" CssClass="btn btn-success" runat="server" OnClick="btnAcceptReprogramming_Click"  />
                    <asp:Button ID="btnRejectReprogramming" Text="Rechazar" CssClass="btn btn-danger" runat="server" OnClick="btnRejectReprogramming_Click" />
                </div>
            </div>
        </div>

    </form>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_JS" runat="server">
    <link href="../../Content/cursos.css" rel="stylesheet" />
    <script>
        function CheckAll(oCheckBox) {
            var GridView1 = document.getElementById("<%=GvRepprogrammings.ClientID %>");

            for (i = 1; i < GridView1.rows.length; i++) {
                GridView1.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckBox.checked;
            }

        }
    </script>
</asp:Content>
