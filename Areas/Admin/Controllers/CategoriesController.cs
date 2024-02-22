using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HaluwinShop.Models;

namespace HaluwinShop.Areas.Admin.Controllers
{
    public class CategoriesController : Controller
    {
        private DBHaluwinEntities db = new DBHaluwinEntities();
        // GET: Admin/Categories

        public ActionResult Index()
        {
            return View(db.Categories.OrderBy(s => s.Id).ToList());
        }
        [HttpGet]
        public ActionResult Index(string name)
        {
            if (name == null)
                return View(db.Categories.ToList());
            else
                return View(db.Categories.Where(s => s.NameCate.Contains(name)).ToList());
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category cate)
        {
            try
            {
                db.Categories.Add(cate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Lỗi tạo mới");
            }
        }
        public ActionResult Delete(int id)
        {
            return View(db.Categories.Where(s => s.Id == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Delete(int id, Category cate)
        {
            try
            {
                cate = db.Categories.Where(s => s.Id == id).FirstOrDefault();
                db.Categories.Remove(cate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("This data is using in other table, Error Delete!");
            }
        }
        public ActionResult Edit(int id)
        {
            return View(db.Categories.Where(s => s.Id == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(int id, Category cate)
        {
            db.Entry(cate).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            return View(db.Categories.Where(s => s.Id == id).FirstOrDefault());
        }
        public PartialViewResult CategoryPartial()
        {
            return PartialView(db.Categories.ToList());
        }

    }
}