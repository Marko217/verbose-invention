using AgreementManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgreementManagement.Repo.Repo.Interfaces
{
    public interface IAgreementRepository : IRepository<Agreement, Agreement>
    {
        IEnumerable<Agreement> GetAgreements();
        Agreement GetAgreement(int id);
    }
}
