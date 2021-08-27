<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/267314153/20.1.3%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T893802)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
# Dashboard for Web Forms - How to handle errors

The following example demostrates two approaches on how to handle errors in the ASP.NET Web Forms Dashboard application:

- How to specify custom text for internal Dashboard errors
- How to throw a custom exception during a server-side processing and display the error in the Dashboard error toast

## How to specify custom text for internal Dashboard errors

<!-- default file list -->
*Files to look at*:
* [WebForm1.aspx.cs](./CS/WebFormsCustomTextForInternalDashboardErrors/WebForm1.aspx.cs) (VB: [WebForm1.aspx.cs](./VB/WebFormsCustomTextForInternalDashboardErrors/WebForm1.aspx.vb))
<!-- default file list end -->

The dashboard in this project contains invalid data connection. This example shows how to use the [ASPxWebControl.CallbackError](https://docs.devexpress.com/AspNet/DevExpress.Web.ASPxWebControl.CallbackError) event to override the default text in the exception. The exception occurs when a control tries to load data.

![](image/web-custom-text-for-internal-dashboard-errors.png)

Handle the `ASPxWebControl.CallbackError` event and specify the callback error message. The displayed text depends on whether the application is in development mode:

```cs
static void ASPxWebControl_CallbackError(object sender, EventArgs e) {
	HttpServerUtility server = HttpContext.Current.Server;
	Exception exception = server.GetLastError();  
	bool isCustomErrorsEnabled = HttpContext.Current != null ? HttpContext.Current.IsCustomErrorEnabled : true;
	ASPxWebControl.SetCallbackErrorMessage(isCustomErrorsEnabled ? "Custom exception text for end users" : "Custom exception text for developers");
}
```

## How to throw a custom exception during a server-side processing and display the error in the Dashboard error toast

<!-- default file list -->
*Files to look at*:
* [WebForm1.aspx.cs](./CS/WebFormsThrowCustomExceptionDashboardErrorToast/WebForm1.aspx.cs) (VB: [WebForm1.aspx.cs](./VB/WebFormsThrowCustomExceptionDashboardErrorToast/WebForm1.aspx.vb))
<!-- default file list end -->

This example shows how to throw a custom exception when a control loads a dashboard. This example uses the [ASPxWebControl.CallbackError](https://docs.devexpress.com/AspNet/DevExpress.Web.ASPxWebControl.CallbackError) event. The [ASPxWebControl.SetCallbackErrorMessage](https://docs.devexpress.com/AspNet/DevExpress.Web.ASPxWebControl.SetCallbackErrorMessage(System.String)) method specifies the message text to be displayed for remote clients when a DevExpress control callback error has occurred.

![](image/web-throw-custom-exception-dashboard-toast.png)

Handle the `ASPxWebControl.CallbackError` event and specify the callback error message. The displayed text depends on whether the application is in development mode:

```cs
static void ASPxWebControl_CallbackError(object sender, EventArgs e) {
	HttpServerUtility server = HttpContext.Current.Server;
	Exception exception = server.GetLastError();
	bool isCustomErrorsEnabled = HttpContext.Current != null ? HttpContext.Current.IsCustomErrorEnabled : true;
	CustomException customException = exception as CustomException;
	string message = customException != null ? (isCustomErrorsEnabled ? CustomException.SafeMessage : CustomException.UnsafeMessage) : "";
	ASPxWebControl.SetCallbackErrorMessage(message);
}
```

To throw an exception when the control loads a dashboard, create custom dashboard storage and override the `LoadDashboard` method:

```cs
public class CustomDashboardStorage : IDashboardStorage {
	IEnumerable<DashboardInfo> IDashboardStorage.GetAvailableDashboardsInfo() {
		return new[] {
			new DashboardInfo { ID = "Dashboard", Name = "Dashboard" }
		};
	}
	XDocument IDashboardStorage.LoadDashboard(string dashboardID) {
		throw new CustomException();
	}
	void IDashboardStorage.SaveDashboard(string dashboardID, XDocument dashboard) {
	}
}
```

## Documentation

- [Error Logging in Web Dashboard](https://docs.devexpress.com/Dashboard/400015/web-dashboard/error-logging)

## More Examples

- [ASP.NET MVC Dashboard - How to handle errors](https://github.com/DevExpress-Examples/asp-net-mvc-dashboard-change-default-error-text-onException)
- [ASP.NET Core Dashboard - How to handle errors](https://github.com/DevExpress-Examples/asp-net-core-dashboard-change-default-error-text-exception-filter)
