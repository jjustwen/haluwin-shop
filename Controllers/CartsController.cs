using HaluwinShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HaluwinShop.Areas.Admin.Controllers
{
    public class CartsController : Controller
    {
        private DBHaluwinEntities db = new DBHaluwinEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ShowCart()
        {
            ViewBag.BackgroundImage = "/Content/assets/img/banner/sikenhay.jpg";
            ViewBag.TitleColor = "#FFA500";
            ViewBag.PageTitle = "Giỏ hàng của bạn";
            ViewBag.SubtitleColor = "#FFA500";
            ViewBag.PageSubtitle = "";
            if (Session["Cart"] == null)
                return View("EmptyCart");
            Cart _cart = Session["Cart"] as Cart;
            return View(_cart);
        }
        //Action tạo mới giỏ hàng 
        public Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
        //Action thêm product vào giỏ hàng
        public ActionResult AddToCart(int id)
        {
            var _pro = db.Products.SingleOrDefault(s => s.ProductID == id);    //lấy Pro theo ID
            if (_pro != null)
            {
                GetCart().Add_Product_Cart(_pro);
            }
            return RedirectToAction("Index", "Product");
        }
        public ActionResult Update_Cart_Quantity(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            int id_pro = int.Parse(form["idPro"]);
            int _quantity = int.Parse(form["cartQuantity"]);
            if (_quantity <= 0)
            {
                _quantity = 1;
            }
            cart.Update_quantity(id_pro, _quantity);
            return RedirectToAction("ShowCart", "Carts");
        }
        public ActionResult RemoveCart(int id)
        {
            Cart cart = Session["Cart"] as Cart;
            cart.Remove_CartItem(id);
            return RedirectToAction("ShowCart", "Carts");
        }
        public PartialViewResult BagCart()
        {
            int toltal_quantity_item = 0;
            Cart cart = Session["Cart"] as Cart;
            if (cart != null)
                toltal_quantity_item = cart.Total_quantity();
            ViewBag.QuantityCart = toltal_quantity_item;
            return PartialView("BagCart");
        }


        public ActionResult CheckOut(FormCollection form)
        {
            ViewBag.BackgroundImage = "/Content/assets/img/banner/sikenhay.jpg";
            ViewBag.TitleColor = "#FFA500";
            ViewBag.PageTitle = "Thank you !";
            ViewBag.SubtitleColor = "#FFA500";
            ViewBag.PageSubtitle = "";

            try
            {
                Cart cart = Session["Cart"] as Cart;
                Customer newcus = new Customer();
                newcus.NameCus = form["Name"];
                newcus.PhoneCus = form["Phone"];
                newcus.EmailCus = Session["Email"] as string;
                OrderPro _order = new OrderPro();
                _order.DateOrder = DateTime.Now;
                _order.AddressDeliverry = form["AddressDelivery"];
                _order.IDCus = (int)Session["Id"];
                db.Customers.Add(newcus);
                db.OrderProes.Add(_order);
                foreach (var item in cart.Items)
                {
                    OrderDetail _order__detail = new OrderDetail();
                    _order__detail.IDOrder = _order.ID;
                    _order__detail.IDProduct = item._product.ProductID;
                    _order__detail.UnitPrice = (double)item._product.Price;
                    _order__detail.Quantity = item._quantity;
                    db.OrderDetails.Add(_order__detail);
                    foreach (var p in db.Products.Where(s => s.ProductID == _order__detail.IDProduct))
                    {
                        var update_quan_pro = p.Quantity - item._quantity;
                        p.Quantity = update_quan_pro;
                    }
                }
                db.SaveChanges();
                cart.ClearCart();
                return RedirectToAction("CheckOut_Success", "Carts");
            }
            catch
            {
                return RedirectToAction("CheckOut_Success", "Carts");
            }
        }
        public ActionResult Checkout_Success()
        {
            ViewBag.BackgroundImage = "/Content/assets/img/banner/sikenhay.jpg";
            ViewBag.TitleColor = "#FFA500";
            ViewBag.PageTitle = "Thank you !";
            ViewBag.SubtitleColor = "#FFA500";
            ViewBag.PageSubtitle = "";
            return View();
        }
        public ActionResult EmptyCart()
        {
            ViewBag.BackgroundImage = "/Content/assets/img/banner/sikenhay.jpg";
            ViewBag.TitleColor = "#FFA500";
            ViewBag.PageTitle = "Giỏ hàng của bạn";
            ViewBag.SubtitleColor = "#FFA500";
            ViewBag.PageSubtitle = "";
            return View();
        }
    }
}