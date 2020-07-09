using log4net.Config;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace IMD.Meditoc.Pagos.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            string logger = ConfigurationManager.AppSettings["IMD_LOGGER"];
            if (Convert.ToBoolean(logger))
            {
                XmlConfigurator.Configure();
            }
        }
    }
}
