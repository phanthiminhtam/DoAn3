using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class StaffDAO
    {
        WebCosmeticEntities db = null;
        public StaffDAO()
        {
            db = new WebCosmeticEntities();
        }
        public List<Staff> getAllStaff()
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Staff> list = db.Staffs.ToList();
            return list;
        }
        public Staff getById(long id)
        {
            Staff staff = new Staff();
            try
            {
                staff = db.Staffs.FirstOrDefault(x => x.StaffId == id);
                return staff;
            }
            catch
            {
                return staff;
            }
        }
        public bool Create(Staff staff)
        {
            try
            {
                db.Staffs.Add(staff);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Delete(long id)
        {
            try
            {
                var s = db.Staffs.Find(id);
                db.Staffs.Remove(s);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void Edit(Staff s)
        {
            Staff st = db.Staffs.Find(s.StaffId);
            if (st != null)
            {
                st.StaffId = s.StaffId;
                st.StaffName = s.StaffName;
                st.PhoneNumber = s.PhoneNumber;
                st.Address = s.Address;
                st.ContractWork = s.ContractWork;
                st.Email = s.Email;
                st.BasicSalary = s.BasicSalary;
                st.Post = s.Post;
                st.StartDate = s.StartDate;
                st.TypeWork = s.TypeWork;
            }
            db.SaveChanges();
        }
    }
}
