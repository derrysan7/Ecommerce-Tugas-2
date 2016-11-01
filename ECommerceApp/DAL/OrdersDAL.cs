using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using ECommerceApp.Models;
using ECommerceApp.ViewModels;

namespace ECommerceApp.DAL
{
    public class OrdersDAL : IDisposable
    {
        private CommerceModel db = new CommerceModel();



        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}