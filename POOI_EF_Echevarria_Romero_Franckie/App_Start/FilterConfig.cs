using System.Web;
using System.Web.Mvc;

namespace POOI_EF_Echevarria_Romero_Franckie
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
