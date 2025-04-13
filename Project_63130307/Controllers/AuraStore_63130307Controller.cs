using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Project_63130307.Models;


namespace Project_63130307.Controllers
{
    public class AuraStore_63130307Controller : Controller
    {
        Project_63130307Entities data = new Project_63130307Entities();

        public ActionResult Signout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Index", "AuraStore_63130307");

        }
        public ActionResult Search()
        {
            return PartialView();

        }
        private List<Item> NewItem(int count)
        {
            return data.Items.Where(d => d.Active == true).OrderByDescending(a => a.DateImport).Take(count).ToList();
        }
        public ActionResult Index(string search)
        {
            var model = new List<Item>();
                model = data.Items.Where(x => x.Active.Value).OrderByDescending(c => c.DateImport).Where(nv => nv.Name.Contains(search) || search == null && nv.Active == true).ToList();
           
            return View(model);
        }
        public ActionResult Detail(int id)
        {
            var gear = from t in data.Items
                       where t.ID == id
                       select t;
            return View(gear.Single());
        }
        public ActionResult Menu()
        {
            var menu = from t in data.Menus select t;
            return PartialView(menu);
        }
        public ActionResult ItemMenuType(long id)
        {
            var b = (from t in data.ItemTypes where t.MenuID == id select t).ToList();
            return PartialView(b);
        }
        public ActionResult ProductbyType(long id)
        {
            var pr = from d in data.Items where d.TypeID == id && d.Active == true select d;
            return View(pr);
        }
        public ActionResult Banner()
        {
            var bn = (from d in data.Banners select d).ToList();
            return PartialView(bn);
        }
        public ActionResult Relatedproducts(long id)
        {
            var i = (from t in data.Items where t.Active == true select t).Take(5).ToList();

            return PartialView(i);
        }
        public ActionResult Newdproducts()
        {

            return PartialView(NewItem(5));
        }
        public ActionResult Helmets()
        {
            long id = 2;
            var i = from t in data.Items
                    join c in data.ItemTypes on t.TypeID equals c.ID
                    where c.MenuID == id && t.Active == true
                    select new { t, c };
            List<HelmetsEntity_63130307> listhl = new List<HelmetsEntity_63130307>();

            foreach (var info in i.ToList())
            {
                HelmetsEntity_63130307 hl = new HelmetsEntity_63130307();
                hl.Name = info.t.Name;
                hl.Picture = info.t.Picture;
                hl.Quantity = info.t.Quantity;
                hl.Sellprice = info.t.SellPrice;
                hl.Status = info.t.ShortTitle;
                listhl.Add(hl);
            }
            return View(listhl);
        }
        public ActionResult RiddingGear()
        {
            long id = 3;
            var i = from t in data.Items
                    join c in data.ItemTypes on t.TypeID equals c.ID

                    where c.MenuID == id && t.Active == true
                    select new { t, c };
            List<HelmetsEntity_63130307> listhl = new List<HelmetsEntity_63130307>();

            foreach (var info in i.ToList())
            {
                HelmetsEntity_63130307 hl = new HelmetsEntity_63130307();
                hl.Name = info.t.Name;
                hl.Picture = info.t.Picture;
                hl.Quantity = info.t.Quantity;
                hl.Sellprice = info.t.SellPrice;
                hl.Status = info.t.ShortTitle;
                listhl.Add(hl);
            }
            return View(listhl);
        }

        public ActionResult Accsesories()
        {
            long id = 4;
            var i = from t in data.Items
                    join c in data.ItemTypes on t.TypeID equals c.ID

                    where c.MenuID == id && t.Active == true
                    select new { t, c };
            List<HelmetsEntity_63130307> listhl = new List<HelmetsEntity_63130307>();

            foreach (var info in i.ToList())
            {
                HelmetsEntity_63130307 hl = new HelmetsEntity_63130307();
                hl.Name = info.t.Name;
                hl.Picture = info.t.Picture;
                hl.Quantity = info.t.Quantity;
                hl.Sellprice = info.t.SellPrice;
                hl.Status = info.t.ShortTitle;
                listhl.Add(hl);
            }
            return View(listhl);
        }
        public ActionResult DetailProduct(long id)
        {
            var i = from t in data.Items
                    join c in data.ItemTypes on t.TypeID equals c.ID
                    where t.ID.Equals(id)
                    select t;
            List<HelmetsEntity_63130307> listhl = new List<HelmetsEntity_63130307>();

            foreach (var info in i)
            {
                HelmetsEntity_63130307 hl = new HelmetsEntity_63130307();
                hl.Name = info.Name;
                hl.Picture = info.Picture;
                hl.Quantity = info.Quantity;
                hl.Sellprice = info.SellPrice;
                hl.Status = info.ShortTitle;
                listhl.Add(hl);
            }
            return View(listhl);

        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult Sessionlogin()
        {
            return PartialView();
        }
        public ActionResult ListOrderClient()
        {
            var ac = (Customer)Session["usr"];
            if (ac == null)
            {
                return RedirectToAction("Login", "Acction");
            }

            var temp = data.Orders.Where(p => p.Customer.Username == ac.Username);
            List<OrderEntity_63130307> listProdcut = new List<OrderEntity_63130307>();
            foreach (var item in temp)
            {
                OrderEntity_63130307 pr = new OrderEntity_63130307();
                pr.TypeOf_OrderEntity_63130307(item);
                listProdcut.Add(pr);
            }
            return View(listProdcut);
        }
        public ActionResult ListOrderDetailClient(long? id)
        {
            var temp = data.OrderDetails.Where(d => d.OrderID == id);
            List<OrderDetailEntity_63130307> listdetail = new List<OrderDetailEntity_63130307>();
            foreach (var item in temp)
            {
                OrderDetailEntity_63130307 or = new OrderDetailEntity_63130307();
                or.TypeOf_OrderEntity_63130307(item);
                listdetail.Add(or);
            }


            return PartialView(listdetail);

        }
        public ActionResult CancelOrder(long? id)
        {
            var temp = data.OrderDetails.Where(d => d.OrderID == id);
            List<OrderDetailEntity_63130307> listdetail = new List<OrderDetailEntity_63130307>();
            foreach (var item in temp)
            {
                OrderDetailEntity_63130307 or = new OrderDetailEntity_63130307();
                or.TypeOf_OrderEntity_63130307(item);
                listdetail.Add(or);
            }
            ViewBag.Date = data.Orders.SingleOrDefault(a => a.ID == id).Deliverydate;
            ViewBag.id = id;
            return View(listdetail);

        }
        [HttpPost]

        public ActionResult CancelOrder(FormCollection fc)
        {

            long id = Convert.ToInt64(fc["id"]);
            var tem = data.Orders.SingleOrDefault(d => d.ID == id);

            tem.Deliverystatus = false;

            data.SaveChanges();


            return RedirectToAction("ListOrderClient");

        }
        public ActionResult Profile()
        {
            var ac = (Customer)Session["usr"];


            var t = from a in data.Customers where a.Username == ac.Username select a;


            return View(t.ToList());


        }
    }
}