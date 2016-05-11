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
            // HUGMYNDIR AF ERRORS 
            // Finnur ekki síðu
            // Submission virkar ekki
            // 


            base.OnException(filterContext);
        }
    }
}