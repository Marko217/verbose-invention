using AgreementManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgreementManagement.Repo.Repo.Interfaces
{
    public interface IProductGroupRepository : IRepository<ProductGroup, ProductGroup>
    {
        IEnumerable<ProductGroup> GetProductGroups();
        ProductGroup GetProductGroup(int id);
    }
}
