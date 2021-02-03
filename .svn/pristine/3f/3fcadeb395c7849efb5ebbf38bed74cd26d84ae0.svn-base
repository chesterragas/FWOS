using System.Web.Mvc;

namespace ROHM_WOS.Areas.WorkOrderApproval
{
    public class WorkOrderApprovalAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "WorkOrderApproval";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "WorkOrderApproval_default",
                "WorkOrderApproval/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}