<%@ Page Title="" Language="C#" MasterPageFile="~/Camiones.Master" AutoEventWireup="true" CodeBehind="RegistrarCobro.aspx.cs" Inherits="Camiones.RegistrarCobro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="panel panel-default">
        <div class="panel-body">
            <div class="form-inline" role="form">
                <h2 id="titulo" runat="server">Registrar Cobro</h2>
                <div class="form-group">
                    <asp:Label ID="lblCliente" for="SelectCliente" class="filter-col" Style="margin-right: 0;" runat="server" Text="Cliente"></asp:Label>
                    <asp:DropDownList class="form-control" ID="SelectCliente" runat="server"></asp:DropDownList>

                </div>
                <!-- form group [rows] -->
                <div class="form-group">
                    <asp:Label ID="lblTipoCobro" for="SelectTipoCobro" class="filter-col" Style="margin-right: 0;" runat="server" Text="Tipo de Cobro"></asp:Label>
                    <asp:DropDownList class="form-control" ID="SelectTipoCobro" runat="server"></asp:DropDownList>
                </div>

                <div>
                 <label for="FechaCobro">Fecha de Cobro</label>
                    <asp:TextBox ID="FechaCobro" class="form-control datepicker" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorFecha" ForeColor="Red" runat="server" ControlToValidate="FechaInscripcion" EnableTheming="True" ErrorMessage="Ingrese una fecha válida con el formato DD/MM/YYYY" SetFocusOnError="False" ValidationExpression="^(((((0[1-9])|(1\d)|(2[0-8]))\/((0[1-9])|(1[0-2])))|((31\/((0[13578])|(1[02])))|((29|30)\/((0[1,3-9])|(1[0-2])))))\/((20[0-9][0-9])|(19[0-9][0-9])))|((29\/02\/(19|20)(([02468][048])|([13579][26]))))$ "></asp:RegularExpressionValidator>
                </div>



                <div class="form-group">
                <asp:Label ID="lblMonto" for="MontoTotal" class="filter-col" Style="margin-right: 0;" runat="server" Text="Monto Total"></asp:Label>
                <asp:TextBox ID="txtMontoTotal" type="number" step="0.01" runat="server"></asp:TextBox>
               
            </div>


            </div>
        </div>
    </div>
    <div class="panel panel-default">
        <h2  id="H1" runat="server">Cargar Detalle de Cobro</h2>
        
        <div class="form-inline" role="form">

        <asp:Label ID="Label1" for="SelectTipoCobro" class="filter-col" Style="margin-right: 0;" runat="server" Text="Seleccione viaje"></asp:Label>   
        <asp:GridView ID="GrillaDetallesViajes" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
            <Columns>
                <asp:CommandField SelectText="Seleccionar" ShowSelectButton="True" />
                <asp:BoundField DataField="idDetalleViaje" HeaderText="Id Detalle Viaje" />
                <asp:BoundField DataField="FechaSalida" DataFormatString="{0:dd/mm/yy}" HeaderText="Fecha de Salida" />
                <asp:BoundField DataField="FechaLlegada" DataFormatString="{0:dd/mm/yy}" HeaderText="Fecha de Llegada" />
                <asp:BoundField DataField="Kilogramos" HeaderText="Kilogramos" DataFormatString="{0:F}" />
                <asp:BoundField DataField="Cliente" HeaderText="Cliente" />
                <asp:BoundField DataField="Distancia" HeaderText="Distancia Recorrida" />
                <asp:BoundField DataField="PrecioUnitario" HeaderText="Precio Unitario" />
                <asp:BoundField DataField="Subtotal" HeaderText="Subtotal" />

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

              <div class="form-group">
                <asp:Button ID="Agregar" runat="server" class="btn btn-default" Text="Agregar" OnClick="Agregar_Click" />
            </div>

        <p>
     </p>

            <asp:GridView ID="gridDetalleCobro" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
            <Columns>

                <asp:BoundField DataField="Cliente" HeaderText="Cliente" />
                <asp:BoundField DataField="TipoDeCobro" HeaderText="Tipo de Cobro" />
                <asp:BoundField DataField="FechaCobro" DataFormatString="{0:mm/dd/yy}" HeaderText="Fecha de Cobro" />              
                <asp:BoundField DataField="FechaSalida" DataFormatString="{0:mm/dd/yy}" HeaderText="Fecha de Salida" />
                <asp:BoundField DataField="FechaLlegada" DataFormatString="{0:mm/dd/yy}" HeaderText="Fecha de Llegada" />
                <asp:BoundField DataField="Kilogramos" HeaderText="Kilogramos" DataFormatString="{0:F}" />               
                <asp:BoundField DataField="Distancia" HeaderText="Distancia Recorrida" />
                <asp:BoundField DataField="PrecioUnitario" HeaderText="Precio Unitario" />
                <asp:BoundField DataField="Subtotal" HeaderText="Subtotal" />

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


     <div>
        <asp:Button ID="Registrar" runat="server" CausesValidation="false" Text="Registrar Cobro" OnClick="Registrar_Click" />
    </div>
            
            </div>
        </div>
</asp:Content>
