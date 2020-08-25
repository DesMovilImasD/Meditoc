using System.Web;
using System.Web.Mvc;

namespace IMD.Meditoc.CallCenter.Mx.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
