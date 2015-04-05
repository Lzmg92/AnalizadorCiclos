<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Principal.aspx.vb" Inherits="AnalizadorCiclos.Principal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <link href ="Style.css" rel="stylesheet" type ="text/css" />
    <title>Analizador</title>
</head>
<body>
    <form id="form1" runat="server">
        
        <asp:TextBox ID="txtlex" CssClass ="textbox1" TextMode="MultiLine" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtentrada" CssClass ="textbox2" TextMode="MultiLine" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtsin" CssClass ="textbox3" TextMode="MultiLine" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtconsola" CssClass ="textbox4" TextMode="MultiLine" runat="server"></asp:TextBox>

        <asp:GridView ID="gridtabla" CssClass ="tabla" runat="server">
             
        </asp:GridView>
        
         


        <asp:Label ID="Label1" CssClass="tit1" runat="server" Text="Analizador Léxico Sintáctico"></asp:Label>
        <asp:Label ID="Label3" CssClass="titlex" runat="server" Text="Análisis Léxico"></asp:Label>
        <asp:Label ID="Label4" CssClass="titsin" runat="server" Text="Análisis Sintáctico"></asp:Label>
        <asp:Label ID="Label2" CssClass="titentrada" runat="server" Text="Texto de Entrada"></asp:Label>
        <asp:Label ID="Label5" CssClass="tittab" runat="server" Text="Tabla de Simbolos"></asp:Label>
        

        <asp:FileUpload ID="file" CssClass="btnfile" runat="server" />

        
         <asp:Button ID="btnanalizar" CssClass="btnanalizar" runat="server" Text="Analizar" />
    </form>
</body>
</html>
