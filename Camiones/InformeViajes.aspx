<%@ Page Title="" Language="C#" MasterPageFile="~/Camiones.Master" AutoEventWireup="true" CodeBehind="InformeViajes.aspx.cs" Inherits="Camiones.InformeViajes" %>

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
                            <!-- form group [rows] -->
                            <div class="form-group">
                                <asp:Label ID="lblCamion" for="SelectCamion" class="filter-col" Style="margin-right: 0;" runat="server" Text="Camion"></asp:Label>
                                <asp:DropDownList class="form-control" ID="SelectCamion" runat="server"></asp:DropDownList>
                            </div>
                            <!-- form group [search] -->
                            <div class="form-group">
                                <asp:Label ID="lblDistancia" for="Distancia" class="filter-col" Style="margin-right: 0;" runat="server" Text="Distancia Entre"></asp:Label>
                                <asp:TextBox ID="Distancia_min" min="1" class="form-control number" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lblDistanciaMax" for="Distancia" class="filter-col" Style="margin-right: 0;" runat="server" Text="Y"></asp:Label>
                                <asp:TextBox ID="Distancia_max" min="1" class="form-control number" runat="server"></asp:TextBox>
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
            <asp:GridView ID="GrillaViajes" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="nombreChofer" HeaderText="Chofer" />
                    <asp:BoundField DataField="numeroCamion" HeaderText="N° Camión" />
                    <asp:BoundField DataField="distanciaTotal" HeaderText="Distancia Total" />
                </Columns>
            </asp:GridView>
        </div>
    </div>




</asp:Content>
