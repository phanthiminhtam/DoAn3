using Models.DAO;
using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebsiteCosmetic.Common;

namespace WebsiteCosmetic.Controllers
{
    public class GioHangController : Controller
    {
        WebCosmeticEntities db = new WebCosmeticEntities();
        // GET: GioHang
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]       
        
        public ActionResult Checkout()
        {
            var cart = Session["Cart"];
            var list = new List<OderDetail>();
            if (cart != null)
            {
                list = (List<OderDetail>)cart;
            }
            return View(list);
        }
        [HttpPost]
        public ActionResult Checkout(string Address, string Note, string Sodienthoai)
        {
            if (Session[CommonConstants.USER_SESSION] != null)
            { 
                //Cách thử lấy dữ liệu khi đăng nhập
                var customer = new Customer();
                var order = new Order();
                var check = db.Customers.FirstOrDefault(s => s.CusName == customer.CusName);
                if( check != null)
                {
                    order.PhoneNumber = check.PhoneNumber;
                    order.ReceivingAddress = check.Address;
                    order.OrderDate = DateTime.Now;
                    order.CusId = check.CusId;
                    order.Note = Note;
                }
                try
                {
                    var id = new OrderDAO().Insert(order);
                    var cart = (List<OderDetail>)Session["Cart"];
                    var detaiDao = new OrderDetailDAO();
                    foreach (var item in cart)
                    {
                        var orderDetail = new OderDetail();
                        orderDetail.SpId = item.SpecificProduct.SpId;
                        orderDetail.OrdId = id;
                        orderDetail.Price = item.SpecificProduct.Price;
                        orderDetail.Quantity = item.Quantity;
                        detaiDao.Insert(orderDetail);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi đặt hàng!");
                }
            }
            else
            {
                var order = new Order();
                order.ReceivingAddress = Address;
                order.OrderDate = DateTime.Now;
                order.Note = Note;
                order.PhoneNumber = Sodienthoai;
                var check = db.Customers.FirstOrDefault(s => s.PhoneNumber == order.PhoneNumber);
                if (check != null)
                {
                    order.CusId = check.CusId;
                }
                try
                {
                    var id = new OrderDAO().Insert(order);
                    var cart = (List<OderDetail>)Session["Cart"];
                    var detaiDao = new OrderDetailDAO();
                    foreach (var item in cart)
                    {
                        var orderDetail = new OderDetail();
                        orderDetail.SpId = item.SpecificProduct.SpId;
                        orderDetail.OrdId = id;
                        orderDetail.Price = item.SpecificProduct.Price;
                        orderDetail.Quantity = item.Quantity;
                        detaiDao.Insert(orderDetail);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi đặt hàng!");
                }
            }
            return Redirect("/GioHang/Successfull");
        }

        public ActionResult MyCcount()
        {
            return View();
        }
        public ActionResult Cart()
        {
            List<OderDetail> ds = (List<OderDetail>)Session["Cart"];
            List<OderDetail> cart = new List<OderDetail>();
            foreach (var sp in ds)
            {
                cart.Add(sp);
            }
            return View(cart);
        }
        public ActionResult Wishlist()
        {
            List<OderDetail> dsw = (List<OderDetail>)Session["CartWishList"];
            List<OderDetail> cartW = new List<OderDetail>();
            foreach (var sp in dsw)
            {
                cartW.Add(sp);
            }
            return View(cartW);
        }
        public JsonResult Update(string cartModel)
        {
            bool status ;
            var jsonCart = new JavaScriptSerializer().Deserialize<List<OderDetail>>(cartModel);
            var sessionCart = (List<OderDetail>)Session["Cart"];
            foreach(var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.SpecificProduct.ProId==item.SpecificProduct.ProId);
                if(jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }
            Session["Cart"] = sessionCart;
            return Json(new
            {
                status = true
            });
        }
        public JsonResult Delete(long id)
        {
            bool status;
            var sessionCart = (List<OderDetail>)Session["Cart"];
            var tg = sessionCart.FirstOrDefault(x => x.SpecificProduct.ProId == id);
            sessionCart.Remove(tg);
            Session["Cart"] = sessionCart;
            return Json(new
            {
                status = true
            });
        }
        public ActionResult Successfull()
        {
            return View();
        }
       
    }
}