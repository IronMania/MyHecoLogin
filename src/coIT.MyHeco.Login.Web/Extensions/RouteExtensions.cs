using System;
using Microsoft.AspNetCore.Mvc;

namespace coIT.MyHeco.Login.Web.Extensions
{
    public static class RouteExtensions
    {
        public static string AbsoluteAction(
            this IUrlHelper url,
            string actionName = null,
            string controllerName = null,
            object routeValues = null)
        {
            return url.Action(actionName, controllerName, routeValues, url.ActionContext.HttpContext.Request.Scheme);
        }
        public static string ControllerName(this Type controller)
        {
            return controller.Name.Replace("Controller", string.Empty);
        }
    }
}