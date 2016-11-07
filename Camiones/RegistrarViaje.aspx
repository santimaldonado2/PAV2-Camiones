<%@ Page Title="" Language="C#" MasterPageFile="~/Camiones.Master" AutoEventWireup="true" CodeBehind="RegistrarViaje.aspx.cs" Inherits="Camiones.RegistrarViaje" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="panel panel-default">
        <div class="panel-body">
            <div class="form-inline" role="form">
                <h2 id="titulo" runat="server">Registrar Viaje</h2>
                <div class="form-group">
                    <asp:Label ID="lblChofer" for="SelectChofer" class="filter-col" Style="margin-right: 0;" runat="server" Text="Chofer"></asp:Label>
                    <asp:DropDownList class="form-control" ID="SelectChofer" runat="server"></asp:DropDownList>

                </div>
                <!-- form group [rows] -->
                <div class="form-group">
                    <asp:Label ID="lblCamion" for="SelectCamion" class="filter-col" Style="margin-right: 0;" runat="server" Text="Camion"></asp:Label>
                    <asp:DropDownList class="form-control" ID="SelectCamion" runat="server"></asp:DropDownList>
                </div>
            </div>
        </div>
    </div>
    <div class="panel panel-default">
        <h2  id="H1" runat="server">Cargar Detalle de Viaje</h2>
        <div class="form-inline" role="form">
            <div class="form-group">
                <asp:Label ID="Label1" for="SelectCiudadOrigen" class="filter-col" Style="margin-right: 0;" runat="server" Text="Ciudad de Origen"></asp:Label>
                <asp:DropDownList class="form-control" ID="SelectCiudadOrigen" runat="server" AutoPostBack="true" OnSelectedIndexChanged="SelectCiudadOrigen_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <div class="form-group">
                <asp:Label ID="Label3" for="FechaOrigen" class="filter-col" Style="margin-right: 0;" runat="server" Text="Fecha de Salida"></asp:Label>
                <asp:TextBox ID="FechaOrigen" CssClass="datepickermin" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ControlToValidate="FechaOrigen" Text="Este campo es requerido" runat="server"></asp:RequiredFieldValidator>
            </div>

        </div>
        <div class="form-inline" role="form">

            <!-- form group [rows] -->
            <div class="form-group">
                <asp:Label ID="Label2" for="SelectCiudadDestino" class="filter-col" Style="margin-right: 0;" runat="server" Text="Ciudad de Destino"></asp:Label>
                <asp:DropDownList class="form-control" ID="SelectCiudadDestino" runat="server"></asp:DropDownList>

            </div>

            <!-- form group [rows] -->
            <div class="form-group">
                <asp:Label ID="Label4" for="FechaDestino" class="filter-col" Style="margin-right: 0;" runat="server" Text="Fecha de Llegada"></asp:Label>
                <asp:TextBox ID="FechaDestino" CssClass="datepickermin" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ControlToValidate="FechaDestino" Text="Este campo es requerido" runat="server"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="form-inline" role="form">
            <!-- form group [rows] -->
            <div class="form-group">
                <asp:Label ID="Label5" for="SelectCliente" class="filter-col" Style="margin-right: 0;" runat="server" Text="Cliente"></asp:Label>
                <asp:DropDownList class="form-control" ID="SelectCliente" runat="server"></asp:DropDownList>
            </div>
            <div class="form-group">
                <asp:Label ID="Label8" for="Kilogramos" class="filter-col" Style="margin-right: 0;" runat="server" Text="Kilogramos"></asp:Label>
                <asp:TextBox ID="Kilogramos" type="number" step="0.01" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ControlToValidate="Kilogramos" Text="Este campo es requerido" runat="server"></asp:RequiredFieldValidator>

            </div>
        </div>
        <div class="form-inline" role="form">
            <div class="form-group">
                <asp:Label ID="Label6" for="PrecioUnitario" class="filter-col" Style="margin-right: 0;" runat="server" Text="Precio Unitario"></asp:Label>
                <asp:TextBox ID="PrecioUnitario" type="number" step="0.01" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ControlToValidate="PrecioUnitario" Text="Este campo es requerido" runat="server"></asp:RequiredFieldValidator>
            </div>
            <div class="form-group">
                <asp:Label ID="Label7" for="Distancia" class="filter-col" Style="margin-right: 0;" runat="server" Text="Distancia"></asp:Label>
                <asp:TextBox ID="Distancia" type="number" step="0.01" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ControlToValidate="Distancia" Text="Este campo es requerido" runat="server"></asp:RequiredFieldValidator>

            </div>
            <div class="form-group">
                <asp:Button ID="Agregar" runat="server" class="btn btn-default" Text="Agregar" OnClick="Agregar_Click" />
            </div>
        </div>
        <asp:GridView ID="GrillaDetallesViajes" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
            <Columns>
                <asp:BoundField DataField="CiudadOrigen" HeaderText="Ciudad Origen" />
                <asp:BoundField DataField="CiudadDestino" HeaderText="Ciudad Destino" />
                <asp:BoundField DataField="FechaSalida" DataFormatString="{0:dd/mm/yy}" HeaderText="Fecha de Salida" />
                <asp:BoundField DataField="FechaLlegada" DataFormatString="{0:dd/mm/yy}" HeaderText="Fecha de Llegada" />
                <asp:BoundField DataField="Kilogramos" HeaderText="Kilogramos" DataFormatString="{0:F}" />
                <asp:BoundField DataField="Cliente" HeaderText="Cliente" />
                <asp:BoundField DataField="Distancia" HeaderText="Distancia Recorrida" />
            </Columns>
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#007DBB" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#00547E" />
        </asp:GridView>



    </div>

    <asp:Label ID="lblDistanciaTotal" for="DistanciaTotal" class="filter-col" Style="margin-right: 0;" runat="server" Text="Distancia Total"></asp:Label>
    <asp:TextBox ID="DistanciaTotal" runat="server"></asp:TextBox>
    <div>
        <asp:Button ID="Registrar" runat="server" CausesValidation="false" Text="Registrar Viaje" OnClick="Registrar_Click" />
    </div>

</asp:Content>
