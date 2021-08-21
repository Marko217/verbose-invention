using AgreementManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgreementManagement.Repo.Repo.Interfaces
{
    public interface IProductRepository : IRepository<Product, Product>
    {
        IEnumerable<Product> GetProducts(int? id);
        Product GetProduct(int id);
    }
}
