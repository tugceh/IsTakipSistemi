//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVCIsTakipSistemi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class YetkiTurler
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public YetkiTurler()
        {
            this.Yetkiler = new HashSet<Yetkiler>();
        }
    
        public int yetkiTurId { get; set; }
        public string yetkiTurAd { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Yetkiler> Yetkiler { get; set; }
    }
}
