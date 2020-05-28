# ASP.NET Web Forms Dashboard - How to specify custom exception text (ASPxWebControl.CallbackError)

<!-- default file list -->
*Files to look at*:
* [WebForm1.aspx.cs](./CS/WebFormsDashboardCallbackError/WebForm1.aspx.cs) (VB: [WebForm1.aspx.cs](./VB/WebFormsDashboardCallbackError/WebForm1.aspx.vb))
<!-- default file list end -->

The dashboard in this project contains invalid data connection. This example shows how to use the [ASPxWebControl.CallbackError](https://docs.devexpress.com/AspNet/DevExpress.Web.ASPxWebControl.CallbackError) event to override the default text in the exception. The exception occurs when a control tries to load data.

![](image/web-exception-on-data-loading.png)

Handle the `ASPxWebControl.CallbackError` event and specify the callback error message. The displayed text depends on whether the application is in development mode:

```cs
static void ASPxWebControl_CallbackError(object sender, EventArgs e) {
	HttpServerUtility server = HttpContext.Current.Server;
	Exception exception = server.GetLastError();  
	bool isCustomErrorsEnabled = HttpContext.Current != null ? HttpContext.Current.IsCustomErrorEnabled : true;
	ASPxWebControl.SetCallbackErrorMessage(isCustomErrorsEnabled ? "Custom exception text for end users" : "Custom exception text for developers");
}
```
