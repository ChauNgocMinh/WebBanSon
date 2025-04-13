using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_63130307.Models
{
	public class CartEntity_63130307
	{
		Project_63130307Entities data = new Project_63130307Entities(); 
		public long IdItem { set; get; }
		public string Name { set; get; }
		public string Picture { set; get; }
		public Double Prices { set; get; }
		public int Quantity { set; get; }
		public Double TotalPrices
		{
			get { return Quantity * Prices; }

		}
		//Khoi tao gio hàng theo Masach duoc truyen vao voi Soluong mac dinh la 1
		public CartEntity_63130307(long id)
		{
			IdItem =  id;
			Item product = data.Items.Single(n => n.ID == IdItem);
			Name = product.Name;
			Picture = product.Picture;
			Prices = Double.Parse(product.SellPrice.ToString());
			Quantity = 1;
		}
	}
}