using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.DAO;
using Models.Framework;

namespace WebsiteCosmetic.Areas.Admin.Controllers
{
    public class ContactAdminController : Controller
    {
        // GET: Admin/ContactAdmin
        ContactDAO contactDAO = new ContactDAO();
        WebCosmeticEntities db = new WebCosmeticEntities();
        public ActionResult Index()
        {
            return View(contactDAO.getAllContact());
        }
        [HttpGet]
        public ActionResult ChitietEmail(int id)
        {
            var list = db.Contacts.FirstOrDefault(x => x.ContactId == id);
            return View(list);
        }
        public PartialViewResult Count()
        {
            return PartialView(contactDAO.getAllContact());
        }
    }
}