using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace PatientForm.Api.Filters;

public class LogActionFilter() : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext filterContext) => 
    Log.Information("Executing endpoint {endpoint}", filterContext.HttpContext.Request.GetDisplayUrl());
}