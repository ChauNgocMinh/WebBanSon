using Project_63130307.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
namespace Project_63130307.Controllers
{
	public class Cart_63130307Controller : Controller
	{
		Project_63130307Entities db = new Project_63130307Entities();
		public List<CartEntity_63130307> GetCart()
		{
			List<CartEntity_63130307> lstCart = Session["Cart_63130307"] as List<CartEntity_63130307>;
			if (lstCart == null)
			{

				lstCart = new List<CartEntity_63130307>();
				Session["Cart_63130307"] = lstCart;
			}
			return lstCart;
		}
		public ActionResult AddtoCart(long id, string strURL)
		{
			List<CartEntity_63130307> lstcart = GetCart();
			CartEntity_63130307 Product = lstcart.Find(n => n.IdItem == id);
			if (Product == null)
			{
				Product = new CartEntity_63130307(id);
				lstcart.Add(Product);
				return Redirect(strURL);
			}
			else
			{
				Product.Quantity++;
				return Redirect(strURL);
			}
		}
		public ActionResult Cart()
		{
			List<CartEntity_63130307> lstCart = GetCart();
			if (lstCart.Count == 0)
			{
				return RedirectToAction("Index", "AuraStore_63130307");
			}
			ViewBag.TotalQuantity = TotalQuantity();
			ViewBag.ToTalPrice = ToTalPrice();
			return View(lstCart);
		}

		private int TotalQuantity()
		{
			int iTongSoLuong = 0;
			List<CartEntity_63130307> lstcart = Session["Cart_63130307"] as List<CartEntity_63130307>;
			if (lstcart != null)
			{
				iTongSoLuong = lstcart.Sum(n => n.Quantity);
			}
			return iTongSoLuong;
		}

		private Double ToTalPrice()
		{
			double ToTal = 0;
			List<CartEntity_63130307> lstCart = Session["Cart_63130307"] as List<CartEntity_63130307>;
			if (lstCart != null)
			{
				ToTal = lstCart.Sum(n => n.TotalPrices);
			}
			return ToTal;
		}

		//Cap nhat Giỏ hàng
		public ActionResult EditCart(long id, FormCollection f)
		{


			List<CartEntity_63130307> lstGiohang = GetCart();

			CartEntity_63130307 item = lstGiohang.SingleOrDefault(n => n.IdItem == id);

			if (item != null)
			{
				item.Quantity = int.Parse(f["txtSoluong"].ToString());
			}
			return RedirectToAction("Cart");
		}
		//Xoa Giohang
		public ActionResult DeleteCart(long id)
		{

			List<CartEntity_63130307> lstGiohang = GetCart();

			CartEntity_63130307 sanpham = lstGiohang.SingleOrDefault(n => n.IdItem == id);

			if (sanpham != null)
			{
				lstGiohang.RemoveAll(n => n.IdItem == id);
				return RedirectToAction("Cart");

			}
			if (lstGiohang.Count == 0)
			{
				return RedirectToAction("Index", "AuraStore_63130307");
			}
			return RedirectToAction("Cart");
		}

		public ActionResult DeleteAll()
		{

			List<CartEntity_63130307> lstGiohang = GetCart();
			lstGiohang.Clear();
			return RedirectToAction("Index", "AuraStore_63130307");
		}
		[HttpGet]
		public ActionResult Order()
		{
			if (Session["usr"] == null || Session["usr"].ToString()=="")
			{
				return RedirectToAction("Login", "Login");
			}
			if (Session["Cart_63130307"] == null)
			{
				return RedirectToAction("Index", "AuraStore_63130307");
			}
			List<CartEntity_63130307> listcart = GetCart();
			ViewBag.ToTalQuanttity = TotalQuantity();
			ViewBag.TotalPrice = ToTalPrice();

			return View(listcart);
		}
		public ActionResult Order(FormCollection collection)
		{
			Order or = new Order();
			Customer cus = (Customer)Session["usr"];
			
			List<CartEntity_63130307> crt = GetCart();
			or.CustomerID = cus.ID;
			or.Orderdate = DateTime.Now;
			or.Status = 0;
			or.Totalprice = (decimal)ToTalPrice();
			db.Orders.Add(or);
			db.SaveChanges();
			foreach (var item in crt)
			{
				OrderDetail ordt = new OrderDetail();
				ordt.OrderID = or.ID;
				ordt.ItemId = item.IdItem;
				ordt.Quantity = item.Quantity;
				ordt.Totalprice = (decimal)item.Prices;
				db.OrderDetails.Add(ordt);


				var it = db.Items.Find(item.IdItem);
				it.Quantity = (it.Quantity) - item.Quantity;
				db.SaveChanges();
			}
			db.SaveChanges();
			Session["Cart_63130307"] = null;

			return RedirectToAction("OrderConfirmation", "Cart_63130307");

	}
		public ActionResult OrderConfirmation()
		{
			return View();
		}
		public ActionResult Newinformation()
		{
			if (Session["usr"] == null)
			{
				return RedirectToAction("Login", "Login");
			}
			var ac = ((Customer)Session["usr"]);

			return View(new AccountClientEntity_63130307(ac));
		}
		[HttpPost]
		public ActionResult Newinformation(FormCollection fc)
		{
			var ac = ((Customer)Session["usr"]);

			if (Session["usr"] != null)
			{
				string userName = fc["userName"].ToString();
				string address = fc["address"].ToString();
				string phoneNumber = fc["phonenumber"].ToString();
				string name = fc["name"].ToString();
				string emailAdress = fc["email"].ToString();
				var temp = db.Customers.SingleOrDefault(x => x.Username == userName);
				if (temp != null && address != "" )
				{
					temp.Address = fc["address"];
					temp.Name = fc["name"];
					temp.Phone = fc["phonenumber"];
					temp.EmailAddress = fc["email"];
					db.SaveChanges();
					Session["usr"] = temp;
					return RedirectToAction("Order", "Cart_63130307");
				}
			}
			else
			{
				return RedirectToAction("Index", "AuraStore_63130307");
			}
			ModelState.AddModelError("", "Error cannot change Infomation..");
			return View(new AccountClientEntity_63130307(ac));
		}
	}
			
}