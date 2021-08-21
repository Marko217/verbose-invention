using AgreementManagement.Data.Entities;
using AgreementManagement.Repo.Repo.Interfaces;
using AgreementManagement.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgreementManagement.Service.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;

        public ProductService(IProductRepository repository)
        {
            _productRepository = repository;
        }
        
        public IEnumerable<Product> GetProducts(int? id)
        {
            return _productRepository.GetProducts(id);
        }
        public Product GetProduct(int id)
        {
            return _productRepository.GetProduct(id);
        }
    }
}