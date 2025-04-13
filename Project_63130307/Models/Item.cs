
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace Project_63130307.Models
{

using System;
    using System.Collections.Generic;
    
public partial class Item
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Item()
    {

        this.OrderDetails = new HashSet<OrderDetail>();

    }


    public long ID { get; set; }

    public string Name { get; set; }

    public Nullable<decimal> PurcharsePrice { get; set; }

    public decimal SellPrice { get; set; }

    public Nullable<System.DateTime> DateImport { get; set; }

    public Nullable<int> Quantity { get; set; }

    public Nullable<long> TypeID { get; set; }

    public string Picture { get; set; }

    public Nullable<bool> Active { get; set; }

    public string ShortTitle { get; set; }



    public virtual ItemType ItemType { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<OrderDetail> OrderDetails { get; set; }

}

}
