using Models.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class ImportBillDAO
    {
        WebCosmeticEntities db = new WebCosmeticEntities();

        public List<ImportBill> getAllImportBill()
        {
            //db.Configuration.ProxyCreationEnabled = false;
            List<ImportBill> list = db.ImportBills.ToList();
            return list;
        }
        public ImportBill getById(long id)
        {
            ImportBill importBill = new ImportBill();
            try
            {
                importBill = db.ImportBills.FirstOrDefault(x => x.ImpId == id);
                return importBill;
            }
            catch
            {
                return importBill;
            }
        }
        public bool Create(ImportBill import)
        {
            import.ImpDate = DateTime.Now;
            import.ImpId = int.Parse(import.ImpId.ToString());
            db.ImportBills.Add(import);
            db.SaveChanges();
            return true;
        }
        public bool Delete(long id)
        {
            try
            {
                var bill = db.ImportBills.Find(id);
                db.ImportBills.Remove(bill);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void Edit(ImportBill import)
        {
            ImportBill bill = db.ImportBills.Find(import.ImpId);
            if (bill != null)
            {
                bill.ImpId = import.ImpId;
                bill.PrvId = import.PrvId;
                bill.StaffId = import.StaffId;
                bill.ImpDate = import.ImpDate;
            }
            db.SaveChanges();
        }
    }
}
