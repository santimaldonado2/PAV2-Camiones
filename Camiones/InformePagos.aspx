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
                            <label for="FechaMin">Fecha desde</label>
                    <asp:TextBox ID="FechaMin" class="form-control datepicker" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorFechaDesde" ForeColor="Red" runat="server" ControlToValidate="FechaMin" EnableTheming="True" ErrorMessage="Ingrese una fecha válida con el formato DD/MM/YYYY" SetFocusOnError="False" ValidationExpression="^(((((0[1-9])|(1\d)|(2[0-8]))\/((0[1-9])|(1[0-2])))|((31\/((0[13578])|(1[02])))|((29|30)\/((0[1,3-9])|(1[0-2])))))\/((20[0-9][0-9])|(19[0-9][0-9])))|((29\/02\/(19|20)(([02468][048])|([13579][26]))))$ "></asp:RegularExpressionValidator>
                    
                            <label for="FechaMax">Fecha hasta</label>
                    <asp:TextBox ID="FechaMax" class="form-control datepicker" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorFechaHasta" ForeColor="Red" runat="server" ControlToValidate="FechaMax" EnableTheming="True" ErrorMessage="Ingrese una fecha válida con el formato DD/MM/YYYY" SetFocusOnError="False" ValidationExpression="^(((((0[1-9])|(1\d)|(2[0-8]))\/((0[1-9])|(1[0-2])))|((31\/((0[13578])|(1[02])))|((29|30)\/((0[1,3-9])|(1[0-2])))))\/((20[0-9][0-9])|(19[0-9][0-9])))|((29\/02\/(19|20)(([02468][048])|([13579][26]))))$ "></asp:RegularExpressionValidator>
                    
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
            <asp:GridView ID="GrillaPagos" runat="server" AutoGenerateColumns="False" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" ForeColor="Black">
                <Columns>
                    <asp:BoundField DataField="nombreChofer" HeaderText="Chofer" />
                    <asp:BoundField DataField="montoTotal" HeaderText="Monto" />
                    <asp:BoundField DataField="fechaPago" HeaderText="Fecha Pago" />
                </Columns>
                <FooterStyle BackColor="#CCCCCC" />
                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                <RowStyle BackColor="White" />
                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#808080" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#383838" />
            </asp:GridView>
        </div>
    </div>




</asp:Content>
