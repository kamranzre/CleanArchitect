using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Shop.Extensions
{
    public static class UrlHelperExtensions
    {
        public static string ActionEx<TController>(this IUrlHelper urlHelper, Expression<Action<TController>> action)
            where TController : Controller
        {
            var methodCall = (MethodCallExpression)action.Body;
            var actionName = methodCall.Method.Name; var controllerName = typeof(TController).Name.Replace("Controller", string.Empty);
            return urlHelper.Action(actionName, controllerName);
        }
    }
}
