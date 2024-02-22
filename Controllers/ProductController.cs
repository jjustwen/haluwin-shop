using HaluwinShop.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HaluwinShop.Controllers
{
    public class ProductController : Controller
    {
        private DBHaluwinEntities db = new DBHaluwinEntities();

        public ActionResult Index(int? page)
        {
            ViewBag.BackgroundImage = "/Content/assets/img/banner/tu.png";
            ViewBag.TitleColor = "#FFA500";
            ViewBag.PageTitle = "Mua sắm thôi !";
            ViewBag.SubtitleColor = "#FFA500";
            ViewBag.PageSubtitle = "Haluwin store mang đến cho bạn những sản phẩm phong phú đa dạng";
            int pageSize = 8;
            int pageNum = (page ?? 1);
            var productList = db.Products.OrderBy(x => x.NamePro);

            return View(productList.ToPagedList(pageNum, pageSize));
        }
        public ActionResult ShowProByCate(int? idCate)
        {
            ViewBag.BackgroundImage = "/Content/assets/img/banner/tu.png";
            ViewBag.TitleColor = "#FFA500";
            ViewBag.PageTitle = "Mua sắm thôi !";
            ViewBag.SubtitleColor = "#FFA500";
            ViewBag.PageSubtitle = "Haluwin store mang đến cho bạn những sản phẩm phong phú đa dạng";
            if (idCate == null)
            {
                return RedirectToAction("Index", "Products");
            }

            var proByCate = db.Products.Where(s => s.CATEGORY.Id == idCate).ToList();

            return View(proByCate);
        }
        public ActionResult Details(int? id)
        {
            ViewBag.BackgroundImage = "/Content/assets/img/banner/tu.png";
            ViewBag.TitleColor = "#FFA500";
            ViewBag.PageTitle = "Mua sắm thôi !";
            ViewBag.SubtitleColor = "#FFA500";
            ViewBag.PageSubtitle = "Haluwin store mang đến cho bạn những sản phẩm phong phú đa dạng";
            return View(db.Products.Find(id));
        }
        public ActionResult SearchPro(string name)
        {
            ViewBag.BackgroundImage = "/Content/assets/img/banner/tu.png";
            ViewBag.TitleColor = "#FFA500";
            ViewBag.PageTitle = "Mua sắm thôi !";
            ViewBag.SubtitleColor = "#FFA500";
            ViewBag.PageSubtitle = "Haluwin store mang đến cho bạn những sản phẩm phong phú đa dạng";
            if (name == null)
                return View(db.Products.ToList());
            else
                return View(db.Products.Where(p => p.NamePro.Contains(name)).OrderBy(p => p.NamePro).ToList());
        }
    }
}