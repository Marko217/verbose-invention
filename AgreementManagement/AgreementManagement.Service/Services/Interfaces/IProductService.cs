using AgreementManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgreementManagement.Service.Services.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> GetProducts(int? id);
        Product GetProduct(int id);
    }
}