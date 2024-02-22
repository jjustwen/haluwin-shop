using HaluwinShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HaluwinShop.Controllers
{
    public class CategoriesController : Controller
    {
        private DBHaluwinEntities db = new DBHaluwinEntities();
        public PartialViewResult CategoryDropdown()
        {
            return PartialView(db.Categories.OrderBy(c => c.Id).ToList());
        }
      
    }
}