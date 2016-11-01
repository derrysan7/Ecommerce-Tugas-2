using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ECommerceApp.DAL;
using ECommerceApp.Models;

namespace ECommerceApp.Controllers
{
    public class ShoppingCartsController : Controller
    {
        // GET: ShoppingCarts
        public ActionResult Index()
        {
            using (ShoppingCartsDAL scService = new ShoppingCartsDAL())
            {
                string username = 
                    Session["username"] != null ? Session["username"].ToString() : string.Empty;
                return View(scService.GetAllData(username).ToList());
            }
        }

        public ActionResult AddToCart(int id)
        {
            //cek apakah user sudah login
            if(Session["username"]==null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    Session["username"] = User.Identity.Name;
                }
                else
                {
                    //var tempUser = Guid.NewGuid().ToString();
                    //Session["username"] = tempUser;
                    return RedirectToAction("LogIn", "Account", new { area = "" });
                    
                }
            }


            using (ShoppingCartsDAL scService = new ShoppingCartsDAL())
            {
                var newShoppingCart = new ShoppingCart
                {
                    CartID = Session["username"].ToString(),
                    Quantity = 1,
                    BookID = id,
                    DateCreated = DateTime.Now
                };
                if (User.Identity.IsAuthenticated) {
                    scService.AddToCart(newShoppingCart);
                }
                
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            using (ShoppingCartsDAL services = new ShoppingCartsDAL())
            {
                var result = services.GetDataById(id);
                return View(result);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ShoppingCart cart)
        {
            using (ShoppingCartsDAL services = new ShoppingCartsDAL())
            {
                try
                {
                    services.Edit(cart);
                }
                catch (Exception ex)
                {
                }
            }
            return RedirectToAction("Index");
        }


        public ActionResult Delete(int id)
        {
            using (ShoppingCartsDAL service = new ShoppingCartsDAL())
            {
                try
                {
                    service.Delete(id);
                }
                catch (Exception ex)
                {
                }
            }
            return RedirectToAction("Index");
        }
    }
}