﻿using DevExpress.DashboardCommon;
using DevExpress.DashboardWeb;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Sql;
using DevExpress.Web;
using System;
using System.Web;

namespace WebFormsCustomTextForInternalDashboardErrors {
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
            DataSourceInMemoryStorage dataSourceStrorage = new DataSourceInMemoryStorage();

            DashboardSqlDataSource sql = new DashboardSqlDataSource("sql", "sqlConn");
            sql.Queries.Add(SelectQueryFluentBuilder.AddTable("Products").SelectAllColumns().Build("query"));
            dataSourceStrorage.RegisterDataSource(sql.SaveToXml());

            ASPxDashboard1.SetDashboardStorage(new DashboardFileStorage(Server.MapPath("~/App_Data/Dashboards")));
            ASPxDashboard1.SetDataSourceStorage(dataSourceStrorage);
            ASPxDashboard1.ConfigureDataConnection += ASPxDashboard1_ConfigureDataConnection;
        }

        void ASPxDashboard1_ConfigureDataConnection(object sender, ConfigureDataConnectionWebEventArgs e) {
            // Invalid connection parameters:
            switch (e.ConnectionName) {
                case "sqlConn":
                    e.ConnectionParameters = new MsSqlConnectionParameters(@"localhost", "Northwind123", null, null, MsSqlAuthorizationType.Windows);
                    break;
            }
        }
    }
}