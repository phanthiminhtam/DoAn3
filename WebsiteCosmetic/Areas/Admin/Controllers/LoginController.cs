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
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(User account)
        {
            if(ModelState.IsValid)
            {
                var dao = new UserDAO();
                var result = dao.Login(account.UserName,Encryptor.MD5Hash(account.PassWord), true);
                if(result == 1)
                {
                    var user = dao.getByUsername(account.UserName);
                    var userSession = new UserLogin();
                    userSession.UserName = user.UserName;
                    userSession.UserID = user.UserId;

                    var listCredentials = dao.GetListCredential(account.UserName);

                    Session.Add(CommonConstants.SESSION_CREDENTIALS, listCredentials);    
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    return RedirectToAction("Index", "Home2");
                }
                else if(result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại!");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản bị khoá!");
                }
                else if (result == -3)
                {
                    ModelState.AddModelError("", "Tài khoản của bạn không có quyền đăng nhập!");
                }
                else
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng!");
                }

            }
            return View("Index");
        }
    }
}