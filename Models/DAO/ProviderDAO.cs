using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class ProviderDAO
    {
        WebCosmeticEntities db = null;
        public ProviderDAO()
        {
            db = new WebCosmeticEntities();
        }
        public List<Provider> getAllProvider()
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Provider> list = db.Providers.ToList();
            return list;
        }
        public Provider getById(long id)
        {
            Provider provider = new Provider();
            try
            {
                provider = db.Providers.FirstOrDefault(x => x.PrvId == id);
                return provider;
            }
            catch
            {
                return provider;
            }
        }
        public bool Create(Provider prv)
        {
            try
            {
                db.Providers.Add(prv);
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
                var p = db.Providers.Find(id);
                db.Providers.Remove(p);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Edit(Provider p)
        {
            try
            {
                Provider prv = db.Providers.Find(p.PrvId);
                if (prv != null)
                {
                    prv.PrvId = p.PrvId;
                    prv.PrvName = p.PrvName;
                    prv.Address = p.Address;
                    prv.Email = p.Email;
                    prv.PhoneNumber = p.PhoneNumber;
                }
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
