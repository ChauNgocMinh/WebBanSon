
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
    
public partial class Order
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Order()
    {

        this.OrderDetails = new HashSet<OrderDetail>();

        this.Payments = new HashSet<Payment>();

    }


    public long ID { get; set; }

    public Nullable<System.DateTime> Orderdate { get; set; }

    public Nullable<System.DateTime> Deliverydate { get; set; }

    public Nullable<int> Status { get; set; }

    public Nullable<long> CustomerID { get; set; }

    public Nullable<bool> Deliverystatus { get; set; }

    public Nullable<decimal> Totalprice { get; set; }



    public virtual Customer Customer { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<OrderDetail> OrderDetails { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Payment> Payments { get; set; }

}

}
