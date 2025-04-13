using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Project_63130307.Models;
using Project_63130307.ViewModel;

namespace Project_63130307.Controllers
{
    public class Menu_63130307Controller : Controller
    {
		// GET: Admin
		Project_63130307Entities db = new Project_63130307Entities();
        public ActionResult Index()
        {
            var items = db.Menus.ToList();
            return View(items.ToList());
        }
        public ActionResult Create()
        {
            return View(new MenuAndItemTypeViewModel
            {
                ItemTypeNames = new List<string> { "" } // ít nhất 1 input ban đầu
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MenuAndItemTypeViewModel model)
        {
            if (ModelState.IsValid && model.ItemTypeNames != null && model.ItemTypeNames.Any(name => !string.IsNullOrWhiteSpace(name)))
            {
                var menu = new Menu
                {
                    Name = model.MenuName,
                    Link = model.Link
                };
                db.Menus.Add(menu);
                db.SaveChanges();

                foreach (var typeName in model.ItemTypeNames)
                {
                    if (!string.IsNullOrWhiteSpace(typeName))
                    {
                        var itemType = new ItemType
                        {
                            TypeName = typeName,
                            MenuID = menu.ID
                        };
                        db.ItemTypes.Add(itemType);
                    }
                }

                db.SaveChanges();
                return RedirectToAction("Index", "Menu");
            }

            return View(model);
        }
    }
}