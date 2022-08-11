using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Order_Exam.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult List()
        {
            try
            {
                var data = new BLL.ProductBLL().List();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                //TODO: Log exeption
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
    }
}