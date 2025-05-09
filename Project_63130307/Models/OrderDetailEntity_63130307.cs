﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Project_63130307.Models
{
	public class OrderDetailEntity_63130307
	{
		public long ID { get; set; }
		public int Quantity { get; set; }
		public Nullable<long> ItemId { get; set; }
		public Nullable<long> OrderID { get; set; }
		public Nullable<decimal> Totalprice { get; set; }

		public virtual Item Item { get; set; }
		public virtual Order Order { get; set; }
		public OrderDetail TypeOf_Order()
		{
			OrderDetail order = new OrderDetail();
			PropertyInfo[] pithis = typeof(OrderDetailEntity_63130307).GetProperties();
			PropertyInfo[] pieClinet = typeof(OrderDetail).GetProperties();
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
		public void TypeOf_OrderEntity_63130307(OrderDetail order)
		{

			PropertyInfo[] pithis = typeof(OrderDetailEntity_63130307).GetProperties();
			PropertyInfo[] pieClinet = typeof(OrderDetail).GetProperties();
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
		public OrderDetailEntity_63130307(OrderDetail order)
		{
			TypeOf_OrderEntity_63130307(order);

		}
		public OrderDetailEntity_63130307()
		{


		}
	}
}