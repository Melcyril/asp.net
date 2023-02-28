using System.Web;
using System.Web.Mvc;

namespace Site_Roll_On_Blb
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
