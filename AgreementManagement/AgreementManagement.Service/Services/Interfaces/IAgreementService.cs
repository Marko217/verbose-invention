using AgreementManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgreementManagement.Service.Services.Interfaces
{
    public interface IAgreementService
    {
        IEnumerable<Agreement> GetAgreements();
        Agreement GetAgreement(int id);
        void DeleteAgreement(int id);
        void AddAgreement(Agreement entity);
        void UpdateAgreement(Agreement entity);
    }
}