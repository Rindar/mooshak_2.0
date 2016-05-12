using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mooshak_2._0.ErrorHandler
{
    public class ErrorAttributeHandler : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            Exception e = filterContext.Exception;

            Logger.Instance.Log(e);

            string currentController = (string)filterContext.RouteData.Values["controller"];
            string currentActionName = (string)filterContext.RouteData.Values["action"];

            string viewName = "";

            // Data doesn't exist
            if (e is Exception)
            {
                viewName = "Error";
            }
            else
            {
                viewName = "Error";
            }

            HandleErrorInfo model = new HandleErrorInfo(filterContext.Exception,
                                                        currentController, currentActionName);

            ViewResult result = new ViewResult
            {
                ViewName = viewName,
                ViewData = new ViewDataDictionary<HandleErrorInfo>(model),
                TempData = filterContext.Controller.TempData
            };

            filterContext.Result = result;
            filterContext.ExceptionHandled = true;

            base.OnException(filterContext);
        }
    }
}

