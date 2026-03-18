<%@ Page Title="" Language="C#" MasterPageFile="~/_Layout.Master" AutoEventWireup="true" CodeBehind="testo.aspx.cs" Inherits="SIE_KEY_USER.Views.testo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <link href="../Content/jquery.dataTables.min.css" rel="stylesheet" />
    <script src="../Scripts/jquery-3.3.1.js"></script>
    <script src="../Scripts/dataTables.bootstrap.min.js"></script>
    <script src="../Scripts/jquery.dataTables.min.js"></script>
    <script type="text/javascript">

        $(document).ready(function() {
            $('#table_id').DataTable();
        } );
    </script>

    <table id="table_id" class="display">
    <thead>
        <tr>
            <th>Column 1</th>
            <th>Column 2</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>Row 1 Data 1</td>
            <td>Row 1 Data 2</td>
        </tr>
        <tr>
            <td>Row 2 Data 1</td>
            <td>Row 2 Data 2</td>
        </tr>
    </tbody>
</table>
</asp:Content>
