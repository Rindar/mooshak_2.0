using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mooshak_2._0.Error_Handling
{
    public class HandleError: HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            //Get the exception
            Exception ex = filterContext.Exception;
            Logger.Instance.LogException(ex);

            //Get current controller and action
            //Create the error model information
            string currentController = (string)filterContext.RouteData.Values["controller"];
            string currentActionName = (string)filterContext.RouteData.Values["action"];

            //Set the view name to be returned, maybe return
            //different error view for different exception types
            string viewName = "";

                HandleErrorInfo model = new
                HandleErrorInfo(filterContext.Exception, currentController, currentActionName);

                ViewResult result = new ViewResult
                {
                    ViewName = viewName,
                    ViewData = new
                        ViewDataDictionary<HandleErrorInfo>(model),
                    TempData = filterContext.Controller.TempData
                };

                filterContext.Result = result;
                filterContext.ExceptionHandled = true;

                //Call the base class implementation
                base.OnException(filterContext);
            }
        }
}