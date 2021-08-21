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
    public class AgreementRepository : Repository<Agreement, Agreement>, IAgreementRepository
    {
        public AgreementRepository(AgreementManagementDbContext context) : base(context)
        {

        }
        public IEnumerable<Agreement> GetAgreements()
        {
            return entities.Include(x => x.Product).Include(x => x.ProductGroup).Include(x => x.User).ToList();
        }
        public Agreement GetAgreement(int id)
        {
            return entities.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}