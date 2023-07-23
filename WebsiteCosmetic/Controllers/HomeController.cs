using Microsoft.Ajax.Utilities;
using Models.DAO;
using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace WebsiteCosmetic.Controllers
{
    public class HomeController : Controller
    {
        WebCosmeticEntities db = new WebCosmeticEntities();
        SpecificProductDAO spDAO = new SpecificProductDAO();
        public ActionResult Index()
        {
            return View(spDAO.getAllSProduct());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public PartialViewResult MenuSanPham()
        {
            var ds = db.Categories.ToList();
            return PartialView(ds);
        }
        public PartialViewResult SPKhuyenMai()
        {
            var tg =  db.SpecificProducts.ToList();
            return PartialView(tg);
        }
        public ActionResult Khuyenmai()
        {
            return View();
        }
        public ActionResult SPMoive()
        {
            var tg = db.SpecificProducts.ToList();
            return PartialView(tg);
        }
        public PartialViewResult SPBanChay()
        {
            var tg = db.SpecificProducts.ToList();
            return PartialView(tg);
        }
        public ActionResult AddWishlist(int id)
        {
            var sps = db.SpecificProducts.FirstOrDefault(s => s.SpId == id);
            if (Session["CartWishList"] != null)
            {
                List<OderDetail> dsw = (List<OderDetail>)Session["CartWishList"];
                var kt = dsw.FirstOrDefault(s => s.SpId == id);
                if(kt == null)
                {
                    OderDetail sp = new OderDetail() { SpId = id, Quantity = 1, SpecificProduct = sps };
                    dsw.Add(sp);
                    Session["CartWishList"] = dsw;
                }
                else
                {
                    var sessionCart = (List<OderDetail>)Session["CartWishList"];
                    sessionCart.RemoveAll(x => x.SpecificProduct.ProId == id);
                    Session["CartWishList"] = sessionCart;
                }
                Session["CartWishList"] = dsw;
            }
            else
            {
                List<OderDetail> dsw = new List<OderDetail>();
                OderDetail sp = new OderDetail() { SpId = id, Quantity = 1, SpecificProduct = sps };
                dsw.Add(sp);
                Session["CartWishList"] = dsw;
            }
            return RedirectToAction("Index");
        }
       
        public ActionResult AddCart(int id)
        {
            var sps = db.SpecificProducts.FirstOrDefault(s => s.SpId == id);
            if (sps.Offer == null) sps.Offer = 0;
            OderDetail sp = new OderDetail()
            {
                SpId = id,
                Quantity = 1,
                SpecificProduct = new SpecificProduct()
                {
                    Price = sps.Price,
                    Product = new Product() { ProName = sps.Product.ProName },
                    Image = sps.Image,
                    Offer = sps.Offer,
                    DiscountPrice = (double)(sps.Price - (double)sps.Price * sps.Offer)
                }
            };
            if (Session["Cart"] != null)
            {
                List<OderDetail> cart = (List<OderDetail>)Session["Cart"];
                var kt = cart.FirstOrDefault(s => s.SpId == id);
                if (kt == null)
                {
                    
                    cart.Add(sp);
                    Session["Cart"] = cart;
                }
                else
                {
                    kt.Quantity = kt.Quantity + 1;
                }
                Session["Cart"] = cart;
            }
            else
            {
                List<OderDetail> cart = new List<OderDetail>();
                cart.Add(sp);
                Session["Cart"] = cart;
            }
            double totalMoney = 0;
            List<OderDetail> list = (List<OderDetail>)Session["Cart"];
            foreach (var item in list)
            {
                totalMoney += item.SpecificProduct.DiscountPrice * item.Quantity;
            }
            
            return Json(new
            {           
                totalMoney = totalMoney,
                list = list
            },JsonRequestBehavior.AllowGet) ;
        }
        public PartialViewResult HeaderCart()
        {
            var cart = Session["Cart"];
            var list = new List<OderDetail>();
            if (cart != null)
            {
                list = (List<OderDetail>)cart;
            }
            return PartialView(list);
        }
        public PartialViewResult CartLike()
        {
            var cart = Session["CartWishList"];
            var list = new List<OderDetail>();
            if (cart != null)
            {
                list = (List<OderDetail>)cart;
            }
            return PartialView(list);
        }
        public PartialViewResult CartMini()
        {
            var cart = Session["Cart"];
            var list = new List<OderDetail>();
            if (cart != null)
            {
                list = (List<OderDetail>)cart;
            }
            return PartialView(list);
        }
        public PartialViewResult CartWishlist()
        {
            var cart = Session["CartWishList"];
            var list = new List<OderDetail>();
            if (cart != null)
            {
                list = (List<OderDetail>)cart;
            }
            return PartialView(list);
        }
        [HttpPost]
        public JsonResult DeleteCartMini(long id)
        {
            bool status;
            var sessionCart = (List<OderDetail>)Session["Cart"];
            sessionCart.RemoveAll(x => x.SpecificProduct.ProId == id);
            Session["Cart"] = sessionCart;
            return Json(new
            {
                status = true
            });
        }
        [HttpPost]
        public JsonResult Send(string name, string email, string mobile,string content)
        {
            bool status;
            var contact = new Contact();
            contact.ContactName = name;
            contact.Email = email;
            contact.PhoneNumber = mobile;
            contact.Content = content;
            contact.CreateDate = DateTime.Now;

            var id = new ContactDAO().Insert(contact);
            if (id > 0)
                return Json(new
                {
                    status = true
                });
            else
                return Json(new
                {
                    status = false
                }); ;
        }

        public PartialViewResult TTNoiBat()
        {
            var list = new NewsDAO().getlast();
            return PartialView(list);
        }
        public JsonResult ListName(string q)
        {
            var data = new ProductDAO().ListSearch(q);
            return Json(new
            {
                data = data,
                status = true   
            }, JsonRequestBehavior.AllowGet);
        }
    }
}