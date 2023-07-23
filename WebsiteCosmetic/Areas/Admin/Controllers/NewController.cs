using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.DAO;
using Models.Framework;

namespace WebsiteCosmetic.Areas.Admin.Controllers
{
    public class NewController : Controller
    {
        WebCosmeticEntities db = new WebCosmeticEntities();
        NewsDAO newsDAO = new NewsDAO();
        // GET: Admin/New
        public ActionResult Index()
        {
            return View(newsDAO.getAllNew());
        }
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(News n, HttpPostedFileBase file)
        {
            string _FileName = " ";
            string _path = " ";
            try
            {
                if (file.ContentLength > 0)
                {
                    _FileName = Path.GetFileName(file.FileName);
                    _path = Path.Combine(Server.MapPath("~/FileUL"), _FileName);
                    file.SaveAs(_path);
                }
            }
            catch
            {

            }
            if (ModelState.IsValid)
            {
                n.Image = "/FileUL/" + _FileName;
                newsDAO.Create(n);
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName",n.UserId);
            return View(n);
        }
        public ActionResult Edit(long id)
        {
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName");
            return View(newsDAO.getById(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(News n)
        {
            if (ModelState.IsValid)
            {
                newsDAO.Edit(n);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Sửa không thành công!");
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", n.UserId);
            return View(n);
        }
        public ActionResult Delete(long id)
        {
            if (ModelState.IsValid)
            {
                newsDAO.Delete(id);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}