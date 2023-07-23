using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteCosmetic.Models;
using Models.DAO;
using BotDetect.Web.Mvc;
using WebsiteCosmetic.Common;
using System.Security.Principal;

namespace WebsiteCosmetic.Controllers
{
   
    public class TinTucController : Controller
    {
        WebCosmeticEntities db = new WebCosmeticEntities();

        // GET: TinTuc
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Review()
        {
            return View();
        }
        public ActionResult CSdoitra()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Logout()
        {
            Session[CommonConstants.USER_SESSION] = null;
            return RedirectToAction("Index","Home");
        }
        [HttpPost]
        public ActionResult Login(LoginCustomer account)
        {
            if (ModelState.IsValid)
            {
                var dao = new CustomerDAO();
                var result = dao.Login(account.UserName, Encryptor.MD5Hash(account.Password));
                if (result == 1)
                {
                    var user = dao.getByUsername(account.UserName);
                    var userSession = new UserLogin();
                    userSession.UserName = user.UserName;
                    userSession.UserID = user.CusId;
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    return RedirectToAction("Index","Home");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại!");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản bị khoá!");
                }
                else
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng!");
                }

            }
            return View(account);
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [CaptchaValidationActionFilter("CaptchaCode", "registerCaptcha", "Mã xác nhận không đúng!")]
        public ActionResult Register(RegisterModel model )
        {
            if(ModelState.IsValid)
            {
                var dao = new CustomerDAO();
                if(dao.CkeckUserName(model.UserName))
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại!");
                }
                else if (dao.CkeckEmail(model.Email))
                {
                    ModelState.AddModelError("", "Tên email đã tồn tại!");
                }
                else
                {
                    var customer = new Customer();
                    customer.CusName = model.UserName;
                    customer.Address = model.Address;
                    customer.PhoneNumber = model.PhoneNumber;
                    customer.Email = model.Email;
                    customer.UserName = model.UserName;
                    customer.PassWord = Encryptor.MD5Hash( model.PassWord);
                    var result = dao.Insert(customer);
                    if(result > 0)
                    {
                        ViewBag.Success = "Đăng ký thành công! ";
                        model = new RegisterModel();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Đăng ký không thành công!");
                        model = new RegisterModel();
                    }
                } 
                    
            }
            return View(model);
        }

        public PartialViewResult ReviewTT()
        {
            var list = new NewsDAO().getAllNew().Take(6);
            return PartialView(list);
        }
        public ActionResult ChitietBV(int id)
        {
            var ds = db.News.FirstOrDefault(s => s.NewId == id);
            return View(ds);
        }
    }
}