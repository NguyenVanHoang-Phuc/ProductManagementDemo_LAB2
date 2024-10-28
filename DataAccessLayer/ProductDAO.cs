using Azure.Messaging;
using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class ProductDAO
    {
        private static ProductDAO instance = null;
        private static readonly object instanceLock = new object();
        public static ProductDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ProductDAO();
                    }
                    return instance;
                }
            }
        }

        public int GetMaxProductID()
        {
            try
            {
                using (var db = new MyStoreDBContext())
                {
                    int maxId = db.Products.DefaultIfEmpty().Max(p => p.ProductID);
                    return maxId + 1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Product> GetProducts()
        {
            var list = new List<Product>();
            try
            {
                using var db = new MyStoreDBContext();
                list = db.Products.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }
        
        public void SaveProduct(Product p)
        {
            try
            {
                using var db = new MyStoreDBContext();
                db.Products.Add(p);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateProduct(Product p)
        {
            try
            {
                using var db = new MyStoreDBContext();
                db.Entry<Product>(p).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteProduct(Product p)
        {
            try
            {
                using var db = new MyStoreDBContext();
                var p1 = db.Products.FirstOrDefault(x => x.ProductID == p.ProductID);
                db.Products.Remove(p1);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Product GetProductByID(int id)
        {
            using var db = new MyStoreDBContext();
            return db.Products.FirstOrDefault(a => a.ProductID.Equals(id));
        }
    }
}
