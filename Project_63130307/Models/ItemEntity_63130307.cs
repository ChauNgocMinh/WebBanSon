using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Project_63130307.Models
{
	public class ItemEntity_63130307
	{
		public long ID { get; set; }
		public string Name { get; set; }
		public Nullable<Decimal> PurchartPrices { get; set; }
		public Nullable<Decimal> Sellprice { get; set; }
		public DateTime ImportDate { get; set; }
		public int Quantity { get; set; }
		public  string Picture { get; set; }
		public string Status { get; set; }
		public string Describe { get; set; }
		public virtual ICollection<ItemType> ItemTypes { get; set; }

		public Item TypeOf_Item()
		{
			Item item = new Item();
			PropertyInfo[] pithis = typeof(ItemEntity_63130307).GetProperties();
			PropertyInfo[] pieClinet = typeof(Item).GetProperties();
			foreach (var items in pithis)
			{
				foreach (var itempiem in pieClinet)
				{
					if (itempiem.Name == items.Name)
					{
						itempiem.SetValue(item, items.GetValue(this));
						break;
					}
				}
			}
			return item;
		}

		// convert tu model sang view

		public void TypeOf_ItemEntity_63130307(Item item)
		{

			PropertyInfo[] pithis = typeof(ItemEntity_63130307).GetProperties();
			PropertyInfo[] pieClinet = typeof(Item).GetProperties();
			foreach (var items in pithis)
			{
				foreach (var itempiem in pieClinet)
				{
					if (itempiem.Name == items.Name)
					{
						items.SetValue(this, itempiem.GetValue(item));
						break;
					}
				}
			}

		}
		//
		public ItemEntity_63130307(Item item)
		{
			TypeOf_ItemEntity_63130307(item);

		}
		public ItemEntity_63130307()
		{


		}
	}
}