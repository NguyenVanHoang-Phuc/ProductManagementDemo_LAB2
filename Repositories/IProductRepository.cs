using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IProductRepository
    {
        List<Product> GetProducts();

        int GetMaxProductID();

        void SaveProduct(Product p);

        void UpdateProduct(Product p);

        void DeleteProduct(Product p);

        Product GetProductById(int id);
    }
}
