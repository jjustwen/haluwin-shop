using HaluwinShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HaluwinShop.Controllers
{
    public class HomeController : Controller
    {
        private DBHaluwinEntities db = new DBHaluwinEntities();
        public ActionResult Index()
        {
            ViewBag.BackgroundImage = "/Content/assets/img/banner/haicaidaulau.png";
            ViewBag.TitleColor = "#FFA500";
            ViewBag.PageTitle = "Chào mừng bạn đến với Haluwin Store";
            ViewBag.SubtitleColor = "#FFA500";
            ViewBag.PageSubtitle = "Nơi trải nghiệm Halloween hoàn hảo !";
            return View(db.Products.Take(4).ToList());
        }

        public ActionResult About()
        {
            ViewBag.BackgroundImage = "/Content/assets/img/banner/cafekhong.jpg";
            ViewBag.TitleColor = "#FFA500";
            ViewBag.PageTitle = "Về chúng tôi";
            ViewBag.SubtitleColor = "#FFA500";
            ViewBag.PageSubtitle = "Những người yêu thích Hallween";
            return View();
        } 

        public ActionResult Contact()
        {
            ViewBag.BackgroundImage = "/Content/assets/img/banner/tay.png";
            ViewBag.TitleColor = "#FFA500";
            ViewBag.PageTitle = "Liên hệ";
            ViewBag.SubtitleColor = "#FFA500";
            ViewBag.PageSubtitle = "Chúng tôi luôn sẵn sàng giải đáp mọi thắc mắc của bạn";
            return View();
        }

    }
}