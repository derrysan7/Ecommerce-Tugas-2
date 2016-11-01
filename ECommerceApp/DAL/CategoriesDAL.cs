using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ECommerceApp.Models;
using ECommerceApp.ViewModels;

namespace ECommerceApp.DAL
{
    public class CategoriesDAL : IDisposable
    {
        private CommerceModel db = new CommerceModel();

        public IQueryable<CategoriesVM> GetData()
        {
            var results = from c in db.Categories
                          orderby c.CategoryName ascending
                          select new CategoriesVM
                          {
                              CategoryID = c.CategoryID,
                              CategoryName = c.CategoryName
                          };
            return results;
        }
        public void Add(Category obj)
        {
            try
            {
                db.Categories.Add(obj);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Category GetDataByID(int CategoryID)
        {
            var result = (from c in db.Categories
                          where c.CategoryID == CategoryID
                          select c).SingleOrDefault();

            return result;
        }

        public void Edit(int CategoryID, Category obj)
        {
            var result = GetDataByID(CategoryID);
            if(result!=null)
            {
                result.CategoryName = obj.CategoryName;
                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public void Delete(int CategoryID)
        {
            var result = GetDataByID(CategoryID);
            if(result!=null)
            {
                db.Categories.Remove(result);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public IQueryable<CategoriesVM> SearchByKriteria(string selectKriteria,
            string txtSearch)
        {
            IQueryable<CategoriesVM> results;
            if (selectKriteria == "CategoryName")
            {
                results = from b in db.Categories
                          where b.CategoryName.ToLower().Contains(txtSearch.ToLower())
                          orderby b.CategoryName
                          select new CategoriesVM
                          {
                              CategoryID = b.CategoryID,
                              CategoryName = b.CategoryName
                          };

            }
            else
            {
                results = from b in db.Categories
                          where b.CategoryName.ToLower().Contains(txtSearch.ToLower())
                          orderby b.CategoryName
                          select new CategoriesVM
                          {
                              CategoryID = b.CategoryID,
                              CategoryName = b.CategoryName
                          };
            }
            return results;
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
