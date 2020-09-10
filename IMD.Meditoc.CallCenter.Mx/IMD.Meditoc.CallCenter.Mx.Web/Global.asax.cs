using log4net.Config;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace IMD.Meditoc.CallCenter.Mx.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            JsonMediaTypeFormatter json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            json.SerializerSettings.Culture = System.Globalization.CultureInfo.CurrentCulture;

            string logger = ConfigurationManager.AppSettings["IMD_LOGGER"];
            if (Convert.ToBoolean(logger))
            {
                XmlConfigurator.Configure();
            }
        }
    }
}
