using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.DAO;
using Models.Framework;
using Newtonsoft.Json;
using System.Web.Services.Description;

namespace WebsiteCosmetic.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        // GET: Admin/Order
        OrderDAO orderDAO = new OrderDAO();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult getAllOrder()
        {
            WebCosmeticEntities db = new WebCosmeticEntities();
            OrderDAO order = new OrderDAO();
            var list = order.getAllOrder();
            var result = list.Select(s => new Order() { OrdId = s.OrdId, PhoneNumber = s.PhoneNumber, ReceivingAddress = s.ReceivingAddress, OrderDate = s.OrderDate }).ToList();
            return Json(JsonConvert.SerializeObject(result), JsonRequestBehavior.AllowGet);
        }
    }
}