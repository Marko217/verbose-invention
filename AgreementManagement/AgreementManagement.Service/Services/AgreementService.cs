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
    public class AgreementService : IAgreementService
    {
        private IAgreementRepository _agrementRepository;

        public AgreementService(IAgreementRepository repository)
        {
            _agrementRepository = repository;
        }
        
        public IEnumerable<Agreement> GetAgreements()
        {
            return _agrementRepository.GetAgreements();
        }
        public Agreement GetAgreement(int id)
        {
            return _agrementRepository.GetAgreement(id);
        }
        public void AddAgreement(Agreement entity)
        {
            _agrementRepository.Add(entity);
        }

        public void UpdateAgreement(Agreement entity)
        {
            _agrementRepository.UpdateNoArh(entity);
        }
        public void DeleteAgreement(int id)
        {
            _agrementRepository.Remove(id);
        }
    }
}
