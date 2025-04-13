using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Project_63130307.Models;
namespace Project_63130307.Controllers
{
    public class Login_63130307Controller : Controller
    {
		// GET: Login
		Project_63130307Entities db = new Project_63130307Entities();
		public ActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public ActionResult Login(FormCollection collection)
        {
			
			var userName = collection["userName"];
			var passWord = collection["passWord"];
			Customer cs = db.Customers.SingleOrDefault(n => n.Username == userName && n.Passwords == passWord);
				if (cs != null)
				{
					
					Session["usr"] = cs;
					return RedirectToAction("Index", "AuraStore_63130307");
				}
				else
					ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không đúng");
			return View();
			
		}
		
		public ActionResult Register()
		{
			return View();
		}
		[HttpPost]
		public ActionResult Register(FormCollection collection)
		{
			string userName = collection["Username"];
			string passWord = collection["Password"];
			string conFirmPassWord = collection["ConfirmPassword"];
			string name = collection["Name"];
			string Email = collection["Email"];
			string address = collection["Address"];
			string phoneNumber = collection["PhoneNumber"];
			if (userName != null && passWord == conFirmPassWord)
			{
					var tem = db.Customers.SingleOrDefault(a => a.Username == userName);
					if (tem == null)
					{
						Customer cs = new Customer();
						cs.Username = userName;
						cs.Passwords = passWord;
						cs.Name = name;
						cs.EmailAddress = Email;
						cs.Address = address;
						cs.Phone = phoneNumber;
						db.Customers.Add(cs);
						db.SaveChanges();
					}
				else
				{
					ModelState.AddModelError("", "Tài khoản đã tồn tại !");
					return View();
				}
				return RedirectToAction("Login", "Login_63130307");
			}
			else
			{
				ViewBag.Confirm = "Mật khẩu không trùng khớp";
			}
			return View();
		}
		public ActionResult Forgotpassword()
		{
			if (Session["usr"] == null)
			{
				return RedirectToAction("Login", "Login_63130307");
			}
			var ac = ((Customer)Session["usr"]);
			return View(new AccountClientEntity_63130307(ac));
		}
		[HttpPost]

		public ActionResult Forgotpassword(FormCollection fc)
		{
			var ac = ((Customer)Session["usr"]);
			if (Session["usr"] != null)
			{
				string userName = fc["userName"].ToString();
				string pass = fc["pass"].ToString();
				string newpass = fc["newpass"].ToString();
				string repass = fc["repass"].ToString();
				var temp = db.Customers.SingleOrDefault(x => x.Username == userName && x.Passwords == pass);
				if (temp != null && pass != "" && newpass != pass && newpass != "" && newpass == repass)
				{
					temp.Passwords = fc["newpass"];
					db.SaveChanges();
					Session["usr"] = temp;
					return RedirectToAction("Profile", "AuraStore_63130307");
				}
			}
			else
			{
				return RedirectToAction("Index", "AuraStore_63130307");
			}
			ModelState.AddModelError("", "Không thể thay đổi mật khẩu");
			return View(new AccountClientEntity_63130307(ac));
		}

		public ActionResult Changepassword()
		{
			if (Session["usr"] == null)
			{
				return RedirectToAction("Login", "Login_63130307");
			}
			var ac = ((Customer)Session["usr"]);

			return View(new AccountClientEntity_63130307(ac));
		}
		[HttpPost]

		public ActionResult Changepassword(FormCollection fc)
		{
			var ac = ((Customer)Session["usr"]);
			if (Session["usr"] != null)
			{
				string userName = fc["userName"].ToString();
				string pass = fc["pass"].ToString();
				string newpass = fc["newpass"].ToString();
				string repass = fc["repass"].ToString();
				var temp = db.Customers.SingleOrDefault(x => x.Username == userName && x.Passwords == pass);
				if (temp != null && pass != "" && newpass != pass && newpass != "" && newpass == repass)
				{
					temp.Passwords = fc["newpass"];
					db.SaveChanges();
					Session["usr"] = temp;
					return RedirectToAction("Profile", "AuraStore_63130307");
				}
			}
			else
			{
				return RedirectToAction("Index", "AuraStore_63130307");
			}
			ModelState.AddModelError("", "Không thể thay đổi mật khẩu..");
			return View(new AccountClientEntity_63130307(ac));
		}
		public static byte[] GetBytes(string str)
		{
			byte[] bytes = new byte[str.Length * sizeof(char)];
			System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
			return bytes;
		}
	}
}