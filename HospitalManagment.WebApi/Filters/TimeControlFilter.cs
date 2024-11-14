using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HospitalManagment.WebApi.Filters
{
    public class TimeControlFilter : ActionFilterAttribute
    {
        public string StartTime { get; set; }
        public string EndTime { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {

            var now = DateTime.Now.TimeOfDay;

            StartTime = "09:00";
            EndTime = "17:00";

            if(now >= TimeSpan.Parse(StartTime) && now <= TimeSpan.Parse(EndTime))
            {
                base.OnActionExecuting(context);
            }
            else
            {
                context.Result = new ContentResult
                {
                    Content = "Appointments cannot be made between these hours.",
                    StatusCode = 403,
                };
            }
              


            base.OnActionExecuting(context);
        }
    }
}
