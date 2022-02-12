using IoT.DataLayer.Interface;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
namespace IoT.WebAPI.Filters
{
    public class UserKeyAttribute : ActionFilterAttribute
    {
        private IUsers users;
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            users = context.HttpContext.RequestServices.GetService<IUsers>();
            string headerKey = "UserKey";
            bool hasUserKey = context.HttpContext.Request.Headers.ContainsKey(headerKey);
            if (hasUserKey && users!=null)
            {
                string userKey = context.HttpContext.Request.Headers[headerKey].ToString();
                if (!string.IsNullOrEmpty(userKey))
                {
                    if (!users.CheckUser(userKey))
                    {
                        return;
                    }
                }
                return;

            }
            return;
        }
    }
}
