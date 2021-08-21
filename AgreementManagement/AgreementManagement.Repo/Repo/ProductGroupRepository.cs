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
    public class ProductGroupRepository : Repository<ProductGroup, ProductGroup>, IProductGroupRepository
    {
        public ProductGroupRepository(AgreementManagementDbContext context) : base(context)
        {

        }
        public IEnumerable<ProductGroup> GetProductGroups()
        {
            var query = this.Find(x => x.Active == true);
            return query.ToList();
        }

        public ProductGroup GetProductGroup(int id)
        {
            var query = this.Find(x => x.Active == true);
            return query.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}