<%@ Page Language="C#" MasterPageFile="~/Camiones.Master" AutoEventWireup="true" CodeBehind="RegistroPago.aspx.cs" Inherits="Camiones.RegistroPago" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Registrar pago</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    
    <div class="row-fluid col-sm-offset-3 col-sm-4">
        <div id="dialog" title="Mensaje" class="dialog">
            <p id="Mensaje" runat="server"></p>
        </div>
        <div class="panel panel-default" id="formulario" runat="server">
            <div class="panel-body">
                <h2 class="col-sm-9 col-sm-offset-3" id="titulo" runat="server">Formulario registro</h2>
                
                <div class="row col-md-12 form-group">
                    <label for="SelectChofer">Chofer</label>
                    <asp:DropDownList ID="SelectChofer" class="form-control" runat="server">
                    </asp:DropDownList>
                </div>
                <div class="row col-md-12 form-group">
                    <label for="FechaPago">Fecha de Pago</label>
                    <asp:TextBox ID="FechaPago" class="form-control datepicker" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorFecha" ForeColor="Red" runat="server" ControlToValidate="FechaPago" EnableTheming="True" ErrorMessage="Ingrese una fecha válida con el formato DD/MM/YYYY" SetFocusOnError="False" ValidationExpression="^(((((0[1-9])|(1\d)|(2[0-8]))\/((0[1-9])|(1[0-2])))|((31\/((0[13578])|(1[02])))|((29|30)\/((0[1,3-9])|(1[0-2])))))\/((20[0-9][0-9])|(19[0-9][0-9])))|((29\/02\/(19|20)(([02468][048])|([13579][26]))))$ "></asp:RegularExpressionValidator>
                    
                </div>
                <div class="row form-group col-md-12">
                    <label for="MontoTotal">Monto total</label>
                    <asp:TextBox ID="MontoTotal" name="MontoTotal" class="form-control" runat="server" MaxLength="10"></asp:TextBox>
                </div>
                </div>
                </div>
            <div class="panel panel-default" id="formDetalle" runat="server">
                <div class="panel-body">
                <div class="row col-md-12 form-group">
                    <label for="IdViaje">Número de Viaje</label>
                    <asp:DropDownList ID="SelectViaje" class="form-control" runat="server">
                    </asp:DropDownList>                  
                    
                </div>
                <div class="row form-group col-md-12">
                    <label for="Monto">Monto</label>
                    <asp:TextBox ID="Monto" name="Monto" class="form-control" runat="server" MaxLength="10"></asp:TextBox>
                </div>
                    <div class="row form-group col-md-12">
                    <label for="DescuentoAdelanto">Descuento Adelanto</label>
                    <asp:TextBox ID="DescuentoAdelanto" name="DescuentoAdelanto" class="form-control" runat="server" MaxLength="10"></asp:TextBox>
                </div>
            </div>

            <asp:Button ID="Agregar" class="btn btn-info abreDialog" OnClientClick="openDialog()" CausesValidation="true" runat="server" Text="Agregar" OnClick="Agregar_Click" />
            
            <br />
        </div>
    </div>
    <div class="panel panel-default" id="panel_grilla" runat="server">
        <div class="panel-body text-center">
          
            <div class="table" id="divGrilla" runat="server">
                <asp:GridView ID="GrillaPagos" runat="server" Width="341px" Style="margin: auto" HorizontalAlign="Center" CellPadding="10" AutoGenerateColumns="False" OnSelectedIndexChanged="GrillaPagos_SelectedIndexChanged">
                    <Columns>
                        <asp:BoundField DataField="IdViaje" HeaderText="Id Viaje" />
                        <asp:BoundField DataField="Monto" HeaderText="Monto" />
                        <asp:BoundField HeaderText="DescuentoAdicional" DataField="DescuentoAdicional" />
                            
                    </Columns>
                </asp:GridView>
            </div>
            <asp:Button ID="Registrar" class="btn btn-info abreDialog" OnClientClick="openDialog()" CausesValidation="true" runat="server" Text="Registrar" OnClick="Registrar_Click" />

        </div>
    </div>


</asp:Content>
