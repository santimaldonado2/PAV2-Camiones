<%@ Page Title="" Language="C#" MasterPageFile="~/Camiones.Master" AutoEventWireup="true" CodeBehind="ABMCCliente.aspx.cs" Inherits="Camiones.ABMCCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>ABMC Cliente</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    
    <div class="row-fluid col-sm-offset-3 col-sm-4">
        <div id="dialog" title="Mensaje" class="dialog">
            <p id="Mensaje" runat="server"></p>
        </div>
        <div class="panel panel-default" id="formulario" runat="server">
            <div class="panel-body">
                <h2 class="col-sm-9 col-sm-offset-3" id="titulo" runat="server">Formulario Clientes</h2>
                
                <div class="row form-group col-md-12">
                    <label for="IdCliente">ID</label>
                    <asp:TextBox ID="IdCliente" name="IdCliente" class="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                </div>
                
                <div class="row form-group col-md-12">
                    <label for="NombreCliente">Nombre de Cliente</label>
                    <asp:TextBox ID="NombreCliente" name="NombreCliente" class="form-control" runat="server" MaxLength="10"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorNombreCliente" ForeColor="Red" ControlToValidate="NombreCliente" runat="server" ErrorMessage="Ingrese el nombre del cliente"></asp:RequiredFieldValidator>
                </div>

                <div class="row form-group col-md-12">
                    <label for="NroCuit">Nro de Cuit</label>
                    <asp:TextBox ID="NroCuit" name="NroCuit" class="form-control" runat="server" MaxLength="10"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorNroCuit" ForeColor="Red" ControlToValidate="NroCuit" runat="server" ErrorMessage="Ingrese el número de cuit"></asp:RequiredFieldValidator>
                </div>
                
                
                <div class="row form-group col-md-12">
                    <asp:CheckBox ID="ClienteFijo" class="checkbox" Text="Cliente Fijo" runat="server" />
                </div>
                <div class="row col-md-12 form-group">

                    <label for="FechaInscripcion">Fecha de Inscripción</label>
                    <asp:TextBox ID="FechaInscripcion" class="form-control datepicker" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorFecha" ForeColor="Red" runat="server" ControlToValidate="FechaInscripcion" EnableTheming="True" ErrorMessage="Ingrese una fecha válida con el formato DD/MM/YYYY" SetFocusOnError="False" ValidationExpression="^(((((0[1-9])|(1\d)|(2[0-8]))\/((0[1-9])|(1[0-2])))|((31\/((0[13578])|(1[02])))|((29|30)\/((0[1,3-9])|(1[0-2])))))\/((20[0-9][0-9])|(19[0-9][0-9])))|((29\/02\/(19|20)(([02468][048])|([13579][26]))))$ "></asp:RegularExpressionValidator>
                    <asp:CompareValidator ID="FechaInscripcionCompareValidator" ForeColor="Red" runat="server" Operator="LessThanEqual" ErrorMessage="La fecha de inscripción debe ser menor al día de la fecha"
                        ControlToValidate="FechaInscripcion" Type="Date" />
                </div>
                
                <div class="row col-md-12 form-group">
                    <label for="SelectCiudad">Ciudad</label>
                    <asp:DropDownList ID="SelectCiudad" class="form-control" runat="server">
                    </asp:DropDownList>
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
                    <label for="BuscarNombre">Buscar por Nombre</label>
                    <asp:TextBox ID="BuscarNombre" class="form-control" runat="server"></asp:TextBox>
                    <asp:Button ID="Buscar" class="btn btn-default " CausesValidation="false" runat="server" Text="Buscar" OnClick="Buscar_Click" />
                    <asp:Button ID="Crear" class="btn btn-primary " CausesValidation="false" runat="server" Text="Crear" OnClick="Crear_Click" />
                </div>
            </div>
            <br />

            <div class="table" id="divGrilla" runat="server">
                <asp:GridView ID="GrillaClientes" runat="server" Width="341px" Style="margin: auto" HorizontalAlign="Center" CellPadding="10" AutoGenerateColumns="False" OnSelectedIndexChanged="GrillaClientes_SelectedIndexChanged">
                    <Columns>
                        <asp:CommandField SelectText="Seleccionar" ShowSelectButton="True" />
                        <asp:BoundField DataField="NombreCliente" HeaderText="Nombre Cliente" />
                        <asp:BoundField DataField="NroCuit" HeaderText="Nro cuit" />
                        <asp:BoundField HeaderText="Ciudad" DataField="ciudad.NombreCiudad" />
                        <asp:BoundField DataField="FechaInscripcion" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha de Inscripción" />
                        <asp:TemplateField HeaderText="Cliente fijo">
                            <ItemTemplate><%# (Boolean.Parse(Eval("ClienteFijo").ToString())) ? "Si" : "No" %></ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
            </div>

        </div>
    </div>


</asp:Content>
