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
    
    public partial class Cart
    {
        public long CartId { get; set; }
        public long CusId { get; set; }
    
        public virtual Customer Customer { get; set; }
    }
}