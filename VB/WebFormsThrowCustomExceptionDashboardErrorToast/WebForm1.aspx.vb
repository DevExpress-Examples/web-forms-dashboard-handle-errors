Imports System
Imports System.Collections.Generic
Imports System.Web
Imports System.Xml.Linq
Imports DevExpress.DashboardWeb
Imports DevExpress.Web

Namespace WebFormsThrowCustomExceptionDashboardErrorToast

    Public Partial Class WebForm1
        Inherits UI.Page

        Shared Sub New()
            AddHandler ASPxWebControl.CallbackError, AddressOf ASPxWebControl_CallbackError
        End Sub

        Private Shared Sub ASPxWebControl_CallbackError(ByVal sender As Object, ByVal e As EventArgs)
            Dim server As HttpServerUtility = HttpContext.Current.Server
            Dim exception As Exception = server.GetLastError()
            Dim isCustomErrorsEnabled As Boolean = If(HttpContext.Current IsNot Nothing, HttpContext.Current.IsCustomErrorEnabled, True)
            Dim customException As CustomException = TryCast(exception, CustomException)
            Dim message As String = If(customException IsNot Nothing, If(isCustomErrorsEnabled, CustomException.SafeMessage, CustomException.UnsafeMessage), "")
            ASPxWebControl.SetCallbackErrorMessage(message)
        End Sub

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
            ASPxDashboard1.SetDashboardStorage(New CustomDashboardStorage())
        End Sub
    End Class

    Public Class CustomException
        Inherits Exception

        Public Const SafeMessage As String = "Custom exception text for end users"

        Public Const UnsafeMessage As String = "Custom exception text for developers"
    End Class

    Public Class CustomDashboardStorage
        Implements IDashboardStorage

        Private Function GetAvailableDashboardsInfo() As IEnumerable(Of DashboardInfo) Implements IDashboardStorage.GetAvailableDashboardsInfo
            Return {New DashboardInfo With {.ID = "Dashboard", .Name = "Dashboard"}}
        End Function

        Private Function LoadDashboard(ByVal dashboardID As String) As XDocument Implements IDashboardStorage.LoadDashboard
            Throw New CustomException()
        End Function

        Private Sub SaveDashboard(ByVal dashboardID As String, ByVal dashboard As XDocument) Implements IDashboardStorage.SaveDashboard
        End Sub
    End Class
End Namespace
