//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    
    public partial class ImportBillDetail
    {
        public int Quantity { get; set; }
        public double ImportPrice { get; set; }
        public System.DateTime ExpiredDate { get; set; }
        public Nullable<long> ImpId { get; set; }
        public long SpId { get; set; }
        public Nullable<double> MoneyTotal { get; set; }
    
        public virtual ImportBill ImportBill { get; set; }
        public virtual SpecificProduct SpecificProduct { get; set; }
    }
}