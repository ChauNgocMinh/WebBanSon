using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Project_63130307.Models
{
	public class OrderEntity_63130307
	{	

		public long ID { get; set; }
		public Nullable<System.DateTime> Orderdate { get; set; }
		public Nullable<System.DateTime> Deliverydate { get; set; }
		public Nullable<Decimal> Totalprice { get; set; }
		public Nullable<int> Status { get; set; }
		public Nullable<long> CustomerID { get; set; }
		public Nullable<bool> Deliverystatus { get; set; }

		public virtual Customer Customer { get; set; }
	
		public virtual ICollection<OrderDetail> OrderDetails { get; set; }
	
		public virtual ICollection<Payment> Payments { get; set; }
		public Order TypeOf_Order()
		{
			Order order = new Order();
			PropertyInfo[] pithis = typeof(OrderEntity_63130307).GetProperties();
			PropertyInfo[] pieClinet = typeof(Order).GetProperties();
			foreach (var items in pithis)
			{
				foreach (var itempiem in pieClinet)
				{
					if (itempiem.Name == items.Name)
					{
						itempiem.SetValue(order, items.GetValue(this));
						break;
					}
				}
			}
			return order;
		}

		// convert tu model sang view

		public void TypeOf_OrderEntity_63130307(Order order)
		{

			PropertyInfo[] pithis = typeof(OrderEntity_63130307).GetProperties();
			PropertyInfo[] pieClinet = typeof(Order).GetProperties();
			foreach (var items in pithis)
			{
				foreach (var itempiem in pieClinet)
				{
					if (itempiem.Name == items.Name)
					{
						items.SetValue(this, itempiem.GetValue(order));
						break;
					}
				}
			}

		}
		//
		public OrderEntity_63130307(Order order)
		{
			TypeOf_OrderEntity_63130307(order);

		}
		public OrderEntity_63130307()
		{


		}
	}
}