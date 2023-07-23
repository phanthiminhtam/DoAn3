using Models.DAO;
using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebsiteCosmetic.Controllers
{
    public class SanPhamController : Controller
    {
        // GET: ChitietSP
        WebCosmeticEntities db = new WebCosmeticEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ChitietSP(int id)
        {
            var ds = db.SpecificProducts.FirstOrDefault(s => s.SpId == id);
            return View(ds);
        }
        public ActionResult Trangdiem()
        {
            return View();
        }
        public ActionResult Chamsocda()
        {
            return View();
        }
        public ActionResult Nuochoa()
        {
            return View();
        }
        public ActionResult Phukien()
        {
            return View();
        }
        public ActionResult AddCart(int id)
        {
            var sps = db.SpecificProducts.FirstOrDefault(s => s.SpId == id);
            if (Session["Cart"] != null)
            {
                List<OderDetail> cart = (List<OderDetail>)Session["Cart"];
                var kt = cart.FirstOrDefault(s => s.SpId == id);
                if (kt == null)
                {
                    OderDetail sp = new OderDetail() { SpId = id, Quantity = 1, SpecificProduct = sps };
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
                OderDetail sp = new OderDetail() { SpId = id, Quantity = 1, SpecificProduct = sps };
                cart.Add(sp);
                Session["Cart"] = cart;
            }
            return RedirectToAction("ChitietSP");
        }
        public PartialViewResult SPTrangDiem()
        {
            var ds = db.CosmeticLines.ToList();
            return PartialView(ds);
        }
        public PartialViewResult SPChamsocda()
        {
            var ds = db.CosmeticLines.ToList();
            return PartialView(ds);
        }
        public PartialViewResult SPPhukien()
        {
            var ds = db.CosmeticLines.ToList();
            return PartialView(ds);
        }
        public PartialViewResult SPNuochoa()
        {
            var ds = db.CosmeticLines.ToList();
            return PartialView(ds);
        }
        public PartialViewResult SPLienquan(int id)
        {
            var list = new SpecificProductDAO().getRelatedProduct(id);
            return PartialView(list);
        }

        public PartialViewResult ReviewDG()
        {
            var list = new ReviewDAO().getAllReview();
            return PartialView(list);
        }

        [HttpPost]
        public JsonResult SendReview(string phonenumber, string email, int vote, string content )
        {

            bool status;
            var review = new Review();
            review.PhoneNumber = phonenumber;
            review.Email = email;
            review.Vote = vote;
            review.Content = content;
            review.CreateDate = DateTime.Now;
            review.Status = false;
            var id = new ReviewDAO().Insert(review);
            if (id == true)
            {
                return Json(new
                {
                    status = true
                });
            }
            else
            {
                return Json(new
                {
                    status = false
                });
            }
        }
    }
}