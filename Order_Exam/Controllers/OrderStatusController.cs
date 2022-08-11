using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Order_Exam.Controllers
{
    public class OrderStatusController : Controller
    {
        // GET: OrderStatus
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Insert(ML.OrderStatus orderStatus)
        {
            try
            {
                var data = new BLL.OrderSatusBLL().Insert(orderStatus);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                //TODO: Log exeption
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult FindById(int orderStatusId)
        {
            try
            {
                var data = new BLL.OrderSatusBLL().FindById(orderStatusId);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                //TODO: Log exeption
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Update(ML.OrderStatus orderStatus)
        {
            try
            {
                var data = new BLL.OrderSatusBLL().Update(orderStatus);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                //TODO: Log exeption
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult DeleteById(int orderStatusId)
        {
            try
            {
                var data = new BLL.OrderSatusBLL().DeleteById(orderStatusId);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                //TODO: Log exeption
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult List()
        {
            try
            {
                var data = new BLL.OrderSatusBLL().List();
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