using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class ImportBillDetailDAO
    {
        WebCosmeticEntities db = null;
        public ImportBillDetailDAO()
        {
            db = new WebCosmeticEntities();
        }
        public List<ImportBillDetail> getAllIBDetail()
        {
            //db.Configuration.ProxyCreationEnabled = false;
            
            List<ImportBillDetail> list = db.ImportBillDetails.ToList();
            return list;
        }
        public ImportBillDetail getById(long id)
        {
            ImportBillDetail importBillDetail = new ImportBillDetail();
            try
            {
                importBillDetail = db.ImportBillDetails.FirstOrDefault(x => x.SpId == id);
                return importBillDetail;
            }
            catch
            {
                return importBillDetail;
            }
        }
        public bool Create(ImportBillDetail importD)
        {
            try
            {
                db.ImportBillDetails.Add(importD);
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
                var billDetail = db.ImportBillDetails.Find(id);
                db.ImportBillDetails.Remove(billDetail);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void Edit(ImportBillDetail importD)
        {
            ImportBillDetail billD = db.ImportBillDetails.Find(importD.ImpId);
            if (billD != null)
            {
                billD.SpId = importD.SpId;
                billD.ImpId = importD.ImpId;
                billD.ExpiredDate = importD.ExpiredDate;
                billD.Quantity = importD.Quantity;
                billD.ImportPrice = importD.ImportPrice;
                billD.MoneyTotal = importD.MoneyTotal;
            }
            db.SaveChanges();
        }
    }
}
