<%@ Page Title="" Language="C#" MasterPageFile="~/Camiones.Master" AutoEventWireup="true" CodeBehind="ABMCCamion.aspx.cs" Inherits="Camiones.ABMCCamion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>ABMC Camion</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    
    <div class="row-fluid col-sm-offset-3 col-sm-4">
        <div id="dialog" title="Mensaje" class="dialog">
            <p id="Mensaje" runat="server"></p>
        </div>
        <div class="panel panel-default" id="formulario" runat="server">
            <div class="panel-body">
                <h2 class="col-sm-9 col-sm-offset-3" id="titulo" runat="server">Formulario Camiones</h2>
                <div class="row form-group col-md-12">
                    <label for="IdCamion">ID</label>
                    <asp:TextBox ID="IdCamion" name="IdCamion" class="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                </div>
                <div class="row form-group col-md-12">
                    <label for="Patente">Patente</label>
                    <asp:TextBox ID="Patente" name="Patente" class="form-control" runat="server" MaxLength="10"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorPatente" ForeColor="Red" ControlToValidate="Patente" runat="server" ErrorMessage="Ingrese la patente del camión"></asp:RequiredFieldValidator>
                </div>
                <div class="row form-group col-md-12">
                    <asp:CheckBox ID="Habilitado" class="checkbox" Text="Habilitado" runat="server" />
                </div>
                <div class="row col-md-12 form-group">

                    <label for="FechaCompra">Fecha de Compra</label>
                    <asp:TextBox ID="FechaCompra" class="form-control datepicker" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorFecha" ForeColor="Red" runat="server" ControlToValidate="FechaCompra" EnableTheming="True" ErrorMessage="Ingrese una fecha válida con el formato DD/MM/YYYY" SetFocusOnError="False" ValidationExpression="^(((((0[1-9])|(1\d)|(2[0-8]))\/((0[1-9])|(1[0-2])))|((31\/((0[13578])|(1[02])))|((29|30)\/((0[1,3-9])|(1[0-2])))))\/((20[0-9][0-9])|(19[0-9][0-9])))|((29\/02\/(19|20)(([02468][048])|([13579][26]))))$ "></asp:RegularExpressionValidator>
                    <asp:CompareValidator ID="FechaCompraCompareValidator" ForeColor="Red" runat="server" Operator="LessThanEqual" ErrorMessage="La fecha de compra debe ser menor al dia de la fecha"
                        ControlToValidate="FechaCompra" Type="Date" />
                </div>
                <div class="row col-md-12 form-group">
                    <label for="NroVehiculo">Número de Vehiculo</label>
                    <asp:TextBox ID="NroVehiculo" class="form-control" runat="server"></asp:TextBox><br />

                </div>
                <div class="row col-md-12 form-group">
                    <label for="SelectMarca">Marca</label>
                    <asp:DropDownList ID="SelectMarca" class="form-control" runat="server">
                    </asp:DropDownList>
                </div>
                <div class="row form-group col-md-12">
                    <label for="Modelo">Modelo</label>
                    <asp:TextBox ID="Modelo" name="Modelo" class="form-control" runat="server" MaxLength="10"></asp:TextBox>
                </div>
            </div>

            <asp:Button ID="Guardar" class="btn btn-info abreDialog" OnClientClick="openDialog()" CausesValidation="true" runat="server" Text="Guardar" OnClick="Guardar_Click" />
            <asp:Button ID="Eliminar" class="btn  btn-danger abreDialog" OnClientClick="openDialog()" CausesValidation="false" runat="server" Text="Eliminar" OnClick="Eliminar_Click" />

            <br />
        </div>
    </div>
    <div class="panel panel-default" id="panel_grilla" runat="server">
        <div class="panel-body text-center">
            <div class="form-inline">
                <div class="row col-md-12 form-group">
                    <label for="BuscarPatente">Buscar Patente</label>
                    <asp:TextBox ID="BuscarPatente" class="form-control" runat="server"></asp:TextBox>
                    <asp:Button ID="Buscar" class="btn btn-default " CausesValidation="false" runat="server" Text="Buscar" OnClick="Buscar_Click" />
                    <asp:Button ID="Crear" class="btn btn-primary " CausesValidation="false" runat="server" Text="Crear" OnClick="Crear_Click" />
                </div>
            </div>
            <br />

            <div class="table" id="divGrilla" runat="server">
                <asp:GridView ID="GrillaCamiones" runat="server" Width="341px" Style="margin: auto" HorizontalAlign="Center" CellPadding="10" AutoGenerateColumns="False" OnSelectedIndexChanged="GrillaCamiones_SelectedIndexChanged">
                    <Columns>
                        <asp:CommandField SelectText="Seleccionar" ShowSelectButton="True" />
                        <asp:BoundField DataField="Patente" HeaderText="Patente" />
                        <asp:BoundField DataField="NroVehiculo" HeaderText="nroVehiculo" />
                        <asp:BoundField HeaderText="Marca" DataField="marca.Nombre" />
                        <asp:BoundField DataField="Modelo" HeaderText="Modelo" />
                        <asp:BoundField DataField="FechaCompra" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha de Compra" />
                        <asp:TemplateField HeaderText="Habilitado">
                            <ItemTemplate><%# (Boolean.Parse(Eval("Habilitado").ToString())) ? "Si" : "No" %></ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
            </div>

        </div>
    </div>


</asp:Content>
