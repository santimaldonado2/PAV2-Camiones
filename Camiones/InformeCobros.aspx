<%@ Page Title="" Language="C#" MasterPageFile="~/Camiones.Master" AutoEventWireup="true" CodeBehind="InformeCobros.aspx.cs" Inherits="Camiones.InformeCobros" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div id="filter-panel" class="collapse filter-panel">
                <div class="panel panel-default">
                    <div class="panel-body">
                        <div class="form-inline" role="form">
                            <div class="form-group">
                                <asp:Label ID="lblCliente" for="SelectCliente" class="filter-col" Style="margin-right: 0;" runat="server" Text="Cliente"></asp:Label>
                                <asp:DropDownList class="form-control" ID="SelectCliente" runat="server"></asp:DropDownList>
                            </div>
                            <!-- form group [rows] -->
                            <div class="form-group">
                                <asp:Label ID="lblCobro" for="SelectCobro" class="filter-col" Style="margin-right: 0;" runat="server" Text="Cobro"></asp:Label>
                                <asp:DropDownList class="form-control" ID="SelectCobro" runat="server"></asp:DropDownList>
                            </div>
                            <!-- form group [search] -->
                            <div class="form-group">
                                <asp:Label ID="lblFechaMin" for="Fecha" class="filter-col" Style="margin-right: 0;" runat="server" Text="Desde"></asp:Label>
                                <asp:TextBox ID="FechaMin" min="1" class="form-control number" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lblFechaMax" for="Fecha" class="filter-col" Style="margin-right: 0;" runat="server" Text="Hasta"></asp:Label>
                                <asp:TextBox ID="FechaMax" min="1" class="form-control number" runat="server"></asp:TextBox>
                            </div>
                            <!-- form group [order by] -->
                            <div class="form-group">
                                <asp:Button ID="Buscar" type="number" class="btn btn-default filter-col" runat="server" Text="Buscar" OnClick="Buscar_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <button type="button" class="btn btn-primary" data-toggle="collapse" data-target="#filter-panel">
                <span class="glyphicon glyphicon-cog"></span>Busqueda Avanzada
       
            </button>
        </div>
    </div>
    <div class="panel panel-default" id="panel_grilla" runat="server">
        <div class="table" id="divGrilla" align="center" runat="server">
            <asp:GridView ID="GrillaCobros" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="nombreCliente" HeaderText="Cliente" />
                    <asp:BoundField DataField="tipoCobro" HeaderText="Tipo de Cobro" />
                    <asp:BoundField DataField="montoCobro" HeaderText="Monto" />
                    <asp:BoundField DataField="fechaCobro" HeaderText="Fecha" />

                </Columns>
            </asp:GridView>
        </div>
    </div>




</asp:Content>