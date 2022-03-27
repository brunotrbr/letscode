
using kanban_api.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace kanban_api.Utils
{
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        
        public CustomExceptionFilter()
        {
            
        }

        public override void OnException(ExceptionContext context)
        {
            bool isPostPutDelete = context.HttpContext.Request.Method == "POST" || context.HttpContext.Request.Method == "PUT" || context.HttpContext.Request.Method == "DELETE";

            if (isPostPutDelete && context.Exception is BusinessException domain)
            {
                context.HttpContext.Response.ContentType = "application/json";

                context.HttpContext.Response.StatusCode = domain.StatusCode;
                context.Result = new JsonResult(domain.Fail);

                context.ExceptionHandled = true;
            }
            base.OnException(context);
        }
    }
}