Imports DevExpress.DashboardCommon
Imports DevExpress.DashboardWeb
Imports DevExpress.DataAccess.ConnectionParameters
Imports DevExpress.DataAccess.Sql
Imports DevExpress.Web
Imports System
Imports System.Web

Namespace WebFormsCustomTextForInternalDashboardErrors

    Public Partial Class WebForm1
        Inherits UI.Page

        Shared Sub New()
            AddHandler ASPxWebControl.CallbackError, AddressOf ASPxWebControl_CallbackError
        End Sub

        Private Shared Sub ASPxWebControl_CallbackError(ByVal sender As Object, ByVal e As EventArgs)
            Dim server As HttpServerUtility = HttpContext.Current.Server
            ' The 'mode' attribute in the 'customErrors' section of the Web.config file specifies whether an application is in development mode.
            Dim isCustomErrorsEnabled As Boolean = If(HttpContext.Current IsNot Nothing, HttpContext.Current.IsCustomErrorEnabled, True)
            ASPxWebControl.SetCallbackErrorMessage(If(isCustomErrorsEnabled, "Custom exception text for end users", "Custom exception text for developers"))
        End Sub

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
            Dim dataSourceStrorage As DataSourceInMemoryStorage = New DataSourceInMemoryStorage()
            Dim sql As DashboardSqlDataSource = New DashboardSqlDataSource("sql", "sqlConn")
            sql.Queries.Add(SelectQueryFluentBuilder.AddTable("Products").SelectAllColumns().Build("query"))
            dataSourceStrorage.RegisterDataSource(sql.SaveToXml())
            ASPxDashboard1.SetDashboardStorage(New DashboardFileStorage(Server.MapPath("~/App_Data/Dashboards")))
            ASPxDashboard1.SetDataSourceStorage(dataSourceStrorage)
            AddHandler ASPxDashboard1.ConfigureDataConnection, AddressOf Me.ASPxDashboard1_ConfigureDataConnection
        End Sub

        Private Sub ASPxDashboard1_ConfigureDataConnection(ByVal sender As Object, ByVal e As ConfigureDataConnectionWebEventArgs)
            ' Invalid connection parameters:
            Select Case e.ConnectionName
                Case "sqlConn"
                    e.ConnectionParameters = New MsSqlConnectionParameters("localhost", "Northwind123", Nothing, Nothing, MsSqlAuthorizationType.Windows)
            End Select
        End Sub
    End Class
End Namespace
