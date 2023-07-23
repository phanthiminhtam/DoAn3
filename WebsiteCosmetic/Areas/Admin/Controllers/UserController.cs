using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.DAO;
using Models.Framework;
using WebsiteCosmetic.Common;

namespace WebsiteCosmetic.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        // GET: Admin/User
        UserDAO userDAO = new UserDAO();
        [HasCredential(RoleID = "VIEW_USER")]
        public ActionResult Index()
        {
            return View(userDAO.getAllUser());
        }
        [HasCredential(RoleID = "ADD_USER")]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [HasCredential(RoleID = "ADD_USER")]
        public ActionResult Create(User user)
        {  
            if (ModelState.IsValid)
            {
                var password = Encryptor.MD5Hash(user.PassWord);
                user.PassWord = password;
                userDAO.Insert(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }
        [HasCredential(RoleID = "DELETE_USER")]
        public ActionResult Delete(long id)
        {
            if (ModelState.IsValid)
            {
                userDAO.Delete(id);
                return RedirectToAction("Index");
            }
            return View();
        }
        [HasCredential(RoleID = "EDIT_USER")]
        public ActionResult Edit(long id)
        {
            User u = userDAO.getByIdUser(id);
            return View(u);
        }
        [HttpPost]
        [HasCredential(RoleID = "EDIT_USER")]
        public ActionResult Edit(User users)
        {
            if (ModelState.IsValid)
            {
                userDAO.Edit(users);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Sửa không thành công!");
            }
            return View(users);
        }
    }
}