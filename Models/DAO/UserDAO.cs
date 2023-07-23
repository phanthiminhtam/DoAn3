using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Framework;
using Models.BaseCommon;
namespace Models.DAO
{
    public class UserDAO
    {
        WebCosmeticEntities db = null;
        public UserDAO()
        {
            db= new WebCosmeticEntities();
        }
        public List<User> getAllUser()
        {
            //db.Configuration.ProxyCreationEnabled = false;

            List<User> list = db.Users.ToList();
            return list;
        }
        public User getByUsername(string  userName)
        {
            return db.Users.SingleOrDefault(u => u.UserName == userName);
        }
        public void Insert(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
        }
        public List<string> GetListCredential(string userName)
        {
            var user = db.Users.Single(x => x.UserName == userName);
            var data = (from a in db.Credentials
                       join b in db.UserGroups on a.UserGroupID equals b.ID
                       join c in db.Roles on a.RoleID equals c.ID
                       where b.ID == user.GroupID
                       select new 
                       {
                           RoleID = a.RoleID,
                            UserGroupID = a.UserGroupID

                        }).AsEnumerable().Select(x=>new Credential()
                        {
                            RoleID = x.RoleID,
                            UserGroupID = x.UserGroupID
                        });
            return data.Select(x => x.RoleID).ToList();
        }
        public int Login(string userName, string passWord, bool isLoginAdmin = false)
        {
            var result = db.Users.SingleOrDefault(x => x.UserName == userName);
            if(result == null)
            {
                return 0; //tài khoản không tồn tại
            }
            else
            {
                if(isLoginAdmin == true)
                {
                    if(result.GroupID == CommonConstants.ADMIN_GROUP || result.GroupID == CommonConstants.BOSS_GROUP)
                    {
                        if (result.Active == false)
                        {
                            return -1; //tài khoản bị khoá
                        }
                        else
                        {
                            if (result.PassWord == passWord)
                            {
                                return 1;
                            }
                            else
                            {
                                return -2;// Mật khẩu không đúng
                            }
                        }
                    }
                    else
                    {
                        return -3;
                    }
                }
                else
                {
                    if (result.Active == false)
                    {
                        return -1; //tài khoản bị khoá
                    }
                    else
                    {
                        if (result.PassWord == passWord)
                        {
                            return 1;
                        }
                        else
                        {
                            return -2;// Mật khẩu không đúng
                        }
                    }
                }
              
            }
        }
        public bool Delete(long id)
        {
            try
            {
                var user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void  Edit(User user)
        {
            var u = db.Users.Find(user.UserId);
            if(u!= null)
            {
                u.UserName = user.UserName;
                u.PassWord = user.PassWord;
                u.Active = user.Active;
            }             
            db.SaveChanges();
        }
        public bool ChangeSattus(long id)
        {
            try
            {
                User u = db.Users.Find(id);
                u.Active = !u.Active;
                db.SaveChanges();                
                return true;
            }
            catch
            {
                return false;
            }
        }
        public User getByIdUser(long id)
        {
            return db.Users.Find(id);
        }
    }
}
