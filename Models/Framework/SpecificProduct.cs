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
    
    public partial class SpecificProduct
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SpecificProduct()
        {
            this.OderDetails = new HashSet<OderDetail>();
            this.ImportBillDetails = new HashSet<ImportBillDetail>();
        }
    
        public long ProId { get; set; }
        public long SpId { get; set; }
        public string Image { get; set; }
        public string Measure { get; set; }
        public string Type { get; set; }
        public double Price { get; set; }
        public System.DateTime StartedDate { get; set; }
        public System.DateTime StopDate { get; set; }
        public Nullable<double> Offer { get; set; }
        //public Nullable<int> Quantity { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public double DiscountPrice { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OderDetail> OderDetails { get; set; }
        public virtual Product Product { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ImportBillDetail> ImportBillDetails { get; set; }
    }
}
