using HaluwinShop.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HaluwinShop.Areas.Admin.Controllers
{
    public class AccountsController : Controller
    {
        private DBHaluwinEntities db = new DBHaluwinEntities();
        // GET: Admin/Accounts
        public ActionResult Index()
        {
            return View(db.Accounts.ToList());
        }
        [HttpGet]
        public ActionResult Index(string name)
        {
            if (name == null)
                return View(db.Accounts.ToList());
            else
                return View(db.Accounts.Where(s => s.NameUser.Contains(name)).ToList());
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Account user)
        {
            if (ModelState.IsValid)
            {
                var check_Name = db.Accounts.Where(s => s.NameUser == user.NameUser).FirstOrDefault();
                if (check_Name == null)
                {
                    user.CreatedDate = DateTime.Now;
                    user.RoleUser = "User";
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.Accounts.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Login", "Accounts");
                }
                else
                {
                    ViewBag.ErrorRegister = "Tên tài khoản này đã tồn tại !!!";
                    return View();
                }
            }
            return View();
        }
        public ActionResult Create()
        {
            var values = new List<string>() { "Admin", "User" };
            ViewBag.RoleUser = new SelectList(values, "User");
            return View();
        }
        [HttpPost]
        public ActionResult Create(Account user)
        {
            if (ModelState.IsValid)
            {
                var check_Name = db.Accounts.Where(s => s.NameUser == user.NameUser).FirstOrDefault();

                if (check_Name == null)
                {
                    db.Configuration.ValidateOnSaveEnabled = false;
                    user.CreatedDate = DateTime.Now;

                    db.Accounts.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorRegister = "Tên tài khoản đã tồn tại !!!";
                    return View();
                }
            }
            return View();
        }
        public ActionResult Delete(int id)
        {
            return View(db.Accounts.Where(s => s.ID == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Delete(int id, Account acc)
        {
            try
            {
                acc = db.Accounts.Where(s => s.ID == id).FirstOrDefault();
                db.Accounts.Remove(acc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Lỗi xóa!");
            }
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Account user)
        {
            var check = db.Accounts.Where(s => s.NameUser == user.NameUser && s.PasswordUser == user.PasswordUser).FirstOrDefault();
            if (check == null)
            {
                ViewBag.ErrorInfo = "Sai tài khoản hoặc mật khẩu";
                return View("Login");
            }
            else
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                Session["ID"] = check.ID;
                Session["NameUser"] = check.NameUser;
                Session["PasswordUser"] = check.PasswordUser;
                return RedirectToAction("Index");
            }
        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Accounts");
        }

    }
}