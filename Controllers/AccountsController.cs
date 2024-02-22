using HaluwinShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HaluwinShop.Controllers
{
    public class AccountsController : Controller
    {
        private DBHaluwinEntities db = new DBHaluwinEntities();
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
                ViewBag.ErrorInfo = "Sai Info";
                return View("Login");
            }
            else
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                Session["Id"] = user.ID;
                Session["Email"] = user.Email;
                Session["NameUser"] = user.NameUser;
                Session["PasswordUser"] = user.PasswordUser;
                if (check.RoleUser == "Admin")
                {
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
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
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorRegister = "Tên tài khoản này đã tồn tại !!!";
                    return View();
                }
            }
            return View();
        }
        public ActionResult ProfileUser(int id)
        {
            var user = db.Accounts.Find(id);
            return View(user);
        }

    }
}