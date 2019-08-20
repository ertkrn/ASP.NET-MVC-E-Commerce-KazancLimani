using proje1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TemplateArayisi.Classes
{
    public class BaseController : System.Web.Mvc.Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            //string controllerName = filterContext.RouteData.Values["controller"].ToString();
            //string actionName = filterContext.RouteData.Values["action"].ToString();
            ////bool iscontroller = controllerName == "Arabalar" && actionName == "Index";
            ////bool iscontroller1 = controllerName == "Arabalar" && actionName == "PersonelSatilanArabalar";
            ////bool iscontroller2 = controllerName == "Arabalar" && actionName == "PersonelKiralananArabalar";
            //bool iscontroller = (controllerName == "Yonetim" && actionName == "Index") ||
            //    (controllerName == "Arabalar" && actionName == "Index") ||
            //    (controllerName == "Arabalar" && actionName == "PersonelSatilanArabalar") ||
            //    (controllerName == "Arabalar" && actionName == "PersonelKiralananArabalar");


            //if (iscontroller && Session["loginp"]==null || Session["logink"] == null)
            //{
            if (Session["giris"] == null)
            {
                Kullanici klnc = Session["giris"] as Kullanici;
                if (!klnc.adminmi)
                {
                    filterContext.Result = new RedirectResult("~/Home/ErrorPage/");
                    return;
                }
            }

            //  }
            base.OnActionExecuting(filterContext);
        }
    }
}