using System;
using System.Collections.Generic;
using System.EnterpriseServices.CompensatingResourceManager;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BotDetect;
using Models.DAO;
using Models.Framework;
using Newtonsoft.Json.Linq;

namespace WebsiteCosmetic.Areas.Admin.Controllers
{
    public class ReviewController : Controller
    {
        // GET: Admin/Review
        ReviewDAO reviewDAO = new ReviewDAO();
        WebCosmeticEntities db = new WebCosmeticEntities();
        public ActionResult Index()
        {
            return View(reviewDAO.getAllReview());
        }
        public ActionResult ChangeStatus(long id)
        {
            try
            {
                var status = reviewDAO.ChangeStatus(id);
                return Json(new
                {
                    check = true,
                    statusReview = status
                }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new
                {
                    check = false
                }, JsonRequestBehavior.AllowGet);
            }

            
        }
    }
}