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
    public class ProductGroupService : IProductGroupService
    {
        private IProductGroupRepository _productGroupRepository;

        public ProductGroupService(IProductGroupRepository repository)
        {
            _productGroupRepository = repository;
        }
        
        public IEnumerable<ProductGroup> GetProductGroups()
        {
            return _productGroupRepository.GetProductGroups();
        }
        public ProductGroup GetProductGroup(int id)
        {
            return _productGroupRepository.GetProductGroup(id);
        }
    }
}