using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class ContactDAO
    {
        WebCosmeticEntities db = null;
        public ContactDAO()
        {
            db = new WebCosmeticEntities();
        }
        public long Insert(Contact ct)
        {
            db.Contacts.Add(ct);
            db.SaveChanges();
            return ct.ContactId;
        }
        public List<Contact> getAllContact()
        {
            //db.Configuration.ProxyCreationEnabled = false;
            List<Contact> contact = db.Contacts.ToList();
            return contact;
        }
    }
}
