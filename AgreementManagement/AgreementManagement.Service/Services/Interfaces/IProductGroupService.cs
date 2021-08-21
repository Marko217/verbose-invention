using AgreementManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgreementManagement.Service.Services.Interfaces
{
    public interface IProductGroupService
    {
        IEnumerable<ProductGroup> GetProductGroups();
        ProductGroup GetProductGroup(int id);
    }
}