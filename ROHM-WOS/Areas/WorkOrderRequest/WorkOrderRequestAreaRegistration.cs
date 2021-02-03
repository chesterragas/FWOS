using System.Web.Mvc;

namespace ROHM_WOS.Areas.WorkOrderRequest
{
    public class WorkOrderRequestAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "WorkOrderRequest";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "WorkOrderRequest_default",
                "WorkOrderRequest/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}