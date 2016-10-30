<%@ Page Title="" Language="C#" MasterPageFile="~/Camiones.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Login.aspx.cs" Inherits="Camiones.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" style="width:330px;">
        <form class="form-signin">
            <h2 class="form-signin-heading">Por favor inicie sesión</h2>
            <label for="inputEmail" class="sr-only">Usuario</label>
            <asp:TextBox type="text" id="usuario" class="form-control" placeholder="Usuario" required="" autofocus="" runat="server"></asp:TextBox>
            <label for="inputPassword" class="sr-only">Contraseña</label>
            <asp:TextBox type="password" id="inputPassword" class="form-control" placeholder="Contraseña" required="" runat="server"></asp:TextBox>
            <asp:Button ID="iniciar_sesion" runat="server" class="btn btn-lg btn-primary btn-block" type="submit" Text="Iniciar Sesión" OnClick="iniciar_sesion_Click" CausesValidation="false" />
            <asp:Label ID="mensaje" runat="server" Text=""></asp:Label>
        </form>
    </div>
</asp:Content>
