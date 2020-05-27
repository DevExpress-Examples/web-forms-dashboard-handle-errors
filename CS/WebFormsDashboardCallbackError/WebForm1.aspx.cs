using DevExpress.DashboardCommon;
using DevExpress.DashboardWeb;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Sql;
using DevExpress.Web;
using System;
using System.Web;

namespace WebFormsDashboardCallbackError {
    public partial class WebForm1 : System.Web.UI.Page {
        static WebForm1() {
            ASPxWebControl.CallbackError += ASPxWebControl_CallbackError;
        }
        static void ASPxWebControl_CallbackError(object sender, EventArgs e) {
            HttpServerUtility server = HttpContext.Current.Server;
            // The 'mode' attribute in the 'customErrors' section of the Web.config file specifies whether an application is in development mode.
            bool isCustomErrorsEnabled = HttpContext.Current != null ? HttpContext.Current.IsCustomErrorEnabled : true;
            ASPxWebControl.SetCallbackErrorMessage(isCustomErrorsEnabled ? "Custom exception text for end users" : "Custom exception text for developers");
        }

        protected void Page_Load(object sender, EventArgs e) {
            ASPxDashboard1.UseDashboardConfigurator = true;

            DashboardFileStorage dashboardStorage = new DashboardFileStorage(Server.MapPath("~/App_Data/Dashboards"));
            DataSourceInMemoryStorage dataSourceStrorage = new DataSourceInMemoryStorage();

            DashboardSqlDataSource sql = new DashboardSqlDataSource("sql");
            sql.Queries.Add(SelectQueryFluentBuilder.AddTable("Products").SelectAllColumns().Build("query"));
            dataSourceStrorage.RegisterDataSource(sql.SaveToXml());

            DashboardConfigurator.Default.SetDashboardStorage(dashboardStorage);
            DashboardConfigurator.Default.SetDataSourceStorage(dataSourceStrorage);
            DashboardConfigurator.Default.ConfigureDataConnection += ASPxDashboard1_ConfigureDataConnection;

        }

        void ASPxDashboard1_ConfigureDataConnection(object sender, ConfigureDataConnectionWebEventArgs e) {
            // Invalid connection parameters:
            switch (e.DataSourceName) {
                case "sql":
                    e.ConnectionParameters = new MsSqlConnectionParameters(@"teamdashboard\sql2008", "Northwind123", null, null, MsSqlAuthorizationType.Windows);
                    break;
            }
        }
    }
}