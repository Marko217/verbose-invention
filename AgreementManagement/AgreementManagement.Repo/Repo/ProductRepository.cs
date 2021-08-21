using AgreementManagement.Data.Entities;
using AgreementManagement.Repo.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgreementManagement.Repo.Repo
{
    public class ProductRepository : Repository<Product, Product>, IProductRepository
    {
        public ProductRepository(AgreementManagementDbContext context) : base(context)
        {

        }
        public IEnumerable<Product> GetProducts(int? id)
        {
            var query = this.Find(x => x.Active == true && x.ProductGroupId == id);
            return query.Include(x => x.ProductGroup).ToList();
        }

        public Product GetProduct(int id)
        {
            var query = this.Find(x => x.Active == true);
            return query.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}