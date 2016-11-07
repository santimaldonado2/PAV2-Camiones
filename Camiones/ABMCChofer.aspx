<%@ Page Language="C#" MasterPageFile="~/Camiones.Master" AutoEventWireup="true" CodeBehind="ABMCChofer.aspx.cs" Inherits="Camiones.ABMCChofer" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>ABMC Chofer</title>
</asp:Content>
<asp:Content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <h2 class="col-sm-9 col-sm-offset-3">Formulario Choferes</h2>

    <div class="row-fluid col-sm-offset-3 col-sm-4">
        <div id="dialog" title="Mensaje" class="dialog">
            <p id="P1" runat="server"></p>
        </div>
        <div class="panel panel-default" id="formulario" runat="server">
            <div class="panel-body">
                <h2 class="col-sm-9 col-sm-offset-3" id="titulo" runat="server">Formulario Choferes</h2>
                <div class="row form-group col-md-12">
                    <label for="IdChofer">ID</label>
                    <asp:TextBox ID="IdChofer" name="IdChofer" class="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                </div>
                <div class="row form-group col-md-12">
                    <label for="NombreChofer">Nombre</label>
                    <asp:TextBox ID="NombreChofer" name="NombreChofer" class="form-control" runat="server" MaxLength="49"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFielValidatorNombre" ForeColor="red" runat="server" ControlToValidate="NombreChofer" EnableTheming="True" ErrorMessage="Nombre de chofer requerido"></asp:RequiredFieldValidator>
                </div>

                <div class="row col-md-12 form-group">

                    <label for="FechaNac">Fecha de Nacimiento</label>
                    <asp:TextBox ID="FechaNac" class="form-control datepicker" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorFecha" ForeColor="Red" runat="server" ControlToValidate="FechaNac" EnableTheming="True" ErrorMessage="Ingrese una fecha válida con el formato DD/MM/YYYY" SetFocusOnError="False" ValidationExpression="^(((((0[1-9])|(1\d)|(2[0-8]))\/((0[1-9])|(1[0-2])))|((31\/((0[13578])|(1[02])))|((29|30)\/((0[1,3-9])|(1[0-2])))))\/((20[0-9][0-9])|(19[0-9][0-9])))|((29\/02\/(19|20)(([02468][048])|([13579][26]))))$ "></asp:RegularExpressionValidator>
                    <asp:CompareValidator ID="FechaNacCompareValidator" ForeColor="Red" runat="server" Operator="LessThanEqual" ErrorMessage="La fecha de nacimiento debe ser menor al dia de la fecha"
                        ControlToValidate="FechaNac" Type="Date" />
                </div>
                <div class="row col-md-12 form-group">
                    <label for="NroDoc">Número de Documento</label>
                    <asp:TextBox ID="NroDoc" class="form-control" runat="server"></asp:TextBox><br />

                </div>
                <div class="row col-md-12 form-group">
                    <label for="SelectCiudad">Ciudad</label>
                    <asp:DropDownList ID="SelectCiudad" class="form-control" runat="server">
                    </asp:DropDownList>
                </div>
                <div class="row form-group col-md-12">
                    <asp:CheckBox ID="Activo" class="checkbox" Text="Activo" runat="server" MaxLength="10"/>
                </div>
            </div>

            <asp:Button ID="Guardar" class="btn btn-info" CausesValidation="true" runat="server" Text="Guardar" OnClick="Guardar_Click" />
            <asp:Button ID="Eliminar" class="btn  btn-danger" CausesValidation="false" runat="server" Text="Eliminar" OnClick="Eliminar_Click" />
            <asp:Label ID="Mensaje" runat="server" Text=""></asp:Label>
            </div>

            <div class="panel panel-default" id="panel_grilla" runat="server">
        <div class="panel-body text-center">
            <div class="form-inline">
                <div class="row col-md-12 form-group">
                    <label for="BuscarNombre">Buscar chofer por nombre</label>
                    <asp:TextBox ID="BuscarNombre" class="form-control" runat="server"></asp:TextBox>
                    <asp:Button ID="Buscar" class="btn btn-default " CausesValidation="false" runat="server" Text="Buscar" OnClick="Buscar_Click" />
                    <asp:Button ID="Crear" class="btn btn-primary " CausesValidation="false" runat="server" Text="Crear" OnClick="Crear_Click" />
                </div>
            </div>
            <br />

            <div class="table" id="divGrilla" runat="server">
                <asp:GridView ID="GrillaChoferes" runat="server" Width="341px" AutoGenerateColumns="False" OnSelectedIndexChanged="GrillaChoferes_SelectedIndexChanged" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" ForeColor="Black">
                    <Columns>
                        <asp:CommandField SelectText="Seleccionar" ShowSelectButton="True" />
                        <asp:BoundField DataField="NombreChofer" HeaderText="Nombre chofer" />
                        <asp:BoundField DataField="NroDoc" HeaderText="Nro Documento" />
                        <asp:BoundField HeaderText="Ciudad" DataField="ciudad.NombreCiudad" />
                        <asp:BoundField DataField="FechaNac" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha de Nacimiento" />
                        <asp:TemplateField HeaderText="Activo" >
                            <ItemTemplate><%# (Boolean.Parse(Eval("Activo").ToString())) ? "Si" : "No" %></ItemTemplate>
                        </asp:TemplateField>
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
            </div>
       
    </div>
</asp:Content>
