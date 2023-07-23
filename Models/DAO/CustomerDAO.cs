using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class CustomerDAO
    {
        WebCosmeticEntities db = null;
        public CustomerDAO()
        {
            db = new WebCosmeticEntities();
        }

        public List<Customer> getAllCustomer()
        {
            //db.Configuration.ProxyCreationEnabled = false;

            List<Customer> list = db.Customers.ToList();
            return list;
        }
        public long Insert(Customer customer)
        {
            db.Customers.Add(customer);
            db.SaveChanges();
            return customer.CusId;
        }
        public bool CkeckUserName(string userName)
        {
            return db.Customers.Count(x => x.UserName == userName) > 0;
        }
        public bool CkeckEmail(string email)
        {
            return db.Customers.Count(x => x.Email == email) > 0;
        }
        public Customer getByUsername(string userName)
        {
            return db.Customers.SingleOrDefault(u => u.UserName == userName);
        }
        public int Login(string userName, string passWord)
        {
            var result = db.Customers.SingleOrDefault(x => x.UserName == userName);
            if (result == null)
            {
                return 0; //tài khoản không tồn tại
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
        public bool Delete(long id)
        {
            try
            {
                var cus = db.Customers.Find(id);
                db.Customers.Remove(cus);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
