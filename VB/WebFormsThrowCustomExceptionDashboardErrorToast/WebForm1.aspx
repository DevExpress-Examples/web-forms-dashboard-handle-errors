<%@ Page Language="vb" AutoEventWireup="true" CodeBehind="WebForm1.aspx.vb" Inherits="WebFormsThrowCustomExceptionDashboardErrorToast.WebForm1" %>

<%@ Register assembly="DevExpress.Dashboard.v20.1.Web.WebForms, Version=20.1.13.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.DashboardWeb" tagprefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
</head>
<body>
	<form id="form1" runat="server">
		<div style="position: absolute; top: 0; bottom: 0; right: 0; left: 0">
			<dx:ASPxDashboard ID="ASPxDashboard1" runat="server" Height="100%" Width="100%">
			</dx:ASPxDashboard>
		</div>
	</form>
</body>
</html>