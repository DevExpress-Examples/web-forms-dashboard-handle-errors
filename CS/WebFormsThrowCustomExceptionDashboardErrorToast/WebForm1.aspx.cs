using System;
using System.Collections.Generic;
using System.Web;
using System.Xml.Linq;
using DevExpress.DashboardWeb;
using DevExpress.Web;

namespace WebFormsThrowCustomExceptionDashboardErrorToast {
    public partial class WebForm1 : System.Web.UI.Page {
        static WebForm1() {
            ASPxWebControl.CallbackError += ASPxWebControl_CallbackError;
        }
        static void ASPxWebControl_CallbackError(object sender, EventArgs e) {
            HttpServerUtility server = HttpContext.Current.Server;
            Exception exception = server.GetLastError();
            bool isCustomErrorsEnabled = HttpContext.Current != null ? HttpContext.Current.IsCustomErrorEnabled : true;
            CustomException customException = exception as CustomException;
            string message = customException != null ? (isCustomErrorsEnabled ? CustomException.SafeMessage : CustomException.UnsafeMessage) : "";
            ASPxWebControl.SetCallbackErrorMessage(message);
        }

        protected void Page_Load(object sender, EventArgs e) {
            ASPxDashboard1.SetDashboardStorage(new CustomDashboardStorage());
        }
    }

    public class CustomException : Exception {
        public const string SafeMessage = "Custom exception text for end users";
        public const string UnsafeMessage = "Custom exception text for developers";
    }

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
}