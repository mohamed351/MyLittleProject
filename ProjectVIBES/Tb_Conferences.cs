//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjectVIBES
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tb_Conferences
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tb_Conferences()
        {
            this.Invoices = new HashSet<Invoice>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public string Persident_of_Conferences { get; set; }
        public System.DateTime from { get; set; }
        public System.DateTime to { get; set; }
        public string introdaction { get; set; }
        public bool close { get; set; }
        public int loginid { get; set; }
        public int customerid { get; set; }
        public int hotelid { get; set; }
        public string Note { get; set; }
    
        public virtual Tb_Customers Tb_Customers { get; set; }
        public virtual Tb_Hotels Tb_Hotels { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}