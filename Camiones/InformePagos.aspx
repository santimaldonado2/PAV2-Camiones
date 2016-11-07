<%@ Page Language="C#"  MasterPageFile="~/Camiones.Master" AutoEventWireup="true" CodeBehind="~/InformePagos.aspx.cs" Inherits="Camiones.WebForm1" %>


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
                                <asp:Label ID="lblChofer" for="SelectChofer" class="filter-col" Style="margin-right: 0;" runat="server" Text="Chofer"></asp:Label>
                                <asp:DropDownList class="form-control" ID="SelectChofer" runat="server"></asp:DropDownList>
                            </div>
                            <!-- form group [search] -->
                            <div class="form-group">
                                <asp:Label ID="lblMonto" for="Monto" class="filter-col" Style="margin-right: 0;" runat="server" Text="Monto Entre"></asp:Label>
                                <asp:TextBox ID="Monto_min" min="1" class="form-control number" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lblMontoMax" for="Monto" class="filter-col" Style="margin-right: 0;" runat="server" Text="Y"></asp:Label>
                                <asp:TextBox ID="Monto_max" min="1" class="form-control number" runat="server"></asp:TextBox>
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
            <asp:GridView ID="GrillaPagos" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="nombreChofer" HeaderText="Chofer" />
                    <asp:BoundField DataField="montoTotal" HeaderText="Monto" />
                    <asp:BoundField DataField="fechaPago" HeaderText="Fecha Pago" />
                </Columns>
            </asp:GridView>
        </div>
    </div>




</asp:Content>
