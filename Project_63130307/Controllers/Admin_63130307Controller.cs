using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project_63130307.Models;
using Project_63130307.Models;

namespace Project_63130307.Controllers
{
    public class Admin_63130307Controller : Controller
    {
		// GET: Admin
		Project_63130307Entities db = new Project_63130307Entities();
		public ActionResult SignOut()
		{
			Response.Cookies.Clear();
			return RedirectToAction("Login", "Admin_63130307");

		}
		public ActionResult Index()
        {
			DateTime dateTimeNow = DateTime.Now.Date;
			dateTimeNow = dateTimeNow.AddYears(-1);

			string[] dateX = new string[12];
			string[] data = new string[12];
			for (int i = 0; i < 12; i++)
			{

				dateX[i] = (dateTimeNow.Month.ToString() + "/" + dateTimeNow.Year.ToString()).ToString();
				var temp = db.Orders.Where(a => a.Orderdate.Value.Month == dateTimeNow.Month).Sum(s => s.Totalprice);
				if (temp == null)
				{
					temp = 0;
				}
				data[i] = temp.ToString();
				dateTimeNow = dateTimeNow.AddMonths(1);
			}
			ViewBag.dateX = dateX;
			ViewBag.data = data;
			var ac = (Admin)Session["Account"];
			if (ac == null)
			{
				return RedirectToAction("Login", "Admin_63130307");
			}
			else { return RedirectToAction("Index", "Items_63130307"); }
			
        }
		public ActionResult Login()
		{
			return View();

		}
		[HttpPost]
		public ActionResult Login(FormCollection collection)
		{
			var userName = collection["userName"];
			var passWord = collection["passWord"];
			Admin ad = db.Admins.SingleOrDefault(n => n.Username == userName && n.Passwords == passWord);
			if (ad != null)
			{
				Session["Account"] = ad;
				Response.Cookies["usr"].Value = ad.Username;
				var name = db.Admins.SingleOrDefault(a => a.Username == ad.Username).Name;
				Response.Cookies["Name"].Value = name;
				var atar = db.Admins.SingleOrDefault(a => a.Username == ad.Username).Picture;
				if (atar == null || atar == "")
				{
					atar = "~/img/Item/avatar-default-icon.png";
				}
				Response.Cookies["avatar"].Value = atar;
				return RedirectToAction("Index", "Items_63130307");
			}
			else
				ModelState.AddModelError("", "The user login or password  is incorrect..");
			return View();


		
	}

		public ActionResult Create()
		{
			return View();
		}

		// POST: Admins/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "Username,Passwords,Name,Picture")] Admin admin)
		{
			if (ModelState.IsValid)
			{
				db.Admins.Add(admin);
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(admin);
		}
		public ActionResult ListOrder()
		{
			var temp = db.Orders.Where(o => o.Status != 1).ToList();
			List<OrderEntity_63130307> lisorder = new List<OrderEntity_63130307>();
			foreach (var item in temp)
			{
				OrderEntity_63130307 or = new OrderEntity_63130307();
				or.TypeOf_OrderEntity_63130307(item);
				lisorder.Add(or);
			}
			return View(lisorder);
		}
		public ActionResult Comfirm(long ? id)
		{
			var temp = db.OrderDetails.Where(d => d.OrderID == id);
			List<OrderDetailEntity_63130307> listdetail = new List<OrderDetailEntity_63130307>();
			foreach (var item in temp)
			{
				OrderDetailEntity_63130307 or = new OrderDetailEntity_63130307();
				or.TypeOf_OrderEntity_63130307(item);
				listdetail.Add(or);
			}
			ViewBag.Date = db.Orders.SingleOrDefault(a => a.ID == id).Deliverydate;
			ViewBag.Status = db.Orders.SingleOrDefault(a => a.ID == id).Status;
			ViewBag.id = id;
			return View(listdetail);

		}

		[HttpPost]

		public ActionResult Comfirm(FormCollection fc)
		{
			var ststua = Int32.Parse(fc["status"]);
			long id = Convert.ToInt64(fc["id"]);
			var tem = db.Orders.SingleOrDefault(d => d.ID ==id);

			tem.Status = ststua;
			if(ststua == 1)
            {
				tem.Deliverydate = DateTime.Now;
			}
			db.SaveChanges();

		
			return RedirectToAction("ListOrder");

		}
		public ActionResult AllListOrder()
		{
			var temp = db.Orders.ToList();
			List<OrderEntity_63130307> lisorder = new List<OrderEntity_63130307>();
			foreach (var item in temp)
			{
				OrderEntity_63130307 or = new OrderEntity_63130307();
				or.TypeOf_OrderEntity_63130307(item);
				lisorder.Add(or);
			}
			return View(lisorder);
		}

		public ActionResult OrderDetail(long? id)
		{
			var temp = db.OrderDetails.Where(d => d.OrderID == id);
			List<OrderDetailEntity_63130307> listdetail = new List<OrderDetailEntity_63130307>();
			foreach (var item in temp)
			{
				OrderDetailEntity_63130307 or = new OrderDetailEntity_63130307();
				or.TypeOf_OrderEntity_63130307(item);
				listdetail.Add(or);
			}
			
			return View(listdetail);

		}
		
		public ActionResult Productnotsold()
		{
			var results = from t1 in db.Items
						  where !(from t2 in db.Orders
								  join a in db.OrderDetails on t2.ID equals a.OrderID
								  where t2.Orderdate == DateTime.Now
								  select t2.ID).Contains(t1.ID)
						  select t1;
			return View(results.ToList());
		}
	}
}