using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Order_Exam.Controllers
{
    public class OrdersController : Controller
    {
        // GET: Orders
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult List()
        {
            List<ML.OrdersDetailViewModel> result = new BLL.OrderBLL().List();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CreateOrder(ML.Order order, List<ML.Product> products)
        {
            ML.Result result = new BLL.OrderBLL().Insert(order, products);
            return Json(result, JsonRequestBehavior.AllowGet);
        } 

        [HttpPost]
        public JsonResult SetOrderStatus(int orderId, int orderStatusId)
        {
            ML.Result result = new BLL.OrderBLL().SetOrderStatus(orderId, orderStatusId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}