using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceApp.Models;
using ECommerceApp.ViewModels;

namespace ECommerceApp.DAL
{
    public class ShoppingCartsDAL : IDisposable
    {
        private CommerceModel db = new CommerceModel();

        public IQueryable<ShoppingCart> GetAllData(string username)
        {
            var results = from s in db.ShoppingCarts.Include("Book")
                          where s.CartID == username
                          orderby s.RecordID ascending
                          select s;
            return results;
        }

        //cek apakah barang dengan user yang sama ada di shopping cart
        public ShoppingCart GetItemByUser(string username,int bookId)
        {
            var result = (from s in db.ShoppingCarts
                          where s.CartID == username && s.BookID == bookId
                          select s).SingleOrDefault();

            return result;
        }

        public void UpdateCartID(string tempUsername, string username)
        {
            var results = from s in db.ShoppingCarts
                          where s.CartID == tempUsername
                          select s;

            foreach(var sc in results)
            {
                sc.CartID = username;
            }
            db.SaveChanges();
        }

        public void AddToCart(ShoppingCart shoppingCart)
        {
            //cek apakah cart dengan pengguna dan buku sama sudah ada
            var result = GetItemByUser(shoppingCart.CartID, shoppingCart.BookID);
            if(result!=null)
            {
                //update
                result.Quantity += 1;
            }
            else
            {
                //tambah baru
                db.ShoppingCarts.Add(shoppingCart);
            }

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public ShoppingCart GetDataById(int id)
        {
            var result = (from a in db.ShoppingCarts
                          where a.RecordID == id
                          select a).SingleOrDefault();

            return result;
        }

        public void Edit(ShoppingCart obj)
        {
            var model = GetDataById(obj.RecordID);

            if (model != null)
            {
                model.Quantity = obj.Quantity;
                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else
            {
                throw new Exception("Data tidak ditemukan !");
            }
        }

        public void Delete(int id)
        {
            var model = GetDataById(id);
            if (model != null)
            {
                try
                {
                    db.ShoppingCarts.Remove(model);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
            }
            else
            {
                throw new Exception("Data tidak ditemukan !");
            }
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
