﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_63130307.Controllers
{
    public class Image_63130307Controller : Controller
    {
		// GET: Image
		public ActionResult UploadImage()
		{
			return View();
		}
		public string ProcessUpload(HttpPostedFileBase file)
		{
			try
			{
				file.SaveAs(Server.MapPath("~/img/Item/" + file.FileName));
				
				if (System.IO.File.Exists(file.FileName))
						ViewBag.ImageExist = "Inmage Exit";
				else
				{
					return file.FileName;
				}
			}
			catch (Exception ex)
			{

				Debug.WriteLine(ex.Message);
			}
			return null;
		}
	}
}